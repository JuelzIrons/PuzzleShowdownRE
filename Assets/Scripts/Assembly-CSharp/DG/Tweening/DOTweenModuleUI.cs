namespace DG.Tweening
{
	public static class DOTweenModuleUI
	{
		public static class Utils
		{
			public static global::UnityEngine.Vector2 SwitchToRectTransform(global::UnityEngine.RectTransform from, global::UnityEngine.RectTransform to)
			{
				global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(from.rect.width * 0.5f + from.rect.xMin, from.rect.height * 0.5f + from.rect.yMin);
				global::UnityEngine.Vector2 screenPoint = global::UnityEngine.RectTransformUtility.WorldToScreenPoint(null, from.position);
				screenPoint += vector;
				global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(to, screenPoint, null, out var localPoint);
				global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2(to.rect.width * 0.5f + to.rect.xMin, to.rect.height * 0.5f + to.rect.yMin);
				return to.anchoredPosition + localPoint - vector2;
			}
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOFade(this global::UnityEngine.CanvasGroup target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.alpha, delegate(float x)
			{
				target.alpha = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOColor(this global::UnityEngine.UI.Graphic target, global::UnityEngine.Color endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOFade(this global::UnityEngine.UI.Graphic target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.ToAlpha(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOColor(this global::UnityEngine.UI.Image target, global::UnityEngine.Color endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOFade(this global::UnityEngine.UI.Image target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.ToAlpha(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOFillAmount(this global::UnityEngine.UI.Image target, float endValue, float duration)
		{
			if (endValue > 1f)
			{
				endValue = 1f;
			}
			else if (endValue < 0f)
			{
				endValue = 0f;
			}
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.fillAmount, delegate(float x)
			{
				target.fillAmount = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Sequence DOGradientColor(this global::UnityEngine.UI.Image target, global::UnityEngine.Gradient gradient, float duration)
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

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOFlexibleSize(this global::UnityEngine.UI.LayoutElement target, global::UnityEngine.Vector2 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => new global::UnityEngine.Vector2(target.flexibleWidth, target.flexibleHeight), delegate(global::UnityEngine.Vector2 x)
			{
				target.flexibleWidth = x.x;
				target.flexibleHeight = x.y;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOMinSize(this global::UnityEngine.UI.LayoutElement target, global::UnityEngine.Vector2 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => new global::UnityEngine.Vector2(target.minWidth, target.minHeight), delegate(global::UnityEngine.Vector2 x)
			{
				target.minWidth = x.x;
				target.minHeight = x.y;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOPreferredSize(this global::UnityEngine.UI.LayoutElement target, global::UnityEngine.Vector2 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => new global::UnityEngine.Vector2(target.preferredWidth, target.preferredHeight), delegate(global::UnityEngine.Vector2 x)
			{
				target.preferredWidth = x.x;
				target.preferredHeight = x.y;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOColor(this global::UnityEngine.UI.Outline target, global::UnityEngine.Color endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.effectColor, delegate(global::UnityEngine.Color x)
			{
				target.effectColor = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOFade(this global::UnityEngine.UI.Outline target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.ToAlpha(() => target.effectColor, delegate(global::UnityEngine.Color x)
			{
				target.effectColor = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOScale(this global::UnityEngine.UI.Outline target, global::UnityEngine.Vector2 endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.effectDistance, delegate(global::UnityEngine.Vector2 x)
			{
				target.effectDistance = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOAnchorPos(this global::UnityEngine.RectTransform target, global::UnityEngine.Vector2 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.anchoredPosition, delegate(global::UnityEngine.Vector2 x)
			{
				target.anchoredPosition = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOAnchorPosX(this global::UnityEngine.RectTransform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.anchoredPosition, delegate(global::UnityEngine.Vector2 x)
			{
				target.anchoredPosition = x;
			}, new global::UnityEngine.Vector2(endValue, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.X, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOAnchorPosY(this global::UnityEngine.RectTransform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.anchoredPosition, delegate(global::UnityEngine.Vector2 x)
			{
				target.anchoredPosition = x;
			}, new global::UnityEngine.Vector2(0f, endValue), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Y, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOAnchorPos3D(this global::UnityEngine.RectTransform target, global::UnityEngine.Vector3 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.anchoredPosition3D, delegate(global::UnityEngine.Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOAnchorPos3DX(this global::UnityEngine.RectTransform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.anchoredPosition3D, delegate(global::UnityEngine.Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, new global::UnityEngine.Vector3(endValue, 0f, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.X, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOAnchorPos3DY(this global::UnityEngine.RectTransform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.anchoredPosition3D, delegate(global::UnityEngine.Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, new global::UnityEngine.Vector3(0f, endValue, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Y, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOAnchorPos3DZ(this global::UnityEngine.RectTransform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.anchoredPosition3D, delegate(global::UnityEngine.Vector3 x)
			{
				target.anchoredPosition3D = x;
			}, new global::UnityEngine.Vector3(0f, 0f, endValue), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Z, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOAnchorMax(this global::UnityEngine.RectTransform target, global::UnityEngine.Vector2 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.anchorMax, delegate(global::UnityEngine.Vector2 x)
			{
				target.anchorMax = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOAnchorMin(this global::UnityEngine.RectTransform target, global::UnityEngine.Vector2 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.anchorMin, delegate(global::UnityEngine.Vector2 x)
			{
				target.anchorMin = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOPivot(this global::UnityEngine.RectTransform target, global::UnityEngine.Vector2 endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.pivot, delegate(global::UnityEngine.Vector2 x)
			{
				target.pivot = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOPivotX(this global::UnityEngine.RectTransform target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.pivot, delegate(global::UnityEngine.Vector2 x)
			{
				target.pivot = x;
			}, new global::UnityEngine.Vector2(endValue, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.X).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOPivotY(this global::UnityEngine.RectTransform target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.pivot, delegate(global::UnityEngine.Vector2 x)
			{
				target.pivot = x;
			}, new global::UnityEngine.Vector2(0f, endValue), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Y).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOSizeDelta(this global::UnityEngine.RectTransform target, global::UnityEngine.Vector2 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.sizeDelta, delegate(global::UnityEngine.Vector2 x)
			{
				target.sizeDelta = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener DOPunchAnchorPos(this global::UnityEngine.RectTransform target, global::UnityEngine.Vector2 punch, float duration, int vibrato = 10, float elasticity = 1f, bool snapping = false)
		{
			return global::DG.Tweening.DOTween.Punch(() => target.anchoredPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.anchoredPosition = x;
			}, punch, duration, vibrato, elasticity).SetTarget(target).SetOptions(snapping);
		}

		public static global::DG.Tweening.Tweener DOShakeAnchorPos(this global::UnityEngine.RectTransform target, float duration, float strength = 100f, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.anchoredPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.anchoredPosition = x;
			}, duration, strength, vibrato, randomness, ignoreZAxis: true, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake).SetOptions(snapping);
		}

		public static global::DG.Tweening.Tweener DOShakeAnchorPos(this global::UnityEngine.RectTransform target, float duration, global::UnityEngine.Vector2 strength, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.anchoredPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.anchoredPosition = x;
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake).SetOptions(snapping);
		}

		public static global::DG.Tweening.Sequence DOJumpAnchorPos(this global::UnityEngine.RectTransform target, global::UnityEngine.Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = 0f;
			float offsetY = -1f;
			bool offsetYSet = false;
			global::DG.Tweening.Sequence s = global::DG.Tweening.DOTween.Sequence();
			global::DG.Tweening.Tween t = global::DG.Tweening.DOTween.To(() => target.anchoredPosition, delegate(global::UnityEngine.Vector2 x)
			{
				target.anchoredPosition = x;
			}, new global::UnityEngine.Vector2(0f, jumpPower), duration / (float)(numJumps * 2)).SetOptions(global::DG.Tweening.AxisConstraint.Y, snapping).SetEase(global::DG.Tweening.Ease.OutQuad)
				.SetRelative()
				.SetLoops(numJumps * 2, global::DG.Tweening.LoopType.Yoyo)
				.OnStart(delegate
				{
					startPosY = target.anchoredPosition.y;
				});
			s.Append(global::DG.Tweening.DOTween.To(() => target.anchoredPosition, delegate(global::UnityEngine.Vector2 x)
			{
				target.anchoredPosition = x;
			}, new global::UnityEngine.Vector2(endValue.x, 0f), duration).SetOptions(global::DG.Tweening.AxisConstraint.X, snapping).SetEase(global::DG.Tweening.Ease.Linear)).Join(t).SetTarget(target)
				.SetEase(global::DG.Tweening.DOTween.defaultEaseType);
			s.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
				}
				global::UnityEngine.Vector2 anchoredPosition = target.anchoredPosition;
				anchoredPosition.y += global::DG.Tweening.DOVirtual.EasedValue(0f, offsetY, s.ElapsedDirectionalPercentage(), global::DG.Tweening.Ease.OutQuad);
				target.anchoredPosition = anchoredPosition;
			});
			return s;
		}

		public static global::DG.Tweening.Tweener DONormalizedPos(this global::UnityEngine.UI.ScrollRect target, global::UnityEngine.Vector2 endValue, float duration, bool snapping = false)
		{
			return global::DG.Tweening.DOTween.To(() => new global::UnityEngine.Vector2(target.horizontalNormalizedPosition, target.verticalNormalizedPosition), delegate(global::UnityEngine.Vector2 x)
			{
				target.horizontalNormalizedPosition = x.x;
				target.verticalNormalizedPosition = x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static global::DG.Tweening.Tweener DOHorizontalNormalizedPos(this global::UnityEngine.UI.ScrollRect target, float endValue, float duration, bool snapping = false)
		{
			return global::DG.Tweening.DOTween.To(() => target.horizontalNormalizedPosition, delegate(float x)
			{
				target.horizontalNormalizedPosition = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static global::DG.Tweening.Tweener DOVerticalNormalizedPos(this global::UnityEngine.UI.ScrollRect target, float endValue, float duration, bool snapping = false)
		{
			return global::DG.Tweening.DOTween.To(() => target.verticalNormalizedPosition, delegate(float x)
			{
				target.verticalNormalizedPosition = x;
			}, endValue, duration).SetOptions(snapping).SetTarget(target);
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOValue(this global::UnityEngine.UI.Slider target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.value, delegate(float x)
			{
				target.value = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOColor(this global::UnityEngine.UI.Text target, global::UnityEngine.Color endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<int, int, global::DG.Tweening.Plugins.Options.NoOptions> DOCounter(this global::UnityEngine.UI.Text target, int fromValue, int endValue, float duration, bool addThousandsSeparator = true, global::System.Globalization.CultureInfo culture = null)
		{
			global::System.Globalization.CultureInfo cInfo = ((!addThousandsSeparator) ? null : (culture ?? global::System.Globalization.CultureInfo.InvariantCulture));
			global::DG.Tweening.Core.TweenerCore<int, int, global::DG.Tweening.Plugins.Options.NoOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => fromValue, delegate(int x)
			{
				fromValue = x;
				target.text = (addThousandsSeparator ? fromValue.ToString("N0", cInfo) : fromValue.ToString());
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOFade(this global::UnityEngine.UI.Text target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.ToAlpha(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<string, string, global::DG.Tweening.Plugins.Options.StringOptions> DOText(this global::UnityEngine.UI.Text target, string endValue, float duration, bool richTextEnabled = true, global::DG.Tweening.ScrambleMode scrambleMode = global::DG.Tweening.ScrambleMode.None, string scrambleChars = null)
		{
			if (endValue == null)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogWarning("You can't pass a NULL string to DOText: an empty string will be used instead to avoid errors");
				}
				endValue = "";
			}
			global::DG.Tweening.Core.TweenerCore<string, string, global::DG.Tweening.Plugins.Options.StringOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.text, delegate(string x)
			{
				target.text = x;
			}, endValue, duration);
			tweenerCore.SetOptions(richTextEnabled, scrambleMode, scrambleChars).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener DOBlendableColor(this global::UnityEngine.UI.Graphic target, global::UnityEngine.Color endValue, float duration)
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

		public static global::DG.Tweening.Tweener DOBlendableColor(this global::UnityEngine.UI.Image target, global::UnityEngine.Color endValue, float duration)
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

		public static global::DG.Tweening.Tweener DOBlendableColor(this global::UnityEngine.UI.Text target, global::UnityEngine.Color endValue, float duration)
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

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> DOShapeCircle(this global::UnityEngine.RectTransform target, global::UnityEngine.Vector2 center, float endValueDegrees, float duration, bool relativeCenter = false, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.CircleOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.Plugins.CirclePlugin.Get(), () => target.anchoredPosition, delegate(global::UnityEngine.Vector2 x)
			{
				target.anchoredPosition = x;
			}, center, duration);
			tweenerCore.SetOptions(endValueDegrees, relativeCenter, snapping).SetTarget(target);
			return tweenerCore;
		}
	}
}
