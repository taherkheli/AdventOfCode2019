using IntCode;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OxygenSystem
{
  public class Grid
  {
    private Tile _currentTile;
    private Tile _previousTile;

    private readonly List<Tile> _tiles;
    private readonly Executor _executor;

    public Tile CurrentTile { get => _currentTile; set => _currentTile = value; }
    public Tile PreviousTile { get => _previousTile; set => _previousTile = value; }
    public List<Tile> Tiles => _tiles;
    public Executor Executor => _executor;

    public Grid(long[] intCode)
    {
      _executor = new Executor(intCode);
      _executor.Initialize();
      _tiles = new List<Tile>();
      _currentTile = new Tile(0, 0, StatusCode.Path);
      _previousTile = _currentTile;
      //_tiles.Add(CurrentTile);
    }

    public void Draw()
    {
      //var X = new List<int>() { };
      //var Y = new List<int>() { };

      //for (int r = 0; r < _rows; r++)
      //{
      //  for (int c = 0; c < _columns; c++)
      //  {
      //    if (_panels[r, c].Color == Colors.White)
      //    {
      //      X.Add(r);
      //      Y.Add(c);
      //    }
      //  }
      //}

      //var r_min = X.Min() - 5;
      //var r_max = X.Max() + 5;
      //var c_min = Y.Min() - 5;
      //var c_max = Y.Max() + 5;

      //for (int c = c_min; c < c_max; c++)
      //{
      //  for (int r = r_min; r < r_max; r++)
      //  {
      //    if (_panels[r, c].Color == Colors.White)
      //      Console.Write('#');
      //    else
      //      Console.Write(' ');
      //  }

      //  Console.WriteLine();
      //}
    }

		public List<Tile> Scan()
		{
			List<Tile> options = new List<Tile>();
			StatusCode status = StatusCode.Unknown;

			//north
			_executor.InputQueue.Enqueue(Convert.ToInt64(Move.North));
			if (_executor.AwaitingInput)
				_executor.ResumeExecution();
			else
				_executor.Execute();
			status = (StatusCode)(Convert.ToInt32((long)_executor.OutputQueue.Dequeue()));
			if (status != StatusCode.Wall)
				options.Add(new Tile(_currentTile.X, _currentTile.Y + 1, status));

			//east
			_executor.InputQueue.Enqueue(Convert.ToInt64(Move.East));
			_executor.ResumeExecution();
			status = (StatusCode)(Convert.ToInt32((long)_executor.OutputQueue.Dequeue()));
			if (status != StatusCode.Wall)
				options.Add(new Tile(_currentTile.X + 1, _currentTile.Y, status));

			//south
			_executor.InputQueue.Enqueue(Convert.ToInt64(Move.South));
			_executor.ResumeExecution();
			status = (StatusCode)(Convert.ToInt32((long)_executor.OutputQueue.Dequeue()));
			if (status != StatusCode.Wall)
				options.Add(new Tile(_currentTile.X, _currentTile.Y - 1, status));

      //west
      _executor.InputQueue.Enqueue(Convert.ToInt64(Move.West));
      _executor.ResumeExecution();
      status = (StatusCode)(Convert.ToInt32((long)_executor.OutputQueue.Dequeue()));
      if (status != StatusCode.Wall)
        options.Add(new Tile(_currentTile.X - 1, _currentTile.Y, status));



      var t = options.Find(t => ((t.X == _previousTile.X) && (t.Y == _previousTile.Y) && (t.StatusCode == _previousTile.StatusCode)));

      if (t != null)
        options.Remove(t);

      //if (options.Contains(_previousTile))
      //  options.Remove(_previousTile);


      return options;
		}
	}
}
