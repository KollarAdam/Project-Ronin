using Godot;
using System;

public partial class Attack : Node2D
{
    [Export] private InputComponent _input;
    [Export] private int _damage = 1;
    [Export] private float _attackSpeed = 1f;
    private AnimationPlayer _attackAnim;
    private Player _player;
    public override void _Ready()
    {
        _attackAnim = GetNode<AnimationPlayer>("AnimationPlayerSwing");
        _player = GetOwner<Player>();
    }
    public override void _Process(double delta)
    {
        _attackAnim.SpeedScale = _attackSpeed;
        _AttackDir();
        if (_input.Attack)
        {
            _attackAnim.Play("Attack"); 
        }
    }
    private void _AttackDir()
    {
        if (_input.Up)
        {
            RotationDegrees = -90f;
        }
        else if (_input.Down && !_player.IsOnFloor())
        {
            RotationDegrees = 90f;
        }
        else
        {
            RotationDegrees = 0f;
        }
    }
}
