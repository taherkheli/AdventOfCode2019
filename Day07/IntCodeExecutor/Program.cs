using System;
using System.IO;

namespace IntCodeExecutorNs
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      int[] program = LoadInput(path);
      int[] sequence = new int[5] { 1, 0, 4, 3, 2 };
      int highest = -1;
      highest = Helpers.GetHighest(program, sequence);

      Console.WriteLine("\n strongest signal value :  {0}", highest);
    }
    
    private static int[] LoadInput(string path)
    {
      StreamReader file = new StreamReader(path);
      string[] strings = file.ReadToEnd().Split(',');
      int[] result = new int[strings.Length];

      for (int i = 0; i < result.Length; i++)
        result[i] = int.Parse(strings[i]);

      return result;
    }
  }
}