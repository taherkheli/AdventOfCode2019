using IntCode;
using System;
using System.IO;

namespace IntCode1202
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      Executor executor = new Executor(LoadInput(path));
      executor.Execute();
      Console.WriteLine("\nPart I: The item in position 0 after intcode execution was: {0}", executor.IntCode[0]);    

      /************* Part II ******************/

      int magicNumber = 19690720;
      long verb = 0;
      long noun = 0;
      bool success = false;

      for (int i = 0; i < 100; i++)
      {
        for (int j = 0; j < 100; j++)
        {
          executor.Initialize();
          executor.IntCode[1] = i;
          executor.IntCode[2] = j;
          executor.Execute();
          if (executor.IntCode[0] == magicNumber)
            break;
        }

        if (executor.IntCode[0] == magicNumber)
        {
          noun = executor.IntCode[1];
          verb = executor.IntCode[2];
          success = true;
          break;
        }
      }

      if (success)
        Console.WriteLine("\nPartII: Success! 100 * {0}(noun) + {1}(verb) = {2}", noun, verb, 100 * noun + verb);
      else
        Console.WriteLine("Did not expect this. Nothing found :(");
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