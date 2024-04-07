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

    public static readonly fp max        = new(long.MaxValue);
		public static readonly fp min        = new(long.MinValue);
		public static readonly fp usable_max = new(2147483648L);
		public static readonly fp usable_min = -usable_max;

		public static readonly fp _0   = 0;
		public static readonly fp _1   = 1;
		public static readonly fp _2   = 2;
		public static readonly fp _3   = 3;
		public static readonly fp _4   = 4;
		public static readonly fp _5   = 5;
		public static readonly fp _6   = 6;
		public static readonly fp _7   = 7;
		public static readonly fp _8   = 8;
		public static readonly fp _9   = 9;
		public static readonly fp _10  = 10;
		public static readonly fp _99  = 99;
		public static readonly fp _100 = 100;
		public static readonly fp _200 = 200;

		public static readonly fp _0_01 = _1 / _100;
		public static readonly fp _0_02 = _0_01 * 2;
		public static readonly fp _0_03 = _0_01 * 3;
		public static readonly fp _0_04 = _0_01 * 4;
		public static readonly fp _0_05 = _0_01 * 5;
		public static readonly fp OneTwelfth = _1 / 12;
		public static readonly fp _0_10 = _1 / 10;
		public static readonly fp _0_20 = _0_10 * 2;
		public static readonly fp _0_25 = _1 / 4;
		public static readonly fp _0_33 = _1 / 3;
		public static readonly fp _0_50 = _1 / 2;
		public static readonly fp _0_75 = _1 - _0_25;
		public static readonly fp _0_95 = _1 - _0_05;
		public static readonly fp _0_99 = _1 - _0_01;
		public static readonly fp _1_01 = _1 + _0_01;
		public static readonly fp _1_10 = _1 + _0_10;
		public static readonly fp _1_50 = _1 + _0_50;

		public static readonly fp minus_one   = -1;
		public static readonly fp pi          = new(205887L);
		public static readonly fp pi2         = pi * 2;
		public static readonly fp pi_quarter  = pi * _0_25;
		public static readonly fp pi_half     = pi * _0_50;
		public static readonly fp one_div_pi2 = 1 / pi2;
		public static readonly fp deg2rad     = new(1143L);
		public static readonly fp rad2deg     = new(3754936L);
		public static readonly fp epsilon     = new(1);
		public static readonly fp e           = new(178145L);

		[FieldOffset(0)]
		public long value;

		public long   AsLong          => value >> fixlut.PRECISION;
		public int    AsInt           => (int) (value >> fixlut.PRECISION);
		public float  AsFloat         => value / 65536f;
		public float  AsFloatRounded  => (float) Math.Round(value / 65536f, 5);
		public double AsDouble        => value / 65536d;
		public double AsDoubleRounded => Math.Round(value / 65536d, 5);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal fp(long v) {
			value = v;
		}


		#region INT/FP/FP OPERATORS

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator -(fp a) {
			a.value = -a.value;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator +(fp a) {
			a.value = +a.value;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator +(fp a, fp b) {
			a.value += b.value;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator +(fp a, int b) {
			a.value += (long) b << fixlut.PRECISION;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator +(int a, fp b) {
			b.value = ((long) a << fixlut.PRECISION) + b.value;
			return b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator -(fp a, fp b) {
			a.value -= b.value;
			return a;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator -(fp a, int b) {
			a.value -= (long) b << fixlut.PRECISION;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator -(int a, fp b) {
			b.value = ((long) a << fixlut.PRECISION) - b.value;
			return b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator *(fp a, fp b) {
			a.value = a.value * b.value >> fixlut.PRECISION;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator *(fp a, int b) {
			a.value *= b;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator *(int a, fp b) {
			b.value *= a;
			return b;
		}

		//[MethodImpl(MethodImplOptions.AggressiveInlining)]
		//public static fp operator /(fp a, fp b) {
		//	a.value = (a.value << fixlut.PRECISION) / b.value;
		//	return a;
		//}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static int CountLeadingZeroes(ulong x)
		{
			int result = 0;
			while ((x & 0xF000000000000000) == 0) { result += 4; x <<= 4; }
			while ((x & 0x8000000000000000) == 0) { result += 1; x <<= 1; }
			return result;
		}

		public static fp operator /(fp x, fp y)
		{
			var xl = x.value;
			var yl = y.value;

			if (yl == 0)
			{
				return max;
				throw new DivideByZeroException();
			}

			var remainder = (ulong)(xl >= 0 ? xl : -xl);
			var divider = (ulong)(yl >= 0 ? yl : -yl);
			var quotient = 0UL;
			var bitPos = (NUM_BITS / 2) + 1;


			// If the divider is divisible by 2^n, take advantage of it.
			while ((divider & 0xF) == 0 && bitPos >= 4)
			{
				divider >>= 4;
				bitPos -= 4;
			}

			while (remainder != 0 && bitPos >= 0)
			{
				int shift = CountLeadingZeroes(remainder);
				if (shift > bitPos)
				{
					shift = bitPos;
				}
				remainder <<= shift;
				bitPos -= shift;

				var div = remainder / divider;
				remainder %= divider;
				quotient += div << bitPos;

				// Detect overflow
				if ((div & ~(0xFFFFFFFFFFFFFFFF >> bitPos)) != 0)
				{
					return ((xl ^ yl) & long.MinValue) == 0 ? max : min;
				}

				remainder <<= 1;
				--bitPos;
			}

			// Rounding
			++quotient;
			var result = (long)(quotient >> 1);
			if (((xl ^ yl) & long.MinValue) != 0)
			{
				result = -result;
			}

			return new fp(result);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator /(fp a, int b) {
			a.value /= b;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator /(int a, fp b) {
			b.value = ((long) a << 32) / b.value;
			return b;
		}

		//[MethodImpl(MethodImplOptions.AggressiveInlining)]
		//public static fp operator %(fp a, fp b) {
		//	a.value %= b.value;
		//	return a;
		//}

		public static fp operator %(fp x, fp y)
		{
			return new fp(
					x.value == long.MinValue & y.value == -1 ?
					0 :
					x.value % y.value);
		}

    // <summary>
    // Performs modulo as fast as possible; throws if x == MinValue and y == -1.
    // Use the operator (%) for a more reliable but slower modulo.
    // </summary> 
		public static fp FastMod(fp x, fp y)
		{
			return new fp(x.value % y.value);
		} 

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator %(fp a, int b) {
			a.value %= (long) b << fixlut.PRECISION;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp operator %(int a, fp b) {
			b.value = ((long) a << fixlut.PRECISION) % b.value;
			return b;
		}

		#endregion


		#region INT/FP/FP BOOL OPERATORS

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(fp a, fp b) {
			return a.value < b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(fp a, int b) {
			return a.value < (long) b << fixlut.PRECISION;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(int a, fp b) {
			return (long) a << fixlut.PRECISION < b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(fp a, fp b) {
			return a.value <= b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(fp a, int b) {
			return a.value <= (long) b << fixlut.PRECISION;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(int a, fp b) {
			return (long) a << fixlut.PRECISION <= b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(fp a, fp b) {
			return a.value > b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(fp a, int b) {
			return a.value > (long) b << fixlut.PRECISION;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(int a, fp b) {
			return (long) a << fixlut.PRECISION > b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(fp a, fp b) {
			return a.value >= b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(fp a, int b) {
			return a.value >= (long) b << fixlut.PRECISION;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(int a, fp b) {
			return (long) a << fixlut.PRECISION >= b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(fp a, fp b) {
			return a.value == b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(fp a, int b) {
			return a.value == (long) b << fixlut.PRECISION;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(int a, fp b) {
			return (long) a << fixlut.PRECISION == b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(fp a, fp b) {
			return a.value != b.value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(fp a, int b) {
			return a.value != (long) b << fixlut.PRECISION;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(int a, fp b) {
			return (long) a << fixlut.PRECISION != b.value;
		}

    #endregion


    #region CASTS

		// for easy use with ints i.e: fp + 5
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator fp(int value) 
		{
			fp f;
			f.value = (long)value << fixlut.PRECISION;
			return f;
		}

		// slides the fp 16 bits to the right, leaving the integer part
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int(fp value) 
		{
			return (int)(value.value >> fixlut.PRECISION);
		}

    // divides by 2^16 to create a fractional number from the long
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator float(fp value)
    {
      return value.value / 65536f;
    }

    // 64 bit float
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator double(fp value)
    {
      return value.value / 65536d;
    }

    // removes 16 bits of fractional part and leaves a 48 bit int
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator long(fp value) 
		{
			return value.value >> fixlut.PRECISION;
		}

		// multiply by 16 and cast to long
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator fp(decimal value)
		{
			return new fp((long)(value * fixlut.ONE));
		}

		// long divided by 16 and cast to decimal
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator decimal(fp value)
		{
			return (decimal)value.value / fixlut.ONE;
		}

		#endregion


		#region HELPER/IEQUITABLE

		public int CompareTo(fp other) {
			return value.CompareTo(other.value);
		}

		public bool Equals(fp other) {
			return value == other.value;
		}

		public override bool Equals(object obj) {
			return obj is fp other && this == other;
		}

		public override int GetHashCode() {
			return value.GetHashCode();
		}

		public override string ToString() {
			var corrected = Math.Round(AsDouble, 5);
			return corrected.ToString("F5", CultureInfo.InvariantCulture);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp ParseRaw(long value) {
			return new fp(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static fp Parse(long value) {
			return new fp(value << fixlut.PRECISION);
		}

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
			{
				return _0;
			}
			
			bool negative;

			var startIndex = 0;
			if (negative = (value[0] == '-')) {
				startIndex = 1;
			}

			var pointIndex = value.IndexOf('.');
			if (pointIndex < startIndex) {
				if (startIndex == 0) {
					return ParseInteger(value);
				}

				return -ParseInteger(value.Substring(startIndex, value.Length - startIndex));

			}

			var result = new fp();
			
			if (pointIndex > startIndex) {
				var integerString = value.Substring(startIndex, pointIndex - startIndex);
				result += ParseInteger(integerString);
			}


			if (pointIndex == value.Length - 1) {
				return negative ? -result : result;
			}

			var fractionString = value.Substring(pointIndex + 1, value.Length - pointIndex - 1);
			if (fractionString.Length > 0) {
				result += ParseFractions(fractionString);
			}

			return negative ? -result : result;
		}
		
		private static fp ParseInteger(string format) {
			return Parse(long.Parse(format, CultureInfo.InvariantCulture));
		}

		private static fp ParseFractions(string format) {
			format = format.Length < 5 ? format.PadRight(5, '0') : format.Substring(0, 5);
			return ParseRaw(long.Parse(format, CultureInfo.InvariantCulture) * 65536 / 100000);
		}

		public class Comparer : IComparer<fp> {
			public static readonly Comparer instance = new Comparer();

			private Comparer() { }

			int IComparer<fp>.Compare(fp x, fp y) {
				return x.value.CompareTo(y.value);
			}
		}

		public class EqualityComparer : IEqualityComparer<fp> {
			public static readonly EqualityComparer instance = new EqualityComparer();

			private EqualityComparer() { }

			bool IEqualityComparer<fp>.Equals(fp x, fp y) {
				return x.value == y.value;
			}

			int IEqualityComparer<fp>.GetHashCode(fp num) {
				return num.value.GetHashCode();
			}
		}

		#endregion
	}
}
