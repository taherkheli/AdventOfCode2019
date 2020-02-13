using System;

namespace IntCodeExecutorNs
{
  public class IntCodeExecutor
  {
    private long _iPtr;
    private long[] _intCode;
    private long _relBase;                  //relative base for supporting relative mode parameters
    private long _intCodeLen;
    private readonly long[] _memory;

    public long[] IntCode { get => _intCode; set => _intCode = value; }

    public IntCodeExecutor(long[] intCode)
    {
      _intCodeLen = intCode.Length;
      _memory = new long[_intCodeLen * 100];  //100 times the program
      _memory.Initialize();
      Array.Copy(intCode, _memory, _intCodeLen);
      _intCode = intCode;
      _relBase = 0;
      _iPtr = 0;
    }

    public void Initialize()
    {
      //reload ROM without doing anything at all to RAM
      Array.Copy(_memory, _intCode, _intCodeLen);
      _iPtr = 0;
    }

    public long[] Execute()
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
          case Opcodes.RelativeBaseOffset:
            RelativeBaseOffset(instruction);
            break;
          case Opcodes.Exit:
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
    
    private void Add(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (i.p3ParamMode == ParamMode.Ref)
        _memory[_intCode[_iPtr + 3]] = p1 + p2;
      else if (i.p3ParamMode == ParamMode.Val)
        _memory[_iPtr + 3] = p1 + p2;
      else
        _memory[_intCode[_iPtr + 3] + _relBase] = p1 + p2;

      _iPtr += 4;
    }

    private void Multiply(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (i.p3ParamMode == ParamMode.Ref)
        _memory[_intCode[_iPtr + 3]] = p1 * p2;
      else if (i.p3ParamMode == ParamMode.Val)
        _memory[_iPtr + 3] = p1 * p2;
      else
        _memory[_intCode[_iPtr + 3] + _relBase] = p1 * p2;
          
      _iPtr += 4;
    }
    
    private void Read(Instruction i)
    {
      Console.Write("\n please enter a diagnostic code and press enter:  ");
      var value = long.Parse(Console.ReadLine());

      if (i.p1ParamMode == ParamMode.Ref)
        _memory[_intCode[_iPtr + 1]] = value;
      else if (i.p1ParamMode == ParamMode.Val)
        _memory[_iPtr + 1] = value;
      else
        _memory[_intCode[_iPtr + 1] + _relBase] = value;
            
      _iPtr += 2;
    }

    private void Write(Instruction i)
    {
      long p1;

      if (i.p1ParamMode == ParamMode.Ref)
        p1 = _memory[_intCode[_iPtr + 1]];
      else if (i.p1ParamMode == ParamMode.Val)
        p1 = _intCode[_iPtr + 1];
      else
        p1 = _memory[_intCode[_iPtr + 1] + _relBase];

      Console.WriteLine("\n value :  {0}", p1);

      _iPtr += 2;
    }

    private void JumpIfTrue(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (p1 != 0)
        _iPtr = p2;
      else
        _iPtr += 3;   //do nothing  
    }

    private void JumpIfFalse(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (p1 == 0)
        _iPtr = p2;
      else
        _iPtr += 3;   //do nothing
    }

    private void LessThan(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;           
      long val = ((p1<p2)? 1: 0);
               
      if (i.p3ParamMode == ParamMode.Ref)
        _memory[_intCode[_iPtr + 3]] = val;
      else if (i.p3ParamMode == ParamMode.Val)
        _memory[_iPtr + 3] = val;
      else
        _memory[_intCode[_iPtr + 3] + _relBase] = val;              

      _iPtr += 4;
    }

    private void Equals(Instruction i)
    {
      long p1, p2;
      Tuple<long, long> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;
      long val = ((p1 == p2) ? 1 : 0);

      if (i.p3ParamMode == ParamMode.Ref)
        _memory[_intCode[_iPtr + 3]] = val;
      else if (i.p3ParamMode == ParamMode.Val)
        _memory[_iPtr + 3] = val;
      else
        _memory[_intCode[_iPtr + 3] + _relBase] = val;

      _iPtr += 4;
    }

    private void RelativeBaseOffset(Instruction i)
    {
      long p1;

      if (i.p1ParamMode == ParamMode.Ref)
        p1 = _memory[_intCode[_iPtr + 1]];
      else if (i.p1ParamMode == ParamMode.Val)
        p1 = _intCode[_iPtr + 1];
      else  //Param.Rel
        p1 = _memory[_intCode[_iPtr + 1] + _relBase];

      _relBase = _relBase + p1;  //update relative base

      _iPtr += 2;
    }

    private Tuple<long, long> GetParameters(Instruction i)
    {
      long p1, p2;
      
      if (i.p1ParamMode == ParamMode.Ref)
        p1 = _memory[_intCode[_iPtr + 1]];
      else if (i.p1ParamMode == ParamMode.Val)
        p1 = _intCode[_iPtr + 1];
      else //Param.Rel
        p1 = _memory[_intCode[_iPtr + 1] + _relBase];

      if (i.p2ParamMode == ParamMode.Ref)
        p2 = _memory[_intCode[_iPtr + 2]];
      else if (i.p2ParamMode == ParamMode.Val)
        p2 = _intCode[_iPtr + 2];
      else
        p2 = _memory[_intCode[_iPtr + 2] + _relBase];

      return Tuple.Create(p1, p2);
    }
  }
}