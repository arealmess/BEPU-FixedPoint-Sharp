using gd = Godot;
using bepu = BEPUutilities; 
using Deterministic.FixedPoint;
using BEPUphysics;
using BEPUphysics.Entities.Prefabs;

namespace ProjectCrescent.Workshop
{
  public partial class Test : gd.Node
  {
		Space space = new();
		Box box = new(new bepu.Vector3(0,0,0), 1, 1, 1, 8);
		Box ground = new(bepu.Vector3.Zero, 30, 1, 30);

		public override void _Ready()
		{
			space.Add(box);
			space.Add(ground);
			space.ForceUpdater.Gravity = new bepu.Vector3(0,10,0);
		}

		public override void _PhysicsProcess(double delta)
		{
			space.Update();
			gd.GD.Print(new gd.Vector3((float)box.Position.X, (float)box.Position.Y, (float)box.Position.Z));
		}
  } 
}
