namespace Rabin_sCryptosystem
{
	public static class IntExtensions
	{
		public static bool IsPrime(this int num)
		{
			for (int i = 2; i <= num / 2; i++)
			{
				if (num % i == 0)
				{
					return false;
				}
			}

			return true;
		}
	}
}