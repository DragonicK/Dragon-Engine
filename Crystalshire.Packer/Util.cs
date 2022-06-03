using System.Globalization;

namespace Crystalshire.Packer;
public static class Util {
    private static readonly NumberFormatInfo numberFormat = NumberFormatInfo.InvariantInfo;

    public const long Bytes = 1024;
    public const long KiloBytes = 1048576; // KiloBytes = (Bytes * 1024) 
    public const long MegaBytes = 1073741824; // MegaBytes = (KiloBytes * 1024) 

    public static string GetFileSize(long fileSize) {
        var kByte = (double)fileSize / 1024D;
        var mByte = kByte / 1024D;
        var gByte = mByte / 1024D;

        if (fileSize < Bytes) {
            return string.Format(numberFormat, "{0} Bytes", fileSize);
        }
        else if (fileSize < KiloBytes) {
            return string.Format(numberFormat, "{0:0.00} KB", kByte);
        }
        else if (fileSize < MegaBytes) {
            return string.Format(numberFormat, "{0:0.00} MB", mByte);
        }
        else {
            return string.Format(numberFormat, "{0:0.00} GB", gByte);
        }
    }

    public static int GetProgressPercentage(float minValue, float maxValue) {
        minValue = minValue < 1 ? 1 : minValue;
        maxValue = maxValue < 1 ? 1 : maxValue;

        var result = (minValue / maxValue);

        return Convert.ToInt32(result * 100);
    }
}