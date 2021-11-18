
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FilesSanitizer.Models
{
    public class AbcFile : FilesSanitizer
    {

        public AbcFile()
        {

        }

        public override string SanitizeLine(string lineStr)
        {
            string newLine = string.Empty;
            List<string> resulcnkGroupst = new List<string>(Regex.Split(lineStr, @"(?<=\G.{3})", RegexOptions.Singleline));
            for (int i = 0; i < resulcnkGroupst.Count - 1; ++i)
            {
                if (resulcnkGroupst[i].Length > 0)
                    newLine += ValidateChunk(resulcnkGroupst[i]);
            }
            return newLine;
        }


        public override string ValidateChunk(string cnk)
        {
            string newCnk = cnk;
            if (!Char.IsDigit(cnk[1]))
            {
                newCnk = "A225C";
            }

            return newCnk;
        }
    }
}
