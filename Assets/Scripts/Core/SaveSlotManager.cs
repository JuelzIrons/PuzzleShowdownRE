public class SaveSlotManager : global::UnityEngine.MonoBehaviour
{
	public static SaveSlotManager Instance;

	private void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			RefreshStates();
		}
		else
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void EraseSaveSlotBtn(int num)
	{
		SaveData saveData = SaveSystem.Load();
		if (num == 0)
		{
			saveData.WAK_Slot0 = 0;
			saveData.WAK_Slot0Sub = 0;
		}
		SaveSystem.Save(saveData);
		RefreshStates();
	}

	public void RefreshStates()
	{
		SaveSystem.Load();
	}
}
