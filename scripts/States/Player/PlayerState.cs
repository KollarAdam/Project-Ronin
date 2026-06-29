using Godot;
using System;
[GlobalClass]
public partial class PlayerState : GenericState
{
	public Player player;
    public override void _Ready()
    {
        player = GetOwner<Player>();
    }
}
