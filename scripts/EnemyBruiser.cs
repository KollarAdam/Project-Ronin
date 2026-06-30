using Godot;
using System;

public partial class EnemyBruiser : Entity
{
    [Export] public Attack attack;
    [Export] private AttackRange range;
    Godot.Label label;
    float h = 3f;
    public override void _Ready()
    {
        label = GetNode<Godot.Label>("Label");
        //Velocity = RandomizeWander();
        range.PlayerInRange += OnPLayerInRange;
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        // h -= (float)delta;
        // if (h < 0)
        // {
        //     Velocity += RandomizeWander();
        //     h = 3f;
        // }
        MoveAndSlide();
    }
    public override void _ExitTree()
    {
        range.PlayerInRange -= OnPLayerInRange;
    }

    public override void TakeDamage(int dmg)
    {
        label.Text = $"I'm John Goon and I took {dmg} damage";
        GD.Print($"I'm John Goon and I took {dmg} damage");
    }

    private Vector2 RandomizeWander()
    {
        return new Vector2(new RandomNumberGenerator().RandfRange(-10, 10) * 10, 0);
    }
    private void OnPLayerInRange()
    {
        attack._ApplyAttack("Bruiser_Attack");
    }
}
