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

	public int remainingJumps = 1;
	public float JumpStrength{ get{return _jumpStrength;} private set{_jumpStrength = value;}}
	public float CoyoteTime {get; set;}

	public float AccelerateHorizontally(float horDir, float velocityX, double delta, bool isOnFloor)
	{
		var accelerationAmount = _acceleration;
		if (!isOnFloor) accelerationAmount = _airAcceleration;
		velocityX = Mathf.MoveToward(velocityX, _maxMoveSpeed * horDir, (float)(accelerationAmount * delta * Math.Abs(horDir)));
		return velocityX;
	}
	public float ApplyFriction(float velocityX, double delta, bool isOnFloor)
	{
		var frictionAmount = _friction;
		if (!isOnFloor) frictionAmount = _airFriction;
		velocityX = Mathf.MoveToward(velocityX, 0f, (float)(frictionAmount * delta));
		return velocityX;
	}
	public float ApplyGravity(float velocityY, double delta, bool isOnFloor)
	{
		if (!isOnFloor)
		{
			if (velocityY <= 0) velocityY += (float)(_upGravity * delta);
			else velocityY += (float)(_downGravity * delta);
		}
		return velocityY;
	}
	public float ApplyJump(float velocityY, bool isOnFloor)
	{
		if (isOnFloor || CoyoteTime > 0)
		{
			velocityY = -_jumpStrength;
		}
		if (!isOnFloor && CoyoteTime <= 0 && remainingJumps != 0)
		{
			velocityY = -_jumpStrength;
			remainingJumps--;
		}
		return velocityY;
	}
	public void ResetJumps(bool isOnFloor){ if (isOnFloor){remainingJumps = _extraJumps;}}

}
