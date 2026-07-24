namespace DG.Tweening
{
	public static class DOTweenModulePhysics2D
	{
		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOMove(this global::UnityEngine.Rigidbody2D target, global::UnityEngine.Vector2 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.position, target.MovePosition, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOMoveX(this global::UnityEngine.Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.position, target.MovePosition, new global::UnityEngine.Vector2(endValue, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.X, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOMoveY(this global::UnityEngine.Rigidbody2D target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.position, target.MovePosition, new global::UnityEngine.Vector2(0f, endValue), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Y, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DORotate(this global::UnityEngine.Rigidbody2D target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.rotation, target.MoveRotation, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Sequence DOJump(this global::UnityEngine.Rigidbody2D target, global::UnityEngine.Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = 0f;
			float offsetY = -1f;
			bool offsetYSet = false;
			global::DG.Tweening.Sequence s = global::DG.Tweening.DOTween.Sequence();
			global::DG.Tweening.Tween yTween = global::DG.Tweening.DOTween.To(() => target.position, delegate(global::UnityEngine.Vector2 x)
			{
				target.position = x;
			}, new global::UnityEngine.Vector2(0f, jumpPower), duration / (float)(numJumps * 2)).SetOptions(global::DG.Tweening.AxisConstraint.Y, snapping).SetEase(global::DG.Tweening.Ease.OutQuad)
				.SetRelative()
				.SetLoops(numJumps * 2, global::DG.Tweening.LoopType.Yoyo)
				.OnStart(delegate
				{
					startPosY = target.position.y;
				});
			s.Append(global::DG.Tweening.DOTween.To(() => target.position, delegate(global::UnityEngine.Vector2 x)
			{
				target.position = x;
			}, new global::UnityEngine.Vector2(endValue.x, 0f), duration).SetOptions(global::DG.Tweening.AxisConstraint.X, snapping).SetEase(global::DG.Tweening.Ease.Linear)).Join(yTween).SetTarget(target)
				.SetEase(global::DG.Tweening.DOTween.defaultEaseType);
			yTween.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
				}
				global::UnityEngine.Vector3 vector = target.position;
				vector.y += global::DG.Tweening.DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(), global::DG.Tweening.Ease.OutQuad);
				target.MovePosition(vector);
			});
			return s;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> DOPath(this global::UnityEngine.Rigidbody2D target, global::UnityEngine.Vector2[] path, float duration, global::DG.Tweening.PathType pathType = global::DG.Tweening.PathType.Linear, global::DG.Tweening.PathMode pathMode = global::DG.Tweening.PathMode.Full3D, int resolution = 10, global::UnityEngine.Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			int num = path.Length;
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = path[i];
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.Plugins.PathPlugin.Get(), () => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.MovePosition(x);
			}, new global::DG.Tweening.Plugins.Core.PathCore.Path(pathType, array, resolution, gizmoColor), duration).SetTarget(target).SetUpdate(global::DG.Tweening.UpdateType.Fixed);
			tweenerCore.plugOptions.isRigidbody2D = true;
			tweenerCore.plugOptions.mode = pathMode;
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> DOLocalPath(this global::UnityEngine.Rigidbody2D target, global::UnityEngine.Vector2[] path, float duration, global::DG.Tweening.PathType pathType = global::DG.Tweening.PathType.Linear, global::DG.Tweening.PathMode pathMode = global::DG.Tweening.PathMode.Full3D, int resolution = 10, global::UnityEngine.Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			int num = path.Length;
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = path[i];
			}
			global::UnityEngine.Transform trans = target.transform;
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.Plugins.PathPlugin.Get(), () => trans.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.MovePosition((trans.parent == null) ? x : trans.parent.TransformPoint(x));
			}, new global::DG.Tweening.Plugins.Core.PathCore.Path(pathType, array, resolution, gizmoColor), duration).SetTarget(target).SetUpdate(global::DG.Tweening.UpdateType.Fixed);
			tweenerCore.plugOptions.isRigidbody2D = true;
			tweenerCore.plugOptions.mode = pathMode;
			tweenerCore.plugOptions.useLocalPosition = true;
			return tweenerCore;
		}

		internal static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> DOPath(this global::UnityEngine.Rigidbody2D target, global::DG.Tweening.Plugins.Core.PathCore.Path path, float duration, global::DG.Tweening.PathMode pathMode = global::DG.Tweening.PathMode.Full3D)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.Plugins.PathPlugin.Get(), () => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.MovePosition(x);
			}, path, duration).SetTarget(target);
			tweenerCore.plugOptions.isRigidbody2D = true;
			tweenerCore.plugOptions.mode = pathMode;
			return tweenerCore;
		}

		internal static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> DOLocalPath(this global::UnityEngine.Rigidbody2D target, global::DG.Tweening.Plugins.Core.PathCore.Path path, float duration, global::DG.Tweening.PathMode pathMode = global::DG.Tweening.PathMode.Full3D)
		{
			global::UnityEngine.Transform trans = target.transform;
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.Plugins.PathPlugin.Get(), () => trans.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.MovePosition((trans.parent == null) ? x : trans.parent.TransformPoint(x));
			}, path, duration).SetTarget(target);
			tweenerCore.plugOptions.isRigidbody2D = true;
			tweenerCore.plugOptions.mode = pathMode;
			tweenerCore.plugOptions.useLocalPosition = true;
			return tweenerCore;
		}
	}
}
