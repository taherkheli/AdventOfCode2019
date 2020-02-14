using System;
using System.Collections.Generic;
using System.Linq;

namespace AsteroidDetection
{
  public class Grid
  {
    private readonly int _rows;
    private readonly int _columns;
    private readonly List<Asteroid> _asteroids;

    public List<Asteroid> Asteroids { get => _asteroids; }

    public int Rows => _rows;

    public int Columns => _columns;

    public Grid(List<string> lines)
    {
      if (lines.Count > 0)
      {
        _rows = lines.Count;
        _columns = lines[0].Length;
        _asteroids = GetAsteroids(lines);
      }
    }

    public List<Asteroid> GetAsteroidsVisibleTo(Asteroid a)
    {
      List<Asteroid> ToAnalyze = new List<Asteroid>(_asteroids);
      var Visible = new List<Asteroid>();
      var Hidden = new List<Asteroid>();

      while (ToAnalyze.Count > 0)
      {
        ToAnalyze.Remove(a);
        var b = ToAnalyze[0];

        var line = GetLine(a, b);
        int index_a = line.FindIndex(p => (p.X == a.Position.X) && (p.Y == a.Position.Y));
        bool firstFound = false;

        //forwards
        for (int i = index_a + 1; i < line.Count; i++)
        {
          var p = line[i];
          var asteroid = ToAnalyze.Find(a => (a.Position.X == p.X) && (a.Position.Y == p.Y));

          if (asteroid != null)
          {
            if (firstFound == false)
            {
              Visible.Add(asteroid);
              ToAnalyze.Remove(asteroid);
              firstFound = true;
            }
            else
            {
              Hidden.Add(asteroid);
              ToAnalyze.Remove(asteroid);
            }
          }
        }

        //backwards
        firstFound = false;
        for (int i = index_a - 1; i > -1; i--)
        {
          var p = line[i];
          var asteroid = ToAnalyze.Find(a => (a.Position.X == p.X) && (a.Position.Y == p.Y));

          if (asteroid != null)
          {
            if (firstFound == false)
            {
              Visible.Add(asteroid);
              ToAnalyze.Remove(asteroid);
              firstFound = true;
            }
            else
            {
              Hidden.Add(asteroid);
              ToAnalyze.Remove(asteroid);
            }
          }
        }
      }

      return Visible;
    }

    public List<Point> GetLine(Asteroid a, Asteroid b)
    {
      var result = new List<Point>
      {
        a.Position,
        b.Position
      };

      for (int x = 0; x < _columns; x++)
      {
        for (int y = 0; y < _rows; y++)
        {
          double slope_a = (double)(a.Position.Y - y) / (a.Position.X - x);
          double slope_b = (double)(b.Position.Y - y) / (b.Position.X - x);

          if (((a.Position.X - x) == 0) && ((b.Position.X - x) == 0))
          {
            var p = new Point(x, y);
            if (result.Contains(p) == false)
              result.Add(p);
          }
          else if (slope_a == slope_b)
          {
            var p = new Point(x, y);
            if (result.Contains(p) == false)
              result.Add(p);
          }
        }
      }

      result = result.OrderBy(p => p.X).ToList();
      result = result.OrderBy(p => p.Y).ToList();

      return result;
    }

    public List<Point> GetPointsListQ1(Point center)
    {
      List<Point> result = new List<Point>();
      List<Point> allPoints = new List<Point>();
      var angles = new List<double> { };

      //add starting point and its angle
      Point current = new Point(center.X, 0);
      allPoints.Add(current);
      angles.Add(current.CalculateAngleQ1(center));

      for (int i = 1; center.X + i < _columns; i++)
      {
        var X = center.X + i;
        for (int j = 0; j < center.Y; j++)
        {
          current = new Point(X, j);
          allPoints.Add(current);
          angles.Add(current.CalculateAngleQ1(center));
        }
      }

      angles = Helpers.RemoveDuplicates(angles);
      angles.Sort();

      foreach (var angle in angles)
      {
        var point = allPoints.Find(p => (p.CalculateAngleQ1(center) == angle));
        result.Add(point);
      }

      return result;
    }

    public List<Point> GetPointsListQ2(Point center)
    {
      List<Point> result = new List<Point>();
      List<Point> allPoints = new List<Point>();
      var angles = new List<double> { };

      //add starting point and its angle
      Point current = new Point(_columns - 1 , center.Y);
      allPoints.Add(current);
      angles.Add(current.CalculateAngleQ2(center));

      for (int i = 1; center.Y + i < _rows; i++)
      {
        var Y = center.Y + i;
        for (int j = _columns-1; j > center.X; j--)
        {
          current = new Point(j, Y);
          allPoints.Add(current);
          angles.Add(current.CalculateAngleQ2(center));
        }
      }

      angles = Helpers.RemoveDuplicates(angles);
      angles.Sort();

      foreach (var angle in angles)
      {
        var point = allPoints.Find(p => (p.CalculateAngleQ2(center) == angle));
        result.Add(point);
      }

      return result;
    }

    public List<Point> GetPointsListQ3(Point center)
    {
      List<Point> result = new List<Point>();
      List<Point> allPoints = new List<Point>();
      var angles = new List<double> { };

      Point current = new Point(center.X, _rows - 1);
      allPoints.Add(current);
      angles.Add(current.CalculateAngleQ3(center, _rows));

      for (int i = 1; center.X - i > -1; i++)
      {
        var X = center.X - i;
        for (int j = _rows - 1; j > center.Y; j--)
        {
          current = new Point(X, j);
          allPoints.Add(current);
          angles.Add(current.CalculateAngleQ3(center, _rows));
        }
      }

      angles = Helpers.RemoveDuplicates(angles);
      angles.Sort();

      foreach (var angle in angles)
      {
        var point = allPoints.Find(p => (p.CalculateAngleQ3(center, _rows) == angle));
        result.Add(point);
      }

      return result;
    }


    public List<Point> GetPointsListQ4(Point center)
    {
      List<Point> result = new List<Point>();
      List<Point> allPoints = new List<Point>();
      var angles = new List<double> { };

      Point current = new Point(0, center.Y);
      allPoints.Add(current);
      angles.Add(current.CalculateAngleQ4(center));

      for (int i = 1; center.Y - i > -1; i++)
      {
        var Y = center.Y - i;
        for (int j = 0; j < center.X; j++)
        {
          current = new Point(j, Y);
          allPoints.Add(current);
          angles.Add(current.CalculateAngleQ4(center));
        }
      }

      angles = Helpers.RemoveDuplicates(angles);
      angles.Sort();

      foreach (var angle in angles)
      {
        var point = allPoints.Find(p => (p.CalculateAngleQ2(center) == angle));
        result.Add(point);
      }

      return result;
    }


    private List<Asteroid> GetAsteroids(List<string> lines)
    {
      var result = new List<Asteroid>();

      for (int r = 0; r < _rows; r++)
      {
        var charArray = lines[r].ToCharArray();

        for (int c = 0; c < _columns; c++)
        {
          if (charArray[c] == '#')
            result.Add(new Asteroid(new Point(c, r)));
        }
      }

      return result;
    }
  }
}