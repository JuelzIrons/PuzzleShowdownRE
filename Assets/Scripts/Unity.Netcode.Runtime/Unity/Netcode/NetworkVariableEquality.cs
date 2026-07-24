namespace Unity.Netcode
{
	internal static class NetworkVariableEquality<T>
	{
		internal unsafe static bool ValueEquals<TValueType>(ref TValueType a, ref TValueType b) where TValueType : unmanaged
		{
			void* ptr = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref a);
			void* ptr2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref b);
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr, ptr2, sizeof(TValueType)) == 0;
		}

		internal unsafe static bool ValueEqualsArray<TValueType>(ref global::Unity.Collections.NativeArray<TValueType> a, ref global::Unity.Collections.NativeArray<TValueType> b) where TValueType : unmanaged
		{
			if (a.IsCreated != b.IsCreated)
			{
				return false;
			}
			if (!a.IsCreated)
			{
				return true;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			TValueType* unsafePtr = (TValueType*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(a);
			TValueType* unsafePtr2 = (TValueType*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(b);
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(unsafePtr, unsafePtr2, sizeof(TValueType) * a.Length) == 0;
		}

		internal static bool EqualityEqualsObject<TValueType>(ref TValueType a, ref TValueType b) where TValueType : class, global::System.IEquatable<TValueType>
		{
			if (a == null)
			{
				return b == null;
			}
			if (b == null)
			{
				return false;
			}
			TValueType other = b;
			return a.Equals(other);
		}

		internal static bool EqualityEquals<TValueType>(ref TValueType a, ref TValueType b) where TValueType : unmanaged, global::System.IEquatable<TValueType>
		{
			return a.Equals(b);
		}

		internal static bool EqualityEqualsList<TValueType>(ref global::System.Collections.Generic.List<TValueType> a, ref global::System.Collections.Generic.List<TValueType> b)
		{
			if (a == null != (b == null))
			{
				return false;
			}
			if (a == null)
			{
				return true;
			}
			if (a.Count != b.Count)
			{
				return false;
			}
			for (int i = 0; i < a.Count; i++)
			{
				TValueType a2 = a[i];
				TValueType b2 = b[i];
				if (!global::Unity.Netcode.NetworkVariableSerialization<TValueType>.AreEqual(ref a2, ref b2))
				{
					return false;
				}
			}
			return true;
		}

		internal static bool EqualityEqualsHashSet<TValueType>(ref global::System.Collections.Generic.HashSet<TValueType> a, ref global::System.Collections.Generic.HashSet<TValueType> b) where TValueType : global::System.IEquatable<TValueType>
		{
			if (a == null != (b == null))
			{
				return false;
			}
			if (a == null)
			{
				return true;
			}
			if (a.Count != b.Count)
			{
				return false;
			}
			foreach (TValueType item in a)
			{
				if (!b.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		internal unsafe static bool EqualityEqualsArray<TValueType>(ref global::Unity.Collections.NativeArray<TValueType> a, ref global::Unity.Collections.NativeArray<TValueType> b) where TValueType : unmanaged, global::System.IEquatable<TValueType>
		{
			if (a.IsCreated != b.IsCreated)
			{
				return false;
			}
			if (!a.IsCreated)
			{
				return true;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			TValueType* unsafePtr = (TValueType*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(a);
			TValueType* unsafePtr2 = (TValueType*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(b);
			for (int i = 0; i < a.Length; i++)
			{
				if (!EqualityEquals(ref unsafePtr[i], ref unsafePtr2[i]))
				{
					return false;
				}
			}
			return true;
		}

		internal static bool ClassEquals<TValueType>(ref TValueType a, ref TValueType b) where TValueType : class
		{
			return a == b;
		}
	}
}
