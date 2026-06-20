using Godot;
using System;

public partial class Attack : Node2D
{
    [Export] private InputComponent _input;
    [Export] private int _damage = 1;
    private AnimationPlayer _attackAnim;
    private Player _player;
    public override void _Ready()
    {
        _attackAnim = GetNode<AnimationPlayer>("AnimationPlayerSwing");
        _player = GetOwner<Player>();
    }

    public override void _Process(double delta)
    {
        _AttackDir();
        if (_input.Attack)
        {
            _attackAnim.Play("Attack");
        }
    }

    private void _AttackDir()
    {
        if (Input.IsActionPressed("dir_up"))
        {
            RotationDegrees = -90f;
        }
        else if (Input.IsActionPressed("dir_down") && !_player.IsOnFloor())
        {
            RotationDegrees = 90f;
        }
        else
        {
            RotationDegrees = 0f;
        }
    }
}
