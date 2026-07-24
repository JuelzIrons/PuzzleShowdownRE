public class CampaignMenu : global::UnityEngine.MonoBehaviour
{
	public global::System.Collections.Generic.List<global::UnityEngine.GameObject> CharacterButtons = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

	[global::UnityEngine.SerializeField]
	private float MAX_HEIGHT;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_stageIndicator;

	public int SelectedDifficulty;

	private void Start()
	{
		RefreshDifficulties();
	}

	private void RefreshDifficulties()
	{
		SaveData saveData = SaveSystem.Load();
		for (int i = 0; i < 4; i++)
		{
			switch (i)
			{
			case 0:
				if (saveData.JeckaCampaign == -1 && saveData.NicoleCampaign >= CampaignManager.Instance.GetCurrentLevelDataCount(0))
				{
					saveData.JeckaCampaign = 0;
				}
				break;
			case 1:
				if (saveData.AriCampaign == -1 && saveData.JeckaCampaign >= CampaignManager.Instance.GetCurrentLevelDataCount(1))
				{
					saveData.AriCampaign = 0;
				}
				break;
			case 2:
				if (saveData.EmilyCampaign == -1 && saveData.AriCampaign >= CampaignManager.Instance.GetCurrentLevelDataCount(2))
				{
					saveData.EmilyCampaign = 0;
				}
				break;
			case 3:
				if (saveData.NicoleJeckaCampaign == -1 && saveData.EmilyCampaign >= CampaignManager.Instance.GetCurrentLevelDataCount(3))
				{
					saveData.NicoleJeckaCampaign = 0;
				}
				break;
			}
		}
		CharacterButtons[0].SetActive(value: true);
		if (saveData.JeckaCampaign == -1)
		{
			CharacterButtons[1].SetActive(value: false);
		}
		else
		{
			CharacterButtons[1].SetActive(value: true);
		}
		if (saveData.AriCampaign == -1)
		{
			CharacterButtons[2].SetActive(value: false);
		}
		else
		{
			CharacterButtons[2].SetActive(value: true);
		}
		if (saveData.EmilyCampaign == -1)
		{
			CharacterButtons[3].SetActive(value: false);
		}
		else
		{
			CharacterButtons[3].SetActive(value: true);
		}
		if (saveData.NicoleJeckaCampaign == -1)
		{
			CharacterButtons[4].SetActive(value: false);
		}
		else
		{
			CharacterButtons[4].SetActive(value: true);
		}
	}
}
