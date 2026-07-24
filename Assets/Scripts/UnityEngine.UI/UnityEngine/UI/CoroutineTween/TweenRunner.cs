namespace UnityEngine.UI.CoroutineTween
{
	internal class TweenRunner<T> where T : struct, global::UnityEngine.UI.CoroutineTween.ITweenValue
	{
		protected global::UnityEngine.MonoBehaviour m_CoroutineContainer;

		protected global::System.Collections.IEnumerator m_Tween;

		private static global::System.Collections.IEnumerator Start(T tweenInfo)
		{
			if (tweenInfo.ValidTarget())
			{
				float elapsedTime = 0f;
				while (elapsedTime < tweenInfo.duration)
				{
					elapsedTime += (tweenInfo.ignoreTimeScale ? global::UnityEngine.Time.unscaledDeltaTime : global::UnityEngine.Time.deltaTime);
					float floatPercentage = global::UnityEngine.Mathf.Clamp01(elapsedTime / tweenInfo.duration);
					tweenInfo.TweenValue(floatPercentage);
					yield return null;
				}
				tweenInfo.TweenValue(1f);
			}
		}

		public void Init(global::UnityEngine.MonoBehaviour coroutineContainer)
		{
			m_CoroutineContainer = coroutineContainer;
		}

		public void StartTween(T info)
		{
			if (m_CoroutineContainer == null)
			{
				global::UnityEngine.Debug.LogWarning("Coroutine container not configured... did you forget to call Init?");
				return;
			}
			StopTween();
			if (!m_CoroutineContainer.gameObject.activeInHierarchy)
			{
				info.TweenValue(1f);
				return;
			}
			m_Tween = Start(info);
			m_CoroutineContainer.StartCoroutine(m_Tween);
		}

		public void StopTween()
		{
			if (m_Tween != null)
			{
				m_CoroutineContainer.StopCoroutine(m_Tween);
				m_Tween = null;
			}
		}
	}
}
