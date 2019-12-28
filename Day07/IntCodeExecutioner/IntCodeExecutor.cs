using IntCodeExecutioner;
using System;

namespace IntCodeExecutor
{
  public class IntCodeExecutor
  {
    private int _iPtr;
    private int[] _intCode;
    private readonly int[] _memory;
    private readonly int[] _input;
    private int _inIndex;
    private int? _output;


    public int[] IntCode { get => _intCode; set => _intCode = value; }
    public int? Output { get => _output; }

    public IntCodeExecutor(int[] intCode, int[] input)
    {
      _memory = new int[intCode.Length];
      _memory.Initialize();
      Array.Copy(intCode, _memory, intCode.Length);
      _intCode = intCode;
      _iPtr = 0; ;
      _input = input;
      _inIndex = 0;
      _output = null;
    }

    public void Initialize()
    {
      Array.Copy(_memory, _intCode, _memory.Length);
      _iPtr = 0;
      _inIndex = 0;
      _output = null;
    }

    public int[] Execute()
    {
      var nextInstruction = DecodeInstruction();

      while (nextInstruction.OpCode != Opcodes.Exit)
      {
        switch (nextInstruction.OpCode)
        {
          case Opcodes.Add:
            Add(nextInstruction);
            break;
          case Opcodes.Multiply:
            Multiply(nextInstruction);
            break;
          case Opcodes.Read:
            Read();
            break;
          case Opcodes.Write:
            Write(nextInstruction);
            break;
          case Opcodes.JumpIfTrue:
            JumpIfTrue(nextInstruction);
            break;
          case Opcodes.JumpIfFalse:
            JumpIfFalse(nextInstruction);
            break;
          case Opcodes.LessThan:
            LessThan(nextInstruction);
            break;
          case Opcodes.Equals:
            Equals(nextInstruction);
            break;
          default:
            throw new InvalidOperationException("Unknown Opcode");
        }

        nextInstruction = DecodeInstruction();
      }

      return _intCode;
    }

    private Instruction DecodeInstruction()
    {
      Instruction instruction = new Instruction();

      char[] charCode = new char[5];
      charCode.Initialize();   //default is \0
      var chars = Convert.ToString(_intCode[_iPtr], 10).ToCharArray();

      for (int i = 0; i < charCode.Length; i++)
      {
        if (i < chars.Length)
          charCode[i] = (chars[(chars.Length - (i + 1))]);   //no need to reverse order now 
        else
          break;
      }

      if ((charCode[0] == '9') && (charCode[1] == '9'))
        instruction.OpCode = Opcodes.Exit;
      else
      {
        switch (charCode[0])
        {
          case '1':
            instruction.OpCode = Opcodes.Add;
            break;
          case '2':
            instruction.OpCode = Opcodes.Multiply;
            break;
          case '3':
            instruction.OpCode = Opcodes.Read;
            break;
          case '4':
            instruction.OpCode = Opcodes.Write;
            break;
          case '5':
            instruction.OpCode = Opcodes.JumpIfTrue;
            break;
          case '6':
            instruction.OpCode = Opcodes.JumpIfFalse;
            break;
          case '7':
            instruction.OpCode = Opcodes.LessThan;
            break;
          case '8':
            instruction.OpCode = Opcodes.Equals;
            break;
          default:
            break;
        }

        instruction.p1ParamCode = ParamCode.Ref;
        instruction.p2ParamCode = ParamCode.Ref;
        instruction.p3ParamCode = ParamCode.Ref;

        if (charCode[2] == '1')
          instruction.p1ParamCode = ParamCode.Val;
        if (charCode[3] == '1')
          instruction.p2ParamCode = ParamCode.Val;
        if (charCode[4] == '1')
          instruction.p3ParamCode = ParamCode.Val;
      }

      return instruction;
    }

    private void Add(Instruction i)
    {
      int p1, p2;
      Tuple<int, int> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;

      if (i.p3ParamCode == ParamCode.Ref)
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

      if (i.p3ParamCode == ParamCode.Ref)
        _intCode[_intCode[_iPtr + 3]] = p1 * p2;
      else
        _intCode[_iPtr + 3] = p1 * p2;

      _iPtr += 4;
    }

    private void Read()
    {
      _intCode[_intCode[_iPtr + 1]] = _input[_inIndex];

      _inIndex++;
      _iPtr += 2;
    }

    private void Write(Instruction i)
    {
      int p1;

      if (i.p1ParamCode == ParamCode.Ref)
        p1 = _intCode[_intCode[_iPtr + 1]];
      else
        p1 = _intCode[_iPtr + 1];

      //Console.WriteLine("\n value :  {0}", p1);
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
      int val = ((p1<p2)? 1: 0);      
      if (i.p3ParamCode == ParamCode.Ref)
        _intCode[_intCode[_iPtr + 3]] = val;
      else
        _intCode[_iPtr + 3] = val;

      _iPtr += 4;
    }

    private void Equals(Instruction i)
    {
      int p1, p2;
      Tuple<int, int> t = GetParameters(i);
      p1 = t.Item1;
      p2 = t.Item2;
      int val = ((p1 == p2) ? 1 : 0);
      if (i.p3ParamCode == ParamCode.Ref)
        _intCode[_intCode[_iPtr + 3]] = val;
      else
        _intCode[_iPtr + 3] = val;

      _iPtr += 4;
    }

    private Tuple<int, int> GetParameters(Instruction i)
    {
      int p1, p2;
      
      if (i.p1ParamCode == ParamCode.Ref)
        p1 = _intCode[_intCode[_iPtr + 1]];
      else
        p1 = _intCode[_iPtr + 1];
      
      if (i.p2ParamCode == ParamCode.Ref)
        p2 = _intCode[_intCode[_iPtr + 2]];
      else
        p2 = _intCode[_iPtr + 2];

      return Tuple.Create(p1, p2);
    }
  }
}