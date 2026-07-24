namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerValue : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.UI.Text valueLabel;

		private global::UnityEngine.Rendering.DebugUI.Value m_Field;

		protected internal float m_Timer;

		private static readonly global::UnityEngine.Color k_ZeroColor = global::UnityEngine.Color.gray;

		protected override void OnEnable()
		{
			m_Timer = 0f;
		}

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.Value>();
			nameLabel.text = m_Field.displayName;
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			nameLabel.color = colorSelected;
			valueLabel.color = colorSelected;
			return true;
		}

		public override void OnDeselection()
		{
			nameLabel.color = colorDefault;
			valueLabel.color = colorDefault;
		}

		private void Update()
		{
			if (m_Timer >= m_Field.refreshRate)
			{
				object value = m_Field.GetValue();
				valueLabel.text = m_Field.FormatString(value);
				if (value is float)
				{
					valueLabel.color = (((float)value == 0f) ? k_ZeroColor : colorDefault);
				}
				m_Timer -= m_Field.refreshRate;
			}
			m_Timer += global::UnityEngine.Time.deltaTime;
		}
	}
}
