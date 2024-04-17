using BEPUutilities;
using FixMath.NET;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deterministic.FixedPoint {
	[Serializable]
	[StructLayout(LayoutKind.Explicit, Size = SIZE)]
	public struct fp : IEquatable<fp>, IComparable<fp> {
		public const int SIZE = 8;
		const int NUM_BITS = 64;

		public static readonly fp usable_max = new(2147483648L);
		public static readonly fp usable_min = -usable_max;
		public static readonly fp minus_one = -F64.C1;
		public static readonly fp pi = new(205887L);
		public static readonly fp pi2 = pi * F64.C2;
		public static readonly fp pi_quarter = pi * F64.C0p25;
		public static readonly fp pi_half = pi * F64.C0p5;
		public static readonly fp one_div_pi2 = F64.C1 / pi2;
		public static readonly fp deg2rad = new(1143L);
		public static readonly fp rad2deg = new(3754936L);
		public static readonly fp epsilon = new(1);
		public static readonly fp e = new(178145L);

		[FieldOffset(0)]
		public long value;

		public readonly long AsLong => value >> fixlut.PRECISION;
		public readonly int AsInt => (int)(value >> fixlut.PRECISION);
		public readonly float AsFloat => value / 65536f;
		public readonly float AsFloatRounded => (float)Math.Round(value / 65536f, 5);
		public readonly double AsDouble => value / 65536d;
		public readonly double AsDoubleRounded => Math.Round(value / 65536d, 5);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal fp(long v) { value = v; }


		#region INT/FP/FP OPERATORS

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator -(fp a) { a.value = -a.value; return a; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator +(fp a) { a.value = +a.value; return a; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator +(fp a, fp b) { a.value += b.value; return a; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator +(fp a, int b) { a.value += (long)b << fixlut.PRECISION; return a; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator +(int a, fp b) { b.value = ((long)a << fixlut.PRECISION) + b.value; return b; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator -(fp a, fp b) { a.value -= b.value; return a; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator -(fp a, int b) { a.value -= (long)b << fixlut.PRECISION; return a; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator -(int a, fp b) { b.value = ((long)a << fixlut.PRECISION) - b.value; return b; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator *(fp x, fp y) 
		{
			x.value = x.value * y.value >> fixlut.PRECISION;
			return x;

			//var xl = x.value;
			//var yl = y.value;

			//var xlo = (ulong)(xl & 0x000000000000FFFF);
			//var xhi = xl >> 16;
			//var ylo = (ulong)(yl & 0x000000000000FFFF);
			//var yhi = yl >> 16;

			//var lolo = xlo * ylo;
			//var lohi = (long)xlo * yhi;
			//var hilo = xhi * (long)ylo;
			//var hihi = xhi * yhi;

			//var loResult = lolo >> 16;
			//var midResult1 = lohi;
			//var midResult2 = hilo;
			//var hiResult = hihi << 16;

			//var sum = (long)loResult + midResult1 + midResult2 + hiResult;
			//return new fp(sum);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator *(fp a, int b) { a.value *= b; return a; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator *(int a, fp b) { b.value *= a; return b; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator /(fp x, fp y)
		{

			if (y.value == 0)
			{
				//Godot.GD.Print("Attempted divide by zero"); 
				return Fix64.MaxValue;
			}

			x.value = (x.value << fixlut.PRECISION) / y.value;
			return x;

			//var xl = x.value;
			//var yl = y.value;

			//if (yl == 0) { return Fix64.MaxValue; throw new DivideByZeroException(); }

			//var remainder = (ulong)(xl >= 0 ? xl : -xl);
			//var divider = (ulong)(yl >= 0 ? yl : -yl);
			//var quotient = 0UL;
			//var bitPos = fixlut.PRECISION + 1; // + sign bit 

			//// If the divider is divisible by 2^n, take advantage of it.
			//while ((divider & 0xF) == 0 && bitPos >= 4)
			//{
			//	divider >>= 4;
			//	bitPos -= 4;
			//}

			//while (remainder != 0 && bitPos >= 0)
			//{
			//	int shift = fixmath.CountLeadingZeroes(remainder);
			//	if (shift > bitPos)
			//		shift = bitPos;
			//	remainder <<= shift;
			//	bitPos -= shift;

			//	var div = remainder / divider;
			//	remainder %= divider;
			//	quotient += div << bitPos;

			//	// Detect overflow
			//	if ((div & ~(0xFFFFFFFFFFFFFFFF >> bitPos)) != 0)
			//		return ((xl ^ yl) & long.MinValue) == 0 ? Fix64.MaxValue : Fix64.MinValue;

			//	remainder <<= 1;
			//	--bitPos;
			//}

			//// Rounding
			//++quotient;
			//var result = (long)(quotient >> 1);
			//if (((xl ^ yl) & long.MinValue) != 0) result = -result;

			//return new fp(result);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator %(fp x, fp y)
		{ return new fp( x.value == Fix64.MinValue & y.value == -F64.C1.value ? F64.C0.value : x.value % y.value); }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator %(fp a, int b) { a.value %= (long)b << fixlut.PRECISION; return a; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp operator %(int a, fp b) { b.value = ((long)a << fixlut.PRECISION) % b.value; return b; }

    /// <summary>
    /// Performs modulo as fast as possible; throws if x == MinValue and y == -1.
    /// Use the operator (%) for a more reliable but slower modulo.
    /// </summary> 
    public static fp FastMod(fp x, fp y) { return new fp(x.value % y.value); }  


		#endregion


		#region INT/FP/FP BOOL OPERATORS


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(fp a, fp b) { return a.value < b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(fp a, int b) { return a.value < (long) b << fixlut.PRECISION; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(int a, fp b) { return (long) a << fixlut.PRECISION < b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(fp a, fp b) { return a.value <= b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(fp a, int b) { return a.value <= (long) b << fixlut.PRECISION; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(int a, fp b) { return (long) a << fixlut.PRECISION <= b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(fp a, fp b) { return a.value > b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(fp a, int b) { return a.value > (long) b << fixlut.PRECISION; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(int a, fp b) { return (long) a << fixlut.PRECISION > b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(fp a, fp b) { return a.value >= b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(fp a, int b) { return a.value >= (long) b << fixlut.PRECISION; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(int a, fp b) { return (long) a << fixlut.PRECISION >= b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(fp a, fp b) { return a.value == b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(fp a, int b) { return a.value == (long) b << fixlut.PRECISION; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(int a, fp b) { return (long) a << fixlut.PRECISION == b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(fp a, fp b) { return a.value != b.value; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(fp a, int b) { return a.value != (long) b << fixlut.PRECISION; }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(int a, fp b) { return (long) a << fixlut.PRECISION != b.value; }


    #endregion


    #region CASTS


    /// <summary>
    /// Be aware of implications of this in some regards of determinism.
    /// </summary> 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator fp(float value)
    {
      //return new fp((long)(value * fixlut.ONE + 0.5f * (value < 0 ? -1 : 1)));
      return new fp((long)(value * fixlut.ONE));
    }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator fp(int value) { fp f; f.value = (long)value << fixlut.PRECISION; return f; }


    // slides the fp 16 bits to the right, leaving the integer part
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int(fp value) { return (int)(value.value >> fixlut.PRECISION); }


    // divides by 2^16 to create a fractional number from the long
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator float(fp value) { return value.value / 65536f; }


    // 64 bit float
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator double(fp value) { return value.value / 65536d; }


    // removes 16 bits of fractional part and leaves a 48 bit int
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator long(fp value)  { return value.value >> fixlut.PRECISION; }


		// multiply by 16 and cast to long
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator fp(decimal value) { return new fp((long)(value * fixlut.ONE)); }


		// long divided by 16 and cast to decimal
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator decimal(fp value) { return (decimal)value.value / fixlut.ONE; }


		#endregion


		#region HELPER/IEQUITABLE

		public int CompareTo(fp other) { return value.CompareTo(other.value); }

		public bool Equals(fp other) { return value == other.value; }

		public override bool Equals(object obj) { return obj is fp other && this == other; }

		public override int GetHashCode() { return value.GetHashCode(); }

		public override string ToString() {
			var corrected = Math.Round(AsDouble, 5);
			return corrected.ToString("F5", CultureInfo.InvariantCulture);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp ParseRaw(long value) { return new fp(value); }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp Parse(long value) { return new fp(value << fixlut.PRECISION); }


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp ParseUnsafe(float value) {
			return new fp((long) (value * fixlut.ONE + 0.5f * (value < 0 ? -1 : 1)));
		}

		public static fp ParseUnsafe(string value) {
			var doubleValue = double.Parse(value, CultureInfo.InvariantCulture);
			var longValue   = (long) (doubleValue * fixlut.ONE + 0.5d * (doubleValue < 0 ? -1 : 1));
			return new fp(longValue);
		}

		/// <summary>
		/// Deterministically parses FP value out of a string
		/// </summary>
		/// <param name="value">Trimmed string to parse</param>
		public static fp Parse(string value) {
			if (string.IsNullOrEmpty(value))
			return F64.C0;
			
			bool negative;

			var startIndex = 0;
			if (negative = (value[0] == '-')) startIndex = 1;

			var pointIndex = value.IndexOf('.');
			if (pointIndex < startIndex) {
				if (startIndex == 0) return ParseInteger(value);

				return -ParseInteger(value[startIndex..]);

			}

			var result = new fp();
			
			if (pointIndex > startIndex) {
				var integerString = value.Substring(startIndex, pointIndex - startIndex);
				result += ParseInteger(integerString);
			}


			if (pointIndex == value.Length - 1) return negative ? -result : result;

			var fractionString = value.Substring(pointIndex + 1, value.Length - pointIndex - 1);
			if (fractionString.Length > 0) result += ParseFractions(fractionString);

			return negative ? -result : result;
		}
		
		private static fp ParseInteger(string format) {
			return Parse(long.Parse(format, CultureInfo.InvariantCulture));
		}

		private static fp ParseFractions(string format) {
			format = format.Length < 5 ? format.PadRight(5, '0') : format[..5];
			return ParseRaw(long.Parse(format, CultureInfo.InvariantCulture) * 65536 / 100000);
		}

		public class Comparer : IComparer<fp> {
			public static readonly Comparer instance = new();

			private Comparer() { }

			int IComparer<fp>.Compare(fp x, fp y) { return x.value.CompareTo(y.value); }
		}

		public class EqualityComparer : IEqualityComparer<fp> 
		{
			public static readonly EqualityComparer instance = new();

			private EqualityComparer() { }

			bool IEqualityComparer<fp>.Equals(fp x, fp y) { return x.value == y.value; }

			int IEqualityComparer<fp>.GetHashCode(fp num) { return num.value.GetHashCode(); } 
		}

		#endregion
	}
}
