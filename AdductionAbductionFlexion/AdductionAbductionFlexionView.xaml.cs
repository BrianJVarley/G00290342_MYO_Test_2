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
using Mantin.Controls.Wpf;
using Mantin.Controls.Wpf.Notification;
using System.Drawing;



namespace MyoTestv4
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class AdductionAbductionFlexionView : UserControl
    {

        IChannel channel;
        IHub hub;


        

        
        //Constants
        private double CALLIBRATION_FACTOR = 61.64;
        private double PITCH_MAX = -1.46;
        private double PITCH_MIN =  1.46;

    
        private int pitch = 0;
        private string startingDegree;
        private string endDegree;
        private int myoCorrected = 0;
        private int repCntr = 0;
        //variable that stores dynamic values for
        //degree output of the device as the user
        //moves there arm through the movement.

        //need to bind this variable to the "gauge"
        //control so that degree readings update the 
        //gauge in real time
        public double degreeOutput { get; set; }

        

        

        #region Methods

        public AdductionAbductionFlexionView()
        {
            InitializeComponent();
            this.DataContext = new AdductionAbductionFlexionViewModel();
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
                await SubmitProgress();



            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                return;

            }

        }


        //submit button click event
        private async void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            await SubmitProgress();

        }

        //method to submit user progress data to database
        private async System.Threading.Tasks.Task SubmitProgress()
        {
            try
            {

                var table = App.MobileService.GetTable<Item>();
                Item item = new Item { Repititions = repCntr.ToString(), Date = " " + DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"), User = HomeView.Name, Exercise = "Abduction flexion", Gender = HomeView.Gender, Painful_Arc_Start = startingDegree, Painful_Arc_End = endDegree};
                await App.MobileService.GetTable<Item>().InsertAsync(item);
               
                new ToastPopUp("Submit Succeeded!", "Progress data submitted succesfully", NotificationType.Information)
                {
                    Background = new LinearGradientBrush(System.Windows.Media.Color.FromRgb(0, 189, 222), System.Windows.Media.Color.FromArgb(255, 10, 13, 248), 90),
                    BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 189, 222)),
                    FontColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255))
                }.Show();


            }
            catch (Exception ex)
            {

                
                statusTbx.Text = "Commit fail: " + ex.Message;
                new ToastPopUp("Submit Failed!", "Progress data did not submit correctly, please check you network connection", NotificationType.Information)
                {
                    Background = new LinearGradientBrush(System.Windows.Media.Color.FromRgb(0, 0, 51), System.Windows.Media.Color.FromArgb(255, 10, 13, 248), 90),
                    BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 51)),
                    FontColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255,255,255))
                }.Show();



            }
        }

        

        

        #endregion


        #region Event Handlers


        private void Myo_PoseChanged(object sender, PoseEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
               
                poseStatusTbx.Text = "Pose " + e.Myo.Pose + "   detected" ;

              
            }));    
        }




        private void Myo_OrientationDataAcquired(object sender, OrientationDataEventArgs e)
        {
           App.Current.Dispatcher.Invoke((Action)(() =>
            {
                
                

                //need to add a repitition count, to increment each time 
                //a repitition from 0 --> 180 degrees is completed.

                //Current implementation not ideal as a repitition will counted,
                //when the user has cmpleted a half repitition.
                //For example user's arm could move from 90degrees to 180 degrees
                //and still be counted as a valid repitition.

                //Need to form a condition that only increments the counter when 
                //the user has started at 0 degrees and ended at 180.

                if(degreeOutput == 180)
                {
                    repCntr++;
                    repCntTblk.Text = repCntr.ToString();

                }
                

                //myo indicator must be facing down or degrees will be inverted.
                degreeOutput = ((e.Pitch + PITCH_MIN) * CALLIBRATION_FACTOR);

                degreeOfAbductionTbx.Text = "Degree: " + degreeOutput;

                //need to measure abduction of arm from 0 to 180 degrees.
                //need to record degree reading when pose made that
                //signifies begining of painful arc.
                if (e.Myo.Pose == Pose.FingersSpread)
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
