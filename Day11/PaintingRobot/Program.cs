using System;
using System.IO;

namespace PaintingRobot
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      Robot robot = new Robot(LoadInput(path), 2500);
      robot.Go();
      Console.WriteLine(robot.Grid.PaintedPanels); 
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