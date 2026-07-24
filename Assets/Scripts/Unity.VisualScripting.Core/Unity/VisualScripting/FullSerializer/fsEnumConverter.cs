namespace Unity.VisualScripting.FullSerializer
{
	public class fsEnumConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			return global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(type).IsEnum;
		}

		public override bool RequestCycleSupport(global::System.Type storageType)
		{
			return false;
		}

		public override bool RequestInheritanceSupport(global::System.Type storageType)
		{
			return false;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return global::System.Enum.ToObject(storageType, (object)0);
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			if (Serializer.Config.SerializeEnumsAsInteger)
			{
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData(global::System.Convert.ToInt64(instance));
			}
			else if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetAttribute<global::System.FlagsAttribute>(storageType) != null)
			{
				long num = global::System.Convert.ToInt64(instance);
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
				bool flag = true;
				foreach (object value in global::System.Enum.GetValues(storageType))
				{
					long num2 = global::System.Convert.ToInt64(value);
					if (num2 != 0L && (num & num2) == num2)
					{
						if (!flag)
						{
							stringBuilder.Append(",");
						}
						flag = false;
						stringBuilder.Append(value.ToString());
					}
				}
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData(stringBuilder.ToString());
			}
			else
			{
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData(global::System.Enum.GetName(storageType, instance));
			}
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			if (data.IsString)
			{
				string[] array = data.AsString.Split(new char[1] { ',' }, global::System.StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (!ArrayContains(global::System.Enum.GetNames(storageType), text))
					{
						if (!global::System.Linq.Enumerable.ToDictionary<(global::System.Enum, string), string, global::System.Enum>(global::System.Linq.Enumerable.SelectMany(global::System.Linq.Enumerable.Cast<global::System.Enum>(global::System.Enum.GetValues(storageType)), (global::System.Enum x) => global::System.Linq.Enumerable.Select(x.GetAttributeOfEnumMember<global::Unity.VisualScripting.RenamedFromAttribute>(), (global::Unity.VisualScripting.RenamedFromAttribute attr) => (x: x, previousName: attr.previousName))), ((global::System.Enum enumMember, string previousName) x) => x.previousName, ((global::System.Enum enumMember, string previousName) x) => x.enumMember).TryGetValue(text, out var value))
						{
							return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Cannot find enum name " + text + " on type " + storageType);
						}
						array[i] = value.ToString();
					}
				}
				if (global::System.Enum.GetUnderlyingType(storageType) == typeof(ulong))
				{
					ulong num = 0uL;
					foreach (string value2 in array)
					{
						ulong num3 = (ulong)global::System.Convert.ChangeType(global::System.Enum.Parse(storageType, value2), typeof(ulong));
						num |= num3;
					}
					instance = global::System.Enum.ToObject(storageType, (object)num);
				}
				else
				{
					long num4 = 0L;
					foreach (string value3 in array)
					{
						long num6 = (long)global::System.Convert.ChangeType(global::System.Enum.Parse(storageType, value3), typeof(long));
						num4 |= num6;
					}
					instance = global::System.Enum.ToObject(storageType, (object)num4);
				}
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			if (data.IsInt64)
			{
				int num7 = (int)data.AsInt64;
				instance = global::System.Enum.ToObject(storageType, (object)num7);
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			return global::Unity.VisualScripting.FullSerializer.fsResult.Fail($"EnumConverter encountered an unknown JSON data type for {storageType}: {data.Type}");
		}

		private static bool ArrayContains<T>(T[] values, T value)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (global::System.Collections.Generic.EqualityComparer<T>.Default.Equals(values[i], value))
				{
					return true;
				}
			}
			return false;
		}
	}
}
