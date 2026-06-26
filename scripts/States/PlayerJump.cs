using Godot;
using System;
[GlobalClass]
public partial class PlayerJump : PlayerState
{
	public override void Enter()
	{
		GD.Print("Entered JUMP state");
	}
	public override void Exit()
    {
        GD.Print("Exited JUMP state");
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void Process(double delta)
	{
	}
    public override void PhysicsProcess(double delta)
    {
		velocity = player.Velocity;
		velocity.X = player.movement.AccelerateHorizontally(player.input.Direction, velocity.X, delta, player.IsOnFloor());
		velocity.Y = player.movement.ApplyJump(player.Velocity.Y, player.IsOnFloor());
		player.Velocity = velocity;
		StateChanged?.Invoke(this, "PLAYERFALL");
    }

}
