namespace UnityEngine.Rendering.Universal
{
	[global::System.Obsolete("Deprecated in favor of RTHandle. #from(2022.1) #breakingFrom(2023.1)", true)]
	public struct RenderTargetHandle
	{
		public static readonly global::UnityEngine.Rendering.Universal.RenderTargetHandle CameraTarget = new global::UnityEngine.Rendering.Universal.RenderTargetHandle
		{
			id = -1
		};

		public int id { get; set; }

		private global::UnityEngine.Rendering.RenderTargetIdentifier rtid { get; set; }

		public RenderTargetHandle(global::UnityEngine.Rendering.RenderTargetIdentifier renderTargetIdentifier)
		{
			id = -2;
			rtid = renderTargetIdentifier;
		}

		public RenderTargetHandle(global::UnityEngine.Rendering.RTHandle rtHandle)
		{
			if (rtHandle.nameID == global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget)
			{
				id = -1;
			}
			else if (rtHandle.name.Length == 0)
			{
				id = -2;
			}
			else
			{
				id = global::UnityEngine.Shader.PropertyToID(rtHandle.name);
			}
			rtid = rtHandle.nameID;
			if (rtHandle.rt != null && id != rtid)
			{
				id = -2;
			}
		}

		internal static global::UnityEngine.Rendering.Universal.RenderTargetHandle GetCameraTarget(ref global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
			if (cameraData.xr.enabled)
			{
				return new global::UnityEngine.Rendering.Universal.RenderTargetHandle(cameraData.xr.renderTarget);
			}
			return CameraTarget;
		}

		public void Init(string shaderProperty)
		{
			id = global::UnityEngine.Shader.PropertyToID(shaderProperty);
		}

		public void Init(global::UnityEngine.Rendering.RenderTargetIdentifier renderTargetIdentifier)
		{
			id = -2;
			rtid = renderTargetIdentifier;
		}

		public global::UnityEngine.Rendering.RenderTargetIdentifier Identifier()
		{
			if (id == -1)
			{
				return global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget;
			}
			if (id == -2)
			{
				return rtid;
			}
			return new global::UnityEngine.Rendering.RenderTargetIdentifier(id, 0, global::UnityEngine.CubemapFace.Unknown, -1);
		}

		public bool HasInternalRenderTargetId()
		{
			return id == -2;
		}

		public bool Equals(global::UnityEngine.Rendering.Universal.RenderTargetHandle other)
		{
			if (id == -2 || other.id == -2)
			{
				return Identifier() == other.Identifier();
			}
			return id == other.id;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.Rendering.Universal.RenderTargetHandle)
			{
				return Equals((global::UnityEngine.Rendering.Universal.RenderTargetHandle)obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return id;
		}

		public static bool operator ==(global::UnityEngine.Rendering.Universal.RenderTargetHandle c1, global::UnityEngine.Rendering.Universal.RenderTargetHandle c2)
		{
			return c1.Equals(c2);
		}

		public static bool operator !=(global::UnityEngine.Rendering.Universal.RenderTargetHandle c1, global::UnityEngine.Rendering.Universal.RenderTargetHandle c2)
		{
			return !c1.Equals(c2);
		}
	}
}
