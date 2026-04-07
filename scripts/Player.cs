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
	
	private int _remainingJumps = 1;

    public override void _PhysicsProcess(double delta)
    {
		var velocity = Velocity;
		var inputDirection = Input.GetAxis("move_left", "move_right");
		var inputJump = Input.IsActionJustPressed("jump");

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

		Velocity = velocity;

		MoveAndSlide();
    }

	private float _AccelerateHorizontally(float horDir, double delta)
	{
		var velocity = Velocity;
		var accelerationAmount = _acceleration;
		if(!IsOnFloor()) accelerationAmount = _airAcceleration;
		velocity.X = Mathf.MoveToward(velocity.X, _maxMoveSpeed * horDir, (float)(accelerationAmount * delta * Math.Abs(horDir)));
		return velocity.X;
	}

	private float _ApplyFriction(double delta)
	{
		var velocity = Velocity;
		var frictionAmount = _friction;
		if(!IsOnFloor()) frictionAmount = _airFriction;
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
		if (IsOnFloor())
		{
			velocity.Y = -_jumpStrength;
		}
		if (!IsOnFloor() && _remainingJumps != 0)
		{
			velocity.Y = -_jumpStrength;
			_remainingJumps--;
		}
		return velocity.Y;
	}

	private void _ResetJumps()
	{
		if(IsOnFloor()) _remainingJumps = _extraJumps;
	}
}
