using Godot;
using Godot.Collections;
using System;
using System.Linq;
[GlobalClass]
public partial class StateMachine : Node
{
	[Export] private GenericState initialState;
	private GenericState currentState;
	private Dictionary<string, GenericState> states = new Dictionary<string, GenericState>();
	public string CurrentState {get{return states.FirstOrDefault(x => x.Value == currentState).Key;}}
    public override void _Ready()
    {
        foreach(GenericState child in GetChildren().Cast<GenericState>())
		{
			if(child is GenericState)
			{
				states[child.Name.ToString().ToUpper()] = child;
				child.StateChanged += OnStateChange;
			}
		}
		if (initialState is GenericState)
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
        foreach(GenericState child in GetChildren().Cast<GenericState>())
		{
			if(child is GenericState)
			{
				child.StateChanged -= OnStateChange;
			}
		}
    }

	public void OnStateChange(GenericState state, string newStateName)
	{
		if(state != currentState) return;

		GenericState newState = states[newStateName.ToUpper()];

		if(newState is null) return;

		currentState?.Exit();

		newState?.Enter();
		currentState = newState;
	}

}
