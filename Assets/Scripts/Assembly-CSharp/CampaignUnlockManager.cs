public class CampaignUnlockManager : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Animator m_unlockAnim;

	public static CampaignUnlockManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		CheckForUnlocks();
	}

	public void CheckForUnlocks()
	{
		if (!global::UnityEngine.PlayerPrefs.HasKey("UL"))
		{
			global::UnityEngine.PlayerPrefs.SetInt("UL", 0);
		}
		global::UnityEngine.Debug.Log(string.Format("{0}", global::UnityEngine.PlayerPrefs.GetInt("UL")));
		m_unlockAnim.SetInteger("stage", global::UnityEngine.PlayerPrefs.GetInt("UL"));
		m_unlockAnim.CrossFade(string.Format("BaseLayer.{0}", global::UnityEngine.PlayerPrefs.GetInt("UL")), 0f, 0, 1f);
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
		if (global::UnityEngine.PlayerPrefs.GetInt("UL") == num)
		{
			m_unlockAnim.SetInteger("stage", num);
			m_unlockAnim.CrossFade($"BaseLayer.{num}", 0f, 0, 1f);
		}
		else
		{
			m_unlockAnim.SetInteger("stage", num);
			global::UnityEngine.PlayerPrefs.SetInt("UL", num);
		}
	}
}
