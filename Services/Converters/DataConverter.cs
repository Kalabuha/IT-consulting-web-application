using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;

namespace Services.Converters
{
    public static class DataConverter
    {
        public static string Array64ToDataImageString(byte[]? array)
        {
            if (array == null)
            {
                return string.Empty;
            }

            string dataString = Convert.ToBase64String(array);
            string dataImageString = string.Format("data:image/jpg;base64,{0}", dataString);

            return dataImageString;
        }

        public static byte[]? DataImageStringToArray64(string? dataImageString)
        {
            if (string.IsNullOrEmpty(dataImageString))
            {
                return null;
            }

            if (dataImageString.Length <= 20)
            {
                return new byte[0];
            }

            string dataString = dataImageString.Substring(22, dataImageString.Length - 22);
            var t = 10;

            byte[] array = Convert.FromBase64String(dataString);

            return array;
        }

        public static byte[] PathToImageToArray64(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new byte[0];
            }

            byte[] array;
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                array = new byte[stream.Length];

                stream.Read(array, 0, (int)stream.Length);
            }

            return array;
        }

        public static string CutTextByParameterIfNullReturnEmpty(string? text, int length)
        {
            var croppedTextOrNull = CutTextByParameterIfNullReturnNull(text, length);

            if (croppedTextOrNull == null)
            {
                return string.Empty;
            }

            return croppedTextOrNull;
        }

        public static string? CutTextByParameterIfNullReturnNull(string? text, int length)
        {
            if (string.IsNullOrEmpty(text) || length <= 0)
            {
                return null;
            }

            if (text.Length <= length)
            {
                return text;
            }

            return text.Substring(0, length);
        }
    }
}
