namespace AffineCipher
{
    public class GCD
    {
        public static int GetGCD(int a, int b)
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

        private static int Min(int x, int y) => x < y ? x : y;

        private static int Max(int x, int y) => x > y ? x : y;
    }
}