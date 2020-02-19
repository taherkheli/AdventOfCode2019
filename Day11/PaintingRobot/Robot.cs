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
    private int _current;

    public Robot(long[] intCode, int size) 
    {
      _grid = new Grid(size);
      //initiliaze();
      _executor = new Executor(intCode);
      _executor.Initialize();
    }
    
    public Executor Executor => _executor;

    public void Go()
    {
      Colors color;
      Directions direction;

      _executor.InputQueue.Enqueue(0);
      _executor.Execute();

      while (true)
      {
        //Paint
        if ((long)_executor.OutputQueue.Dequeue() == 0)
          color = Colors.Black;
        else  //1
          color = Colors.White;

        _grid.CurrentPanel.Paint(color);  //mark as painted and paint the right color

        //Move
        if ((long)_executor.OutputQueue.Dequeue() == 0)
          direction = Directions.Left;
        else
          direction = Directions.Right;

        _grid.Move(direction);   //turn in the provided direction, move one step forward and update '_grid.CurrentPanel'

        //repeat and provide input
        if (_grid.CurrentPanel.Color == Colors.Black)
          _executor.InputQueue.Enqueue(0);
        else  //White
          _executor.InputQueue.Enqueue(1);

        _executor.ResumeExecution();

        if (_executor.AwaitingInput == false)
          break;       
      }
    }
  }
}
