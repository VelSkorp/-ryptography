using System.Text;

namespace DataEncryptionStandard
{
    public static class StringToBinaryStringValueConverter
    {
        public static string Convert(string someString)
        {
            var binaryString = new StringBuilder();

            foreach (byte eachByte in Encoding.Default.GetBytes(someString))
            {
                binaryString.Append(System.Convert.ToString(eachByte, 2).PadLeft(8, '0'));
            }

            return binaryString.ToString();
        }

        public static string ConvertBack(string binaryString)
        {
            int sizeOfChar = 8;
            byte[] bytes = new byte[binaryString.Length / sizeOfChar];
            int byteCounter = 0;

            for (int i = 0; i < binaryString.Length; i += sizeOfChar)
            {
                bytes[byteCounter++] = System.Convert.ToByte(binaryString.ToString().Substring(i, sizeOfChar), 2);
            }

            return Encoding.Default.GetString(bytes);
        }
    }
}