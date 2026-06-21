using Godot;
using System;
[GlobalClass]
public partial class PlayerMove : PlayerState
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
		
		player.coyoteTime -= (float)delta;
		player.movement.CoyoteTime = player.coyoteTime;

		velocity.Y = player.movement.ApplyGravity(velocity.Y, delta, isOnFloor);

		player.movement.ResetJumps(player.IsOnFloor());

		if (player.input.Jump)
		{
			velocity.Y = player.movement.ApplyJump(velocity.Y, isOnFloor, isOnWallOnly);
		}
		if (player.input.Direction == 0)
		{
			velocity.X = player.movement.ApplyFriction(velocity.X, delta, isOnFloor);
		}
		else
		{
			velocity.X = player.movement.AccelerateHorizontally(player.input.Direction, velocity.X ,delta, isOnFloor);
		}

		var wasOnFloor = isOnFloor;

		player.Velocity = velocity;

		if (wasOnFloor && !player.IsOnFloor() && velocity.Y >= 0) player.coyoteTime = 0.1f;
	}

}
