namespace Pie.Common
{
    public class BarcodeGenerator
    {
        public static string? GetBarCode128(string idString)
        {
            var numericString = GuidBarcodeConvert.GuidStringToNumericString(idString);
            //var barcode = new Barcode(numericString, NetBarcode.Type.Code128, false);
            //var result = barcode.GetBase64Image();
            var result = ZXingNet.GetBarCode128(numericString);
            return result;
        }

        public static string? GetBarCode128(Guid idGuid)
        {
            var numericString = GuidBarcodeConvert.GuidToNumericString(idGuid);
            //var barcode = new Barcode(numericString, NetBarcode.Type.Code128, false);
            //var result = barcode.GetBase64Image();
            var result = ZXingNet.GetBarCode128(numericString);
            return result;
        }
    }
}
