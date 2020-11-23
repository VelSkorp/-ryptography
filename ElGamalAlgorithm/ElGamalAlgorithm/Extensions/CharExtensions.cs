namespace ElGamalAlgorithm
{
    public static class CharExtensions
    {
        public static byte GetCharacterNumber(this char character)
        {
            if (character >= 'A' && character <= 'Z')
            {
                return (byte)(character - 'A' + 1);
            }
            else if (character >= 'a' && character <= 'z')
            {
                return (byte)(character - 'a' + 1);
            }

            return default;
        }

        public static char GetCharacterFromNumber(this byte character)
        {
            char ch = (char)(character + '_');

            return ch >= 96 ? ++ch : ch;
        }
    }
}