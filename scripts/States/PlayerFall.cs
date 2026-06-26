using Godot;
using System;
[GlobalClass]
public partial class PlayerFall : PlayerState
{
    private float coyoteFrames = 0f;
    public override void Enter()
	{
		GD.Print("Entered FALL state");
	}
	public override void Exit()
    {
        GD.Print("Exited FALL state");
    }
    public override void PhysicsProcess(double delta)
    {
        velocity = player.Velocity;
        coyoteFrames -= (float)delta;
        velocity.X = player.movement.AccelerateHorizontally(player.input.Direction, velocity.X ,delta, player.IsOnFloor());
        player.Velocity = velocity;
        if (player.IsOnFloor())
        {
            if(player.input.Direction == 0){
                StateChanged.Invoke(this,"PLAYERIDLE");}
            else{
                StateChanged.Invoke(this,"PLAYERMOVE");}
        }
        else
        {
            if(player.input.Jump && player.movement.remainingJumps > 0)
            {
                StateChanged.Invoke(this,"PLAYERJUMP");
            }
        }
    }
}
