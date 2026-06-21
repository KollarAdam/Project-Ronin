using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[ExportGroup("Components")]
	[Export] public InputComponent input;
	[Export] public MovementComponent movement;

	public float wallSlide = 200f;
	public float hangGracePeriod = 1f;

	public int remainingJumps = 1;
	public float coyoteTime = 0f;
	public Node2D anchor;
	public AnimationPlayer upperBody;
	public AnimationPlayer lowerBody;
    public override void _Ready()
    {
		anchor = GetNode<Node2D>("Anchor");
        upperBody = GetNode<AnimationPlayer>("Anchor/AnimationPlayerUpper");
        lowerBody = GetNode<AnimationPlayer>("Anchor/AnimationPlayerLower");
    }
	public override void _PhysicsProcess(double delta)
	{
		input.Update();
		MoveAndSlide();
	}
	public bool IsWallHanging(float input){ return IsOnWallOnly() && (input == -GetWallNormal().X);}

}
