using Godot;
using System;

public partial class Attack : Node2D
{
    [Export] private InputComponent _attack;
    [Export] private int _damage = 1;
    private AnimationPlayer _attackAnim;

    public override void _Ready()
    {
        _attackAnim = GetNode<AnimationPlayer>("AnimationPlayerSwing");
    }

    public override void _Process(double delta)
    {
        LookAt(GetGlobalMousePosition());
        if (_attack.Attack)
        {
            _attackAnim.Play("Attack");
        }
    }


}
