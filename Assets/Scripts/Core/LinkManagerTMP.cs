public class LinkManagerTMP : global::UnityEngine.MonoBehaviour, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.IEventSystemHandler
{
	private global::TMPro.TextMeshProUGUI m_text;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Canvas m_canvas;

	private global::UnityEngine.Camera m_cam;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Color32 m_hoverColor = global::UnityEngine.Color.yellow;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Color32 m_UnhoverColor = global::UnityEngine.Color.yellow;

	private int m_currentLink = -1;

	private global::UnityEngine.Color32[] m_originalVertexColors;

	private void Start()
	{
		m_text = GetComponent<global::TMPro.TextMeshProUGUI>();
		if (m_canvas.renderMode == global::UnityEngine.RenderMode.ScreenSpaceOverlay)
		{
			m_cam = null;
		}
		else
		{
			m_cam = m_canvas.worldCamera;
		}
	}

	private void Update()
	{
		global::UnityEngine.Vector3 mousePosition = global::UnityEngine.Input.mousePosition;
		int num = global::TMPro.TMP_TextUtilities.FindIntersectingLink(m_text, mousePosition, m_cam);
		if (num != m_currentLink)
		{
			if (m_currentLink != -1)
			{
				SetLinkColor(m_currentLink, m_UnhoverColor);
			}
			if (num != -1)
			{
				SetLinkColor(num, m_hoverColor);
			}
			m_currentLink = num;
		}
	}

	private void SetLinkColor(int linkIndex, global::UnityEngine.Color32? color)
	{
		global::TMPro.TMP_TextInfo textInfo = m_text.textInfo;
		global::TMPro.TMP_LinkInfo tMP_LinkInfo = textInfo.linkInfo[linkIndex];
		for (int i = 0; i < tMP_LinkInfo.linkTextLength; i++)
		{
			int num = tMP_LinkInfo.linkTextfirstCharacterIndex + i;
			global::TMPro.TMP_CharacterInfo tMP_CharacterInfo = textInfo.characterInfo[num];
			if (tMP_CharacterInfo.isVisible)
			{
				int materialReferenceIndex = tMP_CharacterInfo.materialReferenceIndex;
				int vertexIndex = tMP_CharacterInfo.vertexIndex;
				global::UnityEngine.Color32[] colors = textInfo.meshInfo[materialReferenceIndex].colors32;
				global::UnityEngine.Color32 color2 = color ?? ((global::UnityEngine.Color32)m_text.color);
				for (int j = 0; j < 4; j++)
				{
					colors[vertexIndex + j] = color2;
				}
			}
		}
		m_text.UpdateVertexData(global::TMPro.TMP_VertexDataUpdateFlags.Colors32);
	}

	public void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
	{
		int num = global::TMPro.TMP_TextUtilities.FindIntersectingLink(position: new global::UnityEngine.Vector3(eventData.position.x, eventData.position.y, 0f), text: m_text, camera: m_cam);
		if (num != -1)
		{
			global::TMPro.TMP_LinkInfo tMP_LinkInfo = m_text.textInfo.linkInfo[num];
			string linkID = tMP_LinkInfo.GetLinkID();
			if (linkID.Contains("https"))
			{
				OpenSteamBrowser(linkID);
			}
		}
	}

	public void OpenSteamBrowser(string url)
	{
		// Without the Steam overlay, hand the link to the system browser so the link
		// still does something instead of silently doing nothing.
		global::UnityEngine.Application.OpenURL(url);
	}
}
