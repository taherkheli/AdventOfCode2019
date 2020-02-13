using IntCodeExecutorNs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntCodeExecutorTest
{
  [TestClass]
  public class ExecutorTest
  {
    [TestMethod]
    public void TC1()
    {
      var intCode = new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
      var expected = new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };

      var actual = (new IntCodeExecutor(intCode)).Execute();

      CollectionAssert.AreEqual(expected, actual, "OK!");
    }
  }
}