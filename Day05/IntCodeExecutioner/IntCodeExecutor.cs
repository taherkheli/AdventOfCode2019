using IntCodeExecutioner;
using System;

namespace IntCodeExecutor
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
      _iPtr = 0;;
    }

    public void Initialize()
    {
      Array.Copy(_memory, _intCode, _memory.Length);
      _iPtr = 0;
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
            _iPtr += 4;
            break;

          case Opcodes.Multiply:
            Multiply(nextInstruction);
            _iPtr += 4;
            break;

          case Opcodes.Read:
            Read(nextInstruction);
            _iPtr += 2;
            break;

          case Opcodes.Write:
            Write(nextInstruction);
            _iPtr += 2;
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

      if (i.p1ParamCode == ParamCode.Ref)
        p1 = _intCode[_intCode[_iPtr + 1]];
      else
        p1 = _intCode[_iPtr + 1];

      if (i.p2ParamCode == ParamCode.Ref)
        p2 = _intCode[_intCode[_iPtr + 2]];
      else
        p2 = _intCode[_iPtr + 2];
      
      if (i.p3ParamCode == ParamCode.Ref)
        _intCode[_intCode[_iPtr + 3]] = p1 + p2;
      else
        _intCode[_iPtr + 3] = p1 + p2;
    }

    private void Multiply(Instruction i)
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

      if (i.p3ParamCode == ParamCode.Ref)
        _intCode[_intCode[_iPtr + 3]] = p1 * p2;
      else
        _intCode[_iPtr + 3] = p1 * p2;
    }

    private void Read(Instruction i)
    {
      //Parameters that an instruction writes to will never be in immediate mode. 
      //probably means 1p will always be ref type here

      Console.Write("\n please enter a diagnostic code and press enter:  ");
      _intCode[_intCode[_iPtr + 1]] = int.Parse(Console.ReadLine());
    }

    private void Write(Instruction i)
    {
      int p1;

      if (i.p1ParamCode == ParamCode.Ref)
        p1 = _intCode[_intCode[_iPtr + 1]];
      else
        p1 = _intCode[_iPtr + 1];

      Console.WriteLine("\n value :  {0}", p1);      
    }
  }
}
