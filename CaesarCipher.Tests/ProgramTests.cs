using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using FluentAssertions;
using CaesarCipher;

namespace CaesarCipher.Tests
{
    public class ProgramTests
    {
        private readonly string[] _correctArguments;

        public ProgramTests()
        {
            var random = new Random();
            var shift = random
                .Next(Int32.MinValue, Int32.MaxValue)
                .ToString();
            
            string fileName = Path.GetRandomFileName();
            using (FileStream fs = File.Create(fileName))
            {
                fs.WriteByte(42);
            }

            var decryptFlag = random.Next(0, 1) == 0 ? "-d" : "";
            
            _correctArguments = new string[] {
                shift,
                fileName,
                decryptFlag
            };
        }

        [Fact]
        public void WhenArgumentsAreCorrect_AndFileExists_ShouldSucceed()
        {   
            Program.Main(_correctArguments).Should().Be(0);
        }

        [Fact]
        public void WhenArgumentsAreCorrect_ButFileDoesNotExists_ShouldFail()
        {
            string wrongFile = "wrong-file.txt";
            var args = _correctArguments;
            args[1] = wrongFile;
            
            Program.Main(args).Should().Be(1);
        }

        [Fact]
        public void WhenArgumentsAreWrong_AndFileExists_ShouldFail()
        {
            var argumentsMissingShift = new string[] {
                _correctArguments[1],
                "-d"
            };
            
            Program.Main(argumentsMissingShift).Should().Be(1);
        }
    }
}
