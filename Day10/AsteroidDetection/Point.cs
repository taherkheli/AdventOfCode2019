using System;

namespace AsteroidDetection
{
  public struct Point
  {
    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }
       
    public int X;
    public int Y;

    public Quadrant GetQuadrant(Point center)
    {
      Quadrant result = Quadrant.Unknown;

      if ((this.X >= center.X) && (this.Y < center.Y))
        result = Quadrant.One;

      if ((this.X > center.X) && (this.Y >= center.Y))
        result = Quadrant.Two;

      if ((this.X <= center.X) && (this.Y > center.Y))
        result = Quadrant.Three;

      if ((this.X < center.X) && (this.Y <= center.Y))
        result = Quadrant.Four;

      return result;
    }

    public double CalculateAngle(Point center)
    {
      double a, b;
      double angle = double.NaN;

      if ((this.GetQuadrant(center) == Quadrant.One) || (this.GetQuadrant(center) == Quadrant.Three))
      {
        a = Math.Abs(center.X - this.X);   //opposite
        b = Math.Abs(center.Y - this.Y);   //adjacent
        angle = Math.Tanh(a / b);
      }
      else if ((this.GetQuadrant(center) == Quadrant.Two) || (this.GetQuadrant(center) == Quadrant.Four))
      {
        a = Math.Abs(center.Y - this.Y);   //opposite
        b = Math.Abs(center.X - this.X);   //adjacent
        angle = Math.Tanh(a / b);
      }

      return angle;
    }
  }
}
