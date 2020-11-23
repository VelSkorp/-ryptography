using System;
using System.Collections.Generic;
using System.Text;

namespace ElGamalAlgorithm
{
    public class ElGamal
	{
		private int _p;
		private int _q;
		private int _x;
		private int _y;
		private static readonly int MaxPrimeNum = 40;
		private static readonly int MinPrimeNum = 26;
		private static readonly int MaxNumForGCD = 20;

		public List<int> PublicKey => new List<int> { _p, _q, _y };

		public ElGamal()
		{
			var random = new Random();
			var primitiveRoot = new PrimitiveRoot();

			_p = random.NextPrime(MinPrimeNum, MaxPrimeNum);
			_q = primitiveRoot.GetPRoot(_p);
			_x = random.Next(2, _p - 2);
			_y = (int)(Math.Pow(_q, _x) % _p);
		}

		public string Encrypt(string message)
		{
			var random = new Random();
			var encryptedCharacters = new List<byte>();
			int k = random.NextGCD(MaxNumForGCD, _p - 1);

			foreach (char character in message)
			{
				byte ch = character.GetCharacterNumber();

				encryptedCharacters.Add((byte)(Math.Pow(_q, k) % _p));
				encryptedCharacters.Add((byte)((Math.Pow(_y, k) * ch) % _p));
			}

			return Encoding.Default.GetString(encryptedCharacters.ToArray());
		}

		public string Decrypt(string encyptedMessage)
		{
			var decryptedCharacters = new List<char>();
			byte[] characters = Encoding.Default.GetBytes(encyptedMessage);

			for (int i = 0; i < characters.Length; i += 2)
			{
				byte character = (byte)(characters[i + 1] * MathExtensions.Pow(characters[i], _x * (_p - 2)) % _p);

				decryptedCharacters.Add(character.GetCharacterFromNumber());
			}

			return new string(decryptedCharacters.ToArray());
		}
	}
}