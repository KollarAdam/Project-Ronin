using Godot;
using System;

public partial class EnemyChase : EnemyState
{
    Player player;
    private float _moveDir;
    bool posMoveDir;
    public override void _Ready()
    {
        base._Ready();
        player = Player.Instance;
    }
    public override void PhysicsProcess(double delta)
    {
        _moveDir = Math.Clamp(player.GlobalPosition.X - enemy.GlobalPosition.X, -1, 1);
        posMoveDir = _moveDir > 0;
        if (enemy.siteLine.HasOverlappingAreas())
        {
            var velocity = enemy.Velocity;
            velocity.X = enemy.movement.AccelerateHorizontally(_moveDir, velocity.X, delta, enemy.IsOnFloor());
            enemy.Velocity = velocity;
        }
        else
        {
            enemy.Velocity = Vector2.Zero;
            StateChanged?.Invoke(this, "ENEMYIDLE");
        }
    }

}
