using System.Numerics;

namespace ElGamalAlgorithm
{
    public static class MathExtensions
    {
        public static BigInteger Pow(BigInteger x, BigInteger y)
        {
            if (y == 0)
                return 1;
            if (y % 2 == 1)
                return Pow(x, y - 1) * x;
            else
            {
                BigInteger b = Pow(x, y / 2);
                return b * b;
            }
        }
    }
}