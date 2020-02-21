using FeedbackAmplifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Part1Test
{
  [TestClass]
  public class PartITest
  {
    [TestMethod]
    public void TC1()
    {
      var program = new long[] { 3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0 };
      var sequence = new int[] { 4, 3, 2, 1, 0 };
      int expected = 43210;

      var actual = Helpers.GetHighest(program, sequence);

      Assert.AreEqual(expected, actual, "OK!");
    }
    
    [TestMethod]
    public void TC2()
    {
      var program = new long[] { 3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0 };
      var sequence = new int[] { 0, 1, 2, 3, 4 };
      int expected = 54321;

      var actual = Helpers.GetHighest(program, sequence);

      Assert.AreEqual(expected, actual, "OK!");
    }
          
    [TestMethod]
    public void TC3()
    {
      var program = new long[] { 3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0};
      var sequence = new int[] { 1, 0, 4, 3, 2 };
      int expected = 65210;

      var actual = Helpers.GetHighest(program, sequence);

      Assert.AreEqual(expected, actual, "OK!");
    }
  }
}

