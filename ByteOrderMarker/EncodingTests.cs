using FluentAssertions;
using System;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace ByteOrderMarker
{
    public class EncodingTests
    {
        private readonly ITestOutputHelper output;

        public EncodingTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CanSeeBomOnUTF8()
        {
            // by default the BOM is not required on UTF 8
            var bom = new UTF8Encoding(true);

            var preamble = bom.GetPreamble();
            preamble[0].Should().Be(0xEF);
            preamble[1].Should().Be(0xBB);
            preamble[2].Should().Be(0xBF);
        }

        [Fact]
        public void CanSeeBomOnUTF16()
        {
            var bom = new UnicodeEncoding();

            var preamble = bom.GetPreamble();
            preamble[0].Should().Be(0xFF);
        }

        [Fact]
        public void BinaryReaderDetectEncoding()
        {
            var filename = "sampler.txt";
            using (var f = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                byte[] bom = new byte[4];
                f.Read(bom, 0, bom.Length);

                bom[0].Should().Be(0xEF);
                bom[1].Should().Be(0xBB);
                bom[2].Should().Be(0xBF);

            }
        }

        [Fact]
        public void StreamReaderDetect()
        {
            var filename = "sampler.txt";
            // stream reader by default skips the BOM
            using (var f = new StreamReader(filename, true))
            {
                var c = f.Read();
                f.CurrentEncoding.Should().Be(Encoding.UTF8);
                // c.Should().Be(0xFF);
            }
        }

        [Fact]
        public void StreamReaderDefault()
        {
            var filename = "sampler.txt";
            // stream reader by default skips the BOM
            using (var f = new StreamReader(filename))
            {
                var c = f.Read();
                f.CurrentEncoding.Should().Be(Encoding.UTF8);
                // c.Should().Be(0xFF);
            }
        }

        [Fact]
        public void WriteBomToStream()
        {
            var filename = "sampler.txt";
            var text = "text";
            var encoding = new UTF8Encoding(true);
            var bytes = encoding.GetBytes(text);
            var preamble = encoding.GetPreamble();
            using (var f = new FileStream(filename, FileMode.Create))
            {
                f.Write(preamble, 0, preamble.Length);
                f.Write(bytes, 0, bytes.Length);
            }
            (new FileInfo(filename)).Length.Should().Be(7);
        }
    }
}