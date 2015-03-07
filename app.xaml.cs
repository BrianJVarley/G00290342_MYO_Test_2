using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace MyoTestv4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The mobile service
        /// </summary>
        public static MobileServiceClient MobileService = new MobileServiceClient(
            "https://progressreportdb.azure-mobile.net/",
                "yZdIWPDVuQQruDylqLZCVXvKsKRsKD40"
            );


        /// <summary>
        /// Raises the <see cref="E:Startup"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            
            ApplicationView app = new ApplicationView();
            ApplicationViewModel context = new ApplicationViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}
