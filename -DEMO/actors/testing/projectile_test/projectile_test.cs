using Godot;
using System;

using FixMath.NET;
using BEPUphysics;
using BEPUphysics.Entities;
using BEPUutilities;

// Collision libraries
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
//using BEPUphysics.BroadPhaseSystems;
//using BEPUphysics.CollisionTests;
using Deterministic.FixedPoint;


[Tool]
public partial class projectile_test : KinematicBody
{
  PhysicsBody target;
  fp t = 25;
  fp3 rngDir = default;
  fp3 lastDir = default;
  int frameCounter = 0;
  Deterministic.FixedPoint.Random rng = new(5);

  public override void _Ready()
  {
    base._Ready();

    if (Godot.Engine.IsEditorHint()) return;

    // Set target to player
    Global global = (Global)GetTree().Root.GetNode("Global");
    target = global.Player;
  }

  public override void _PhysicsProcess(double delta)
  {
    if (Godot.Engine.IsEditorHint()) return;

    base._PhysicsProcess(delta);

    // Move towards target
    frameCounter++; 
    if (frameCounter % 10 == 0) rngDir = rng.NextDirection3D();  
    fp3 dir = (fp3)(target.Body.Position - Body.Position) + rngDir;
    dir.Normalize(); Body.BecomeDynamic(1);
    Body.LinearMomentum = fixmath.MagnitudeClamp(
                          fixmath.Lerp(lastDir + rngDir, dir, 
                          (fp)GetPhysicsProcessDeltaTime() * 
                          fixmath.Clamp01(frameCounter)), t);
    lastDir = dir;
  }

  public override void OnBodyEntered(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
  {
    if (Godot.Engine.IsEditorHint()) return;

    if (other.Owner.Body != target.Body) { return; }

    GD.Print("projectile hit target");

    QueueFree();
  }

  public override void OnBodyExited(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
  {
    if (Godot.Engine.IsEditorHint()) return;
  }
}

