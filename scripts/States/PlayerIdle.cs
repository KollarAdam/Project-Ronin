using Godot;
using System;
[GlobalClass]
public partial class PlayerIdle : PlayerState
{
    public override void Enter()
    {
        GD.Print("Entered IDLE state");
        player.movement.ResetJumps(player.IsOnFloor());
    }
    public override void Exit()
    {
        GD.Print("Exited IDLE state");
    }
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
            velocity.X = player.movement.ApplyFriction(velocity.X, delta, player.IsOnFloor());
        }
        else
        {
            StateChanged?.Invoke(this, "PLAYERMOVE");
        }
        
        player.Velocity = velocity;
    }
}
