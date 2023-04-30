using System;
using Godot;

public partial class Player : CharacterBody3D
{
    // Player speed in meters per second
    [Export] public int Speed { get; set; } = 14;

    // Downward acceleration while in the air in meters per second
    [Export] public int FallAcceleration { get; set; } = 75;

    private Vector3 _targetVelocity = Vector3.Zero;
}
