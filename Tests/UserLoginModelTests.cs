using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyoTestv4.Tests
{
    /// <summary>
    /// UserLoginModelTests 
    /// </summary>
    [TestFixture]
    class UserLoginModelTests 
    {
        string ChangedPropertyName = "";

        /// <summary>
        /// Tests the user name property.
        /// </summary>
        [Test]
        public void TestUserNameProperty()
        {
            
            MyoTestv4.Home.UserLoginModel LoginVM = new MyoTestv4.Home.UserLoginModel();
            LoginVM.UserName = "Brian V";
            LoginVM.PropertyChanged += LoginVM_PropertyChanged;

            Assert.AreEqual(LoginVM.UserName, ChangedPropertyName);
       
        }

        /// <summary>
        /// Handles the PropertyChanged event of the LoginVM control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        void LoginVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            ChangedPropertyName = e.PropertyName;

            
        }


        



    }
}
