using IntCodeExecutioner;
using System;
using System.IO;

namespace IntCodeExecutor
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      int[] program = LoadInput(path);
      int[] sequence = new int[5] { 0, 1, 2, 3, 4 };
      int highest = -1;
      var permutations = PermutationCalculator.GetCombinations(sequence);

      foreach (var permutation in permutations)
      {
        var temp = Calculate(program, permutation.ToArray());
        if (temp > highest)
          highest = temp;   
      }

      Console.WriteLine("\n strongest signal value :  {0}", highest);
    }

    private static int[] LoadInput(string path)
    {
      int[] result;

      using (StreamReader file = new StreamReader(path))
      {
        string[] strings = file.ReadToEnd().Split(',');
        result = new int[strings.Length];

        for (int i = 0; i < result.Length; i++)
          result[i] = int.Parse(strings[i]);
      }

      return result;
    }

    private static int Calculate(int[] program, int[] pattern)
    {
      IntCodeExecutor amp1 = new IntCodeExecutor(program, new int[] { pattern[0], 0 });
      amp1.Execute();
      IntCodeExecutor amp2 = new IntCodeExecutor(program, new int[] { pattern[1], (int)amp1.Output });
      amp2.Execute();
      IntCodeExecutor amp3 = new IntCodeExecutor(program, new int[] { pattern[2], (int)amp2.Output });
      amp3.Execute();
      IntCodeExecutor amp4 = new IntCodeExecutor(program, new int[] { pattern[3], (int)amp3.Output });
      amp4.Execute();
      IntCodeExecutor amp5 = new IntCodeExecutor(program, new int[] { pattern[4], (int)amp4.Output });
      amp5.Execute();

      return (int)amp5.Output;
    } 
  }
}
