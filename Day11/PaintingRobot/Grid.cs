using IntCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingRobot
{
  public class Grid
  {
    private readonly int _rows;
    private readonly int _columns;
    private readonly List<Panel> _panels;

    public int Rows => _rows;
    public int Columns => _columns;
    public List<Panel> Panels => _panels;

    public Grid(int size)
    {
      if ((size > 0) && (size < 201))
      {
        _rows = size;
        _columns = size;
        _panels = new List<Panel>() { };
        //initialize();
      }
    }




  }
}
