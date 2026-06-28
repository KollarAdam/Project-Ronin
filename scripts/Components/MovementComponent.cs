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
	[Export] private float _wallSlide = 20f;
	[Export] private float _hangGracePeriod = 3f;
	private float _coyoteTime = 0.1f;
	private const float _COYOTETIMEDEFAULTVALUE = 0.1f; 
	private const float _WALLSLIDEDEFAULTVALUE = 20f;
	private const float _HANGGRACEPERIODDEFAULTVALUE = 3f;
	public int remainingJumps = 1;
	public float WallSlide { get { return _wallSlide; } }
	public float HangGracePeriod { get { return _hangGracePeriod; } set { _hangGracePeriod = value; } }
	public float JumpStrength { get { return _jumpStrength; } private set { _jumpStrength = value; } }
	public float CoyoteTime { get { return _coyoteTime; } set { _coyoteTime = value; } }

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
		if (isOnFloor || _coyoteTime > 0)
		{
			velocityY = -_jumpStrength;
		}

		return velocityY;
	}
	public float ApplyDoubleJump(float velocityY, bool isOnFloor)
	{
		if (!isOnFloor && _coyoteTime <= 0 && remainingJumps > 0)
		{
			velocityY = -_jumpStrength*1.2f;
			remainingJumps--;
		}

		return velocityY;
	}
	public void ResetJumps() {remainingJumps = _extraJumps; }
	public void ResetCoyoteFrames() {  _coyoteTime = _COYOTETIMEDEFAULTVALUE;  }
	public bool IsWallHanging(float input, bool isOnWallOnly, bool isOnFloor, Vector2 wallNormal) { return isOnWallOnly && !isOnFloor && (input == -wallNormal.X); }
	public float WallHang(float velocityY, double delta)
	{
		if (_hangGracePeriod > 0)
		{
			_hangGracePeriod -= (float)delta;
		}
		else
		{
			velocityY += (float)(_wallSlide*delta);
			if (_wallSlide < 100f) _wallSlide *= 1.02f;
		}
		return velocityY;
	}
	public void ResetWallhangValues()
	{
		_hangGracePeriod = _HANGGRACEPERIODDEFAULTVALUE;
		_wallSlide = _WALLSLIDEDEFAULTVALUE;
	}
}
