namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("Resource ({GetType().Name}:{GetName()})")]
	internal abstract class RenderGraphResource<DescType, ResType> : global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource where DescType : struct where ResType : class
	{
		public DescType desc;

		public bool validDesc;

		public ResType graphicsResource;

		protected global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<ResType> m_Pool;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override void Reset(global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResourcePool pool = null)
		{
			base.Reset();
			m_Pool = pool as global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourcePool<ResType>;
			graphicsResource = null;
			validDesc = false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override bool IsCreated()
		{
			return graphicsResource != null;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override void ReleaseGraphicsResource()
		{
			graphicsResource = null;
		}

		public override void CreatePooledGraphicsResource(bool forceResourceCreation)
		{
			int descHashCode = GetDescHashCode();
			if (graphicsResource != null)
			{
				throw new global::System.InvalidOperationException("RenderGraphResource: Trying to create an already created resource (" + GetName() + "). Resource was probably declared for writing more than once in the same pass.");
			}
			if (forceResourceCreation || !m_Pool.TryGetResource(descHashCode, out graphicsResource))
			{
				CreateGraphicsResource();
			}
			else
			{
				UpdateGraphicsResource();
			}
			cachedHash = descHashCode;
		}

		public override void ReleasePooledGraphicsResource(int frameIndex)
		{
			if (graphicsResource == null)
			{
				throw new global::System.InvalidOperationException("RenderGraphResource: Tried to release a resource (" + GetName() + ") that was never created. Check that there is at least one pass writing to it first.");
			}
			if (m_Pool != null)
			{
				m_Pool.ReleaseResource(cachedHash, graphicsResource, frameIndex);
			}
			Reset();
		}
	}
}
