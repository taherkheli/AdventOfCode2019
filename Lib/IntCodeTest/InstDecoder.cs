using IntCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntCodeTest
{
	[TestClass]
	public class InstDecoder
	{
		[TestMethod]
		public void DecoderWorks1()
		{
			int input = 1002;
			var expected = new Instruction
			{
				OpCode = Opcodes.Multiply,
				p1ParamMode = ParamMode.Reference,
				p2ParamMode = ParamMode.Value,
				p3ParamMode = ParamMode.Reference
			};

			var actual = IntCode.InstDecoder.Decode(input);

			Assert.AreEqual((Instruction)expected, (Instruction)actual, "OK!");
		}

		[TestMethod]
		public void DecoderWorks2()
		{
			int input = 109;
			var expected = new Instruction
			{
				OpCode = Opcodes.RelativeBaseOffset,
				p1ParamMode = ParamMode.Value,
				p2ParamMode = ParamMode.NA,
				p3ParamMode = ParamMode.NA
			};

			var actual = IntCode.InstDecoder.Decode(input);

			Assert.AreEqual((Instruction)expected, (Instruction)actual, "OK!");
		}

		[TestMethod]
		public void DecoderWorks3()
		{
			int input = 204;
			var expected = new Instruction
			{
				OpCode = Opcodes.Write,
				p1ParamMode = ParamMode.Relative,
				p2ParamMode = ParamMode.NA,
				p3ParamMode = ParamMode.NA
			};

			var actual = IntCode.InstDecoder.Decode(input);

			Assert.AreEqual((Instruction)expected, (Instruction)actual, "OK!");
		}

		[TestMethod]
		public void DecoderWorks4()
		{
			int input = 203;
			var expected = new Instruction
			{
				OpCode = Opcodes.Read,
				p1ParamMode = ParamMode.Relative,
				p2ParamMode = ParamMode.NA,
				p3ParamMode = ParamMode.NA
			};

			var actual = IntCode.InstDecoder.Decode(input);

			Assert.AreEqual((Instruction)expected, (Instruction)actual, "OK!");
		}
	}
}