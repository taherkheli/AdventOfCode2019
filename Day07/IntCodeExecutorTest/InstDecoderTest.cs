using Microsoft.VisualStudio.TestTools.UnitTesting;
using FeedbackAmplifiers;

namespace InstDecoderTest
{
  [TestClass]
  public class UnitTest
  {
    [TestMethod]
    public void DecoderWorks()
    {
      int input = 1002;
      var expected = new Instruction
      {
        OpCode = Opcodes.Multiply,
        p1ParamMode = ParamMode.Ref,
        p2ParamMode = ParamMode.Val,
        p3ParamMode = ParamMode.Ref
      };

      var actual = InstDecoder.Decode(input);

      Assert.AreEqual((Instruction)expected, (Instruction)actual, "OK!");
    }
  }
}
