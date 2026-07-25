/// <summary>
/// Steam has been removed, so achievements and stats no longer go anywhere. The
/// component and its methods are kept because call sites across GameLoop,
/// CampaignManager, LineClearingManager and CutsceneTheatre still invoke them, and
/// because MainMenu.unity holds a reference to this script.
/// </summary>
public class SteamAchievementsHandler : global::UnityEngine.MonoBehaviour
{
	public static SteamAchievementsHandler Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		Instance = this;
		// GameLoop and LineClearingManager dereference Instance without a null check
		// from gameplay scenes, so this has to outlive MainMenu.
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}

	public void GrantAchievement(string AchivementCode)
	{
	}

	public void SetStats(string statname, float fdata)
	{
	}

	public void Resetcheivements()
	{
	}
}
