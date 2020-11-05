using System;

namespace AffineCipher
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var key = new Key();
			Console.WriteLine("введите a ключ");
			key.A = int.Parse(Console.ReadLine());
			Console.WriteLine("введите b ключ");
			key.B = int.Parse(Console.ReadLine());
			Console.WriteLine("введите сообщение");
			string message = Console.ReadLine();

			var cipher = new AffineCipher(key, message);
			Console.WriteLine(cipher.Encode());

			cipher = new AffineCipher(key, cipher.Encode());
			Console.WriteLine(cipher.Decode());
		}
	}
}