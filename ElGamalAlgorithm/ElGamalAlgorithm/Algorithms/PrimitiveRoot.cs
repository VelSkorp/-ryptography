using System.Collections.Generic;

namespace ElGamalAlgorithm
{
    public class PrimitiveRoot
    {
        public int GetPRoot(int p)
        {
            for (int i = 0; i < p; i++)
            {
                if (IsPRoot(p, i))
                {
                    return i;
                }
            }

            return default;
        }

        private bool IsPRoot(int p, int a)
        {
            if (a == 0 || a == 1)
                return false;

            int last = 1;
            var set = new HashSet<int>();

            for (int i = 0; i < p - 1; i++)
            {
                last = (last * a) % p;

                if (set.Contains(last))
                {
                    return false;
                }

                set.Add(last);
            }

            return true;
        }
    }
}