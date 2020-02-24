using System;
using System.Collections.Generic;
using System.Linq;

namespace PaintingRobot
{
  public class Grid
  {
    //TODO: ideally, build the grid as you go instead of fixed dimensions beforehand
    //performnace tuning, data dependent
    private const int _rowOffset = 2100;
    private const int _colOffset = 2100;
    private readonly Position _origin;

    private readonly int _rows;
    private readonly int _columns;
    private readonly Panel[,] _panels;
    private Panel _currentPanel;

    public int Rows => _rows;
    public int Columns => _columns;
    public Panel CurrentPanel => _currentPanel;
    public int PaintedPanels { get => GetPaintedPanels(); }

    public Grid(int size)
    {
      if (size > _rowOffset)
        _rows = size - _rowOffset;

      if (size > _colOffset)
        _columns = size - _colOffset;

      _origin = new Position(2400 - _rowOffset, 2400 - _colOffset);
      _panels = new Panel[_rows, _columns];
      Initialize();
    }

    public void Initialize()
    {
      for (int r = 0; r < _rows; r++)
      {
        for (int c = 0; c < _columns; c++)
        {
          var panel = new Panel(new Position(r, c));
          _panels[r, c] = panel;
        }
      }

      _currentPanel = _panels[_origin.X, _origin.Y];
      _currentPanel.Direction = Directions.Up;
    }

    private int GetPaintedPanels()
    {
      int count = 0;

      for (int r = 0; r < _rows; r++)
      {
        for (int c = 0; c < _columns; c++)
        {
          if (_panels[r, c].PaintCount > 0)
            count++;
        }
      }

      return count;
    }

    internal void Draw()
   {
      var X = new List<int>() { };
      var Y = new List<int>() { };

      for (int r = 0; r < _rows; r++)
      {
        for (int c = 0; c < _columns; c++)
        {
          if (_panels[r, c].Color == Colors.White)
          {
            X.Add(r);
            Y.Add(c);
          }
        }
      }

      var r_min = X.Min() - 5;
      var r_max = X.Max() + 5;
      var c_min = Y.Min() - 5;
      var c_max = Y.Max() + 5;
                    
      for (int c = c_min; c < c_max; c++)
      {
        for (int r = r_min; r < r_max; r++)
        {
          if (_panels[r, c].Color == Colors.White)
            Console.Write('#');
          else
            Console.Write(' ');
        }

        Console.WriteLine();
      }
    }

    public void Move(Directions direction)
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

      _currentPanel = _panels[newPosition.X, newPosition.Y];
      _currentPanel.Direction = newDirection;

      //Console.WriteLine("Current Panel is now ->  X:{0}; Y:{1}; Direction:'{3}'", _currentPanel.Position.X, _currentPanel.Position.Y,_currentPanel.PaintCount, _currentPanel.Direction);
    }
  }
}
