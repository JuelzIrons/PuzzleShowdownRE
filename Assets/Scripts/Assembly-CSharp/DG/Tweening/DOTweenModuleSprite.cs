namespace DG.Tweening
{
	public static class DOTweenModuleSprite
	{
		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOColor(this global::UnityEngine.SpriteRenderer target, global::UnityEngine.Color endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOFade(this global::UnityEngine.SpriteRenderer target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.ToAlpha(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Sequence DOGradientColor(this global::UnityEngine.SpriteRenderer target, global::UnityEngine.Gradient gradient, float duration)
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

		public static global::DG.Tweening.Tweener DOBlendableColor(this global::UnityEngine.SpriteRenderer target, global::UnityEngine.Color endValue, float duration)
		{
			endValue -= target.color;
			global::UnityEngine.Color to = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
			return global::DG.Tweening.Core.Extensions.Blendable(global::DG.Tweening.DOTween.To(() => to, delegate(global::UnityEngine.Color x)
			{
				global::UnityEngine.Color color = x - to;
				to = x;
				target.color += color;
			}, endValue, duration)).SetTarget(target);
		}
	}
}
