using System.Windows.Input;
using MyoSharp.Communication;
using MyoSharp.Device;
using System;
using System.Windows.Threading;
using System.ComponentModel; 
using System.Runtime.CompilerServices; 
using MyoTestv4.AdductionAbductionFlexion;

namespace MyoTestv4
{
    public class AdductionAbductionFlexionViewModel : ObservableObject, IPageViewModel, INotifyPropertyChanged 
    {
        private MyoDeviceModel _myoDevice;
        private DatabaseModel _dataObj;

        public event Action<string> DataChanged;

             
        public AdductionAbductionFlexionViewModel(MyoDeviceModel device, DatabaseModel progressData)
        {
            _myoDevice = device;
            _myoDevice.MyoDeviceStart();

            _dataObj = progressData;

            _myoDevice.StatusUpdated += (update) => 
                { 
                  CurrentStatus = update; 
                  NotifyPropertyChanged("CurrentStatus");
                };


            _myoDevice.PoseUpdated += (update) =>
            {
                PoseStatus = update;
                NotifyPropertyChanged("PoseStatus");
            };

            _myoDevice.DegreesUpdated += (update) =>
            {
                DegreeStatus = update;
                NotifyPropertyChanged("DegreeStatus");

            };

            _myoDevice.StartDegreeUpdated += (update) =>
            {
                StartDegreeStatus = update;
                NotifyPropertyChanged("StartDegreeStatus");
            };

            _myoDevice.EndDegreeUpdated += (update) =>
            {
                EndDegreeStatus = update;
                NotifyPropertyChanged("EndDegreeStatus");
            };

            _dataObj.CommitUpdated += (update) =>
            {
                CommitStatus = update;
                DataChanged("CommitStatus");
            };



        }


        public string CurrentStatus { get; set; }
        public string PoseStatus { get; set; }
        public double DegreeStatus { get; set; }
        public string EndDegreeStatus { get; set; }
        public string StartDegreeStatus { get; set; }
        public string CommitStatus { get; set; }


        

       
        public string Name
        {
            get
            {
                return "Adduction Abduction Flexion";
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        
    }
}