[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.AudioSource))]
public class LoopFromTimestamp : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.Header("Playback")]
	[global::UnityEngine.Tooltip("The AudioSource to control. If left empty, will use the one on this GameObject.")]
	public global::UnityEngine.AudioSource audioSource;

	[global::UnityEngine.Tooltip("Timestamp in seconds where playback should start.")]
	public float startTimestamp;

	[global::UnityEngine.Tooltip("Play automatically when this object starts.")]
	public bool playOnStart = true;

	[global::UnityEngine.Header("Fade In")]
	[global::UnityEngine.Tooltip("Enable fade-in when playback starts.")]
	public bool fadeInOnStart = true;

	[global::UnityEngine.Tooltip("Volume to start the fade from.")]
	[global::UnityEngine.Range(0f, 1f)]
	public float fadeStartVolume;

	[global::UnityEngine.Tooltip("Volume to fade up to (the 'normal' playing volume).")]
	[global::UnityEngine.Range(0f, 1f)]
	public float fadeTargetVolume = 1f;

	[global::UnityEngine.Tooltip("Duration of the fade-in, in seconds.")]
	public float fadeDuration = 2f;

	private global::UnityEngine.Coroutine fadeCoroutine;

	private void Awake()
	{
		if (audioSource == null)
		{
			audioSource = GetComponent<global::UnityEngine.AudioSource>();
		}
	}

	private void Start()
	{
		if (playOnStart)
		{
			PlayFromTimestamp(startTimestamp);
		}
	}

	public void PlayFromTimestamp(float timestamp)
	{
		if (audioSource == null || audioSource.clip == null)
		{
			global::UnityEngine.Debug.LogWarning("LoopFromTimestamp: No AudioSource or AudioClip assigned.");
			return;
		}
		timestamp = global::UnityEngine.Mathf.Clamp(timestamp, 0f, audioSource.clip.length);
		audioSource.loop = true;
		audioSource.time = timestamp;
		if (fadeInOnStart)
		{
			audioSource.volume = fadeStartVolume;
			audioSource.Play();
			StartFade(fadeStartVolume, fadeTargetVolume, fadeDuration);
		}
		else
		{
			audioSource.volume = fadeTargetVolume;
			audioSource.Play();
		}
	}

	public void StartFade(float fromVolume, float toVolume, float duration)
	{
		if (fadeCoroutine != null)
		{
			StopCoroutine(fadeCoroutine);
		}
		fadeCoroutine = StartCoroutine(FadeVolume(fromVolume, toVolume, duration));
	}

	public void FadeOut()
	{
		if (fadeCoroutine != null)
		{
			StopCoroutine(fadeCoroutine);
		}
		fadeCoroutine = StartCoroutine(FadeVolume(audioSource.volume, 0f, 0.5f));
	}

	private global::System.Collections.IEnumerator FadeVolume(float fromVolume, float toVolume, float duration)
	{
		float elapsed = 0f;
		audioSource.volume = fromVolume;
		if (duration <= 0f)
		{
			audioSource.volume = toVolume;
			yield break;
		}
		while (elapsed < duration)
		{
			elapsed += global::UnityEngine.Time.deltaTime;
			float t = global::UnityEngine.Mathf.Clamp01(elapsed / duration);
			audioSource.volume = global::UnityEngine.Mathf.Lerp(fromVolume, toVolume, t);
			yield return null;
		}
		audioSource.volume = toVolume;
		fadeCoroutine = null;
	}
}
