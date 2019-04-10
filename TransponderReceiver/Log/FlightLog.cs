using System;
using System.IO;

namespace TransponderReceiverLib.Log
{
    public class FileConfig
    {
        public string FileName { get; }
        public string FilePath { get; }
        public FileConfig(string FileName, string FilePath)
        {
            this.FileName = FileName;
            this.FilePath = FilePath;
        }
    }

    public class FlightLog : Log
    {
        private FileConfig _cfg;

        public FlightLog(FileConfig cfg)
        {
            _cfg = cfg;
        }
        // giver consolen en tur med bistandspensel.
        public void FormatConsole(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.WARNING:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogLevel.CRITICAL:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.NORMAL:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        public override void Write(LogLevel level, string message)
        {
            if (level == LogLevel.CRITICAL)
            {
                string path = Path.Combine(_cfg.FilePath, _cfg.FileName);

                using (var fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fileStream.Dispose();
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(message);
                        sw.Dispose();
                    }

                }
            }
            else { 
                FormatConsole(level);
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}