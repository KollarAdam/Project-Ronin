using Godot;
using System;

public partial class EnemyChase : EnemyState
{
    Player player;
    private float _moveDir;
    public override void _Ready()
    {
        base._Ready();
        player = Player.Instance;
    }
    public override void PhysicsProcess(double delta)
    {
        var velocity = enemy.Velocity;
        _moveDir = Math.Clamp(player.GlobalPosition.X - enemy.GlobalPosition.X, -1, 1);
        if (enemy.siteLine.HasOverlappingAreas())
        {
            velocity.X = enemy.movement.AccelerateHorizontally(_moveDir, velocity.X, delta, enemy.IsOnFloor());
            enemy.Velocity = velocity;
        }
        else
        {
            enemy.Velocity = Vector2.Zero;
            StateChanged?.Invoke(this, "ENEMYIDLE");
        }
        if (!enemy.mapEdge.IsColliding())
        {
            enemy.Velocity = Vector2.Zero;
        }
    }

}
