namespace Unity.Multiplayer.Tools.Common
{
	internal static class EnumContinuity
	{
		public static (int min, int max, int uniqueValueCount) GetMinMaxAndUniqueValueCount<TEnum>() where TEnum : unmanaged, global::System.Enum
		{
			TEnum[] values = global::Unity.Multiplayer.Tools.Common.EnumUtil.GetValues<TEnum>();
			int num = int.MinValue;
			int num2 = int.MaxValue;
			global::System.Collections.Generic.HashSet<int> hashSet = new global::System.Collections.Generic.HashSet<int>();
			TEnum[] array = values;
			for (int i = 0; i < array.Length; i++)
			{
				int num3 = global::System.Convert.ToInt32(array[i]);
				num = global::System.Math.Max(num, num3);
				num2 = global::System.Math.Min(num2, num3);
				hashSet.Add(num3);
			}
			return (min: num2, max: num, uniqueValueCount: hashSet.Count);
		}

		public static int ValidateEnumForEnumMap<TEnum, TValue>() where TEnum : unmanaged, global::System.Enum
		{
			if (global::System.Enum.GetUnderlyingType(typeof(TEnum)) != typeof(int))
			{
				throw new global::Unity.Multiplayer.Tools.Common.UnhandledEnumBackingTypeException<TEnum, TValue>();
			}
			var (num, num2, num3) = GetMinMaxAndUniqueValueCount<TEnum>();
			if (num3 <= 0)
			{
				throw new global::Unity.Multiplayer.Tools.Common.EmptyEnumException<TEnum, TValue>();
			}
			if (num != 0)
			{
				throw new global::Unity.Multiplayer.Tools.Common.NonZeroEnumMinimumValueException<TEnum, TValue>();
			}
			if (num2 - num + 1 != num3)
			{
				throw new global::Unity.Multiplayer.Tools.Common.DiscontinuousEnumException<TEnum, TValue>();
			}
			return num3;
		}
	}
}
