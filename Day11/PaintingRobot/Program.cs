using IntCode;
using System;
using System.IO;

namespace PaintingRobot
{
  class Program
  {
    static void Main()
    {
      string path = "input.txt";
      Robot robot = new Robot(LoadInput(path), 10);
      robot.Executor.InputQueue.Enqueue(0);
      robot.Executor.Execute();

      Colors color = Colors.Black;
      Directions direction = Directions.Up;

      if ((long)robot.Executor.OutputQueue.Dequeue() == 1)
        color = Colors.White;

      if ((long)robot.Executor.OutputQueue.Dequeue() == 0)
        direction = Directions.Left;
      else
        direction = Directions.Right;

      robot.Executor.InputQueue.Enqueue(0);
      robot.Executor.ResumeExecution();

      if ((long)robot.Executor.OutputQueue.Dequeue() == 1)
        color = Colors.White;

      if ((long)robot.Executor.OutputQueue.Dequeue() == 0)
        direction = Directions.Left;
      else
        direction = Directions.Right;

    }

    private static long[] LoadInput(string path)
    {
      StreamReader file = new StreamReader(path);
      string[] strings = file.ReadToEnd().Split(',');
      long[] result = new long[strings.Length];

      for (int i = 0; i < result.Length; i++)
        result[i] = long.Parse(strings[i]);

      return result;
    }
  }
}
