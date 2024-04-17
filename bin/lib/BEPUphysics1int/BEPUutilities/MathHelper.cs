using Deterministic.FixedPoint;
using FixMath.NET;
using System;

namespace BEPUutilities
{
  /// <summary>
  /// Contains helper math methods.
  /// </summary>
  public static class MathHelper
  {
    /// <summary>
    /// Approximate value of Pi.
    /// </summary>
    public static readonly fp Pi = fp.pi;

    /// <summary>
    /// Approximate value of Pi multiplied by two.
    /// </summary>
    public static readonly fp TwoPi = fp.pi2;

    /// <summary>
    /// Approximate value of Pi divided by two.
    /// </summary>
    public static readonly fp PiOver2 = fp.pi / F64.C2;

    /// <summary>
    /// Approximate value of Pi divided by four.
    /// </summary>
    public static readonly fp PiOver4 = fp.pi / (fp)4;

    /// <summary>
    /// Calculate remainder of of fp division using same algorithm
    /// as Math.IEEERemainder
    /// </summary>
    /// <param name="dividend">Dividend</param>
    /// <param name="divisor">Divisor</param>
    /// <returns>Remainder</returns>
    public static fp IEEERemainder(fp dividend, fp divisor)
    {
      return dividend - (divisor * Fix64.Round(dividend / divisor));
    }

    /// <summary>
    /// Reduces the angle into a range from -Pi to Pi.
    /// </summary>
    /// <param name="angle">Angle to wrap.</param>
    /// <returns>Wrapped angle.</returns>
    public static fp WrapAngle(fp angle)
    {
      angle = IEEERemainder(angle, TwoPi);
      if (angle < -Pi)
      {
        angle += TwoPi;
        return angle;
      }
      if (angle >= Pi)
      {
        angle -= TwoPi;
      }
      return angle;

    }

    /// <summary>
    /// Clamps a value between a minimum and maximum value.
    /// </summary>
    /// <param name="value">Value to clamp.</param>
    /// <param name="min">Minimum value.  If the value is less than this, the minimum is returned instead.</param>
    /// <param name="max">Maximum value.  If the value is more than this, the maximum is returned instead.</param>
    /// <returns>Clamped value.</returns>
    public static fp Clamp(fp value, fp min, fp max)
    {
      if (value < min)
        return min;
      else if (value > max)
        return max;
      return value;
    }


    /// <summary>
    /// Returns the higher value of the two parameters.
    /// </summary>
    /// <param name="a">First value.</param>
    /// <param name="b">Second value.</param>
    /// <returns>Higher value of the two parameters.</returns>
    public static fp Max(fp a, fp b)
    {
      return a > b ? a : b;
    }

    /// <summary>
    /// Returns the lower value of the two parameters.
    /// </summary>
    /// <param name="a">First value.</param>
    /// <param name="b">Second value.</param>
    /// <returns>Lower value of the two parameters.</returns>
    public static fp Min(fp a, fp b)
    {
      return a < b ? a : b;
    }

    /// <summary>
    /// Converts degrees to radians.
    /// </summary>
    /// <param name="degrees">Degrees to convert.</param>
    /// <returns>Radians equivalent to the input degrees.</returns>
    public static fp ToRadians(fp degrees)
    {
      return degrees * (Pi / F64.C180);
    }

    /// <summary>
    /// Converts radians to degrees.
    /// </summary>
    /// <param name="radians">Radians to convert.</param>
    /// <returns>Degrees equivalent to the input radians.</returns>
    public static fp ToDegrees(fp radians)
    {
      return radians * (F64.C180 / Pi);
    }
  }
}
