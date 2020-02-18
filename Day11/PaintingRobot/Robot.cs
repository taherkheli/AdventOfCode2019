using IntCode;
using System;
using System.Collections.Generic;
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
  }
}
