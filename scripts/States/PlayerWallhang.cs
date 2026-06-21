using Godot;
using System;
[GlobalClass]
public partial class PlayerWallhang : PlayerState
{


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = player.Velocity;
		bool isOnFloor = player.IsOnFloor();
		bool isOnWallOnly = player.IsOnWallOnly();

		if (player.IsWallHanging(player.input.Direction))
		{
			player.hangGracePeriod -= (float)delta;
			velocity.Y = 0f;
			if (player.hangGracePeriod <= 0f)
			{
				velocity.Y += (float)(player.wallSlide * delta);
				if (player.wallSlide <= 2000f) player.wallSlide *= 1.02f;
			}
			;
			if (player.input.Jump)
			{
				velocity.Y = player.movement.ApplyJump(velocity.Y, isOnFloor, isOnWallOnly);
				velocity.X = player.GetWallNormal().X * player.movement.JumpStrength / 2;
			}
			player.Velocity = velocity;
		}
		else
		{
			player.wallSlide = 200f;
			player.hangGracePeriod = 1f;
		}
	}
}
