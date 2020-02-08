using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroidDetection
{
  public class Grid
  {
    private readonly int _rows;
    private readonly int _columns;
    private readonly int _size;
    private List<Asteroid> _asteroids;

    public List<Asteroid> Asteroids { get => _asteroids; }

    public Grid(List<string> lines)
    {
      if (lines.Count > 0)
      {
        _rows = lines.Count;
        _columns = lines[0].Length;
        _size = _rows * _columns;
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

    private List<Asteroid> GetAsteroidsVisibleTo(Asteroid a)
    {      
      List<Asteroid> ToAnalyze = new List<Asteroid>(_asteroids);
      ToAnalyze.Remove(a);

      var Visible = new List<Asteroid>();
      var Hidden = new List<Asteroid>();

      while (ToAnalyze.Count > 0)
      {
        var b = ToAnalyze[0];


        //pluck 1 (and remove from ToAnalyze)
        //
        //determine LOS   between a and this one
        //
        //process results i.e. dump the identified ones in the right bucket 
        //
        //repeat   (remove here if not done already)


        continue;
      }
      
      
      return Visible;
    }


    public List<Point> GetLineOfSight(Asteroid a, Asteroid b)
    {
      //TODO: improve it so Line of sight actually is all possible points on the straight line

      var result = new List<Point>();
      var forward = new List<Point>();
      var backward = new List<Point>();

      var deltaX = Math.Abs(a.Position.X - b.Position.X);
      var deltaY = Math.Abs(a.Position.Y - b.Position.Y);

      if (deltaX >= deltaY)
      {
        //forwrads
        forward.Add(a.Position); //operand must be on line of sight

        Point next = new Point((a.Position.X + deltaX) , (a.Position.Y + deltaY));

        while (next.X < _columns)  //inside grid
        {
          forward.Add(next);
          next = new Point((next.X + deltaX), (next.Y + deltaY));
        }

        //backwards
        next = new Point((a.Position.X - deltaX), (a.Position.Y - deltaY));

        while (next.X > -1)  
        {
          backward.Add(next);
          next = new Point((next.X - deltaX), (next.Y - deltaY));
        }
      }
      else  //deltaY > deltaX
      {
        forward.Add(a.Position); 
        Point next = new Point((a.Position.X + deltaX), (a.Position.Y + deltaY));

        while (next.Y < _rows)  
        {
          forward.Add(next);
          next = new Point((next.X + deltaX), (next.Y + deltaY));
        }

        next = new Point((a.Position.X - deltaX), (a.Position.Y - deltaY));

        while (next.Y > -1)  
        {
          backward.Add(next);
          next = new Point((next.X - deltaX), (next.Y - deltaY));
        }
      }

      //combine in right order
      backward.Reverse();
      result.AddRange(backward);
      result.AddRange(forward);

      return result;
    }
  }
}