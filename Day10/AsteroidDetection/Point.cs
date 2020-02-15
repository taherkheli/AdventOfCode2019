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

    public double CalculateAngle(Point center)
    {
      double a, b;
      double angle = double.NaN;

      if ( ((this.X >= center.X) && (this.Y < center.Y)) || ((this.X <= center.X) && (this.Y > center.Y)))   //Q1|Q3
      {
        a = Math.Abs(center.X - this.X);   //opposite
        b = Math.Abs(center.Y - this.Y);   //adjacent
        angle = Math.Tanh(a / b);
      }
      else   //Q2|Q4
      {
        a = Math.Abs(center.Y - this.Y);   //opposite
        b = Math.Abs(center.X - this.X);   //adjacent
        angle = Math.Tanh(a / b);
      }

      return angle;
    }
  }
}
