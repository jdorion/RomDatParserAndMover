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
                RomList = ParseDAT(DAT, Keyword, IncludeClones);
            }

            if (RomList.Count() == 0)
            {
                Errors.Add("No matching roms found.");
            }
        }

        public static List<string> ParseDAT(XDocument dat, string keyword, bool includeClones)
        {
            // find all descendents with a romof attributes that matches the keyword,
            // then select the value of their name attribute
            var result = dat.Descendants().Where(xn => IsMatch(xn, keyword, includeClones)).Select(xn => xn.Attribute("name").Value).ToList();

            if (includeClones)
            {
                var cloneList = new List<string>();
                foreach (string romName in result)
                {
                    cloneList.AddRange(ParseDAT(dat, romName, includeClones));
                }
                result.AddRange(cloneList);
            }

            // if there exists a game called 'keyword'
            if (dat.Descendants().Any(xn => xn.Attribute("name").AttributeValueMatches(keyword)))
            {
                result.Add(keyword);
            }

            // in case some got double-added
            return result.Distinct().ToList();
        }

        private static bool IsMatch(XElement node, string keyword, bool includeClones)
        {
            if (node == null) { return false; }

            if (node.Attribute("cloneof").AttributeValueMatches(keyword))
            {
                if (!includeClones)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            if (node.Attribute("romof").AttributeValueMatches(keyword))
            {
                return true;
            }

            return false;
        }
    }
}
