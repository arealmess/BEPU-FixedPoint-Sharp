using Godot;
using System;

using FixMath.NET;
using BEPUutilities;
using Deterministic.FixedPoint;

[Tool]
public partial class ColShapeCapsule : Shape
{
	public fp Height = (fp)1;
	[Export]
	private float height {
		get => (float)Fix64.FromRaw(HeightRaw);
		set {
			if (Engine.IsEditorHint()) {  // Avoid any float values changing fixed point raw values when the game runs
				HeightRaw = ((fp)value).value;
				((CapsuleMesh)Mesh).Height = height + (radius * 2);
			}
		}
	}

	public fp Radius = ((fp)1 / (fp)2);
	[Export]
	private float radius {
		get => (float)Fix64.FromRaw(RadiusRaw);
		set {
			if (Engine.IsEditorHint()) {  // Avoid any float values changing fixed point raw values when the game runs
				RadiusRaw = ((fp)value).value;
				((CapsuleMesh)Mesh).Radius = radius;
			}
		}
	}

	private long HeightRaw = ((fp)1).value;
	[Export]
	private long heightRaw {
		get => HeightRaw;
		set {
			HeightRaw = value;
			Height = Fix64.FromRaw(HeightRaw);

			((CapsuleMesh)Mesh).Height = height + (radius * 2);
		}
	}

	private long RadiusRaw = ((fp)1 / (fp)2).value;
	[Export]
	private long radiusRaw {
		get => RadiusRaw;
		set {
			RadiusRaw = value;
			Radius = Fix64.FromRaw(RadiusRaw);

			((CapsuleMesh)Mesh).Radius = radius;
		}
	}
}
