namespace BIOS
{
  public class Image
  {
    private readonly int _rows;
    private readonly int _columns;
    private Layer[] _layers;

    public Image(int rows, int columns, int[] input)
    {
      _rows = rows;
      _columns = columns;
      FillData(input);
    }

    public Layer[] Layers { get => _layers; }

    private void FillData(int[] input)
    {
      int size = _rows * _columns;

      if (input.Length % size == 0)   // input length must be a multiple of the layer size 
      {
        int numLayers = input.Length / size;
        _layers = new Layer[numLayers];

        for (int i = 0; i < numLayers; i++)
          _layers[i] = new Layer(_rows, _columns, GetFragment(input, i, size));
      }
    }

    private int[] GetFragment(int[] input, int index, int size)
    {
      var result = new int[size];

      for (int i = 0; i < size; i++)
        result[i] = input[index * size + i];

      return result;
    }
  }
}