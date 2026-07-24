namespace Unity.Multiplayer.Tools.NetStats
{
	internal readonly struct BaseUnits
	{
		private static readonly char[] k_Superscripts = new char[10] { '⁰', '¹', '²', '³', '⁴', '⁵', '⁶', '⁷', '⁸', '⁹' };

		internal sbyte BytesExponent { get; }

		internal sbyte SecondsExponent { get; }

		internal (string, string) NumeratorAndDenominatorDisplayStrings
		{
			get
			{
				string str = "";
				string str2 = "";
				for (global::Unity.Multiplayer.Tools.NetStats.BaseUnit baseUnit = global::Unity.Multiplayer.Tools.NetStats.BaseUnit.Byte; baseUnit < (global::Unity.Multiplayer.Tools.NetStats.BaseUnit)2; baseUnit++)
				{
					sbyte exponent = GetExponent(baseUnit);
					if (exponent > 0)
					{
						AddUnit(baseUnit, exponent, ref str);
					}
					else if (exponent < 0)
					{
						AddUnit(baseUnit, global::System.Math.Abs(exponent), ref str2);
					}
				}
				return (str, str2);
				static void AddUnit(global::Unity.Multiplayer.Tools.NetStats.BaseUnit unit, sbyte b, ref string reference)
				{
					reference += unit.GetSymbol();
					if (b > 1)
					{
						if (b >= 100)
						{
							reference += k_Superscripts[b / 100];
							b %= 100;
						}
						if (b >= 10)
						{
							reference += k_Superscripts[b / 10];
							b %= 10;
						}
						reference += k_Superscripts[b / 10];
					}
				}
			}
		}

		internal string DisplayString
		{
			get
			{
				var (text, text2) = NumeratorAndDenominatorDisplayStrings;
				return text + ((text2 == "") ? "" : ("/" + text2));
			}
		}

		public BaseUnits(sbyte bytesExponent = 0, sbyte secondsExponent = 0)
		{
			BytesExponent = bytesExponent;
			SecondsExponent = secondsExponent;
		}

		public global::Unity.Multiplayer.Tools.NetStats.BaseUnits WithSeconds(sbyte seconds)
		{
			return new global::Unity.Multiplayer.Tools.NetStats.BaseUnits(BytesExponent, seconds);
		}

		public bool Equals(global::Unity.Multiplayer.Tools.NetStats.BaseUnits other)
		{
			if (BytesExponent == other.BytesExponent)
			{
				return SecondsExponent == other.SecondsExponent;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::Unity.Multiplayer.Tools.NetStats.BaseUnits other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return global::System.HashCode.Combine(BytesExponent, SecondsExponent);
		}

		internal sbyte GetExponent(global::Unity.Multiplayer.Tools.NetStats.BaseUnit unit)
		{
			return unit switch
			{
				global::Unity.Multiplayer.Tools.NetStats.BaseUnit.Byte => BytesExponent, 
				global::Unity.Multiplayer.Tools.NetStats.BaseUnit.Second => SecondsExponent, 
				_ => throw new global::System.ArgumentException($"Unhandled BaseUnit {unit}"), 
			};
		}

		public override string ToString()
		{
			return DisplayString;
		}
	}
}
