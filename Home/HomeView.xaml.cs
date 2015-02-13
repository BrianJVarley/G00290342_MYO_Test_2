using Facebook;
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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        private FacebookClient FBClient;
        private FacebookClient fbC;
        public static string AccessToken { get; set; }
        public static string Name;
        public static string Gender;
        public static string Link;

        


        public HomeView()
        {
            InitializeComponent();
            WBrowser.Navigate(new Uri("https://graph.facebook.com/oauth/authorize?client_id=559878724040104&redirect_uri=http://www.facebook.com/connect/login_success.html&type=user_agent&display=popup").AbsoluteUri);
            //619560198173727

        }

        private void WBrowser_OnNavigated(object sender, NavigationEventArgs e)
        {

            if (e.Uri.ToString().StartsWith("http://www.facebook.com/connect/login_success.html"))
            {

                AccessToken = e.Uri.Fragment.Split('&')[0].Replace("#access_token=", "");
                FBClient = new FacebookClient(AccessToken);

                WBrowser.Visibility = Visibility.Hidden;
                TBInfos.Visibility = Visibility.Visible;

                fbC = FBClient;
                dynamic me = FBClient.Get("Me");

                TBInfos.Text = "Name : " + (me.name ?? (object)string.Empty).ToString() + "\n\r"
                               + "Gender : " + (me.gender ?? (object)string.Empty).ToString() + "\n\r"
                               + "Link : " + (me.link ?? (object)string.Empty).ToString();

                //set profile fields to string variables
                Name = me.name;
                Gender = me.gender;
                Link = me.link;

            }
        }
    }
}
