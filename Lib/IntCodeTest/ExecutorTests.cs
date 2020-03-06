using IntCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntCodeTest
{
  [TestClass]
  public class ExecutorTests
  {
    [TestMethod]
    public void D5_TC1_InputIsEqualTo8_Reference()
    {
      var intCode = new long[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };
      var executor = new Executor(intCode);
      long expected1 = 0;
      long expected2 = 1;
      long expected3 = 0;

      executor.InputQueue.Enqueue(7);
      executor.Execute();
      var actual1 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(8);
      executor.Execute();
      var actual2 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(15);
      executor.Execute();
      var actual3 = executor.OutputQueue.Dequeue();

      Assert.AreEqual<long>(expected1, actual1, "OK!");
      Assert.AreEqual<long>(expected2, actual2, "OK!");
      Assert.AreEqual<long>(expected3, actual3, "OK!");
    }

    [TestMethod]
    public void D5_TC2_InputIsEqualTo8_Value()
    {
      var intCode = new long[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 };
      var executor = new Executor(intCode);
      long expected1 = 0;
      long expected2 = 1;
      long expected3 = 0;

      executor.InputQueue.Enqueue(3);
      executor.Execute();
      var actual1 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(8);
      executor.Execute();
      var actual2 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(9);
      executor.Execute();
      var actual3 = executor.OutputQueue.Dequeue();

      Assert.AreEqual<long>(expected1, actual1, "OK!");
      Assert.AreEqual<long>(expected2, actual2, "OK!");
      Assert.AreEqual<long>(expected3, actual3, "OK!");
    }
    
    [TestMethod]
    public void D5_TC3_InputIsLessThan8_Reference()
    {
      var intCode = new long[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 };
      var executor = new Executor(intCode);
      long expected1 = 1;
      long expected2 = 0;
      long expected3 = 0;

      executor.InputQueue.Enqueue(7);
      executor.Execute();
      var actual1 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(8);
      executor.Execute();
      var actual2 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(11);
      executor.Execute();
      var actual3 = executor.OutputQueue.Dequeue();

      Assert.AreEqual<long>(expected1, actual1, "OK!");
      Assert.AreEqual<long>(expected2, actual2, "OK!");
      Assert.AreEqual<long>(expected3, actual3, "OK!");
    }

    [TestMethod]
    public void D5_TC4_InputIsLessThan8_Value()
    {
      var intCode = new long[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 };
      var executor = new Executor(intCode);
      long expected1 = 1;
      long expected2 = 0;
      long expected3 = 0;

      executor.InputQueue.Enqueue(7);
      executor.Execute();
      var actual1 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(8);
      executor.Execute();
      var actual2 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(11);
      executor.Execute();
      var actual3 = executor.OutputQueue.Dequeue();

      Assert.AreEqual<long>(expected1, actual1, "OK!");
      Assert.AreEqual<long>(expected2, actual2, "OK!");
      Assert.AreEqual<long>(expected3, actual3, "OK!");
    }

    [TestMethod]
    public void D5_TC5_InputIsZero_JumpIfFalse_Reference()
    {
      var intCode = new long[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 };
      var executor = new Executor(intCode);
      long expected1 = 0;
      long expected2 = 1;
      long expected3 = 1;

      executor.InputQueue.Enqueue(0);
      executor.Execute();
      var actual1 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(1);
      executor.Execute();
      var actual2 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(9);
      executor.Execute();
      var actual3 = executor.OutputQueue.Dequeue();

      Assert.AreEqual<long>(expected1, actual1, "OK!");
      Assert.AreEqual<long>(expected2, actual2, "OK!");
      Assert.AreEqual<long>(expected3, actual3, "OK!");
    }

    [TestMethod]
    public void D5_TC6_InputIsZero_JumpIfTrue_Value()
    {
      var intCode = new long[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 };
      var executor = new Executor(intCode);
      long expected1 = 1;
      long expected2 = 0;
      long expected3 = 1;

      executor.InputQueue.Enqueue(-1);
      executor.Execute();
      var actual1 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(0);
      executor.Execute();
      var actual2 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(8);
      executor.Execute();
      var actual3 = executor.OutputQueue.Dequeue();

      Assert.AreEqual<long>(expected1, actual1, "OK!");
      Assert.AreEqual<long>(expected2, actual2, "OK!");
      Assert.AreEqual<long>(expected3, actual3, "OK!");
    }

    [TestMethod]
    public void D5_TC7_ComparisonWith8_Reference()
    {
      var intCode = new long[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31, 
                                 1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104, 
                                 999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99};
      var executor = new Executor(intCode);
      long expected1 = 999;
      long expected2 = 1000;
      long expected3 = 1001;

      executor.InputQueue.Enqueue(-3);
      executor.Execute();
      var actual1 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(8);
      executor.Execute();
      var actual2 = executor.OutputQueue.Dequeue();
      executor.Initialize();
      executor.InputQueue.Enqueue(15);
      executor.Execute();
      var actual3 = executor.OutputQueue.Dequeue();

      Assert.AreEqual<long>(expected1, actual1, "OK!");
      Assert.AreEqual<long>(expected2, actual2, "OK!");
      Assert.AreEqual<long>(expected3, actual3, "OK!");
    }

    [TestMethod]
    public void D9_TC1()
    {
      var intCode = new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
      var expected = new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };

      var actual = (new Executor(intCode)).Execute();

      CollectionAssert.AreEqual(expected, actual, "OK!");
    }

    [TestMethod]
    public void D9_TC2_ShouldProduce_16_DigitNumber()
    {
      var intCode = new long[] { 1102, 34915192, 34915192, 7, 4, 7, 99, 0 };
      long expected = 1219070632396864;
      var executor = new Executor(intCode);
      executor.Execute();
      var actual = executor.OutputQueue.Dequeue();
      Assert.AreEqual<long>(expected, actual, "OK!");
    }

    [TestMethod]
    public void D9_TC3_ShouldOutputLargeNumber()
    {
      var intCode = new long[] { 104, 1125899906842624, 99 };
      long expected = 1125899906842624;   
      var executor = new Executor(intCode);
      executor.Execute();
      var actual = executor.OutputQueue.Dequeue();
      Assert.AreEqual<long>(expected, actual, "OK!");
    }
  }
}