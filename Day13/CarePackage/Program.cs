using IntCode;
using System;
using System.Collections.Generic;
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
      //executor.Execute();

      //while(executor.OutputQueue.Count != 0)
      //{
      //  var x = Convert.ToInt32((long)executor.OutputQueue.Dequeue());
      //  var y = Convert.ToInt32((long)executor.OutputQueue.Dequeue());
      //  var tileId = (TileId)Convert.ToInt32((long)executor.OutputQueue.Dequeue());
      //  grid.Tiles.Add(new Tile(x, y, tileId));
      //}

      //int count = 0;

      //foreach (var tile in grid.Tiles)
      //  if (tile.TileId == TileId.Block)
      //    count++;

      //Console.WriteLine("\nPart I: Block tiles on the screen: {0}\n\n", count);


      /****************  PartII   *********************/

      executor.Initialize();
      executor.Memory[0] = (long)2;

      int highScore = 0;
      int yPrev = -1;
      int xPrev = -1;


      TileId tileId = TileId.Unknown;

      executor.Execute();

      while (executor.AwaitingInput)
      {
        //handle output
        while (executor.OutputQueue.Count > 0)
        {
          var x = Convert.ToInt32((long)executor.OutputQueue.Dequeue());
          var y = Convert.ToInt32((long)executor.OutputQueue.Dequeue());
          var z = Convert.ToInt32((long)executor.OutputQueue.Dequeue());

          if ((x == -1) && (y == 0))
            highScore = z;
          else
          {
            tileId = (TileId)z;
            var index = grid.Tiles.FindIndex(t => ((t.X == x) && (t.Y == y)));

            if (index == -1) //not found
              grid.Tiles.Add(new Tile(x, y, tileId));
            else  //update
              grid.Tiles[index].TileId = tileId;
          }
        }

        //handle input
        var ball = grid.Tiles.Find(t => t.TileId == TileId.Ball);
        var paddle = grid.Tiles.Find(t => t.TileId == TileId.HorizontalPaddle);

        int count = 0;
        foreach (var tile in grid.Tiles)
          if (tile.TileId == TileId.Block)
            count++;

        Console.WriteLine("\nBall ({0},{1})", ball.X, ball.Y);
        Console.WriteLine("Paddle ({0},{1})", paddle.X, paddle.Y);
        Console.WriteLine("Remaining blocks ({0})", count);


        if (ball.Y > yPrev)  //falling
        {
          List<Position> trajectory = new List<Position>();
          int num = paddle.Y - ball.Y;
          for (int i = 0; i < num; i++)
          {
            if (ball.X > xPrev)
              trajectory.Add(new Position(ball.X + (i + 1), ball.Y + (i + 1)));
            else
              trajectory.Add(new Position(ball.X - (i + 1), ball.Y + (i + 1)));
          }

          bool losBlocked = false;
          foreach (var position in trajectory)
          {
            var tile = grid.Tiles.Find(t => ((t.X == position.X) && (t.Y == position.Y)));

            if ((tile != null) && (tile.TileId == TileId.Wall))
            {
              losBlocked = true;
              break;
            }
          }

          if (losBlocked == false)
          {
            // do something
            //place paddel in one go on the trajectorydsafqwae


            int diff = paddle.X - trajectory[trajectory.Count - 1].X;

            if (diff == 0)
            {
              executor.InputQueue.Enqueue((long)0);
            }

            else if (diff > 0)
            {
              //for (int i = 0; i < diff; i++)
              //  executor.InputQueue.Enqueue((long)-1);   // move left as per diff          

              executor.InputQueue.Enqueue((long)-2);
            }

            else if ( diff < 0)
            {
              diff = Math.Abs(diff);
              for (int i = 0; i < diff; i++)
                executor.InputQueue.Enqueue((long)1);   // move right as per diff
            }
          }
        }
        else
          executor.InputQueue.Enqueue((long)0);

        yPrev = ball.Y;
        xPrev = ball.X;
        executor.ResumeExecution();

      }
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
