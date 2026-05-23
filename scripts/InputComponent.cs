using Godot;
using System;

[GlobalClass]
public partial class InputComponent : Node
{
	private float inputDirection = 0f;
	private bool inputJump = false;
	public float Direction{
		get{
			return inputDirection;
		}
	}
	public bool Jump{
		get{
			return inputJump;
		}
	}
	public void Update()
	{
		inputDirection = Input.GetAxis("move_left", "move_right");
		inputJump = Input.IsActionJustPressed("jump");
	}
}
