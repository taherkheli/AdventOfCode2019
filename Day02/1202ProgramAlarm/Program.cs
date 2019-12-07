using System;
using System.IO;

namespace Alarm
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";

      IntCodeExecutor intCodeExecutor = new IntCodeExecutor(LoadInput(path));

      foreach (var item in intCodeExecutor.Execute())      
        Console.WriteLine(item);   

      Console.WriteLine("\nIntcode Program executed successfully!\n");
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