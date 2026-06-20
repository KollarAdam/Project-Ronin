using Godot;
using System;
[GlobalClass]
public partial class PlayerState : Node
{
	public Player player;
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
	public override void _Process(double delta)
	{
	}
    public override void _PhysicsProcess(double delta)
    {
        
    }

}
