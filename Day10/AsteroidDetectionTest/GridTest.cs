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
  }
}