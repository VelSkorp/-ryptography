namespace GOST_28147_89
{
	public partial class GOST_28147_89Algorithm
	{
		private static readonly int[] SBox1 = new int[]
		{
			4, 10, 9, 2, 13, 8, 0, 14, 6, 11, 1, 12, 7, 15, 5, 3 
		};

		private static readonly int[] SBox2 = new int[]
		{
			14, 11, 4, 12, 6, 13, 15, 10, 2, 3, 8, 1, 0, 7, 5, 9
		};

		private static readonly int[] SBox3 = new int[]
		{
			5, 8, 1, 13, 10, 3, 4, 2, 14, 15, 12, 7, 6, 0, 9, 11
		};

		private static readonly int[] SBox4 = new int[]
		{
			7, 13, 10, 1, 0, 8, 9, 15, 14, 4, 6, 12, 11, 2, 5, 3
		};

		private static readonly int[] SBox5 = new int[]
		{
			6, 12, 7, 1, 5, 15, 13, 8, 4, 10, 9, 14, 0, 3, 11, 2
		};

		private static readonly int[] SBox6 = new int[]
		{
			4, 11, 10, 0, 7, 2, 1, 13, 3, 6, 8, 5, 9, 12, 15, 14
		};

		private static readonly int[] SBox7 = new int[]
		{
			13, 11, 4, 1, 3, 15, 5, 9, 0, 10, 14, 7, 6, 8, 2, 12
		};

		private static readonly int[] SBox8 = new int[]
		{
			1, 15, 13, 0, 5, 7, 10, 4, 9, 2, 3, 14, 6, 11, 8, 12
		};

		private static readonly int[][] SBox = new int[][]
		{
			SBox1,
			SBox2,
			SBox3,
			SBox4,
			SBox5,
			SBox6,
			SBox7,
			SBox8,
		};

		private static readonly int[] SubkeyOrder = new int[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 7, 6, 5, 4, 3, 2, 1, 0
		};
	}
}