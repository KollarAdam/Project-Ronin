using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[ExportGroup("Movement")]
	[Export] private HorizontalMovementComponent _horizontalMovement;
	[Export] private VerticalMovementComponent _verticalMovement;

	enum States { MOVE, HANG };
	private States state = States.MOVE;
	float wallSlide = 200f;
	float hangGracePeriod = 1f;

	private int _remainingJumps = 1;
	private float _coyoteTime = 0f;

	public override void _PhysicsProcess(double delta)
	{
        _horizontalMovement.VelocityX = Velocity.X;
		_horizontalMovement.IsOnFloor = IsOnFloor();
		_verticalMovement.VelocityY = Velocity.Y;
		_verticalMovement.IsOnFloor = IsOnFloor();
		_verticalMovement.IsOnWallOnly = IsOnWallOnly();
		var velocity = Velocity;
		var inputDirection = Input.GetAxis("move_left", "move_right");
		var inputJump = Input.IsActionJustPressed("jump");
		switch (state)
		{
			case States.MOVE:
				_coyoteTime -= (float)delta;
				_verticalMovement.CoyoteTime = _coyoteTime;

				velocity.Y = _verticalMovement.ApplyGravity(delta);

				_verticalMovement.ResetJumps();

				if (inputJump)
				{
					velocity.Y = _verticalMovement.ApplyJump();
				}
				if (inputDirection == 0)
				{
					velocity.X = _horizontalMovement.ApplyFriction(delta);
				}
				else
				{
					velocity.X = _horizontalMovement.AccelerateHorizontally(inputDirection, delta);
				}

				var wasOnFloor = IsOnFloor();

				Velocity = velocity;

				MoveAndSlide();

				if (wasOnFloor && !IsOnFloor() && velocity.Y >= 0) _coyoteTime = 0.1f;
				if (_IsWallHanging(inputDirection)) state = States.HANG;
				GD.Print($"Player's coyote time {_coyoteTime}\n vertmovement coyote time {_verticalMovement.CoyoteTime}");
				break;

			case States.HANG:
				if (_IsWallHanging(inputDirection))
				{
					hangGracePeriod -= (float)delta;
					velocity.Y = 0f;
					if(hangGracePeriod <= 0f){ velocity.Y += (float)(wallSlide * delta);
					if(wallSlide <= 2000f) wallSlide *= 1.02f;};
					if(inputJump){
						velocity.Y = _verticalMovement.ApplyJump();
						velocity.X = GetWallNormal().X*_verticalMovement.JumpStrength/2;
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
	private bool _IsWallHanging(float input){ return IsOnWallOnly() && (input == -GetWallNormal().X);}

}
