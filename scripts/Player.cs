using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[ExportGroup("Movement")]
	[Export] private float _baseMoveSpeed = 10f;
	[Export] private float _baseJumpHeight = 5f;
	[Export] private int _extraJumps = 1;

	private Vector2 _moveDirection = Vector2.Zero;

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        var inputDirection = Input.GetAxis("move_left", "move_right");
		_moveDirection = new Vector2(inputDirection, 0).Normalized();
    }

	private void HandleMovement()
	{
		if(_moveDirection == Vector2.Zero)
		{
			Velocity = Vector2.Zero;
			return;
		}
		Velocity = _moveDirection * _baseMoveSpeed;
	}

    public override void _PhysicsProcess(double delta)
    {
		HandleMovement();
		MoveAndSlide();
    }

}
