using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RomDatParserAndMoverLibrary
{
    public static class CustomExtensions
    {
        public static bool AttributeValueMatches(this XAttribute xa, string keyword)
        {
            if (xa == null) { return false; }
            return xa.Value == keyword;
        }
    }
}
