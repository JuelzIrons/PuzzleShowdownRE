namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal static class NumericUtils
	{
		private const char k_SmallSpace = '\u2009';

		private const char k_DivisionSlash = '/';

		public static global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent ToBase10(float value)
		{
			if (value == 0f)
			{
				return default(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent);
			}
			int num = global::System.MathF.Sign(value);
			value = global::System.MathF.Abs(value);
			float num2 = global::System.MathF.Floor(global::System.MathF.Log10(value));
			float num3 = global::System.MathF.Pow(10f, num2);
			float num4 = value / num3;
			return new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent
			{
				Mantissa = (float)num * num4,
				Exponent = (int)num2
			};
		}

		public static global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent ToBase10(double value)
		{
			if (value == 0.0)
			{
				return default(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent);
			}
			int num = global::System.Math.Sign(value);
			value = global::System.Math.Abs(value);
			double num2 = global::System.Math.Floor(global::System.Math.Log10(value));
			double num3 = global::System.Math.Pow(10.0, num2);
			double num4 = value / num3;
			return new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent
			{
				Mantissa = (float)num * (float)num4,
				Exponent = (int)num2
			};
		}

		public static global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent Base10ToBase1000(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent inputBase10)
		{
			int exponent = (int)global::System.MathF.Floor((float)inputBase10.Exponent / 3f);
			int num = ((inputBase10.Exponent % 3 + 3) % 3) switch
			{
				1 => 10, 
				2 => 100, 
				_ => 1, 
			};
			return new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent
			{
				Mantissa = inputBase10.Mantissa * (float)num,
				Exponent = exponent
			};
		}

		public static float RoundToSignificantDigits(float input, int significantDigits, int inputDigitsAboveDecimal)
		{
			float num = global::System.MathF.Pow(10f, significantDigits - inputDigitsAboveDecimal);
			return global::System.MathF.Round(input * num) / num;
		}

		public static (string leadingNumber, string prefixSymbol) GetLeadingNumberAndPrefixSymbol(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent inputBase1000, float roundedValue, int digitsBelowDecimal)
		{
			global::Unity.Multiplayer.Tools.NetStats.MetricPrefix metricPrefix = (global::Unity.Multiplayer.Tools.NetStats.MetricPrefix)inputBase1000.Exponent;
			if (metricPrefix < global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Atto)
			{
				return (leadingNumber: "0", prefixSymbol: "");
			}
			if (metricPrefix > global::Unity.Multiplayer.Tools.NetStats.MetricPrefix.Exa)
			{
				return (leadingNumber: (inputBase1000.Mantissa >= 0f) ? "+∞" : "-∞", prefixSymbol: "");
			}
			string symbol = global::Unity.Multiplayer.Tools.NetStats.UnitPrefixExtensions.GetSymbol(metricPrefix);
			return (leadingNumber: roundedValue.ToString("N" + digitsBelowDecimal, global::System.Globalization.CultureInfo.CurrentCulture), prefixSymbol: symbol);
		}

		public static (string leadingNumber, string prefixSymbol) GetLeadingNumberAndPrefixSymbol(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent inputBase1000, int significantDigits)
		{
			if (global::System.Math.Abs(inputBase1000.Mantissa - 1000f) < global::System.MathF.Pow(10f, 3 - significantDigits))
			{
				inputBase1000.Mantissa *= 0.001f;
				inputBase1000.Exponent++;
			}
			int digitsAboveDecimal = GetDigitsAboveDecimal(inputBase1000, displayAsPercentage: false);
			int digitsBelowDecimal = GetDigitsBelowDecimal(significantDigits, digitsAboveDecimal);
			float roundedValue = RoundToSignificantDigits(inputBase1000.Mantissa, significantDigits, digitsAboveDecimal);
			return GetLeadingNumberAndPrefixSymbol(inputBase1000, roundedValue, digitsBelowDecimal);
		}

		public static string Base1000ToEngineeringNotation(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent inputBase1000, global::Unity.Multiplayer.Tools.NetStats.BaseUnits units, float roundedValue, int digitsBelowdecimal)
		{
			var (text, text2) = GetLeadingNumberAndPrefixSymbol(inputBase1000, roundedValue, digitsBelowdecimal);
			var (text3, text4) = units.NumeratorAndDenominatorDisplayStrings;
			return text + ((text3 == "") ? text2 : $"{'\u2009'}{text2}{text3}") + ((text4 == "") ? "" : $"{'/'}{text4}");
		}

		public static string Base1000ToEngineeringNotation(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent inputBase1000, int significantDigits, global::Unity.Multiplayer.Tools.NetStats.BaseUnits units)
		{
			var (text, text2) = GetLeadingNumberAndPrefixSymbol(inputBase1000, significantDigits);
			var (text3, text4) = units.NumeratorAndDenominatorDisplayStrings;
			return text + ((text3 == "") ? text2 : $"{'\u2009'}{text2}{text3}") + ((text4 == "") ? "" : $"{'/'}{text4}");
		}

		public static string Base10ToPercentageNotation(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent inputBase10, int significantDigits, global::Unity.Multiplayer.Tools.NetStats.BaseUnits units, float roundedValue)
		{
			float num = 100f * roundedValue * global::System.MathF.Pow(10f, inputBase10.Exponent);
			int num2 = global::System.Math.Max(inputBase10.Exponent + 3, 0);
			string text = num.ToString("N" + global::System.Math.Max(0, significantDigits - num2), global::System.Globalization.CultureInfo.CurrentCulture);
			var (text2, text3) = units.NumeratorAndDenominatorDisplayStrings;
			return text + "%" + ((text2 == "") ? "" : $"{'\u2009'}{text2}") + ((text3 == "") ? "" : $"{'/'}{text3}");
		}

		public static string Base10ToPercentageNotation(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent inputBase10, int significantDigits, global::Unity.Multiplayer.Tools.NetStats.BaseUnits units)
		{
			float roundedValue = RoundToSignificantDigits(inputBase10.Mantissa, significantDigits, 1);
			return Base10ToPercentageNotation(inputBase10, significantDigits, units, roundedValue);
		}

		public static string Base10ToDisplayNotation(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent inputBase10, int significantDigits, global::Unity.Multiplayer.Tools.NetStats.BaseUnits units, bool displayAsPercentage)
		{
			if (!displayAsPercentage)
			{
				return Base1000ToEngineeringNotation(Base10ToBase1000(inputBase10), significantDigits, units);
			}
			return Base10ToPercentageNotation(inputBase10, significantDigits, units);
		}

		public static bool Approximately(double a, double b, double tolerance)
		{
			return global::System.Math.Abs(a - b) < tolerance;
		}

		public static int GetDigitsAboveDecimal(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent inputBase1000, bool displayAsPercentage)
		{
			if (!displayAsPercentage)
			{
				if (!(inputBase1000.Mantissa >= 100f))
				{
					if (!(inputBase1000.Mantissa >= 10f))
					{
						return 1;
					}
					return 2;
				}
				return 3;
			}
			return 1;
		}

		public static int GetDigitsBelowDecimal(int significantDigits, int digitsAboveDecimal)
		{
			return global::System.Math.Max(significantDigits - digitsAboveDecimal, 0);
		}
	}
}
