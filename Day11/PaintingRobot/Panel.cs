namespace PaintingRobot
{
  public class Panel
  {
    private int _paintCount;
    private Colors _color;
    private Directions _direction;
    private Position _position;

    public Panel(Position position)
    {
      _paintCount = 0;
      _color = Colors.Black;
      _position = position;
    }

    public int PaintCount { get => _paintCount; }
    public Colors Color { get => _color; set => _color = value; }
    public Directions Direction { get => _direction; set => _direction = value; }
    public Position Position { get => _position; set => _position = value; }

    internal void Paint(Colors color)
    {
      _color = color;
      _paintCount++;
    }
  }
}
