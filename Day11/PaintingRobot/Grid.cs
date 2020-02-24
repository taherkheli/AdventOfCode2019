using System.Collections.Generic;

namespace PaintingRobot
{
  public class Grid
  {
    private readonly int _rows;
    private readonly int _columns;
    private readonly List<Panel> _panels;
    private Panel _currentPanel;

    public int Rows => _rows;
    public int Columns => _columns;
    public Panel CurrentPanel => _currentPanel;

    public int PaintedPanels { get => GetPaintedPanels(); }

    private int GetPaintedPanels()
    {
      int count = 0;

      foreach (var p in _panels)
      {
        if (p.PaintCount > 0)
          count++;
      }

      return count;
    }

    public Grid(int size)
    {
      _rows = size;
      _columns = size;
      _panels = new List<Panel>() { };
      Initialize();
    }

    private void Initialize()
    {
      for (int r = 0; r < _rows; r++)
      {
        for (int c = 0; c < _columns; c++)
        {
          var panel = new Panel(new Position(r, c));
          _panels.Add(panel);
        }
      }

      //var position = new Position(_rows / 2, _columns / 2);
      var position = new Position( 2400, 2400);

      _currentPanel = _panels.Find(p => ((p.Position.X == position.X) && (p.Position.Y == position.Y)));
      _currentPanel.Direction = Directions.Up;
    }

    internal void Move(Directions direction)
    {
      Directions newDirection = Directions.Unknown;
      Position newPosition = new Position(-1, -1);

      switch (_currentPanel.Direction)
      {
        case Directions.Up:
          if (direction == Directions.Left)
          {
            newDirection = Directions.Left;
            newPosition = new Position(_currentPanel.Position.X - 1, _currentPanel.Position.Y);
          }
          else
          {
            newDirection = Directions.Right;
            newPosition = new Position(_currentPanel.Position.X + 1, _currentPanel.Position.Y);
          }
          break;

        case Directions.Down:
          if (direction == Directions.Left)
          {
            newDirection = Directions.Right;
            newPosition = new Position(_currentPanel.Position.X + 1, _currentPanel.Position.Y);
          }
          else
          {
            newDirection = Directions.Left;
            newPosition = new Position(_currentPanel.Position.X - 1, _currentPanel.Position.Y);
          }
          break;

        case Directions.Left:
          if (direction == Directions.Left)
          {
            newDirection = Directions.Down;
            newPosition = new Position(_currentPanel.Position.X, _currentPanel.Position.Y + 1);
          }
          else
          {
            newDirection = Directions.Up;
            newPosition = new Position(_currentPanel.Position.X, _currentPanel.Position.Y - 1);
          }
          break;

        case Directions.Right:
          if (direction == Directions.Left)
          {
            newDirection = Directions.Up;
            newPosition = new Position(_currentPanel.Position.X, _currentPanel.Position.Y - 1);
          }
          else
          {
            newDirection = Directions.Down;
            newPosition = new Position(_currentPanel.Position.X, _currentPanel.Position.Y + 1);
          }
          break;
      }

      _currentPanel = _panels.Find(p => ((p.Position.X == newPosition.X) && (p.Position.Y == newPosition.Y)));
      _currentPanel.Direction = newDirection;

      //Console.WriteLine("Current Panel is now ->  X:{0}; Y:{1}; Direction:'{3}'", _currentPanel.Position.X, _currentPanel.Position.Y,_currentPanel.PaintCount, _currentPanel.Direction);
    }
  }
}
