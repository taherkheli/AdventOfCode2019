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
      int answer2 = 0;

      var highest = 0;
      Asteroid actualAsteroid = null;

      for (int i = 0; i < grid.Asteroids.Count; i++)
      {
        var count = grid.GetAsteroidsVisibleTo(grid.Asteroids[i]).Count;
        //Console.WriteLine("Asteroid on position ({0},{1}) can detect:  {2}", grid.Asteroids[i].Position.X, grid.Asteroids[i].Position.Y, count);

        if (count > highest)
        {
          highest = count;
          actualAsteroid = grid.Asteroids[i];
        }
      }
      
      var X = actualAsteroid.Position.X;
      var Y = actualAsteroid.Position.Y;
      Console.WriteLine("\nAsteroid on position ({0},{1}) wins as it can detect:  {2}\n", X, Y, highest);
      
      /************* Part II ****************/

      Point center = new Point(actualAsteroid.Position.X, actualAsteroid.Position.Y);
      int vaporizedCount = 0;

      while (grid.Asteroids.Count > 1) //all except the monitoring station
      {
        vaporizedCount = Vaporize(grid, center, vaporizedCount, grid.GetPointsListQ1(center), ref answer2);    
        vaporizedCount = Vaporize(grid, center, vaporizedCount, grid.GetPointsListQ2(center), ref answer2);
        vaporizedCount = Vaporize(grid, center, vaporizedCount, grid.GetPointsListQ3(center), ref answer2);
        vaporizedCount = Vaporize(grid, center, vaporizedCount, grid.GetPointsListQ4(center), ref answer2);
      }

      Console.WriteLine("\nPartII:  Answer : {0}", answer2);
    }

    private static int Vaporize(Grid grid, Point center, int vaporizedCount, List<Point> pointsOfInterest, ref int result)
    {
      foreach (var p in pointsOfInterest)
      {
        var lineOfSight = grid.GetLineOfSight(center, p);

        for (int i = 0; i < lineOfSight.Count; i++)
        {
          var asteroid = grid.Asteroids.Find(a => (a.Position.X == lineOfSight[i].X) && (a.Position.Y == lineOfSight[i].Y));

          if (asteroid != null)
          {
            vaporizedCount++;
            grid.Asteroids.Remove(asteroid);
            Console.WriteLine("Asteroid #{0} to be vaporized is at ({1},{2})", vaporizedCount, asteroid.Position.X, asteroid.Position.Y);

            if (vaporizedCount == 200)
              result = 100* asteroid.Position.X + asteroid.Position.Y;
            
            break;
          }
        }
      }

      return vaporizedCount;
    }
  }
}