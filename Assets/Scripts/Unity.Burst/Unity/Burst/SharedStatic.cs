namespace Unity.Burst
{
	public readonly struct SharedStatic<T> where T : struct
	{
		private unsafe readonly void* _buffer;

		private const uint DefaultAlignment = 16u;

		public unsafe ref T Data => ref global::Unity.Burst.Unsafe.AsRef<T>(_buffer);

		public unsafe void* UnsafeDataPointer => _buffer;

		private unsafe SharedStatic(void* buffer)
		{
			_buffer = buffer;
		}

		public static global::Unity.Burst.SharedStatic<T> GetOrCreate<TContext>(uint alignment = 0u)
		{
			return GetOrCreateUnsafe(alignment, global::Unity.Burst.BurstRuntime.GetHashCode64<TContext>(), 0L);
		}

		public static global::Unity.Burst.SharedStatic<T> GetOrCreate<TContext, TSubContext>(uint alignment = 0u)
		{
			return GetOrCreateUnsafe(alignment, global::Unity.Burst.BurstRuntime.GetHashCode64<TContext>(), global::Unity.Burst.BurstRuntime.GetHashCode64<TSubContext>());
		}

		public unsafe static global::Unity.Burst.SharedStatic<T> GetOrCreateUnsafe(uint alignment, long hashCode, long subHashCode)
		{
			return new global::Unity.Burst.SharedStatic<T>(global::Unity.Burst.SharedStatic.GetOrCreateSharedStaticInternal(hashCode, subHashCode, (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), (alignment == 0) ? 16u : alignment));
		}

		public unsafe static global::Unity.Burst.SharedStatic<T> GetOrCreatePartiallyUnsafeWithHashCode<TSubContext>(uint alignment, long hashCode)
		{
			return new global::Unity.Burst.SharedStatic<T>(global::Unity.Burst.SharedStatic.GetOrCreateSharedStaticInternal(hashCode, global::Unity.Burst.BurstRuntime.GetHashCode64<TSubContext>(), (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), (alignment == 0) ? 16u : alignment));
		}

		public unsafe static global::Unity.Burst.SharedStatic<T> GetOrCreatePartiallyUnsafeWithSubHashCode<TContext>(uint alignment, long subHashCode)
		{
			return new global::Unity.Burst.SharedStatic<T>(global::Unity.Burst.SharedStatic.GetOrCreateSharedStaticInternal(global::Unity.Burst.BurstRuntime.GetHashCode64<TContext>(), subHashCode, (uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), (alignment == 0) ? 16u : alignment));
		}

		public static global::Unity.Burst.SharedStatic<T> GetOrCreate(global::System.Type contextType, uint alignment = 0u)
		{
			return GetOrCreateUnsafe(alignment, global::Unity.Burst.BurstRuntime.GetHashCode64(contextType), 0L);
		}

		public static global::Unity.Burst.SharedStatic<T> GetOrCreate(global::System.Type contextType, global::System.Type subContextType, uint alignment = 0u)
		{
			return GetOrCreateUnsafe(alignment, global::Unity.Burst.BurstRuntime.GetHashCode64(contextType), global::Unity.Burst.BurstRuntime.GetHashCode64(subContextType));
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckIf_T_IsUnmanagedOrThrow()
		{
			if (!global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.IsUnmanaged<T>())
			{
				throw new global::System.InvalidOperationException($"The type {typeof(T)} used in SharedStatic<{typeof(T)}> must be unmanaged (contain no managed types).");
			}
		}
	}
	internal static class SharedStatic
	{
		internal class PreserveAttribute : global::System.Attribute
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckSizeOf(uint sizeOf)
		{
			if (sizeOf == 0)
			{
				throw new global::System.ArgumentException("sizeOf must be > 0", "sizeOf");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe static void CheckResult(void* result)
		{
			if (result == null)
			{
				throw new global::System.InvalidOperationException("Unable to create a SharedStatic for this key. This is most likely due to the size of the struct inside of the SharedStatic having changed or the same key being reused for differently sized values. To fix this the editor needs to be restarted.");
			}
		}

		[global::Unity.Burst.SharedStatic.Preserve]
		public unsafe static void* GetOrCreateSharedStaticInternal(long getHashCode64, long getSubHashCode64, uint sizeOf, uint alignment)
		{
			global::UnityEngine.Hash128 key = new global::UnityEngine.Hash128((ulong)getHashCode64, (ulong)getSubHashCode64);
			return global::Unity.Burst.LowLevel.BurstCompilerService.GetOrCreateSharedMemory(ref key, sizeOf, alignment);
		}
	}
}
