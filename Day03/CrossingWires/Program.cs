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

      //TODO: finding intersections in bands worked super for part 1 performance improvement! can be set to 100 and everything wikll work well
      //for part 2 it has to be sufficiently big to guarantee that the band is big enough to contain all the steps a wire takes
      //after some hit and trial, I foudn 3000 to be a good number as 2800 wont cut it. Not ideal but I am no JLo ;)

      int start = 0;
      int step = 3000;    //read comment above        
      var intersections = new List<Point>();

      while (intersections.Count < 1)
      {
        var w1 = wire1.FindPointsInBand(start, start+step);
        var w2 = wire2.FindPointsInBand(start, start+step);
        intersections = Wire.FindAllCrossings(w1, w2);
        start += step;
      }

      Console.WriteLine("\nPart1: shortest Manhattan distance: {0}", CalculateManhattanDistance(intersections));

      //Part 2
      int count = intersections.Count;
      int[] totalSteps = new int[count];

      for (int i = 0; i < count; i++)
        totalSteps[i] = wire1.StepsTakenToReach(intersections[i]) + wire2.StepsTakenToReach(intersections[i]);

      Array.Sort(totalSteps);
      Console.WriteLine("\nPart II: fewest combined steps: {0}", totalSteps[0]);
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