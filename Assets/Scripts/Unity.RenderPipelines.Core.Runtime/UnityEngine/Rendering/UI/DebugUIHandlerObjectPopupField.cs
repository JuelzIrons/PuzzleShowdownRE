namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerObjectPopupField : global::UnityEngine.Rendering.UI.DebugUIHandlerField<global::UnityEngine.Rendering.DebugUI.ObjectPopupField>
	{
		private int m_Index;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Index = 0;
		}

		private void ChangeSelectedObject()
		{
			if (m_Field == null)
			{
				return;
			}
			global::System.Collections.Generic.IEnumerable<global::UnityEngine.Object> enumerable = m_Field.getObjects();
			if (enumerable != null)
			{
				global::UnityEngine.Object[] array = global::System.Linq.Enumerable.ToArray(enumerable);
				int num = array.Length;
				if (m_Index >= num)
				{
					m_Index = 0;
				}
				else if (m_Index < 0)
				{
					m_Index = num - 1;
				}
				global::UnityEngine.Object value = array[m_Index];
				m_Field.SetValue(value);
				UpdateValueLabel();
			}
		}

		public override void OnIncrement(bool fast)
		{
			m_Index++;
			ChangeSelectedObject();
		}

		public override void OnDecrement(bool fast)
		{
			m_Index--;
			ChangeSelectedObject();
		}

		public override void UpdateValueLabel()
		{
			global::UnityEngine.Object value = m_Field.GetValue();
			string labelText = ((value != null) ? value.name : "Empty");
			SetLabelText(labelText);
		}
	}
}
