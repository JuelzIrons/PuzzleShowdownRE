public class SteamInitTest : global::UnityEngine.MonoBehaviour
{
	private void Start()
	{
		if (!SteamManager.Initialized)
		{
			global::UnityEngine.Debug.LogWarning("Steam not initilized!");
		}
		else
		{
			global::UnityEngine.Debug.Log(global::Steamworks.SteamFriends.GetPersonaName() + " logged in!");
		}
	}
}
