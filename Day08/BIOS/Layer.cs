namespace BIOS
{
  public class Layer
  {
    private readonly int _rows;
    private readonly int _columns;
    private readonly int[] _data;

    public Layer(int rows, int columns, int[] input)
    {
      _rows = rows;
      _columns = columns;
      _data = new int[_rows * _columns];
      FillData(input);
    }

    public int Zeroes
    {
      get
      {
        return CountOccurences(0);
      }
    }

    public int Ones
    {
      get
      {
        return CountOccurences(1);
      }
    }

    public int Twos
    {
      get
      {
        return CountOccurences(2);
      }
    }

    public int[] Data { get => _data; }

    //paints a layer on top of itself to return a new merged layer
    public Layer PaintOnTop(Layer layer)
    {
      var result = new Layer(this._rows, this._columns, this._data);

      for (int i = 0; i < result._data.Length; i++)
      {
        if (result._data[i] == 2)   //if pixel is transparent, overwrite it
          result._data[i] = layer._data[i];        
      }

      return result;
    }
       
    private int CountOccurences(int digit)
    {
      int result = 0;
      int size = _rows * _columns;

      for (int i = 0; i < size; i++)
      {
        if (_data[i] == digit)
          result++;
      }

      return result;
    }

    private void FillData(int[] input)
    {
      if (input.Length == (_rows * _columns))
      {
        for (int i = 0; i < input.Length; i++)
          _data[i] = input[i];
      }
    }
  }
}