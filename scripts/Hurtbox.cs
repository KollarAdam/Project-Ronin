using Godot;
using System;

[GlobalClass]
public partial class Hurtbox : Area2D
{
    public override void _Ready()
    {
        AreaEntered += _OnAreaEntered;
    }

	private void _OnAreaEntered(Area2D area)
	{
        Hitbox hitbox = area as Hitbox;
        GD.Print($"You hit me, ouch! I took {hitbox.damage} damage");
	}


}
