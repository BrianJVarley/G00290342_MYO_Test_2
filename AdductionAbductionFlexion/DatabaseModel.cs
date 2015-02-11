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
    public class DatabaseModel
    {
        //custom events
        public event Action<string> CommitUpdated;

        //constants
        private String ExerciseValue = "Adduction Flexion";
        private String RepititionsValue = "21";
        private String GenderValue = "Male";
        private String UserValue = "Brian J";


        
        //method to push progress data to database
        public async void pushData(String Painful_Arc_Start, String Painful_Arc_End)
        {

            var table = App.MobileService.GetTable<Item>();
            Item item = new Item { Repititions = RepititionsValue, Date = " " + DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"), User = UserValue, Exercise = ExerciseValue, Gender = GenderValue, Painful_Arc_Start = "5", Painful_Arc_End = "76"};
            await App.MobileService.GetTable<Item>().InsertAsync(item);

            var handler = CommitUpdated;
            if (handler != null)
                handler("Data committed successfully!");

        }


        //method to poll progress data from database
        public async void pullData()
        {

            

        }
       


    }
}
