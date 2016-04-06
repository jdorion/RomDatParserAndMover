using System.Collections.Generic;
using System.IO;

namespace RomDatParserAndMoverLibrary
{
    public class RomCopier
    {
        private const string romExt = ".zip";
        private delegate void XferFunc(string source, string dest);
        public List<string> RomList { get; private set; }
        public string SourcePath { get; private set; }
        public string DestPath { get; private set; }
        public List<string> Errors { get; private set; }

        public RomCopier(List<string> romList, string sourcePath, string destPath)
        {
            RomList = romList;
            SourcePath = sourcePath;
            DestPath = destPath;
            Errors = new List<string>();
        }

        public void CopyRoms()
        {
            XferRoms(File.Copy);
        }

        public void MoveRoms()
        {
            XferRoms(File.Move);
        }

        private void XferRoms(XferFunc xferFunc)
        {
            foreach (string romName in RomList)
            {
                var sourceFile = Path.Combine(SourcePath, romName + romExt);
                var outputFile = Path.Combine(DestPath, romName + romExt);

                if (File.Exists(sourceFile))
                {
                    xferFunc(sourceFile, outputFile);
                }
                else
                {
                    Errors.Add("Source rom missing: " + sourceFile);
                }
            }
        }

        public static void CopyRom(string romName, string sourcePath, string destPath)
        {
            XferRom(File.Copy, romName, sourcePath, destPath);
        }

        public static void MoveRom(string romName, string sourcePath, string destPath)
        {
            XferRom(File.Move, romName, sourcePath, destPath);
        }

        private static void XferRom(XferFunc xferFunc, string romName, string sourcePath, string destPath)
        {
            var sourceFile = Path.Combine(sourcePath, romName + romExt);
            var outputFile = Path.Combine(destPath, romName + romExt);

            if (File.Exists(sourceFile))
            {
                xferFunc(sourceFile, outputFile);
            }
        }
    }
}
