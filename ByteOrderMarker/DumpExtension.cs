using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace ByteOrderMarker
{
    public static class DumpExtension
    {
        public static void Dump(this object value,ITestOutputHelper output)
        {
            var json = JsonConvert.SerializeObject(value, Formatting.Indented);
            output.WriteLine(json);
        }
    }
}
