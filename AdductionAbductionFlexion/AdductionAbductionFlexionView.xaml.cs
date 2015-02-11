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
        

        #region Methods

        public AdductionAbductionFlexionView()
        {
            InitializeComponent();
            this.DataContext = new AdductionAbductionFlexionViewModel(new MyoDeviceModel()); 
            this.Loaded += AdductionAbductionFlexionView_Loaded;


        }
        
           
            

        void AdductionAbductionFlexionView_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Closing += new CancelEventHandler(window_Closing);


        }

        public void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Leave exercise", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

        }

        

        

        #endregion


        
    }
}
