namespace ElGamalAlgorithm
{
    public class GCD
    {
        public int GetGCD(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }
            else
            {
                int min = Min(a, b);
                int max = Max(a, b);
                return GetGCD(max % min, min);
            }
        }

        private int Min(int x, int y) => x < y ? x : y;

        private int Max(int x, int y) => x > y ? x : y;
    }
}