using Godot;  

[Tool]
public partial class groundBenchmark : StaticBody  // Change inheirited class to desired PhysicsBody (CharacterBody, KinematicBody, RigidBody, StaticBody)
{
	public override void _Ready()
	{
		if (Engine.IsEditorHint()) return;

		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Engine.IsEditorHint()) return;

		base._PhysicsProcess(delta);
	}
} 