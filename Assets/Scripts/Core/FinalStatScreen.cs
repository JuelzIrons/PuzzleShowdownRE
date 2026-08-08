public class FinalStatScreen : global::UnityEngine.MonoBehaviour
{
	public PodManager MyPodManager;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_statBlockPrefab;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_statBlockPrefabChain;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_statBlockPrefabCombo;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform m_contentLayoutGroup;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_singlePlayerObject;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_btns;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_midPos;

	private void OnEnable()
	{
		GenerateStatBlock("score - " + MyPodManager.GameLoop.Score);
		int num = global::UnityEngine.Mathf.FloorToInt(MyPodManager.GameLoop.TimePassed / 60f);
		int num2 = global::UnityEngine.Mathf.FloorToInt(MyPodManager.GameLoop.TimePassed - (float)(num * 60));
		string text = $"{num:00}:{num2:00}";
		GenerateStatBlock("time - " + text);
		for (int i = 0; i < MyPodManager.GameLoop.ComboCounters.Length; i++)
		{
			_ = MyPodManager.GameLoop.ComboCounters[i];
			_ = 1;
			if (MyPodManager.GameLoop.ComboCounters[i] > 0 && i > 3)
			{
				GenerateStatBlockCombo(i, MyPodManager.GameLoop.ComboCounters[i]);
			}
		}
		for (int j = 1; j < MyPodManager.GameLoop.ChainCounters.Length; j++)
		{
			_ = MyPodManager.GameLoop.ChainCounters[j];
			_ = 1;
			if (MyPodManager.GameLoop.ChainCounters[j] > 0)
			{
				GenerateStatBlockChain(j + 1, MyPodManager.GameLoop.ChainCounters[j]);
			}
		}
		GenerateStatBlock("");
		GenerateStatBlock("");
		GenerateStatBlock("");
		GenerateStatBlock("");
		GenerateStatBlock("");
		GenerateStatBlock("");
		GenerateStatBlock("");
		GenerateStatBlock("");
		SaveLifetimeStats();
	}

	private void SaveLifetimeStats()
	{
		SaveData saveData = SaveSystem.Load();
		int[] comboCounters = MyPodManager.GameLoop.ComboCounters;
		int[] chainCounters = MyPodManager.GameLoop.ChainCounters;
		if (saveData.lifetimeComboCounters.Length < comboCounters.Length)
		{
			global::System.Array.Resize(ref saveData.lifetimeComboCounters, comboCounters.Length);
		}
		for (int i = 0; i < comboCounters.Length; i++)
		{
			saveData.lifetimeComboCounters[i] += comboCounters[i];
		}
		if (saveData.lifetimeChainCounters.Length < chainCounters.Length)
		{
			global::System.Array.Resize(ref saveData.lifetimeChainCounters, chainCounters.Length);
		}
		for (int j = 0; j < chainCounters.Length; j++)
		{
			saveData.lifetimeChainCounters[j] += chainCounters[j];
		}
		SaveSystem.Save(saveData);
	}

	private void GenerateStatBlockChain(int chainX, int amount)
	{
		global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(m_statBlockPrefabChain);
		obj.GetComponent<global::TMPro.TextMeshProUGUI>().text = amount.ToString() ?? "";
		obj.transform.GetChild(1).GetComponent<global::TMPro.TextMeshProUGUI>().text = chainX.ToString() ?? "";
		obj.transform.SetParent(m_contentLayoutGroup);
		obj.transform.localScale = global::UnityEngine.Vector3.one;
	}

	private void GenerateStatBlockCombo(int comboX, int amount)
	{
		global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(m_statBlockPrefabCombo);
		obj.GetComponent<global::TMPro.TextMeshProUGUI>().text = amount.ToString() ?? "";
		obj.transform.GetChild(1).GetComponent<global::TMPro.TextMeshProUGUI>().text = comboX.ToString() ?? "";
		obj.transform.SetParent(m_contentLayoutGroup);
		obj.transform.localScale = global::UnityEngine.Vector3.one;
	}

	private void GenerateStatBlock(string value)
	{
		global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(m_statBlockPrefab);
		obj.GetComponent<global::TMPro.TextMeshProUGUI>().text = value;
		obj.transform.SetParent(m_contentLayoutGroup);
		obj.transform.localScale = global::UnityEngine.Vector3.one;
	}

	public void Set2Pmode()
	{
		if (m_btns != null)
		{
			m_btns.transform.SetParent(m_midPos.transform);
			m_btns.transform.localPosition = global::UnityEngine.Vector3.zero;
		}
		m_singlePlayerObject.SetActive(value: false);
	}
}
