using System;
using System.Text;

namespace RSA
{
	public class RSA
	{
		private int _p;
		private int _q;
		private int _n;
		private int _d;
		private int _e;
		private int _f;

		public int PublicKey => _e;

		public RSA(int p, int q)
		{
			if (!p.IsPrime() || !q.IsPrime())
			{
				throw new InvalidOperationException("P and q must be prime");
			}

			if (p == q)
			{
				throw new InvalidOperationException("P and q must be different");
			}

			_p = p;
			_q = q;
			_n = p * q;
			_f = (p - 1) * (q - 1);
			_e = GenerateNumberE();
			_d = GenerateNumberD();
		}

		public string Encrypt(string message)
		{
			byte[] characters = message.GenerateCharactersNumbers();
			byte[] encryptedCharacters = new byte[characters.Length];

			for (int i = 0; i < characters.Length; i++)
			{
				encryptedCharacters[i] = (byte)(Math.Pow(characters[i], _e) % _n);
			}
			
			Confuse(characters);

			return Encoding.Default.GetString(encryptedCharacters);
		}

		public string Decrypt(string encryptedMessage)
		{
			byte[] characters = Encoding.Default.GetBytes(encryptedMessage);
			byte[] decryptedCharacters = new byte[characters.Length];

			for (int i = 0; i < characters.Length; i++)
			{
				decryptedCharacters[i] = (byte)(Math.Pow(characters[i], _d) % _n);

				byte decryptedCharacter = (byte)(decryptedCharacters[i] + '_');

				decryptedCharacters[i] = decryptedCharacter >= 96 ? ++decryptedCharacter : decryptedCharacter;
			}

			Untangle(characters);

			return Encoding.Default.GetString(decryptedCharacters);
		}

		private void Confuse(byte[] messageCharacters)
		{
			for (int i = 1; i < messageCharacters.Length; i++)
			{
				messageCharacters[i] = (byte)((messageCharacters[i - 1] + messageCharacters[i]) % _n);
			}
		}

		private void Untangle(byte[] messageCharacters)
		{
			for (int i = messageCharacters.Length - 1; i > 0; i--)
			{
				messageCharacters[i] = (byte)((messageCharacters[i] - messageCharacters[i - 1]) % _n);
			}
		}

		private int GenerateNumberD()
		{
			for (int i = 2; i < int.MaxValue; i++)
			{
				if (i != _e && i * _e % _f == 1)
				{
					return i;
				}
			}

			return default;
		}

		private int GenerateNumberE()
		{
			for (int i = 2; i < _f; i++)
			{
				if (i.IsPrime() && GCD.GetGCD(i, _f) == 1)
				{
					return i;
				}
			}

			return default;
		}
	}
}