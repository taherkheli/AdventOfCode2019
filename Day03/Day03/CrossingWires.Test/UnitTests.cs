using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CrossingWires.Test
{
  [TestClass]
  public class UnitTests
  {
    [TestMethod]
    public void TestMethod1()
    {
      Movement m1 = new Movement { Direction = Direction.Right, StepSize = 8 };
      Movement m2 = new Movement { Direction = Direction.Up, StepSize = 5 };
      Movement m3 = new Movement { Direction = Direction.Left, StepSize = 5 };
      Movement m4 = new Movement { Direction = Direction.Down, StepSize = 3 };

      List<Movement> movements = new List<Movement>
      {
        m1, m2, m3, m4
      };

      var Wire = new Wire();
      var expectedHead = new Point() { X = 3, Y = 2 };
      var expectedPoints = new List<Point>();
      expectedPoints.Add( new Point() { X = 0, Y = 0 } );
      expectedPoints.Add( new Point() { X = 1, Y = 0 } );
      expectedPoints.Add( new Point() { X = 2, Y = 0 } );
      expectedPoints.Add( new Point() { X = 3, Y = 0 } );
      expectedPoints.Add( new Point() { X = 4, Y = 0 } );
      expectedPoints.Add( new Point() { X = 5, Y = 0 } );
      expectedPoints.Add( new Point() { X = 6, Y = 0 } );
      expectedPoints.Add( new Point() { X = 7, Y = 0 } );
      expectedPoints.Add( new Point() { X = 8, Y = 0 } );
      expectedPoints.Add( new Point() { X = 8, Y = 1 } );
      expectedPoints.Add( new Point() { X = 8, Y = 2 } );
      expectedPoints.Add( new Point() { X = 8, Y = 3 } );
      expectedPoints.Add( new Point() { X = 8, Y = 4 } );
      expectedPoints.Add( new Point() { X = 8, Y = 5 } );
      expectedPoints.Add( new Point() { X = 7, Y = 5 } );
      expectedPoints.Add( new Point() { X = 6, Y = 5 } );
      expectedPoints.Add( new Point() { X = 5, Y = 5 } );
      expectedPoints.Add( new Point() { X = 4, Y = 5 } );
      expectedPoints.Add( new Point() { X = 3, Y = 5 } );
      expectedPoints.Add( new Point() { X = 3, Y = 4 } );
      expectedPoints.Add( new Point() { X = 3, Y = 3 } );
      expectedPoints.Add( new Point() { X = 3, Y = 2 } );

      Wire.Move(movements);
      var actualHead = Wire.Head;
      var actualPoints = Wire.Points;

      Assert.AreEqual<Point>(expectedHead, actualHead);
      CollectionAssert.AreEqual(expectedPoints, actualPoints);
    }
  }
}
