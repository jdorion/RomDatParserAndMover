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
            Assert.IsTrue(DATParserTest.RomList.Count() == 189);
        }

        [TestMethod]
        public void TestHappyPathIncludeClones()
        {
            var DATParserTest = new DATParser(GetTestDATPath(), "neogeo", true);

            Assert.IsFalse(DATParserTest.Errors.Any());
            Assert.IsTrue(DATParserTest.RomList.Count() == 321);
        }

        [TestMethod]
        public void TestIncludeClonesNoClones()
        {
            var dpt = new DATParser(GetTestDATPath(), "3countba", true);

            Assert.IsFalse(dpt.Errors.Any());
            Assert.IsTrue(dpt.RomList.Count() == 1);
        }

        [TestMethod]
        public void TestIncludeClones()
        {
            var dpt = new DATParser(GetTestDATPath(), "3countb", true);

            Assert.IsFalse(dpt.Errors.Any());
            Assert.IsTrue(dpt.RomList.Count() == 2);
        }

        [TestMethod]
        public void TestNoMatch()
        {
            var dpt = new DATParser(GetTestDATPath(), "buckfutter", false);
            Assert.IsFalse(dpt.Errors.Any());
            Assert.IsTrue(dpt.RomList.Count() == 0);
        }

        [TestMethod]
        public void TestNoMatchIncludeClones()
        {
            var dpt = new DATParser(GetTestDATPath(), "buckfutter", false);
            Assert.IsFalse(dpt.Errors.Any());
            Assert.IsTrue(dpt.RomList.Count() == 0);
        }

        private string GetTestDATPath()
        {
            return ConfigurationManager.AppSettings["TestDATPath"];
        }
    }
}
