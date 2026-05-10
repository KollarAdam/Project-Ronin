using Godot;
using System;

[GlobalClass]
public partial class Hurtbox : Area2D
{
    private CharacterBody2D _player;
    public override void _Ready()
    {
        AreaEntered += _OnAreaEntered;
    }

	private void _OnAreaEntered(Area2D area)
	{
        Hitbox hitbox = area as Hitbox;

	}


}
