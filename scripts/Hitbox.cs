using Godot;
using System;

[GlobalClass]
public partial class Hitbox : Area2D
{
	[Export] private Attack _attackDamage;
	public int Damage{ get {return _attackDamage.Damage;}}
	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		
	}
}
