using Godot;
using System;

[GlobalClass]
public partial class GenericState : Node
{
    public Entity entity;
	public Vector2 velocity;
	public Action<GenericState,string> StateChanged;
    public override void _Ready()
    {
        entity = GetOwner<Entity>();
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
