using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client
{
    public static class IDExtractor
    {
        public static int GetIDFromString(string cbItem)
        {
            var regex = new Regex(@"\(\d+\)");
            var match = regex.Match(cbItem);
            var result = match.Value;
            result = result.Substring(1, result.Length - 2);
            return int.Parse(result);
        }
    }
}
