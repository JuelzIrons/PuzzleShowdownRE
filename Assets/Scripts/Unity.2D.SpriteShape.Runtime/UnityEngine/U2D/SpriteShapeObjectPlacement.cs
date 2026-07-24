namespace UnityEngine.U2D
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.ExecuteAlways]
	public class SpriteShapeObjectPlacement : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.SpriteShapeController m_SpriteShapeController;

		[global::UnityEngine.SerializeField]
		private bool m_SetNormal = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.SpriteShapeObjectPlacementMode m_Mode;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Min(0f)]
		private int m_StartPoint;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Min(0f)]
		private int m_EndPoint = 1;

		[global::UnityEngine.SerializeField]
		private float m_Ratio = 0.5f;

		private int m_ActiveHashCode;

		private static readonly float kMaxDistance = 10000f;

		private static readonly int kMaxIteration = 128;

		public bool setNormal
		{
			get
			{
				return m_SetNormal;
			}
			set
			{
				m_SetNormal = value;
			}
		}

		public global::UnityEngine.U2D.SpriteShapeObjectPlacementMode mode
		{
			get
			{
				return m_Mode;
			}
			set
			{
				m_Mode = value;
			}
		}

		public float ratio
		{
			get
			{
				return m_Ratio;
			}
			set
			{
				m_Ratio = value;
			}
		}

		public global::UnityEngine.U2D.SpriteShapeController spriteShapeController
		{
			get
			{
				return m_SpriteShapeController;
			}
			set
			{
				m_SpriteShapeController = value;
			}
		}

		public int startPoint
		{
			get
			{
				return m_StartPoint;
			}
			set
			{
				m_StartPoint = value;
			}
		}

		public int endPoint
		{
			get
			{
				return m_EndPoint;
			}
			set
			{
				m_EndPoint = value;
			}
		}

		private bool PlaceObjectOnHashChange()
		{
			if (null == spriteShapeController)
			{
				return false;
			}
			int num = 0;
			int num2 = -2128831035 ^ spriteShapeController.splineHashCode;
			num2 = (num2 * 16777619) ^ spriteShapeController.spriteShapeHashCode;
			global::UnityEngine.Transform transform = spriteShapeController.gameObject.transform;
			global::UnityEngine.Vector3 position = base.gameObject.transform.position;
			global::UnityEngine.Quaternion rotation = base.gameObject.transform.rotation;
			num2 = (num2 * 16777619) ^ (setNormal ? 1 : 0);
			num2 = (num2 * 16777619) ^ startPoint;
			num2 = (num2 * 16777619) ^ endPoint;
			num2 = (num2 * 16777619) ^ transform.position.GetHashCode();
			num2 = (num2 * 16777619) ^ transform.rotation.GetHashCode();
			bool num4;
			do
			{
				int num3 = (num2 * 16777619) ^ global::System.Math.Round(position.x * 1000f).GetHashCode();
				num3 = (num3 * 16777619) ^ global::System.Math.Round(position.y * 1000f).GetHashCode();
				num3 = (num3 * 16777619) ^ global::System.Math.Round(position.z * 1000f).GetHashCode();
				num3 = (num3 * 16777619) ^ global::System.Math.Round(ratio * 1000f).GetHashCode();
				num3 = (num3 * 16777619) ^ global::System.Math.Round(rotation.x * 1000f).GetHashCode();
				num3 = (num3 * 16777619) ^ global::System.Math.Round(rotation.y * 1000f).GetHashCode();
				num3 = (num3 * 16777619) ^ global::System.Math.Round(rotation.z * 1000f).GetHashCode();
				num3 = (num3 * 16777619) ^ global::System.Math.Round(rotation.w * 1000f).GetHashCode();
				if (m_ActiveHashCode == num3)
				{
					break;
				}
				num4 = Place();
				m_ActiveHashCode = num3;
			}
			while (num4 && num++ < kMaxIteration);
			return false;
		}

		private static float Angle(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			float x = global::UnityEngine.Vector3.Dot(a, b);
			return global::UnityEngine.Mathf.Atan2(a.x * b.y - b.x * a.y, x) * 57.29578f;
		}

		private float GetDistance(float dist, int spoint, int epoint, ref int start, ref int end, ref float r, global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ShapeControlPoint> shapePoints)
		{
			start = -1;
			int splineDetail = spriteShapeController.splineDetail;
			float num = 0f;
			float num2 = splineDetail - 1;
			int length = shapePoints.Length;
			for (int i = spoint; i < epoint; i++)
			{
				int num3 = i + 1;
				if (num3 == length)
				{
					num3 = 0;
				}
				global::UnityEngine.U2D.ShapeControlPoint shapeControlPoint = shapePoints[i];
				global::UnityEngine.U2D.ShapeControlPoint shapeControlPoint2 = shapePoints[num3];
				global::UnityEngine.Vector3 position = shapeControlPoint.position;
				global::UnityEngine.Vector3 position2 = shapeControlPoint2.position;
				global::UnityEngine.Vector3 vector = position;
				global::UnityEngine.Vector3 startRightTangent = position + shapeControlPoint.rightTangent;
				global::UnityEngine.Vector3 endLeftTangent = position2 + shapeControlPoint2.leftTangent;
				float num4 = 0f;
				float num5 = 0f;
				bool flag = false;
				if (dist != 0f && dist > num)
				{
					start = i;
					end = ((i + 1 != length) ? (i + 1) : 0);
					num5 = num;
					flag = true;
				}
				for (int j = 1; j < splineDetail; j++)
				{
					float t = (float)j / num2;
					float num6 = global::Unity.Mathematics.math.distance(global::UnityEngine.U2D.BezierUtility.BezierPoint(startRightTangent, position, position2, endLeftTangent, t), vector);
					num4 += num6;
					num += num6;
				}
				if (flag)
				{
					float num7 = dist - num5;
					r = num7 / num4;
				}
			}
			return num;
		}

		private global::UnityEngine.Vector3 PlaceObjectInternal(int sp, int ep, float t, global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ShapeControlPoint> shapePoints)
		{
			ep %= shapePoints.Length;
			global::UnityEngine.Vector3 position = shapePoints[sp].position;
			global::UnityEngine.Vector3 position2 = shapePoints[ep].position;
			global::UnityEngine.Vector3 startRightTangent = position + shapePoints[sp].rightTangent;
			global::UnityEngine.Vector3 endLeftTangent = position2 + shapePoints[ep].leftTangent;
			global::UnityEngine.Vector3 vector = global::UnityEngine.U2D.BezierUtility.BezierPoint(startRightTangent, position, position2, endLeftTangent, t);
			global::UnityEngine.Vector3 point = new global::UnityEngine.Vector3(vector.x, vector.y, 0f);
			global::UnityEngine.Matrix4x4 localToWorldMatrix = spriteShapeController.transform.localToWorldMatrix;
			global::UnityEngine.Transform transform = base.gameObject.transform;
			global::UnityEngine.Vector3 vector2 = localToWorldMatrix.MultiplyPoint3x4(point);
			global::UnityEngine.Vector3 vector3 = transform.position;
			if (m_Mode == global::UnityEngine.U2D.SpriteShapeObjectPlacementMode.Auto)
			{
				vector3.y = vector2.y;
			}
			else
			{
				vector3 = vector2;
			}
			transform.position = vector3;
			if (setNormal)
			{
				float num = global::Unity.Mathematics.math.clamp(t, 0.002f, 0.998f);
				global::UnityEngine.Vector3 vector4 = global::UnityEngine.U2D.BezierUtility.BezierPoint(startRightTangent, position, position2, endLeftTangent, num - 0.001f);
				vector = global::UnityEngine.U2D.BezierUtility.BezierPoint(startRightTangent, position, position2, endLeftTangent, num);
				global::UnityEngine.Vector3 vector5 = global::UnityEngine.U2D.BezierUtility.BezierPoint(startRightTangent, position, position2, endLeftTangent, num + 0.001f);
				global::UnityEngine.Vector3 vector6 = global::UnityEngine.Vector3.Normalize(new global::UnityEngine.Vector3(vector4.x, vector4.y, 0f) - new global::UnityEngine.Vector3(vector.x, vector.y, 0f));
				global::UnityEngine.Vector3 b = global::UnityEngine.Vector3.Normalize(new global::UnityEngine.Vector3(vector5.x, vector5.y, 0f) - new global::UnityEngine.Vector3(vector.x, vector.y, 0f));
				float num2 = Angle(global::UnityEngine.Vector3.up, vector6);
				float num3 = Angle(vector6, b);
				float num4 = num2 + num3 * 0.5f;
				if (num3 > 0f)
				{
					num4 = 180f + num4;
				}
				global::UnityEngine.Quaternion quaternion2 = global::UnityEngine.Quaternion.Euler(0f, 0f, num4);
				transform.rotation = localToWorldMatrix.rotation * quaternion2;
			}
			return vector3;
		}

		private global::UnityEngine.Vector3 PlaceObject(global::UnityEngine.U2D.Spline spline, int sp, int ep, ref bool run)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ShapeControlPoint> shapeControlPoints = spriteShapeController.GetShapeControlPoints();
			if (sp > shapeControlPoints.Length || ep > shapeControlPoints.Length)
			{
				run = false;
				return global::UnityEngine.Vector3.zero;
			}
			float num = global::Unity.Mathematics.math.clamp(ratio, 0.0001f, 0.9999f);
			if (ep - sp == 1)
			{
				run = true;
				return PlaceObjectInternal(sp, ep, num, shapeControlPoints);
			}
			int start = 0;
			int end = 0;
			float dist = 0f;
			float r = 0f;
			dist = GetDistance(dist, sp, ep, ref start, ref end, ref r, shapeControlPoints) * num;
			GetDistance(dist, sp, ep, ref start, ref end, ref r, shapeControlPoints);
			if (start >= 0)
			{
				run = true;
				return PlaceObjectInternal(start, end, r, shapeControlPoints);
			}
			run = false;
			return global::UnityEngine.Vector3.zero;
		}

		private int GetSplinePointCount()
		{
			global::UnityEngine.U2D.Spline spline = spriteShapeController.spline;
			int pointCount = spline.GetPointCount();
			return spline.isOpenEnded ? (pointCount - 1) : pointCount;
		}

		private bool Place()
		{
			int splinePointCount = GetSplinePointCount();
			bool run = false;
			if (m_Mode == global::UnityEngine.U2D.SpriteShapeObjectPlacementMode.Manual)
			{
				int num = global::Unity.Mathematics.math.clamp(startPoint, 0, splinePointCount);
				int num2 = global::Unity.Mathematics.math.clamp(endPoint, 0, splinePointCount);
				if (num >= num2)
				{
					endPoint = splinePointCount;
					global::UnityEngine.Debug.LogWarning("Invalid End point and it has been clamped", base.transform);
				}
				PlaceObject(spriteShapeController.spline, num, num2, ref run);
				return run;
			}
			_ = kMaxDistance;
			global::UnityEngine.Vector3 position = base.transform.position;
			_ = global::UnityEngine.Vector3.zero;
			int num3 = 0;
			int num4 = 0;
			float num5 = 100f;
			float num6 = kMaxDistance;
			global::UnityEngine.U2D.Spline spline = spriteShapeController.spline;
			global::UnityEngine.Matrix4x4 localToWorldMatrix = spriteShapeController.transform.localToWorldMatrix;
			int pointCount = spline.GetPointCount();
			for (int i = 0; i < splinePointCount; i++)
			{
				int num7 = (i + 1) % spline.GetPointCount();
				global::UnityEngine.Vector3 vector = localToWorldMatrix.MultiplyPoint3x4(spline.GetPosition(i));
				global::UnityEngine.Vector3 vector2 = localToWorldMatrix.MultiplyPoint3x4(spline.GetPosition(num7));
				global::UnityEngine.Vector3 startTangent = spline.GetRightTangent(i) + vector;
				global::UnityEngine.Vector3 endTangent = spline.GetLeftTangent(num7) + vector2;
				float t;
				float magnitude = (global::UnityEngine.U2D.BezierUtility.ClosestPointOnCurve(position, vector, vector2, startTangent, endTangent, 0.0001f, out t) - position).magnitude;
				if (magnitude < num6)
				{
					num3 = i;
					num4 = num7;
					num5 = t;
					num6 = magnitude;
				}
			}
			if (num3 >= 0 && num3 < pointCount && num4 >= 0 && num4 < pointCount)
			{
				startPoint = num3;
				endPoint = ((num4 == 0) ? (num3 + 1) : num4);
				ratio = num5;
				position = PlaceObject(spline, startPoint, endPoint, ref run);
			}
			return run;
		}

		private void Start()
		{
			PlaceObjectOnHashChange();
		}

		private void Update()
		{
			PlaceObjectOnHashChange();
		}
	}
}
