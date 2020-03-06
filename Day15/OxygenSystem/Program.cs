using System;
using System.Collections.Generic;
using System.IO;

namespace OxygenSystem
{
	class Program
	{
    static void Main()
    {
      string path = "input.txt";
      Grid grid = new Grid(LoadInput(path));

      List<Tile> Path = new List<Tile>();
      Queue<Tile> toBeExplored = new Queue<Tile>();
      bool found = false;

      while (!found)
      {
        var options = grid.Scan();

        switch (options.Count)
        {
          case 0:    //hit a dead end
            var tile = toBeExplored.Dequeue();
            var index = Path.FindIndex( t => ((t.X == tile.X) && (t.Y  == tile.Y)));
            grid.PreviousTile = Path[index - 1];
            grid.CurrentTile = Path[index];
            break;
          case 1:    //keep going
            Path.Add(options[0]);
            grid.PreviousTile = grid.CurrentTile;
            grid.CurrentTile = Path[Path.Count - 1];
            break;
          case 2:
            Path.Add(options[0]);
            toBeExplored.Enqueue(options[1]);
            grid.PreviousTile = grid.CurrentTile;
            grid.CurrentTile = Path[Path.Count - 1];
            break;
          case 3:
            Path.Add(options[0]);
            toBeExplored.Enqueue(options[1]);
            toBeExplored.Enqueue(options[2]);
            grid.PreviousTile = grid.CurrentTile;
            grid.CurrentTile = Path[Path.Count - 1];
            break;
          default:
            throw new Exception("Something went wrong!");
        }
                                   
        foreach (var option in options)
          if (option.StatusCode == StatusCode.Target)
          {
            found = true;
            Path.Add(option);
          }
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




//      Console.WriteLine("\nPart I: Panels painted at least once: {0}\n\n", robot.Grid.PaintedPanels);



  
        //int x = Convert.ToInt32((long)grid.Executor.OutputQueue.Dequeue());

        //switch (x)
        //{
        //  case 0:
        //    Console.WriteLine("Hit a wall : ({0},{1})", grid.CurrentTile.X, grid.CurrentTile.Y);
        //    break;
        //  case 1:
        //    Console.WriteLine("Keep going : ({0},{1})", grid.CurrentTile.X, grid.CurrentTile.Y);
        //    break;
        //  case 2:
        //    Console.WriteLine("Hit jackpot at: ({0},{1})", grid.CurrentTile.X, grid.CurrentTile.Y);
        //    break;
        //  default:
        //    break;
        //}

        //Console.Write("\nProvide next move >>>>>>       ");
        //var nextMove = Convert.ToInt64(Console.ReadLine());

        //grid.Executor.InputQueue.Enqueue(nextMove);
        //grid.Executor.ResumeExecution();