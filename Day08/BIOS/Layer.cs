namespace BIOS
{
  public class Layer
  {
    private int _rows;
    private int _columns;
    private int[,] _data;

    public Layer(int rows, int columns)
    {
      _rows = rows;
      _columns = columns;
      _data = new int[_rows, columns];
    }

    public int Zeroes { 
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

    public void FillData(int[] input)
    {
      if (input.Length == (_rows * _columns))
      {
        int index = 0;
        for (int i = 0; i < _rows; i++)
        {
          for (int j = 0; j < _columns; j++)
          {
            _data[i, j] = input[index];
            index++;
          }
        }
      }
    }

    private int CountOccurences(int digit)
    {
      int result = 0;

      for (int i = 0; i < _rows; i++)
        for (int j = 0; j < _columns; j++)
          if (_data[i, j] == digit)
            result++;

      return result;
    }
  }
}