namespace UnityEngine.Timeline
{
	[global::UnityEngine.AssetFileNameExtension("signal", new string[] { })]
	public class SignalAsset : global::UnityEngine.ScriptableObject
	{
		internal static event global::System.Action<global::UnityEngine.Timeline.SignalAsset> OnEnableCallback;

		private void OnEnable()
		{
			if (global::UnityEngine.Timeline.SignalAsset.OnEnableCallback != null)
			{
				global::UnityEngine.Timeline.SignalAsset.OnEnableCallback(this);
			}
		}
	}
}
