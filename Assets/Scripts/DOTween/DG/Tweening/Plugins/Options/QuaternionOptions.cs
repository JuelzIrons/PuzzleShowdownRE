namespace DG.Tweening.Plugins.Options
{
	public struct QuaternionOptions : global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public global::DG.Tweening.RotateMode rotateMode;

		public global::DG.Tweening.AxisConstraint axisConstraint;

		public global::UnityEngine.Vector3 up;

		public bool dynamicLookAt;

		public global::UnityEngine.Vector3 dynamicLookAtWorldPosition;

		public void Reset()
		{
			rotateMode = global::DG.Tweening.RotateMode.Fast;
			axisConstraint = global::DG.Tweening.AxisConstraint.None;
			up = global::UnityEngine.Vector3.zero;
			dynamicLookAt = false;
			dynamicLookAtWorldPosition = global::UnityEngine.Vector3.zero;
		}
	}
}
