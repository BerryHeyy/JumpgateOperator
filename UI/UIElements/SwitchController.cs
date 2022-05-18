using Godot;
using System;

public class SwitchController : TextureRect
{
    [Export]
    public Texture onTexture;
    [Export]
    public Texture offTexture;

    private bool state = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.Connect("gui_input", this, nameof(onInput));
    }

    public void onInput(InputEvent @event)
    {
        if (@event.GetType() == typeof(InputEventMouseButton))
        {
            InputEventMouseButton mouseEvent = (InputEventMouseButton) @event;
            if (mouseEvent.ButtonIndex == 1 && mouseEvent.Pressed)
            {
                state = !state;
                Texture = state ? onTexture : offTexture;
            }
        }
    }
}
