namespace Unity.Multiplayer.Tools.NetStats
{
	public static class MetricIdTypeLibrary
	{
		private static readonly global::System.Collections.Generic.List<global::System.Type> k_Types;

		private static readonly global::System.Collections.Generic.List<string> k_TypeDisplayNames;

		private static readonly global::System.Collections.Generic.List<int[]> k_EnumValues;

		private static readonly global::System.Collections.Generic.List<string[]> k_EnumNames;

		private static readonly global::System.Collections.Generic.List<string[]> k_EnumDisplayNames;

		private static readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricKind[]> k_MetricKinds;

		private static readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.BaseUnits[]> k_Units;

		private static readonly global::System.Collections.Generic.List<bool[]> k_DisplayAsPercentage;

		internal static global::System.Collections.Generic.IReadOnlyList<global::System.Type> Types => k_Types;

		internal static global::System.Collections.Generic.IReadOnlyList<string> TypeDisplayNames => k_TypeDisplayNames;

		static MetricIdTypeLibrary()
		{
			k_Types = new global::System.Collections.Generic.List<global::System.Type>();
			k_TypeDisplayNames = new global::System.Collections.Generic.List<string>();
			k_EnumValues = new global::System.Collections.Generic.List<int[]>();
			k_EnumNames = new global::System.Collections.Generic.List<string[]>();
			k_EnumDisplayNames = new global::System.Collections.Generic.List<string[]>();
			k_MetricKinds = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricKind[]>();
			k_Units = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.BaseUnits[]>();
			k_DisplayAsPercentage = new global::System.Collections.Generic.List<bool[]>();
			global::Unity.Multiplayer.Tools.NetStats.TypeRegistration.RunIfNeeded();
		}

		public static void RegisterType<TEnumType>()
		{
			k_Types.Add(typeof(TEnumType));
		}

		internal static void TypeRegistrationPostProcess()
		{
			k_Types.Sort(delegate(global::System.Type a, global::System.Type b)
			{
				global::Unity.Multiplayer.Tools.NetStats.SortPriority sortPriority = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::Unity.Multiplayer.Tools.NetStats.MetricTypeSortPriorityAttribute>(a)?.SortPriority ?? global::Unity.Multiplayer.Tools.NetStats.SortPriority.Neutral;
				global::Unity.Multiplayer.Tools.NetStats.SortPriority sortPriority2 = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::Unity.Multiplayer.Tools.NetStats.MetricTypeSortPriorityAttribute>(b)?.SortPriority ?? global::Unity.Multiplayer.Tools.NetStats.SortPriority.Neutral;
				int num2 = sortPriority.CompareTo(sortPriority2);
				return (num2 != 0) ? num2 : global::System.StringComparer.InvariantCulture.Compare(a.FullName, b.FullName);
			});
			foreach (global::System.Type k_Type in k_Types)
			{
				string item = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::Unity.Multiplayer.Tools.NetStats.MetricTypeEnumAttribute>(k_Type)?.DisplayName ?? k_Type.Name;
				int[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Cast<int>(k_Type.GetEnumValues()));
				string[] enumNames = k_Type.GetEnumNames();
				global::System.Array.Sort(enumNames, array);
				string[] array2 = new string[array.Length];
				global::Unity.Multiplayer.Tools.NetStats.MetricKind[] array3 = new global::Unity.Multiplayer.Tools.NetStats.MetricKind[array.Length];
				global::Unity.Multiplayer.Tools.NetStats.BaseUnits[] array4 = new global::Unity.Multiplayer.Tools.NetStats.BaseUnits[array.Length];
				bool[] array5 = new bool[array.Length];
				for (int num = 0; num < array.Length; num++)
				{
					string text = enumNames[num];
					global::Unity.Multiplayer.Tools.NetStats.MetricMetadataAttribute metricMetadataAttribute = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::Unity.Multiplayer.Tools.NetStats.MetricMetadataAttribute>(global::System.Linq.Enumerable.FirstOrDefault(k_Type.GetMember(text))?);
					if (metricMetadataAttribute != null)
					{
						array2[num] = metricMetadataAttribute.DisplayName ?? global::Unity.Multiplayer.Tools.Common.StringUtil.AddSpacesToCamelCase(text);
						array3[num] = metricMetadataAttribute.MetricKind;
						array4[num] = metricMetadataAttribute.Units.GetBaseUnits();
						array5[num] = metricMetadataAttribute.DisplayAsPercentage;
					}
					ref string reference = ref array2[num];
					if (reference == null)
					{
						reference = global::Unity.Multiplayer.Tools.Common.StringUtil.AddSpacesToCamelCase(text);
					}
					if (array3[num] == global::Unity.Multiplayer.Tools.NetStats.MetricKind.Counter)
					{
						global::Unity.Multiplayer.Tools.NetStats.BaseUnits baseUnits = array4[num];
						array4[num] = baseUnits.WithSeconds((sbyte)(baseUnits.SecondsExponent - 1));
					}
				}
				k_TypeDisplayNames.Add(item);
				k_EnumValues.Add(array);
				k_EnumNames.Add(enumNames);
				k_EnumDisplayNames.Add(array2);
				k_MetricKinds.Add(array3);
				k_Units.Add(array4);
				k_DisplayAsPercentage.Add(array5);
			}
		}

		internal static bool IsValidTypeIndex(int index)
		{
			if (0 <= index)
			{
				return index < k_Types.Count;
			}
			return false;
		}

		internal static int GetTypeIndex(global::System.Type type)
		{
			return k_Types.IndexOf(type);
		}

		internal static global::System.Type GetType(int typeIndex)
		{
			return k_Types[typeIndex];
		}

		internal static bool ContainsType(global::System.Type type)
		{
			return k_Types.Contains(type);
		}

		internal static global::System.Collections.Generic.IReadOnlyList<int> GetEnumValues(int typeIndex)
		{
			return k_EnumValues[typeIndex];
		}

		internal static global::System.Collections.Generic.IReadOnlyList<string> GetEnumNames(int typeIndex)
		{
			return k_EnumNames[typeIndex];
		}

		[global::JetBrains.Annotations.NotNull]
		internal static string GetEnumName(int typeIndex, int enumValue)
		{
			return GetEnumMetadata(k_EnumNames, typeIndex, enumValue) ?? enumValue.ToString();
		}

		internal static global::Unity.Multiplayer.Tools.NetStats.MetricKind GetEnumMetricKind(int typeIndex, int enumValue)
		{
			return GetEnumMetadata(k_MetricKinds, typeIndex, enumValue);
		}

		internal static global::System.Collections.Generic.IReadOnlyList<string> GetEnumDisplayNames(int typeIndex)
		{
			return k_EnumDisplayNames[typeIndex];
		}

		[global::JetBrains.Annotations.NotNull]
		internal static string GetEnumDisplayName(int typeIndex, int enumValue)
		{
			return GetEnumMetadata(k_EnumDisplayNames, typeIndex, enumValue) ?? "";
		}

		internal static global::Unity.Multiplayer.Tools.NetStats.BaseUnits GetEnumUnit(int typeIndex, int enumValue)
		{
			return GetEnumMetadata(k_Units, typeIndex, enumValue);
		}

		internal static bool GetDisplayAsPercentage(int typeIndex, int enumValue)
		{
			return GetEnumMetadata(k_DisplayAsPercentage, typeIndex, enumValue);
		}

		private static T GetEnumMetadata<T>(global::System.Collections.Generic.List<T[]> data, int typeIndex, int enumValue)
		{
			if (typeIndex >= k_EnumValues.Count)
			{
				return default(T);
			}
			int num = global::System.Array.IndexOf(k_EnumValues[typeIndex], enumValue);
			if (num != -1)
			{
				return data[typeIndex][num];
			}
			return default(T);
		}
	}
}
