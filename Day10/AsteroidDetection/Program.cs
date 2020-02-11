using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AsteroidDetection
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      var lines = File.ReadAllLines(path).ToList<string>();
      var grid = new Grid(lines);

      var highest = 0;

      for (int i = 0; i < grid.Asteroids.Count; i++)
      {
        var X = grid.Asteroids[i].Position.X;
        var Y = grid.Asteroids[i].Position.Y;
        var count = grid.GetAsteroidsVisibleTo(grid.Asteroids[i]).Count;
        Console.WriteLine("Asteroid on position ({0},{1}) can detect:  {2}", X, Y, count);

        if (count > highest)
          highest = count;
      }

      Console.WriteLine("The highest was : {0}", highest);
    }
  }
}
