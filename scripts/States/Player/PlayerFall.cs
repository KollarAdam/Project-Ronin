using Godot;
using System;
[GlobalClass]
public partial class PlayerFall : PlayerState
{
    public override void Enter()
    {
    }
    public override void Exit()
    {
    }
    public override void PhysicsProcess(double delta)
    {
        velocity = player.Velocity;
        player.movement.CoyoteTime -= (float)delta;
        velocity.X = player.movement.AccelerateHorizontally(player.input.Direction, velocity.X, delta, player.IsOnFloor());
        velocity.Y = player.movement.ApplyGravity(velocity.Y, delta, player.IsOnFloor());
        player.Velocity = velocity;
        if (player.IsOnFloor())
        {
            if (player.input.Direction == 0)
            {
                StateChanged?.Invoke(this, "PLAYERIDLE");
            }
            else
            {
                StateChanged?.Invoke(this, "PLAYERMOVE");
            }
        }
        else
        {
            if (player.input.Jump)
            {
                if (player.movement.CoyoteTime > 0)
                {
                    StateChanged?.Invoke(this, "PLAYERJUMP");
                }
                else
                {
                    if (player.movement.remainingJumps > 0)
                    {
                        StateChanged?.Invoke(this, "PLAYERDOUBLEJUMP");
                    }
                }
            }
            if (player.movement.IsWallHanging(player.input.Direction, player.IsOnWallOnly(), player.IsOnFloor(), player.GetWallNormal()))
            {
                StateChanged?.Invoke(this, "PLAYERWALLHANG");
            }
        }
    }
}
