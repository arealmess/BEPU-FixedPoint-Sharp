using Godot;
using System;

using FixMath.NET;
using BEPUutilities;
using Deterministic.FixedPoint;

[Tool]
public partial class ColShapeCylinder : Shape
{
	public fp Height = F64.C2;
	[Export]
	private float height {
		get => (float)Fix64.FromRaw(HeightRaw);
		set {
			if (Engine.IsEditorHint()) {  // Avoid any float values changing fixed point raw values when the game runs
				HeightRaw = ((fp)(decimal)value).value;
				((CylinderMesh)Mesh).Height = height;
			}
		}
	}
	
	public fp Radius = ((fp)1 / (fp)2);
	[Export]
	private float radius {
		get => (float)Fix64.FromRaw(RadiusRaw);
		set {
			if (Engine.IsEditorHint()) {  // Avoid any float values changing fixed point raw values when the game runs
				RadiusRaw = ((fp)(decimal)value).value;
				((CylinderMesh)Mesh).TopRadius = radius;
				((CylinderMesh)Mesh).BottomRadius = radius;
			}
		}
	}

	private long HeightRaw = ((fp)2).value;
	[Export]
	private long heightRaw {
		get => HeightRaw;
		set {
			HeightRaw = value;
			Height = Fix64.FromRaw(HeightRaw);

			((CylinderMesh)Mesh).Height = height;
		}
	}

	private long RadiusRaw = ((fp)1 / (fp)2).value;
	[Export]
	private long radiusRaw {
		get => RadiusRaw;
		set {
			RadiusRaw = value;
			Radius = Fix64.FromRaw(RadiusRaw);

			((CylinderMesh)Mesh).TopRadius = radius;
			((CylinderMesh)Mesh).BottomRadius = radius;
		}
	}
}
