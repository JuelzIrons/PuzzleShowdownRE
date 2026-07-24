namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerToggle : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.UI.Toggle valueToggle;

		public global::UnityEngine.UI.Image checkmarkImage;

		protected internal global::UnityEngine.Rendering.DebugUI.BoolField m_Field;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.BoolField>();
			nameLabel.text = m_Field.displayName;
			UpdateValueLabel();
			valueToggle.onValueChanged.AddListener(OnToggleValueChanged);
		}

		private void OnToggleValueChanged(bool value)
		{
			m_Field.SetValue(value);
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			nameLabel.color = colorSelected;
			checkmarkImage.color = colorSelected;
			return true;
		}

		public override void OnDeselection()
		{
			nameLabel.color = colorDefault;
			checkmarkImage.color = colorDefault;
		}

		public override void OnAction()
		{
			bool value = !m_Field.GetValue();
			m_Field.SetValue(value);
			UpdateValueLabel();
		}

		protected internal virtual void UpdateValueLabel()
		{
			if (valueToggle != null)
			{
				valueToggle.isOn = m_Field.GetValue();
			}
		}
	}
}
