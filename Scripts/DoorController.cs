using Godot;
using System;

public class DoorController : Node2D
{
    AnimationPlayer animationPlayer;
    Area2D openingArea;

    AudioStreamPlayer2D openSound;
    AudioStreamPlayer2D closeSound;

    
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>(@"AnimationPlayer");
        openingArea = GetNode<Area2D>(@"Area2D");

        openSound = GetNode<AudioStreamPlayer2D>(@"AudioPlayerOpen");
        closeSound = GetNode<AudioStreamPlayer2D>(@"AudioPlayerClose");

        openingArea.Connect("body_shape_entered", this, nameof(onPlayerEntered));
        openingArea.Connect("body_shape_exited", this, nameof(onPlayerExited));
    }

    public void onPlayerEntered(RID body_rid, Node body, int body_shape_index, int local_shape_index)
    {
        if (body.GetType() == typeof(PlayerController)) 
        {
            animationPlayer.Play("DoorOpen");
            closeSound.Stop();
            openSound.Play();
        }
        
    }

    public void onPlayerExited(RID body_rid, Node body, int body_shape_index, int local_shape_index)
    {
        if (body.GetType() == typeof(PlayerController))
        {
            animationPlayer.PlayBackwards("DoorOpen");
            openSound.Stop();
            closeSound.Play();
        }
    }
}
