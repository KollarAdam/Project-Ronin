using Godot;
using System;

public partial class EnemyPatrol : EnemyState
{
    private float _patrolSpeed = 10f;
    private float _patrolDirection = -1f; //Positive value: goes right, Negative value: goes left 
    private bool _shouldTurnaround = false;
    public override void Enter()
    {
        if (_shouldTurnaround)
        {
            Vector2 anchorScale = enemy.anchor.Scale;
            anchorScale.X *= -1;
            enemy.anchor.Scale = anchorScale;
        }
    }
    public override void Exit()
    {
        _patrolDirection *= -1; //At the end of patrol: changes direction
        _shouldTurnaround = true;
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void Process(double delta)
    {

    }
    public override void PhysicsProcess(double delta)
    {
        var velocity = enemy.Velocity;
        if (enemy.mapEdge.IsColliding() && !enemy.wallCollision.IsColliding())
        {
            velocity.X = enemy.movement.AccelerateHorizontally(_patrolDirection, velocity.X, delta, enemy.IsOnFloor());
            enemy.Velocity = velocity;
        }
        else
        {
            enemy.Velocity = Vector2.Zero;
            StateChanged?.Invoke(this, "ENEMYIDLE");
        }
        if (enemy.siteLine.HasOverlappingAreas())
        {
            StateChanged?.Invoke(this,"ENEMYCHASE");
        }
    }
}
