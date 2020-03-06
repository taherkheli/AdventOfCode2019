using IntCode;
using System;
using System.IO;

namespace CarePackage
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      Grid grid = new Grid();
      Executor executor = new Executor(LoadInput(path));
      executor.Execute();

      while (executor.OutputQueue.Count != 0)
      {
        var x = Convert.ToInt32((long)executor.OutputQueue.Dequeue());
        var y = Convert.ToInt32((long)executor.OutputQueue.Dequeue());
        var tileId = (TileId)Convert.ToInt32((long)executor.OutputQueue.Dequeue());
        grid.Tiles.Add(new Tile(x, y, tileId));
      }

      int count = 0;
      foreach (var tile in grid.Tiles)
        if (tile.TileId == TileId.Block)
          count++;

      Console.WriteLine("\nPart I: Block tiles on the screen: {0}", count);

      /*********************  Part II  *******************************/

      int score = -1;
      int lastX = -1;
      int lastY = -1;
      executor.Initialize();
      executor.Memory[0] = (long)2;
      executor.Execute();

      while (executor.AwaitingInput)
      {
        score = HandleOutput(grid, executor, score);
        var ball = grid.Tiles.Find(t => t.TileId == TileId.Ball);
        var paddle = grid.Tiles.Find(t => t.TileId == TileId.HorizontalPaddle);
        var blocks = grid.Tiles.FindAll(t => t.TileId == TileId.Block);
        var joystick = Joystick.Unknown;
        var dir = GetDirection(lastX, lastY, ball);
        switch (dir)
        {
          case Direction.Q1:
            if (ball.X <= paddle.X)
              joystick = Joystick.Neutral;
            else
              joystick = Joystick.Right;
            break;
          case Direction.Q2:
            if (ball.X <= paddle.X)
              joystick = Joystick.Neutral;
            else
              joystick = Joystick.Right;
            break;
          case Direction.Q3:
            if (ball.X >= paddle.X)
              joystick = Joystick.Neutral;
            else
              joystick = Joystick.Left;
            break;
          case Direction.Q4:
            if (ball.X >= paddle.X)
              joystick = Joystick.Neutral;
            else
              joystick = Joystick.Left;
            break;
        }
        executor.InputQueue.Enqueue(Convert.ToInt64(joystick));
        lastX = ball.X;
        lastY = ball.Y;
        executor.ResumeExecution();
      }

      score = HandleOutput(grid, executor, score);
      Console.WriteLine("\nPart II: Score after breaking all blocks: {0}\n\n\n", score);
    }

    private static int HandleOutput(Grid grid, Executor executor, int highScore)
    {
      while (executor.OutputQueue.Count != 0)
      {
        var x = Convert.ToInt32((long)executor.OutputQueue.Dequeue());
        var y = Convert.ToInt32((long)executor.OutputQueue.Dequeue());
        var tileId = TileId.Unknown;
        int index = -1;

        if ((x == -1) && (y == 0))
          highScore = Convert.ToInt32((long)executor.OutputQueue.Dequeue());
        else
          tileId = (TileId)Convert.ToInt32((long)executor.OutputQueue.Dequeue());

        index = grid.Tiles.FindIndex(t => (t.X == x) && (t.Y == y));
        if (index == -1)
          grid.Tiles.Add(new Tile(x, y, tileId));
        else
          grid.Tiles[index].TileId = tileId;
      }

      return highScore;
    }

    private static Direction GetDirection(int lastX, int lastY, Tile ball)
    {
      var dir = Direction.Unknown;

      if (ball.X > lastX) //Q1 or Q2
      {
        if (ball.Y > lastY)
          dir = Direction.Q2;
        else
          dir = Direction.Q1;
      }
      else //Q3 or Q 4
      {
        if (ball.Y < lastY)
          dir = Direction.Q4;
        else
          dir = Direction.Q3;
      }

      return dir;
    }

    private static long[] LoadInput(string path)
    {
      StreamReader file = new StreamReader(path);
      string[] strings = file.ReadToEnd().Split(',');
      long[] result = new long[strings.Length];

      for (int i = 0; i < result.Length; i++)
        result[i] = long.Parse(strings[i]);

      return result;
    }
  }
}