namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class FixedString
	{
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, int arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, int arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, int arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, int arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, int arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, int arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, int arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, int arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, int arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in fs2);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, float arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, float arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, float arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, float arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, float arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, float arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, float arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, float arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, float arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, string arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, string arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, string arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, string arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, string arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, string arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, string arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, string arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, string arg2, int arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, int arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in arg2, in fs);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, int arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, int arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, int arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, int arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, int arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, int arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, int arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, int arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, int arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in fs2);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, float arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, float arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, float arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, float arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, float arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, float arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, float arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, float arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, float arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, string arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, string arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, string arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, string arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, string arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, string arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, string arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, string arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, string arg2, float arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, float arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in arg2, in fs);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, int arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, int arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, int arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, int arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, int arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, int arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, int arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, int arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, int arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, float arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, float arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, float arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, float arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, float arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, float arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, float arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, float arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, float arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, string arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, string arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, string arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, string arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, string arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, string arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, string arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, string arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, string arg2, string arg3)
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedString32Bytes fs4 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs4, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in fs4);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, string arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg3);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in arg2, in fs);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, int arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, int arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, int arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, int arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, int arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, int arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, int arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, int arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, int arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, int arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, int arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, int arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, int arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, int arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, int arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, int arg2, T3 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, float arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, float arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, float arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, float arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, float arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, float arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, float arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, float arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, float arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, float arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, float arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, float arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, float arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, float arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, float arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, float arg2, T3 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, string arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, string arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, string arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, string arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, string arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, string arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, string arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, string arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, string arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, string arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, string arg2, T1 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, string arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, string arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, string arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, string arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, string arg2, T3 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, int arg1, T1 arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, int arg1, T1 arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, int arg1, T1 arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, int arg1, T2 arg2, T3 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, float arg1, T1 arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, float arg1, T1 arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, float arg1, T1 arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, float arg1, T2 arg2, T3 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, string arg1, T1 arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, string arg1, T1 arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, string arg1, T1 arg2, T2 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, string arg1, T2 arg2, T3 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, int arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, float arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString512Bytes formatString, string arg0, T1 arg1, T2 arg2, T3 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2, in arg3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString512Bytes Format<T1, T2, T3, T4>(global::Unity.Collections.FixedString512Bytes formatString, T1 arg0, T2 arg1, T3 arg2, T4 arg3) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T4 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString512Bytes dest = default(global::Unity.Collections.FixedString512Bytes);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in arg2, in arg3);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, int arg1, int arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, int arg1, int arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, int arg1, int arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, int arg1, int arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, float arg1, int arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, float arg1, int arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, float arg1, int arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, float arg1, int arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, string arg1, int arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, string arg1, int arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, string arg1, int arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, string arg1, int arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, int arg0, T1 arg1, int arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, float arg0, T1 arg1, int arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, string arg0, T1 arg1, int arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, T2 arg1, int arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, int arg1, float arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, int arg1, float arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, int arg1, float arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, int arg1, float arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, float arg1, float arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, float arg1, float arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, float arg1, float arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, float arg1, float arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, string arg1, float arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, string arg1, float arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, string arg1, float arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, string arg1, float arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, int arg0, T1 arg1, float arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, float arg0, T1 arg1, float arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, string arg0, T1 arg1, float arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, T2 arg1, float arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, int arg1, string arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, int arg1, string arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, int arg1, string arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, int arg1, string arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, float arg1, string arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, float arg1, string arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, float arg1, string arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, float arg1, string arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, string arg1, string arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, string arg1, string arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, string arg1, string arg2)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedString32Bytes fs3 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs3, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in fs3);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, string arg1, string arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, int arg0, T1 arg1, string arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, float arg0, T1 arg1, string arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, string arg0, T1 arg1, string arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, T2 arg1, string arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg2);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in fs);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, int arg0, int arg1, T1 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, float arg0, int arg1, T1 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, string arg0, int arg1, T1 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, int arg1, T2 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, int arg0, float arg1, T1 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, float arg0, float arg1, T1 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, string arg0, float arg1, T1 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, float arg1, T2 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, int arg0, string arg1, T1 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, float arg0, string arg1, T1 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, string arg0, string arg1, T1 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2, in arg2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, string arg1, T2 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs, in arg2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2>(global::Unity.Collections.FixedString128Bytes formatString, int arg0, T1 arg1, T2 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2>(global::Unity.Collections.FixedString128Bytes formatString, float arg0, T1 arg1, T2 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2>(global::Unity.Collections.FixedString128Bytes formatString, string arg0, T1 arg1, T2 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1, in arg2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2, T3>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, T2 arg1, T3 arg2) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T3 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1, in arg2);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, int arg1)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, int arg1)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, int arg1)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, int arg1) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, float arg1)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, float arg1)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, float arg1)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, float arg1) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0, string arg1)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0, string arg1)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0, string arg1)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedString32Bytes fs2 = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs2, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in fs2);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, string arg1) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg1);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in fs);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, int arg0, T1 arg1) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, float arg0, T1 arg1) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, string arg0, T1 arg1) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs, in arg1);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(global::Unity.Collections.FixedString32Bytes),
			typeof(global::Unity.Collections.FixedString32Bytes)
		})]
		public static global::Unity.Collections.FixedString128Bytes Format<T1, T2>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0, T2 arg1) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes where T2 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0, in arg1);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, int arg0)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs);
			return dest;
		}

		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, float arg0)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs);
			return dest;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed string")]
		public static global::Unity.Collections.FixedString128Bytes Format(global::Unity.Collections.FixedString128Bytes formatString, string arg0)
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fs = default(global::Unity.Collections.FixedString32Bytes);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, arg0);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in fs);
			return dest;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.FixedString32Bytes) })]
		public static global::Unity.Collections.FixedString128Bytes Format<T1>(global::Unity.Collections.FixedString128Bytes formatString, T1 arg0) where T1 : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			global::Unity.Collections.FixedString128Bytes dest = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedStringMethods.AppendFormat(ref dest, in formatString, in arg0);
			return dest;
		}
	}
}
