using System;

namespace IntCodeExecutorPartI
{
  public class IntCodeExecutor
  {
    private int _iPtr;
    private int[] _intCode;

    public IntCodeExecutor(int[] intCode)
    {
      _intCode = intCode;
      _iPtr = 0;
    }

    public int[] IntCode { get => _intCode; set => _intCode = value; }

    public int[] Execute()
    {
      while (_intCode[_iPtr] != (int)Opcodes.Exit)
      {
        switch (_intCode[_iPtr])
        {
          case (int)Opcodes.Add:
            Add();
            break;

          case (int)Opcodes.Multiply:
            Multiply();
            break;

          default:
            throw new InvalidOperationException("Unknown Opcode");
        }

        _iPtr += 4;
      }

      _iPtr = 0;  //reset _iPtr
      return _intCode;
    }
    
    private void Add()
    {
      _intCode[_intCode[_iPtr + 3]] = _intCode[_intCode[_iPtr + 1]] + _intCode[_intCode[_iPtr + 2]];   
    }

    private void Multiply()
    {
      _intCode[_intCode[_iPtr + 3]] = _intCode[_intCode[_iPtr + 1]] * _intCode[_intCode[_iPtr + 2]];
    }
  }
}
