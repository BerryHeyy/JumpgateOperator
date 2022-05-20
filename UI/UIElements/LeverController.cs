using Godot;
using System;

public class LeverController : TextureRect
{
    [Signal] public delegate void PushProgressChanged(float currentProgress);

    [Export] public float initialPushProgress = 0; // Value between 0 - 1 indicating how far the lever has been pushed.
    [Export] public float movableLength = 44;


    private TextureRect leverSprite;
    private float bottomMargin;

    private bool mousePressed = false;
    private float pushProgress;
    private float lastPushProgress = -1;

    public override void _Ready()
    {
        leverSprite = GetNode<TextureRect>(@"Interact");

        bottomMargin = (RectSize.y - movableLength) / 2;

        leverSprite.RectPosition = new Vector2(
            (RectSize.x - leverSprite.RectSize.x) / 2,
             bottomMargin + movableLength - movableLength * initialPushProgress
             );

        leverSprite.Connect("gui_input", this, nameof(onMouseInputLever));
        
    }

    // Note: According to DOCS, InputMouseMotion only gets called when the mouse moves, this would make
    //       the lastPushProgress check useless, I still like to have it there though :).
    private void onMouseInputLever(InputEvent @event)
    {
        if (@event.GetType() == typeof(InputEventMouseButton))
        {
            if (((InputEventMouseButton) @event).ButtonIndex == 1) mousePressed = ((InputEventMouseButton) @event).Pressed;
        }
        else if (@event.GetType() == typeof(InputEventMouseMotion) && mousePressed)
        {
            pushProgress = Mathf.Clamp(pushProgress - ((InputEventMouseMotion) @event).Relative.y / 100, 0, 1);
            
            if (pushProgress != lastPushProgress)
            {
                updateLeverSprite();
                EmitSignal(nameof(PushProgressChanged), pushProgress);
                GD.Print(pushProgress);
            }

            lastPushProgress = pushProgress;
        }
    }

    private void updateLeverSprite()
    {
        leverSprite.RectPosition = new Vector2(
            (RectSize.x - leverSprite.RectSize.x) / 2,
             bottomMargin + movableLength - movableLength * pushProgress
             );
    }
}
