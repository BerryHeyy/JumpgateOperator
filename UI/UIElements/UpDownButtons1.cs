using Godot;
using System;

public class UpDownButtons1 : Node
{
    public enum BUTTON_CODES {NONE, UP_BUTTON, DOWN_BUTTON}
    
    [Signal] public delegate void Incremented(float increment);

    BaseButton buttonUp;
    BaseButton buttonDown;

    TextureRect texUp;
    TextureRect texDown;

    Timer initialTimer;
    Timer fastIncrementTimer;

    [Export] public float timeBeforeFastIncrement = 1f; // Seconds
    [Export] public float fastIncrementTimeDelay = 0.1f;
    [Export] public float incrementStep = 1;
    [Export] public float fastIncrementStep = 1f;

    private BUTTON_CODES inUseButton = BUTTON_CODES.NONE;
    
    public override void _Ready()
    {
        buttonUp = GetNode<BaseButton>(@"ButtonUp");
        buttonDown = GetNode<BaseButton>(@"ButtonDown");

        texUp = GetNode<TextureRect>("TextureUp");
        texDown = GetNode<TextureRect>("TextureDown");

        initialTimer = GetNode<Timer>("InitialTimer");
        fastIncrementTimer = GetNode<Timer>("FastIncrementTimer");

        buttonUp.Connect("button_down", this, nameof(OnButtonDown), binds: new Godot.Collections.Array(new[] {BUTTON_CODES.UP_BUTTON}));
        buttonDown.Connect("button_down", this, nameof(OnButtonDown), binds: new Godot.Collections.Array(new[] {BUTTON_CODES.DOWN_BUTTON}));
        buttonUp.Connect("button_up", this, nameof(OnButtonUp), binds: new Godot.Collections.Array(new[] {BUTTON_CODES.UP_BUTTON}));
        buttonDown.Connect("button_up", this, nameof(OnButtonUp), binds: new Godot.Collections.Array(new[] {BUTTON_CODES.DOWN_BUTTON}));

        initialTimer.Connect("timeout", this, nameof(OnInitialTimerTimeout));
        fastIncrementTimer.Connect("timeout", this, nameof(OnFastIncrementTimerTimeout));

        initialTimer.WaitTime = timeBeforeFastIncrement;
        fastIncrementTimer.WaitTime = fastIncrementTimeDelay;
    }

    private void OnButtonDown(BUTTON_CODES buttonCode)
    {
        switch (buttonCode)
        {
        case BUTTON_CODES.UP_BUTTON:
            texUp.Visible = true;
            inUseButton = BUTTON_CODES.UP_BUTTON;
            break;
        case BUTTON_CODES.DOWN_BUTTON:
            texDown.Visible = true;
            inUseButton = BUTTON_CODES.DOWN_BUTTON;
            break;
        }

        initialTimer.Start();
    }

    private void OnButtonUp(BUTTON_CODES buttonCode)
    {
        switch (buttonCode)
        {
        case BUTTON_CODES.UP_BUTTON:
            texUp.Visible = false;
            break;
        case BUTTON_CODES.DOWN_BUTTON:
            texDown.Visible = false;
            break;
        }

        inUseButton = BUTTON_CODES.NONE;
        initialTimer.Stop();
    }

    private void OnInitialTimerTimeout()
    {
        fastIncrementTimer.Start();
    }

    private void OnFastIncrementTimerTimeout()
    {
        if(inUseButton != BUTTON_CODES.NONE) 
        {
            EmitSignal(nameof(Incremented), inUseButton == BUTTON_CODES.UP_BUTTON ? incrementStep : -incrementStep);

            fastIncrementTimer.Start();
        }
    }
}
