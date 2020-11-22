using System;

namespace RSA
{
	public class Program
	{
		public static void Main(string[] args)
		{
			RSA rsa = new RSA(3, 11);

			string encryptedMessage = rsa.Encrypt("Encrypted Message");

			Console.WriteLine(encryptedMessage);

			Console.WriteLine("Public key:" + rsa.PublicKey);

			Console.WriteLine(rsa.Decrypt(encryptedMessage));
		}
	}
}