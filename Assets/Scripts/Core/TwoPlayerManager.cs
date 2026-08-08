public class TwoPlayerManager : global::UnityEngine.MonoBehaviour
{
	public static TwoPlayerManager Instance;

	private bool m_hasCalledGameOver;

	private PodManager m_pendingLoser;

	private void Awake()
	{
		Instance = this;
	}

	public void OnPlayerLose(PodManager losingManager)
	{
		if (m_hasCalledGameOver)
		{
			return;
		}
		if (GameManager.Instance.DefinedGameMode == GameModeType.LocalMp)
		{
			if (m_pendingLoser == null)
			{
				m_pendingLoser = losingManager;
				StartCoroutine(CheckForLossWithinSameFrame(losingManager));
			}
			else
			{
				m_pendingLoser = losingManager;
			}
			return;
		}
		if (GameManager.Instance.DefinedGameMode == GameModeType.Campaign)
		{
			PodManager[] array = global::UnityEngine.Object.FindObjectsByType<PodManager>(global::UnityEngine.FindObjectsInactive.Exclude, global::UnityEngine.FindObjectsSortMode.None);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GameLoop.Lost = true;
				array[i].GameLoop.CheckForLoss(fromTwoPlayerManager: true);
				array[i].GameLoop.Clearing = true;
				if (array[i] == losingManager)
				{
					losingManager.GameLoop.EndGame();
				}
				else
				{
					array[i].GameLoop.EndGame(isWinner: true);
				}
			}
			global::UnityEngine.Debug.Log("ITS A MAtch");
			return;
		}
		m_hasCalledGameOver = true;
		PodManager[] array2 = global::UnityEngine.Object.FindObjectsByType<PodManager>(global::UnityEngine.FindObjectsInactive.Exclude, global::UnityEngine.FindObjectsSortMode.None);
		for (int j = 0; j < array2.Length; j++)
		{
			if (array2[j] != losingManager)
			{
				array2[j].GameLoop.Lost = true;
				array2[j].GameLoop.CheckForLoss(fromTwoPlayerManager: true);
				array2[j].GameLoop.Clearing = true;
			}
		}
	}

	private global::System.Collections.IEnumerator CheckForLossWithinSameFrame(PodManager calledByLoser)
	{
		if (m_hasCalledGameOver)
		{
			yield break;
		}
		yield return new global::UnityEngine.WaitForEndOfFrame();
		if (m_pendingLoser != calledByLoser)
		{
			m_hasCalledGameOver = true;
			global::UnityEngine.Debug.Log("ITS A DRAW!");
			if (m_pendingLoser.GameLoop.Score > calledByLoser.GameLoop.Score)
			{
				calledByLoser.GameLoop.EndGame();
				m_pendingLoser.GameLoop.EndGame(isWinner: true);
			}
			else if (m_pendingLoser.GameLoop.Score < calledByLoser.GameLoop.Score)
			{
				calledByLoser.GameLoop.EndGame(isWinner: true);
				m_pendingLoser.GameLoop.EndGame();
			}
			else
			{
				calledByLoser.GameLoop.EndGame(isWinner: false, isDraw: true);
				m_pendingLoser.GameLoop.EndGame(isWinner: false, isDraw: true);
			}
			PodManager[] array = global::UnityEngine.Object.FindObjectsByType<PodManager>(global::UnityEngine.FindObjectsInactive.Exclude, global::UnityEngine.FindObjectsSortMode.None);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GameLoop.Lost = true;
				array[i].GameLoop.CheckForLoss(fromTwoPlayerManager: true);
				array[i].GameLoop.Clearing = true;
			}
			yield break;
		}
		m_hasCalledGameOver = true;
		PodManager[] array2 = global::UnityEngine.Object.FindObjectsByType<PodManager>(global::UnityEngine.FindObjectsInactive.Exclude, global::UnityEngine.FindObjectsSortMode.None);
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].GameLoop.Lost = true;
			array2[j].GameLoop.CheckForLoss(fromTwoPlayerManager: true);
			array2[j].GameLoop.Clearing = true;
			if (array2[j] == calledByLoser)
			{
				calledByLoser.GameLoop.EndGame();
			}
			else
			{
				array2[j].GameLoop.EndGame(isWinner: true);
			}
		}
		global::UnityEngine.Debug.Log("ITS A MAtch");
	}

	public void FlushPendingLoss()
	{
		if (m_pendingLoser == null || m_hasCalledGameOver || GameManager.Instance.DefinedGameMode != GameModeType.LocalMp)
		{
			return;
		}
		m_hasCalledGameOver = true;
		PodManager pendingLoser = m_pendingLoser;
		m_pendingLoser = null;
		PodManager[] array = global::UnityEngine.Object.FindObjectsByType<PodManager>(global::UnityEngine.FindObjectsInactive.Exclude, global::UnityEngine.FindObjectsSortMode.None);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != pendingLoser)
			{
				array[i].GameLoop.Lost = true;
				array[i].GameLoop.CheckForLoss(fromTwoPlayerManager: true);
				array[i].GameLoop.Clearing = true;
			}
		}
	}
}
