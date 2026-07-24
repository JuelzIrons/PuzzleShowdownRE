namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerBitField : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.Rendering.UI.UIFoldout valueToggle;

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.UI.DebugUIHandlerIndirectToggle> toggles;

		private global::UnityEngine.Rendering.DebugUI.BitField m_Field;

		private global::UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.BitField>();
			m_Container = GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerContainer>();
			nameLabel.text = m_Field.displayName;
			int i = 0;
			global::UnityEngine.GUIContent[] enumNames = m_Field.enumNames;
			foreach (global::UnityEngine.GUIContent gUIContent in enumNames)
			{
				if (i < toggles.Count)
				{
					global::UnityEngine.Rendering.UI.DebugUIHandlerIndirectToggle debugUIHandlerIndirectToggle = toggles[i];
					debugUIHandlerIndirectToggle.getter = GetValue;
					debugUIHandlerIndirectToggle.setter = SetValue;
					debugUIHandlerIndirectToggle.nextUIHandler = ((i < m_Field.enumNames.Length - 1) ? toggles[i + 1] : null);
					debugUIHandlerIndirectToggle.previousUIHandler = ((i > 0) ? toggles[i - 1] : null);
					debugUIHandlerIndirectToggle.parentUIHandler = this;
					debugUIHandlerIndirectToggle.index = i;
					debugUIHandlerIndirectToggle.nameLabel.text = gUIContent.text;
					debugUIHandlerIndirectToggle.Init();
					i++;
				}
			}
			for (; i < toggles.Count; i++)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(toggles[i].gameObject);
				toggles[i] = null;
			}
		}

		private bool GetValue(int index)
		{
			if (index == 0)
			{
				return false;
			}
			index--;
			return (global::System.Convert.ToInt32(m_Field.GetValue()) & (1 << index)) != 0;
		}

		private void SetValue(int index, bool value)
		{
			if (index == 0)
			{
				m_Field.SetValue(global::System.Enum.ToObject(m_Field.enumType, 0));
				{
					foreach (global::UnityEngine.Rendering.UI.DebugUIHandlerIndirectToggle toggle in toggles)
					{
						if ((object)toggle != null && toggle.getter != null)
						{
							toggle.UpdateValueLabel();
						}
					}
					return;
				}
			}
			int num = global::System.Convert.ToInt32(m_Field.GetValue());
			num = ((!value) ? (num & ~m_Field.enumValues[index]) : (num | m_Field.enumValues[index]));
			m_Field.SetValue(global::System.Enum.ToObject(m_Field.enumType, num));
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			if (fromNext || !valueToggle.isOn)
			{
				nameLabel.color = colorSelected;
			}
			else if (valueToggle.isOn)
			{
				if (m_Container.IsDirectChild(previous))
				{
					nameLabel.color = colorSelected;
				}
				else
				{
					global::UnityEngine.Rendering.UI.DebugUIHandlerWidget lastItem = m_Container.GetLastItem();
					global::UnityEngine.Rendering.DebugManager.instance.ChangeSelection(lastItem, fromNext: false);
				}
			}
			return true;
		}

		public override void OnDeselection()
		{
			nameLabel.color = colorDefault;
		}

		public override void OnIncrement(bool fast)
		{
			valueToggle.isOn = true;
		}

		public override void OnDecrement(bool fast)
		{
			valueToggle.isOn = false;
		}

		public override void OnAction()
		{
			valueToggle.isOn = !valueToggle.isOn;
		}

		public override global::UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
		{
			if (!valueToggle.isOn || m_Container == null)
			{
				return base.Next();
			}
			global::UnityEngine.Rendering.UI.DebugUIHandlerWidget firstItem = m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}
	}
}
