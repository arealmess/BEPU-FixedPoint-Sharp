using Godot;
using System;

using FixMath.NET;
using BEPUphysics;
using BEPUphysics.Entities;
using BEPUutilities;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using Deterministic.FixedPoint;


public partial class cameraRigPlayer : Node3D
{
	InputHandler inputHandler;
	BEPUutilities.Vector2 inputDirRight = new BEPUutilities.Vector2();

	public BEPUutilities.Vector2 cameraRotation = new BEPUutilities.Vector2();
	public BEPUutilities.Vector2 cameraDir = new BEPUutilities.Vector2();

	fp sensitivity = (fp)1.6m;
	BEPUutilities.Vector2 rotationLimit = new BEPUutilities.Vector2(90, 360);

	Node3D Pivot;

	public override void _Ready()
	{
		inputHandler = GetNode<InputHandler>("/root/InputHandler");
		Pivot = GetNode<Node3D>("Pivot");
	}

	public override void _PhysicsProcess(double delta)
	{
		inputDirRight = inputHandler.GetJoyAxisRight();
		cameraDir.X = (fp)Pivot.GlobalTransform.Basis.Z.X;
		cameraDir.Y = (fp)Pivot.GlobalTransform.Basis.Z.Y;

		MoveCamera();
	}

	public void MoveCamera()
	{
		BEPUutilities.Vector2 rotAmount;

		rotAmount.X = -inputDirRight.Y * sensitivity;
		rotAmount.Y = -inputDirRight.X * sensitivity;

		cameraRotation += rotAmount;

		// Correct rotation if it goes past 360 degrees
		if (cameraRotation.X >= (fp)360) {
			cameraRotation.X -= (fp)360;
		}
		if (cameraRotation.X <= (fp)(-360)) {
			cameraRotation.X += (fp)360;
		}
		if (cameraRotation.Y >= (fp)360) {
			cameraRotation.Y -= (fp)360;
		}
		if (cameraRotation.Y <= (fp)(-360)) {
			cameraRotation.Y += (fp)360;
		}

		// Limit rotation
		if (cameraRotation.X > rotationLimit.X) {
			cameraRotation.X = rotationLimit.X;
		}
		if (cameraRotation.X < -rotationLimit.X) {
			cameraRotation.X = -rotationLimit.X;
		}
		if (cameraRotation.Y > rotationLimit.Y) {
			cameraRotation.Y = rotationLimit.Y;
		}
		if (cameraRotation.Y < -rotationLimit.Y) {
			cameraRotation.Y = -rotationLimit.Y;
		}

		Set("rotation", new Godot.Vector3(0,(float)(cameraRotation.Y * (fp)0.0174533m),0));
		Pivot.Set("rotation", new Godot.Vector3((float)(cameraRotation.X * (fp)0.0174533m),0,0));
	}
}


