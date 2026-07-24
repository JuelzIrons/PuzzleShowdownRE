public class WAKEndingCounter : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_endingsUnlockedText;

	private void Awake()
	{
		SaveData saveData = SaveSystem.Load();
		int num = 0;
		for (int i = 0; i < 8; i++)
		{
			if ((saveData.WAKendingsUnlocked & (1 << i)) != 0)
			{
				num++;
			}
		}
		m_endingsUnlockedText.text = $"Weekends Unlocked: {num} / 8";
	}
}
