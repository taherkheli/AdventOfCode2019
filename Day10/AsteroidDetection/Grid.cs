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
      var angles = new List<double> {};
      Point current = new Point();

      for (int i = 0; (center.X + i) < _columns; i++)
      {
        //Console.WriteLine("\ncenter.X + i =  {0}", center.X + i);

        for (int j = 0; j < center.Y; j++)
        {
          current = new Point(center.X + i, j);
          allPoints.Add(current);
          angles.Add(current.CalculateAngleQ1(center));

          //Console.WriteLine("point is ({0},{1})", center.X + i, j);
        }
      }
           
      angles = Helpers.RemoveDuplicates(angles);      
      angles.Sort();

      foreach (var angle in angles)
      {
        var point = allPoints.Find(p => (p.CalculateAngleQ1(center) == angle));
        result.Add(point);
      }

      //foreach (var point in result)
      //  Console.WriteLine("point is ({0},{1}) and theta is {2} radians", point.X, point.Y, point.CalculateAngleQ1(center));

      return result;
    }
    
    public List<Point> GetPointsListQ2(Point center)
    {
      List<Point> result = new List<Point>();
      List<Point> allPoints = new List<Point>();
      var angles = new List<double> { };
      Point current = new Point();

      for (int i = 0; (center.Y + i) < _rows; i++)
      {
        for (int j = (_columns - 1); j > center.X; j--)
        {
          current = new Point(j, center.Y + i);
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
      Point current = new Point();

      for (int i = 0; (center.X - i) > -1; i++)
      {
        for (int j = center.Y + 1; j < _rows; j++)
        {
          current = new Point(center.X - i, j);
          allPoints.Add(current);
          angles.Add(current.CalculateAngleQ3(center));
        }
      }
      
      angles = Helpers.RemoveDuplicates(angles);
      angles.Sort();

      foreach (var angle in angles)
      {
        var point = allPoints.Find(p => (p.CalculateAngleQ3(center) == angle));
        result.Add(point);
      }

      return result;
    }
    
    public List<Point> GetPointsListQ4(Point center)
    {
      List<Point> result = new List<Point>();
      List<Point> allPoints = new List<Point>();
      var angles = new List<double> { };
      Point current = new Point();

      for (int i = 0; (center.Y - i) > -1; i++)
      {
        for (int j = 0; j < center.X; j++)
        {
          current = new Point(j, center.Y - i);
          allPoints.Add(current);
          angles.Add(current.CalculateAngleQ4(center));
        }
      }

      angles = Helpers.RemoveDuplicates(angles);
      angles.Sort();

      foreach (var angle in angles)
      {
        var point = allPoints.Find(p => (p.CalculateAngleQ4(center) == angle));
        result.Add(point);
      }

      return result;
    }

    public List<Point> GetLineOfSightQ1(Point center, Point p)
    {
      var result = new List<Point> { };
      var point = new Point();
      var desiredAngle = p.CalculateAngleQ1(center);

      for (int i = 0; (center.X + i) < _columns; i++)
      {
        for (int j = 0; j < center.Y; j++)
        {
          point = new Point(center.X + i, j);
          if (point.CalculateAngleQ1(center) == desiredAngle)
            result.Add(point);
        }
      }

      result = result.OrderByDescending(p => p.Y).ToList();   //closest from the center point first
      return result;
    }

    public List<Point> GetLineOfSightQ2(Point center, Point p)
    {
      var result = new List<Point> { };
      var point = new Point();
      var desiredAngle = p.CalculateAngleQ2(center);

      for (int i = 0; (center.Y + i) < _rows; i++)
      {
        for (int j = (_columns - 1); j > center.X; j--)
        {
          point = new Point(j, center.Y + i);
          if (point.CalculateAngleQ2(center) == desiredAngle)
            result.Add(point);
        }
      }

      result = result.OrderBy(p => p.X).ToList();   //closest from the center point first
      return result;
    }

    public List<Point> GetLineOfSightQ3(Point center, Point p)
    {
      var result = new List<Point> { };
      var point = new Point();
      var desiredAngle = p.CalculateAngleQ3(center);

      for (int i = 0; (center.X - i) > -1; i++)
      {
        for (int j = center.Y + 1; j < _rows; j++)
        {
          point = new Point(center.X - i, j);
          if (point.CalculateAngleQ3(center) == desiredAngle)
            result.Add(point);
        }
      }

      result = result.OrderBy(p => p.Y).ToList();   //closest from the center point first
      return result;
    }

    public List<Point> GetLineOfSightQ4(Point center, Point p)
    {
      var result = new List<Point> { };
      var point = new Point();
      var desiredAngle = p.CalculateAngleQ4(center);

      for (int i = 0; (center.Y - i) > -1; i++)
      {
        for (int j = 0; j < center.X; j++)
        {
          point = new Point(j, center.Y - i);
          if (point.CalculateAngleQ4(center) == desiredAngle)
            result.Add(point);
        }
      }     

      result = result.OrderByDescending(p => p.X).ToList();   //closest from the center point first
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