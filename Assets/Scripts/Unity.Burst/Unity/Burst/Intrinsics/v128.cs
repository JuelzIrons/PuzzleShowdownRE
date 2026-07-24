namespace Unity.Burst.Intrinsics
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Burst.Intrinsics.V128DebugView))]
	public struct v128
	{
		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public byte Byte0;

		[global::System.Runtime.InteropServices.FieldOffset(1)]
		public byte Byte1;

		[global::System.Runtime.InteropServices.FieldOffset(2)]
		public byte Byte2;

		[global::System.Runtime.InteropServices.FieldOffset(3)]
		public byte Byte3;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		public byte Byte4;

		[global::System.Runtime.InteropServices.FieldOffset(5)]
		public byte Byte5;

		[global::System.Runtime.InteropServices.FieldOffset(6)]
		public byte Byte6;

		[global::System.Runtime.InteropServices.FieldOffset(7)]
		public byte Byte7;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public byte Byte8;

		[global::System.Runtime.InteropServices.FieldOffset(9)]
		public byte Byte9;

		[global::System.Runtime.InteropServices.FieldOffset(10)]
		public byte Byte10;

		[global::System.Runtime.InteropServices.FieldOffset(11)]
		public byte Byte11;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public byte Byte12;

		[global::System.Runtime.InteropServices.FieldOffset(13)]
		public byte Byte13;

		[global::System.Runtime.InteropServices.FieldOffset(14)]
		public byte Byte14;

		[global::System.Runtime.InteropServices.FieldOffset(15)]
		public byte Byte15;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public sbyte SByte0;

		[global::System.Runtime.InteropServices.FieldOffset(1)]
		public sbyte SByte1;

		[global::System.Runtime.InteropServices.FieldOffset(2)]
		public sbyte SByte2;

		[global::System.Runtime.InteropServices.FieldOffset(3)]
		public sbyte SByte3;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		public sbyte SByte4;

		[global::System.Runtime.InteropServices.FieldOffset(5)]
		public sbyte SByte5;

		[global::System.Runtime.InteropServices.FieldOffset(6)]
		public sbyte SByte6;

		[global::System.Runtime.InteropServices.FieldOffset(7)]
		public sbyte SByte7;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public sbyte SByte8;

		[global::System.Runtime.InteropServices.FieldOffset(9)]
		public sbyte SByte9;

		[global::System.Runtime.InteropServices.FieldOffset(10)]
		public sbyte SByte10;

		[global::System.Runtime.InteropServices.FieldOffset(11)]
		public sbyte SByte11;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public sbyte SByte12;

		[global::System.Runtime.InteropServices.FieldOffset(13)]
		public sbyte SByte13;

		[global::System.Runtime.InteropServices.FieldOffset(14)]
		public sbyte SByte14;

		[global::System.Runtime.InteropServices.FieldOffset(15)]
		public sbyte SByte15;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public ushort UShort0;

		[global::System.Runtime.InteropServices.FieldOffset(2)]
		public ushort UShort1;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		public ushort UShort2;

		[global::System.Runtime.InteropServices.FieldOffset(6)]
		public ushort UShort3;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public ushort UShort4;

		[global::System.Runtime.InteropServices.FieldOffset(10)]
		public ushort UShort5;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public ushort UShort6;

		[global::System.Runtime.InteropServices.FieldOffset(14)]
		public ushort UShort7;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public short SShort0;

		[global::System.Runtime.InteropServices.FieldOffset(2)]
		public short SShort1;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		public short SShort2;

		[global::System.Runtime.InteropServices.FieldOffset(6)]
		public short SShort3;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public short SShort4;

		[global::System.Runtime.InteropServices.FieldOffset(10)]
		public short SShort5;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public short SShort6;

		[global::System.Runtime.InteropServices.FieldOffset(14)]
		public short SShort7;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public uint UInt0;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		public uint UInt1;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public uint UInt2;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public uint UInt3;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public int SInt0;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		public int SInt1;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public int SInt2;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public int SInt3;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public ulong ULong0;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public ulong ULong1;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public long SLong0;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public long SLong1;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public float Float0;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		public float Float1;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public float Float2;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		public float Float3;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public double Double0;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public double Double1;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::Unity.Burst.Intrinsics.v64 Lo64;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		public global::Unity.Burst.Intrinsics.v64 Hi64;

		public v128(byte b)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			Byte0 = (Byte1 = (Byte2 = (Byte3 = (Byte4 = (Byte5 = (Byte6 = (Byte7 = (Byte8 = (Byte9 = (Byte10 = (Byte11 = (Byte12 = (Byte13 = (Byte14 = (Byte15 = b)))))))))))))));
		}

		public v128(byte a, byte b, byte c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k, byte l, byte m, byte n, byte o, byte p)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			Byte0 = a;
			Byte1 = b;
			Byte2 = c;
			Byte3 = d;
			Byte4 = e;
			Byte5 = f;
			Byte6 = g;
			Byte7 = h;
			Byte8 = i;
			Byte9 = j;
			Byte10 = k;
			Byte11 = l;
			Byte12 = m;
			Byte13 = n;
			Byte14 = o;
			Byte15 = p;
		}

		public v128(sbyte b)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			SByte0 = (SByte1 = (SByte2 = (SByte3 = (SByte4 = (SByte5 = (SByte6 = (SByte7 = (SByte8 = (SByte9 = (SByte10 = (SByte11 = (SByte12 = (SByte13 = (SByte14 = (SByte15 = b)))))))))))))));
		}

		public v128(sbyte a, sbyte b, sbyte c, sbyte d, sbyte e, sbyte f, sbyte g, sbyte h, sbyte i, sbyte j, sbyte k, sbyte l, sbyte m, sbyte n, sbyte o, sbyte p)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			SByte0 = a;
			SByte1 = b;
			SByte2 = c;
			SByte3 = d;
			SByte4 = e;
			SByte5 = f;
			SByte6 = g;
			SByte7 = h;
			SByte8 = i;
			SByte9 = j;
			SByte10 = k;
			SByte11 = l;
			SByte12 = m;
			SByte13 = n;
			SByte14 = o;
			SByte15 = p;
		}

		public v128(short v)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			SShort0 = (SShort1 = (SShort2 = (SShort3 = (SShort4 = (SShort5 = (SShort6 = (SShort7 = v)))))));
		}

		public v128(short a, short b, short c, short d, short e, short f, short g, short h)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			SShort0 = a;
			SShort1 = b;
			SShort2 = c;
			SShort3 = d;
			SShort4 = e;
			SShort5 = f;
			SShort6 = g;
			SShort7 = h;
		}

		public v128(ushort v)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			UShort0 = (UShort1 = (UShort2 = (UShort3 = (UShort4 = (UShort5 = (UShort6 = (UShort7 = v)))))));
		}

		public v128(ushort a, ushort b, ushort c, ushort d, ushort e, ushort f, ushort g, ushort h)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			UShort0 = a;
			UShort1 = b;
			UShort2 = c;
			UShort3 = d;
			UShort4 = e;
			UShort5 = f;
			UShort6 = g;
			UShort7 = h;
		}

		public v128(int v)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			SInt0 = (SInt1 = (SInt2 = (SInt3 = v)));
		}

		public v128(int a, int b, int c, int d)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			SInt0 = a;
			SInt1 = b;
			SInt2 = c;
			SInt3 = d;
		}

		public v128(uint v)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			UInt0 = (UInt1 = (UInt2 = (UInt3 = v)));
		}

		public v128(uint a, uint b, uint c, uint d)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			UInt0 = a;
			UInt1 = b;
			UInt2 = c;
			UInt3 = d;
		}

		public v128(float f)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			Float0 = (Float1 = (Float2 = (Float3 = f)));
		}

		public v128(float a, float b, float c, float d)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			Float0 = a;
			Float1 = b;
			Float2 = c;
			Float3 = d;
		}

		public v128(double f)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			Double0 = (Double1 = f);
		}

		public v128(double a, double b)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			Double0 = a;
			Double1 = b;
		}

		public v128(long f)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			SLong0 = (SLong1 = f);
		}

		public v128(long a, long b)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			SLong0 = a;
			SLong1 = b;
		}

		public v128(ulong f)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			ULong0 = (ULong1 = f);
		}

		public v128(ulong a, ulong b)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			ULong0 = a;
			ULong1 = b;
		}

		public v128(global::Unity.Burst.Intrinsics.v64 lo, global::Unity.Burst.Intrinsics.v64 hi)
		{
			this = default(global::Unity.Burst.Intrinsics.v128);
			Lo64 = lo;
			Hi64 = hi;
		}
	}
}
