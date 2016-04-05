using System.Collections.Generic;
using System.IO;

namespace RomDatParserAndMoverLibrary
{
    public class RomMover
    {
        private const string romExt = ".zip";
        public List<string> RomList { get; private set; }
        public string SourcePath { get; private set; }
        public string OutputPath { get; private set; }
        public List<string> Errors { get; private set; }

        public RomMover(List<string> romList, string sourcePath, string outputPath)
        {
            RomList = romList;
            SourcePath = sourcePath;
            OutputPath = outputPath;
            Errors = new List<string>();
        }

        public void MoveRoms()
        {
            foreach (string romName in RomList)
            {
                var sourceFile = Path.Combine(SourcePath, romName, romExt);
                if (File.Exists(sourceFile))
                {
                    File.Copy(sourceFile, Path.Combine(OutputPath, romName, romExt));
                }
                else
                {
                    Errors.Add("Source rom missing: " + sourceFile);
                }
            }
        }
    }
}
