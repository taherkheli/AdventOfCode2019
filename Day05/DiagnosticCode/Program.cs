using IntCode;
using System;
using System.IO;

namespace Day09
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      var input = LoadInput(path);
      
      Executor executor = new Executor(input);
      executor.InputQueue.Enqueue(1);
      executor.Execute();
      while (executor.OutputQueue.Count > 0)
        Console.WriteLine("\nPart I: Executor returned: {0}", (long)executor.OutputQueue.Dequeue());

      executor.Initialize();
      executor.InputQueue.Enqueue(5);
      executor.Execute();
      while (executor.OutputQueue.Count > 0)
        Console.WriteLine("\nPart II: Executor returned: {0}", (long)executor.OutputQueue.Dequeue());
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
