namespace UnityEngine.Rendering.UI
{
	public abstract class DebugUIHandlerField<T> : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget where T : global::UnityEngine.Rendering.DebugUI.Widget
	{
		public global::UnityEngine.UI.Text nextButtonText;

		public global::UnityEngine.UI.Text previousButtonText;

		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.UI.Text valueLabel;

		protected internal T m_Field;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<T>();
			nameLabel.text = m_Field.displayName;
			UpdateValueLabel();
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			if (nextButtonText != null)
			{
				nextButtonText.color = colorSelected;
			}
			if (previousButtonText != null)
			{
				previousButtonText.color = colorSelected;
			}
			nameLabel.color = colorSelected;
			valueLabel.color = colorSelected;
			return true;
		}

		public override void OnDeselection()
		{
			if (nextButtonText != null)
			{
				nextButtonText.color = colorDefault;
			}
			if (previousButtonText != null)
			{
				previousButtonText.color = colorDefault;
			}
			nameLabel.color = colorDefault;
			valueLabel.color = colorDefault;
		}

		public override void OnAction()
		{
			OnIncrement(fast: false);
		}

		public abstract void UpdateValueLabel();

		protected void SetLabelText(string text)
		{
			if (text.Length > 26)
			{
				text = text.Substring(0, 23) + "...";
			}
			valueLabel.text = text;
		}
	}
}
