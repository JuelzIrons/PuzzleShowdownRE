namespace UnityEngine
{
	[global::UnityEngine.Bindings.NativeType(Header = "Modules/RenderAs2D/Public/RenderAs2D.h")]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Transform))]
	[global::UnityEngine.AddComponentMenu("")]
	internal sealed class RenderAs2D : global::UnityEngine.Renderer
	{
		internal void Init(global::UnityEngine.Component owner)
		{
			global::System.IntPtr intPtr = global::UnityEngine.Object.MarshalledUnityObject.MarshalNotNull(this);
			if (intPtr == (global::System.IntPtr)0)
			{
				global::UnityEngine.Bindings.ThrowHelper.ThrowNullReferenceException(this);
			}
			Init_Injected(intPtr, global::UnityEngine.Object.MarshalledUnityObject.Marshal(owner));
		}

		internal bool IsOwner(global::UnityEngine.Component owner)
		{
			global::System.IntPtr intPtr = global::UnityEngine.Object.MarshalledUnityObject.MarshalNotNull(this);
			if (intPtr == (global::System.IntPtr)0)
			{
				global::UnityEngine.Bindings.ThrowHelper.ThrowNullReferenceException(this);
			}
			return IsOwner_Injected(intPtr, global::UnityEngine.Object.MarshalledUnityObject.Marshal(owner));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		private static extern void Init_Injected(global::System.IntPtr _unity_self, global::System.IntPtr owner);

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		private static extern bool IsOwner_Injected(global::System.IntPtr _unity_self, global::System.IntPtr owner);
	}
}
