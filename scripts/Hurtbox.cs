using Godot;
using System;

[GlobalClass]
public partial class Hurtbox : Area2D
{
    [Export] private Entity _entity;
    public Action<int> TakeDamage;

    public override void _Ready()
    {
        AreaEntered += _OnAreaEntered;
    }
    public override void _ExitTree()
    {
        AreaEntered -= _OnAreaEntered;
    }

    private void _OnAreaEntered(Area2D area)
    {
        Hitbox hitbox = area as Hitbox;
        if (hitbox is Hitbox)
        {
            _entity.TakeDamage(hitbox.Damage);   
        }
    }


}
