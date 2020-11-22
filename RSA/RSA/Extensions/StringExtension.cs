using System.Text;

namespace RSA
{
    public static class StringExtension
    {
        public static byte[] GenerateCharactersNumbers(this string message)
        {
            byte[] characters = Encoding.Default.GetBytes(message);

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] >= 'A' && message[i] <= 'Z')
                {
                    characters[i] = (byte)(characters[i] - 'A' + 1);
                }
                else if (message[i] >= 'a' && message[i] <= 'z')
                {
                    characters[i] = (byte)(characters[i] - 'a' + 1);
                }
                else if (message[i] == ' ')
                {
                    characters[i] = 0;
                }
            }

            return characters;
        }
    }
}