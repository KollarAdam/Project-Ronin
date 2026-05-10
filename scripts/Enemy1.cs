using Godot;
using System;


public partial class Enemy1 : Node2D
{
	[Export] Stats stats;
	public Hurtbox hurtbox;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		hurtbox = GetNode<Hurtbox>("Hurtbox");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void OnDamageTaken()
	{

		GD.Print("I got hit, ouch.");
	}
}
