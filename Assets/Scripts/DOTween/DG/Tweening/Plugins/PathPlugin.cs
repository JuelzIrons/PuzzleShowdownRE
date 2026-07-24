namespace DG.Tweening.Plugins
{
	public class PathPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions>
	{
		public const float MinLookAhead = 0.0001f;

		public override void Reset(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t)
		{
			t.endValue.Destroy();
			t.startValue = (t.endValue = (t.changeValue = null));
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, bool isRelative)
		{
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, global::DG.Tweening.Plugins.Core.PathCore.Path fromValue, bool setImmediately, bool isRelative)
		{
		}

		public static global::DG.Tweening.Plugins.Core.ABSTweenPlugin<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> Get()
		{
			return global::DG.Tweening.Plugins.Core.PluginsManager.GetCustomPlugin<global::DG.Tweening.Plugins.PathPlugin, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions>();
		}

		public override global::DG.Tweening.Plugins.Core.PathCore.Path ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t, global::UnityEngine.Vector3 value)
		{
			return t.endValue;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t)
		{
			if (!t.endValue.isFinalized)
			{
				global::UnityEngine.Vector3 vector = t.getter();
				int num = t.endValue.wps.Length;
				for (int i = 0; i < num; i++)
				{
					t.endValue.wps[i] += vector;
				}
			}
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> t)
		{
			global::UnityEngine.Transform transform = ((t.target is global::UnityEngine.GameObject gameObject) ? gameObject.transform : ((global::UnityEngine.Component)t.target).transform);
			if (t.plugOptions.orientType == global::DG.Tweening.Plugins.Options.OrientType.ToPath)
			{
				t.plugOptions.parent = transform.parent;
			}
			if (t.endValue.isFinalized)
			{
				t.changeValue = t.endValue;
				return;
			}
			global::UnityEngine.Vector3 vector = t.getter();
			global::DG.Tweening.Plugins.Core.PathCore.Path endValue = t.endValue;
			endValue.plugOptions = t.plugOptions;
			int num = endValue.wps.Length;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			if (num <= endValue.minInputWaypoints || !global::DG.Tweening.Core.DOTweenUtils.Vector3AreApproximatelyEqual(endValue.wps[0], vector))
			{
				flag = true;
				num2++;
			}
			if (t.plugOptions.isClosedPath)
			{
				global::UnityEngine.Vector3 vector2 = endValue.wps[num - 1];
				if (endValue.type == global::DG.Tweening.PathType.CubicBezier)
				{
					if (num < 3)
					{
						global::UnityEngine.Debug.LogError("CubicBezier paths must contain waypoints in multiple of 3 excluding the starting point added automatically by DOTween (1: waypoint, 2: IN control point, 3: OUT control point — the minimum amount of waypoints for a single curve is 3)");
					}
					else
					{
						vector2 = endValue.wps[num - 3];
					}
				}
				if (vector2 != vector)
				{
					flag2 = true;
					num2++;
				}
			}
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[num + num2];
			int num3 = (flag ? 1 : 0);
			if (flag)
			{
				array[0] = vector;
			}
			for (int i = 0; i < num; i++)
			{
				array[i + num3] = endValue.wps[i];
			}
			if (flag2)
			{
				array[^1] = array[0];
			}
			endValue.wps = array;
			endValue.addedExtraStartWp = flag;
			endValue.addedExtraEndWp = flag2;
			endValue.FinalizePath(t.plugOptions.isClosedPath, t.plugOptions.lockPositionAxis, vector);
			t.plugOptions.startupRot = transform.rotation;
			t.plugOptions.startupZRot = transform.eulerAngles.z;
			t.changeValue = t.endValue;
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.PathOptions options, float unitsXSecond, global::DG.Tweening.Plugins.Core.PathCore.Path changeValue)
		{
			return changeValue.length / unitsXSecond;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.PathOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<global::UnityEngine.Vector3> getter, global::DG.Tweening.Core.DOSetter<global::UnityEngine.Vector3> setter, float elapsed, global::DG.Tweening.Plugins.Core.PathCore.Path startValue, global::DG.Tweening.Plugins.Core.PathCore.Path changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			if (t.loopType == global::DG.Tweening.LoopType.Incremental && !options.isClosedPath)
			{
				int num = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
				if (num > 0)
				{
					changeValue = changeValue.CloneIncremental(num);
				}
			}
			float perc = global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			float num2 = changeValue.ConvertToConstantPathPerc(perc);
			global::UnityEngine.Vector3 vector = (changeValue.targetPosition = changeValue.GetPoint(num2));
			setter(vector);
			if (options.mode != global::DG.Tweening.PathMode.Ignore && options.orientType != global::DG.Tweening.Plugins.Options.OrientType.None)
			{
				SetOrientation(options, t, changeValue, num2, vector, updateNotice);
			}
			bool flag = !usingInversePosition;
			if (t.isBackwards)
			{
				flag = !flag;
			}
			int waypointIndexFromPerc = changeValue.GetWaypointIndexFromPerc(perc, flag);
			if (waypointIndexFromPerc == t.miscInt)
			{
				return;
			}
			int miscInt = t.miscInt;
			t.miscInt = waypointIndexFromPerc;
			if (t.onWaypointChange == null)
			{
				return;
			}
			bool flag2 = t.isBackwards;
			if (t.hasLoops && t.loopType == global::DG.Tweening.LoopType.Yoyo)
			{
				flag2 = (!t.isBackwards && t.completedLoops % 2 != 0) || (t.isBackwards && t.completedLoops % 2 == 0);
			}
			if (flag2)
			{
				for (int num3 = miscInt - 1; num3 > waypointIndexFromPerc - 1; num3--)
				{
					if (num3 != waypointIndexFromPerc)
					{
						global::DG.Tweening.Tween.OnTweenCallback(t.onWaypointChange, t, num3);
					}
				}
			}
			else
			{
				for (int i = miscInt + 1; i < waypointIndexFromPerc; i++)
				{
					if (i != waypointIndexFromPerc)
					{
						global::DG.Tweening.Tween.OnTweenCallback(t.onWaypointChange, t, i);
					}
				}
			}
			if (newCompletedSteps > 0 && !t.isComplete)
			{
				int num4 = ((t.loopType != global::DG.Tweening.LoopType.Yoyo) ? ((!t.isBackwards) ? (changeValue.wps.Length - 1) : 0) : ((t.completedLoops % 2 != 0 && !t.isBackwards) ? (changeValue.wps.Length - 1) : 0));
				if (num4 != waypointIndexFromPerc)
				{
					global::DG.Tweening.Tween.OnTweenCallback(t.onWaypointChange, t, num4);
				}
			}
			global::DG.Tweening.Tween.OnTweenCallback(t.onWaypointChange, t, waypointIndexFromPerc);
		}

		public void SetOrientation(global::DG.Tweening.Plugins.Options.PathOptions options, global::DG.Tweening.Tween t, global::DG.Tweening.Plugins.Core.PathCore.Path path, float pathPerc, global::UnityEngine.Vector3 tPos, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			global::UnityEngine.Transform transform = ((t.target is global::UnityEngine.GameObject gameObject) ? gameObject.transform : ((global::UnityEngine.Component)t.target).transform);
			global::UnityEngine.Quaternion newRot = global::UnityEngine.Quaternion.identity;
			global::UnityEngine.Vector3 position = transform.position;
			if (updateNotice == global::DG.Tweening.Core.Enums.UpdateNotice.RewindStep)
			{
				transform.rotation = options.startupRot;
			}
			switch (options.orientType)
			{
			case global::DG.Tweening.Plugins.Options.OrientType.LookAtPosition:
				path.lookAtPosition = options.lookAtPosition;
				newRot = global::UnityEngine.Quaternion.LookRotation(options.lookAtPosition - position, options.stableZRotation ? global::UnityEngine.Vector3.up : transform.up);
				break;
			case global::DG.Tweening.Plugins.Options.OrientType.LookAtTransform:
				if (options.lookAtTransform != null)
				{
					path.lookAtPosition = options.lookAtTransform.position;
					newRot = global::UnityEngine.Quaternion.LookRotation(options.lookAtTransform.position - position, options.stableZRotation ? global::UnityEngine.Vector3.up : transform.up);
				}
				break;
			case global::DG.Tweening.Plugins.Options.OrientType.ToPath:
			{
				global::UnityEngine.Vector3 vector;
				if (path.type == global::DG.Tweening.PathType.Linear && options.lookAhead <= 0.0001f)
				{
					vector = tPos + path.wps[path.linearWPIndex] - path.wps[path.linearWPIndex - 1];
				}
				else
				{
					float num = pathPerc + options.lookAhead;
					if (num > 1f)
					{
						num = (options.isClosedPath ? (num - 1f) : ((path.type == global::DG.Tweening.PathType.Linear) ? 1f : 1.00001f));
					}
					vector = path.GetPoint(num);
				}
				if (path.type == global::DG.Tweening.PathType.Linear)
				{
					global::UnityEngine.Vector3 vector2 = path.wps[path.wps.Length - 1];
					if (vector == vector2)
					{
						vector = ((tPos == vector2) ? (vector2 + (vector2 - path.wps[path.wps.Length - 2])) : vector2);
					}
				}
				global::UnityEngine.Vector3 upwards = transform.up;
				bool flag = options.parent != null;
				bool flag2 = options.useLocalPosition && flag;
				if (flag2)
				{
					vector = options.parent.TransformPoint(vector);
				}
				if (options.lockRotationAxis != global::DG.Tweening.AxisConstraint.None)
				{
					if ((options.lockRotationAxis & global::DG.Tweening.AxisConstraint.X) == global::DG.Tweening.AxisConstraint.X)
					{
						global::UnityEngine.Vector3 position2 = transform.InverseTransformPoint(vector);
						position2.y = 0f;
						vector = transform.TransformPoint(position2);
						upwards = (flag2 ? options.parent.up : global::UnityEngine.Vector3.up);
					}
					if ((options.lockRotationAxis & global::DG.Tweening.AxisConstraint.Y) == global::DG.Tweening.AxisConstraint.Y)
					{
						global::UnityEngine.Vector3 position3 = transform.InverseTransformPoint(vector);
						if (position3.z < 0f)
						{
							position3.z = 0f - position3.z;
						}
						position3.x = 0f;
						vector = transform.TransformPoint(position3);
					}
					if ((options.lockRotationAxis & global::DG.Tweening.AxisConstraint.Z) == global::DG.Tweening.AxisConstraint.Z)
					{
						upwards = ((!flag2) ? transform.TransformDirection(global::UnityEngine.Vector3.up) : options.parent.TransformDirection(global::UnityEngine.Vector3.up));
						upwards.z = options.startupZRot;
					}
				}
				if (options.mode == global::DG.Tweening.PathMode.Full3D)
				{
					global::UnityEngine.Vector3 vector3 = vector - position;
					if (vector3 == global::UnityEngine.Vector3.zero)
					{
						vector3 = transform.forward;
					}
					if (flag)
					{
						vector3 = DivideVectorByVector(vector3, options.parent.localScale);
					}
					newRot = global::UnityEngine.Quaternion.LookRotation(vector3, upwards);
					break;
				}
				if (flag)
				{
					global::UnityEngine.Vector3 vector4 = DivideVectorByVector(vector - position, options.parent.localScale);
					vector = position + vector4;
				}
				float y = 0f;
				float num2 = global::DG.Tweening.Core.DOTweenUtils.Angle2D(position, vector);
				if (num2 < 0f)
				{
					num2 = 360f + num2;
				}
				if (options.mode == global::DG.Tweening.PathMode.Sidescroller2D)
				{
					y = ((vector.x < position.x) ? 180 : 0);
					if (num2 > 90f && num2 < 270f)
					{
						num2 = 180f - num2;
					}
				}
				newRot = global::UnityEngine.Quaternion.Euler(0f, y, num2);
				break;
			}
			}
			if (options.hasCustomForwardDirection)
			{
				newRot *= options.forward;
			}
			global::DG.Tweening.Core.DOTweenExternalCommand.Dispatch_SetOrientationOnPath(options, t, newRot, transform);
		}

		private global::UnityEngine.Vector3 DivideVectorByVector(global::UnityEngine.Vector3 vector, global::UnityEngine.Vector3 byVector)
		{
			return new global::UnityEngine.Vector3(vector.x / byVector.x, vector.y / byVector.y, vector.z / byVector.z);
		}

		private global::UnityEngine.Vector3 MultiplyVectorByVector(global::UnityEngine.Vector3 vector, global::UnityEngine.Vector3 byVector)
		{
			return new global::UnityEngine.Vector3(vector.x * byVector.x, vector.y * byVector.y, vector.z * byVector.z);
		}
	}
}
