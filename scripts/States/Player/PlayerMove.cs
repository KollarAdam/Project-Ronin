using Godot;
using System;

[GlobalClass]
public partial class PlayerMove : PlayerState
{
	public override void Enter()
	{
		player.movement.ResetJumps();
		player.movement.ResetCoyoteFrames();
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
		if (player.IsOnFloor())
		{
			if (player.input.Jump)
			{
				StateChanged?.Invoke(this, "PLAYERJUMP");
			}
		}
		else
		{
			StateChanged?.Invoke(this, "PLAYERFALL");
		}

		if (player.input.Direction == 0)
		{
			StateChanged?.Invoke(this, "PLAYERIDLE");
		}
		else
		{
			velocity.X = player.movement.AccelerateHorizontally(player.input.Direction, velocity.X, delta, player.IsOnFloor());
		}


		player.Velocity = velocity;
	}
}
