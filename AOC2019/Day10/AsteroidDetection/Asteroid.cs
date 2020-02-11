namespace AsteroidDetection
{
  public class Asteroid
  {
    private Point _p;

    public Asteroid(Point p)
    {
      _p = p;
    }

    public Point Position{ get => _p;  }
  }
}
