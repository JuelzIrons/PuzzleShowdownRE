public class MainMenuExplainer : global::UnityEngine.MonoBehaviour
{
	[global::System.Serializable]
	public struct ExplainerEntry
	{
		public global::UnityEngine.UI.Button button;

		[global::UnityEngine.TextArea]
		public string explainerText;
	}

	[global::UnityEngine.SerializeField]
	private MainMenuExplainer.ExplainerEntry[] m_entries;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_explainerText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_explainerObj;

	private void Update()
	{
		global::UnityEngine.GameObject gameObject = ((global::UnityEngine.EventSystems.EventSystem.current != null) ? global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject : null);
		if (gameObject == null)
		{
			m_explainerObj.SetActive(value: false);
			return;
		}
		for (int i = 0; i < m_entries.Length; i++)
		{
			if (m_entries[i].button != null && m_entries[i].button.gameObject == gameObject)
			{
				m_explainerText.text = m_entries[i].explainerText;
				m_explainerObj.SetActive(value: true);
				return;
			}
		}
		m_explainerObj.SetActive(value: false);
	}
}
