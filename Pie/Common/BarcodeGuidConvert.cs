using System.Numerics;

namespace Pie.Common
{
    public static class BarcodeGuidConvert
    {
        private const string alphabet = "0123456789abcdef";

        public static string ToNumericString(Guid guid)
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

        public static string ToNumericString(string guidString)
        {
            if (!Guid.TryParse(guidString, out Guid guid))
                throw new ApplicationException($"Guid. Failure to parse ({guidString}) ");

            return ToNumericString(guid);
        }

        public static Guid FromNumericString(string numericString)
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

        public static string FromNumericStringAsString(string numericString)
        {
            Guid guid = FromNumericString(numericString);

            return guid.ToString();
        }

        public static string? GetBarcodeBase64(string idString)
        {
            var numericString = ToNumericString(idString);
            var barcode = new Barcode(numericString, NetBarcode.Type.Code128, false);
            var result = barcode.GetBase64Image();
            return result;
        }

        public static string? GetBarcodeBase64(Guid idGuid)
        {
            var numericString = ToNumericString(idGuid);
            var barcode = new Barcode(numericString, NetBarcode.Type.Code128, false);
            var result = barcode.GetBase64Image();
            return result;
        }
    }
}
