using System;
using System.Text;

namespace DataEncryptionStandard
{
	public partial class DES
	{
		private readonly int _sizeOfBlock = 64;
		private readonly int _sizeOfChar = 8;
		private readonly int _roundCount = 16;

		private string _binaryText;
		private string _binaryKey;

		public DES(string text, string key)
		{
			if (key.Length != 8)
			{
				throw new InvalidOperationException("Key length must be 8 characters");
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

				block = InitialPermutation(block);

				for (int round = _roundCount - 1; round >= 0; round--)
				{
					block = Round(block, round, round != 0);
				}

				decryptedText.Append(FinalPermutation(block));
			}

			return StringToBinaryStringValueConverter.ConvertBack(decryptedText.ToString());
		}

		public string Encrypt()
		{
			var encryptedText = new StringBuilder();

			for (int i = 0; i < _binaryText.Length; i += _sizeOfBlock)
			{
				string block = _binaryText.Substring(i, _sizeOfBlock);

				block = InitialPermutation(block);

				for (int round = 0; round < _roundCount; round++)
				{
					block = Round(block, round, round != 15);
				}

				encryptedText.Append(FinalPermutation(block));
			}

			return StringToBinaryStringValueConverter.ConvertBack(encryptedText.ToString());
		}

		private string Round(string block, int roundNumber, bool swapParts)
		{
			string L = block.Substring(0, _sizeOfBlock / 2);
			string R = block.Substring(_sizeOfBlock / 2, _sizeOfBlock / 2);
			string key = KeyGeneration(roundNumber);

			block = R;
			R = FunctionDES(key, R);
			L = XOR(L, R, 32);

			return swapParts ? string.Concat(block, L) : string.Concat(L, block);
		}

		private string FunctionDES(string key, string rightBits)
		{
			rightBits = ExpandRight(rightBits);

			string xor = XOR(key, rightBits, 48);
			string bits = ReplacingSixBitsByFourBits(xor);

			return StraightP_BoxPermute(bits);
		}

		private string StraightP_BoxPermute(string bits)
		{
			int straightP_BoxRowCount = StraightP_Box.GetLength(0);
			int straightP_BoxCollumnCount = StraightP_Box.GetLength(1);
			var outBits = new StringBuilder();

			for (int i = 0; i < straightP_BoxRowCount; i++)
			{
				for (int j = 0; j < straightP_BoxCollumnCount; j++)
				{
					int index = StraightP_Box[i, j];

					outBits.Append(bits[index - 1]);
				}
			}

			return outBits.ToString();
		}

		private string ReplacingSixBitsByFourBits(string xor)
		{
			int sboxNumber = 0;
			var bits = new StringBuilder();

			for (int i = 0; i < xor.Length; i += 6)
			{
				int row = Convert.ToInt32($"{xor[i]}{xor[i + 5]}", 2);
				int collumn = Convert.ToInt32($"{xor[i + 1]}{xor[i + 2]}{xor[i + 3]}{xor[i + 4]}", 2);
				int num = SBox[sboxNumber++][row, collumn];

				bits.Append(Convert.ToString(num, 2).PadLeft(4, '0'));
			}

			return bits.ToString();
		}

		private string ExpandRight(string rightBits)
		{
			int p_BoxExpansionRowCount = P_BoxExpansion.GetLength(0);
			int p_BoxExpansionCollumnCount = P_BoxExpansion.GetLength(1);
			var bits = new StringBuilder();

			for (int i = 0; i < p_BoxExpansionRowCount; i++)
			{
				for (int j = 0; j < p_BoxExpansionCollumnCount; j++)
				{
					int index = P_BoxExpansion[i, j];

					bits.Append(rightBits[index - 1]);
				}
			}

			return bits.ToString();
		}

		private string XOR(string part1, string part2, int partLength)
		{
			long k = Convert.ToInt64(part1, 2);
			long b = Convert.ToInt64(part2, 2);
			long x = k ^ b;
			return Convert.ToString(x, 2).PadLeft(partLength, '0');
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

		private string InitialPermutation(string block)
		{
			var permutateitedText = new StringBuilder();
			int size = InitialPermutationIP.GetLength(0);

			for (int j = 0; j < size; j++)
			{
				for (int k = 0; k < size; k++)
				{
					int index = InitialPermutationIP[j, k];

					permutateitedText.Append(block[index - 1]);
				}
			}

			return permutateitedText.ToString();
		}

		private string FinalPermutation(string text)
		{
			var permutateitedText = new StringBuilder();
			int size = FinalPermutationIP.GetLength(0);

			for (int j = 0; j < size; j++)
			{
				for (int k = 0; k < size; k++)
				{
					int index = FinalPermutationIP[j, k];

					permutateitedText.Append(text[index - 1]);
				}
			}

			return permutateitedText.ToString();
		}

		private string KeyPreparation()
		{
			var key = new StringBuilder();
			int KeyPreparationTableRowCount = KeyPreparationTable.GetLength(0);
			int KeyPreparationTableCollumnCount = KeyPreparationTable.GetLength(1);

			for (int i = 0; i < KeyPreparationTableRowCount; i++)
			{
				for (int j = 0; j < KeyPreparationTableCollumnCount; j++)
				{
					int index = KeyPreparationTable[i, j];

					key.Append(_binaryKey[index - 1]);
				}
			}

			return key.ToString();
		}

		private string KeyCompletion(string shiftedKey)
		{
			var key = new StringBuilder();
			int KeyCompletionTableRowCount = KeyCompletionTable.GetLength(0);
			int KeyCompletionTableCollumnCount = KeyCompletionTable.GetLength(1);

			for (int i = 0; i < KeyCompletionTableRowCount; i++)
			{
				for (int j = 0; j < KeyCompletionTableCollumnCount; j++)
				{
					int index = KeyCompletionTable[i, j];

					key.Append(shiftedKey[index - 1]);
				}
			}

			return key.ToString();
		}

		private string KeyGeneration(int roundNumber)
		{
			string key = KeyPreparation();
			string shiftedKey = ShiftKey(key,roundNumber);

			return KeyCompletion(shiftedKey);
		}

		private string ShiftKey(string key, int roundNumber)
		{
			string C = key.Substring(0, 28);
			string D = key.Substring(28, 28);

			for (int i = 0; i <= roundNumber; i++)
			{
				if (i == 0 || i == 1 || i == 8 || i == 15)
				{
					C = LeftShift(C, 1);
					D = LeftShift(D, 1);
				}
				else
				{
					C = LeftShift(C, 2);
					D = LeftShift(D, 2);
				}
			}

			return C + D;
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