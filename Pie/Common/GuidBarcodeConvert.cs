﻿using NetBarcode;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace Pie.Common
{
    public static class GuidBarcodeConvert
    {
        private const string alphabet = "0123456789abcdef";

        public static string GuidStringToNumericString(string guidString)
        {
            BigInteger bigInt = 0;

            for (int i = 0; i < guidString.Length; i++)
            {
                bigInt = bigInt * 16 + alphabet.IndexOf(guidString.Substring(i, 1));
            }

            string numericString = bigInt.ToString();

            numericString = numericString.Length % 2 != 0 ? $"0{numericString}" : numericString;

            return numericString;
        }

        public static string GuidToNumericString(Guid guid)
        {
            string guidString = guid.ToString("n");

            string numericString = GuidStringToNumericString(guidString);

            return numericString;
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