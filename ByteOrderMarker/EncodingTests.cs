using FluentAssertions;
using System;
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
            var bom = new UTF8Encoding(true);

            var preamble = bom.GetPreamble();
            preamble[0].Should().Be(0xEF);
            preamble[1].Should().Be(0xBB);
            preamble[2].Should().Be(0xBF);
        }

        [Fact]
        public void CanSeeBomOnUTF16(){
            // technically the BOM is false in the default constructor
            var noBom = new UnicodeEncoding();
            var bom = new UnicodeEncoding();

            var preamble = bom.GetPreamble();
            preamble[0].Should().Be(0xFF);
        }

        [Fact]
        public void CanSeeBomOnStream(){}
    }
}