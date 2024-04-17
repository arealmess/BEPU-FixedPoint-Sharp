using Godot;
using System;

using FixMath.NET;
using BEPUutilities;
using Deterministic.FixedPoint;

[Tool]
public partial class ColShapeSphere : Shape
{
	public fp Radius = ((fp)1 / (fp)2);
	[Export]
	private float radius {
		get => (float)Fix64.FromRaw(RadiusRaw);
		set {
			if (Engine.IsEditorHint()) {  // Avoid any float values changing fixed point raw values when the game runs
				RadiusRaw = ((fp)(decimal)value).value;
			}

			((SphereMesh)Mesh).Radius = radius;
			((SphereMesh)Mesh).Height = radius * 2;
		}
	}

	private long RadiusRaw = ((fp)1 / (fp)2).value;
	[Export]
	private long radiusRaw {
		get => RadiusRaw;
		set {
			RadiusRaw = value;
			Radius = Fix64.FromRaw(RadiusRaw);
			((SphereMesh)Mesh).Radius = radius;
			((SphereMesh)Mesh).Height = radius * 2;
		}
	}
}
