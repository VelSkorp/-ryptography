using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm_Blum_Blum_Shub
{
	public class BBS
	{
		#region Public Properties

		public int P { get; private set; }
		public int Q { get; private set; }
		public int S { get; private set; }
		public int N { get; private set; } 

		#endregion

		#region Constructors

		public BBS(int p, int q, int s)
		{
			P = p;
			Q = q;
			S = s;
			N = p * q;
		}

		public BBS(int n, int s)
		{
			S = s;
			N = n;
		}

		#endregion

		#region Public Methods

		public string GenerateBinarySequence(int length)
		{
			var sequence = new StringBuilder();
			int num = S;

			for (int i = 0; i < length; i++)
			{
				num = (int)(Math.Pow(num, 2) % N);
				sequence.Append(num % 2);
			}

			return sequence.ToString();
		}

		public List<int> GenerateNumericSequence(int numsCount)
		{
			var sequence = new List<int>();
			int num = S;

			for (int i = 0; i < numsCount; i++)
			{
				num = (int)(Math.Pow(num, 2) % N);
				sequence.Add(num);
			}

			return sequence;
		}

		public string Decrypt(int[] cryptogram)
		{
			int bitCount = (int)Math.Ceiling(Math.Log(N + 1, 2));
			bitCount = (int)Math.Floor(Math.Log(bitCount, 2));

			int length = cryptogram.Length * 8 / bitCount;

			List<int> sequence = GenerateNumericSequence(length);
			sequence = GenerateLeastSignificantSequence(length, bitCount, sequence);

			List<int> fragments = GenerateCiphertextFragments(cryptogram, bitCount);

			return GetPlaintext(fragments, sequence, bitCount);
		}

		public List<int> Encrypt(string message)
		{
			int bitCount = (int)Math.Ceiling(Math.Log(N + 1, 2));
			bitCount = (int)Math.Floor(Math.Log(bitCount, 2));

			int length = message.Length * 8 / bitCount;

			List<int> sequence = GenerateNumericSequence(length);
			sequence = GenerateLeastSignificantSequence(length, bitCount, sequence);

			List<int> fragments = GenerateCiphertextFragments(message, bitCount);

			return GetCiphertextCodes(fragments, sequence, bitCount);
		}

		#endregion

		#region Private Methods

		private List<int> GenerateLeastSignificantSequence(int length, int bitCount, List<int> sequence)
		{
			var newSequence = new List<int>();

			for (int i = 0; i < length; i++)
			{
				newSequence.Add((int)(sequence[i] % Math.Pow(2, bitCount)));
			}

			return newSequence;
		}

		private List<int> GenerateCiphertextFragments(int[] cryptogram, int bitCount)
		{
			var y = new StringBuilder();

			for (int i = 0; i < cryptogram.Length; i++)
			{
				y.Append(Convert.ToString(cryptogram[i], 2).PadLeft(8, '0'));
			}

			var fragments = new List<int>();
			string temp = y.ToString();

			for (int i = 0; i < y.Length; i += bitCount)
			{
				fragments.Add(Convert.ToInt32(temp.Substring(i, bitCount), 2));
			}

			return fragments;
		}

		private List<int> GenerateCiphertextFragments(string message, int bitCount)
		{
			var fragments = new List<int>();
			byte[] bytes = Encoding.GetEncoding(1251).GetBytes(message);

			for (int i = 0; i < bytes.Length; i++)
			{
				fragments.Add(bytes[i]);
			}

			fragments = GenerateCiphertextFragments(fragments.ToArray(), bitCount);

			return fragments;
		}

		private string GetPlaintext(List<int> fragments, List<int> sequence, int bitCount)
		{
			var binaryPlaintext = new StringBuilder();

			for (int i = 0; i < fragments.Count; i++)
			{
				binaryPlaintext.Append(Convert.ToString(sequence[i] ^ fragments[i], 2).PadLeft(bitCount, '0'));
			}

			string temp = binaryPlaintext.ToString();
			var chars = new List<byte>();

			for (int i = 0; i < binaryPlaintext.Length; i += 8)
			{
				chars.Add(Convert.ToByte(temp.Substring(i, 8), 2));
			}

			return Encoding.GetEncoding(1251).GetString(chars.ToArray());
		}

		private List<int> GetCiphertextCodes(List<int> fragments, List<int> sequence, int bitCount)
		{
			string chipertext = GetPlaintext(fragments, sequence, bitCount);
			byte[] bytes = Encoding.GetEncoding(1251).GetBytes(chipertext);
			var codes = new List<int>();

			for (int i = 0; i < bytes.Length; i++)
			{
				codes.Add(bytes[i]);
			}

			return codes;
		} 

		#endregion
	}
}