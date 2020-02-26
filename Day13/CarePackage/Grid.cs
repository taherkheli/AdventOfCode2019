using System.Collections.Generic;

namespace CarePackage
{
  public class Grid
  {    
    private readonly List<Tile> _tiles;
    
    public Grid()
    {
      _tiles = new List<Tile>();
    }

    public List<Tile> Tiles => _tiles;
  }
}