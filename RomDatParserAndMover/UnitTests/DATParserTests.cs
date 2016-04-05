using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomDatParserAndMoverLibrary;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class DATParserTests
    {

        [TestMethod]
        public void TestHappyPath()
        {
            var DATParserTest = new DATParser(GetTestDATPath(), "neogeo", false);

            Assert.IsFalse(DATParserTest.Errors.Any());
            Assert.IsTrue(DATParserTest.RomList.Count() == 188);
        }

        [TestMethod]
        public void TestHappyPathIncludeClones()
        {
            var DATParserTest = new DATParser(GetTestDATPath(), "neogeo", true);

            Assert.IsFalse(DATParserTest.Errors.Any());
            Assert.IsTrue(DATParserTest.RomList.Count() == 320);
        }

        private string GetTestDATPath()
        {
            return ConfigurationManager.AppSettings["TestDATPath"];
        }
    }
}
