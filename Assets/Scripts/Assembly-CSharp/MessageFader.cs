public class MessageFader : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_textField;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationCurve m_easeCurve;

	private global::DG.Tweening.Tween m_displayTween;

	public void DisplayMessage()
	{
		if (global::DG.Tweening.TweenExtensions.IsActive(m_displayTween))
		{
			global::DG.Tweening.TweenExtensions.Kill(m_displayTween);
		}
		m_textField.color = new global::UnityEngine.Color(m_textField.color.r, m_textField.color.g, m_textField.color.b, 1f);
		m_displayTween = global::DG.Tweening.DOTweenModuleUI.DOColor(m_textField, new global::UnityEngine.Color(m_textField.color.r, m_textField.color.g, m_textField.color.b, 0f), 3f);
		global::DG.Tweening.TweenSettingsExtensions.SetEase(m_displayTween, m_easeCurve);
	}
}
