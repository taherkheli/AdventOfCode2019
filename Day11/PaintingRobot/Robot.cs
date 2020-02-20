using IntCode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PaintingRobot
{
  public class Robot
  {
    private readonly Grid _grid;
    private readonly Executor _executor;

    public Robot(long[] intCode, int size) 
    {
      _grid = new Grid(size);
      _executor = new Executor(intCode);
      _executor.Initialize();
    }
    
    public Executor Executor => _executor;

    public Grid Grid => _grid;

    public void Go()
    {
      Colors color;
      Directions direction;
      long out1, out2;

      int count=0;

      while (true)
      {        
        Console.WriteLine("Loop #: {0}", ++count);

        if (_grid.CurrentPanel.Color == Colors.Black)
          _executor.InputQueue.Enqueue(0);
        else  //White 
          _executor.InputQueue.Enqueue(1);

        if (_executor.AwaitingInput)
          _executor.ResumeExecution();
        else
          _executor.Execute();

        out1 = (long)_executor.OutputQueue.Dequeue();
        out2 = (long)_executor.OutputQueue.Dequeue();

        if (_executor.AwaitingInput == true)  //intCode has not exited yet 
        {
          //Paint
          if (out1 == 0)
            color = Colors.Black;
          else  //1
            color = Colors.White;

          _grid.CurrentPanel.Paint(color);

          //Move
          if (out2 == 0)
            direction = Directions.Left;
          else
            direction = Directions.Right;

          _grid.Move(direction);

        }
        else
          break;       
      }
    }
  }
}
