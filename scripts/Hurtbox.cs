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
        EmitSignal("AreaEntered", hitbox);
	}


}
