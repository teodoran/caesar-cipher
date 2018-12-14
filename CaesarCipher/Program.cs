using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;

namespace CaesarCipher
{   
    public class Program
    {
        private static int RunCryptAndPrintResults(CommandLineOptions options)
        {
            if (!File.Exists(options.InputFile))
            {
                Console.WriteLine($"{ options.InputFile } is not a file");
                return 1;
            }

            var cryptedLines = File
                .ReadLines(options.InputFile)
                .CryptLines(options.Shift, options.Decrypt);

            foreach (var line in cryptedLines)
            {
                Console.WriteLine(line);
            }

            return 0;
        }
        
        public static int Main(string[] args)
        {
            return CommandLine.Parser.Default
                .ParseArguments<CommandLineOptions>(args)
                .MapResult(RunCryptAndPrintResults, errs => 1);
        }
    }
}
