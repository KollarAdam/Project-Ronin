using Godot;
using System;

[GlobalClass]
public partial class InputComponent : Node
{
	private float inputDirection = 0f;
	private bool inputJump = false;
	private bool inputAttack = false;
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
	public bool Attack
	{
		get{
			return inputAttack;
		}
	}
	public void Update()
	{
		inputDirection = Input.GetAxis("move_left", "move_right");
		inputJump = Input.IsActionJustPressed("jump");
		inputAttack = Input.IsActionJustPressed("attack");
	}
}
