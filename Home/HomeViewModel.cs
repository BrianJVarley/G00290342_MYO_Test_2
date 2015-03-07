using Facebook;
using MyoTestv4.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Navigation;

// <author>Brian Varley</author>
// <summary>HomeViewModel</summary>

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


        /// <summary>
        /// Initializes a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary>
        /// <param name="login">The login.</param>
        public HomeViewModel(UserLoginModel login)
        {

            _loginObj = login;



        }

        /// <summary>
        /// Gets or sets a value indicating whether [logged in].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [logged in]; otherwise, <c>false</c>.
        /// </value>
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


        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public static string Gender
        {
            get { return _gender; }
            set
            {
                if (String.Equals(value, _gender, StringComparison.OrdinalIgnoreCase)) return;
                _gender = value;
                //OnPropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the login object.
        /// </summary>
        /// <value>
        /// The login object.
        /// </value>
        public static UserLoginModel LoginObject
        {
            get { return _loginObj; }
            set
            {
                if (value == _loginObj) return;
                _loginObj = value;
            }
        }



        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
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


        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return "Home Page";
            }
        }



        /// <summary>
        /// Initializes the login.
        /// </summary>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
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




       
    }
}