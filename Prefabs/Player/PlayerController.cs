using Godot;
using System;

public class PlayerController : KinematicBody2D
{
    public float movementSpeed = 1.4f * Reference.MeterInGDUnits;
    public float sprintMovementSpeedMultiplier = 3.5f;
    public float zoomStep = 0.1f;
    public float minZoom = 0.2f;
    public float maxZoom = 1.5f;

    private Camera2D playerCamera;
    private Sprite playerSprite;

    private float zoomLevel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerCamera = GetNode<Camera2D>(@"Camera2D");
        zoomLevel = playerCamera.Zoom.x;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 direction = new Vector2();
        float speedMultiplier = 1;

        if (Input.IsActionPressed("movement_up"))       direction.y -= 1;
        if (Input.IsActionPressed("movement_down"))     direction.y += 1;
        if (Input.IsActionPressed("movement_left"))     direction.x -= 1;
        if (Input.IsActionPressed("movement_right"))    direction.x += 1;

        if (Input.IsActionPressed("movement_sprint")) speedMultiplier = sprintMovementSpeedMultiplier;

        MoveAndCollide(direction.Normalized() * movementSpeed * speedMultiplier * delta);

        // Set character rotation
        Vector2 mousePosition = GetViewport().GetMousePosition() - GetViewport().Size / 2;

        float angle = Mathf.Atan(mousePosition.y / mousePosition.x);

        if (mousePosition.x < 0) Rotation = angle - Mathf.Pi;
        else Rotation = angle;
    }

    public override void _UnhandledInput(InputEvent @event){
        if (@event is InputEventMouseButton){
            InputEventMouseButton emb = (InputEventMouseButton)@event;
            if (emb.IsPressed()){
                if (emb.ButtonIndex == (int)ButtonList.WheelUp){
                    zoomLevel = Mathf.Clamp(zoomLevel - zoomStep, minZoom, maxZoom);
                    playerCamera.Zoom = new Vector2(zoomLevel, zoomLevel);
                }
                if (emb.ButtonIndex == (int)ButtonList.WheelDown){
                    zoomLevel = Mathf.Clamp(zoomLevel + zoomStep, minZoom, maxZoom);
                    playerCamera.Zoom = new Vector2(zoomLevel, zoomLevel);
                }
            }
        }
    }
}
