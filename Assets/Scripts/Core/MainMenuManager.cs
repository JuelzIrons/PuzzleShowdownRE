public class MainMenuManager : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject FirstSelected;

	private void Start()
	{
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(FirstSelected);
		}
		PersistentInputReader.Instance.SetSelfUpdate(selfUpdating: true);
	}

	public void PressMultiplayer()
	{
		SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.MultiplayerMenu);
	}

	public void ExitGame()
	{
		global::UnityEngine.Application.Quit();
	}

	public void PressCampaign()
	{
		SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.CampaignMenu);
	}

	public void PressLineClear()
	{
		SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.LineClearingMenu);
	}

	public void PressMarathon()
	{
		SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.MarathonMenu);
	}

	public void PressAbout()
	{
		SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.AboutMenu);
	}

	public void PressOptions()
	{
		SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.SettingsMenu);
	}
}
