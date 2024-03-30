using Godot; 

using Deterministic.FixedPoint;
using BEPUphysics;
using BEPUphysics.Entities.Prefabs; 

namespace TestBoxes
{
  public partial class Test : Node
  {
	Space space = new();
	Box box = new(new BEPUutilities.Vector3(1, 1, 1), (fp)1, (fp)1, (fp)1, (fp)8);

	public override void _Ready()
	{
	  space.Add(box);
	  space.ForceUpdater.Gravity = new BEPUutilities.Vector3(0, (fp)(-9.81), 0);
	}

	public override void _PhysicsProcess(double delta)
	{
	  space.Update();
	  GD.Print(new Godot.Vector3((float)box.Position.X, (float)box.Position.Y, (float)box.Position.Z));
	}
  } 
}
