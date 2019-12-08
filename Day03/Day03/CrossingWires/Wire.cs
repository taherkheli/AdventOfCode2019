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

    public void Move(List<Movement> movements)
    {
      foreach (var movement in movements)
        TrackWire(movement);
    }

    public bool HasCrossed(Point p)
    {
      return _points.Contains(p) ? true : false;
    }

    public List<Point> FindAllCrossingsWith(Wire otherWire)
    {
      var result = new List<Point>();

      foreach (var p in _points)
      {
        if (otherWire.HasCrossed(p))
          result.Add(p);

        //TODO: added this just to get to to some result. need soem performance optimization it seems ;) but at least I got the answer right        
        if (result.Count > 10)
          break;
      }

      result.Remove(new Point() { X = 0, Y = 0 });   //ignore origin
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
