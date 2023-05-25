using System.Text;

namespace Pie.Common
{
    public class EncodingConvert
    {
        private static async Task<string> ReadBytesAsWindows1251(HttpResponseMessage response)
        {
            var byteArray = await response.Content.ReadAsByteArrayAsync();
            var encoding = Encoding.GetEncoding("windows-1251"); // replace with your desired encoding
            var responseString = encoding.GetString(byteArray);
            return responseString;
        }

        private string Win1251ToUTF8(string source)
        {
            Encoding win1251 = Encoding.GetEncoding("windows-1251");
            Encoding utf8 = Encoding.GetEncoding("utf-8");

            byte[] win1251Bytes = win1251.GetBytes(source);
            byte[] utf8Bytes = Encoding.Convert(win1251, utf8, win1251Bytes);
            string result = utf8.GetString(utf8Bytes);
            return result;
        }

        private static string UTF8ToWin1251(string source)
        {
            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            byte[] utf8Bytes = utf8.GetBytes(source);
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
            string result = win1251.GetString(win1251Bytes);
            return result;
        }
    }
}
