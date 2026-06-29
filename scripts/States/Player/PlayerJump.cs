using Godot;
using System;
[GlobalClass]
public partial class PlayerJump : PlayerState
{
	public override void Enter()
	{

	}
	public override void Exit()
	{

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void Process(double delta)
	{
	}
	public override void PhysicsProcess(double delta)
	{
		velocity = player.Velocity;
		player.movement.CoyoteTime -= (float)delta;
		velocity.X = player.movement.AccelerateHorizontally(player.input.Direction, velocity.X, delta, player.IsOnFloor());
		velocity.Y = player.movement.ApplyJump(player.Velocity.Y, player.IsOnFloor());
		velocity.Y = player.movement.ApplyGravity(velocity.Y, delta, player.IsOnFloor());
		player.Velocity = velocity;
		if (velocity.Y >= 0)
		{
			StateChanged?.Invoke(this, "PLAYERFALL");
		}
		if (player.input.Jump)
		{
			StateChanged?.Invoke(this, "PLAYERDOUBLEJUMP");
		}
	}

}
