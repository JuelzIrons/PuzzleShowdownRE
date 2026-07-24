namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct NativeReference<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.IEquatable<global::Unity.Collections.NativeReference<T>> where T : unmanaged
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ReadOnly
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			private unsafe readonly void* m_Data;

			public unsafe T Value => *(T*)m_Data;

			internal unsafe ReadOnly(void* data)
			{
				m_Data = data;
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Data;

		internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;

		public unsafe T Value
		{
			get
			{
				return *(T*)m_Data;
			}
			set
			{
				*(T*)m_Data = value;
			}
		}

		public unsafe readonly bool IsCreated => m_Data != null;

		public unsafe NativeReference(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory)
		{
			Allocate(allocator, out this);
			if (options == global::Unity.Collections.NativeArrayOptions.ClearMemory)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(m_Data, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>());
			}
		}

		public unsafe NativeReference(T value, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			Allocate(allocator, out this);
			*(T*)m_Data = value;
		}

		private unsafe static void Allocate(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, out global::Unity.Collections.NativeReference<T> reference)
		{
			reference = default(global::Unity.Collections.NativeReference<T>);
			reference.m_Data = global::Unity.Collections.Memory.Unmanaged.Allocate(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<T>(), allocator);
			reference.m_AllocatorLabel = allocator;
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				if (global::Unity.Collections.CollectionHelper.ShouldDeallocate(m_AllocatorLabel))
				{
					global::Unity.Collections.Memory.Unmanaged.Free(m_Data, m_AllocatorLabel);
					m_AllocatorLabel = global::Unity.Collections.Allocator.Invalid;
				}
				m_Data = null;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			if (global::Unity.Collections.CollectionHelper.ShouldDeallocate(m_AllocatorLabel))
			{
				global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.NativeReferenceDisposeJob
				{
					Data = new global::Unity.Collections.NativeReferenceDispose
					{
						m_Data = m_Data,
						m_AllocatorLabel = m_AllocatorLabel
					}
				}, inputDeps);
				m_Data = null;
				m_AllocatorLabel = global::Unity.Collections.Allocator.Invalid;
				return result;
			}
			m_Data = null;
			return inputDeps;
		}

		public void CopyFrom(global::Unity.Collections.NativeReference<T> reference)
		{
			Copy(this, reference);
		}

		public void CopyTo(global::Unity.Collections.NativeReference<T> reference)
		{
			Copy(reference, this);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Equals boxes because Value does not implement IEquatable<T>")]
		public bool Equals(global::Unity.Collections.NativeReference<T> other)
		{
			return Value.Equals(other.Value);
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed object")]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::Unity.Collections.NativeReference<T>)
			{
				return Equals((global::Unity.Collections.NativeReference<T>)obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public static bool operator ==(global::Unity.Collections.NativeReference<T> left, global::Unity.Collections.NativeReference<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::Unity.Collections.NativeReference<T> left, global::Unity.Collections.NativeReference<T> right)
		{
			return !left.Equals(right);
		}

		public unsafe static void Copy(global::Unity.Collections.NativeReference<T> dst, global::Unity.Collections.NativeReference<T> src)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(dst.m_Data, src.m_Data, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>());
		}

		public unsafe global::Unity.Collections.NativeReference<T>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.NativeReference<T>.ReadOnly(m_Data);
		}

		public static implicit operator global::Unity.Collections.NativeReference<T>.ReadOnly(global::Unity.Collections.NativeReference<T> nativeReference)
		{
			return nativeReference.AsReadOnly();
		}
	}
}
