using System;

namespace N_BodyProblem
{
  public class Moon
  {
    private Position _position;
    private Velocity _velocity;

    public Moon()
    {
      _velocity = new Velocity(0, 0, 0);
    }

    public string Name { get; set; }
    public Position Position { get => _position; set => _position = value; }
    public Velocity Velocity { get => _velocity; set => _velocity = value; }

    public void ApplyGravity(Moon moon)
    {
      if (_position.X < moon.Position.X)
        _velocity.X++;      
      else if (_position.X > moon.Position.X)
        _velocity.X--;

      if (_position.Y < moon.Position.Y)
        _velocity.Y++;
      else if (_position.Y > moon.Position.Y)
        _velocity.Y--;

      if (_position.Z < moon.Position.Z)
        _velocity.Z++;
      else if (_position.Z > moon.Position.Z)
        _velocity.Z--;
    }

    public void ApplyGravityX(int x)
    {
      if (_position.X < x)
        _velocity.X++;
      else if (_position.X > x)
        _velocity.X--;      
    }

    public void ApplyGravityY(int y)
    {
      if (_position.Y < y)
        _velocity.Y++;
      else if (_position.Y > y)
        _velocity.Y--;
    }

    public void ApplyGravityZ(int z)
    {
      if (_position.Z < z)
        _velocity.Z++;
      else if (_position.Z > z)
        _velocity.Z--;
    }

    public void ApplyVelocity() 
    {
      _position = new Position(_position.X + _velocity.X, _position.Y + _velocity.Y, _position.Z + _velocity.Z);
    }

    public void ApplyVelocityX()
    {
      _position.X = _position.X + _velocity.X;
    }

    public void ApplyVelocityY()
    {
      _position.Y = _position.Y + _velocity.Y;
    }

    public void ApplyVelocityZ()
    {
      _position.Z = _position.Z + _velocity.Z;
    }

    public int GetTotalEnergy()
    {
      int pot = Math.Abs(_position.X) + Math.Abs(_position.Y) + Math.Abs(_position.Z);
      int kin = Math.Abs(_velocity.X) + Math.Abs(_velocity.Y) + Math.Abs(_velocity.Z);

      return pot * kin;
    }
  }
}
