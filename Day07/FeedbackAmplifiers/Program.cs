using System;
using System.IO;

namespace FeedbackAmplifiers
{
	class Program
	{
		static void Main()
		{
			/********** Part I ************/

			string path = "input.txt";
			long[] program = LoadInput(path);
			long[] sequence = new long[5] { 1, 0, 4, 3, 2 };
			long highest = -1;
			highest = Helpers.GetHighest(program, sequence, 1);
			Console.WriteLine("\n strongest signal value :  {0}", highest);

			/********** Part II ************/

			sequence = new long[5] { 9, 8, 7, 6, 5 };
			highest = -1;
			highest = Helpers.GetHighest(program, sequence, 2);
			Console.WriteLine("\n strongest signal value :  {0}", highest);
		}

		private static long[] LoadInput(string path)
		{
			StreamReader file = new StreamReader(path);
			string[] strings = file.ReadToEnd().Split(',');
			long[] result = new long[strings.Length];

			for (int i = 0; i < result.Length; i++)
				result[i] = long.Parse(strings[i]);

			return result;
		}
	}
}