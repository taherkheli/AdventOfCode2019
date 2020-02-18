using System;

namespace PaintingRobot
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

    public Quadrants GetQuadrant(Position center)
    {
      Quadrants result = Quadrants.Unknown;

      if ((this.X >= center.X) && (this.Y < center.Y))
        result = Quadrants.One;

      if ((this.X > center.X) && (this.Y >= center.Y))
        result = Quadrants.Two;

      if ((this.X <= center.X) && (this.Y > center.Y))
        result = Quadrants.Three;

      if ((this.X < center.X) && (this.Y <= center.Y))
        result = Quadrants.Four;

      return result;
    }

    public double CalculateAngle(Position center)
    {
      double a, b;
      double angle = double.NaN;

      if ((this.GetQuadrant(center) == Quadrants.One) || (this.GetQuadrant(center) == Quadrants.Three))
      {
        a = Math.Abs(center.X - this.X);   //opposite
        b = Math.Abs(center.Y - this.Y);   //adjacent
        angle = Math.Tanh(a / b);
      }
      else if ((this.GetQuadrant(center) == Quadrants.Two) || (this.GetQuadrant(center) == Quadrants.Four))
      {
        a = Math.Abs(center.Y - this.Y);   //opposite
        b = Math.Abs(center.X - this.X);   //adjacent
        angle = Math.Tanh(a / b);
      }

      return angle;
    }
  }
}
