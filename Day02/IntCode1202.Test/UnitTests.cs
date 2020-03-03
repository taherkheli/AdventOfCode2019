using IntCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntCode1202.Test
{
  [TestClass]
  public class UnitTests
  {
    [TestMethod]
    public void Test1()
    {
      long[] input = { 1, 0, 0, 0, 99 };
      long[] expected = { 2, 0, 0, 0, 99 };
      Executor intCodeExecutor = new Executor(input);

      var actual = intCodeExecutor.Execute();
           
      CollectionAssert.AreEqual(actual, expected);
    }

    [TestMethod]
    public void Test2()
    {
      long[] input = { 2, 3, 0, 3, 99 };
      long[] expected = { 2, 3, 0, 6, 99 };
      Executor intCodeExecutor = new Executor(input);

      var actual = intCodeExecutor.Execute();

      CollectionAssert.AreEqual(actual, expected);
    }

    [TestMethod]
    public void Test3()
    {
      long[] input = { 2, 4, 4, 5, 99, 0 };
      long[] expected = { 2, 4, 4, 5, 99, 9801 };
      Executor intCodeExecutor = new Executor(input);

      var actual = intCodeExecutor.Execute();

      CollectionAssert.AreEqual(actual, expected);
    }
    [TestMethod]
    public void Test4()
    {
      long[] input = { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
      long[] expected = { 30, 1, 1, 4, 2, 5, 6, 0, 99 };
      Executor intCodeExecutor = new Executor(input);

      var actual = intCodeExecutor.Execute();

      CollectionAssert.AreEqual(actual, expected);
    }
  }
}
