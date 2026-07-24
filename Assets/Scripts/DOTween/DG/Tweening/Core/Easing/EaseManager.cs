namespace DG.Tweening.Core.Easing
{
	public static class EaseManager
	{
		private const float _PiOver2 = (float)global::System.Math.PI / 2f;

		private const float _TwoPi = (float)global::System.Math.PI * 2f;

		public static float Evaluate(global::DG.Tweening.Tween t, float time, float duration, float overshootOrAmplitude, float period)
		{
			return Evaluate(t.easeType, t.customEase, time, duration, overshootOrAmplitude, period);
		}

		public static float Evaluate(global::DG.Tweening.Ease easeType, global::DG.Tweening.EaseFunction customEase, float time, float duration, float overshootOrAmplitude, float period)
		{
			switch (easeType)
			{
			case global::DG.Tweening.Ease.Linear:
				return time / duration;
			case global::DG.Tweening.Ease.InSine:
				return 0f - (float)global::System.Math.Cos(time / duration * ((float)global::System.Math.PI / 2f)) + 1f;
			case global::DG.Tweening.Ease.OutSine:
				return (float)global::System.Math.Sin(time / duration * ((float)global::System.Math.PI / 2f));
			case global::DG.Tweening.Ease.InOutSine:
				return -0.5f * ((float)global::System.Math.Cos((float)global::System.Math.PI * time / duration) - 1f);
			case global::DG.Tweening.Ease.InQuad:
				return (time /= duration) * time;
			case global::DG.Tweening.Ease.OutQuad:
				return (0f - (time /= duration)) * (time - 2f);
			case global::DG.Tweening.Ease.InOutQuad:
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * time * time;
				}
				return -0.5f * ((time -= 1f) * (time - 2f) - 1f);
			case global::DG.Tweening.Ease.InCubic:
				return (time /= duration) * time * time;
			case global::DG.Tweening.Ease.OutCubic:
				return (time = time / duration - 1f) * time * time + 1f;
			case global::DG.Tweening.Ease.InOutCubic:
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * time * time * time;
				}
				return 0.5f * ((time -= 2f) * time * time + 2f);
			case global::DG.Tweening.Ease.InQuart:
				return (time /= duration) * time * time * time;
			case global::DG.Tweening.Ease.OutQuart:
				return 0f - ((time = time / duration - 1f) * time * time * time - 1f);
			case global::DG.Tweening.Ease.InOutQuart:
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * time * time * time * time;
				}
				return -0.5f * ((time -= 2f) * time * time * time - 2f);
			case global::DG.Tweening.Ease.InQuint:
				return (time /= duration) * time * time * time * time;
			case global::DG.Tweening.Ease.OutQuint:
				return (time = time / duration - 1f) * time * time * time * time + 1f;
			case global::DG.Tweening.Ease.InOutQuint:
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * time * time * time * time * time;
				}
				return 0.5f * ((time -= 2f) * time * time * time * time + 2f);
			case global::DG.Tweening.Ease.InExpo:
				if (time != 0f)
				{
					return (float)global::System.Math.Pow(2.0, 10f * (time / duration - 1f));
				}
				return 0f;
			case global::DG.Tweening.Ease.OutExpo:
				if (time == duration)
				{
					return 1f;
				}
				return 0f - (float)global::System.Math.Pow(2.0, -10f * time / duration) + 1f;
			case global::DG.Tweening.Ease.InOutExpo:
				if (time == 0f)
				{
					return 0f;
				}
				if (time == duration)
				{
					return 1f;
				}
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * (float)global::System.Math.Pow(2.0, 10f * (time - 1f));
				}
				return 0.5f * (0f - (float)global::System.Math.Pow(2.0, -10f * (time -= 1f)) + 2f);
			case global::DG.Tweening.Ease.InCirc:
				return 0f - ((float)global::System.Math.Sqrt(1f - (time /= duration) * time) - 1f);
			case global::DG.Tweening.Ease.OutCirc:
				return (float)global::System.Math.Sqrt(1f - (time = time / duration - 1f) * time);
			case global::DG.Tweening.Ease.InOutCirc:
				if ((time /= duration * 0.5f) < 1f)
				{
					return -0.5f * ((float)global::System.Math.Sqrt(1f - time * time) - 1f);
				}
				return 0.5f * ((float)global::System.Math.Sqrt(1f - (time -= 2f) * time) + 1f);
			case global::DG.Tweening.Ease.InElastic:
			{
				if (time == 0f)
				{
					return 0f;
				}
				if ((time /= duration) == 1f)
				{
					return 1f;
				}
				if (period == 0f)
				{
					period = duration * 0.3f;
				}
				float num3;
				if (overshootOrAmplitude < 1f)
				{
					overshootOrAmplitude = 1f;
					num3 = period / 4f;
				}
				else
				{
					num3 = period / ((float)global::System.Math.PI * 2f) * (float)global::System.Math.Asin(1f / overshootOrAmplitude);
				}
				return 0f - overshootOrAmplitude * (float)global::System.Math.Pow(2.0, 10f * (time -= 1f)) * (float)global::System.Math.Sin((time * duration - num3) * ((float)global::System.Math.PI * 2f) / period);
			}
			case global::DG.Tweening.Ease.OutElastic:
			{
				if (time == 0f)
				{
					return 0f;
				}
				if ((time /= duration) == 1f)
				{
					return 1f;
				}
				if (period == 0f)
				{
					period = duration * 0.3f;
				}
				float num2;
				if (overshootOrAmplitude < 1f)
				{
					overshootOrAmplitude = 1f;
					num2 = period / 4f;
				}
				else
				{
					num2 = period / ((float)global::System.Math.PI * 2f) * (float)global::System.Math.Asin(1f / overshootOrAmplitude);
				}
				return overshootOrAmplitude * (float)global::System.Math.Pow(2.0, -10f * time) * (float)global::System.Math.Sin((time * duration - num2) * ((float)global::System.Math.PI * 2f) / period) + 1f;
			}
			case global::DG.Tweening.Ease.InOutElastic:
			{
				if (time == 0f)
				{
					return 0f;
				}
				if ((time /= duration * 0.5f) == 2f)
				{
					return 1f;
				}
				if (period == 0f)
				{
					period = duration * 0.45000002f;
				}
				float num;
				if (overshootOrAmplitude < 1f)
				{
					overshootOrAmplitude = 1f;
					num = period / 4f;
				}
				else
				{
					num = period / ((float)global::System.Math.PI * 2f) * (float)global::System.Math.Asin(1f / overshootOrAmplitude);
				}
				if (time < 1f)
				{
					return -0.5f * (overshootOrAmplitude * (float)global::System.Math.Pow(2.0, 10f * (time -= 1f)) * (float)global::System.Math.Sin((time * duration - num) * ((float)global::System.Math.PI * 2f) / period));
				}
				return overshootOrAmplitude * (float)global::System.Math.Pow(2.0, -10f * (time -= 1f)) * (float)global::System.Math.Sin((time * duration - num) * ((float)global::System.Math.PI * 2f) / period) * 0.5f + 1f;
			}
			case global::DG.Tweening.Ease.InBack:
				return (time /= duration) * time * ((overshootOrAmplitude + 1f) * time - overshootOrAmplitude);
			case global::DG.Tweening.Ease.OutBack:
				return (time = time / duration - 1f) * time * ((overshootOrAmplitude + 1f) * time + overshootOrAmplitude) + 1f;
			case global::DG.Tweening.Ease.InOutBack:
				if ((time /= duration * 0.5f) < 1f)
				{
					return 0.5f * (time * time * (((overshootOrAmplitude *= 1.525f) + 1f) * time - overshootOrAmplitude));
				}
				return 0.5f * ((time -= 2f) * time * (((overshootOrAmplitude *= 1.525f) + 1f) * time + overshootOrAmplitude) + 2f);
			case global::DG.Tweening.Ease.InBounce:
				return global::DG.Tweening.Core.Easing.Bounce.EaseIn(time, duration, overshootOrAmplitude, period);
			case global::DG.Tweening.Ease.OutBounce:
				return global::DG.Tweening.Core.Easing.Bounce.EaseOut(time, duration, overshootOrAmplitude, period);
			case global::DG.Tweening.Ease.InOutBounce:
				return global::DG.Tweening.Core.Easing.Bounce.EaseInOut(time, duration, overshootOrAmplitude, period);
			case global::DG.Tweening.Ease.INTERNAL_Custom:
				return customEase(time, duration, overshootOrAmplitude, period);
			case global::DG.Tweening.Ease.INTERNAL_Zero:
				return 1f;
			case global::DG.Tweening.Ease.Flash:
				return global::DG.Tweening.Core.Easing.Flash.Ease(time, duration, overshootOrAmplitude, period);
			case global::DG.Tweening.Ease.InFlash:
				return global::DG.Tweening.Core.Easing.Flash.EaseIn(time, duration, overshootOrAmplitude, period);
			case global::DG.Tweening.Ease.OutFlash:
				return global::DG.Tweening.Core.Easing.Flash.EaseOut(time, duration, overshootOrAmplitude, period);
			case global::DG.Tweening.Ease.InOutFlash:
				return global::DG.Tweening.Core.Easing.Flash.EaseInOut(time, duration, overshootOrAmplitude, period);
			default:
				return (0f - (time /= duration)) * (time - 2f);
			}
		}

		public static global::DG.Tweening.EaseFunction ToEaseFunction(global::DG.Tweening.Ease ease)
		{
			return ease switch
			{
				global::DG.Tweening.Ease.Linear => (float time, float duration, float overshootOrAmplitude, float period) => time / duration, 
				global::DG.Tweening.Ease.InSine => (float time, float duration, float overshootOrAmplitude, float period) => 0f - (float)global::System.Math.Cos(time / duration * ((float)global::System.Math.PI / 2f)) + 1f, 
				global::DG.Tweening.Ease.OutSine => (float time, float duration, float overshootOrAmplitude, float period) => (float)global::System.Math.Sin(time / duration * ((float)global::System.Math.PI / 2f)), 
				global::DG.Tweening.Ease.InOutSine => (float time, float duration, float overshootOrAmplitude, float period) => -0.5f * ((float)global::System.Math.Cos((float)global::System.Math.PI * time / duration) - 1f), 
				global::DG.Tweening.Ease.InQuad => (float time, float duration, float overshootOrAmplitude, float period) => (time /= duration) * time, 
				global::DG.Tweening.Ease.OutQuad => (float time, float duration, float overshootOrAmplitude, float period) => (0f - (time /= duration)) * (time - 2f), 
				global::DG.Tweening.Ease.InOutQuad => (float time, float duration, float overshootOrAmplitude, float period) => ((time /= duration * 0.5f) < 1f) ? (0.5f * time * time) : (-0.5f * ((time -= 1f) * (time - 2f) - 1f)), 
				global::DG.Tweening.Ease.InCubic => (float time, float duration, float overshootOrAmplitude, float period) => (time /= duration) * time * time, 
				global::DG.Tweening.Ease.OutCubic => (float time, float duration, float overshootOrAmplitude, float period) => (time = time / duration - 1f) * time * time + 1f, 
				global::DG.Tweening.Ease.InOutCubic => (float time, float duration, float overshootOrAmplitude, float period) => ((time /= duration * 0.5f) < 1f) ? (0.5f * time * time * time) : (0.5f * ((time -= 2f) * time * time + 2f)), 
				global::DG.Tweening.Ease.InQuart => (float time, float duration, float overshootOrAmplitude, float period) => (time /= duration) * time * time * time, 
				global::DG.Tweening.Ease.OutQuart => (float time, float duration, float overshootOrAmplitude, float period) => 0f - ((time = time / duration - 1f) * time * time * time - 1f), 
				global::DG.Tweening.Ease.InOutQuart => (float time, float duration, float overshootOrAmplitude, float period) => ((time /= duration * 0.5f) < 1f) ? (0.5f * time * time * time * time) : (-0.5f * ((time -= 2f) * time * time * time - 2f)), 
				global::DG.Tweening.Ease.InQuint => (float time, float duration, float overshootOrAmplitude, float period) => (time /= duration) * time * time * time * time, 
				global::DG.Tweening.Ease.OutQuint => (float time, float duration, float overshootOrAmplitude, float period) => (time = time / duration - 1f) * time * time * time * time + 1f, 
				global::DG.Tweening.Ease.InOutQuint => (float time, float duration, float overshootOrAmplitude, float period) => ((time /= duration * 0.5f) < 1f) ? (0.5f * time * time * time * time * time) : (0.5f * ((time -= 2f) * time * time * time * time + 2f)), 
				global::DG.Tweening.Ease.InExpo => (float time, float duration, float overshootOrAmplitude, float period) => (time != 0f) ? ((float)global::System.Math.Pow(2.0, 10f * (time / duration - 1f))) : 0f, 
				global::DG.Tweening.Ease.OutExpo => (float time, float duration, float overshootOrAmplitude, float period) => (time == duration) ? 1f : (0f - (float)global::System.Math.Pow(2.0, -10f * time / duration) + 1f), 
				global::DG.Tweening.Ease.InOutExpo => delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if (time == 0f)
					{
						return 0f;
					}
					if (time == duration)
					{
						return 1f;
					}
					return ((time /= duration * 0.5f) < 1f) ? (0.5f * (float)global::System.Math.Pow(2.0, 10f * (time - 1f))) : (0.5f * (0f - (float)global::System.Math.Pow(2.0, -10f * (time -= 1f)) + 2f));
				}, 
				global::DG.Tweening.Ease.InCirc => (float time, float duration, float overshootOrAmplitude, float period) => 0f - ((float)global::System.Math.Sqrt(1f - (time /= duration) * time) - 1f), 
				global::DG.Tweening.Ease.OutCirc => (float time, float duration, float overshootOrAmplitude, float period) => (float)global::System.Math.Sqrt(1f - (time = time / duration - 1f) * time), 
				global::DG.Tweening.Ease.InOutCirc => (float time, float duration, float overshootOrAmplitude, float period) => ((time /= duration * 0.5f) < 1f) ? (-0.5f * ((float)global::System.Math.Sqrt(1f - time * time) - 1f)) : (0.5f * ((float)global::System.Math.Sqrt(1f - (time -= 2f) * time) + 1f)), 
				global::DG.Tweening.Ease.InElastic => delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if (time == 0f)
					{
						return 0f;
					}
					if ((time /= duration) == 1f)
					{
						return 1f;
					}
					if (period == 0f)
					{
						period = duration * 0.3f;
					}
					float num;
					if (overshootOrAmplitude < 1f)
					{
						overshootOrAmplitude = 1f;
						num = period / 4f;
					}
					else
					{
						num = period / ((float)global::System.Math.PI * 2f) * (float)global::System.Math.Asin(1f / overshootOrAmplitude);
					}
					return 0f - overshootOrAmplitude * (float)global::System.Math.Pow(2.0, 10f * (time -= 1f)) * (float)global::System.Math.Sin((time * duration - num) * ((float)global::System.Math.PI * 2f) / period);
				}, 
				global::DG.Tweening.Ease.OutElastic => delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if (time == 0f)
					{
						return 0f;
					}
					if ((time /= duration) == 1f)
					{
						return 1f;
					}
					if (period == 0f)
					{
						period = duration * 0.3f;
					}
					float num;
					if (overshootOrAmplitude < 1f)
					{
						overshootOrAmplitude = 1f;
						num = period / 4f;
					}
					else
					{
						num = period / ((float)global::System.Math.PI * 2f) * (float)global::System.Math.Asin(1f / overshootOrAmplitude);
					}
					return overshootOrAmplitude * (float)global::System.Math.Pow(2.0, -10f * time) * (float)global::System.Math.Sin((time * duration - num) * ((float)global::System.Math.PI * 2f) / period) + 1f;
				}, 
				global::DG.Tweening.Ease.InOutElastic => delegate(float time, float duration, float overshootOrAmplitude, float period)
				{
					if (time == 0f)
					{
						return 0f;
					}
					if ((time /= duration * 0.5f) == 2f)
					{
						return 1f;
					}
					if (period == 0f)
					{
						period = duration * 0.45000002f;
					}
					float num;
					if (overshootOrAmplitude < 1f)
					{
						overshootOrAmplitude = 1f;
						num = period / 4f;
					}
					else
					{
						num = period / ((float)global::System.Math.PI * 2f) * (float)global::System.Math.Asin(1f / overshootOrAmplitude);
					}
					return (time < 1f) ? (-0.5f * (overshootOrAmplitude * (float)global::System.Math.Pow(2.0, 10f * (time -= 1f)) * (float)global::System.Math.Sin((time * duration - num) * ((float)global::System.Math.PI * 2f) / period))) : (overshootOrAmplitude * (float)global::System.Math.Pow(2.0, -10f * (time -= 1f)) * (float)global::System.Math.Sin((time * duration - num) * ((float)global::System.Math.PI * 2f) / period) * 0.5f + 1f);
				}, 
				global::DG.Tweening.Ease.InBack => (float time, float duration, float overshootOrAmplitude, float period) => (time /= duration) * time * ((overshootOrAmplitude + 1f) * time - overshootOrAmplitude), 
				global::DG.Tweening.Ease.OutBack => (float time, float duration, float overshootOrAmplitude, float period) => (time = time / duration - 1f) * time * ((overshootOrAmplitude + 1f) * time + overshootOrAmplitude) + 1f, 
				global::DG.Tweening.Ease.InOutBack => (float time, float duration, float overshootOrAmplitude, float period) => ((time /= duration * 0.5f) < 1f) ? (0.5f * (time * time * (((overshootOrAmplitude *= 1.525f) + 1f) * time - overshootOrAmplitude))) : (0.5f * ((time -= 2f) * time * (((overshootOrAmplitude *= 1.525f) + 1f) * time + overshootOrAmplitude) + 2f)), 
				global::DG.Tweening.Ease.InBounce => (float time, float duration, float overshootOrAmplitude, float period) => global::DG.Tweening.Core.Easing.Bounce.EaseIn(time, duration, overshootOrAmplitude, period), 
				global::DG.Tweening.Ease.OutBounce => (float time, float duration, float overshootOrAmplitude, float period) => global::DG.Tweening.Core.Easing.Bounce.EaseOut(time, duration, overshootOrAmplitude, period), 
				global::DG.Tweening.Ease.InOutBounce => (float time, float duration, float overshootOrAmplitude, float period) => global::DG.Tweening.Core.Easing.Bounce.EaseInOut(time, duration, overshootOrAmplitude, period), 
				global::DG.Tweening.Ease.Flash => (float time, float duration, float overshootOrAmplitude, float period) => global::DG.Tweening.Core.Easing.Flash.Ease(time, duration, overshootOrAmplitude, period), 
				global::DG.Tweening.Ease.InFlash => (float time, float duration, float overshootOrAmplitude, float period) => global::DG.Tweening.Core.Easing.Flash.EaseIn(time, duration, overshootOrAmplitude, period), 
				global::DG.Tweening.Ease.OutFlash => (float time, float duration, float overshootOrAmplitude, float period) => global::DG.Tweening.Core.Easing.Flash.EaseOut(time, duration, overshootOrAmplitude, period), 
				global::DG.Tweening.Ease.InOutFlash => (float time, float duration, float overshootOrAmplitude, float period) => global::DG.Tweening.Core.Easing.Flash.EaseInOut(time, duration, overshootOrAmplitude, period), 
				_ => (float time, float duration, float overshootOrAmplitude, float period) => (0f - (time /= duration)) * (time - 2f), 
			};
		}

		internal static bool IsFlashEase(global::DG.Tweening.Ease ease)
		{
			if ((uint)(ease - 32) <= 3u)
			{
				return true;
			}
			return false;
		}
	}
}
