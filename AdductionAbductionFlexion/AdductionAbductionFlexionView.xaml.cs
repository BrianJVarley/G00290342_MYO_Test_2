﻿using MyoSharp.Communication;
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
using Mantin.Controls.Wpf.Notification;


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
            this.DataContext = new AdductionAbductionFlexionViewModel(new MyoDeviceModel(), new DatabaseModel()); 
            this.Loaded += AdductionAbductionFlexionView_Loaded;
            this.Loaded -= AdductionAbductionFlexionView_Loaded;

            ((AdductionAbductionFlexionViewModel)DataContext).DataChanged += x =>
            {
           
                new ToastPopUp("Submit Succeeded!", "Progress data submitted succesfully", NotificationType.Information)
                {
                    Background = new LinearGradientBrush(System.Windows.Media.Color.FromRgb(0, 189, 222), System.Windows.Media.Color.FromArgb(255, 10, 13, 248), 90),
                    BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 189, 222)),
                    FontColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255))
                }.Show();
            
            };


        }
        
           
            

        void AdductionAbductionFlexionView_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Closing += new CancelEventHandler(window_Closing);


        }

        public void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {


            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Leave Application", System.Windows.MessageBoxButton.YesNo);
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
