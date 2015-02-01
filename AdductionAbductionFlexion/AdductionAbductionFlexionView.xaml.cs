using MyoSharp.Communication;
using MyoSharp.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


namespace MyoTestv4
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class AdductionAbductionFlexionView : UserControl
    {
        IChannel channel;
        IHub hub;

        

        private double CALLIBRATION_FACTOR = 61.64;
        private double PITCH_MAX = -1.46;
        private double PITCH_MIN =  1.46;

        private int myoCorrected = 0;

        
        
        //pitch class field
        private int pitch = 0;

        private double degreeOutput;

        private string startingDegree;
        private string endDegree;

       
        

        #region Methods

        public AdductionAbductionFlexionView()
        {
            InitializeComponent();
            this.Loaded += AdductionAbductionFlexionView_Loaded;

           


           
            // create a hub that will manage Myo devices for us
            channel = Channel.Create(ChannelDriver.Create(ChannelBridge.Create()));
            hub = Hub.Create(channel);
            {

                // listen for when the Myo connects
                hub.MyoConnected += (sender, e) =>
                {

                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        statusTbx.Text = "Myo has connected! " + e.Myo.Handle;
                        e.Myo.Vibrate(VibrationType.Short);

                        // unlock the Myo so that it doesn't keep locking between our poses
                        e.Myo.Unlock(UnlockType.Hold);

                        e.Myo.PoseChanged += Myo_PoseChanged;

                        e.Myo.OrientationDataAcquired += Myo_OrientationDataAcquired;

                        

                    }));
                };

                // listen for when the Myo disconnects
                hub.MyoDisconnected += (sender, e) =>
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        statusTbx.Text = "Myo has disconnected!";
                        e.Myo.Vibrate(VibrationType.Medium);
                        e.Myo.OrientationDataAcquired -= Myo_OrientationDataAcquired;
                        e.Myo.PoseChanged -= Myo_PoseChanged;
                        
                        
                    }));
                };

                // start listening for Myo data
                channel.StartListening();
            }
        }

        void AdductionAbductionFlexionView_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Closing += new CancelEventHandler(window_Closing);
        }

        async void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Leave exercise", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {

                    var table = App.MobileService.GetTable<Item>();
                    Item item = new Item { Repititions = "22", Date = " " + DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"), User = HomeView.Name, Exercise = "Abduction flexion", Gender = HomeView.Gender };
                    await App.MobileService.GetTable<Item>().InsertAsync(item);


                }
                catch (Exception ex)
                {

                    statusTbx.Text = "Commit fail: " + ex.Message;

                }



            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                return;

            }

        }

        

        

        #endregion


        #region Event Handlers


        private void Myo_PoseChanged(object sender, PoseEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                //need to measure abduction of arm from 0 to 180 degrees.
                poseStatusTbx.Text = "Pose " + e.Myo.Pose + "   detected" ;

                

          
            }));    
        }




        private void Myo_OrientationDataAcquired(object sender, OrientationDataEventArgs e)
        {
           App.Current.Dispatcher.Invoke((Action)(() =>
            {

                //need to record degree reading when pose made that
                //signifies begining of painful arc.
                //then specify a second pose that signals the end of
                //painful arc and store arc reading, eg 92dg - 108dg


                //myo indicator must be facing down or degrees will be inverted.
                degreeOutput = ((e.Pitch + PITCH_MIN) * CALLIBRATION_FACTOR);

                degreeOfAbductionTbx.Text = "Degree: " + degreeOutput;

                if (e.Myo.Pose == Pose.Fist)
                {
                    endDegree = string.Empty;
                    if (string.IsNullOrEmpty(startingDegree))
                    {
                        startingDegree = "start: " + degreeOutput;
                    }

                    //get the start reading of the painful arc
                    painfulArcStartTbx.Text = startingDegree;

                }
                //then get the finish reading of the painful arc
                //after the fist pose has been let go, indicating
                //end of pain in movement
                else
                {
                    startingDegree = string.Empty;
                    if (string.IsNullOrEmpty(endDegree))
                    {
                        endDegree = "end: " + degreeOutput;
                    }
                    painfulArcEndTbx.Text = endDegree;

                }




            })); 


        }

     
        #endregion
    }
}
