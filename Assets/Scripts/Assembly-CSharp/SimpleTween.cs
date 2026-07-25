using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Minimal coroutine-free tween helper that replaces the handful of DOTween
/// features this project actually used. Tweens are ticked by a hidden runner
/// object, either on Update or FixedUpdate depending on the tween's mode.
/// </summary>
public class SimpleTween
{
	public enum Ease
	{
		/// <summary>DOTween's default ease, kept so timings look the same.</summary>
		OutQuad,
		Linear
	}

	public enum UpdateMode
	{
		Normal,
		Fixed
	}

	private static readonly List<SimpleTween> s_active = new List<SimpleTween>();
	private static readonly List<SimpleTween> s_ticking = new List<SimpleTween>();

	private float m_elapsed;
	private float m_duration;
	private Ease m_ease = Ease.OutQuad;
	private AnimationCurve m_easeCurve;
	private Action<float> m_apply;
	private Action m_onUpdate;
	private Action m_onComplete;
	private bool m_isActive;
	private UpdateMode m_updateMode = UpdateMode.Normal;
	private bool m_useUnscaledTime;
	private object m_target;

	public bool IsActive()
	{
		return m_isActive;
	}

	/// <summary>Normalized 0-1 progress, before easing. Mirrors DOTween's ElapsedPercentage.</summary>
	public float ElapsedPercentage()
	{
		if (m_duration <= 0f)
		{
			return 1f;
		}
		return Mathf.Clamp01(m_elapsed / m_duration);
	}

	public static SimpleTween To(Func<float> getter, Action<float> setter, float endValue, float duration)
	{
		float startValue = getter();
		return Create(null, duration, delegate(float t)
		{
			setter(Mathf.LerpUnclamped(startValue, endValue, t));
		});
	}

	internal static SimpleTween Create(object target, float duration, Action<float> apply)
	{
		SimpleTween tween = new SimpleTween
		{
			m_target = target,
			m_duration = duration,
			m_apply = apply,
			m_isActive = true
		};
		s_active.Add(tween);
		SimpleTweenRunner.EnsureExists();
		// A zero-length tween still needs one tick so its OnComplete can be
		// registered by the caller before it fires.
		return tween;
	}

	public SimpleTween OnUpdate(Action callback)
	{
		m_onUpdate = (Action)Delegate.Combine(m_onUpdate, callback);
		return this;
	}

	public SimpleTween OnComplete(Action callback)
	{
		m_onComplete = (Action)Delegate.Combine(m_onComplete, callback);
		return this;
	}

	public SimpleTween SetEase(Ease ease)
	{
		m_ease = ease;
		m_easeCurve = null;
		return this;
	}

	public SimpleTween SetEase(AnimationCurve curve)
	{
		m_easeCurve = curve;
		return this;
	}

	public SimpleTween SetUpdate(UpdateMode mode)
	{
		m_updateMode = mode;
		return this;
	}

	/// <summary>Independent update means the tween ignores Time.timeScale.</summary>
	public SimpleTween SetUpdate(bool isIndependentUpdate)
	{
		m_useUnscaledTime = isIndependentUpdate;
		return this;
	}

	/// <summary>Stops the tween where it is. OnComplete does not fire.</summary>
	public void Kill()
	{
		if (m_isActive)
		{
			m_isActive = false;
			s_active.Remove(this);
		}
	}

	/// <summary>Jumps to the end value and fires OnComplete.</summary>
	public void Complete()
	{
		if (!m_isActive)
		{
			return;
		}
		m_elapsed = m_duration;
		m_isActive = false;
		s_active.Remove(this);
		m_apply?.Invoke(Evaluate(1f));
		m_onUpdate?.Invoke();
		m_onComplete?.Invoke();
	}

	/// <summary>Kills every running tween that was started on the given object.</summary>
	public static void KillTarget(object target)
	{
		if (target == null)
		{
			return;
		}
		for (int i = s_active.Count - 1; i >= 0; i--)
		{
			if (s_active[i].m_target == target)
			{
				s_active[i].m_isActive = false;
				s_active.RemoveAt(i);
			}
		}
	}

	private float Evaluate(float t)
	{
		if (m_easeCurve != null)
		{
			return m_easeCurve.Evaluate(t);
		}
		if (m_ease == Ease.Linear)
		{
			return t;
		}
		// OutQuad
		return t * (2f - t);
	}

	internal static void Step(UpdateMode mode, float deltaTime, float unscaledDeltaTime)
	{
		// Snapshot first: callbacks are free to start, kill or complete tweens.
		s_ticking.Clear();
		s_ticking.AddRange(s_active);
		for (int i = 0; i < s_ticking.Count; i++)
		{
			SimpleTween tween = s_ticking[i];
			if (!tween.m_isActive || tween.m_updateMode != mode)
			{
				continue;
			}
			tween.Tick(tween.m_useUnscaledTime ? unscaledDeltaTime : deltaTime);
		}
		s_ticking.Clear();
	}

	private void Tick(float deltaTime)
	{
		m_elapsed += deltaTime;
		float t = ElapsedPercentage();
		m_apply?.Invoke(Evaluate(t));
		m_onUpdate?.Invoke();
		if (t >= 1f && m_isActive)
		{
			m_isActive = false;
			s_active.Remove(this);
			m_onComplete?.Invoke();
		}
	}
}

/// <summary>Hidden object that ticks the tween list. Created on demand.</summary>
public class SimpleTweenRunner : MonoBehaviour
{
	private static SimpleTweenRunner s_instance;

	internal static void EnsureExists()
	{
		if (s_instance != null)
		{
			return;
		}
		GameObject go = new GameObject("SimpleTweenRunner");
		go.hideFlags = HideFlags.HideAndDontSave;
		UnityEngine.Object.DontDestroyOnLoad(go);
		s_instance = go.AddComponent<SimpleTweenRunner>();
	}

	private void Update()
	{
		SimpleTween.Step(SimpleTween.UpdateMode.Normal, Time.deltaTime, Time.unscaledDeltaTime);
	}

	private void FixedUpdate()
	{
		SimpleTween.Step(SimpleTween.UpdateMode.Fixed, Time.fixedDeltaTime, Time.fixedUnscaledDeltaTime);
	}

	private void OnDestroy()
	{
		if (s_instance == this)
		{
			s_instance = null;
		}
	}
}

public static class SimpleTweenExtensions
{
	public static SimpleTween DOColor(this Graphic graphic, Color endColor, float duration)
	{
		Color startColor = graphic.color;
		return SimpleTween.Create(graphic, duration, delegate(float t)
		{
			graphic.color = Color.LerpUnclamped(startColor, endColor, t);
		});
	}

	public static SimpleTween DOFade(this Graphic graphic, float endAlpha, float duration)
	{
		Color startColor = graphic.color;
		return SimpleTween.Create(graphic, duration, delegate(float t)
		{
			Color c = graphic.color;
			c.a = Mathf.LerpUnclamped(startColor.a, endAlpha, t);
			graphic.color = c;
		});
	}

	public static SimpleTween DOColor(this SpriteRenderer renderer, Color endColor, float duration)
	{
		Color startColor = renderer.color;
		return SimpleTween.Create(renderer, duration, delegate(float t)
		{
			renderer.color = Color.LerpUnclamped(startColor, endColor, t);
		});
	}

	public static SimpleTween DOFade(this CanvasGroup canvasGroup, float endAlpha, float duration)
	{
		float startAlpha = canvasGroup.alpha;
		return SimpleTween.Create(canvasGroup, duration, delegate(float t)
		{
			canvasGroup.alpha = Mathf.LerpUnclamped(startAlpha, endAlpha, t);
		});
	}

	public static SimpleTween DOFade(this AudioSource audioSource, float endVolume, float duration)
	{
		float startVolume = audioSource.volume;
		return SimpleTween.Create(audioSource, duration, delegate(float t)
		{
			audioSource.volume = Mathf.LerpUnclamped(startVolume, endVolume, t);
		});
	}

	public static SimpleTween DOMove(this Transform transform, Vector3 endValue, float duration)
	{
		Vector3 startPos = transform.position;
		return SimpleTween.Create(transform, duration, delegate(float t)
		{
			transform.position = Vector3.LerpUnclamped(startPos, endValue, t);
		});
	}

	public static SimpleTween DOLocalMoveX(this Transform transform, float endX, float duration)
	{
		float startX = transform.localPosition.x;
		return SimpleTween.Create(transform, duration, delegate(float t)
		{
			Vector3 pos = transform.localPosition;
			pos.x = Mathf.LerpUnclamped(startX, endX, t);
			transform.localPosition = pos;
		});
	}

	public static SimpleTween DOFloat(this Material material, float endValue, string property, float duration)
	{
		float startValue = material.GetFloat(property);
		return SimpleTween.Create(material, duration, delegate(float t)
		{
			material.SetFloat(property, Mathf.LerpUnclamped(startValue, endValue, t));
		});
	}

	public static void DOKill(this UnityEngine.Object target)
	{
		SimpleTween.KillTarget(target);
	}
}
