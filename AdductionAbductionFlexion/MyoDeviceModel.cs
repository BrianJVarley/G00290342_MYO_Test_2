using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyoSharp.Communication;
using MyoSharp.Device;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAzure.MobileServices;
using MyoTestv4.AdductionAbductionFlexion;
using MyoSharp.Poses;
using System.ComponentModel;

namespace MyoTestv4.AdductionAbductionFlexion
{
    public class MyoDeviceModel
    {

        private IChannel channel;
        private IHub hub;

        public event Action<string> StatusUpdated;
        public event Action<string> PoseUpdated;
        public event Action<double> DegreesUpdated;
        public event Action<string> StartDegreeUpdated;
        public event Action<string> EndDegreeUpdated;

        //Constants
        private const double CALLIBRATION_FACTOR = 61.64;
        private const double PITCH_MAX = -1.46;
        private const double PITCH_MIN = 1.46;


        private int myoCorrected = 0;
        private int pitch = 0;
        private string startingDegree;
        private string endDegree;
        private double degreeOutput;


        #region Methods

        public void MyoDeviceStart()
        {
            // create a hub that will manage Myo devices for us
            this.channel = Channel.Create(ChannelDriver.Create(ChannelBridge.Create()));
            hub = Hub.Create(channel);
            {
               // listen for when the Myo connects
                this.hub.MyoConnected += (sender, e) =>
                {
                    var handler = StatusUpdated;
                    if (handler != null)
                    {
                        handler("Connected!");
                    }
                        
                    e.Myo.Vibrate(VibrationType.Short);

                    // unlock the Myo so that it doesn't keep locking between our poses
                    e.Myo.Unlock(UnlockType.Hold);

                    e.Myo.PoseChanged += Myo_PoseChanged;

                    e.Myo.OrientationDataAcquired += Myo_OrientationDataAcquired;

                };

                // listen for when the Myo disconnects
                hub.MyoDisconnected += (sender, e) =>
                {

                    var handler = StatusUpdated;
                    if (handler != null)
                    {
                        handler("Disconnected!");
                    }
                    e.Myo.Vibrate(VibrationType.Medium);
                    e.Myo.OrientationDataAcquired -= Myo_OrientationDataAcquired;
                    e.Myo.PoseChanged -= Myo_PoseChanged;
                        
                 
                };

                // start listening for Myo data
                channel.StartListening();
            }
        }


        private void Myo_PoseChanged(object sender, PoseEventArgs e)
        {   
                var handler = PoseUpdated;
                if (handler != null)
                {
                    handler("Pose: " + e.Myo.Pose);
                }

                e.Myo.Vibrate(VibrationType.Short);
        }


        private void Myo_OrientationDataAcquired(object sender, OrientationDataEventArgs e)
        { 
                //myo indicator must be facing down or degrees will be inverted.
                degreeOutput = ((e.Pitch + PITCH_MIN) * CALLIBRATION_FACTOR);

                var handler = DegreesUpdated;
                if (handler != null)
                {
                    handler(degreeOutput);
                }

                //painful arc logic
                if (e.Myo.Pose == Pose.WaveOut)
                {
                    endDegree = string.Empty;
                    if (string.IsNullOrEmpty(startingDegree))
                    {
                        startingDegree = degreeOutput.ToString();
                    }
                    var handlerArcStart = StartDegreeUpdated;
                    if (handlerArcStart != null)
                    {
                        handlerArcStart(startingDegree);
                    }
                       

                }
                
                else
                {
                    startingDegree = string.Empty;
                    if (string.IsNullOrEmpty(endDegree))
                    {
                        endDegree = degreeOutput.ToString();
                    }
                    var handlerArcEnd = EndDegreeUpdated; 
                    if (handlerArcEnd != null)
                    {
                        handlerArcEnd(endDegree);
                    }
                    
                }
                
        }


        public object PoseChanged { get; set; }
    }
         
}

 #endregion


