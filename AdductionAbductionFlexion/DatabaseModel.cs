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
        
        //constants
        const string exerciseValue = "Adduction Flexion";
        //const String RepititionsValue = "21";
        const string genderValue = "Male";
        const string userValue = "Brian J";

        //custom events
        public event Action<string> CommitUpdated;



        
        //method to push progress data to database
        public Task SubmitChanges(string startDegreeStatus, string endDegreeStatus)
        {
            var table = App.MobileService.GetTable<Item>();
            Item item = new Item { Date = " " + DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"), User = userValue, Exercise = exerciseValue, Gender = genderValue, Painful_Arc_Start = startDegreeStatus, Painful_Arc_End = endDegreeStatus };
            return table.InsertAsync(item);
        }


        /*
        //method to poll progress data from database
        public Task PullChanges()
        {

            

        }
        */
       


    }
}
