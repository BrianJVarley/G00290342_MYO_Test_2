using MyoSharp.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoTestv4.Tests
{
    
    class MyoMock : IMyo
    {

        /// <summary>
        /// Occurs when [mock event].
        /// </summary>
        public event EventHandler MockEvent;


        public void OnMockEvent()
        {
            var handler = MockEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }




        public IntPtr Handle
        {
            get { throw new NotImplementedException(); }
        }

        public void Lock()
        {
            throw new NotImplementedException();
        }

        public void RequestRssi()
        {
            throw new NotImplementedException();
        }

        public void SetEmgStreaming(bool enabled)
        {
            throw new NotImplementedException();
        }

        public void Unlock(UnlockType type)
        {
            throw new NotImplementedException();
        }

        public void Vibrate(VibrationType type)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<AccelerometerDataEventArgs> AccelerometerDataAcquired;

        public event EventHandler<MyoEventArgs> ArmLost;

        public event EventHandler<ArmRecognizedEventArgs> ArmRecognized;

        public event EventHandler<MyoEventArgs> Connected;

        public event EventHandler<MyoEventArgs> Disconnected;

        public event EventHandler<EmgDataEventArgs> EmgDataAcquired;

        public event EventHandler<GyroscopeDataEventArgs> GyroscopeDataAcquired;

        public event EventHandler<MyoEventArgs> Locked;

        public event EventHandler<OrientationDataEventArgs> OrientationDataAcquired;

        public event EventHandler<PoseEventArgs> PoseChanged;

        public event EventHandler<RssiEventArgs> Rssi;

        public event EventHandler<MyoEventArgs> Unlocked;

        public MyoSharp.Math.Vector3F Accelerometer
        {
            get { throw new NotImplementedException(); }
        }

        public Arm Arm
        {
            get { throw new NotImplementedException(); }
        }

        public IEmgData EmgData
        {
            get { throw new NotImplementedException(); }
        }

        public MyoSharp.Math.Vector3F Gyroscope
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsConnected
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsUnlocked
        {
            get { throw new NotImplementedException(); }
        }

        public MyoSharp.Math.QuaternionF Orientation
        {
            get { throw new NotImplementedException(); }
        }

        public MyoSharp.Poses.Pose Pose
        {
            get { throw new NotImplementedException(); }
        }

        public XDirection XDirectionOnArm
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
     
}
