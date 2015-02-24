using MyoTestv4.Home;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoTestv4.Tests
{
    [TestFixture]
    public class VMTests
    {
        [Test]
        public void TestApplicationViewModel()
        {

            HomeViewModel vm = new HomeViewModel(new UserLoginModel());
            string actualName = vm.Name;
            Assert.AreEqual("Home Page", actualName);


        }



    }
}
