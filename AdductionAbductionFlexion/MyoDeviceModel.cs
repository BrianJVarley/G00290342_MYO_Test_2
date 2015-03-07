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


// <author>Brian Varley</author>
// <summary>MyoDeviceModel</summary>
namespace MyoTestv4.AdductionAbductionFlexion
{
    public class MyoDeviceModel
    {

        private IChannel channel;
        private IHub hub;

        public event Action<string> StatusUpdated;
        public event Action<string> PoseUpdated;
        public event Action<double> DegreesUpdated;
        public event Action<double> StartDegreeUpdated;
        public event Action<double> EndDegreeUpdated;
        public event Action<double> PainfulArcDegreeUpdated;
        
        //Constants
        private const double CALLIBRATION_FACTOR = 61.64;
        //private const double PITCH_MAX = -1.46;
        //Need to calibrate start point
        //by setting PITCH_MIN to current pitch
        //when calibration button clicked.
        private double _pitchMin = 1.46;
        private double _startingDegree;
        private double _endDegree;
        private double _degreeOutputDouble;
        private double _degreeOutput;
        private double _painfulArcOutput;
        

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

                    // unlock the Myo so that it doesn't keep locking between poses
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
                    handler(e.Myo.Pose.ToString());
                }

                e.Myo.Vibrate(VibrationType.Short);
        }


        private void Myo_OrientationDataAcquired(object sender, OrientationDataEventArgs e)
        {


            //myo indicator must be facing down or degrees will be inverted.
            _degreeOutputDouble = ((e.Pitch + _pitchMin) * CALLIBRATION_FACTOR);
            _degreeOutputDouble = Math.Round(_degreeOutputDouble, 2);
            _degreeOutput = _degreeOutputDouble;

            var handler = DegreesUpdated;
            if (handler != null)
            {
                handler(_degreeOutput);
            }

            //painful arc logic
            if (e.Myo.Pose == Pose.Fist)
            {

                //provide haptic feedback, to indicate that 
                //painfull arc is being tracked.
                // e.Myo.Vibrate(VibrationType.Short);//buggy

                _endDegree = 0;
                if (_startingDegree == 0)
                {
                    _startingDegree = _degreeOutput;
                }
                var handlerArcStart = StartDegreeUpdated;
                if (handlerArcStart != null)
                {
                    handlerArcStart(_startingDegree);


                }


            }

            else
            {
                _startingDegree = 0;
                if (_endDegree == 0)
                {
                    _endDegree = _degreeOutput;
                }
                var handlerArcEnd = EndDegreeUpdated;
                if (handlerArcEnd != null)
                {
                    handlerArcEnd(_endDegree);

                    _painfulArcOutput = _endDegree - _startingDegree;
                    var handlerPainfulArc = PainfulArcDegreeUpdated;
                    if (handlerPainfulArc != null)
                    {
                        handlerPainfulArc(_painfulArcOutput);
                        
                    }

                }

            }
        }



        //method to calibrate minimum pitch reading
        public static void CallibratePitchMinimumReading()
        {
            //_pitchMin = e.Pitch;
            
            
        }




        public object PoseChanged { get; set; }
    }
         
}

 #endregion


