namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerRenderingLayerField : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.Rendering.UI.UIFoldout valueToggle;

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.UI.DebugUIHandlerIndirectToggle> toggles;

		private global::UnityEngine.Rendering.DebugUI.RenderingLayerField m_Field;

		private global::UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.RenderingLayerField>();
			m_Container = GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerContainer>();
			nameLabel.text = m_Field.displayName;
			int i = 0;
			int num = m_Field.renderingLayersNames.Length - 1;
			string[] renderingLayersNames = m_Field.renderingLayersNames;
			foreach (string text in renderingLayersNames)
			{
				if (i < toggles.Count)
				{
					global::UnityEngine.Rendering.UI.DebugUIHandlerIndirectToggle debugUIHandlerIndirectToggle = toggles[i];
					debugUIHandlerIndirectToggle.getter = GetValue;
					debugUIHandlerIndirectToggle.setter = SetValue;
					debugUIHandlerIndirectToggle.nextUIHandler = ((i < num) ? toggles[i + 1] : null);
					debugUIHandlerIndirectToggle.previousUIHandler = ((i > 0) ? toggles[i - 1] : null);
					debugUIHandlerIndirectToggle.parentUIHandler = this;
					debugUIHandlerIndirectToggle.index = i;
					debugUIHandlerIndirectToggle.nameLabel.text = text;
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
			return ((uint)m_Field.GetValue() & (uint)(1 << index)) != 0;
		}

		private void SetValue(int index, bool value)
		{
			global::UnityEngine.RenderingLayerMask value2 = m_Field.GetValue();
			value2 = ((!value) ? ((global::UnityEngine.RenderingLayerMask)((int)value2 & ~(1 << index))) : ((global::UnityEngine.RenderingLayerMask)((int)value2 | (1 << index))));
			m_Field.SetValue(value2);
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
