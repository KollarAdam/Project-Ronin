using Godot;
using System;
[GlobalClass]
public partial class Attack : Node2D
{
    [Export] private Entity _entity;
    [Export] private AnimationPlayer _attackAnim;
    [Export] private int _damage = 1;
    [Export] private float _attackSpeed = 1f;
    private Player _player;
    public int Damage
    {
        get { return _damage; }
    }
    public override void _Ready()
    {
        if (_entity is Player)
        {
            _player = (Player)_entity;
        }
    }
    public override void _Process(double delta)
    {
        if (_player != null)
        {
            _attackAnim.SpeedScale = _attackSpeed;
            _AttackDir();
            if (_player.input.Attack)
            {
                _attackAnim.Play("Attack");
            }
        }
    }
    private void _AttackDir()
    {
        if (_player.input.Up)
        {
            RotationDegrees = -90f;
        }
        else if (_player.input.Down && !_player.IsOnFloor())
        {
            RotationDegrees = 90f;
        }
        else
        {
            RotationDegrees = 0f;
        }
    }
}
