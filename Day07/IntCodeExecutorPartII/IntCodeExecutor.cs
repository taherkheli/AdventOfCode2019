using System;
using System.Collections;

namespace IntCodeExecutorPartII
{
  public class IntCodeExecutor
  {
    private int _iPtr;
    private int[] _intCode;
    private readonly int[] _memory;    
    private Queue _inputQueue;
    private bool _awaitingInput;

    private int? _output;
    
    //public int[] IntCode { get => _intCode; set => _intCode = value; }
    public int[] IntCode { get => _intCode; }
    public bool AwaitingInput { get => _awaitingInput; }

    public Queue InputQueue  { get => _inputQueue; set => _inputQueue = value; }

    public int? Output { get => _output; }
    
    public IntCodeExecutor(int[] intCode)
    {
      _memory = new int[intCode.Length];
      _memory.Initialize();
      Array.Copy(intCode, _memory, intCode.Length);
      _intCode = intCode;
      _iPtr = 0;
      _output = null;
      _inputQueue = new Queue();
      _awaitingInput = false;
      Initialize();
    }

    public int[] Execute()
    {
      bool keepGoing = true;

      while (keepGoing)
      {
        var instruction = InstDecoder.Decode(_intCode[_iPtr]);

        switch (instruction.OpCode)
        {
          case Opcodes.Add:
            Add(instruction);
            break;
          case Opcodes.Multiply:
            Multiply(instruction);
            break;
          case Opcodes.Read:
            Read(instruction);
            break;
          case Opcodes.Write:
            Write(instruction);
            break;
          case Opcodes.JumpIfTrue:
            JumpIfTrue(instruction);
            break;
          case Opcodes.JumpIfFalse:
            JumpIfFalse(instruction);
            break;
          case Opcodes.LessThan:
            LessThan(instruction);
            break;
          case Opcodes.Equals:
            Equals(instruction);
            break;
          case Opcodes.Exit:
            _iPtr = 0;  //reset
            keepGoing = false;
            break;
          default:
            _iPtr = 0;  //reset
            throw new InvalidOperationException("Unknown Opcode");
        }

        if (_awaitingInput)
          return _intCode;
      }

      return _intCode;
    }

    public void ResumeExecution()
    {
      if (_awaitingInput)
      {
        _awaitingInput = false;
        _output = null;
        Execute();
      }
    }

    public void Initialize()
    {
      Array.Copy(_memory, _intCode, _memory.Length);
      _iPtr = 0;
      _output = null;
    }

    private void Add(Instruction i)
    {
      int p1, p2;
      Tuple<int, int> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (i.p3ParamMode == ParamMode.Ref)
        _intCode[_intCode[_iPtr + 3]] = p1 + p2;
      else
        _intCode[_iPtr + 3] = p1 + p2;

      _iPtr += 4;
    }

    private void Multiply(Instruction i)
    {
      int p1, p2;
      Tuple<int, int> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (i.p3ParamMode == ParamMode.Ref)
        _intCode[_intCode[_iPtr + 3]] = p1 * p2;
      else
        _intCode[_iPtr + 3] = p1 * p2;

      _iPtr += 4;
    }

    private void Read(Instruction i)
    {
      if (_inputQueue.Count != 0)
      {
        _intCode[_intCode[_iPtr + 1]] = (int)_inputQueue.Dequeue();
        _iPtr += 2;
      }
      else
        _awaitingInput = true;
    }

    private void Write(Instruction i)
    {
      int p1;

      if (i.p1ParamMode == ParamMode.Ref)
        p1 = _intCode[_intCode[_iPtr + 1]];
      else
        p1 = _intCode[_iPtr + 1];

      _output = p1;
      _iPtr += 2;
    }

    private void JumpIfTrue(Instruction i)
    {
      int p1, p2;
      Tuple<int, int> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (p1 != 0)
        _iPtr = p2;
      else
        _iPtr += 3;   //do nothing  
    }

    private void JumpIfFalse(Instruction i)
    {
      int p1, p2;
      Tuple<int, int> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (p1 == 0)
        _iPtr = p2;
      else
        _iPtr += 3;   //do nothing
    }

    private void LessThan(Instruction i)
    {
      int p1, p2;
      Tuple<int, int> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;
      int val = ((p1 < p2) ? 1 : 0);

      _intCode[_intCode[_iPtr + 3]] = val;

      _iPtr += 4;
    }

    private void Equals(Instruction i)
    {
      int p1, p2;
      Tuple<int, int> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;
      int val = ((p1 == p2) ? 1 : 0);

      _intCode[_intCode[_iPtr + 3]] = val;

      _iPtr += 4;
    }

    private Tuple<int, int> GetParameters(Instruction i)
    {
      int p1, p2;

      if (i.p1ParamMode == ParamMode.Ref)
        p1 = _intCode[_intCode[_iPtr + 1]];
      else
        p1 = _intCode[_iPtr + 1];

      if (i.p2ParamMode == ParamMode.Ref)
        p2 = _intCode[_intCode[_iPtr + 2]];
      else
        p2 = _intCode[_iPtr + 2];

      return Tuple.Create(p1, p2);
    }
  }
}