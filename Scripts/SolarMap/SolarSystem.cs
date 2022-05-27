using Godot;
using System;

public class SolarSystem
{
    public Vector2 Position {get; private set;}

    public SolarSystem(Vector2 position)
    {
        Position = position;
    }

}