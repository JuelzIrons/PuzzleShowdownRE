public class JoinCodeHider : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_joinCode;

	private bool m_isHiding;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Color m_invis;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Color m_vis;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Toggle m_tog;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_stars;

	private void OnEnable()
	{
		if (global::UnityEngine.PlayerPrefs.HasKey("IsStreamerMode"))
		{
			if (global::UnityEngine.PlayerPrefs.GetInt("IsStreamerMode") == 0)
			{
				SetStreamerMode(isStreamerMode: false);
				m_tog.isOn = false;
			}
			else
			{
				SetStreamerMode(isStreamerMode: true);
				m_tog.isOn = true;
			}
		}
		else
		{
			SetStreamerMode(isStreamerMode: false);
			m_tog.isOn = false;
		}
	}

	public void SetStreamerMode(bool isStreamerMode)
	{
		m_isHiding = isStreamerMode;
		global::UnityEngine.PlayerPrefs.SetInt("IsStreamerMode", isStreamerMode ? 1 : 0);
	}

	private void Update()
	{
		m_stars.SetActive(m_isHiding);
		if (m_isHiding)
		{
			m_joinCode.color = m_invis;
		}
		else
		{
			m_joinCode.color = m_vis;
		}
	}
}
