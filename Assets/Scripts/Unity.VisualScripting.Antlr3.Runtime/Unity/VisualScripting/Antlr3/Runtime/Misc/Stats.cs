namespace Unity.VisualScripting.Antlr3.Runtime.Misc
{
	public class Stats
	{
		public static double Stddev(int[] X)
		{
			int num = X.Length;
			if (num <= 1)
			{
				return 0.0;
			}
			double num2 = Avg(X);
			double num3 = 0.0;
			for (int i = 0; i < num; i++)
			{
				num3 += ((double)X[i] - num2) * ((double)X[i] - num2);
			}
			num3 /= (double)(num - 1);
			return global::System.Math.Sqrt(num3);
		}

		public static double Avg(int[] X)
		{
			double num = 0.0;
			int num2 = X.Length;
			if (num2 == 0)
			{
				return 0.0;
			}
			for (int i = 0; i < num2; i++)
			{
				num += (double)X[i];
			}
			if (num >= 0.0)
			{
				return num / (double)num2;
			}
			return 0.0;
		}

		public static int Min(int[] X)
		{
			int num = int.MaxValue;
			int num2 = X.Length;
			if (num2 == 0)
			{
				return 0;
			}
			for (int i = 0; i < num2; i++)
			{
				if (X[i] < num)
				{
					num = X[i];
				}
			}
			return num;
		}

		public static int Max(int[] X)
		{
			int num = int.MinValue;
			int num2 = X.Length;
			if (num2 == 0)
			{
				return 0;
			}
			for (int i = 0; i < num2; i++)
			{
				if (X[i] > num)
				{
					num = X[i];
				}
			}
			return num;
		}

		public static int Sum(int[] X)
		{
			int num = 0;
			int num2 = X.Length;
			if (num2 == 0)
			{
				return 0;
			}
			for (int i = 0; i < num2; i++)
			{
				num += X[i];
			}
			return num;
		}

		public static void WriteReport(string filename, string data)
		{
			string absoluteFileName = GetAbsoluteFileName(filename);
			global::System.IO.FileInfo fileInfo = new global::System.IO.FileInfo(absoluteFileName);
			fileInfo.Directory.Create();
			try
			{
				global::System.IO.StreamWriter streamWriter = new global::System.IO.StreamWriter(fileInfo.FullName, append: true);
				streamWriter.WriteLine(data);
				streamWriter.Close();
			}
			catch (global::System.IO.IOException e)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.Misc.ErrorManager.InternalError("can't write stats to " + absoluteFileName, e);
			}
		}

		public static string GetAbsoluteFileName(string filename)
		{
			return global::System.IO.Path.Combine(global::System.IO.Path.Combine(global::System.Environment.CurrentDirectory, global::Unity.VisualScripting.Antlr3.Runtime.Constants.ANTLRWORKS_DIR), filename);
		}
	}
}
