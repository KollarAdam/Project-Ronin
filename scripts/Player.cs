using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[ExportGroup("Components")]
	[Export] private InputComponent _input;
	[Export] private MovementComponent _movement;

	enum States { MOVE, HANG };
	private States state = States.MOVE;
	float wallSlide = 200f;
	float hangGracePeriod = 1f;

	private int _remainingJumps = 1;
	private float _coyoteTime = 0f;

	public override void _PhysicsProcess(double delta)
	{
		_input.Update();
        _movement.VelocityX = Velocity.X;
		_movement.IsOnFloor = IsOnFloor();
		_movement.VelocityY = Velocity.Y;
		_movement.IsOnFloor = IsOnFloor();
		_movement.IsOnWallOnly = IsOnWallOnly();
		var velocity = Velocity;
		switch (state)
		{
			case States.MOVE:
				_coyoteTime -= (float)delta;
				_movement.CoyoteTime = _coyoteTime;

				velocity.Y = _movement.ApplyGravity(delta);

				_movement.ResetJumps();

				if (_input.Jump)
				{
					velocity.Y = _movement.ApplyJump();
				}
				if (_input.Direction == 0)
				{
					velocity.X = _movement.ApplyFriction(delta);
				}
				else
				{
					velocity.X = _movement.AccelerateHorizontally(_input.Direction, delta);
				}

				var wasOnFloor = IsOnFloor();

				Velocity = velocity;

				MoveAndSlide();

				if (wasOnFloor && !IsOnFloor() && velocity.Y >= 0) _coyoteTime = 0.1f;
				if (_IsWallHanging(_input.Direction)) state = States.HANG;
				GD.Print($"Player's coyote time {_coyoteTime}\n vertmovement coyote time {_movement.CoyoteTime}");
				break;

			case States.HANG:
				if (_IsWallHanging(_input.Direction))
				{
					hangGracePeriod -= (float)delta;
					velocity.Y = 0f;
					if(hangGracePeriod <= 0f){ velocity.Y += (float)(wallSlide * delta);
					if(wallSlide <= 2000f) wallSlide *= 1.02f;};
					if(_input.Jump){
						velocity.Y = _movement.ApplyJump();
						velocity.X = GetWallNormal().X*_movement.JumpStrength/2;
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
