﻿using System.IO;

namespace IntCodeExecutorNs
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      IntCodeExecutor intCodeExecutor = new IntCodeExecutor(LoadInput(path));
      intCodeExecutor.Initialize();
      intCodeExecutor.Execute();
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
