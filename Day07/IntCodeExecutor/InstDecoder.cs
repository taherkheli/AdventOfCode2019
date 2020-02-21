using System;

namespace FeedbackAmplifiers
{
  public static class InstDecoder
  {
    public static Instruction Decode(int input)
    {
      Instruction instruction = new Instruction();

      char[] charCode = new char[5];
      charCode.Initialize();   //default is \0
      var chars = Convert.ToString(input, 10).ToCharArray();

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

        instruction.p1ParamMode = ParamMode.Ref;
        instruction.p2ParamMode = ParamMode.Ref;
        instruction.p3ParamMode = ParamMode.Ref;

        if (charCode[2] == '1')
          instruction.p1ParamMode = ParamMode.Val;
        if (charCode[3] == '1')
          instruction.p2ParamMode = ParamMode.Val;
        if (charCode[4] == '1')
          instruction.p3ParamMode = ParamMode.Val;
      }

      return instruction;
    }
  }
}