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

    public double CalculateAngleQ1(Point center)
    {
      double a = this.X - center.X;   //opposite
      double b = center.Y - this.Y;   //adjacent
      double angle = Math.Tanh(a / b);

      return angle;
    }

    public double CalculateAngleQ2(Point center)
    {
      double a = this.Y - center.Y;   //opposite
      double b = this.X - center.X;   //adjacent
      double angle = Math.Tanh(a / b);

      return angle;
    }

    public double CalculateAngleQ3(Point center, int rows)
    {
      double a = center.X - this.X;     //opposite
      double b = (rows-1) - center.Y;   //adjacent
      double angle = Math.Tanh(a / b);

      return angle;
    }

    public double CalculateAngleQ4(Point center)
    {
      double a = center.Y - this.Y;   //opposite
      double b = center.X - this.X;   //adjacent
      double angle = Math.Tanh(a / b);

      return angle;
    }
  }
}
