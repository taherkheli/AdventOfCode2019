using System;
using System.Collections.Generic;
using System.IO;

namespace CrossingWires
{
  class Program
  {
    static void Main()
    {
      Wire wire1 = new Wire();
      Wire wire2 = new Wire();
      string path = "input.txt";

      StreamReader file = new StreamReader(path);
      string[] strings = file.ReadToEnd().Split((char)ConsoleKey.Enter);

      wire1.Move(LoadInput(strings[0]));
      wire2.Move(LoadInput(strings[1].Substring(1)));  //get rid of '\n' in second string

      var crosses = wire1.FindAllCrossingsWith(wire2);

      Console.WriteLine(CalculateManhattanDistance(crosses));
    }

    private static List<Movement> LoadInput(string path)
    {
      List<Movement> result = new List<Movement>();

      string[] strings = path.Split(',');

      foreach (var str in strings)
        result.Add(ExtractMovement(str));

      return result;
    }

    private static Movement ExtractMovement(string str)
    {
      Movement result = new Movement { };
      var firstChar = str[0];      
      result.StepSize = int.Parse(str.Trim(firstChar));

      switch (firstChar)
      {
        case 'U':
          result.Direction = Direction.Up;
          break;
        case 'D':
          result.Direction = Direction.Down;
          break;
        case 'L':
          result.Direction = Direction.Left;
          break;
        case 'R':
          result.Direction = Direction.Right;
          break;
        default:
          throw new InvalidOperationException("Wrong input!");
      }

      return result;      
    }

    private static int CalculateManhattanDistance(List<Point> points)
    {
      int[] distances = new int[points.Count];

      for (int i = 0; i < points.Count; i++)
        distances[i] = Math.Abs(points[i].X) + Math.Abs(points[i].Y);

      Array.Sort(distances);

      return distances[0];
    }
  }
}