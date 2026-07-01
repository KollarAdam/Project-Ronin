using Godot;
using System;

public partial class EnemyState : GenericState
{
    public EnemyBruiser enemy;
    public static Action TurnAround;
    public override void _Ready()
    {
        enemy = GetOwner<EnemyBruiser>();
    }
}
