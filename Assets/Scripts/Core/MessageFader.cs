public class MessageFader : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_textField;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationCurve m_easeCurve;

	private SimpleTween m_displayTween;

	public void DisplayMessage()
	{
		if (m_displayTween != null && m_displayTween.IsActive())
		{
			m_displayTween.Kill();
		}
		m_textField.color = new global::UnityEngine.Color(m_textField.color.r, m_textField.color.g, m_textField.color.b, 1f);
		m_displayTween = m_textField.DOColor(new global::UnityEngine.Color(m_textField.color.r, m_textField.color.g, m_textField.color.b, 0f), 3f);
		m_displayTween.SetEase(m_easeCurve);
	}
}
