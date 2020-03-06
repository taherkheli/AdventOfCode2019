namespace OxygenSystem
{
	public class Tile
	{
		private int _x;
		private int _y;
		private StatusCode _statusCode;

		public Tile(int x, int y, StatusCode statusCode)
		{
			_x = x;
			_y = y;
			_statusCode = StatusCode.Unknown;
		}

		public int X { get => _x; set => _x = value; }
		public int Y { get => _y; set => _y = value; }
		public StatusCode StatusCode { get => _statusCode; set => _statusCode = value; }
	}
}