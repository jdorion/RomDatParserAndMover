using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RomDatParserAndMoverLibrary
{
    public class DATParser
    {
        public XDocument DAT { get; private set; }
        public string Keyword { get; private set; }
        public bool IncludeClones { get; private set; }
        public List<string> RomList { get; private set; }
        public List<string> Errors { get; private set; }

        public DATParser(string DATPath, string keyword, bool includeClones)
        {
            Errors = new List<string>();
            IncludeClones = includeClones;

            Keyword = keyword;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Errors.Add("keyword is missing.");
            }

            try
            {
                DAT = XDocument.Load(DATPath);
            }
            catch (Exception e)
            {
                Errors.Add("Problem loading .DAT file: " + e.Message);
            }


            if (!Errors.Any())
            {
                RomList = ParseDAT(Keyword, IncludeClones);
            }
        }

        private List<string> ParseDAT(string keyword, bool includeClones)
        {
            // find all descendents with a romof attributes that matches the keyword,
            // then select the value of their name attribute
            var result = DAT.Descendants().Where(xn => xn.Attribute("romof") != null && xn.Attribute("romof").Value == keyword).Select(xn => xn.Attribute("name").Value).ToList();

            if (includeClones)
            {
                var cloneList = new List<string>();
                foreach (string romName in result)
                {
                    cloneList.AddRange(ParseDAT(romName, includeClones));
                }
                result.AddRange(cloneList);
            }

            return result;
        }
    }
}
