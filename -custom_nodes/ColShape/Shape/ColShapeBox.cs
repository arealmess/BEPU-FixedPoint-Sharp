using Godot;
using System;

using FixMath.NET;
using BEPUutilities;
using Deterministic.FixedPoint;

[Tool]
public partial class ColShapeBox : Shape
{
	public BEPUutilities.Vector3 Size = new BEPUutilities.Vector3(1,1,1);
	[Export]
	private Godot.Vector3 size {
		get => new Godot.Vector3((float)Fix64.FromRaw(SizeX), (float)Fix64.FromRaw(SizeY), (float)Fix64.FromRaw(SizeZ));
		set {
			if (Engine.IsEditorHint()) {  // Avoid any float values changing fixed point raw values when the game runs
				SizeX = ((fp)value.X).value;
				SizeY = ((fp)value.Y).value;
				SizeZ = ((fp)value.Z).value;
				((BoxMesh)Mesh).Size = size;
			}
		}
	}

	private long SizeX = ((fp)1).value;
	[Export]
	private long sizeX {
		get => SizeX;
		set {
			SizeX = value;
			Size = new BEPUutilities.Vector3(Fix64.FromRaw(SizeX), Fix64.FromRaw(SizeY), Fix64.FromRaw(SizeZ));

			// Resize the visual of this body's mesh
			((BoxMesh)Mesh).Size = size;
		}
	}
	private long SizeY = ((fp)1).value;
	[Export]
	private long sizeY {
		get => SizeY;
		set {
			SizeY = value;
			Size = new BEPUutilities.Vector3(Fix64.FromRaw(SizeX), Fix64.FromRaw(SizeY), Fix64.FromRaw(SizeZ));
						
			// Resize the visual of this body's mesh
			((BoxMesh)Mesh).Size = size;
		}
	}
	private long SizeZ = ((fp)1).value;
	[Export]
	private long sizeZ {
		get => SizeZ;
		set {
			SizeZ = value;
			Size = new BEPUutilities.Vector3(Fix64.FromRaw(SizeX), Fix64.FromRaw(SizeY), Fix64.FromRaw(SizeZ));
						
			// Resize the visual of this body's mesh
			((BoxMesh)Mesh).Size = size;
		}
	}
}
