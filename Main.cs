using Godot;

public partial class Main : Node
{
    [Export] public PackedScene MobScene { get; set; }

    public override void _Ready()
    {
        GetNode<Control>("UI/Retry").Hide();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept") && GetNode<Control>("UI/Retry").Visible)
        {
            GetTree().ReloadCurrentScene();
        }
    }

    private void _on_mob_timer_timeout()
    {
        Mob mob = MobScene.Instantiate<Mob>();
        PathFollow3D mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();
        Vector3 playerPosition = GetNode<Player>("Player").Position;
        mob.Initialize(mobSpawnLocation.Position, playerPosition);
        AddChild(mob);
        mob.Squashed += GetNode<ScoreLabel>("UI/ScoreLabel")._on_mob_squashed;
    }

    private void _on_player_hit()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Control>("UI/Retry").Show();
    }
}
