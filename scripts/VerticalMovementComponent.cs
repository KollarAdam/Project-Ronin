using Godot;
using System;
[GlobalClass]
public partial class VerticalMovementComponent : Node
{
	[Export] private float _upGravity = 500f;
	[Export] private float _downGravity = 1000f;
	[Export] private float _jumpStrength = 200f;
	[Export] private int _extraJumps = 1;

	public float VelocityY {get; set;}
	public bool IsOnWallOnly{get; set;}
	public bool IsOnFloor {get; set;}
	public float JumpStrength{ get{return _jumpStrength;} private set{_jumpStrength = value;}}
	
	private int _remainingJumps = 1;
	public float CoyoteTime {get; set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
