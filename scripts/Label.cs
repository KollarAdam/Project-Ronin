using Godot;
using System;

public partial class Label : Godot.Label
{
    private StateMachine mach;
    private string label;

    public override void _Ready()
    {
        mach = GetNode<StateMachine>("%StateMachine");
    }

    public override void _Process(double delta)
    {

        label = mach.CurrentState;
        Text = label;
    }

}
