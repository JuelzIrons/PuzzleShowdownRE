namespace DG.Tweening
{
	public static class ShortcutExtensions
	{
		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOAspect(this global::UnityEngine.Camera target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.aspect, delegate(float x)
			{
				target.aspect = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOColor(this global::UnityEngine.Camera target, global::UnityEngine.Color endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.backgroundColor, delegate(global::UnityEngine.Color x)
			{
				target.backgroundColor = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOFarClipPlane(this global::UnityEngine.Camera target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.farClipPlane, delegate(float x)
			{
				target.farClipPlane = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOFieldOfView(this global::UnityEngine.Camera target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.fieldOfView, delegate(float x)
			{
				target.fieldOfView = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DONearClipPlane(this global::UnityEngine.Camera target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.nearClipPlane, delegate(float x)
			{
				target.nearClipPlane = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOOrthoSize(this global::UnityEngine.Camera target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.orthographicSize, delegate(float x)
			{
				target.orthographicSize = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> DOPixelRect(this global::UnityEngine.Camera target, global::UnityEngine.Rect endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.pixelRect, delegate(global::UnityEngine.Rect x)
			{
				target.pixelRect = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> DORect(this global::UnityEngine.Camera target, global::UnityEngine.Rect endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Rect, global::UnityEngine.Rect, global::DG.Tweening.Plugins.Options.RectOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.rect, delegate(global::UnityEngine.Rect x)
			{
				target.rect = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener DOShakePosition(this global::UnityEngine.Camera target, float duration, float strength = 3f, int vibrato = 10, float randomness = 90f, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOShakePosition: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.transform.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.transform.localPosition = x;
			}, duration, strength, vibrato, randomness, ignoreZAxis: true, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetCameraShakePosition);
		}

		public static global::DG.Tweening.Tweener DOShakePosition(this global::UnityEngine.Camera target, float duration, global::UnityEngine.Vector3 strength, int vibrato = 10, float randomness = 90f, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOShakePosition: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.transform.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.transform.localPosition = x;
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetCameraShakePosition);
		}

		public static global::DG.Tweening.Tweener DOShakeRotation(this global::UnityEngine.Camera target, float duration, float strength = 90f, int vibrato = 10, float randomness = 90f, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOShakeRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.transform.localEulerAngles, delegate(global::UnityEngine.Vector3 x)
			{
				target.transform.localRotation = global::UnityEngine.Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness, ignoreZAxis: false, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake);
		}

		public static global::DG.Tweening.Tweener DOShakeRotation(this global::UnityEngine.Camera target, float duration, global::UnityEngine.Vector3 strength, int vibrato = 10, float randomness = 90f, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOShakeRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.transform.localEulerAngles, delegate(global::UnityEngine.Vector3 x)
			{
				target.transform.localRotation = global::UnityEngine.Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOColor(this global::UnityEngine.Light target, global::UnityEngine.Color endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOIntensity(this global::UnityEngine.Light target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.intensity, delegate(float x)
			{
				target.intensity = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOShadowStrength(this global::UnityEngine.Light target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.shadowStrength, delegate(float x)
			{
				target.shadowStrength = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener DOColor(this global::UnityEngine.LineRenderer target, global::DG.Tweening.Color2 startValue, global::DG.Tweening.Color2 endValue, float duration)
		{
			return global::DG.Tweening.DOTween.To(() => startValue, delegate(global::DG.Tweening.Color2 x)
			{
				target.startColor = x.ca;
				target.endColor = x.cb;
			}, endValue, duration).SetTarget(target);
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOColor(this global::UnityEngine.Material target, global::UnityEngine.Color endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOColor(this global::UnityEngine.Material target, global::UnityEngine.Color endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.GetColor(property), delegate(global::UnityEngine.Color x)
			{
				target.SetColor(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOColor(this global::UnityEngine.Material target, global::UnityEngine.Color endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.GetColor(propertyID), delegate(global::UnityEngine.Color x)
			{
				target.SetColor(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOFade(this global::UnityEngine.Material target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.ToAlpha(() => target.color, delegate(global::UnityEngine.Color x)
			{
				target.color = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOFade(this global::UnityEngine.Material target, float endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.ToAlpha(() => target.GetColor(property), delegate(global::UnityEngine.Color x)
			{
				target.SetColor(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> DOFade(this global::UnityEngine.Material target, float endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Color, global::UnityEngine.Color, global::DG.Tweening.Plugins.Options.ColorOptions> tweenerCore = global::DG.Tweening.DOTween.ToAlpha(() => target.GetColor(propertyID), delegate(global::UnityEngine.Color x)
			{
				target.SetColor(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOFloat(this global::UnityEngine.Material target, float endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.GetFloat(property), delegate(float x)
			{
				target.SetFloat(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOFloat(this global::UnityEngine.Material target, float endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.GetFloat(propertyID), delegate(float x)
			{
				target.SetFloat(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOOffset(this global::UnityEngine.Material target, global::UnityEngine.Vector2 endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.mainTextureOffset, delegate(global::UnityEngine.Vector2 x)
			{
				target.mainTextureOffset = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOOffset(this global::UnityEngine.Material target, global::UnityEngine.Vector2 endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.GetTextureOffset(property), delegate(global::UnityEngine.Vector2 x)
			{
				target.SetTextureOffset(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOTiling(this global::UnityEngine.Material target, global::UnityEngine.Vector2 endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.mainTextureScale, delegate(global::UnityEngine.Vector2 x)
			{
				target.mainTextureScale = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> DOTiling(this global::UnityEngine.Material target, global::UnityEngine.Vector2 endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector2, global::UnityEngine.Vector2, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.GetTextureScale(property), delegate(global::UnityEngine.Vector2 x)
			{
				target.SetTextureScale(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::DG.Tweening.Plugins.Options.VectorOptions> DOVector(this global::UnityEngine.Material target, global::UnityEngine.Vector4 endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.GetVector(property), delegate(global::UnityEngine.Vector4 x)
			{
				target.SetVector(property, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::DG.Tweening.Plugins.Options.VectorOptions> DOVector(this global::UnityEngine.Material target, global::UnityEngine.Vector4 endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.GetVector(propertyID), delegate(global::UnityEngine.Vector4 x)
			{
				target.SetVector(propertyID, x);
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener DOResize(this global::UnityEngine.TrailRenderer target, float toStartWidth, float toEndWidth, float duration)
		{
			return global::DG.Tweening.DOTween.To(() => new global::UnityEngine.Vector2(target.startWidth, target.endWidth), delegate(global::UnityEngine.Vector2 x)
			{
				target.startWidth = x.x;
				target.endWidth = x.y;
			}, new global::UnityEngine.Vector2(toStartWidth, toEndWidth), duration).SetTarget(target);
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOTime(this global::UnityEngine.TrailRenderer target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.time, delegate(float x)
			{
				target.time = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOMove(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.position = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOMoveX(this global::UnityEngine.Transform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.position = x;
			}, new global::UnityEngine.Vector3(endValue, 0f, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.X, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOMoveY(this global::UnityEngine.Transform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.position = x;
			}, new global::UnityEngine.Vector3(0f, endValue, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Y, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOMoveZ(this global::UnityEngine.Transform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.position = x;
			}, new global::UnityEngine.Vector3(0f, 0f, endValue), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Z, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOLocalMove(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, endValue, duration);
			tweenerCore.SetOptions(snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOLocalMoveX(this global::UnityEngine.Transform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, new global::UnityEngine.Vector3(endValue, 0f, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.X, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOLocalMoveY(this global::UnityEngine.Transform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, new global::UnityEngine.Vector3(0f, endValue, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Y, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOLocalMoveZ(this global::UnityEngine.Transform target, float endValue, float duration, bool snapping = false)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, new global::UnityEngine.Vector3(0f, 0f, endValue), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Z, snapping).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> DORotate(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 endValue, float duration, global::DG.Tweening.RotateMode mode = global::DG.Tweening.RotateMode.Fast)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.rotation, delegate(global::UnityEngine.Quaternion x)
			{
				target.rotation = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions> DORotateQuaternion(this global::UnityEngine.Transform target, global::UnityEngine.Quaternion endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.CustomPlugins.PureQuaternionPlugin.Plug(), () => target.rotation, delegate(global::UnityEngine.Quaternion x)
			{
				target.rotation = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> DOLocalRotate(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 endValue, float duration, global::DG.Tweening.RotateMode mode = global::DG.Tweening.RotateMode.Fast)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.localRotation, delegate(global::UnityEngine.Quaternion x)
			{
				target.localRotation = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions> DOLocalRotateQuaternion(this global::UnityEngine.Transform target, global::UnityEngine.Quaternion endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Quaternion, global::DG.Tweening.Plugins.Options.NoOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.CustomPlugins.PureQuaternionPlugin.Plug(), () => target.localRotation, delegate(global::UnityEngine.Quaternion x)
			{
				target.localRotation = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOScale(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.localScale, delegate(global::UnityEngine.Vector3 x)
			{
				target.localScale = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOScale(this global::UnityEngine.Transform target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> obj = global::DG.Tweening.DOTween.To(endValue: new global::UnityEngine.Vector3(endValue, endValue, endValue), getter: () => target.localScale, setter: delegate(global::UnityEngine.Vector3 x)
			{
				target.localScale = x;
			}, duration: duration);
			obj.SetTarget(target);
			return obj;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOScaleX(this global::UnityEngine.Transform target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.localScale, delegate(global::UnityEngine.Vector3 x)
			{
				target.localScale = x;
			}, new global::UnityEngine.Vector3(endValue, 0f, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.X).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOScaleY(this global::UnityEngine.Transform target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.localScale, delegate(global::UnityEngine.Vector3 x)
			{
				target.localScale = x;
			}, new global::UnityEngine.Vector3(0f, endValue, 0f), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Y).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> DOScaleZ(this global::UnityEngine.Transform target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.localScale, delegate(global::UnityEngine.Vector3 x)
			{
				target.localScale = x;
			}, new global::UnityEngine.Vector3(0f, 0f, endValue), duration);
			tweenerCore.SetOptions(global::DG.Tweening.AxisConstraint.Z).SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener DOLookAt(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 towards, float duration, global::DG.Tweening.AxisConstraint axisConstraint = global::DG.Tweening.AxisConstraint.None, global::UnityEngine.Vector3? up = null)
		{
			return target.LookAt(towards, duration, axisConstraint, up, dynamic: false);
		}

		public static global::DG.Tweening.Tweener DODynamicLookAt(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 towards, float duration, global::DG.Tweening.AxisConstraint axisConstraint = global::DG.Tweening.AxisConstraint.None, global::UnityEngine.Vector3? up = null)
		{
			return target.LookAt(towards, duration, axisConstraint, up, dynamic: true);
		}

		private static global::DG.Tweening.Tweener LookAt(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 towards, float duration, global::DG.Tweening.AxisConstraint axisConstraint, global::UnityEngine.Vector3? up, bool dynamic)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> tweenerCore = global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.To(() => target.rotation, delegate(global::UnityEngine.Quaternion x)
			{
				target.rotation = x;
			}, towards, duration).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetLookAt);
			tweenerCore.plugOptions.axisConstraint = axisConstraint;
			tweenerCore.plugOptions.up = ((!up.HasValue) ? global::UnityEngine.Vector3.up : up.Value);
			if (dynamic)
			{
				tweenerCore.plugOptions.dynamicLookAt = true;
				tweenerCore.plugOptions.dynamicLookAtWorldPosition = towards;
			}
			else
			{
				tweenerCore.plugOptions.dynamicLookAt = false;
			}
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener DOPunchPosition(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 punch, float duration, int vibrato = 10, float elasticity = 1f, bool snapping = false)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOPunchPosition: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.DOTween.Punch(() => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, punch, duration, vibrato, elasticity).SetTarget(target).SetOptions(snapping);
		}

		public static global::DG.Tweening.Tweener DOPunchScale(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 punch, float duration, int vibrato = 10, float elasticity = 1f)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOPunchScale: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.DOTween.Punch(() => target.localScale, delegate(global::UnityEngine.Vector3 x)
			{
				target.localScale = x;
			}, punch, duration, vibrato, elasticity).SetTarget(target);
		}

		public static global::DG.Tweening.Tweener DOPunchRotation(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 punch, float duration, int vibrato = 10, float elasticity = 1f)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOPunchRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.DOTween.Punch(() => target.localEulerAngles, delegate(global::UnityEngine.Vector3 x)
			{
				target.localRotation = global::UnityEngine.Quaternion.Euler(x);
			}, punch, duration, vibrato, elasticity).SetTarget(target);
		}

		public static global::DG.Tweening.Tweener DOShakePosition(this global::UnityEngine.Transform target, float duration, float strength = 1f, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOShakePosition: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, duration, strength, vibrato, randomness, ignoreZAxis: false, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake).SetOptions(snapping);
		}

		public static global::DG.Tweening.Tweener DOShakePosition(this global::UnityEngine.Transform target, float duration, global::UnityEngine.Vector3 strength, int vibrato = 10, float randomness = 90f, bool snapping = false, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOShakePosition: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake).SetOptions(snapping);
		}

		public static global::DG.Tweening.Tweener DOShakeRotation(this global::UnityEngine.Transform target, float duration, float strength = 90f, int vibrato = 10, float randomness = 90f, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOShakeRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.localEulerAngles, delegate(global::UnityEngine.Vector3 x)
			{
				target.localRotation = global::UnityEngine.Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness, ignoreZAxis: false, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake);
		}

		public static global::DG.Tweening.Tweener DOShakeRotation(this global::UnityEngine.Transform target, float duration, global::UnityEngine.Vector3 strength, int vibrato = 10, float randomness = 90f, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOShakeRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.localEulerAngles, delegate(global::UnityEngine.Vector3 x)
			{
				target.localRotation = global::UnityEngine.Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake);
		}

		public static global::DG.Tweening.Tweener DOShakeScale(this global::UnityEngine.Transform target, float duration, float strength = 1f, int vibrato = 10, float randomness = 90f, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				global::UnityEngine.Debug.Log(global::DG.Tweening.Core.Debugger.logPriority);
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOShakeScale: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.localScale, delegate(global::UnityEngine.Vector3 x)
			{
				target.localScale = x;
			}, duration, strength, vibrato, randomness, ignoreZAxis: false, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake);
		}

		public static global::DG.Tweening.Tweener DOShakeScale(this global::UnityEngine.Transform target, float duration, global::UnityEngine.Vector3 strength, int vibrato = 10, float randomness = 90f, bool fadeOut = true, global::DG.Tweening.ShakeRandomnessMode randomnessMode = global::DG.Tweening.ShakeRandomnessMode.Full)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOShakeScale: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			return global::DG.Tweening.Core.Extensions.SetSpecialStartupMode(global::DG.Tweening.DOTween.Shake(() => target.localScale, delegate(global::UnityEngine.Vector3 x)
			{
				target.localScale = x;
			}, duration, strength, vibrato, randomness, fadeOut, randomnessMode).SetTarget(target), global::DG.Tweening.Core.Enums.SpecialStartupMode.SetShake);
		}

		public static global::DG.Tweening.Sequence DOJump(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = target.position.y;
			float offsetY = -1f;
			bool offsetYSet = false;
			global::DG.Tweening.Sequence s = global::DG.Tweening.DOTween.Sequence();
			global::DG.Tweening.Tween yTween = global::DG.Tweening.DOTween.To(() => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.position = x;
			}, new global::UnityEngine.Vector3(0f, jumpPower, 0f), duration / (float)(numJumps * 2)).SetOptions(global::DG.Tweening.AxisConstraint.Y, snapping).SetEase(global::DG.Tweening.Ease.OutQuad)
				.SetRelative()
				.SetLoops(numJumps * 2, global::DG.Tweening.LoopType.Yoyo)
				.OnStart(delegate
				{
					startPosY = target.position.y;
				});
			s.Append(global::DG.Tweening.DOTween.To(() => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.position = x;
			}, new global::UnityEngine.Vector3(endValue.x, 0f, 0f), duration).SetOptions(global::DG.Tweening.AxisConstraint.X, snapping).SetEase(global::DG.Tweening.Ease.Linear)).Join(global::DG.Tweening.DOTween.To(() => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.position = x;
			}, new global::UnityEngine.Vector3(0f, 0f, endValue.z), duration).SetOptions(global::DG.Tweening.AxisConstraint.Z, snapping).SetEase(global::DG.Tweening.Ease.Linear)).Join(yTween)
				.SetTarget(target)
				.SetEase(global::DG.Tweening.DOTween.defaultEaseType);
			yTween.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
				}
				global::UnityEngine.Vector3 position = target.position;
				position.y += global::DG.Tweening.DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(), global::DG.Tweening.Ease.OutQuad);
				target.position = position;
			});
			return s;
		}

		public static global::DG.Tweening.Sequence DOLocalJump(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = target.localPosition.y;
			float offsetY = -1f;
			bool offsetYSet = false;
			global::DG.Tweening.Sequence s = global::DG.Tweening.DOTween.Sequence();
			global::DG.Tweening.Tween yTween = global::DG.Tweening.DOTween.To(() => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, new global::UnityEngine.Vector3(0f, jumpPower, 0f), duration / (float)(numJumps * 2)).SetOptions(global::DG.Tweening.AxisConstraint.Y, snapping).SetEase(global::DG.Tweening.Ease.OutQuad)
				.SetRelative()
				.SetLoops(numJumps * 2, global::DG.Tweening.LoopType.Yoyo)
				.OnStart(delegate
				{
					startPosY = target.localPosition.y;
				});
			s.Append(global::DG.Tweening.DOTween.To(() => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, new global::UnityEngine.Vector3(endValue.x, 0f, 0f), duration).SetOptions(global::DG.Tweening.AxisConstraint.X, snapping).SetEase(global::DG.Tweening.Ease.Linear)).Join(global::DG.Tweening.DOTween.To(() => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, new global::UnityEngine.Vector3(0f, 0f, endValue.z), duration).SetOptions(global::DG.Tweening.AxisConstraint.Z, snapping).SetEase(global::DG.Tweening.Ease.Linear)).Join(yTween)
				.SetTarget(target)
				.SetEase(global::DG.Tweening.DOTween.defaultEaseType);
			yTween.OnUpdate(delegate
			{
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
				}
				global::UnityEngine.Vector3 localPosition = target.localPosition;
				localPosition.y += global::DG.Tweening.DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(), global::DG.Tweening.Ease.OutQuad);
				target.localPosition = localPosition;
			});
			return s;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> DOPath(this global::UnityEngine.Transform target, global::UnityEngine.Vector3[] path, float duration, global::DG.Tweening.PathType pathType = global::DG.Tweening.PathType.Linear, global::DG.Tweening.PathMode pathMode = global::DG.Tweening.PathMode.Full3D, int resolution = 10, global::UnityEngine.Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.Plugins.PathPlugin.Get(), () => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.position = x;
			}, new global::DG.Tweening.Plugins.Core.PathCore.Path(pathType, path, resolution, gizmoColor), duration).SetTarget(target);
			tweenerCore.plugOptions.mode = pathMode;
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> DOLocalPath(this global::UnityEngine.Transform target, global::UnityEngine.Vector3[] path, float duration, global::DG.Tweening.PathType pathType = global::DG.Tweening.PathType.Linear, global::DG.Tweening.PathMode pathMode = global::DG.Tweening.PathMode.Full3D, int resolution = 10, global::UnityEngine.Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.Plugins.PathPlugin.Get(), () => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, new global::DG.Tweening.Plugins.Core.PathCore.Path(pathType, path, resolution, gizmoColor), duration).SetTarget(target);
			tweenerCore.plugOptions.mode = pathMode;
			tweenerCore.plugOptions.useLocalPosition = true;
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> DOPath(this global::UnityEngine.Transform target, global::DG.Tweening.Plugins.Core.PathCore.Path path, float duration, global::DG.Tweening.PathMode pathMode = global::DG.Tweening.PathMode.Full3D)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.Plugins.PathPlugin.Get(), () => target.position, delegate(global::UnityEngine.Vector3 x)
			{
				target.position = x;
			}, path, duration).SetTarget(target);
			tweenerCore.plugOptions.mode = pathMode;
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> DOLocalPath(this global::UnityEngine.Transform target, global::DG.Tweening.Plugins.Core.PathCore.Path path, float duration, global::DG.Tweening.PathMode pathMode = global::DG.Tweening.PathMode.Full3D)
		{
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Core.PathCore.Path, global::DG.Tweening.Plugins.Options.PathOptions> tweenerCore = global::DG.Tweening.DOTween.To(global::DG.Tweening.Plugins.PathPlugin.Get(), () => target.localPosition, delegate(global::UnityEngine.Vector3 x)
			{
				target.localPosition = x;
			}, path, duration).SetTarget(target);
			tweenerCore.plugOptions.mode = pathMode;
			tweenerCore.plugOptions.useLocalPosition = true;
			return tweenerCore;
		}

		public static global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> DOTimeScale(this global::DG.Tweening.Tween target, float endValue, float duration)
		{
			global::DG.Tweening.Core.TweenerCore<float, float, global::DG.Tweening.Plugins.Options.FloatOptions> tweenerCore = global::DG.Tweening.DOTween.To(() => target.timeScale, delegate(float x)
			{
				target.timeScale = x;
			}, endValue, duration);
			tweenerCore.SetTarget(target);
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener DOBlendableColor(this global::UnityEngine.Light target, global::UnityEngine.Color endValue, float duration)
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

		public static global::DG.Tweening.Tweener DOBlendableColor(this global::UnityEngine.Material target, global::UnityEngine.Color endValue, float duration)
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

		public static global::DG.Tweening.Tweener DOBlendableColor(this global::UnityEngine.Material target, global::UnityEngine.Color endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			endValue -= target.GetColor(property);
			global::UnityEngine.Color to = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
			return global::DG.Tweening.Core.Extensions.Blendable(global::DG.Tweening.DOTween.To(() => to, delegate(global::UnityEngine.Color x)
			{
				global::UnityEngine.Color color = x - to;
				to = x;
				target.SetColor(property, target.GetColor(property) + color);
			}, endValue, duration)).SetTarget(target);
		}

		public static global::DG.Tweening.Tweener DOBlendableColor(this global::UnityEngine.Material target, global::UnityEngine.Color endValue, int propertyID, float duration)
		{
			if (!target.HasProperty(propertyID))
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::DG.Tweening.Core.Debugger.LogMissingMaterialProperty(propertyID);
				}
				return null;
			}
			endValue -= target.GetColor(propertyID);
			global::UnityEngine.Color to = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
			return global::DG.Tweening.Core.Extensions.Blendable(global::DG.Tweening.DOTween.To(() => to, delegate(global::UnityEngine.Color x)
			{
				global::UnityEngine.Color color = x - to;
				to = x;
				target.SetColor(propertyID, target.GetColor(propertyID) + color);
			}, endValue, duration)).SetTarget(target);
		}

		public static global::DG.Tweening.Tweener DOBlendableMoveBy(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 byValue, float duration, bool snapping = false)
		{
			global::UnityEngine.Vector3 to = global::UnityEngine.Vector3.zero;
			return global::DG.Tweening.Core.Extensions.Blendable(global::DG.Tweening.DOTween.To(() => to, delegate(global::UnityEngine.Vector3 x)
			{
				global::UnityEngine.Vector3 vector = x - to;
				to = x;
				target.position += vector;
			}, byValue, duration)).SetOptions(snapping).SetTarget(target);
		}

		public static global::DG.Tweening.Tweener DOBlendableLocalMoveBy(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 byValue, float duration, bool snapping = false)
		{
			global::UnityEngine.Vector3 to = global::UnityEngine.Vector3.zero;
			return global::DG.Tweening.Core.Extensions.Blendable(global::DG.Tweening.DOTween.To(() => to, delegate(global::UnityEngine.Vector3 x)
			{
				global::UnityEngine.Vector3 vector = x - to;
				to = x;
				target.localPosition += vector;
			}, byValue, duration)).SetOptions(snapping).SetTarget(target);
		}

		public static global::DG.Tweening.Tweener DOBlendableRotateBy(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 byValue, float duration, global::DG.Tweening.RotateMode mode = global::DG.Tweening.RotateMode.Fast)
		{
			global::UnityEngine.Quaternion to = global::UnityEngine.Quaternion.identity;
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> tweenerCore = global::DG.Tweening.Core.Extensions.Blendable(global::DG.Tweening.DOTween.To(() => to, delegate(global::UnityEngine.Quaternion x)
			{
				global::UnityEngine.Quaternion quaternion = x * global::UnityEngine.Quaternion.Inverse(to);
				to = x;
				global::UnityEngine.Quaternion rotation = target.rotation;
				target.rotation = rotation * global::UnityEngine.Quaternion.Inverse(rotation) * quaternion * rotation;
			}, byValue, duration)).SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener DOBlendableLocalRotateBy(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 byValue, float duration, global::DG.Tweening.RotateMode mode = global::DG.Tweening.RotateMode.Fast)
		{
			global::UnityEngine.Quaternion to = global::UnityEngine.Quaternion.identity;
			global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Quaternion, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.QuaternionOptions> tweenerCore = global::DG.Tweening.Core.Extensions.Blendable(global::DG.Tweening.DOTween.To(() => to, delegate(global::UnityEngine.Quaternion x)
			{
				global::UnityEngine.Quaternion quaternion = x * global::UnityEngine.Quaternion.Inverse(to);
				to = x;
				global::UnityEngine.Quaternion localRotation = target.localRotation;
				target.localRotation = localRotation * global::UnityEngine.Quaternion.Inverse(localRotation) * quaternion * localRotation;
			}, byValue, duration)).SetTarget(target);
			tweenerCore.plugOptions.rotateMode = mode;
			return tweenerCore;
		}

		public static global::DG.Tweening.Tweener DOBlendablePunchRotation(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 punch, float duration, int vibrato = 10, float elasticity = 1f)
		{
			if (duration <= 0f)
			{
				if (global::DG.Tweening.Core.Debugger.logPriority > 0)
				{
					global::UnityEngine.Debug.LogWarning("DOBlendablePunchRotation: duration can't be 0, returning NULL without creating a tween");
				}
				return null;
			}
			global::UnityEngine.Vector3 to = global::UnityEngine.Vector3.zero;
			return global::DG.Tweening.Core.Extensions.Blendable(global::DG.Tweening.DOTween.Punch(() => to, delegate(global::UnityEngine.Vector3 v)
			{
				global::UnityEngine.Quaternion rotation = global::UnityEngine.Quaternion.Euler(to.x, to.y, to.z);
				global::UnityEngine.Quaternion quaternion = global::UnityEngine.Quaternion.Euler(v.x, v.y, v.z) * global::UnityEngine.Quaternion.Inverse(rotation);
				to = v;
				global::UnityEngine.Quaternion rotation2 = target.rotation;
				target.rotation = rotation2 * global::UnityEngine.Quaternion.Inverse(rotation2) * quaternion * rotation2;
			}, punch, duration, vibrato, elasticity)).SetTarget(target);
		}

		public static global::DG.Tweening.Tweener DOBlendableScaleBy(this global::UnityEngine.Transform target, global::UnityEngine.Vector3 byValue, float duration)
		{
			global::UnityEngine.Vector3 to = global::UnityEngine.Vector3.zero;
			return global::DG.Tweening.Core.Extensions.Blendable(global::DG.Tweening.DOTween.To(() => to, delegate(global::UnityEngine.Vector3 x)
			{
				global::UnityEngine.Vector3 vector = x - to;
				to = x;
				target.localScale += vector;
			}, byValue, duration)).SetTarget(target);
		}

		public static int DOComplete(this global::UnityEngine.Component target, bool withCallbacks = false)
		{
			return global::DG.Tweening.DOTween.Complete(target, withCallbacks);
		}

		public static int DOComplete(this global::UnityEngine.Material target, bool withCallbacks = false)
		{
			return global::DG.Tweening.DOTween.Complete(target, withCallbacks);
		}

		public static int DOKill(this global::UnityEngine.Component target, bool complete = false)
		{
			return global::DG.Tweening.DOTween.Kill(target, complete);
		}

		public static int DOKill(this global::UnityEngine.Material target, bool complete = false)
		{
			return global::DG.Tweening.DOTween.Kill(target, complete);
		}

		public static int DOFlip(this global::UnityEngine.Component target)
		{
			return global::DG.Tweening.DOTween.Flip(target);
		}

		public static int DOFlip(this global::UnityEngine.Material target)
		{
			return global::DG.Tweening.DOTween.Flip(target);
		}

		public static int DOGoto(this global::UnityEngine.Component target, float to, bool andPlay = false)
		{
			return global::DG.Tweening.DOTween.Goto(target, to, andPlay);
		}

		public static int DOGoto(this global::UnityEngine.Material target, float to, bool andPlay = false)
		{
			return global::DG.Tweening.DOTween.Goto(target, to, andPlay);
		}

		public static int DOPause(this global::UnityEngine.Component target)
		{
			return global::DG.Tweening.DOTween.Pause(target);
		}

		public static int DOPause(this global::UnityEngine.Material target)
		{
			return global::DG.Tweening.DOTween.Pause(target);
		}

		public static int DOPlay(this global::UnityEngine.Component target)
		{
			return global::DG.Tweening.DOTween.Play(target);
		}

		public static int DOPlay(this global::UnityEngine.Material target)
		{
			return global::DG.Tweening.DOTween.Play(target);
		}

		public static int DOPlayBackwards(this global::UnityEngine.Component target)
		{
			return global::DG.Tweening.DOTween.PlayBackwards(target);
		}

		public static int DOPlayBackwards(this global::UnityEngine.Material target)
		{
			return global::DG.Tweening.DOTween.PlayBackwards(target);
		}

		public static int DOPlayForward(this global::UnityEngine.Component target)
		{
			return global::DG.Tweening.DOTween.PlayForward(target);
		}

		public static int DOPlayForward(this global::UnityEngine.Material target)
		{
			return global::DG.Tweening.DOTween.PlayForward(target);
		}

		public static int DORestart(this global::UnityEngine.Component target, bool includeDelay = true)
		{
			return global::DG.Tweening.DOTween.Restart(target, includeDelay);
		}

		public static int DORestart(this global::UnityEngine.Material target, bool includeDelay = true)
		{
			return global::DG.Tweening.DOTween.Restart(target, includeDelay);
		}

		public static int DORewind(this global::UnityEngine.Component target, bool includeDelay = true)
		{
			return global::DG.Tweening.DOTween.Rewind(target, includeDelay);
		}

		public static int DORewind(this global::UnityEngine.Material target, bool includeDelay = true)
		{
			return global::DG.Tweening.DOTween.Rewind(target, includeDelay);
		}

		public static int DOSmoothRewind(this global::UnityEngine.Component target)
		{
			return global::DG.Tweening.DOTween.SmoothRewind(target);
		}

		public static int DOSmoothRewind(this global::UnityEngine.Material target)
		{
			return global::DG.Tweening.DOTween.SmoothRewind(target);
		}

		public static int DOTogglePause(this global::UnityEngine.Component target)
		{
			return global::DG.Tweening.DOTween.TogglePause(target);
		}

		public static int DOTogglePause(this global::UnityEngine.Material target)
		{
			return global::DG.Tweening.DOTween.TogglePause(target);
		}
	}
}
