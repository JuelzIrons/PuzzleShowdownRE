namespace DG.Tweening
{
	public static class DOTweenModuleUtils
	{
		public static class Physics
		{
			public static void SetOrientationOnPath(global::DG.Tweening.Plugins.Options.PathOptions options, global::DG.Tweening.Tween t, global::UnityEngine.Quaternion newRot, global::UnityEngine.Transform trans)
			{
				if (options.isRigidbody)
				{
					((global::UnityEngine.Rigidbody)t.target).rotation = newRot;
				}
				else
				{
					trans.rotation = newRot;
				}
			}

			public static bool HasRigidbody2D(global::UnityEngine.Component target)
			{
				return target.GetComponent<global::UnityEngine.Rigidbody2D>() != null;
			}

			[global::UnityEngine.Scripting.Preserve]
			public static bool HasRigidbody(global::UnityEngine.Component target)
			{
				return target.GetComponent<global::UnityEngine.Rigidbody>() != null;
			}

			[global::UnityEngine.Scripting.Preserve]
			public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> CreateDOTweenPathTween(global::UnityEngine.MonoBehaviour target, bool tweenRigidbody, bool isLocal, global::DG.Tweening.Plugins.Core.PathCore.Path path, float duration, global::DG.Tweening.PathMode pathMode)
			{
				global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> result = null;
				bool flag = false;
				if (tweenRigidbody)
				{
					global::UnityEngine.Rigidbody component = target.GetComponent<global::UnityEngine.Rigidbody>();
					if (component != null)
					{
						flag = true;
						result = (isLocal ? component.DOLocalPath(path, duration, pathMode) : component.DOPath(path, duration, pathMode));
					}
				}
				if (!flag && tweenRigidbody)
				{
					global::UnityEngine.Rigidbody2D component2 = target.GetComponent<global::UnityEngine.Rigidbody2D>();
					if (component2 != null)
					{
						flag = true;
						result = (isLocal ? component2.DOLocalPath(path, duration, pathMode) : component2.DOPath(path, duration, pathMode));
					}
				}
				if (!flag)
				{
					result = (isLocal ? target.transform.DOLocalPath(path, duration, pathMode) : target.transform.DOPath(path, duration, pathMode));
				}
				return result;
			}
		}

		private static bool _initialized;

		[global::UnityEngine.Scripting.Preserve]
		public static void Init()
		{
			if (!_initialized)
			{
				_initialized = true;
				global::DG.Tweening.Core.DOTweenExternalCommand.SetOrientationOnPath += global::DG.Tweening.DOTweenModuleUtils.Physics.SetOrientationOnPath;
			}
		}

		[global::UnityEngine.Scripting.Preserve]
		private static void Preserver()
		{
			global::System.AppDomain.CurrentDomain.GetAssemblies();
			typeof(global::UnityEngine.MonoBehaviour).GetMethod("Stub");
		}
	}
}
