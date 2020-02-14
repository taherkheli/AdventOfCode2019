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

      int vaporizedCount = 0;
      int rotationCount = 0;

      while (grid.Asteroids.Count > 1) //all except the monitoring station
      {
        rotationCount++;
        Console.WriteLine("Rotation #: {0}", rotationCount);

        //process Quad1
        Console.WriteLine("\nProcessing quadrant 1");
        var pointsOfInterest = grid.GetPointsListQ1(new Point(X, Y));
        var toBeVaporized = new List<Asteroid>() { };
        foreach (var p in pointsOfInterest)
        {
          var line = grid.GetLine(actualAsteroid, new Asteroid(p));
          int index_a = line.FindIndex(p => (p.X == X) && (p.Y == Y));
          //look for an asteroid, mark it for deletion if found, and break
          for (int j = index_a - 1; j > -1; j--)
          {
            var asteroid = grid.Asteroids.Find(a => (a.Position.X == line[j].X) && (a.Position.Y == line[j].Y));
            if (asteroid != null)
            {
              if (toBeVaporized.Contains(asteroid) == false)
              {
                toBeVaporized.Add(asteroid);
                vaporizedCount++;
                Console.WriteLine("Asteroid #{0} to be vaporized is at ({1},{2})", vaporizedCount, asteroid.Position.X, asteroid.Position.Y);
              }
              break;
            }
          }
        }
        foreach (var asteroid in toBeVaporized)
          grid.Asteroids.Remove(asteroid);

        //process Quad2
        Console.WriteLine("\nProcessing quadrant 2");
        pointsOfInterest = grid.GetPointsListQ2(new Point(X, Y));
        foreach (var p in pointsOfInterest)
        {
          var line = grid.GetLine(actualAsteroid, new Asteroid(p));
          int index_center = line.FindIndex(p => (p.X == X) && (p.Y == Y));
          for (int i = index_center + 1; i < line.Count; i++)
          {
            var asteroid = grid.Asteroids.Find(a => (a.Position.X == line[i].X) && (a.Position.Y == line[i].Y));
            if (asteroid != null)
            {
              if (toBeVaporized.Contains(asteroid) == false)
              {
                toBeVaporized.Add(asteroid);
                vaporizedCount++;
                Console.WriteLine("Asteroid #{0} to be vaporized is at ({1},{2})", vaporizedCount, asteroid.Position.X, asteroid.Position.Y);
              }
              break;
            }
          }
        }   
        foreach (var asteroid in toBeVaporized)
            grid.Asteroids.Remove(asteroid);


        }
    }
  }
}