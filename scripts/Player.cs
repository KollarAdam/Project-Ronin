using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[ExportGroup("Movement")]
	[Export] private float _baseMoveSpeed = 100f;
	[Export] private float _maxMoveSpeed = 200f;
	[Export] private float _acceleration = 2000f;
	[Export] private float _airAcceleration = 1000f;
	[Export] private float _airFriction = 700f;
	[Export] private float _friction = 2000f;
	[Export] private float _upGravity = 500f;
	[Export] private float _downGravity = 700f;
	[Export] private float _jumpStrength = 200f;
	[Export] private int _extraJumps = 1;

	enum States { MOVE, HANG };
	float wallSlide = 200f;
	float hangGracePeriod = 1f;
	private States state = States.MOVE;

	private int _remainingJumps = 1;
	private float _coyoteTime = 0f;

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;
		var inputDirection = Input.GetAxis("move_left", "move_right");
		var inputJump = Input.IsActionJustPressed("jump");
		switch (state)
		{
			case States.MOVE:
				_coyoteTime -= (float)delta;

				velocity.Y = _ApplyGravity(delta);

				_ResetJumps();

				if (inputJump)
				{
					velocity.Y = _ApplyJump();
				}
				if (inputDirection == 0)
				{
					velocity.X = _ApplyFriction(delta);
				}
				else
				{
					velocity.X = _AccelerateHorizontally(inputDirection, delta);
				}

				var wasOnFloor = IsOnFloor();

				Velocity = velocity;

				MoveAndSlide();

				if (wasOnFloor && !IsOnFloor() && velocity.Y >= 0 || IsOnWallOnly()) _coyoteTime = 0.1f;
				if (_IsWallHanging(inputDirection)) state = States.HANG;

				break;

			case States.HANG:
				if (_IsWallHanging(inputDirection))
				{
					hangGracePeriod -= (float)delta;
					velocity.Y = 0f;
					if(hangGracePeriod <= 0f){ velocity.Y += (float)(wallSlide * delta);
					if(wallSlide <= 2000f) wallSlide *= 1.02f;};
					if(inputJump){
						velocity.Y = _ApplyJump();
						velocity.X = GetWallNormal().X*_jumpStrength/2;
						}
					Velocity = velocity;
					MoveAndSlide();
				}
				else
				{
					wallSlide = 200f;
					hangGracePeriod = 1f;
					state = States.MOVE;	
				}

				break;
		}
	}
	private float _AccelerateHorizontally(float horDir, double delta)
	{
		var velocity = Velocity;
		var accelerationAmount = _acceleration;
		if (!IsOnFloor()) accelerationAmount = _airAcceleration;
		velocity.X = Mathf.MoveToward(velocity.X, _maxMoveSpeed * horDir, (float)(accelerationAmount * delta * Math.Abs(horDir)));
		return velocity.X;
	}
	private float _ApplyFriction(double delta)
	{
		var velocity = Velocity;
		var frictionAmount = _friction;
		if (!IsOnFloor()) frictionAmount = _airFriction;
		velocity.X = Mathf.MoveToward(velocity.X, 0f, (float)(frictionAmount * delta));
		return velocity.X;
	}
	private float _ApplyGravity(double delta)
	{
		var velocity = Velocity;
		if (!IsOnFloor())
		{
			if (velocity.Y <= 0) velocity.Y += (float)(_upGravity * delta);
			else velocity.Y += (float)(_downGravity * delta);
		}
		return velocity.Y;
	}
	private float _ApplyJump()
	{
		var velocity = Velocity;
		if (IsOnFloor() || _coyoteTime > 0 || IsOnWallOnly())
		{
			velocity.Y = -_jumpStrength;
		}
		if (!IsOnFloor() && _coyoteTime <= 0 && _remainingJumps != 0)
		{
			velocity.Y = -_jumpStrength;
			_remainingJumps--;
		}
		return velocity.Y;
	}
	private void _ResetJumps(){ if (IsOnFloor()){_remainingJumps = _extraJumps; GD.Print("Double jump reset!");}}
	private bool _IsWallHanging(float input){ return IsOnWallOnly() && (input == -GetWallNormal().X);}
}
