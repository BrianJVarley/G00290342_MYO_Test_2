using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

// <author>Brian Varley</author>
// <summary>Item</summary>
namespace MyoTestv4.AdductionAbductionFlexion
{
    /// <summary>
    /// Database Item Class
    /// </summary>
    public class Item
    {
        ////get and set Item table fields
        public string Id { get; set; }
        public string User { get; set; }
        public string Exercise { get; set; }
        public string Date { get; set; }
        public string Painful_Arc_Start { get; set; }
        public string Painful_Arc_End { get; set; }
        public string Gender { get; set; }
        public string Max_Range { get; set; }
        
          
        
    }

    
}
