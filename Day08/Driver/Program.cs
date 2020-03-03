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

      /****************************************************************************************/
      //part b
      
      if (image.Layers.Length > 0)
      {
        var finalImage = image.Layers[0];

        for (int i = 0; i < image.Layers.Length; i++)
          finalImage = finalImage.PaintOnTop(image.Layers[i]);

        Console.WriteLine("\n\nfinal image represented in linear format:\n\n");
        for (int i = 0; i < finalImage.Data.Length; i++)
          Console.Write(finalImage.Data[i]);

        Console.WriteLine("\n\nfinal image represented as layers/matrix:\n");
        for (int r = 0; r < rows; r++)
        {
          for (int c = 0; c < columns; c++)
            Console.Write("{0}", finalImage.Data[r * columns + c]);

          Console.WriteLine("");
        }

        PrintImage(rows, columns, finalImage);
      }
    }

    private static void PrintImage(int rows, int columns, Layer finalImage)
    {
      Console.WriteLine("\n\nfinal image represented as an image:\n\n");

      for (int r = 0; r < rows; r++)
      {
        for (int c = 0; c < columns; c++)
        {
          int pixel = finalImage.Data[r * columns + c];

          if (pixel == 1)
            Console.Write(" 1 ");
          else
            Console.Write("   ");
        }

        Console.WriteLine("");
      }
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
