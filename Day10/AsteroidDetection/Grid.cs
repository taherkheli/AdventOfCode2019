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

    public Grid(List<string> lines)
    {
      if (lines.Count > 0)
      {
        _rows = lines.Count;
        _columns = lines[0].Length;
        _asteroids = GetAsteroids(lines);
      }
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

        firstFound = false;
        for (int i = index_a - 1; i > -1 ; i--)
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
            if (result.Contains(p) ==  false)
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
  }
}