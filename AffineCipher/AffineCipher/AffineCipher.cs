using System;
using System.Text;

namespace AffineCipher
{
    public class AffineCipher
    {
        private delegate char GetCharacterMethod(char messageCharacter, char startAlphabet, char endAlphabet);

        public Key Key { get; set; }
        public string EnteredMessage { get; set; }

        public AffineCipher(Key key, string enteredMessage)
        {
            Key = key;
            EnteredMessage = enteredMessage;
        }

        public string Encode() => GetEncryptedOrDecryptedMessage(GetEncryptedCharacter);

        public string Decode() => GetEncryptedOrDecryptedMessage(GetDecryptedCharacter);

        private string GetEncryptedOrDecryptedMessage(GetCharacterMethod method)
        {
            var message = new StringBuilder();

            for (var i = 0; i < EnteredMessage.Length; i++)
            {
                if (char.IsUpper(EnteredMessage[i]))
                {
                    if (EnteredMessage[i] >= 'А' && EnteredMessage[i] <= 'Я')
                    {
                        message.Append(method(EnteredMessage[i], 'А', 'Я'));
                    }
                    else if (EnteredMessage[i] >= 'A' && EnteredMessage[i] <= 'Z')
                    {
                        message.Append(method(EnteredMessage[i], 'A', 'Z'));
                    }
                }
                else
                {
                    if (EnteredMessage[i] >= 'а' && EnteredMessage[i] <= 'я')
                    {
                        message.Append(method(EnteredMessage[i], 'а', 'я'));
                    }
                    else if (EnteredMessage[i] >= 'a' && EnteredMessage[i] <= 'z')
                    {
                        message.Append(method(EnteredMessage[i], 'a', 'z'));
                    }
                }
            }

            return message.ToString();
        }

        private char GetEncryptedCharacter(char messageCharacter, char startAlphabet, char endAlphabet)
        {
            int m = endAlphabet - startAlphabet + 1;

            if (GCD.GetGCD(Key.A, m) != 1)
            {
                throw new InvalidOperationException("a и m не взаимно простые числа");
            }

            int t = messageCharacter - startAlphabet;
            int index = (Key.A * t + Key.B) % m;
            return (char)(startAlphabet + index);
        }

        private char GetDecryptedCharacter(char messageCharacter, char startAlphabet, char endAlphabet)
        {
            int m = endAlphabet - startAlphabet + 1;

            if (GCD.GetGCD(Key.A, m) != 1)
            {
                throw new InvalidOperationException("a и m не взаимно простые числа");
            }

            int y = messageCharacter - startAlphabet;
            int reverseElement = GetReverseElement(Key.A, m);
            int index = (reverseElement * (y + m - Key.B)) % m;
            return (char)(startAlphabet + index);
        }

        private int GetReverseElement(int element, int module)
        {
            var result = 0;
            int i;

            for (i = 1; i < module - 1 && result != 1; i++)
            {
                result = (element * i) % module;
            }

            return i - 1;
        }
    }
}