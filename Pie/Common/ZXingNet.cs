using SkiaSharp;
using System.Runtime.InteropServices;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace Pie.Common
{
    public class ZXingNet
    {
        public static string GetBarCode128(string barcodeString)
        {
            BarcodeWriterPixelData writer = new()
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 100,
                    Width = 300,
                    Margin = 10
                }
            };

            PixelData pixelData = writer.Write(barcodeString);

            SKBitmap skBitmap = new(pixelData.Width, pixelData.Height);

            Marshal.Copy(pixelData.Pixels, 0, skBitmap.GetPixels(), pixelData.Pixels.Length);

            using SKImage skImage = SKImage.FromBitmap(skBitmap);

            using SKData skData = skImage.Encode(SKEncodedImageFormat.Png, 100);

            byte[] imageBytes = skData.ToArray();

            string barcodeBase64 = Convert.ToBase64String(imageBytes);

            return barcodeBase64;
        }
    }
}
