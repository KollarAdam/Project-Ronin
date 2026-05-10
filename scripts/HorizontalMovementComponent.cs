using Godot;
using System;
[GlobalClass]
public partial class HorizontalMovementComponent : Node
{
	[Export] private float _baseMoveSpeed = 100f;
	[Export] private float _maxMoveSpeed = 200f;
	[Export] private float _acceleration = 10000f;
	[Export] private float _airAcceleration = 1000f;
	[Export] private float _friction = 10000f;
	[Export] private float _airFriction = 700f;

	public float VelocityX {get; set;}
	public bool IsOnFloor {get; set;}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public float AccelerateHorizontally(float horDir, double delta)
	{
		var velocityX = VelocityX;
		var accelerationAmount = _acceleration;
		if (!IsOnFloor) accelerationAmount = _airAcceleration;
		velocityX = Mathf.MoveToward(velocityX, _maxMoveSpeed * horDir, (float)(accelerationAmount * delta * Math.Abs(horDir)));
		return velocityX;
	}
	public float ApplyFriction(double delta)
	{
		var velocityX = VelocityX;
		var frictionAmount = _friction;
		if (!IsOnFloor) frictionAmount = _airFriction;
		velocityX = Mathf.MoveToward(velocityX, 0f, (float)(frictionAmount * delta));
		return velocityX;
	}

}
