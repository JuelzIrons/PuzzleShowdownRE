namespace UnityEngine.Rendering.Universal
{
	public abstract class UniversalResourceDataBase : global::UnityEngine.Rendering.ContextItem
	{
		internal enum ActiveID
		{
			Camera = 0,
			BackBuffer = 1
		}

		internal bool isAccessible { get; set; }

		internal void InitFrame()
		{
			isAccessible = true;
		}

		internal void EndFrame()
		{
			isAccessible = false;
		}

		protected void CheckAndSetTextureHandle(ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle handle, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle newHandle)
		{
			if (CheckAndWarnAboutAccessibility())
			{
				handle = newHandle;
			}
		}

		protected global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CheckAndGetTextureHandle(ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle handle)
		{
			if (!CheckAndWarnAboutAccessibility())
			{
				return global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle;
			}
			return handle;
		}

		protected void CheckAndSetTextureHandle(ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] handle, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] newHandle)
		{
			if (CheckAndWarnAboutAccessibility())
			{
				if (handle == null || handle.Length != newHandle.Length)
				{
					handle = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[newHandle.Length];
				}
				for (int i = 0; i < newHandle.Length; i++)
				{
					handle[i] = newHandle[i];
				}
			}
		}

		protected global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] CheckAndGetTextureHandle(ref global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[] handle)
		{
			if (!CheckAndWarnAboutAccessibility())
			{
				return new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle[1] { global::UnityEngine.Rendering.RenderGraphModule.TextureHandle.nullHandle };
			}
			return handle;
		}

		protected bool CheckAndWarnAboutAccessibility()
		{
			if (!isAccessible)
			{
				global::UnityEngine.Debug.LogError("Trying to access Universal Resources outside of the current frame setup.");
			}
			return isAccessible;
		}
	}
}
