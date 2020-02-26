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

      while(executor.OutputQueue.Count != 0)
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
     
      Console.WriteLine("\nPart I: Block tiles on the screen: {0}\n\n", count);
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
