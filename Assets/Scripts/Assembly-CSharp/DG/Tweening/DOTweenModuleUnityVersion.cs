namespace DG.Tweening
{
	public static class DOTweenModuleUnityVersion
	{
		public static global::DG.Tweening.Sequence DOGradientColor(this global::UnityEngine.Material target, global::UnityEngine.Gradient gradient, float duration)
		{
			global::DG.Tweening.Sequence sequence = global::DG.Tweening.DOTween.Sequence();
			global::UnityEngine.GradientColorKey[] colorKeys = gradient.colorKeys;
			int num = colorKeys.Length;
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.GradientColorKey gradientColorKey = colorKeys[i];
				if (i == 0 && gradientColorKey.time <= 0f)
				{
					target.color = gradientColorKey.color;
					continue;
				}
				float duration2 = ((i == num - 1) ? (duration - sequence.Duration(includeLoops: false)) : (duration * ((i == 0) ? gradientColorKey.time : (gradientColorKey.time - colorKeys[i - 1].time))));
				sequence.Append(target.DOColor(gradientColorKey.color, duration2).SetEase(global::DG.Tweening.Ease.Linear));
			}
			sequence.SetTarget(target);
			return sequence;
		}

		public static global::DG.Tweening.Sequence DOGradientColor(this global::UnityEngine.Material target, global::UnityEngine.Gradient gradient, string property, float duration)
		{
			global::DG.Tweening.Sequence sequence = global::DG.Tweening.DOTween.Sequence();
			global::UnityEngine.GradientColorKey[] colorKeys = gradient.colorKeys;
			int num = colorKeys.Length;
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.GradientColorKey gradientColorKey = colorKeys[i];
				if (i == 0 && gradientColorKey.time <= 0f)
				{
					target.SetColor(property, gradientColorKey.color);
					continue;
				}
				float duration2 = ((i == num - 1) ? (duration - sequence.Duration(includeLoops: false)) : (duration * ((i == 0) ? gradientColorKey.time : (gradientColorKey.time - colorKeys[i - 1].time))));
				sequence.Append(target.DOColor(gradientColorKey.color, property, duration2).SetEase(global::DG.Tweening.Ease.Linear));
			}
			sequence.SetTarget(target);
			return sequence;
		}

		public static global::UnityEngine.CustomYieldInstruction WaitForCompletion(this global::DG.Tweening.Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new global::DG.Tweening.DOTweenCYInstruction.WaitForCompletion(t);
		}

		public static global::UnityEngine.CustomYieldInstruction WaitForRewind(this global::DG.Tweening.Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new global::DG.Tweening.DOTweenCYInstruction.WaitForRewind(t);
		}

		public static global::UnityEngine.CustomYieldInstruction WaitForKill(this global::DG.Tweening.Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new global::DG.Tweening.DOTweenCYInstruction.WaitForKill(t);
		}

		public static global::UnityEngine.CustomYieldInstruction WaitForElapsedLoops(this global::DG.Tweening.Tween t, int elapsedLoops, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new global::DG.Tweening.DOTweenCYInstruction.WaitForElapsedLoops(t, elapsedLoops);
		}

		public static global::UnityEngine.CustomYieldInstruction WaitForPosition(this global::DG.Tweening.Tween t, float position, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new global::DG.Tweening.DOTweenCYInstruction.WaitForPosition(t, position);
		}

		public static global::UnityEngine.CustomYieldInstruction WaitForStart(this global::DG.Tweening.Tween t, bool returnCustomYieldInstruction)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return new global::DG.Tweening.DOTweenCYInstruction.WaitForStart(t);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOOffset(this global::UnityEngine.Material target, global::UnityEngine.Vector2 endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.GetTextureOffset(propertyID), delegate(global::UnityEngine.Vector2 x)
			{
				target.SetTextureOffset(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOTiling(this global::UnityEngine.Material target, global::UnityEngine.Vector2 endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.GetTextureScale(propertyID), delegate(global::UnityEngine.Vector2 x)
			{
				target.SetTextureScale(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static async global::System.Threading.Tasks.Task AsyncWaitForCompletion(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active && !t.IsComplete())
				{
					await global::System.Threading.Tasks.Task.Yield();
				}
			}
		}

		public static async global::System.Threading.Tasks.Task AsyncWaitForRewind(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active && (!t.playedOnce || t.position * (float)(t.CompletedLoops() + 1) > 0f))
				{
					await global::System.Threading.Tasks.Task.Yield();
				}
			}
		}

		public static async global::System.Threading.Tasks.Task AsyncWaitForKill(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active)
				{
					await global::System.Threading.Tasks.Task.Yield();
				}
			}
		}

		public static async global::System.Threading.Tasks.Task AsyncWaitForElapsedLoops(this global::DG.Tweening.Tween t, int elapsedLoops)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active && t.CompletedLoops() < elapsedLoops)
				{
					await global::System.Threading.Tasks.Task.Yield();
				}
			}
		}

		public static async global::System.Threading.Tasks.Task AsyncWaitForPosition(this global::DG.Tweening.Tween t, float position)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active && t.position * (float)(t.CompletedLoops() + 1) < position)
				{
					await global::System.Threading.Tasks.Task.Yield();
				}
			}
		}

		public static async global::System.Threading.Tasks.Task AsyncWaitForStart(this global::DG.Tweening.Tween t)
		{
			if (!t.active)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogInvalidTween(t);
				}
			}
			else
			{
				while (t.active && !t.playedOnce)
				{
					await global::System.Threading.Tasks.Task.Yield();
				}
			}
		}
	}
}
