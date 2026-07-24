namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerProgressBar : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.UI.Text valueLabel;

		public global::UnityEngine.RectTransform progressBarRect;

		private global::UnityEngine.Rendering.DebugUI.ProgressBarValue m_Value;

		private float m_Timer;

		protected override void OnEnable()
		{
			m_Timer = 0f;
		}

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Value = CastWidget<global::UnityEngine.Rendering.DebugUI.ProgressBarValue>();
			nameLabel.text = m_Value.displayName;
			UpdateValue();
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			nameLabel.color = colorSelected;
			return true;
		}

		public override void OnDeselection()
		{
			nameLabel.color = colorDefault;
		}

		private void Update()
		{
			if (m_Timer >= m_Value.refreshRate)
			{
				UpdateValue();
				m_Timer -= m_Value.refreshRate;
			}
			m_Timer += global::UnityEngine.Time.deltaTime;
		}

		private void UpdateValue()
		{
			float num = (float)m_Value.GetValue();
			valueLabel.text = m_Value.FormatString(num);
			global::UnityEngine.Vector3 localScale = progressBarRect.localScale;
			localScale.x = num;
			progressBarRect.localScale = localScale;
		}
	}
}
