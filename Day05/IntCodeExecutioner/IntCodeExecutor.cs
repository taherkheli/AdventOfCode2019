using IntCodeExecutioner;
using System;
using System.Collections.Generic;
using System.Text;

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
        switch (DecodeInstruction().OpCode)
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

      if (_intCode[_iPtr] == 99)
        instruction.OpCode = Opcodes.Exit;
      else
      {
        //var chars = Convert.ToString(_intCode[_iPtr], 2).ToCharArray();
        //int[] bits = new int[16];
        //bits.Initialize();
        //for (int i = 0; i < bits.Length; i++)
        //{
        //  if (i < chars.Length)
        //    bits[i] = (int)Char.GetNumericValue(chars[(chars.Length - (i + 1))]);   //no need to reverse order now 
        //  else
        //    break;
        //}

        switch (_intCode[_iPtr])
        {
          case 1:
            instruction.OpCode = Opcodes.Add;
            break;
          case 2:
            instruction.OpCode = Opcodes.Multiply;
            break;
          case 3:
            instruction.OpCode = Opcodes.Read;
            break;
          case 4:
            instruction.OpCode = Opcodes.Write;
            break;
          default:
            break;
        }
      }

      return instruction;
    }

    private void Add(Instruction i)
    {
      _intCode[_intCode[_iPtr + 3]] = _intCode[_intCode[_iPtr + 1]] + _intCode[_intCode[_iPtr + 2]];
    }

    private void Multiply(Instruction i)
    {
      _intCode[_intCode[_iPtr + 3]] = _intCode[_intCode[_iPtr + 1]] * _intCode[_intCode[_iPtr + 2]];
    }

    private void Read(Instruction i)
    {
      Console.Write("\n please enter a diagnostic code and press enter:  ");
      _intCode[_intCode[_iPtr + 1]] = int.Parse(Console.ReadLine());
    }

    private void Write(Instruction i)
    {
      Console.WriteLine("\n value :  {0}", _intCode[_intCode[_iPtr + 1]]);      
    }
  }
}
