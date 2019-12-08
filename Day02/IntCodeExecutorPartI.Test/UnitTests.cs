using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntCodeExecutorPartI.Test
{
  [TestClass]
  public class UnitTests
  {
    [TestMethod]
    public void Test1()
    {
      int[] input = { 1, 0, 0, 0, 99 };
      int [] expected = { 2, 0, 0, 0, 99 };
      IntCodeExecutor intCodeExecutor = new IntCodeExecutor(input);

      var actual = intCodeExecutor.Execute();
           
      CollectionAssert.AreEqual(actual, expected);
    }

    [TestMethod]
    public void Test2()
    {
      int[] input = { 2, 3, 0, 3, 99 };
      int[] expected = { 2, 3, 0, 6, 99 };
      IntCodeExecutor intCodeExecutor = new IntCodeExecutor(input);

      var actual = intCodeExecutor.Execute();

      CollectionAssert.AreEqual(actual, expected);
    }

    [TestMethod]
    public void Test3()
    {
      int[] input = { 2, 4, 4, 5, 99, 0 };
      int[] expected = { 2, 4, 4, 5, 99, 9801 };
      IntCodeExecutor intCodeExecutor = new IntCodeExecutor(input);

      var actual = intCodeExecutor.Execute();

      CollectionAssert.AreEqual(actual, expected);
    }
    [TestMethod]
    public void Test4()
    {
      int[] input = { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
      int[] expected = { 30, 1, 1, 4, 2, 5, 6, 0, 99 };
      IntCodeExecutor intCodeExecutor = new IntCodeExecutor(input);

      var actual = intCodeExecutor.Execute();

      CollectionAssert.AreEqual(actual, expected);
    }
  }
}
