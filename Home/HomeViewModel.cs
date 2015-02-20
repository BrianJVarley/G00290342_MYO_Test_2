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

        private UserLoginModel _loginObj;

        //bool to set view visibility 
        //between browser and text block.
        private bool _loggedIn = false;
        private string _userName;


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


        public UserLoginModel LoginObject
        {
            get { return _loginObj; }
            set
            {
                if (value == _loginObj) return;
                _loginObj = value;
                OnPropertyChanged();
            }
        }



        public string UserName
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
                    OnPropertyChanged("UserName");
                }
            }
        }



        public void initLogin(NavigationEventArgs e)
        {

            if (e.Uri.ToString().StartsWith("http://www.facebook.com/connect/login_success.html"))
            {



                this._loginObj.AccessToken = e.Uri.Fragment.Split('&')[0].Replace("#access_token=", "");
                this._loginObj.FbClient = new FacebookClient(this._loginObj.AccessToken);

                LoggedIn = true;


                this._loginObj.FbC = this._loginObj.FbClient;

                dynamic me = this._loginObj.FbClient.Get("Me");

                //set profile fields to string variables
                this._loginObj.UserName = me.name;
                this._loginObj.Gender = me.gender;
                this._loginObj.Link = me.link;
               

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
