using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Mantin.Controls.Wpf;
using Mantin.Controls.Wpf.Notification;
using Microsoft.WindowsAzure.MobileServices;

// <author>Brian Varley</author>
// <summary>DatabaseModel</summary>

namespace MyoTestv4.AdductionAbductionFlexion
{
    public class DatabaseModel
    {
        
        //constants
        const string ExerciseValue = "Adduction Flexion";
        
        
       
        /// <summary>
        /// Submits the changes asynchronously.
        /// </summary>
        /// <param name="StartDegreeStatus">The start degree status.</param>
        /// <param name="EndDegreeStatus">The end degree status.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Gender">The gender.</param>
        /// <returns></returns>
        public static Task SubmitChangesAsync(double StartDegreeStatus, double EndDegreeStatus, string UserName, string Gender)
        {
            var table = App.MobileService.GetTable<Item>();
            Item item = new Item { Date = " " + DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"), User = UserName, Exercise = ExerciseValue, Painful_Arc_Start = StartDegreeStatus.ToString(), Painful_Arc_End = EndDegreeStatus.ToString() ,Gender = Gender};
            return table.InsertAsync(item);
        }




        /// <summary>
        /// Queries the table and returns a list of table entries.
        /// </summary>
        /// <returns></returns>
        public async static Task<List<Item>> QueryTable()
        {
            var table = App.MobileService.GetTable<Item>();
            IMobileServiceTableQuery<Item> query = table.
                OrderBy(item => item.User);

            return await query.ToListAsync();
        }
       
         
       
        

    }
}
