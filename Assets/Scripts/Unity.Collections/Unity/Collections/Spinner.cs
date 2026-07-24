namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct Spinner
	{
		private int m_Lock;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void Acquire()
		{
			while (global::System.Threading.Interlocked.CompareExchange(ref m_Lock, 1, 0) != 0)
			{
				while (global::System.Threading.Volatile.Read(ref m_Lock) == 1)
				{
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal bool TryAcquire()
		{
			if (global::System.Threading.Volatile.Read(ref m_Lock) == 0)
			{
				return global::System.Threading.Interlocked.CompareExchange(ref m_Lock, 1, 0) == 0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal bool TryAcquire(bool spin)
		{
			if (spin)
			{
				Acquire();
				return true;
			}
			return TryAcquire();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void Release()
		{
			global::System.Threading.Volatile.Write(ref m_Lock, 0);
		}
	}
}
