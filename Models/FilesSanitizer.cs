using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FilesSanitizer.Models
{
    public abstract class FilesSanitizer
    {
       public abstract string SanitizeLine(string lineStr) ;
       public abstract string ValidateChunk(string cnk);

        public FilesSanitizer()
        {
            
        }

        public  byte[] Sanitize(IFormFile file)
        {
            //var response = new HttpResponseMessage(HttpStatusCode.OK);
            var streamcontent = new StreamReader(file.OpenReadStream());
            string lineStr = string.Empty;
            string newStr = string.Empty;
            string CurrntDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string[] fileParts = file.FileName.Split('.');
            string newFile = fileParts[0] + "Sanitized" + "." + fileParts[1];
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(CurrntDirectory, newFile)))
            {
                while ((lineStr = streamcontent.ReadLine()) != null)
                {
                    if (lineStr.Length > 0)
                    {
                        if (!lineStr.Equals("123") && !lineStr.Equals("789"))
                        {

                            newStr = SanitizeLine(lineStr);

                        }
                        else
                        {
                            newStr = lineStr;
                        }
                        outputFile.WriteLine(newStr);
                    }
                }
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(CurrntDirectory, newFile));
            return fileBytes;
        }
    }
}
