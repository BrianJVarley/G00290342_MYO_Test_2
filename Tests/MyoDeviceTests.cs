using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoTestv4.Tests
{
    [TestFixture]
    class MyoDeviceTests
    {

        MyoTestv4.AdductionAbductionFlexion.MyoDeviceModel myo = new AdductionAbductionFlexion.MyoDeviceModel();

        [Test]
        public void TestMyoDevicePoseChanged()
        {
            
            //unit test the Myo device pose changed code
            //myo.PoseChanged += Myo_PoseChanged


        }





        
    }
}
