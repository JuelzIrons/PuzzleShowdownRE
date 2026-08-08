public class CutsceneTheatre : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> m_cutsceneBtns;

	[global::UnityEngine.SerializeField]
	private CutscenePlayer m_cutscenePlayer;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_backBtn;

	private void OnEnable()
	{
		RefreshAvaiableCutscenes();
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_backBtn);
	}

	public void ClickCutsceneTheater(int index)
	{
		m_cutscenePlayer.PlayCutscene((CutsceneVideoIndicies)index, playSilently: false, playWithAnySkip: false, activatePanel: true);
	}

	public void RefreshAvaiableCutscenes()
	{
		SaveData saveData = SaveSystem.Load();
		for (int i = 0; i < m_cutsceneBtns.Count; i++)
		{
			m_cutsceneBtns[i].GetComponent<global::UnityEngine.UI.Button>().interactable = false;
		}
		for (int j = 0; j < m_cutsceneBtns.Count; j++)
		{
			if (j == 0)
			{
				m_cutsceneBtns[j].GetComponent<global::UnityEngine.UI.Button>().interactable = true;
			}
			if (j == 1 && saveData.JeckaCampaign != -1)
			{
				m_cutsceneBtns[j].GetComponent<global::UnityEngine.UI.Button>().interactable = true;
			}
			if (j == 2 && saveData.AriCampaign != -1)
			{
				m_cutsceneBtns[j].GetComponent<global::UnityEngine.UI.Button>().interactable = true;
			}
			if (j == 3 && saveData.EmilyCampaign != -1)
			{
				m_cutsceneBtns[j].GetComponent<global::UnityEngine.UI.Button>().interactable = true;
			}
			if (j == 4 && saveData.NicoleJeckaCampaign != -1)
			{
				m_cutsceneBtns[j].GetComponent<global::UnityEngine.UI.Button>().interactable = true;
			}
			if (j == 5 && saveData.HasUnlockedFinalCampaignCutscene > 0)
			{
				m_cutsceneBtns[j].GetComponent<global::UnityEngine.UI.Button>().interactable = true;
			}
		}
		if (saveData.HasUnlockedFinalCampaignCutscene > 0)
		{
			SteamAchievementsHandler.Instance?.GrantAchievement("THE_END");
		}
	}
}
