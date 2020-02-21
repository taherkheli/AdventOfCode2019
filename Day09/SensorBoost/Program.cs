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
      Executor executor = new Executor(LoadInput(path));
      executor.Initialize();
      executor.InputQueue.Enqueue(1);
      executor.Execute();
      Console.WriteLine("\nPart I :  {0}", (long)executor.OutputQueue.Dequeue());

      //reload intcode
      executor = new Executor(LoadInput(path));
      executor.InputQueue.Enqueue(2);
      executor.Execute();
      Console.WriteLine("\nPart II :  {0}", (long)executor.OutputQueue.Dequeue());
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
