using Godot;
using System;

using FixMath.NET;
using BEPUphysics;
using BEPUphysics.Entities;
using BEPUutilities;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using Deterministic.FixedPoint;

public partial class Util : Node3D
{
	// Convertes degrees to radians
	public static fp DegToRad(fp degrees)
	{
		return degrees * (fp)0.0174533m;
	}

	// Converts a float value to a fp raw value (Used for animation conversion EditorScript)
	public static long Float64ToFPRaw(double value)
	{
		long convertedValue = ((fp)((decimal)value)).value;

		return convertedValue;
	}
}

