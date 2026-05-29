using Godot;
using Godot.Collections;
using System;
using System.Linq;
[GlobalClass]
public partial class StateMachine : Node
{
	[Export] private State initialState;
	private State currentState;
	private Dictionary<string, State> states = new Dictionary<string, State>();

    public override void _Ready()
    {
        foreach(State child in GetChildren().Cast<State>())
		{
			if(child is State)
			{
				states[child.Name.ToString().ToUpper()] = child;
			}
		}
    }
	public override void _Process(double delta)
	{
		currentState?._Process(delta);
	}
    public override void _PhysicsProcess(double delta)
    {
        currentState?._PhysicsProcess(delta);
    }

}
