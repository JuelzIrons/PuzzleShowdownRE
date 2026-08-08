public class ReloadSceneIfLost : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private PodManager Poddy1;

	[global::UnityEngine.SerializeField]
	private PodManager Poddy2;

	private void Start()
	{
	}

	private void Update()
	{
		if (Poddy1.GameLoop.Lost)
		{
			global::UnityEngine.SceneManagement.SceneManager.LoadScene(global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}
		if (Poddy2.GameLoop.Lost)
		{
			global::UnityEngine.SceneManagement.SceneManager.LoadScene(global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}
	}
}
