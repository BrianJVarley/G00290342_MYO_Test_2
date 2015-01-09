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

namespace MyoTestv4
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class AdductionAbductionFlexionView : UserControl
    {
        IChannel channel;
        IHub hub;

        public AdductionAbductionFlexionView()
        {
            InitializeComponent();

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

                    }));
                };

                // listen for when the Myo disconnects
                hub.MyoDisconnected += (sender, e) =>
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        statusTbx.Text = "Myo has disconnected!";
                        e.Myo.Vibrate(VibrationType.Medium);
                    }));
                };

                // start listening for Myo data
                channel.StartListening();
            }
        }
    }
}
