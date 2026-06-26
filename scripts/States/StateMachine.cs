using Godot;
using Godot.Collections;
using System;
using System.Linq;
[GlobalClass]
public partial class StateMachine : Node
{
	[Export] private PlayerState initialState;
	private PlayerState currentState;
	private Dictionary<string, PlayerState> states = new Dictionary<string, PlayerState>();

    public override void _Ready()
    {
        foreach(PlayerState child in GetChildren().Cast<PlayerState>())
		{
			if(child is PlayerState)
			{
				states[child.Name.ToString().ToUpper()] = child;
				child.StateChanged += OnStateChange;
			}
		}
		if (initialState is PlayerState)
		{
			initialState.Enter();
			currentState = initialState;
		}
    }
	public override void _Process(double delta)
	{
		currentState?.Process(delta);
	}
    public override void _PhysicsProcess(double delta)
    {
        currentState?.PhysicsProcess(delta);
    }
    public override void _ExitTree()
    {
        foreach(PlayerState child in GetChildren().Cast<PlayerState>())
		{
			if(child is PlayerState)
			{
				child.StateChanged -= OnStateChange;
			}
		}
    }

	public void OnStateChange(PlayerState state,string newStateName)
	{
		if(state != currentState) return;

		PlayerState newState = states[newStateName.ToUpper()];

		if(newState is null) return;

		currentState?.Exit();

		newState.Enter();
		currentState = newState;
	}

}
