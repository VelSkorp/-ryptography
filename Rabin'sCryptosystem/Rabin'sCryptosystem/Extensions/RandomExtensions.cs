using System;

namespace Rabin_sCryptosystem
{
	public static class RandomExtensions
	{
		public static int NextPrime(this Random random, int maxValue)
		{
			int number;

			do
			{
				number = random.Next(4, maxValue);
			} while (!number.IsPrime());

			return number;
		}

		public static int NextPrime(this Random random, int minValue, int maxValue)
		{
			int number;

			do
			{
				number = random.Next(minValue, maxValue);
			} while (!number.IsPrime());

			return number;
		}
	}
}