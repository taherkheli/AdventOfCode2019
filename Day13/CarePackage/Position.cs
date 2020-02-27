using System;
using System.Collections.Generic;
using System.Text;

namespace CarePackage
{
  public struct Position
  {
    public Position(int x, int y)
    {
      X = x;
      Y = y;
    }

    public int X;
    public int Y;
  }
}