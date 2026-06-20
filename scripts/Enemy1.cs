using Godot;
using System;


public partial class Enemy1 : Node2D
{
	[Export] private int _hp = 1;
	public Hurtbox hurtbox;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		hurtbox = GetNode<Hurtbox>("Hurtbox");
		hurtbox.TakeDamage += _OnDamageTaken;
	}
    public override void _ExitTree()
    {
        hurtbox.TakeDamage -= _OnDamageTaken;
    }
    public override void _Process(double delta)
    {
        _DestroyObject();
    }

	private void _OnDamageTaken(int dmg)
	{
		_hp -= dmg;
		GD.Print(_hp);
	}

	private void _DestroyObject()
	{
		if(_hp < 1)
		{
			QueueFree();
		}
	}
}
