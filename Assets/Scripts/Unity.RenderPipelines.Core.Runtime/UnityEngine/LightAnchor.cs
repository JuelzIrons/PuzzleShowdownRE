namespace UnityEngine
{
	[global::UnityEngine.AddComponentMenu("Rendering/Light Anchor")]
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.DisallowMultipleComponent]
	public class LightAnchor : global::UnityEngine.MonoBehaviour
	{
		public enum UpDirection
		{
			World = 0,
			Local = 1
		}

		private struct Axes
		{
			public global::UnityEngine.Vector3 up;

			public global::UnityEngine.Vector3 right;

			public global::UnityEngine.Vector3 forward;
		}

		private const float k_ArcRadius = 5f;

		private const float k_AxisLength = 10f;

		internal const float k_MaxDistance = 10000f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Min(0f)]
		private float m_Distance;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.LightAnchor.UpDirection m_FrameSpace;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Transform m_AnchorPositionOverride;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector3 m_AnchorPositionOffset;

		[global::UnityEngine.SerializeField]
		private float m_Yaw;

		[global::UnityEngine.SerializeField]
		private float m_Pitch;

		[global::UnityEngine.SerializeField]
		private float m_Roll;

		public float yaw
		{
			get
			{
				return m_Yaw;
			}
			set
			{
				m_Yaw = NormalizeAngleDegree(value);
			}
		}

		public float pitch
		{
			get
			{
				return m_Pitch;
			}
			set
			{
				m_Pitch = NormalizeAngleDegree(value);
			}
		}

		public float roll
		{
			get
			{
				return m_Roll;
			}
			set
			{
				m_Roll = NormalizeAngleDegree(value);
			}
		}

		public float distance
		{
			get
			{
				return m_Distance;
			}
			set
			{
				m_Distance = global::UnityEngine.Mathf.Clamp(value, 0f, 10000f);
			}
		}

		public global::UnityEngine.LightAnchor.UpDirection frameSpace
		{
			get
			{
				return m_FrameSpace;
			}
			set
			{
				m_FrameSpace = value;
			}
		}

		public global::UnityEngine.Vector3 anchorPosition
		{
			get
			{
				if (anchorPositionOverride != null)
				{
					return anchorPositionOverride.position + anchorPositionOverride.TransformDirection(anchorPositionOffset);
				}
				return base.transform.position + base.transform.forward * distance;
			}
		}

		public global::UnityEngine.Transform anchorPositionOverride
		{
			get
			{
				return m_AnchorPositionOverride;
			}
			set
			{
				m_AnchorPositionOverride = value;
			}
		}

		public global::UnityEngine.Vector3 anchorPositionOffset
		{
			get
			{
				return m_AnchorPositionOffset;
			}
			set
			{
				m_AnchorPositionOffset = value;
			}
		}

		public static float NormalizeAngleDegree(float angle)
		{
			float num = angle - -180f;
			return num - global::UnityEngine.Mathf.Floor(num / 360f) * 360f + -180f;
		}

		public void SynchronizeOnTransform(global::UnityEngine.Camera camera)
		{
			global::UnityEngine.LightAnchor.Axes worldSpaceAxes = GetWorldSpaceAxes(camera, anchorPosition);
			global::UnityEngine.Vector3 vector = base.transform.position - anchorPosition;
			if (vector.magnitude == 0f)
			{
				vector = -base.transform.forward;
			}
			global::UnityEngine.Vector3 vector2 = global::UnityEngine.Vector3.ProjectOnPlane(vector, worldSpaceAxes.up);
			if (vector2.magnitude < 0.0001f)
			{
				vector2 = global::UnityEngine.Vector3.ProjectOnPlane(vector, worldSpaceAxes.up + worldSpaceAxes.right * 0.0001f);
			}
			vector2.Normalize();
			float angle = global::UnityEngine.Vector3.SignedAngle(worldSpaceAxes.forward, vector2, worldSpaceAxes.up);
			global::UnityEngine.Vector3 axis = global::UnityEngine.Quaternion.AngleAxis(angle, worldSpaceAxes.up) * worldSpaceAxes.right;
			float num = global::UnityEngine.Vector3.SignedAngle(vector2, vector, axis);
			yaw = angle;
			pitch = num;
			roll = base.transform.rotation.eulerAngles.z;
		}

		public void UpdateTransform(global::UnityEngine.Camera camera, global::UnityEngine.Vector3 anchor)
		{
			global::UnityEngine.LightAnchor.Axes worldSpaceAxes = GetWorldSpaceAxes(camera, anchor);
			UpdateTransform(worldSpaceAxes.up, worldSpaceAxes.right, worldSpaceAxes.forward, anchor);
		}

		private global::UnityEngine.LightAnchor.Axes GetWorldSpaceAxes(global::UnityEngine.Camera camera, global::UnityEngine.Vector3 anchor)
		{
			if (base.transform.IsChildOf(camera.transform))
			{
				return new global::UnityEngine.LightAnchor.Axes
				{
					up = global::UnityEngine.Vector3.up,
					right = global::UnityEngine.Vector3.right,
					forward = global::UnityEngine.Vector3.forward
				};
			}
			global::UnityEngine.Matrix4x4 matrix4x = camera.cameraToWorldMatrix;
			if (m_FrameSpace == global::UnityEngine.LightAnchor.UpDirection.Local)
			{
				global::UnityEngine.Vector3 up = global::UnityEngine.Camera.main.transform.up;
				matrix4x = (global::UnityEngine.Matrix4x4.Scale(new global::UnityEngine.Vector3(1f, 1f, -1f)) * global::UnityEngine.Matrix4x4.LookAt(camera.transform.position, anchor, up).inverse).inverse;
			}
			else if (!camera.orthographic && camera.transform.position != anchor)
			{
				global::UnityEngine.Quaternion q = global::UnityEngine.Quaternion.LookRotation((anchor - camera.transform.position).normalized);
				matrix4x = (global::UnityEngine.Matrix4x4.Scale(new global::UnityEngine.Vector3(1f, 1f, -1f)) * global::UnityEngine.Matrix4x4.TRS(camera.transform.position, q, global::UnityEngine.Vector3.one).inverse).inverse;
			}
			global::UnityEngine.Vector3 up2 = (matrix4x * global::UnityEngine.Vector3.up).normalized;
			global::UnityEngine.Vector3 right = (matrix4x * global::UnityEngine.Vector3.right).normalized;
			global::UnityEngine.Vector3 forward = (matrix4x * global::UnityEngine.Vector3.forward).normalized;
			return new global::UnityEngine.LightAnchor.Axes
			{
				up = up2,
				right = right,
				forward = forward
			};
		}

		private void Update()
		{
			if (!(anchorPositionOverride == null) && !(global::UnityEngine.Camera.main == null) && (anchorPositionOverride.hasChanged || global::UnityEngine.Camera.main.transform.hasChanged))
			{
				UpdateTransform(global::UnityEngine.Camera.main, anchorPosition);
			}
		}

		private void OnDrawGizmosSelected()
		{
			global::UnityEngine.Camera main = global::UnityEngine.Camera.main;
			if (!(main == null))
			{
				global::UnityEngine.Vector3 vector = anchorPosition;
				global::UnityEngine.LightAnchor.Axes worldSpaceAxes = GetWorldSpaceAxes(main, vector);
				global::UnityEngine.Vector3.ProjectOnPlane(base.transform.position - vector, worldSpaceAxes.up);
				global::UnityEngine.Mathf.Min(distance * 0.25f, 5f);
				global::UnityEngine.Mathf.Min(distance * 0.5f, 10f);
			}
		}

		private void UpdateTransform(global::UnityEngine.Vector3 up, global::UnityEngine.Vector3 right, global::UnityEngine.Vector3 forward, global::UnityEngine.Vector3 anchor)
		{
			global::UnityEngine.Quaternion quaternion = global::UnityEngine.Quaternion.AngleAxis(m_Yaw, up);
			global::UnityEngine.Quaternion quaternion2 = global::UnityEngine.Quaternion.AngleAxis(m_Pitch, right);
			global::UnityEngine.Vector3 position = anchor + quaternion * quaternion2 * forward * distance;
			base.transform.position = position;
			global::UnityEngine.Vector3 eulerAngles = global::UnityEngine.Quaternion.LookRotation(-(quaternion * quaternion2 * forward).normalized, up).eulerAngles;
			eulerAngles.z = m_Roll;
			base.transform.eulerAngles = eulerAngles;
		}
	}
}
