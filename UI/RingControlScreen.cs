using Godot;
using System;

public class RingControlScreen : Node
{
    [Export] NodePath ringViewportPath;
    Viewport ringViewport;

    public override void _Ready()
    {
        ringViewport = GetNode<Viewport>(ringViewportPath);
        GD.Print("BaLLAS");

        VisualServer.SetDebugGenerateWireframes(true);
        
    }

    public override void _Process(float delta)
    {
        ringViewport.DebugDraw = Viewport.DebugDrawEnum.Wireframe;
    }
}
