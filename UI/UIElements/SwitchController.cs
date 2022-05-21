using Godot;
using System;

public class SwitchController : TextureRect
{
    [Signal] public delegate void SwitchToggled(bool state);

    [Export] public Texture onTexture;
    [Export] public Texture offTexture;

    private bool state = false;
    private bool prevState = false;

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
                Vector2 relPos = GetViewport().GetMousePosition() - RectGlobalPosition;

                
                state = relPos.y < RectSize.y;
                
                Texture = state ? onTexture : offTexture;
                
                if (prevState != state) 
                {
                    GD.Print(state);
                    EmitSignal(nameof(SwitchToggled), state);
                }

                prevState = state;
            }
        }
    }
}
