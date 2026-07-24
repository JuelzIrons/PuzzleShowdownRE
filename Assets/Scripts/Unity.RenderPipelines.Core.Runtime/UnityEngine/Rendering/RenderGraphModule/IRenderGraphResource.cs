namespace UnityEngine.Rendering.RenderGraphModule
{
	internal class IRenderGraphResource
	{
		public bool imported;

		public bool shared;

		public bool sharedExplicitRelease;

		public bool requestFallBack;

		public uint writeCount;

		public uint readCount;

		public int cachedHash;

		public int transientPassIndex;

		public int sharedResourceLastFrameUsed;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public virtual void Reset(global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResourcePool _ = null)
		{
			imported = false;
			shared = false;
			sharedExplicitRelease = false;
			cachedHash = -1;
			transientPassIndex = -1;
			sharedResourceLastFrameUsed = -1;
			requestFallBack = false;
			writeCount = 0u;
			readCount = 0u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public virtual string GetName()
		{
			return "";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public virtual bool IsCreated()
		{
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public virtual uint IncrementWriteCount()
		{
			writeCount++;
			return writeCount;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public virtual void IncrementReadCount()
		{
			readCount++;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public virtual bool NeedsFallBack()
		{
			if (requestFallBack)
			{
				return writeCount == 0;
			}
			return false;
		}

		public virtual void CreatePooledGraphicsResource(bool forceResourceCreation)
		{
		}

		public virtual void CreateGraphicsResource()
		{
		}

		public virtual void UpdateGraphicsResource()
		{
		}

		public virtual void ReleasePooledGraphicsResource(int frameIndex)
		{
		}

		public virtual void ReleaseGraphicsResource()
		{
		}

		public virtual void LogCreation(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger logger)
		{
		}

		public virtual void LogRelease(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger logger)
		{
		}

		public virtual int GetSortIndex()
		{
			return 0;
		}

		public virtual int GetDescHashCode()
		{
			return 0;
		}
	}
}
