using System;

namespace Algorithm_Blum_Blum_Shub
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var f = new BBS(243793, 48914);
			Console.WriteLine(f.Decrypt(f.Encrypt("коза").ToArray()));
			Console.WriteLine(f.GenerateBinarySequence(8));
		}
	}
}