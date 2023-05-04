using Godot;

public partial class Main : Node
{
    [Export] public PackedScene MobScene { get; set; }

    private void _on_mob_timer_timeout()
    {
        Mob mob = MobScene.Instantiate<Mob>();
        PathFollow3D mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();
        Vector3 playerPosition = GetNode<Player>("Player").Position;
        mob.Initialize(mobSpawnLocation.Position, playerPosition);
        AddChild(mob);
    }
}