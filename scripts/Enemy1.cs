using Godot;
using System;

[GlobalClass]
public partial class Enemy1 : Entity
{
	[Export] private int _hp = 1;

	[Export] private AudioStreamPlayer2D audio;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

    public override void _Process(double delta)
    {
        _DestroyObject();
    }

	public override void TakeDamage(int dmg)
	{
		_hp -= dmg;
		audio.Play();
		// GD.Print(_hp);
	}

	private void _DestroyObject()
	{
		if(_hp < 1)
		{
			QueueFree();
		}
	}
}
