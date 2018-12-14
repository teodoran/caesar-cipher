using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using CaesarCipher;

namespace CaesarCipher.Tests
{
    public class CipherTests
    {
        [Fact]
        public void EncryptionAndDecryptionShouldWorkForMultipleLines()
        {
            var plaintextLines = new List<string>{ "i came", "i saw", "i conquered" };
            var ciphertextLines = new List<string> { "pgjhtl", "pgzh ", "pgjvuxølylk" };

            plaintextLines.CryptLines(7, decrypt: false).Should().ContainInOrder(ciphertextLines);
            ciphertextLines.CryptLines(7, decrypt: true).Should().ContainInOrder(plaintextLines);
        }

        [Theory]
        [InlineData("Experience is the teacher of all things.", "hæshulhqfhclvcwkhcwhdfkhucricdoocwklqjv", 3)]
        [InlineData("No one is so brave that he is not disturbed by something unexpected.", "zælæzqlualaæln mdqlbtmbltqlualzæblpuabc nqplnglaæyqbtuzslczqføqobqp", 42)]
        public void EncryptionShouldWork(string plaintext, string ciphertext, int shift)
        {
            var lines = new List<string>{ plaintext };
            var ciphertextLines = lines.CryptLines(shift);

            ciphertextLines.Should().ContainSingle().Which.Should().Be(ciphertext);
        }

        [Theory]
        [InlineData("føgwåkøøerwyøebøoøwmaxmwpabzawmaørwæølbkø", "men freely believe that which they desire", -7)]
        [InlineData("pgohkgyhæolygilgmpyzægpughgåpsshnlgæohugzljvukghægyvtl", "i had rather be first in a village than second at rome", 7)]
        public void DecryptionShouldWork(string ciphertext, string plaintext, int shift)
        {
            var lines = new List<string> { ciphertext };
            var plaintextLines = lines.CryptLines(shift, decrypt: true);

            plaintextLines.Should().ContainSingle().Which.Should().Be(plaintext);
        }
    }
}
