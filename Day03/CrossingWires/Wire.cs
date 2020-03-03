using System;
using System.Collections.Generic;
using System.Text;

namespace CrossingWires
{
  public class Wire
  {
    private Point _head;

    private readonly List<Point> _points;

    public Wire()
    {
      _head = new Point { X = 0, Y = 0 };
      _points = new List<Point>
      {
        _head
      };
    }

    public Point Head { get => _head; }
    public List<Point> Points { get => _points; }

    public List<Point> FindPointsInBand(int min, int max)
    {
      var result = new List<Point>();

      foreach (var p in _points)
      {
        if ((Math.Abs(p.X) > min) && (Math.Abs(p.X) <= max))             //x is in range
        {
          if (Math.Abs(p.Y) <= max)           //y is also in range     
            result.Add(p);
        }
      }

      return result;
    }

    public void Move(List<Movement> movements)
    {
      foreach (var movement in movements)
        TrackWire(movement);
    }

    //return -1 if item not found
    public int StepsTakenToReach(Point p)
    {
      //int index = myList.FindIndex(a => a.Prop == oProp);

      int index = _points.FindIndex(t =>((t.X == p.X) && (t.Y == p.Y)));

      return index;
    }

    public static List<Point> FindAllCrossings(List<Point> w1, List<Point> w2)
    {
      var result = new List<Point>();

      foreach (var p in w1)
      {
        if (w2.Contains(p))
          result.Add(p);
      }

      return result;
    }

    private void TrackWire(Movement movement)
    {
      switch (movement.Direction)
      {
        case Direction.Up:
          for (int i = 1; i <= movement.StepSize; i++)
          {
            _head.Y++;
            _points.Add(_head);
          }
          break;

        case Direction.Down:
          for (int i = 1; i <= movement.StepSize; i++)
          {
            _head.Y--;
            _points.Add(_head);
          }
          break;

        case Direction.Left:
          for (int i = 1; i <= movement.StepSize; i++)
          {
            _head.X--;
            _points.Add(_head);
          }
          break;

        case Direction.Right:
          for (int i = 1; i <= movement.StepSize; i++)
          {
            _head.X++;
            _points.Add(_head);
          }
          break;

        default:
          break;
      }

    }
  }
}  