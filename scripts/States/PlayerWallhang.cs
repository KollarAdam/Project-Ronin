using Godot;
using System;
[GlobalClass]
public partial class PlayerWallhang : PlayerState
{
	private float _wallSlide = 200f;
	private float _hangGracePeriod = 1f;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = player.Velocity;
		bool isOnFloor = player.IsOnFloor();
		bool isOnWallOnly = player.IsOnWallOnly();

		if (_IsWallHanging(player.input.Direction))
		{
			_hangGracePeriod -= (float)delta;
			velocity.Y = 0f;
			if (_hangGracePeriod <= 0f)
			{
				velocity.Y += (float)(_wallSlide * delta);
				if (_wallSlide <= 2000f) _wallSlide *= 1.02f;
			}
			if (player.input.Jump)
			{
				velocity.Y = player.movement.ApplyJump(velocity.Y, isOnFloor, isOnWallOnly);
				velocity.X = player.GetWallNormal().X * player.movement.JumpStrength / 2;
			}
			player.Velocity = velocity;
		}
		else
		{
			_wallSlide = 200f;
			_hangGracePeriod = 1f;
		}
	}
	private bool _IsWallHanging(float input){ return player.IsOnWallOnly() && (input == -player.GetWallNormal().X);}
}
