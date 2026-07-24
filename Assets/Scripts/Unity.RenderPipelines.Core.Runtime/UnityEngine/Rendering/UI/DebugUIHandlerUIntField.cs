namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerUIntField : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.UI.Text valueLabel;

		private global::UnityEngine.Rendering.DebugUI.UIntField m_Field;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.UIntField>();
			nameLabel.text = m_Field.displayName;
			UpdateValueLabel();
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

		public override void OnIncrement(bool fast)
		{
			ChangeValue(fast, 1);
		}

		public override void OnDecrement(bool fast)
		{
			ChangeValue(fast, -1);
		}

		private void ChangeValue(bool fast, int multiplier)
		{
			long num = m_Field.GetValue();
			if (num != 0L || multiplier >= 0)
			{
				num += m_Field.incStep * ((!fast) ? 1 : m_Field.intStepMult) * multiplier;
				m_Field.SetValue((uint)num);
				UpdateValueLabel();
			}
		}

		private void UpdateValueLabel()
		{
			if (valueLabel != null)
			{
				valueLabel.text = m_Field.GetValue().ToString("N0");
			}
		}
	}
}
