namespace Unity.Netcode
{
	internal class RealTimeProvider : global::Unity.Netcode.IRealTimeProvider
	{
		public float RealTimeSinceStartup => global::UnityEngine.Time.realtimeSinceStartup;

		public float UnscaledTime => global::UnityEngine.Time.unscaledTime;

		public float UnscaledDeltaTime => global::UnityEngine.Time.unscaledDeltaTime;

		public float DeltaTime => global::UnityEngine.Time.deltaTime;

		public float FixedDeltaTime => global::UnityEngine.Time.fixedDeltaTime;
	}
}
