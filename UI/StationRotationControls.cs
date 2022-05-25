using Godot;
using System;

public class StationRotationControls : Node
{
    [Export] NodePath mapViewportPath;
    Viewport mapViewport;

    public override void _Ready()
    {
        mapViewport = GetNode<Viewport>(mapViewportPath);
    }

    public override void _Process(float delta)
    {
        RenderMap(delta);
    }

    private void RenderMap(float delta)
    {
        
    }
}
