namespace DG.Tweening.Plugins.Options
{
	public struct PathOptions : global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public global::DG.Tweening.PathMode mode;

		public global::DG.Tweening.Plugins.Options.OrientType orientType;

		public global::DG.Tweening.AxisConstraint lockPositionAxis;

		public global::DG.Tweening.AxisConstraint lockRotationAxis;

		public bool isClosedPath;

		public global::UnityEngine.Vector3 lookAtPosition;

		public global::UnityEngine.Transform lookAtTransform;

		public float lookAhead;

		public bool hasCustomForwardDirection;

		public global::UnityEngine.Quaternion forward;

		public bool useLocalPosition;

		public global::UnityEngine.Transform parent;

		public bool isRigidbody;

		public bool isRigidbody2D;

		public bool stableZRotation;

		internal global::UnityEngine.Quaternion startupRot;

		internal float startupZRot;

		internal bool addedExtraStartWp;

		internal bool addedExtraEndWp;

		public void Reset()
		{
			mode = global::DG.Tweening.PathMode.Ignore;
			orientType = global::DG.Tweening.Plugins.Options.OrientType.None;
			lockPositionAxis = (lockRotationAxis = global::DG.Tweening.AxisConstraint.None);
			isClosedPath = false;
			lookAtPosition = global::UnityEngine.Vector3.zero;
			lookAtTransform = null;
			lookAhead = 0f;
			hasCustomForwardDirection = false;
			forward = global::UnityEngine.Quaternion.identity;
			useLocalPosition = false;
			parent = null;
			isRigidbody = (isRigidbody2D = false);
			stableZRotation = false;
			startupRot = global::UnityEngine.Quaternion.identity;
			startupZRot = 0f;
			addedExtraStartWp = (addedExtraEndWp = false);
		}
	}
}
