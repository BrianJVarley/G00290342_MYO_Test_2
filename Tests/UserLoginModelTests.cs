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
    [TestFixture]
    class UserLoginModelTests : INotifyPropertyChanged
    {
        string ChangedPropertyName = "";

        [Test]
        public void TestUserNameProperty()
        {
            
            MyoTestv4.Home.UserLoginModel LoginVM = new MyoTestv4.Home.UserLoginModel();
            LoginVM.UserName = "Brian V";
            LoginVM.PropertyChanged += LoginVM_PropertyChanged;

            Assert.AreEqual(LoginVM.UserName, ChangedPropertyName);
       
        }

        void LoginVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            ChangedPropertyName = e.PropertyName;

            
        }


        



    }
}
