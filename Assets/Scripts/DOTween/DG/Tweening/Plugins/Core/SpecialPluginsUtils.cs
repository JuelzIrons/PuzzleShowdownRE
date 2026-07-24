namespace DG.Tweening.Plugins.Core
{
	internal static class SpecialPluginsUtils
	{
		internal static bool SetLookAt(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> t)
		{
			global::UnityEngine.Transform transform = t.target as global::UnityEngine.Transform;
			global::UnityEngine.Vector3 endValue = t.endValue;
			endValue -= transform.position;
			switch (t.plugOptions.axisConstraint)
			{
			case global::DG.Tweening.AxisConstraint.X:
				endValue.x = 0f;
				break;
			case global::DG.Tweening.AxisConstraint.Y:
				endValue.y = 0f;
				break;
			case global::DG.Tweening.AxisConstraint.Z:
				endValue.z = 0f;
				break;
			}
			global::UnityEngine.Vector3 eulerAngles = global::UnityEngine.Quaternion.LookRotation(endValue, t.plugOptions.up).eulerAngles;
			t.endValue = eulerAngles;
			return true;
		}

		internal static bool SetPunch(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
		{
			global::UnityEngine.Vector3 vector;
			try
			{
				vector = t.getter();
			}
			catch
			{
				return false;
			}
			t.isRelative = (t.isSpeedBased = false);
			t.easeType = global::DG.Tweening.Ease.OutQuad;
			t.customEase = null;
			int num = t.endValue.Length;
			for (int i = 0; i < num; i++)
			{
				t.endValue[i] = t.endValue[i] + vector;
			}
			return true;
		}

		internal static bool SetShake(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
		{
			if (!SetPunch(t))
			{
				return false;
			}
			t.easeType = global::DG.Tweening.Ease.Linear;
			return true;
		}

		internal static bool SetCameraShakePosition(global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3[], global::DG.Tweening.Plugins.Options.Vector3ArrayOptions> t)
		{
			if (!SetShake(t))
			{
				return false;
			}
			global::UnityEngine.Camera camera = t.target as global::UnityEngine.Camera;
			if (camera == null)
			{
				return false;
			}
			global::UnityEngine.Vector3 vector = t.getter();
			global::UnityEngine.Transform transform = camera.transform;
			int num = t.endValue.Length;
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.Vector3 vector2 = t.endValue[i];
				t.endValue[i] = transform.localRotation * (vector2 - vector) + vector;
			}
			return true;
		}
	}
}
