using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntCodeExecutorNs;

namespace IntCodeExecutorTest
{
  [TestClass]
  public class InstDecoderTest
  {
    [TestMethod]
    public void DecoderWorks1()
    {
      int input = 1002;
      var expected = new Instruction();
      expected.OpCode = Opcodes.Multiply;
      expected.p1ParamMode = ParamMode.Ref;
      expected.p2ParamMode = ParamMode.Val;
      expected.p3ParamMode = ParamMode.Ref;

      var actual = InstDecoder.Decode(input);

      Assert.AreEqual((Instruction)expected, (Instruction)actual, "OK!");
    }

    [TestMethod]
    public void DecoderWorks2()
    {
      int input = 109;
      var expected = new Instruction();
      expected.OpCode = Opcodes.RelativeBaseOffset;
      expected.p1ParamMode = ParamMode.Val;

      var actual = InstDecoder.Decode(input);

      Assert.AreEqual((Instruction)expected, (Instruction)actual, "OK!");
    }

    [TestMethod]
    public void DecoderWorks3()
    {
      int input = 204;
      var expected = new Instruction();
      expected.OpCode = Opcodes.Write;
      expected.p1ParamMode = ParamMode.Rel;

      var actual = InstDecoder.Decode(input);

      Assert.AreEqual((Instruction)expected, (Instruction)actual, "OK!");
    }

    [TestMethod]
    public void DecoderWorks4()
    {
      int input = 203;
      var expected = new Instruction();
      expected.OpCode = Opcodes.Read;
      expected.p1ParamMode = ParamMode.Rel;

      var actual = InstDecoder.Decode(input);

      Assert.AreEqual((Instruction)expected, (Instruction)actual, "OK!");
    }
  }
}
