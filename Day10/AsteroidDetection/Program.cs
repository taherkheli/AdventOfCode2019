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
      int rotationCount = 0;

      while (grid.Asteroids.Count > 1) //all except the monitoring station
      {
        rotationCount++;
        Console.WriteLine("\nRotation #: {0}", rotationCount);

        //process Quad1
        Console.WriteLine("\nProcessing quadrant 1");
        var pointsOfInterest = grid.GetPointsListQ1(center);    //points of interest represent one point from each line the laser will shoot at during rotation
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
              break;
            }
          }
        }

        //process Quad2
        Console.WriteLine("\nProcessing quadrant 2");
        pointsOfInterest = grid.GetPointsListQ2(center);
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
              break;
            }
          }
        }

        //process Quad3
        Console.WriteLine("\nProcessing quadrant 3");
        pointsOfInterest = grid.GetPointsListQ3(center);        
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
              break;
            }
          }
        }

        //process Quad4
        Console.WriteLine("\nProcessing quadrant 4");
        pointsOfInterest = grid.GetPointsListQ4(center);    
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
              break;
            }
          }
        }
      }
    }
  }
}