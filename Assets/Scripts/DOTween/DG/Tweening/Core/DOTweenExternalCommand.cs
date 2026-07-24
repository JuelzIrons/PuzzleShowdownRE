namespace DG.Tweening.Core
{
	public static class DOTweenExternalCommand
	{
		public static event global::System.Action<global::DG.Tweening.Plugins.Options.PathOptions, global::DG.Tweening.Tween, global::UnityEngine.Quaternion, global::UnityEngine.Transform> SetOrientationOnPath;

		internal static void Dispatch_SetOrientationOnPath(global::DG.Tweening.Plugins.Options.PathOptions options, global::DG.Tweening.Tween t, global::UnityEngine.Quaternion newRot, global::UnityEngine.Transform trans)
		{
			if (global::DG.Tweening.Core.DOTweenExternalCommand.SetOrientationOnPath != null)
			{
				global::DG.Tweening.Core.DOTweenExternalCommand.SetOrientationOnPath(options, t, newRot, trans);
			}
		}
	}
}
