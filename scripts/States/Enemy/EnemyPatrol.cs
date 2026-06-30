using Godot;
using System;

public partial class EnemyPatrol : EnemyState
{
    private float _patrolSpeed = 10f;
    private float _patrolDirection = -1f; //Positive value: goes right, Negative value: goes left 
    public override void Enter()
    {
       
    }
    public override void Exit()
    {
        _patrolDirection *= -1; //At the end of patrol: changes direction
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void Process(double delta)
    {

    }
    public override void PhysicsProcess(double delta)
    {
        var velocity = enemy.Velocity;
        if (enemy.mapEdge.IsColliding())
        {
            velocity.X = enemy.movement.AccelerateHorizontally(_patrolDirection, velocity.X, delta, enemy.IsOnFloor());
            enemy.Velocity = velocity;
        }
        else
        {
            enemy.Velocity = Vector2.Zero;
            StateChanged?.Invoke(this, "ENEMYIDLE");
        }
    }
}
