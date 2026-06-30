using Godot;
using System;
[GlobalClass]
public partial class AttackRange : Area2D
{
    public Action PlayerInRange;

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

        Hurtbox hurtbox = area as Hurtbox;
        if (hurtbox is Hurtbox)
        {
            GD.Print("PLayer entered attack range");
            PlayerInRange?.Invoke();
        }

    }
}
