using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaskingLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPMaskingTool.Controllers
{
    public class IPMaskingController : Controller
    {
        private readonly IMaskIPsManager maskIPsManager;

        public IPMaskingController(IMaskIPsManager maskIPsManager)
        {
            this.maskIPsManager = maskIPsManager;
        }

        // POST: IPMasking/Post
        [HttpPost]
        public FileContentResult Post(IFormFile file)
        {
            List<String> strList = new List<string>();
            var sb = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    strList.Add(reader.ReadLine());
                }
                var maskedResult = maskIPsManager.GetMaskedIPs(strList.ToArray());
                foreach (var ip in maskedResult)
                {
                    sb.AppendLine(ip);
                }
                var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                string name = $"{file.Name}_masked.txt";
                var fileResult = File(bytes, "text/plain", name);
                return fileResult;

            }

           
        }


    }
}
