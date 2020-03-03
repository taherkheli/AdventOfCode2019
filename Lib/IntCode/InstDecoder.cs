using System;

namespace IntCode
{
	public static class InstDecoder
	{
		public static Instruction Decode(long input)
		{
			Instruction instruction = new Instruction();

			char[] charCode = new char[5];
			charCode.Initialize();   //default is \0 so no padding needed if leading zereos are missing
			var chars = Convert.ToString(input, 10).ToCharArray();

			for (int i = 0; i < charCode.Length; i++)
			{
				if (i < chars.Length)
					charCode[i] = (chars[(chars.Length - (i + 1))]);   //no need to reverse order now 
				else
					break;
			}

			if ((charCode[1] == '9') && (charCode[0] == '9'))
				instruction.OpCode = Opcodes.Exit;
			else
			{
				switch (charCode[0])
				{
					case '1':
						instruction.OpCode = Opcodes.Add;
						instruction = AssignModesToParams(instruction, 3, charCode);
						break;
					case '2':
						instruction.OpCode = Opcodes.Multiply;
						instruction = AssignModesToParams(instruction, 3, charCode);
						break;
					case '3':
						instruction.OpCode = Opcodes.Read;
						instruction = AssignModesToParams(instruction, 1, charCode);
						instruction.p2ParamMode = ParamMode.NA;
						instruction.p3ParamMode = ParamMode.NA;
						break;
					case '4':
						instruction.OpCode = Opcodes.Write;
						instruction = AssignModesToParams(instruction, 1, charCode);
						instruction.p2ParamMode = ParamMode.NA;
						instruction.p3ParamMode = ParamMode.NA;
						break;
					case '5':
						instruction.OpCode = Opcodes.JumpIfTrue;
						instruction = AssignModesToParams(instruction, 2, charCode);
						instruction.p3ParamMode = ParamMode.NA;
						break;
					case '6':
						instruction.OpCode = Opcodes.JumpIfFalse;
						instruction = AssignModesToParams(instruction, 2, charCode);
						instruction.p3ParamMode = ParamMode.NA;
						break;
					case '7':
						instruction.OpCode = Opcodes.LessThan;
						instruction = AssignModesToParams(instruction, 3, charCode);
						break;
					case '8':
						instruction.OpCode = Opcodes.Equals;
						instruction = AssignModesToParams(instruction, 3, charCode);
						break;
					case '9':
						instruction.OpCode = Opcodes.RelativeBaseOffset;
						instruction = AssignModesToParams(instruction, 1, charCode);
						instruction.p2ParamMode = ParamMode.NA;
						instruction.p3ParamMode = ParamMode.NA;
						break;
					default:
						instruction.p1ParamMode = ParamMode.NA;
						instruction.p2ParamMode = ParamMode.NA;
						instruction.p3ParamMode = ParamMode.NA;
						break;
				}
			}

			return instruction;
		}

		private static Instruction AssignModesToParams(Instruction i, int numberOfParams, char[] charCode)
		{
			switch (numberOfParams)
			{
				case 1:					
					i.p1ParamMode = ParamMode.Reference;
					if (charCode[2] == '1')
						i.p1ParamMode = ParamMode.Value;
					if (charCode[2] == '2')
						i.p1ParamMode = ParamMode.Relative;
					break;

				case 2:
					i.p1ParamMode = ParamMode.Reference;
					if (charCode[2] == '1')
						i.p1ParamMode = ParamMode.Value;
					if (charCode[2] == '2')
						i.p1ParamMode = ParamMode.Relative;

					i.p2ParamMode = ParamMode.Reference;
					if (charCode[3] == '1')
						i.p2ParamMode = ParamMode.Value;
					if (charCode[3] == '2')
						i.p2ParamMode = ParamMode.Relative;
					break;

				case 3:
					i.p1ParamMode = ParamMode.Reference;
					if (charCode[2] == '1')
						i.p1ParamMode = ParamMode.Value;
					if (charCode[2] == '2')
						i.p1ParamMode = ParamMode.Relative;

					i.p2ParamMode = ParamMode.Reference;
					if (charCode[3] == '1')
						i.p2ParamMode = ParamMode.Value;
					if (charCode[3] == '2')
						i.p2ParamMode = ParamMode.Relative;

					i.p3ParamMode = ParamMode.Reference;
					if (charCode[4] == '1')
						i.p3ParamMode = ParamMode.Value;
					if (charCode[4] == '2')
						i.p3ParamMode = ParamMode.Relative;
					break;

				default:
					i.p1ParamMode = ParamMode.NA;
					i.p2ParamMode = ParamMode.NA;
					i.p3ParamMode = ParamMode.NA;
					break;
			}

			return i;
		}
	}
}