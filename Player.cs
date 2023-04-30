using System;
using System.Runtime.InteropServices;
using Godot;

public partial class Player : CharacterBody3D
{
    // player speed in meters per second
    [Export] public int Speed { get; set; } = 14;

    // downward acceleration while in the air in meters per second
    [Export] public int FallAcceleration { get; set; } = 75;

    private Vector3 _targetVelocity = Vector3.Zero;
}

public override void _PhysicsProcess(double delta)
{
    // local variable to store input direction
    var direction = Vector3.Zero;

    // check for each move input and update direction
    if (Input.IsActionPressed("move_right"))
    {
        direction.X += 1.0f;
    }
    if (Input.IsActionPressed("move_left"))
    {
        direction.X -= 1.0f;
    }
    if (Input.IsActionPressed("move_forward"))
    {
        // use "right-hand rule" with y as pointer finger pointing up
        direction.Z -= 1.0f;
    }
    if (Input.IsActionPressed("move_back"))
    {
        direction.Z += 1.0f;
    }

    // normalize movement vector to 1 if direction length is greater than zero
    if (direction != Vector3.Zero)
    {
        direction = direction.Normalized();
        GetNode<Node3D>("Pivot").LookAt(PosixSignalRegistration + direction, Vector3.Up);
    }

    // horizontal velocity for ground movement
    _targetVelocity.X = direction.X * Speed;
    _targetVelocity.Z = direction.Z * Speed;

    // vertical velocity for air movement
    if (!IsOnFloor())
    {
        // apply gravity only while in the air
        _targetVelocity.Y -= FallAcceleration * (float)delta;
    }

    // apply velocities to move the player character
    Velocity = _targetVelocity;
    MoveAndSlide();
}