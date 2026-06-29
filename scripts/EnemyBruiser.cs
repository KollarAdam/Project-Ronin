using Godot;
using System;

public partial class EnemyBruiser : Entity
{
    Godot.Label label;
    float h = 3f;
    public override void _Ready()
    {
        label = GetNode<Godot.Label>("Label");
        Velocity = RandomizeWander();
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        h -= (float)delta;
        if (h < 0)
        {
            Velocity += RandomizeWander();
            h = 3f;
        }
        MoveAndSlide();
    }

    public override void TakeDamage(int dmg)
    {
        label.Text = $"I'm John Bruiser and I took {dmg} damage";
    }

    private Vector2 RandomizeWander()
    {
        return new Vector2(new RandomNumberGenerator().RandfRange(-10, 10)*10, 0);
    }
}
