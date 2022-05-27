using Godot;
using System;

public class StationRotationControls : Node
{
    [Export] Texture planetTexture;
    [Export] NodePath mapViewportPath;
    Viewport mapViewport;    

    public override void _Ready()
    {
        mapViewport = GetNode<Viewport>(mapViewportPath);

        SetupMap();
    }

    private void SetupMap()
    {
        foreach (SolarSystem solarSystem in Galaxy.solarSystems)
        {
            Sprite newSprite = new Sprite();
            newSprite.Position = solarSystem.Position;
            newSprite.Texture = planetTexture;
            newSprite.Scale = new Vector2(0.25f, 0.25f);

            mapViewport.AddChild(newSprite);
        }
    }
}
