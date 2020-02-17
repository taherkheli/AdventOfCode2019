using IntCode;
using System.IO;

namespace IntCodeExecutorNs
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      Executor intCodeExecutor = new Executor(LoadInput(path));
      intCodeExecutor.Initialize();
      intCodeExecutor.Execute();
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
