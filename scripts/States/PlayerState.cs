using Godot;
using System;
[GlobalClass]
public partial class PlayerState : Node
{
	public Player player;
	public Vector2 velocity;
	public Action<PlayerState,string> StateChanged;
    public override void _Ready()
    {
        player = GetOwner<Player>();
    }

    public virtual void Enter()
	{
		
	}
	public virtual void Exit()
	{
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public virtual void Process(double delta)
	{
	}
    public virtual void PhysicsProcess(double delta)
    {
        
    }

}
