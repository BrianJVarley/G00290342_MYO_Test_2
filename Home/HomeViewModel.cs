using MyoTestv4.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MyoTestv4
{
    public class HomeViewModel : ObservableObject, IPageViewModel, INotifyPropertyChanged
    {

        private UserLoginModel _loginObj;



        public HomeViewModel(UserLoginModel login)
        {

            _loginObj = login;
            //_myoDevice.MyoDeviceStart();

           
        }


        public string Name
        {
            get
            {
                return "Home Page";
            }
        }
    }
}
