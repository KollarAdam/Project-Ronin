using Godot;
using System;
[GlobalClass]
public partial class PlayerWallhang : PlayerState
{
	private float _wallSlide = 200f;
	private float _hangGracePeriod = 1f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void Process(double delta)
	{
	}
	public override void PhysicsProcess(double delta)
	{
		Vector2 velocity = player.Velocity;
		bool isOnFloor = player.IsOnFloor();
		bool isOnWallOnly = player.IsOnWallOnly();
		_hangGracePeriod -= (float)delta;
		velocity.Y = 0f;
		if (_hangGracePeriod <= 0f)
		{
			velocity.Y += (float)(_wallSlide * delta);
			if (_wallSlide <= 2000f) _wallSlide *= 1.02f;
		}
		if (player.input.Jump)
		{
			velocity.Y = player.movement.ApplyJump(velocity.Y, isOnFloor);
			velocity.X = player.GetWallNormal().X * player.movement.JumpStrength / 2;
		}
		player.Velocity = velocity;
	}
	private bool _IsWallHanging(float input) { return player.IsOnWallOnly() && (input == -player.GetWallNormal().X); }
}
