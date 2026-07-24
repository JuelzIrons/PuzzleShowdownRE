namespace DG.Tweening.Plugins.Core.PathCore
{
	internal abstract class ABSPathDecoder
	{
		internal abstract int minInputWaypoints { get; }

		internal abstract void FinalizePath(global::DG.Tweening.Plugins.Core.PathCore.Path p, global::UnityEngine.Vector3[] wps, bool isClosedPath);

		internal abstract global::UnityEngine.Vector3 GetPoint(float perc, global::UnityEngine.Vector3[] wps, global::DG.Tweening.Plugins.Core.PathCore.Path p, global::DG.Tweening.Plugins.Core.PathCore.ControlPoint[] controlPoints);
	}
}
