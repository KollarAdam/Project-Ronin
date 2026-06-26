using Godot;
using System;

[GlobalClass]
public partial class InputComponent : Node
{
	private float inputDirection = 0f;
	private bool inputJump = false;
	private bool inputAttack = false;
	private bool inputUp = false;
	private bool inputDown = false;
	public float Direction{
		get {return inputDirection;}
	}
	public bool Jump{
		get {return inputJump;}
	}
	public bool Attack{
		get {return inputAttack;}
	}
	public bool Up{
		get {return inputUp;}
	}
	public bool Down{
		get {return inputDown;}
	}
	public void Update()
	{
		inputDirection = Input.GetAxis("move_left", "move_right");
		inputJump = Input.IsActionJustPressed("jump");
		inputAttack = Input.IsActionJustPressed("attack");
		inputDown = Input.IsActionPressed("dir_down");
		inputUp = Input.IsActionPressed("dir_up");
	}
}
