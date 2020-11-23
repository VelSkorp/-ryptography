using System;

namespace ElGamalAlgorithm
{
	public static class RandomExtensions
	{
		public static int NextPrime(this Random random)
		{
			int number;

			do
			{
				number = random.Next(4);
			} while (!number.IsPrime());

			return number;
		}

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

		public static int NextGCD(this Random random, int valueForGCD)
		{
			var gcd = new GCD();
			int number;

			do
			{
				number = random.Next();
			} while (gcd.GetGCD(valueForGCD, number) == 1);

			return number;
		}

		public static int NextGCD(this Random random, int maxValue, int valueForGCD)
		{
			var gcd = new GCD();
			int number;

			do
			{
				number = random.Next(2, maxValue);
			} while (gcd.GetGCD(valueForGCD, number) == 1);

			return number;
		}

		public static int NextGCD(this Random random, int minValue, int maxValue, int valueForGCD)
		{
			var gcd = new GCD();
			int number;

			do
			{
				number = random.Next(minValue, maxValue);
			} while (gcd.GetGCD(valueForGCD, number) == 1);

			return number;
		}
	}
}