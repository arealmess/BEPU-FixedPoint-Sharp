using Godot;
using System; 

public partial class BEPUBenchmark : Level
{
	Random rng = new(256);  // Generates pseudorandom numbers

	PackedScene boxScene = GD.Load<PackedScene>("res://-DEMO/scenes/physics_tests/BEPUBenchmark/objects/box_benchmark/box_benchmark.tscn");
	int count = 0;  // Tracks frame count
	int spawnDelay = 8;  // Delay between spawning each box
	int boxCount = 0;  // Tracks number of boxes
	int boxLimit = 10240;  // Max boxes that will be spawned

	DebugUI UI; Global global;

  public override void _Ready()
	{
		base._Ready();

		UI = GetNode<DebugUI>("DebugUI");
    global = GetTree().Root.GetNode<Global>("Global");
  }

	public override void _PhysicsProcess(double delta)
	{

    // Spawn a box every few frames
    if (count % spawnDelay == 0 && boxCount < boxLimit) {
			SpawnBox();
		}

		count++;
	}

	private void SpawnBox() {
		RigidBody box = (RigidBody)boxScene.Instantiate();

		// Randomize color
		StandardMaterial3D material = (StandardMaterial3D)(box.GetNode<MeshInstance3D>("ColShape/MeshInstance3D").Mesh.SurfaceGetMaterial(0));
		material.AlbedoColor = new Color((float)rng.NextDouble(), (float)rng.NextDouble(), (float)rng.NextDouble(), 1);
		box.GetNode<MeshInstance3D>("ColShape/MeshInstance3D").Mesh.SurfaceSetMaterial(0, material);

		AddChild(box);

		BEPUutilities.Vector3 pos = new(rng.Next(-32,32),16,rng.Next(-32,32));
		box.Body.Position = pos;
		box.Position = new Godot.Vector3((float)pos.X, (float)pos.Y, (float)pos.Z);

    boxCount++;

		// Update UI
		UI.UpdateStats(boxCount);
	}
} 