using System;
using System.Text;

namespace GOST_28147_89
{
	public partial class GOST_28147_89Algorithm
	{
		private readonly int _sizeOfBlock = 64;
		private readonly int _sizeOfChar = 8;
		private readonly int _roundCount = 32;

		private string _binaryText;
		private string _binaryKey;

		public GOST_28147_89Algorithm(string text, string key)
		{
			if (key.Length != 32)
			{
				throw new InvalidOperationException("Key length must be 32 characters");
			}

			text = CheckLengthAndAddSymbols(text);

			_binaryText = StringToBinaryStringValueConverter.Convert(text);
			_binaryKey = StringToBinaryStringValueConverter.Convert(key);
		}

		public string Decrypt()
		{
			var decryptedText = new StringBuilder();

			for (int i = 0; i < _binaryText.Length; i += _sizeOfBlock)
			{
				string block = _binaryText.Substring(i, _sizeOfBlock);

				for (int round = _roundCount - 1; round >= 0; round--)
				{
					block = Round(block, round, round != 0);
				}

				decryptedText.Append(block);
			}

			return StringToBinaryStringValueConverter.ConvertBack(decryptedText.ToString());
		}

		public string Encrypt()
		{
			var encryptedText = new StringBuilder();

			for (int i = 0; i < _binaryText.Length; i += _sizeOfBlock)
			{
				string block = _binaryText.Substring(i, _sizeOfBlock);

				for (int round = 0; round < _roundCount; round++)
				{
					block = Round(block, round, round != 31);
				}

				encryptedText.Append(block);
			}

			return StringToBinaryStringValueConverter.ConvertBack(encryptedText.ToString());
		}

		private string Round(string block, int roundNumber, bool swapParts)
		{
			string L = block.Substring(0, _sizeOfBlock / 2);
			string R = block.Substring(_sizeOfBlock / 2, _sizeOfBlock / 2);
			string key = GenerateKey(roundNumber);

			block = R;
			R = FeistelTransform(key, R);
			L = XOR(L, R);

			return swapParts ? string.Concat(block, L) : string.Concat(L, block);
		}

		private string FeistelTransform(string key, string rightBits)
		{
			string xor = XOR(key, rightBits);
			string bits = ConvertToAnotherNumber(xor);

			return LeftShift(bits, 11);
		}

		private string ConvertToAnotherNumber(string xor)
		{
			int sboxNumber = 0;
			var bits = new StringBuilder();

			for (int i = 0; i < xor.Length; i += 4)
			{
				int num = Convert.ToInt32($"{xor[i]}{xor[i + 1]}{xor[i + 2]}{xor[i + 3]}", 2);
				num = SBox[sboxNumber++][num];

				bits.Append(Convert.ToString(num, 2).PadLeft(4, '0'));
			}

			return bits.ToString();
		}

		private string XOR(string part1, string part2)
		{
			long k = Convert.ToInt64(part1, 2);
			long b = Convert.ToInt64(part2, 2);
			long x = k ^ b;

			return Convert.ToString(x, 2).PadLeft(32, '0');
		}

		private string CheckLengthAndAddSymbols(string someString)
		{
			var text = new StringBuilder(someString);

			while (text.Length % _sizeOfChar != 0)
			{
				text.Append('#');
			}

			return text.ToString();
		}

		private string GenerateKey(int roundNumber)
		{
			return _binaryKey.Substring(SubkeyOrder[roundNumber] * 32, 32);
		}

		private string LeftShift(string key, int countOfShift)
		{
			int n = key.Length;
			countOfShift %= n;

			char[] charKey = key.ToCharArray();
			 
			Array.Reverse(charKey);
			Array.Reverse(charKey, 0, n - countOfShift);
			Array.Reverse(charKey, n - countOfShift, countOfShift);

			return new string(charKey);
		}
	}
}