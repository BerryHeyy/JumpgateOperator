using Godot;
using System;

public class ProgressBarController : Node
{    
    TextureProgress textureProgress;
    Label label;


    public override void _Ready()
    {
        textureProgress = GetNode<TextureProgress>("TextureProgress");
        label = GetNode<Label>("Label");

        UpdateLabelPercentage();
    }

    public void SetProgressValue(double value)
    {
        textureProgress.Value = value;
        UpdateLabelPercentage();
    }

    public double GetProgressValue()
    {
        return textureProgress.Value;
    }

    private void UpdateLabelPercentage()
    {
        label.Text = String.Format("{0:0.0}%", textureProgress.Value / textureProgress.MaxValue * 100);
    }
}
