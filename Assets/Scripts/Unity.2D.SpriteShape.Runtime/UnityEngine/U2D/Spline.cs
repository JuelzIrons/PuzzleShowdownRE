namespace UnityEngine.U2D
{
	[global::System.Serializable]
	public class Spline
	{
		private static readonly string KErrorMessage = "Internal error: Point too close to neighbor";

		private static readonly float KEpsilon = 0.01f;

		[global::UnityEngine.SerializeField]
		private bool m_IsOpenEnded;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.U2D.SplineControlPoint> m_ControlPoints = new global::System.Collections.Generic.List<global::UnityEngine.U2D.SplineControlPoint>();

		private int m_DirtyIndex;

		public bool isOpenEnded
		{
			get
			{
				if (GetPointCount() < 3)
				{
					return true;
				}
				return m_IsOpenEnded;
			}
			set
			{
				m_IsOpenEnded = value;
				m_DirtyIndex++;
			}
		}

		private bool IsPositionValid(int index, int next, global::UnityEngine.Vector3 point)
		{
			int pointCount = GetPointCount();
			if (isOpenEnded && (index == 0 || index == pointCount))
			{
				return true;
			}
			int num = ((index == 0) ? (pointCount - 1) : (index - 1));
			if (num >= 0 && (m_ControlPoints[num].position - point).magnitude < KEpsilon)
			{
				return false;
			}
			next = ((next < pointCount) ? next : 0);
			if (next < pointCount && (m_ControlPoints[next].position - point).magnitude < KEpsilon)
			{
				return false;
			}
			return true;
		}

		public void Clear()
		{
			m_ControlPoints.Clear();
			m_DirtyIndex++;
		}

		public int GetPointCount()
		{
			return m_ControlPoints.Count;
		}

		public void InsertPointAt(int index, global::UnityEngine.Vector3 point)
		{
			if (!IsPositionValid(index, index, point))
			{
				throw new global::System.ArgumentException(KErrorMessage);
			}
			m_ControlPoints.Insert(index, new global::UnityEngine.U2D.SplineControlPoint
			{
				position = point,
				height = 1f,
				cornerMode = global::UnityEngine.U2D.Corner.Automatic
			});
			m_DirtyIndex++;
		}

		public void RemovePointAt(int index)
		{
			if (m_ControlPoints.Count > 2)
			{
				m_ControlPoints.RemoveAt(index);
				m_DirtyIndex++;
			}
		}

		public global::UnityEngine.Vector3 GetPosition(int index)
		{
			return m_ControlPoints[index].position;
		}

		public void SetPosition(int index, global::UnityEngine.Vector3 point)
		{
			if (!IsPositionValid(index, index + 1, point))
			{
				throw new global::System.ArgumentException(KErrorMessage);
			}
			global::UnityEngine.U2D.SplineControlPoint splineControlPoint = m_ControlPoints[index];
			splineControlPoint.position = point;
			m_ControlPoints[index] = splineControlPoint;
			m_DirtyIndex++;
		}

		public global::UnityEngine.Vector3 GetLeftTangent(int index)
		{
			if (GetTangentMode(index) == global::UnityEngine.U2D.ShapeTangentMode.Linear)
			{
				return global::UnityEngine.Vector3.zero;
			}
			return m_ControlPoints[index].leftTangent;
		}

		public void SetLeftTangent(int index, global::UnityEngine.Vector3 tangent)
		{
			if (GetTangentMode(index) != global::UnityEngine.U2D.ShapeTangentMode.Linear)
			{
				global::UnityEngine.U2D.SplineControlPoint splineControlPoint = m_ControlPoints[index];
				splineControlPoint.leftTangent = tangent;
				m_ControlPoints[index] = splineControlPoint;
				m_DirtyIndex++;
			}
		}

		public global::UnityEngine.Vector3 GetRightTangent(int index)
		{
			if (GetTangentMode(index) == global::UnityEngine.U2D.ShapeTangentMode.Linear)
			{
				return global::UnityEngine.Vector3.zero;
			}
			return m_ControlPoints[index].rightTangent;
		}

		public void SetRightTangent(int index, global::UnityEngine.Vector3 tangent)
		{
			if (GetTangentMode(index) != global::UnityEngine.U2D.ShapeTangentMode.Linear)
			{
				global::UnityEngine.U2D.SplineControlPoint splineControlPoint = m_ControlPoints[index];
				splineControlPoint.rightTangent = tangent;
				m_ControlPoints[index] = splineControlPoint;
				m_DirtyIndex++;
			}
		}

		public global::UnityEngine.U2D.ShapeTangentMode GetTangentMode(int index)
		{
			return m_ControlPoints[index].mode;
		}

		public void SetTangentMode(int index, global::UnityEngine.U2D.ShapeTangentMode mode)
		{
			global::UnityEngine.U2D.SplineControlPoint splineControlPoint = m_ControlPoints[index];
			splineControlPoint.mode = mode;
			m_ControlPoints[index] = splineControlPoint;
			m_DirtyIndex++;
		}

		public float GetHeight(int index)
		{
			return m_ControlPoints[index].height;
		}

		public void SetHeight(int index, float value)
		{
			m_ControlPoints[index].height = value;
			m_DirtyIndex++;
		}

		public int GetSpriteIndex(int index)
		{
			return m_ControlPoints[index].spriteIndex;
		}

		public void SetSpriteIndex(int index, int value)
		{
			m_ControlPoints[index].spriteIndex = value;
			m_DirtyIndex++;
		}

		public bool GetCorner(int index)
		{
			return GetCornerMode(index) != global::UnityEngine.U2D.Corner.Disable;
		}

		public void SetCorner(int index, bool value)
		{
			m_ControlPoints[index].corner = value;
			m_ControlPoints[index].cornerMode = (value ? global::UnityEngine.U2D.Corner.Automatic : global::UnityEngine.U2D.Corner.Disable);
			m_DirtyIndex++;
		}

		internal void SetCornerMode(int index, global::UnityEngine.U2D.Corner value)
		{
			m_ControlPoints[index].corner = value != global::UnityEngine.U2D.Corner.Disable;
			m_ControlPoints[index].cornerMode = value;
			m_DirtyIndex++;
		}

		internal global::UnityEngine.U2D.Corner GetCornerMode(int index)
		{
			if (m_ControlPoints[index].cornerMode == global::UnityEngine.U2D.Corner.Disable && m_ControlPoints[index].corner)
			{
				m_ControlPoints[index].cornerMode = global::UnityEngine.U2D.Corner.Automatic;
				return global::UnityEngine.U2D.Corner.Automatic;
			}
			return m_ControlPoints[index].cornerMode;
		}

		internal int GetChangeIndex()
		{
			return m_DirtyIndex;
		}

		public override int GetHashCode()
		{
			int num = -2128831035;
			for (int i = 0; i < GetPointCount(); i++)
			{
				num = (num * 16777619) ^ m_ControlPoints[i].GetHashCode();
			}
			return (num * 16777619) ^ m_IsOpenEnded.GetHashCode();
		}
	}
}
