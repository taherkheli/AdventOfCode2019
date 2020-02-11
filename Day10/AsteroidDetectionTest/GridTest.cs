using AsteroidDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
      //TODO: Debug this
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
  }

  //[TestClass]
  //public class GridTest
  //{
  //  [TestMethod]
  //  public void GetLineOfSightTestA()
  //  {
  //    List<string> lines = new List<string>
  //    {
  //      "#.........",
  //      "...#......",
  //      "...B..a...",
  //      ".EDCG....a",
  //      "..F.c.b...",
  //      ".....c....",
  //      "..efd.c.gb",
  //      ".......c..",
  //      "....f...c.",
  //      "...e..d..c"
  //    };

  //    var expected = new List<Point>() {  new Point(0,0),
  //                                        new Point(3,1),
  //                                        new Point(6,2),
  //                                        new Point(9,3)
  //                                        };  

  //    var grid = new Grid(lines);
  //    Asteroid a = grid.Asteroids[0];
  //    Asteroid b = grid.Asteroids[1];

  //    var actual = grid.GetLine(a, b);

  //    CollectionAssert.AreEqual(expected, actual, "OK!");
  //  }

  //  [TestMethod]
  //  public void GetLineOfSightTestB()
  //  {
  //    List<string> lines = new List<string>
  //    {
  //      "#.........",
  //      "...A......",
  //      "...#..a...",
  //      ".EDCG....a",
  //      "..F.c.b...",
  //      ".....c....",
  //      "..efd.c.gb",
  //      ".......c..",
  //      "....f...c.",
  //      "...e..d..c"
  //    };

  //    var expected = new List<Point>() {  new Point(0,0),
  //                                        new Point(3,2),
  //                                        new Point(6,4),
  //                                        new Point(9,6)
  //                                        };

  //    var grid = new Grid(lines);
  //    Asteroid a = grid.Asteroids[0];
  //    Asteroid b = grid.Asteroids[1];

  //    var actual = grid.GetLine(a, b);

  //    CollectionAssert.AreEqual(expected, actual, "OK!");
  //  }

  //  [TestMethod]
  //  public void GetLineOfSightTestC()
  //  {
  //    List<string> lines = new List<string>
  //    {
  //      "#.........",
  //      "...A......",
  //      "...B..a...",
  //      ".ED#G....a",
  //      "..F.c.b...",
  //      ".....c....",
  //      "..efd.c.gb",
  //      ".......c..",
  //      "....f...c.",
  //      "...e..d..c"
  //    };

  //    var expected = new List<Point>() {  new Point(0,0),
  //                                        new Point(3,3),
  //                                        new Point(4,4),
  //                                        new Point(5,5),
  //                                        new Point(6,6),
  //                                        new Point(7,7),
  //                                        new Point(8,8),
  //                                        new Point(9,9)
  //                                        };

  //    var grid = new Grid(lines);
  //    Asteroid a = grid.Asteroids[0];
  //    Asteroid b = grid.Asteroids[1];

  //    var actual = grid.GetLine(a, b);

  //    CollectionAssert.AreEqual(expected, actual, "OK!");
  //  }

  //  [TestMethod]
  //  public void GetLineOfSightTestD()
  //  {
  //    List<string> lines = new List<string>
  //    {
  //      "#.........",
  //      "...A......",
  //      "...B..a...",
  //      ".E#CG....a",
  //      "..F.c.b...",
  //      ".....c....",
  //      "..efd.c.gb",
  //      ".......c..",
  //      "....f...c.",
  //      "...e..d..c"
  //    };

  //    var expected = new List<Point>() {  new Point(0,0),
  //                                        new Point(2,3),
  //                                        new Point(4,6),
  //                                        new Point(6,9)
  //                                        };

  //    var grid = new Grid(lines);
  //    Asteroid a = grid.Asteroids[0];
  //    Asteroid b = grid.Asteroids[1];

  //    var actual = grid.GetLine(a, b);

  //    CollectionAssert.AreEqual(expected, actual, "OK!");
  //  }

  //  [TestMethod]
  //  public void GetLineOfSightTestE()
  //  {
  //    List<string> lines = new List<string>
  //    {
  //      "#.........",
  //      "...A......",
  //      "...B..a...",
  //      ".#DCG....a",
  //      "..F.c.b...",
  //      ".....c....",
  //      "..efd.c.gb",
  //      ".......c..",
  //      "....f...c.",
  //      "...e..d..c"
  //    };

  //    var expected = new List<Point>() {  new Point(0,0),
  //                                        new Point(1,3),
  //                                        new Point(2,6),
  //                                        new Point(3,9)
  //                                        };

  //    var grid = new Grid(lines);
  //    Asteroid a = grid.Asteroids[0];
  //    Asteroid b = grid.Asteroids[1];

  //    var actual = grid.GetLine(a, b);

  //    CollectionAssert.AreEqual(expected, actual, "OK!");
  //  }

  //  [TestMethod]
  //  public void GetLineOfSightTestF()
  //  {
  //    List<string> lines = new List<string>
  //    {
  //      "#.........",
  //      "...A......",
  //      "...B..a...",
  //      ".EDCG....a",
  //      "..#.c.b...",
  //      ".....c....",
  //      "..efd.c.gb",
  //      ".......c..",
  //      "....f...c.",
  //      "...e..d..c"
  //    };

  //    var expected = new List<Point>() {  new Point(0,0),
  //                                        new Point(2,4),
  //                                        new Point(3,6),
  //                                        new Point(4,8)
  //                                        };

  //    var grid = new Grid(lines);
  //    Asteroid a = grid.Asteroids[0];
  //    Asteroid b = grid.Asteroids[1];

  //    var actual = grid.GetLine(a, b);

  //    CollectionAssert.AreEqual(expected, actual, "OK!");
  //  }

  //  [TestMethod]
  //  public void GetLineOfSightTestG()
  //  {
  //    List<string> lines = new List<string>
  //    {
  //      "#.........",
  //      "...A......",
  //      "...B..a...",
  //      ".EDC#....a",
  //      "..F.c.b...",
  //      ".....c....",
  //      "..efd.c.gb",
  //      ".......c..",
  //      "....f...c.",
  //      "...e..d..c"
  //    };

  //    var expected = new List<Point>() {  new Point(0,0),
  //                                        new Point(4,3),
  //                                        new Point(8,6)
  //                                        };

  //    var grid = new Grid(lines);
  //    Asteroid a = grid.Asteroids[0];
  //    Asteroid b = grid.Asteroids[1];

  //    var actual = grid.GetLine(a, b);

  //    CollectionAssert.AreEqual(expected, actual, "OK!");
  //  }

  //  [TestMethod]
  //  public void GetLineOfSightTest_BackwardsA()
  //  {
  //    List<string> lines = new List<string>
  //    {
  //      "a.........",
  //      "...#......",
  //      "...B..#...",
  //      ".EDCG....a",
  //      "..F.c.b...",
  //      ".....c....",
  //      "..efd.c.gb",
  //      ".......c..",
  //      "....f...c.",
  //      "...e..d..c"
  //    };

  //    var expected = new List<Point>() {  new Point(0,0),
  //                                        new Point(3,1),
  //                                        new Point(6,2),
  //                                        new Point(9,3)
  //                                        };

  //    var grid = new Grid(lines);
  //    Asteroid a = grid.Asteroids[0];
  //    Asteroid b = grid.Asteroids[1];

  //    var actual = grid.GetLine(a, b);

  //    CollectionAssert.AreEqual(expected, actual, "OK!");
  //  }

  //  [TestMethod]
  //  public void GetLineOfSightTest_BackwardsC()
  //  {
  //    List<string> lines = new List<string>
  //    {
  //      "c.........",
  //      ".c.A......",
  //      "..cB..a...",
  //      ".EDcG....a",
  //      "..F.#.b...",
  //      ".....#....",
  //      "..efd.c.gb",
  //      ".......c..",
  //      "....f...c.",
  //      "...e..d..c"
  //    };

  //    var expected = new List<Point>() {  new Point(0,0),
  //                                        new Point(1,1),
  //                                        new Point(2,2),
  //                                        new Point(3,3),
  //                                        new Point(4,4),
  //                                        new Point(5,5),
  //                                        new Point(6,6),
  //                                        new Point(7,7),
  //                                        new Point(8,8),
  //                                        new Point(9,9)
  //                                        };

  //    var grid = new Grid(lines);
  //    Asteroid a = grid.Asteroids[0];
  //    Asteroid b = grid.Asteroids[1];

  //    var actual = grid.GetLine(a, b);

  //    CollectionAssert.AreEqual(expected, actual, "OK!");
  //  }
  //}
}
