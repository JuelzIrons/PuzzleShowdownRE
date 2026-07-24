namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerPanel : global::UnityEngine.MonoBehaviour
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.UI.ScrollRect scrollRect;

		public global::UnityEngine.RectTransform viewport;

		public global::UnityEngine.Rendering.UI.DebugUIHandlerCanvas Canvas;

		private global::UnityEngine.RectTransform m_ScrollTransform;

		private global::UnityEngine.RectTransform m_ContentTransform;

		private global::UnityEngine.RectTransform m_MaskTransform;

		private global::UnityEngine.Rendering.UI.DebugUIHandlerWidget m_ScrollTarget;

		protected internal global::UnityEngine.Rendering.DebugUI.Panel m_Panel;

		private void OnEnable()
		{
			m_ScrollTransform = scrollRect.GetComponent<global::UnityEngine.RectTransform>();
			m_ContentTransform = GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerContainer>().contentHolder;
			m_MaskTransform = GetComponentInChildren<global::UnityEngine.UI.Mask>(includeInactive: true).rectTransform;
		}

		internal void SetPanel(global::UnityEngine.Rendering.DebugUI.Panel panel)
		{
			m_Panel = panel;
			nameLabel.text = panel.displayName;
		}

		internal global::UnityEngine.Rendering.DebugUI.Panel GetPanel()
		{
			return m_Panel;
		}

		public void SelectNextItem()
		{
			Canvas.SelectNextPanel();
		}

		public void SelectPreviousItem()
		{
			Canvas.SelectPreviousPanel();
		}

		public void OnScrollbarClicked()
		{
			global::UnityEngine.Rendering.DebugManager.instance.SetScrollTarget(null);
		}

		internal void SetScrollTarget(global::UnityEngine.Rendering.UI.DebugUIHandlerWidget target)
		{
			m_ScrollTarget = target;
		}

		internal void UpdateScroll()
		{
			if (!(m_ScrollTarget == null))
			{
				global::UnityEngine.RectTransform component = m_ScrollTarget.GetComponent<global::UnityEngine.RectTransform>();
				float yPosInScroll = GetYPosInScroll(component);
				float num = (GetYPosInScroll(m_MaskTransform) - yPosInScroll) / (m_ContentTransform.rect.size.y - m_ScrollTransform.rect.size.y);
				float value = scrollRect.verticalNormalizedPosition - num;
				value = global::UnityEngine.Mathf.Clamp01(value);
				scrollRect.verticalNormalizedPosition = global::UnityEngine.Mathf.Lerp(scrollRect.verticalNormalizedPosition, value, global::UnityEngine.Time.deltaTime * 10f);
			}
		}

		private float GetYPosInScroll(global::UnityEngine.RectTransform target)
		{
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3((0.5f - target.pivot.x) * target.rect.size.x, (0.5f - target.pivot.y) * target.rect.size.y, 0f);
			global::UnityEngine.Vector3 position = target.localPosition + vector;
			global::UnityEngine.Vector3 position2 = target.parent.TransformPoint(position);
			return m_ScrollTransform.TransformPoint(position2).y;
		}

		internal global::UnityEngine.Rendering.UI.DebugUIHandlerWidget GetFirstItem()
		{
			return GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerContainer>().GetFirstItem();
		}

		public void ResetDebugManager()
		{
			global::UnityEngine.Rendering.DebugManager.instance.Reset();
		}
	}
}
