using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyoSharp.Poses;
using MyoSharp.Device;
using MyoTestv4.AdductionAbductionFlexion;

namespace MyoTestv4.Tests
{
    [TestFixture]
    class MyoDeviceTests 
    {


        [Test]
        public void MockTest()
        {
            
            //unit test the Myo device pose changed code
            //need to mock device to test this.
            
            var mockMyo = new MyoMock();
            mockMyo.OnMockEvent();
            //Assert.AreEqual(1, device.Count);
            

        }





        
    }
}
