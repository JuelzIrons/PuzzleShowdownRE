namespace Unity.VisualScripting.FullSerializer
{
	public class fsDateConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		private const string DefaultDateTimeFormatString = "o";

		private const string DateTimeOffsetFormatString = "o";

		private string DateTimeFormatString => Serializer.Config.CustomDateTimeFormatString ?? "o";

		public override bool CanProcess(global::System.Type type)
		{
			if (!(type == typeof(global::System.DateTime)) && !(type == typeof(global::System.DateTimeOffset)))
			{
				return type == typeof(global::System.TimeSpan);
			}
			return true;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			if (instance is global::System.DateTime dateTime)
			{
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData(dateTime.ToString(DateTimeFormatString));
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			if (instance is global::System.DateTimeOffset dateTimeOffset)
			{
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData(dateTimeOffset.ToString("o"));
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			if (instance is global::System.TimeSpan timeSpan)
			{
				serialized = new global::Unity.VisualScripting.FullSerializer.fsData(timeSpan.ToString());
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			throw new global::System.InvalidOperationException("FullSerializer Internal Error -- Unexpected serialization type");
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			if (!data.IsString)
			{
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Date deserialization requires a string, not " + data.Type);
			}
			if (storageType == typeof(global::System.DateTime))
			{
				if (global::System.DateTime.TryParse(data.AsString, null, global::System.Globalization.DateTimeStyles.RoundtripKind, out var result))
				{
					instance = result;
					return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
				}
				if (global::Unity.VisualScripting.FullSerializer.fsGlobalConfig.AllowInternalExceptions)
				{
					try
					{
						instance = global::System.Convert.ToDateTime(data.AsString);
						return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
					}
					catch (global::System.Exception ex)
					{
						return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Unable to parse " + data.AsString + " into a DateTime; got exception " + ex);
					}
				}
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Unable to parse " + data.AsString + " into a DateTime");
			}
			if (storageType == typeof(global::System.DateTimeOffset))
			{
				if (global::System.DateTimeOffset.TryParse(data.AsString, null, global::System.Globalization.DateTimeStyles.RoundtripKind, out var result2))
				{
					instance = result2;
					return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
				}
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Unable to parse " + data.AsString + " into a DateTimeOffset");
			}
			if (storageType == typeof(global::System.TimeSpan))
			{
				if (global::System.TimeSpan.TryParse(data.AsString, out var result3))
				{
					instance = result3;
					return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
				}
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Unable to parse " + data.AsString + " into a TimeSpan");
			}
			throw new global::System.InvalidOperationException("FullSerializer Internal Error -- Unexpected deserialization type");
		}
	}
}
