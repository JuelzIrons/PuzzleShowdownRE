namespace DG.Tweening.Core
{
	public static class DOTweenUtils
	{
		private static global::System.Reflection.Assembly[] _loadedAssemblies;

		private static readonly string[] _defAssembliesToQuery = new string[3] { "DOTween.Modules", "Assembly-CSharp", "Assembly-CSharp-firstpass" };

		internal static global::UnityEngine.Vector3 Vector3FromAngle(float degrees, float magnitude)
		{
			float f = degrees * ((float)global::System.Math.PI / 180f);
			return new global::UnityEngine.Vector3(magnitude * global::UnityEngine.Mathf.Cos(f), magnitude * global::UnityEngine.Mathf.Sin(f), 0f);
		}

		internal static float Angle2D(global::UnityEngine.Vector3 from, global::UnityEngine.Vector3 to)
		{
			global::UnityEngine.Vector2 right = global::UnityEngine.Vector2.right;
			to -= from;
			float num = global::UnityEngine.Vector2.Angle(right, to);
			if (global::UnityEngine.Vector3.Cross(right, to).z > 0f)
			{
				num = 360f - num;
			}
			return num * -1f;
		}

		internal static global::UnityEngine.Vector3 RotateAroundPivot(global::UnityEngine.Vector3 point, global::UnityEngine.Vector3 pivot, global::UnityEngine.Quaternion rotation)
		{
			return rotation * (point - pivot) + pivot;
		}

		public static global::UnityEngine.Vector2 GetPointOnCircle(global::UnityEngine.Vector2 center, float radius, float degrees)
		{
			degrees = 90f - degrees;
			float f = degrees * ((float)global::System.Math.PI / 180f);
			return center + new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Cos(f), global::UnityEngine.Mathf.Sin(f)) * radius;
		}

		internal static bool Vector3AreApproximatelyEqual(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			if (global::UnityEngine.Mathf.Approximately(a.x, b.x) && global::UnityEngine.Mathf.Approximately(a.y, b.y))
			{
				return global::UnityEngine.Mathf.Approximately(a.z, b.z);
			}
			return false;
		}

		internal static global::System.Type GetLooseScriptType(string typeName)
		{
			for (int i = 0; i < _defAssembliesToQuery.Length; i++)
			{
				global::System.Type type = global::System.Type.GetType($"{typeName}, {_defAssembliesToQuery[i]}");
				if ((object)type != null)
				{
					return type;
				}
			}
			if (_loadedAssemblies == null)
			{
				_loadedAssemblies = global::System.AppDomain.CurrentDomain.GetAssemblies();
			}
			for (int j = 0; j < _loadedAssemblies.Length; j++)
			{
				global::System.Type type2 = global::System.Type.GetType($"{typeName}, {_loadedAssemblies[j].GetName()}");
				if ((object)type2 != null)
				{
					return type2;
				}
			}
			return null;
		}
	}
}
