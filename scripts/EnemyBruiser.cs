using Godot;
using System;

public partial class EnemyBruiser : Entity
{
    [Export] public MovementComponent movement;
    [Export] public Attack attack;
    [Export] private AttackRange _range;
    [Export] public bool canPatrol = true;
    public RayCast2D mapEdge;
    public RayCast2D wallCollision;
    public float moveDir;
    public Node2D anchor;
    private float _attackDelay = 0f;
    private const float _ATTACKDELAYDEFAULTVALUE = 1f;
    private bool _isAttacking = false;
    public override void _Ready()
    {
        anchor = GetNode<Node2D>("Anchor");
        mapEdge = GetNode<RayCast2D>("Anchor/CheckMapEdge");
        wallCollision = GetNode<RayCast2D>("Anchor/CheckWall");
        _range.PlayerInRange += OnPLayerInRange;
        _range.PlayerOutOfRange += OnPLayerOutOfRange;
    }
    public override void _Process(double delta)
    {
        if(_attackDelay < 0 && _isAttacking)
        {
            attack._ApplyAttack("Bruiser_Attack");
            _attackDelay = _ATTACKDELAYDEFAULTVALUE;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _attackDelay -= (float)delta;
        var velocity = Velocity;
        velocity.Y = movement.ApplyGravity(velocity.Y, delta, IsOnFloor());
        moveDir = Velocity.X;
        Velocity = velocity;
        MoveAndSlide();
    }
    public override void _ExitTree()
    {
        _range.PlayerInRange -= OnPLayerInRange;
        _range.PlayerOutOfRange -= OnPLayerOutOfRange;
    }

    public override void TakeDamage(int dmg)
    {
        GD.Print($"I'm John Goon and I took {dmg} damage");
    }
    private void OnPLayerInRange()
    {
        _isAttacking = true;
    }
    private void OnPLayerOutOfRange()
    {
        _isAttacking = false;
    }
}
