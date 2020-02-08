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
      List<string> lines = new List<string>();
      lines.Add("#.........");
      lines.Add("...#......");
      lines.Add("...B..a...");
      lines.Add(".EDCG....a");
      lines.Add("..F.c.b...");
      lines.Add(".....c....");
      lines.Add("..efd.c.gb");
      lines.Add(".......c..");
      lines.Add("....f...c.");
      lines.Add("...e..d..c");

      var expected = new List<Point>() {  new Point(0,0),
                                          new Point(3,1),
                                          new Point(6,2),
                                          new Point(9,3)
                                          };  
      
      var grid = new Grid(lines);
      Asteroid a = grid.Asteroids[0];
      Asteroid b = grid.Asteroids[1];

      var actual = grid.GetLineOfSight(a, b);

      CollectionAssert.AreEqual(expected, actual, "OK!");
    }

    [TestMethod]
    public void GetLineOfSightTestB()
    {
      List<string> lines = new List<string>();
      lines.Add("#.........");
      lines.Add("...A......");
      lines.Add("...#..a...");
      lines.Add(".EDCG....a");
      lines.Add("..F.c.b...");
      lines.Add(".....c....");
      lines.Add("..efd.c.gb");
      lines.Add(".......c..");
      lines.Add("....f...c.");
      lines.Add("...e..d..c");

      var expected = new List<Point>() {  new Point(0,0),
                                          new Point(3,2),
                                          new Point(6,4),
                                          new Point(9,6)
                                          };

      var grid = new Grid(lines);
      Asteroid a = grid.Asteroids[0];
      Asteroid b = grid.Asteroids[1];

      var actual = grid.GetLineOfSight(a, b);

      CollectionAssert.AreEqual(expected, actual, "OK!");
    }

    [TestMethod]
    public void GetLineOfSightTestC()
    {
      List<string> lines = new List<string>();
      lines.Add("#.........");
      lines.Add("...A......");
      lines.Add("...B..a...");
      lines.Add(".ED#G....a");
      lines.Add("..F.c.b...");
      lines.Add(".....c....");
      lines.Add("..efd.c.gb");
      lines.Add(".......c..");
      lines.Add("....f...c.");
      lines.Add("...e..d..c");

      var expected = new List<Point>() {  new Point(0,0),
                                          new Point(3,3),
                                          new Point(4,4),
                                          new Point(5,5),
                                          new Point(6,6),
                                          new Point(7,7),
                                          new Point(8,8),
                                          new Point(9,6)
                                          };

      var grid = new Grid(lines);
      Asteroid a = grid.Asteroids[0];
      Asteroid b = grid.Asteroids[1];

      var actual = grid.GetLineOfSight(a, b);

      CollectionAssert.AreEqual(expected, actual, "OK!");
    }
  }
}
