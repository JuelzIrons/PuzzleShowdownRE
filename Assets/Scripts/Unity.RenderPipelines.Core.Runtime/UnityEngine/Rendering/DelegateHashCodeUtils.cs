namespace UnityEngine.Rendering
{
	internal static class DelegateHashCodeUtils
	{
		private static readonly global::System.Lazy<global::System.Collections.Generic.Dictionary<int, bool>> s_MethodHashCodeToSkipTargetHashMap = new global::System.Lazy<global::System.Collections.Generic.Dictionary<int, bool>>(() => new global::System.Collections.Generic.Dictionary<int, bool>(64));

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int GetFuncHashCode(global::System.Delegate del)
		{
			int hashCode = global::System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(del.Method);
			if (!s_MethodHashCodeToSkipTargetHashMap.Value.TryGetValue(hashCode, out var value))
			{
				int num;
				if (del.Target != null)
				{
					global::System.Type declaringType = del.Method.DeclaringType;
					num = (((object)declaringType != null && declaringType.IsNestedPrivate && global::System.Attribute.IsDefined(del.Method.DeclaringType, typeof(global::System.Runtime.CompilerServices.CompilerGeneratedAttribute), inherit: false)) ? 1 : 0);
				}
				else
				{
					num = 1;
				}
				value = (byte)num != 0;
				s_MethodHashCodeToSkipTargetHashMap.Value[hashCode] = value;
			}
			if (!value)
			{
				return hashCode ^ global::System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(del.Target);
			}
			return hashCode;
		}

		internal static int GetTotalCacheCount()
		{
			return s_MethodHashCodeToSkipTargetHashMap.Value.Count;
		}

		internal static void ClearCache()
		{
			s_MethodHashCodeToSkipTargetHashMap.Value.Clear();
		}
	}
}
