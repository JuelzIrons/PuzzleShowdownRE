namespace DG.Tweening.Plugins.Core.PathCore
{
	[global::System.Serializable]
	public class Path
	{
		private static global::DG.Tweening.Plugins.Core.PathCore.CatmullRomDecoder _catmullRomDecoder;

		private static global::DG.Tweening.Plugins.Core.PathCore.LinearDecoder _linearDecoder;

		private static global::DG.Tweening.Plugins.Core.PathCore.CubicBezierDecoder _cubicBezierDecoder;

		public float[] wpLengths;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Vector3[] wps;

		[global::UnityEngine.SerializeField]
		internal global::DG.Tweening.PathType type;

		[global::UnityEngine.SerializeField]
		internal int subdivisionsXSegment;

		[global::UnityEngine.SerializeField]
		internal int subdivisions;

		[global::UnityEngine.SerializeField]
		internal global::DG.Tweening.Plugins.Core.PathCore.ControlPoint[] controlPoints;

		[global::UnityEngine.SerializeField]
		internal float length;

		[global::UnityEngine.SerializeField]
		internal bool isFinalized;

		[global::UnityEngine.SerializeField]
		internal float[] timesTable;

		[global::UnityEngine.SerializeField]
		internal float[] lengthsTable;

		internal int linearWPIndex = -1;

		internal bool addedExtraStartWp;

		internal bool addedExtraEndWp;

		internal global::DG.Tweening.Plugins.Options.PathOptions plugOptions;

		private global::DG.Tweening.Plugins.Core.PathCore.Path _incrementalClone;

		private int _incrementalIndex;

		private global::DG.Tweening.Plugins.Core.PathCore.ABSPathDecoder _decoder;

		private bool _changed;

		internal global::UnityEngine.Vector3[] nonLinearDrawWps;

		internal global::UnityEngine.Vector3 targetPosition;

		internal global::UnityEngine.Vector3? lookAtPosition;

		internal global::UnityEngine.Color gizmoColor = new global::UnityEngine.Color(1f, 1f, 1f, 0.7f);

		internal int minInputWaypoints => _decoder.minInputWaypoints;

		public Path(global::DG.Tweening.PathType type, global::UnityEngine.Vector3[] waypoints, int subdivisionsXSegment, global::UnityEngine.Color? gizmoColor = null)
		{
			this.type = type;
			this.subdivisionsXSegment = subdivisionsXSegment;
			if (gizmoColor.HasValue)
			{
				this.gizmoColor = gizmoColor.Value;
			}
			AssignWaypoints(waypoints, cloneWps: true);
			AssignDecoder(type);
			if (global::DG.Tweening.Core.TweenManager.isUnityEditor)
			{
				global::DG.Tweening.DOTween.GizmosDelegates.Add(Draw);
			}
		}

		internal Path()
		{
		}

		internal void FinalizePath(bool isClosedPath, global::DG.Tweening.AxisConstraint lockPositionAxes, global::UnityEngine.Vector3 currTargetVal)
		{
			if (lockPositionAxes != global::DG.Tweening.AxisConstraint.None)
			{
				bool flag = (lockPositionAxes & global::DG.Tweening.AxisConstraint.X) == global::DG.Tweening.AxisConstraint.X;
				bool flag2 = (lockPositionAxes & global::DG.Tweening.AxisConstraint.Y) == global::DG.Tweening.AxisConstraint.Y;
				bool flag3 = (lockPositionAxes & global::DG.Tweening.AxisConstraint.Z) == global::DG.Tweening.AxisConstraint.Z;
				for (int i = 0; i < wps.Length; i++)
				{
					global::UnityEngine.Vector3 vector = wps[i];
					wps[i] = new global::UnityEngine.Vector3(flag ? currTargetVal.x : vector.x, flag2 ? currTargetVal.y : vector.y, flag3 ? currTargetVal.z : vector.z);
				}
			}
			_decoder.FinalizePath(this, wps, isClosedPath);
			isFinalized = true;
		}

		internal global::UnityEngine.Vector3 GetPoint(float perc, bool convertToConstantPerc = false)
		{
			if (convertToConstantPerc)
			{
				perc = ConvertToConstantPathPerc(perc);
			}
			return _decoder.GetPoint(perc, wps, this, controlPoints);
		}

		internal float ConvertToConstantPathPerc(float perc)
		{
			if (type == global::DG.Tweening.PathType.Linear)
			{
				return perc;
			}
			if (perc > 0f && perc < 1f)
			{
				if (length <= 0f)
				{
					return perc;
				}
				float num = length * perc;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				int num6 = lengthsTable.Length;
				for (int i = 0; i < num6; i++)
				{
					if (lengthsTable[i] > num)
					{
						num4 = timesTable[i];
						num5 = lengthsTable[i];
						if (i > 0)
						{
							num3 = lengthsTable[i - 1];
						}
						break;
					}
					num2 = timesTable[i];
				}
				perc = num2 + (num - num3) / (num5 - num3) * (num4 - num2);
			}
			if (perc > 1f)
			{
				perc = 1f;
			}
			else if (perc < 0f)
			{
				perc = 0f;
			}
			return perc;
		}

		internal int GetWaypointIndexFromPerc(float perc, bool isMovingForward)
		{
			if (perc >= 1f)
			{
				return wps.Length - 1;
			}
			if (perc <= 0f)
			{
				return 0;
			}
			float num = length * perc;
			float num2 = 0f;
			int i = 0;
			for (int num3 = wpLengths.Length; i < num3; i++)
			{
				num2 += wpLengths[i];
				if (i == num3 - 1)
				{
					if (!isMovingForward)
					{
						return i;
					}
					return i - 1;
				}
				if (num2 < num)
				{
					continue;
				}
				if (num2 > num)
				{
					if (!isMovingForward)
					{
						return i;
					}
					return i - 1;
				}
				return i;
			}
			return 0;
		}

		internal static global::UnityEngine.Vector3[] GetDrawPoints(global::DG.Tweening.Plugins.Core.PathCore.Path p, int drawSubdivisionsXSegment)
		{
			int num = p.wps.Length;
			if (p.type == global::DG.Tweening.PathType.Linear)
			{
				return p.wps;
			}
			int num2 = num * drawSubdivisionsXSegment;
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[num2 + 1];
			for (int i = 0; i <= num2; i++)
			{
				float perc = (float)i / (float)num2;
				global::UnityEngine.Vector3 point = p.GetPoint(perc);
				array[i] = point;
			}
			return array;
		}

		internal static void RefreshNonLinearDrawWps(global::DG.Tweening.Plugins.Core.PathCore.Path p)
		{
			int num = p.wps.Length * 10;
			if (p.nonLinearDrawWps == null || p.nonLinearDrawWps.Length != num + 1)
			{
				p.nonLinearDrawWps = new global::UnityEngine.Vector3[num + 1];
			}
			for (int i = 0; i <= num; i++)
			{
				float perc = (float)i / (float)num;
				global::UnityEngine.Vector3 point = p.GetPoint(perc);
				p.nonLinearDrawWps[i] = point;
			}
		}

		internal void Destroy()
		{
			if (global::DG.Tweening.Core.TweenManager.isUnityEditor)
			{
				global::DG.Tweening.DOTween.GizmosDelegates.Remove(Draw);
			}
			wps = null;
			wpLengths = (timesTable = (lengthsTable = null));
			nonLinearDrawWps = null;
			isFinalized = false;
		}

		internal global::DG.Tweening.Plugins.Core.PathCore.Path CloneIncremental(int loopIncrement)
		{
			if (_incrementalClone != null)
			{
				if (_incrementalIndex == loopIncrement)
				{
					return _incrementalClone;
				}
				_incrementalClone.Destroy();
			}
			int num = wps.Length;
			global::UnityEngine.Vector3 vector = wps[num - 1] - wps[0];
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[wps.Length];
			for (int i = 0; i < num; i++)
			{
				array[i] = wps[i] + vector * loopIncrement;
			}
			int num2 = controlPoints.Length;
			global::DG.Tweening.Plugins.Core.PathCore.ControlPoint[] array2 = new global::DG.Tweening.Plugins.Core.PathCore.ControlPoint[num2];
			for (int j = 0; j < num2; j++)
			{
				array2[j] = controlPoints[j] + vector * loopIncrement;
			}
			global::UnityEngine.Vector3[] array3 = null;
			if (nonLinearDrawWps != null)
			{
				int num3 = nonLinearDrawWps.Length;
				array3 = new global::UnityEngine.Vector3[num3];
				for (int k = 0; k < num3; k++)
				{
					array3[k] = nonLinearDrawWps[k] + vector * loopIncrement;
				}
			}
			_incrementalClone = new global::DG.Tweening.Plugins.Core.PathCore.Path();
			_incrementalIndex = loopIncrement;
			_incrementalClone.type = type;
			_incrementalClone.subdivisionsXSegment = subdivisionsXSegment;
			_incrementalClone.subdivisions = subdivisions;
			_incrementalClone.wps = array;
			_incrementalClone.controlPoints = array2;
			if (global::DG.Tweening.Core.TweenManager.isUnityEditor)
			{
				global::DG.Tweening.DOTween.GizmosDelegates.Add(_incrementalClone.Draw);
			}
			_incrementalClone.length = length;
			_incrementalClone.wpLengths = wpLengths;
			_incrementalClone.timesTable = timesTable;
			_incrementalClone.lengthsTable = lengthsTable;
			_incrementalClone._decoder = _decoder;
			_incrementalClone.nonLinearDrawWps = array3;
			_incrementalClone.targetPosition = targetPosition;
			_incrementalClone.lookAtPosition = lookAtPosition;
			_incrementalClone.isFinalized = true;
			return _incrementalClone;
		}

		internal void AssignWaypoints(global::UnityEngine.Vector3[] newWps, bool cloneWps = false)
		{
			if (cloneWps)
			{
				int num = newWps.Length;
				wps = new global::UnityEngine.Vector3[num];
				for (int i = 0; i < num; i++)
				{
					wps[i] = newWps[i];
				}
			}
			else
			{
				wps = newWps;
			}
		}

		internal void AssignDecoder(global::DG.Tweening.PathType pathType)
		{
			type = pathType;
			switch (pathType)
			{
			case global::DG.Tweening.PathType.Linear:
				if (_linearDecoder == null)
				{
					_linearDecoder = new global::DG.Tweening.Plugins.Core.PathCore.LinearDecoder();
				}
				_decoder = _linearDecoder;
				break;
			case global::DG.Tweening.PathType.CubicBezier:
				if (_cubicBezierDecoder == null)
				{
					_cubicBezierDecoder = new global::DG.Tweening.Plugins.Core.PathCore.CubicBezierDecoder();
				}
				_decoder = _cubicBezierDecoder;
				break;
			default:
				if (_catmullRomDecoder == null)
				{
					_catmullRomDecoder = new global::DG.Tweening.Plugins.Core.PathCore.CatmullRomDecoder();
				}
				_decoder = _catmullRomDecoder;
				break;
			}
		}

		internal void Draw()
		{
			Draw(this);
		}

		private static void Draw(global::DG.Tweening.Plugins.Core.PathCore.Path p)
		{
			if (p.timesTable == null)
			{
				return;
			}
			global::UnityEngine.Color color = p.gizmoColor;
			color.a *= 0.5f;
			global::UnityEngine.Gizmos.color = p.gizmoColor;
			int num = p.wps.Length;
			if (p._changed || (p.type != global::DG.Tweening.PathType.Linear && p.nonLinearDrawWps == null))
			{
				p._changed = false;
				if (p.type != global::DG.Tweening.PathType.Linear)
				{
					RefreshNonLinearDrawWps(p);
				}
			}
			if (p.type == global::DG.Tweening.PathType.Linear)
			{
				global::UnityEngine.Vector3 to = ConvertToDrawPoint(p.wps[0], p.plugOptions);
				for (int i = 0; i < num; i++)
				{
					global::UnityEngine.Vector3 vector = ConvertToDrawPoint(p.wps[i], p.plugOptions);
					global::UnityEngine.Gizmos.DrawLine(vector, to);
					to = vector;
				}
			}
			else
			{
				global::UnityEngine.Vector3 to = ConvertToDrawPoint(p.nonLinearDrawWps[0], p.plugOptions);
				int num2 = p.nonLinearDrawWps.Length;
				for (int j = 1; j < num2; j++)
				{
					global::UnityEngine.Vector3 vector2 = ConvertToDrawPoint(p.nonLinearDrawWps[j], p.plugOptions);
					global::UnityEngine.Gizmos.DrawLine(vector2, to);
					to = vector2;
				}
			}
			global::UnityEngine.Gizmos.color = color;
			for (int k = 0; k < num; k++)
			{
				global::UnityEngine.Gizmos.DrawSphere(ConvertToDrawPoint(p.wps[k], p.plugOptions), 0.075f);
			}
			if (p.lookAtPosition.HasValue)
			{
				global::UnityEngine.Vector3 value = p.lookAtPosition.Value;
				global::UnityEngine.Gizmos.DrawLine(p.targetPosition, value);
				global::UnityEngine.Gizmos.DrawWireSphere(value, 0.075f);
			}
		}

		private static global::UnityEngine.Vector3 ConvertToDrawPoint(global::UnityEngine.Vector3 wp, global::DG.Tweening.Plugins.Options.PathOptions plugOptions)
		{
			if (!plugOptions.useLocalPosition || plugOptions.parent == null)
			{
				return wp;
			}
			return plugOptions.parent.TransformPoint(wp);
		}
	}
}
