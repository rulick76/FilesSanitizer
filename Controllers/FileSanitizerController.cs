using FilesSanitizer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FileSanitizer.Controllers
{
 
    [Route("api/")]
    [ApiController]
    public class FileSanitizerController : ControllerBase
    {
        [HttpPost("sanitize")]
        public async Task<IActionResult> Post()
        {
            byte[] result= { };
            var file = Request.Form.Files[0];
            
            string[] fileParts = file.FileName.Split('.');
            string newFile = fileParts[0] + "Sanitized" + "." + fileParts[1];
            var ext = fileParts[1];
            FilesSanitizer.Models.FilesSanitizer filesSanitizer;
            
            switch (ext)
            {
                case "abc":
                    filesSanitizer = new AbcFile();
                    result =  filesSanitizer.Sanitize(file);
                    break;
                default:
                    break;
            }

            return File(result, "application/force-download", newFile);
        }
    }
}
