namespace UnityEngine.Shaders
{
	[global::UnityEngine.Bindings.NativeHeader("Modules/ShaderRuntime/Public/ShaderTypes.h")]
	public sealed class Utility
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		public static extern bool IsShaderStageEnabled(global::UnityEngine.Shaders.ShaderStageFlags flags, global::UnityEngine.Shaders.ShaderStage stage);

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		public static extern bool IsShaderTypeEnabled(global::UnityEngine.Shaders.ShaderTypeFlags flags, global::UnityEngine.Shaders.ShaderType type);

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		public static extern global::UnityEngine.Shaders.ShaderStageFlags ShaderStageToFlags(global::UnityEngine.Shaders.ShaderStage stage);

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		public static extern global::UnityEngine.Shaders.ShaderTypeFlags ShaderTypeToFlags(global::UnityEngine.Shaders.ShaderType type);
	}
}
