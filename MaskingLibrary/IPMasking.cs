using System;
using System.Linq;
using System.Text;

namespace MaskingLibrary
{
    public class IPMasking : IIPMasking
    {
        private int[] IPmask;
        private int sectionCounter;
        StringBuilder sb;
        public IPMasking()
        {
            sb = new StringBuilder();
        }

        public string MaskIP(string ip, int[] maskArr)
        {
            sb.Clear();
            var ipArr = GetIPintArrFromString(ip);

            for (int i = 3; i > -1; i--)
            {
                ipArr[i] = ipArr[i] ^ maskArr[i];
                sb.Append(ipArr[i].ToString());
                sb.Append(".");
            }
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public int[] GetIPintArrFromString(string ip)
        {
            int[] ipIntArr = new int[4];

            string[] splitedIp = ip.Split('.').Reverse().ToArray();
            if (splitedIp.Length != 4)
            {
                throw new FormatException("Failed to parse IP address into 4 sections");
            }

            for (int i = 0; i < 4; i++)
            {
                int parsedSection;
                if (int.TryParse(splitedIp[i], out parsedSection) == false)
                {
                    throw new FormatException("Unable to parse IP");
                }
                ipIntArr[i] = parsedSection;
            }
            return ipIntArr;
        }
    }
}
