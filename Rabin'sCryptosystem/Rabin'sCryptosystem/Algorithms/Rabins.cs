using System;
using System.Collections.Generic;
using System.Text;

namespace Rabin_sCryptosystem
{
	public class Rabins
	{
		private int _p;
		private int _q;
		private int _n;
		private int _b;

		public List<int> PublicKey => new List<int> { _n, _b };

		public Rabins()
		{
			GeneratePAndQ(out _p, out _q);

			_n = _p * _q;

			var random = new Random();

			_b = random.Next(_n - 250);
		}

		public Rabins(int p, int q)
		{
			_p = p;
			_q = q;
			_n = p * q;

			var random = new Random();

			_b = random.Next(_n);
		}

		public void GeneratePAndQ(out int p, out int q)
		{
			var random = new Random();

			do
			{
				p = random.NextPrime(10, 17);
				q = random.NextPrime(20, 30);
			} while (!(p % 4 == 3 && q % 4 == 3));
		}

		public string Encrypt(string message)
		{
			byte[] chars = Encoding.Default.GetBytes(message);

			var encyptedMessage = new List<byte>();

			foreach (byte character in chars)
			{
				encyptedMessage.Add((byte)(character * (character + _b) % _n));
			}

			return Encoding.Default.GetString(encyptedMessage.ToArray());
		}

		public string Decrypt(string encyptedMessage)
		{
			byte[] chars = Encoding.Default.GetBytes(encyptedMessage);
			var decyptedMessage = new List<byte>();

			foreach (byte character in chars)
			{
				GetSqare(out int r, out int s, character);
				GetSquareRoots(out List<int> dm, r, s);

				for (int i = 0; i < dm.Count; i++)
				{
					decyptedMessage.Add((byte)(((-_b + dm[i]) / 2) % _n));
				}

				decyptedMessage.Add((byte)'\n');
			}

			return Encoding.Default.GetString(decyptedMessage.ToArray());
		}

		private void ShareAlgoryeOfEyclid(out int d, out int u, out int v, int a, int b)
		{
			if (b == 0)
			{
				d = a;
				u = 1;
				v = 0;
				return;
			}

			int u2 = 1, u1 = 0, v2 = 0, v1 = 1, q, r;

			while (b > 0)
			{
				q = a / b;
				r = a - q * b;
				u = u2 - q * u1;
				v = v2 - q * v1;
				a = b;
				b = r;
				u2 = u1;
				u1 = u;
				v2 = v1;
				v1 = v;
			}
			d = a;
			u = u2;
			v = v2;
		}

		private void GetSqare(out int r,out int s, byte character)
		{
			int discriminant = (int)((Math.Pow(_b, 2) + 4 * character) % _n);
			int power = (_p + 1) / 4;
			s = (int)(Math.Pow(discriminant, power) % _p);

			power = (_q + 1) / 4;
			r = (int)(Math.Pow(discriminant, power) % _q);
		}

		private void GetSquareRoots(out List<int> dm, int r, int s)
		{
			int D, c, d;
			do
			{
				ShareAlgoryeOfEyclid(out D, out c, out d, _p, _q);
			} while (D != 1);

			int d1 = (c * _p * r + d * _q * s) % _n;

			while (d1 < 0)
			{
				d1 += _n;
			}

			int d2 = _n - d1;
			int d3 = (c * _p * r - d * _q * s) % _n;

			while (d3 < 0)
			{
				d3 += _n;
			}

			int d4 = _n - d3;

			dm = new List<int>
			{
				d1,
				d2,
				d3,
				d4
			};
		}
	}
}