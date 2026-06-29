using Godot;
using System;
[GlobalClass]
public partial class Player : Entity
{
	[ExportGroup("Components")]
	[Export] public InputComponent input;
	[Export] public MovementComponent movement;
	public Node2D anchor;
	public AnimationPlayer upperBody;
	public AnimationPlayer lowerBody;
    public override void _Ready()
    {
		anchor = GetNode<Node2D>("Anchor");
        upperBody = GetNode<AnimationPlayer>("Anchor/AnimationPlayerUpper");
        lowerBody = GetNode<AnimationPlayer>("Anchor/AnimationPlayerLower");
    }
    public override void _Process(double delta)
	{
		upperBody.Play("Idle");
		lowerBody.Play("Idle");
	}
	public override void _PhysicsProcess(double delta)
	{
		input.Update();
		MoveAndSlide();
	}

	public override void TakeDamage(int dmg)
	{
		GD.Print($"Player got hit for {dmg} damage!");
	}

}
