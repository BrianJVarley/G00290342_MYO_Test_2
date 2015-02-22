using Facebook;
using MyoTestv4.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Navigation;

namespace MyoTestv4
{
    public class HomeViewModel : ObservableObject, IPageViewModel, INotifyPropertyChanged
    {

        private static UserLoginModel _loginObj;

        //bool to set view visibility 
        //between browser and text block.
        private bool _loggedIn = false;
        private static string _userName;
        private static string _gender;


        public HomeViewModel(UserLoginModel login)
        {

            _loginObj = login;



        }

        public bool LoggedIn
        {
            get { return _loggedIn; }
            set
            {
                if (Equals(value, _loggedIn)) return;
                _loggedIn = value;
                OnPropertyChanged("LoggedIn");
            }
        }


        public string Gender
        {
            get 
            {
                if (_loginObj != null)
                {
                    _gender = _loginObj.Gender;
                }
                else
                {
                    _gender = "Not Logged In";
                }
                return _gender;             
            }
                 
            set
            {
                if (_loginObj != null)
                {
                    if (Equals(_loginObj.Gender, value)) return;
                    _loginObj.Gender = value;
                    OnPropertyChanged("Gender");
                }
            }
        }


        public static UserLoginModel LoginObject
        {
            get { return _loginObj; }
            set
            {
                if (value == _loginObj) return;
                _loginObj = value;
            }
        }



        public static string UserName
        {
            get
            {
                if (_loginObj != null)
                {
                    _userName = _loginObj.UserName;
                }
                else
                {
                    _userName = "Not Logged In";
                }
                return _userName;
            }
            set
            {
                if (_loginObj != null)
                {
                    if (Equals(_loginObj.UserName, value)) return;
                    _loginObj.UserName = value;
                    //OnPropertyChanged("UserName");
                }
            }
        }



        public void initLogin(NavigationEventArgs e)
        {

            if (e.Uri.ToString().StartsWith("http://www.facebook.com/connect/login_success.html"))
            {



                _loginObj.AccessToken = e.Uri.Fragment.Split('&')[0].Replace("#access_token=", "");
                _loginObj.FbClient = new FacebookClient(_loginObj.AccessToken);

                LoggedIn = true;


                _loginObj.FbC = _loginObj.FbClient;

                dynamic me = _loginObj.FbClient.Get("Me");

                //set profile fields to string variables
                _loginObj.UserName = me.name;
                _loginObj.Gender = me.gender;
                _loginObj.Link = me.link;


            }


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