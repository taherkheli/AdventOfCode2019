using BIOS;
using System;
using System.Collections.Generic;
using System.IO;

namespace Driver
{
  class Program
  {
    static void Main()
    {
      int rows = 6;         //tall
      int columns = 25;     //wide

      string path = "input.txt";
      int[] input = LoadInput(path);
      var image = new Image(rows, columns, input);
      var layer = GetLayerWithLeastZeroes(image);
      Console.WriteLine("\nThe product of digit 1 occurences and digit 2 occurences on the layer with the fewest zeroes is : {0}", layer.Ones * layer.Twos);
    }

    private static int[] LoadInput(string path)
    {
      int[] result;

      using (StreamReader file = new StreamReader(path))
      {
        char[] charArray = file.ReadToEnd().ToCharArray();
        result = new int[charArray.Length];

        for (int i = 0; i < result.Length; i++)
          result[i] = int.Parse(charArray[i].ToString());
      }

      return result;
    }

    private static Layer GetLayerWithLeastZeroes(Image image)
    {
      Layer result = null;
      int temp = int.MaxValue;

      foreach (var layer in image.Layers)
      {
        if (layer.Zeroes < temp)
        {
          result = layer;
          temp = layer.Zeroes;
        }
      }

      return result;
    }
  }
}
