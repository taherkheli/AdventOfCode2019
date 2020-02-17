using AsteroidDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AsteroidDetectionTest
{
  [TestClass]
  public class GridTest
  {
    [TestMethod]
    public void GetLineOfSightTestA()
    {
      List<string> lines = new List<string>
      {
        ".#..#",
        ".....",
        "#####",
        "....#",
        "...##"
      };

      var expected = new List<Point>() {  new Point(0,0),
                                          new Point(1,0),
                                          new Point(2,0),
                                          new Point(3,0),
                                          new Point(4,0)
                                          };

      var grid = new Grid(lines);
      Asteroid a = grid.Asteroids[0];
      Asteroid b = grid.Asteroids[1];

      var actual = grid.GetLine(a, b);

      CollectionAssert.AreEqual(expected, actual, "OK!");
    }

    [TestMethod]
    public void GetLineOfSightTestB()
    {
      List<string> lines = new List<string>
      {
        ".#..#",
        ".....",
        "#####",
        "....#",
        "...##"
      };

      var expected = new List<Point>() {  new Point(4,0),
                                          new Point(4,1),
                                          new Point(4,2),
                                          new Point(4,3),
                                          new Point(4,4)
                                          };

      var grid = new Grid(lines);
      Asteroid a = grid.Asteroids[7];
      Asteroid b = grid.Asteroids[9];

      var actual = grid.GetLine(a, b);

      CollectionAssert.AreEqual(expected, actual, "OK!");
    }

    [TestMethod]
    public void GetLineOfSightTestC()
    {
      List<string> lines = new List<string>
      {
        ".#..#",
        ".....",
        "#####",
        "....#",
        "...##"
      };

      var expected = new List<Point>() {  new Point(4,0),
                                          new Point(4,1),
                                          new Point(4,2),
                                          new Point(4,3),
                                          new Point(4,4)
                                          };

      var grid = new Grid(lines);
      Asteroid a = grid.Asteroids[7];
      Asteroid b = grid.Asteroids[1];

      var actual = grid.GetLine(a, b);

      CollectionAssert.AreEqual(expected, actual, "OK!");
    }

    [TestMethod]
    public void TC1()
    {
      string path = "TC1.txt";
      var lines = File.ReadAllLines(path).ToList<string>();
      var grid = new Grid(lines);

      var highest = 0;
      Asteroid actualAsteroid = null;

      for (int i = 0; i < grid.Asteroids.Count; i++)
      {
        var count = grid.GetAsteroidsVisibleTo(grid.Asteroids[i]).Count;

        if (count > highest)
        {
          highest = count;
          actualAsteroid = grid.Asteroids[i];
        }
      }

      int expectedHighest = 33;
      int expectedX = 5;
      int expectedY = 8;
      Assert.AreEqual<int>(expectedHighest, highest);
      Assert.AreEqual<int>(expectedX, actualAsteroid.Position.X);
      Assert.AreEqual<int>(expectedY, actualAsteroid.Position.Y);
    }

    [TestMethod]
    public void TC2()
    {
      string path = "TC2.txt";
      var lines = File.ReadAllLines(path).ToList<string>();
      var grid = new Grid(lines);

      var highest = 0;
      Asteroid actualAsteroid = null;

      for (int i = 0; i < grid.Asteroids.Count; i++)
      {
        var count = grid.GetAsteroidsVisibleTo(grid.Asteroids[i]).Count;

        if (count > highest)
        {
          highest = count;
          actualAsteroid = grid.Asteroids[i];
        }
      }

      int expectedHighest = 35;
      int expectedX = 1;
      int expectedY = 2;
      Assert.AreEqual<int>(expectedHighest, highest);
      Assert.AreEqual<int>(expectedX, actualAsteroid.Position.X);
      Assert.AreEqual<int>(expectedY, actualAsteroid.Position.Y);
    }

    [TestMethod]
    public void TC3()
    {
      string path = "TC3.txt";
      var lines = File.ReadAllLines(path).ToList<string>();
      var grid = new Grid(lines);

      var highest = 0;
      Asteroid actualAsteroid = null;

      for (int i = 0; i < grid.Asteroids.Count; i++)
      {
        var count = grid.GetAsteroidsVisibleTo(grid.Asteroids[i]).Count;

        if (count > highest)
        {
          highest = count;
          actualAsteroid = grid.Asteroids[i];
        }
      }

      int expectedHighest = 41;
      int expectedX = 6;
      int expectedY = 3;
      Assert.AreEqual<int>(expectedHighest, highest);
      Assert.AreEqual<int>(expectedX, actualAsteroid.Position.X);
      Assert.AreEqual<int>(expectedY, actualAsteroid.Position.Y);
    }

    [TestMethod]
    public void TC4()
    {
      string path = "TC4.txt";
      var lines = File.ReadAllLines(path).ToList<string>();
      var grid = new Grid(lines);

      var highest = 0;
      Asteroid actualAsteroid = null;

      for (int i = 0; i < grid.Asteroids.Count; i++)
      {
        var count = grid.GetAsteroidsVisibleTo(grid.Asteroids[i]).Count;

        if (count > highest)
        {
          highest = count;
          actualAsteroid = grid.Asteroids[i];
        }
      }

      int expectedHighest = 210;
      int expectedX = 11;
      int expectedY = 13;
      Assert.AreEqual<int>(expectedHighest, highest);
      Assert.AreEqual<int>(expectedX, actualAsteroid.Position.X);
      Assert.AreEqual<int>(expectedY, actualAsteroid.Position.Y);
    }

    //Part II applied to the same file (TC4) i.e. the example with monitoring station @(11,13) and detected asteroids = 210 
    [TestMethod]
    public void TC5()
    {
      string path = "TC4.txt";
      var lines = File.ReadAllLines(path).ToList<string>();
      var grid = new Grid(lines);

      var highest = 0;
      Asteroid actualAsteroid = null;

      for (int i = 0; i < grid.Asteroids.Count; i++)
      {
        var count = grid.GetAsteroidsVisibleTo(grid.Asteroids[i]).Count;

        if (count > highest)
        {
          highest = count;
          actualAsteroid = grid.Asteroids[i];
        }
      }

      Point center = new Point(actualAsteroid.Position.X, actualAsteroid.Position.Y);
      List<Asteroid> removed = new List<Asteroid>() { };
      int vaporizedCount = 0;

      while (grid.Asteroids.Count > 1) 
      {
        vaporizedCount = Vaporize(grid, center, vaporizedCount, grid.GetPointsListQ1(center), ref removed);
        vaporizedCount = Vaporize(grid, center, vaporizedCount, grid.GetPointsListQ2(center), ref removed);
        vaporizedCount = Vaporize(grid, center, vaporizedCount, grid.GetPointsListQ3(center), ref removed);
        vaporizedCount = Vaporize(grid, center, vaporizedCount, grid.GetPointsListQ4(center), ref removed);
      }
      
      int expectedCount = 299;
      int expectedX_last = 11;
      int expectedY_last = 1;
      int expectedX_200 = 8;
      int expectedY_200 = 2;

      Assert.AreEqual<int>(expectedCount, removed.Count);
      Assert.AreEqual<int>(expectedX_last, removed[298].Position.X);
      Assert.AreEqual<int>(expectedY_last, removed[298].Position.Y);
      Assert.AreEqual<int>(expectedX_200, removed[199].Position.X);
      Assert.AreEqual<int>(expectedY_200, removed[199].Position.Y);
    }

    private static int Vaporize(Grid grid, Point center, int vaporizedCount, List<Point> pointsOfInterest, ref List<Asteroid> removed)
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
            removed.Add(asteroid);
            grid.Asteroids.Remove(asteroid);
            break;
          }
        }
      }

      return vaporizedCount;
    }
  }
}