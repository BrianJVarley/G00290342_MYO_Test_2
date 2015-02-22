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
        const string ExerciseValue = "Adduction Flexion";
        
        

       
        
        //method to push progress data to database
        public static Task SubmitChangesAsync(string StartDegreeStatus, string EndDegreeStatus, string UserName)
        {
            var table = App.MobileService.GetTable<Item>();
            Item item = new Item { Date = " " + DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"), User = UserName, Exercise = ExerciseValue, Painful_Arc_Start = StartDegreeStatus, Painful_Arc_End = EndDegreeStatus };
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
