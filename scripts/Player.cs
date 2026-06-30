using Godot;
using System;
[GlobalClass]
public partial class Player : Entity
{
	[ExportGroup("Components")]
	[Export] public InputComponent input;
	[Export] public MovementComponent movement;
	[Export] public Attack attack;
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
		Vector2 anchorScale = anchor.Scale;
		input.Update();
		if (input.Direction != 0)
		{
			anchorScale.X = Math.Sign(input.Direction);
			anchor.Scale = anchorScale;
		}
		attack._AttackDir(input.Up, input.Down, IsOnFloor());
		if (input.Attack)
		{
			attack._ApplyAttack("Attack");
		}
		MoveAndSlide();
	}

	public override void TakeDamage(int dmg)
	{
		GD.Print($"Player got hit for {dmg} damage!");
	}

}
