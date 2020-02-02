using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntCodeExecutorNs;

namespace InstDecoderTest
{
  [TestClass]
  public class UnitTest
  {
    [TestMethod]
    public void DecoderWorks()
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
  }
}
