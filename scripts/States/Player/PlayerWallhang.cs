using Godot;
using System;
[GlobalClass]
public partial class PlayerWallhang : PlayerState
{
    public override void Enter()
    {
		player.Velocity = Vector2.Zero;
		player.movement.ResetCoyoteFrames();
    }
    public override void Exit()
    {
        player.movement.ResetWallhangValues();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void Process(double delta)
	{
	}
	public override void PhysicsProcess(double delta)
	{
		velocity = player.Velocity;
		if(player.movement.IsWallHanging(player.input.Direction, player.IsOnWallOnly(), player.IsOnFloor(), player.GetWallNormal()))
		{
			velocity.Y = player.movement.WallHang(velocity.Y, delta);
			if (player.input.Jump)
			{
				StateChanged?.Invoke(this,"PLAYERJUMP");
			}
		}
		else
		{
			StateChanged?.Invoke(this, "PLAYERFALL");
		}
		player.Velocity = velocity;
	}
}
