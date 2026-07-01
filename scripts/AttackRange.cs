using Godot;
using System;
[GlobalClass]
public partial class AttackRange : Area2D
{
    public Action PlayerInRange;
    public Action PlayerOutOfRange;

    public override void _Ready()
    {
        AreaEntered += _OnAreaEntered;
        AreaExited += _OnAreaExited;
    }
    public override void _ExitTree()
    {
        AreaEntered -= _OnAreaEntered;
        AreaExited -= _OnAreaExited;
    }

    private void _OnAreaEntered(Area2D area)
    {
        Hurtbox hurtbox = area as Hurtbox;
        if (hurtbox is Hurtbox)
        {
            // GD.Print("Player entered attack range");
            PlayerInRange?.Invoke();
        }
    }
    private void _OnAreaExited(Area2D area)
    {
        Hurtbox hurtbox = area as Hurtbox;
        if (hurtbox is Hurtbox)
        {
            // GD.Print("Player exited attack range");
            PlayerOutOfRange?.Invoke();
        }
    }
}
