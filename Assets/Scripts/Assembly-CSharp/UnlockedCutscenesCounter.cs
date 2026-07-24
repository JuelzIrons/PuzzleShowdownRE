public class UnlockedCutscenesCounter : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_campaignCutsceneCounter;

	private void Start()
	{
		SaveData saveData = SaveSystem.Load();
		int num = 1;
		if (saveData.JeckaCampaign != -1)
		{
			num++;
		}
		if (saveData.AriCampaign != -1)
		{
			num++;
		}
		if (saveData.EmilyCampaign != -1)
		{
			num++;
		}
		if (saveData.NicoleJeckaCampaign != -1)
		{
			num++;
		}
		if (saveData.HasUnlockedFinalCampaignCutscene > 0)
		{
			num++;
		}
		m_campaignCutsceneCounter.text = $"Cutscenes\r\nUnlocked: {num}/6";
	}
}
