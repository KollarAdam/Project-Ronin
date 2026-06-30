using Godot;
using System;

public partial class EnemyIdle : EnemyState
{
    private const float _DEFAULTIDLETIMER = 3f;
    private float _idleTimer;
    public override void Enter()
    {
        _ResetIdleTimer();
    }
    public override void Exit()
    {
       
    }
    public override void Process(double delta)
    {
        _idleTimer -= (float)delta;
        if(_idleTimer <= 0 && enemy.canPatrol)
        {
            StateChanged?.Invoke(this,"ENEMYPATROL");
        }
    }

    private void _ResetIdleTimer()
    {
        _idleTimer = _DEFAULTIDLETIMER;
    }
}
