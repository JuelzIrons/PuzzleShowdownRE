namespace UnityEngine.Rendering.UnifiedRayTracing
{
	public class UnifiedRayTracingException : global::System.Exception
	{
		public global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError errorCode { get; private set; }

		public UnifiedRayTracingException(string message, global::UnityEngine.Rendering.UnifiedRayTracing.UnifiedRayTracingError errorCode)
			: base(message)
		{
			this.errorCode = errorCode;
		}
	}
}
