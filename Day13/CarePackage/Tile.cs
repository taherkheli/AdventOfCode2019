namespace CarePackage
{
  public class Tile
  {
    private int _x;
    private int _y;
    private TileId _tileId;

    public Tile(int x, int y, TileId tileId)
    {
      _x = x;
      _y = y;
      _tileId = tileId;
    }

    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
    public TileId TileId { get => _tileId; set => _tileId = value; }
  }
}
