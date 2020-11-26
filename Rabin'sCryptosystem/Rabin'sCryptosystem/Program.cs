using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabin_sCryptosystem
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Rabins rabins = new Rabins();
			Console.WriteLine(rabins.Decrypt(rabins.Encrypt("Hello")));
		}
	}
}
