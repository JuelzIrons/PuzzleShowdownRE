public class VideoPlayerManager : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Video.VideoPlayer videoPlayer;

	[global::UnityEngine.SerializeField]
	private string[] m_vidUrls;

	public static VideoPlayerManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	public void PlayVideoUsingURL(int index)
	{
		videoPlayer.url = global::System.IO.Path.Combine(global::UnityEngine.Application.streamingAssetsPath, m_vidUrls[index]);
		videoPlayer.Play();
	}
}
