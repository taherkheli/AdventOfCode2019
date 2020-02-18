using IntCode;
using System;
using System.IO;

namespace PaintingRobot
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      Executor executor = new Executor(LoadInput(path));
      executor.Initialize();
      executor.InputQueue.Enqueue(1);
      var x = executor.Execute();
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
