using Godot;
using System;

public partial class PlayerDoubleJump : PlayerState
{
    public override void Enter()
    {
    }
    public override void Exit()
    {
    }

    public override void Process(double delta)
    {
        
    }

    public override void PhysicsProcess(double delta)
    {
        velocity = player.Velocity;
		player.movement.CoyoteTime -= (float)delta;
		velocity.X = player.movement.AccelerateHorizontally(player.input.Direction, velocity.X, delta, player.IsOnFloor());
		velocity.Y = player.movement.ApplyDoubleJump(player.Velocity.Y, player.IsOnFloor());
		player.Velocity = velocity;
        if(velocity.Y >= 0){
			StateChanged?.Invoke(this, "PLAYERFALL");
		}
    }


}
