using System.Text;

namespace CaesarCipher
{
	public class CaesarСipher
	{
		public delegate char GetCharacterMethod(int key, char messageCharacter, char startAlphabet, char endAlphabet);

		public string Encode(int key, string enteredMessage) => GetEncryptedOrDecryptedMessage(key, enteredMessage, GetEncryptedCharacter);

		public string Decode(int key, string enteredMessage) => GetEncryptedOrDecryptedMessage(key, enteredMessage, GetDecryptedCharacter);

		private string GetEncryptedOrDecryptedMessage(int key, string enteredMessage, GetCharacterMethod method)
		{
			var message = new StringBuilder();

			for (var i = 0; i < enteredMessage.Length; i++)
			{
				if (char.IsUpper(enteredMessage[i]))
				{
					if (enteredMessage[i] >= 'А' && enteredMessage[i] <= 'Я')
					{
						message.Append(method(key, enteredMessage[i], 'А', 'Я'));
					}
					else if (enteredMessage[i] >= 'A' && enteredMessage[i] <= 'Z')
					{
						message.Append(method(key, enteredMessage[i], 'A', 'Z'));
					}
				}
				else
				{
					if (enteredMessage[i] >= 'а' && enteredMessage[i] <= 'я')
					{
						message.Append(method(key, enteredMessage[i], 'а', 'я'));
					}
					else if (enteredMessage[i] >= 'a' && enteredMessage[i] <= 'z')
					{
						message.Append(method(key, enteredMessage[i], 'a', 'z'));
					}
				}
			}

			return message.ToString();
		}

		private char GetEncryptedCharacter(int key, char messageCharacter, char startAlphabet, char endAlphabet)
		{
			var symbol = (char)(messageCharacter + key);

			if (symbol >= startAlphabet && symbol <= endAlphabet)
			{
				return symbol;
			}
			else
			{
				key = symbol - endAlphabet;
				return (char)(startAlphabet + key - 1);
			}
		}

		private char GetDecryptedCharacter(int key, char messageCharacter, char startAlphabet, char endAlphabet)
		{
			var symbol = (char)(messageCharacter - key);

			if (symbol >= startAlphabet && symbol <= endAlphabet)
			{
				return symbol;
			}
			else
			{
				key = startAlphabet - symbol;
				return (char)(endAlphabet - key + 1);
			}
		}
	}
}