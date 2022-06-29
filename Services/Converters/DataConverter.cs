namespace Services.Converters
{
    public static class DataConverter
    {
        public static string Array64ToString(byte[]? array)
        {
            if (array == null) return string.Empty;

            string imreArray64Data = Convert.ToBase64String(array);
            string imageAsString = string.Format("data:image/jpg;base64,{0}", imreArray64Data);

            return imageAsString;
        }


        public static byte[] PathToImageToArray64(string path)
        {
            byte[] array;
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                array = new byte[stream.Length];

                stream.Read(array, 0, (int)stream.Length);
            }

            return array;
        }

        public static string CutTextByParameter(string? text, int length)
        {
            if (text == null || length <= 0)
            {
                return string.Empty;
            }

            if (text.Length <= length)
            {
                return text;
            }

            return text.Substring(0, length);
        }
    }
}
