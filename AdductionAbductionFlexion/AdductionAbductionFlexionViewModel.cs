﻿using System.Windows.Input;
using MyoSharp.Communication;
using MyoSharp.Device;
using System;
using System.Windows.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyoTestv4.AdductionAbductionFlexion;
using MyoTestv4.Home;



// <author>Brian Varley</author>
// <summary>AdductionAbductionViewModel</summary>

namespace MyoTestv4
{
    public class AdductionAbductionFlexionViewModel : ObservableObject, IPageViewModel, INotifyPropertyChanged
    {
        private MyoDeviceModel _myoDevice;
        private DatabaseModel _dataObj;
        
        

        public event Action<string> DataChanged;


        public AdductionAbductionFlexionViewModel(MyoDeviceModel device, DatabaseModel progressData)
        {

            DataSubmitCommand = new RelayCommand (this.SaveChangesToPersistence);

            _myoDevice = device;
            _myoDevice.MyoDeviceStart();

            _dataObj = progressData;

            _myoDevice.StatusUpdated += (update) =>
            {
                CurrentStatus = update;   
            };


            _myoDevice.PoseUpdated += (update) =>
            {
                PoseStatus = update;   
            };


            _myoDevice.PainfulArcDegreeUpdated += (update) =>
            {
                PainfulArcStatus = update;
            };


            _myoDevice.DegreesUpdated += (update) =>
            {
                DegreeStatus = update;
            };

            _myoDevice.StartDegreeUpdated += (update) =>
            {
                StartDegreeStatus = update;    
            };

            _myoDevice.EndDegreeUpdated += (update) =>
            {
                EndDegreeStatus = update;    
            };
        }


       
        private string commitStatus;
        public string CommitStatus
        {
            get { return this.commitStatus; }
            set
            {
                if (this.commitStatus != value)
                {
                    this.commitStatus = value;
                    this.RaisePropertyChanged("CommitStatus");
                }
            }
        }


        private string currentStatus;
        public string CurrentStatus
        {
            get { return this.currentStatus; }
            set
            {
                if (this.currentStatus != value)
                {
                    this.currentStatus = value;
                    this.RaisePropertyChanged("CurrentStatus");
                }
            }
        }


        private double painfulArcStatus;
        public double PainfulArcStatus
        {
            get { return this.painfulArcStatus; }
            set
            {
                if (this.painfulArcStatus != value)
                {
                    this.painfulArcStatus = value;
                    this.RaisePropertyChanged("PainfulArcStatus");
                }
            }
        }



        private string poseStatus;
        public string PoseStatus
        {
            get { return this.poseStatus; }
            set
            {
                if (this.poseStatus != value)
                {
                    this.poseStatus = value;
                    this.RaisePropertyChanged("PoseStatus");
                }
            }
        }


        private double degreeStatus;
        public double DegreeStatus
        {
            get { return this.degreeStatus; }
            set
            {
                if (this.degreeStatus != value)
                {
                    this.degreeStatus = value;
                    this.RaisePropertyChanged("DegreeStatus");
                }
            }
        }



        private double endDegreeStatus;
        public double EndDegreeStatus
        {
            get { return this.endDegreeStatus; }
            set
            {
                if (this.endDegreeStatus != value)
                {
                    this.endDegreeStatus = value;
                    this.RaisePropertyChanged("EndDegreeStatus");
                }
            }
        }


        private double startDegreeStatus;
        public double StartDegreeStatus
        {
            get { return this.startDegreeStatus; }
            set
            {
                if (this.startDegreeStatus != value)
                {
                    this.startDegreeStatus = value;
                    this.RaisePropertyChanged("StartDegreeStatus");
                }
            }
        }


    
        public ICommand DataSubmitCommand { get; private set; }


        public void SaveChangesToPersistence(object param)
        {


            DatabaseModel.SubmitChangesAsync(StartDegreeStatus, EndDegreeStatus, HomeViewModel.LoginObject.UserName, HomeViewModel.LoginObject.Gender);
            this.CommitStatus = "Data committed successfully!";

            DataChanged(CommitStatus);

        }


        public string Name
        {
            get
            {
                return "Adduction Abduction Flexion";
            }
        }
    }
}