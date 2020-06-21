using MaskingLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace MaskingLibraryTestProject
{
    [TestClass]
    public class TestMaskIPsManager
    {
        MaskIPsManager ipMask;

        public Regex networkRegex { get; private set; }

        [TestInitialize]
        public void init()
        {
            ipMask = new MaskIPsManager(new IPMasking());
            networkRegex = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\b");

        }
        [TestMethod]
        public void TestMaskIPsManager_stringIps_MaskedIP()
        {
            string[] ipArr =
            {
                "125.2.3.5",
                  "126.2.3.5",
                "125.2.3.5",
            };
            var ips = ipMask.GetMaskedIPs(ipArr);
            Assert.AreEqual(ips[0], ips[2]);

        }
        [TestMethod]
        public void TestMaskIPsManager_sameNetworkDifferentEnding_MaskededIP()
        {
            string[] ipArr =
            {
                "125.2.3.5",
                "125.2.3.50"
            };
            var ips = ipMask.GetMaskedIPs(ipArr);
            Assert.AreEqual(networkRegex.Match(ips[0]).Value, networkRegex.Match( ips[1]).Value);

        }
        [TestMethod]
        public void TestMaskIPsManager_twoSimilarIps_equalIps()
        {
            string[] ipArr =
            {
                "125.2.3.5",
                  "126.3.3.5",
                "125.2.3.5",
            };
            var ips = ipMask.GetMaskedIPs(ipArr);

            Assert.AreEqual(ipArr[0], ipArr[2]);
        }
        [TestMethod]
        public void TestMaskIPsManager_twoSimilarIpsOverBadIP_equalIps()
        {
            string[] ipArr =
            {
                "125.2.3.5",
                  "S",
                "125.2.3.5",
            };
            var ips = ipMask.GetMaskedIPs(ipArr);

            Assert.AreEqual(ipArr[0], ipArr[2]);
        }
    }
}
