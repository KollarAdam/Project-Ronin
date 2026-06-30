using Godot;
using System;
[GlobalClass]
public partial class Attack : Node2D
{
    [Export] private AnimationPlayer _attackAnim;
    [Export] private Area2D _range;
    [Export] private int _damage = 1;
    [Export] private float _attackSpeed = 1f;
    public int Damage
    {
        get { return _damage; }
    }
    public void _AttackDir(bool upInput, bool downInput, bool isOnFloor)
    {
        if (upInput)
        {
            RotationDegrees = -90f;
        }
        else if (downInput && !isOnFloor)
        {
            RotationDegrees = 90f;
        }
        else
        {
            RotationDegrees = 0f;
        }
    }
    public void _ApplyAttack(string anim, float atkSpeed = 1f)
    {
        _attackAnim.SpeedScale = atkSpeed;
        _attackAnim.Play(anim);
    }
    
}
