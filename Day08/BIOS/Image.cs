using System;
using System.Collections.Generic;

namespace BIOS
{
  public class Image
  {
    private readonly int _rows;
    private readonly int _columns;
    private readonly List<Layer> _layers;

    public Image(int rows, int columns)
    {
      _rows = rows;
      _columns = columns;
      _layers = new List<Layer>();
    }

    public List<Layer> Layers { get => _layers; }

    public void FillData(int[] input)
    {
      foreach (var chunk in GetChunks(input))
      {
        var layer = new Layer(_rows, _columns);
        layer.FillData(chunk);
        _layers.Add(layer);
      }
    }

    private List<int[]> GetChunks(int[] input)
    {
      List<int[]> result = null;
      int size = _rows * _columns;

      if (input.Length % size == 0)   // must be a multiple of the size of a layer 
      {
        int numLayers = input.Length / size;
        result = new List<int[]>();

        for (int i = 0; i < numLayers; i++)
        {
          var x = new int[size];
          int index = 0;
          for (int j = size * i; j < size * (i + 1); j++)
          {
            x[index] = input[j];
            index++;
          }

          result.Add(x);
        }
      }

      return result;
    }
  }  
}