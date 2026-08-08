[global::System.Serializable]
public class SaveData
{
	public string playerName = "Player";

	public int WAK_Slot0;

	public int WAK_Slot0Sub;

	public int NicoleCampaign = -1;

	public int JeckaCampaign = -1;

	public int AriCampaign = -1;

	public int EmilyCampaign = -1;

	public int NicoleJeckaCampaign = -1;

	public HighscoreEntry[] highscores = new HighscoreEntry[0];

	public int[] lifetimeComboCounters = new int[0];

	public int[] lifetimeChainCounters = new int[0];

	public int WAKendingsUnlocked;

	public int HasUnlockedFinalCampaignCutscene;
}
