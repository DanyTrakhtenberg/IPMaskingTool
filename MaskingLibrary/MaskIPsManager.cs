using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace MaskingLibrary
{
    public class MaskIPsManager : IMaskIPsManager
    {
        readonly Regex ipRegex;
        readonly Regex networkRegex;
        Random random;
        private IIPMasking iIPMasking;

        public MaskIPsManager(IIPMasking iIPMasking)
        {
            this.iIPMasking = iIPMasking;
            ipRegex = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
            networkRegex = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            random = new Random();
        }

        public string[] GetMaskedIPs(string[] ipArr)
        {
            int[] mask = new int[] { 1, 1, 1, 255 };
            Dictionary<string, int[]> ipMaskPairs = new Dictionary<string, int[]>();
            string[] maskedIps = new string[ipArr.Length];
            IterateIpArr(ipArr, ipMaskPairs, maskedIps);
            return maskedIps;
        }

        private void IterateIpArr(string[] ipArr, Dictionary<string, int[]> ipMaskPairs, string[] maskedIps)
        {
            string maskStr;
            for (int i = 0; i < ipArr.Length; i++)
            {
                string ip = ipArr[i];
                if (ipRegex.Match(ip).Success == false)
                {
                   continue;
                }
                string ipNetwork = networkRegex.Match(ip).Value;

                if (ipMaskPairs.ContainsKey(ipNetwork) == true)
                {
                    maskedIps[i] = iIPMasking.MaskIP(ip, ipMaskPairs[ipNetwork]);
                }
                else
                {
                    int[] randomMask = new int[] { 0, 0, 0, 255 };
                    do
                    {
                        maskStr = getRandomMask(randomMask);

                    } while (ipMaskPairs.ContainsKey(maskStr) == true);

                    maskedIps[i] = iIPMasking.MaskIP(ip, randomMask);
                    ipMaskPairs[ipNetwork] = randomMask;
                }
            }
        }

        private string getRandomMask(int[] randomMask)
        {
            randomMask[0] = 255;
            randomMask[1] = random.Next(0, 255);
            randomMask[2] = random.Next(0, 255);
            randomMask[3] = random.Next(0, 255);
            return $"{randomMask[3]}.{randomMask[2]}.{randomMask[1]}.255";
        }

    }
}
