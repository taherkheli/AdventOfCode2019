using System;
using System.IO;

namespace IntCodeExecutorPartI
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      IntCodeExecutor intCodeExecutor = new IntCodeExecutor(LoadInput(path));
      intCodeExecutor.Execute();   
      Console.WriteLine("\nThe item in position 0 after intcode execution was: {0}", intCodeExecutor.IntCode[0]); 
      Console.WriteLine("\nIntcode executed successfully!\n");
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