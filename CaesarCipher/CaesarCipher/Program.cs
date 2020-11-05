using System;

namespace CaesarCipher
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("введите ключ");
			int key = int.Parse(Console.ReadLine());
			Console.WriteLine("введите сообщение");
			string message = Console.ReadLine();
			var caesarСipher = new CaesarСipher();
			string encryptedMessage = caesarСipher.Encode(key, message);
			Console.WriteLine(encryptedMessage);
			Console.WriteLine(caesarСipher.Decode(key, encryptedMessage));
		}
	}
}