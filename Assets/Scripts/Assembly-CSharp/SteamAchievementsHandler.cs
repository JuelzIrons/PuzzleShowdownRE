public class SteamAchievementsHandler : global::UnityEngine.MonoBehaviour
{
	public static SteamAchievementsHandler Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	public void GrantAchievement(string AchivementCode)
	{
		if (SteamManager.Initialized)
		{
			global::Steamworks.SteamUserStats.SetAchievement(AchivementCode);
			global::Steamworks.SteamUserStats.StoreStats();
		}
	}

	public void SetStats(string statname, float fdata)
	{
		global::Steamworks.SteamUserStats.SetStat(statname, fdata);
	}

	public void Resetcheivements()
	{
		global::Steamworks.SteamUserStats.ResetAllStats(bAchievementsToo: true);
	}
}
