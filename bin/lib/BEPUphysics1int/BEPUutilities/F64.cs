using Deterministic.FixedPoint;
using FixMath.NET;

namespace BEPUutilities
{
#pragma warning disable F64_NUM, CS1591
	public static class F64
	{
		public static readonly fp C0 = (fp)0;
		public static readonly fp C1 = (fp)1;
		public static readonly fp C180 = (fp)180;
		public static readonly fp C2 = (fp)2;
		public static readonly fp C3 = (fp)3;
		public static readonly fp C5 = (fp)5;
		public static readonly fp C6 = (fp)6;
		public static readonly fp C16 = (fp)16;
		public static readonly fp C24 = (fp)24;
		public static readonly fp C50 = (fp)50;
		public static readonly fp C60 = (fp)60;
		public static readonly fp C120 = (fp)120;
		public static readonly fp C0p001 = (fp)0.001m;
		public static readonly fp C0p5 = (fp)0.5m;
		public static readonly fp C0p25 = (fp)0.25m;
		public static readonly fp C1em09 = (fp)1e-9m;
		public static readonly fp C1em9 = (fp)1e-9m;
		public static readonly fp Cm1em9 = (fp)(-1e-9m);
		public static readonly fp C1em14 = (fp)(1e-14m);		
		public static readonly fp C0p1 = (fp)0.1m;
		public static readonly fp OneThird = C1 / C3;
		public static readonly fp C0p75 = (fp)0.75m;
		public static readonly fp C0p15 = (fp)0.15m;
		public static readonly fp C0p3 = (fp)0.3m;
		public static readonly fp C0p0625 = (fp)0.0625m;
		public static readonly fp C0p99 = (fp).99m;
		public static readonly fp C0p9 = (fp).9m;
		public static readonly fp C1p5 = (fp)1.5m;
		public static readonly fp C1p1 = (fp)1.1m;
		public static readonly fp OneEighth = C1 / (fp)8;
		public static readonly fp FourThirds = (fp)4 / C3;
		public static readonly fp TwoFifths = C2 / C5;
		public static readonly fp C0p2 = (fp)0.2m;
		public static readonly fp C0p8 = (fp)0.8m;
		public static readonly fp C0p01 = (fp)0.01m;
		public static readonly fp C1em7 = (fp)1e-7m;
		public static readonly fp C1em5 = (fp)1e-5m;
		public static readonly fp C1em4 = (fp)1e-4m;
		public static readonly fp C1em10 = (fp)1e-10m;
		public static readonly fp Cm0p25 = (fp)(-0.25m);
		public static readonly fp Cm0p9999 = (fp)(-0.9999m);
		public static readonly fp C1m1em12 = C1 - (fp)1e-12m;
		public static readonly fp GoldenRatio = C1 + fixmath.Sqrt(C5) / C2;
		public static readonly fp OneTwelfth = C1 / (fp)12;
		public static readonly fp C0p0833333333 = (fp).0833333333m;
		public static readonly fp C90000 = (fp)90000;
		public static readonly fp C600000 = (fp)600000;
		public static readonly fp OverNineThousand = (fp)0x2329;
  }
}
