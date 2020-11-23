using System;

namespace ElGamalAlgorithm
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var elGamal = new ElGamal();
			var a = elGamal.Encrypt("Hello");

			Console.WriteLine(a);
			Console.WriteLine(elGamal.Decrypt(a));
		}
	}
}