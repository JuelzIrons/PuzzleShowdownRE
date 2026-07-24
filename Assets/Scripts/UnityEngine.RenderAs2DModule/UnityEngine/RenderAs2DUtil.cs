namespace UnityEngine
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::UnityEngine.Bindings.NativeHeader("Modules/RenderAs2D/Public/RenderAs2DUtil.h")]
	internal struct RenderAs2DUtil
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		[global::UnityEngine.Bindings.FreeFunction("RenderAs2DUtil::InitializeCanRenderAs2D")]
		internal static extern void InitializeCanRenderAs2D();

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		[global::UnityEngine.Bindings.FreeFunction("RenderAs2DUtil::DisposeCanRenderAs2D")]
		internal static extern void DisposeCanRenderAs2D();
	}
}
