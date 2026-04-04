using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[ExportGroup("Movement")]
	[Export] private float _baseMoveSpeed = 10f;
	[Export] private float _maxMoveSpeed = 20f;
	[Export] private float _acceleration = 1000f;
	[Export] private float _airAcceleration = 50f;
	[Export] private float _airFriction = 1000f;
	[Export] private float _friction = 2000f;
	[Export] private float _upGravity = 500f;
	[Export] private float _downGravity = 600f;
	[Export] private float _jumpStrength = 200f;
	[Export] private int _extraJumps = 1;
	private Vector2 _moveDirection = Vector2.Zero;
    public override void _UnhandledKeyInput(InputEvent @event)
    {
        var inputDirection = Input.GetAxis("move_left", "move_right");
		_moveDirection = new Vector2(inputDirection, 0).Normalized();
    }

	private void HandleMovement()
	{
		Velocity = _moveDirection * _baseMoveSpeed;
		
	}
	private void accelerateHorizontally(float delta, float horizontalDirection)
	{
		Vector2 velocity = Velocity;
		var accelerationAmount = _acceleration;
		if (!IsOnFloor())
		{
			accelerationAmount = _airAcceleration;
		}
	}

    public override void _PhysicsProcess(double delta)
    {
		HandleMovement();
		MoveAndSlide();
    }

}
