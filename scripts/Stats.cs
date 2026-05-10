using Godot;
using System;

[GlobalClass]
public partial class Stats : Resource
{
    [Export] private int _health = 3;
    [Export] private int _damage = 1;
    public int Health{
        set{
            _health = value;
        }
    }
    public int Damage{
        set{
           _damage = value; 
        }
        get{
            return _damage;
        }
    }
}
