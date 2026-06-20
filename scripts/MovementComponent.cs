using Godot;
using System;
[GlobalClass]
public partial class MovementComponent : Node
{
	[Export] private float _maxMoveSpeed = 200f;
	[Export] private float _acceleration = 10000f;
	[Export] private float _airAcceleration = 1000f;
	[Export] private float _friction = 10000f;
	[Export] private float _airFriction = 700f;
	[Export] private float _upGravity = 500f;
	[Export] private float _downGravity = 1000f;
	[Export] private float _jumpStrength = 200f;
	[Export] private int _extraJumps = 1;

	private int _remainingJumps = 1;
	public float VelocityX {get; set;}
	public float VelocityY {get; set;}
	public bool IsOnWallOnly{get; set;}
	public bool IsOnFloor {get; set;}
	public float JumpStrength{ get{return _jumpStrength;} private set{_jumpStrength = value;}}
	public float CoyoteTime {get; set;}

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
	public float ApplyGravity(double delta)
	{
		var velocityY = VelocityY;
		if (!IsOnFloor)
		{
			if (velocityY <= 0) velocityY += (float)(_upGravity * delta);
			else velocityY += (float)(_downGravity * delta);
		}
		return velocityY;
	}
	public float ApplyJump()
	{
		var velocityY = VelocityY;
		if (IsOnFloor || CoyoteTime > 0 || IsOnWallOnly)
		{
			velocityY = -_jumpStrength;
		}
		if (!IsOnFloor && CoyoteTime <= 0 && _remainingJumps != 0)
		{
			velocityY = -_jumpStrength;
			_remainingJumps--;
		}
		return velocityY;
	}
	public void ResetJumps(){ if (IsOnFloor){_remainingJumps = _extraJumps;}}

}
