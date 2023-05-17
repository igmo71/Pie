using NetBarcode;
using System.Numerics;

namespace Pie.Common
{
    public static class GuidBarcodeConvert_copy
    {
        private const string alphabet = "0123456789abcdef";

        public static string GuidStringToNumericString(string guidString)
        {
            if (!Guid.TryParse(guidString, out Guid guid))
                throw new ApplicationException($"Guid. Failure to parse ({guidString}) ");

            return GuidToNumericString(guid);
        }

        public static string GuidToNumericString(Guid guid)
        {
            string value = guid.ToString("n");

            BigInteger bigInt = 0;

            for (int i = 0; i < value.Length; i++)
            {
                bigInt = bigInt * 16 + alphabet.IndexOf(value.Substring(i, 1));
            }

            string result = bigInt.ToString();

            result = result.Length % 2 != 0 ? $"0{result}" : result;

            return result;
        }

        public static Guid GuidFromNumericString(string numericString)
        {
            if (!BigInteger.TryParse(numericString, out BigInteger bigInt))
                throw new ApplicationException($"BigInteger. Failure to parse ({numericString}) ");
            
            string result = "";

            while (bigInt > 0)
            {
                int remainder = (int)(bigInt % 16);
                result = alphabet.Substring(remainder, 1) + result;
                bigInt = (bigInt - remainder) / 16;
            }

            while (result.Length < 32)
                result = $"0{result}";

            if (!Guid.TryParse(result, out Guid guid))
                throw new ApplicationException($"Guid. Failure to parse ({result}) ");
           
            return guid;
        }

        public static string GuidStringFromNumericString(string numericString)
        {
            Guid guid = GuidFromNumericString(numericString);

            return guid.ToString();
        }
    }
}
