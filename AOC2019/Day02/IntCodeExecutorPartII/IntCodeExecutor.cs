using System;

namespace IntCodeExecutorPartII
{
  public class IntCodeExecutor
  {
    private int _iPtr;
    private int[] _intCode;
    private readonly int[] _memory;

    public int[] IntCode { get => _intCode; set => _intCode = value; }

    public IntCodeExecutor(int[] intCode)
    {
      _memory = new int[intCode.Length];
      _memory.Initialize();      
      Array.Copy(intCode, _memory, intCode.Length);
      _intCode = intCode;
      _iPtr = 0;
    }

    public void Initialize()
    {
      Array.Copy(_memory, _intCode, _memory.Length);
      _iPtr = 0;
    }
    
    public int[] Execute()
    {
      bool keepGoing = true;

      while (keepGoing)
      {
        switch (_intCode[_iPtr])
        {
          case (int)Opcodes.Add:
            Add();
            break;

          case (int)Opcodes.Multiply:
            Multiply();
            break;

          case (int)Opcodes.Exit:
            _iPtr = 0;  //reset
            keepGoing = false;
            break;

          default:
            _iPtr = 0;  //reset
            throw new InvalidOperationException("Unknown Opcode");
        }
      }

      return _intCode;
    }

    private void Add()
    {
      _intCode[_intCode[_iPtr + 3]] = _intCode[_intCode[_iPtr + 1]] + _intCode[_intCode[_iPtr + 2]];
      _iPtr += 4;
    }

    private void Multiply()
    {
      _intCode[_intCode[_iPtr + 3]] = _intCode[_intCode[_iPtr + 1]] * _intCode[_intCode[_iPtr + 2]];
      _iPtr += 4;
    }
  }
}