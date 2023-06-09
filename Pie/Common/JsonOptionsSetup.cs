using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Pie.Common
{
    public class JsonOptionsSetup : IConfigureOptions<JsonSerializerOptions>
    {
        public void Configure(JsonSerializerOptions options)
        {
            options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.PropertyNameCaseInsensitive = true;
            options.WriteIndented = true;
            options.Encoder = JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic));
        }
    }

}
