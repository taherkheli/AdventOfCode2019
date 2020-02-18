using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingRobot
{
  public class Panel
  {
    private bool _isPainted;
    private Directions _direction;
    private Colors _color;
    private Position _position;

    public Panel(Position position)
    {
      _isPainted = false;
      _direction = Directions.Up;
      _color = Colors.Black;
      _position = position;
    }

    public bool IsPainted => _isPainted;
    public Directions Direction => _direction;
    public Colors Color => _color;
    public Position Position => _position;
  }
}
