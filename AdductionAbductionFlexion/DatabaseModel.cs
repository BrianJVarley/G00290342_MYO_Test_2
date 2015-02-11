using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Mantin.Controls.Wpf;
using Mantin.Controls.Wpf.Notification;


namespace MyoTestv4.AdductionAbductionFlexion
{
    class DatabaseModel
    {

        

        //method to push progress data to database
        public async void pushData()
        {

            var table = App.MobileService.GetTable<Item>();
            Item item = new Item { Repititions = "22", Date = " " + DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"), User = HomeView.Name, Exercise = "Abduction flexion", Gender = HomeView.Gender, Painful_Arc_Start = "5", Painful_Arc_End = "76"};
            await App.MobileService.GetTable<Item>().InsertAsync(item);

            new ToastPopUp("Submit Succeeded!", "Progress data submitted succesfully", NotificationType.Information)
            {
                Background = new LinearGradientBrush(System.Windows.Media.Color.FromRgb(0, 189, 222), System.Windows.Media.Color.FromArgb(255, 10, 13, 248), 90),
                BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 189, 222)),
                FontColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255))
            }.Show();

        }

       


    }
}
