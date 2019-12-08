using System;
using System.IO;

namespace IntCodeExecutorPartII
{
  class Program
  {
    static void Main()
    {
      string path = "input2.txt";
      int magicNumber = 19690720;
      int verb = 0;
      int noun = 0;
      bool success = false;
      IntCodeExecutor intCodeExecutor = new IntCodeExecutor(LoadInput(path));

      for (int i = 0; i < 100; i++)
      {
        for (int j = 0; j < 100; j++)
        {
          intCodeExecutor.Initialize();
          intCodeExecutor.IntCode[1] = i;
          intCodeExecutor.IntCode[2] = j;
          intCodeExecutor.Execute();
          if (intCodeExecutor.IntCode[0] == magicNumber)
            break;
        }
        if (intCodeExecutor.IntCode[0] == magicNumber)
        {
          noun = intCodeExecutor.IntCode[1];
          verb = intCodeExecutor.IntCode[2];
          success = true;
          break;
        }
      }

      if (success)
        Console.WriteLine("\nSuccess! 100 * {0}(noun) + {1}(verb) = {2}", noun, verb, 100*noun+verb);
      else
        Console.WriteLine("Did not expect this. Nothing found :(");
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
