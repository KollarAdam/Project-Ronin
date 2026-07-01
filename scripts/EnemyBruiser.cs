using Godot;
using System;

public partial class EnemyBruiser : Entity
{
    [Export] private int _health = 1;
    [Export] public MovementComponent movement;
    [Export] public Attack attack;
    [Export] public Area2D siteLine;
    [Export] private AttackRange _range;
    [Export] public bool canPatrol = true;
    [Export] public bool isFlipped = false;
    private float _currentTime = 0f;
    public RayCast2D mapEdge;
    public RayCast2D wallCollision;
    public float moveDir;
    public Node2D anchor;
    private float _attackDelay = 0f;
    private const float _ATTACKDELAYDEFAULTVALUE = 1f;
    private bool _isAttacking = false;
    public override void _Ready()
    {
        EnemyState.TurnAround += OnPlayerTurn;
        anchor = GetNode<Node2D>("Anchor");
        mapEdge = GetNode<RayCast2D>("Anchor/CheckMapEdge");
        wallCollision = GetNode<RayCast2D>("Anchor/CheckWall");
        _range.PlayerInRange += OnPLayerInRange;
        _range.PlayerOutOfRange += OnPLayerOutOfRange;
        if (isFlipped)
        {
            Vector2 anchorScale = anchor.Scale;
            anchorScale.X *= -1;
            anchor.Scale = anchorScale;
        }
    }
    public override void _Process(double delta)
    {
        if (_attackDelay < 0 && _isAttacking)
        {
            attack._ApplyAttack("Bruiser_Attack");
            _attackDelay = _ATTACKDELAYDEFAULTVALUE;
        }
        if(_currentTime > 0)
		{
			_currentTime -= (float)delta;
		}
		else
		{
			anchor.Modulate = Colors.White;
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
        EnemyState.TurnAround -= OnPlayerTurn;
    }

    public override void TakeDamage(int dmg)
    {
        _health -= dmg;
		_currentTime = .1f;
		anchor.Modulate = Colors.Red;
        if (_health <= 0) Death();
        // GD.Print($"I'm John Goon and I took {dmg} damage");
    }
    private void Death()
    {
        QueueFree();
    }
    private void OnPLayerInRange()
    {
        _isAttacking = true;
    }
    private void OnPLayerOutOfRange()
    {
        _isAttacking = false;
    }
    private void OnPlayerTurn()
    {
        Vector2 anchorScale = anchor.Scale;
        anchorScale.X *= -1;
        anchor.Scale = anchorScale;
    }
}
