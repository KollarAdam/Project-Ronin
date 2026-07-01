using Godot;
using System;
[GlobalClass]
public partial class Player : Entity
{
	[ExportGroup("Components")]
	[Export] public InputComponent input;
	[Export] public MovementComponent movement;
	[Export] public Attack attack;
	[Export] private int _health = 3;
	public static Player Instance;
	public Node2D anchor;
	public AnimationPlayer upperBody;
	public AnimationPlayer lowerBody;
	private float _currentTime = 0f;
	public override void _Ready()
	{
		Instance = this;
		anchor = GetNode<Node2D>("Anchor");
		upperBody = GetNode<AnimationPlayer>("Anchor/AnimationPlayerUpper");
		lowerBody = GetNode<AnimationPlayer>("Anchor/AnimationPlayerLower");
		upperBody.Play("Idle");
		lowerBody.Play("Idle");
	}
	public override void _Process(double delta){
	
		if(_currentTime > 0)
		{
			_currentTime -= (float)delta;
		}
		else
		{
			anchor.Modulate = Colors.White;
		}
		if(_health <= 0) Die();
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 anchorScale = anchor.Scale;
		input.Update();
		if (input.Direction != 0)
		{
			anchorScale.X = Math.Sign(input.Direction);
			anchor.Scale = anchorScale;
		}
		attack._AttackDir(input.Up, input.Down, IsOnFloor());
		if (input.Attack)
		{
			attack._ApplyAttack("Attack");
		}
		MoveAndSlide();
	}

	public override void TakeDamage(int dmg)
	{
		_health -= dmg;
		_currentTime = .1f;
		anchor.Modulate = Colors.Red;
		// GD.Print($"Player got hit for {dmg} damage!\nCurrent hp: {_health}");
	}
	private void Die()
	{
		GetTree().ReloadCurrentScene();
	}
}
