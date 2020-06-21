using MaskingLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MaskingLibraryTestProject
{
    [TestClass]
    public class UnitTest1
    {
        IPMasking ipMask;
        [TestInitialize]
        public void init()
        {
            ipMask = new IPMasking();
        }
        [TestMethod]
        public void TestIPMasking_stringIp_inArrIP()
        {
            var ip = ipMask.GetIPintArrFromString("123.4.23.1");
            int[] expected = new[] { 123, 4, 23, 1};
            CollectionAssert.AreEqual(ip, expected);

        }
        [TestMethod]
        [ExpectedException( typeof(FormatException))]
        public void TestIPMasking_stringIpWithOnly3Sections_Exception()
        {
            var ip = ipMask.GetIPintArrFromString("123.4.23");
          

        }
        [TestMethod]
        public void TestIPMasking_stringIp_IpArr()
        {
            var ip = ipMask.GetIPintArrFromString("123.4.23.4");
            CollectionAssert.AreEqual(ip, new int[] { 4, 23,4, 123 });
        }
        [TestMethod]
        public void TestIPMasking_stringIp_MaskedIP()
        {
            var ip = ipMask.MaskIP("0.0.0.0",new int[] { 1, 1, 1,1  });
            Assert.AreEqual(ip, "1.1.1.1");

        }

        [TestMethod]
        public void TestIPMasking_stringIp2_MaskedIP()
        {
            var ip = ipMask.MaskIP("1.1.1.1", new int[] { 1, 1, 1, 1 });
            Assert.AreEqual(ip, "0.0.0.0");

        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestIPMasking_stringIpWithLetters_Exception()
        {
            var ip = ipMask.GetIPintArrFromString("123.4.23.A");

        }
    }
}
