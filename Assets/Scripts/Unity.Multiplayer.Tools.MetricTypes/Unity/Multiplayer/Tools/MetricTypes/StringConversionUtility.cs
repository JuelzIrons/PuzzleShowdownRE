namespace Unity.Multiplayer.Tools.MetricTypes
{
	internal static class StringConversionUtility
	{
		public unsafe static global::Unity.Collections.FixedString64Bytes ConvertToFixedString(string value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (global::Unity.Collections.FixedString64Bytes.UTF8MaxLengthInBytes < value.Length)
			{
				global::Unity.Collections.FixedString64Bytes result = default(global::Unity.Collections.FixedString64Bytes);
				fixed (char* src = value)
				{
					global::Unity.Collections.UTF8ArrayUnsafeUtility.Copy(result.GetUnsafePtr(), out var destLength, global::Unity.Collections.FixedString64Bytes.UTF8MaxLengthInBytes, src, value.Length);
					result.Length = destLength;
				}
				return result;
			}
			return value;
		}
	}
}
