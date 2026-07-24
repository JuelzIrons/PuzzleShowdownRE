namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerFoldout : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.Rendering.UI.UIFoldout valueToggle;

		private global::UnityEngine.Rendering.DebugUI.Foldout m_Field;

		private global::UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;

		private const float k_FoldoutXOffset = 215f;

		private const float k_XOffset = 230f;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.Foldout>();
			m_Container = GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerContainer>();
			nameLabel.text = m_Field.displayName;
			string[] columnLabels = m_Field.columnLabels;
			int num = ((columnLabels != null) ? columnLabels.Length : 0);
			float num2 = ((num > 0) ? (230f / (float)num) : 0f);
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(nameLabel.gameObject, GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerContainer>().contentHolder);
				obj.AddComponent<global::UnityEngine.UI.LayoutElement>().ignoreLayout = true;
				global::UnityEngine.RectTransform obj2 = obj.transform as global::UnityEngine.RectTransform;
				global::UnityEngine.RectTransform rectTransform = nameLabel.transform as global::UnityEngine.RectTransform;
				global::UnityEngine.Vector2 anchorMax = (obj2.anchorMin = new global::UnityEngine.Vector2(0f, 1f));
				obj2.anchorMax = anchorMax;
				obj2.sizeDelta = new global::UnityEngine.Vector2(100f, 26f);
				global::UnityEngine.Vector3 vector2 = rectTransform.anchoredPosition;
				vector2.x += (float)(i + 1) * num2 + 215f;
				obj2.anchoredPosition = vector2;
				obj2.pivot = new global::UnityEngine.Vector2(0f, 0.5f);
				obj2.eulerAngles = new global::UnityEngine.Vector3(0f, 0f, 13f);
				global::UnityEngine.UI.Text component = obj.GetComponent<global::UnityEngine.UI.Text>();
				component.fontSize = 15;
				component.text = m_Field.columnLabels[i];
			}
			UpdateValue();
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
			m_Field.SetValue(value: true);
			UpdateValue();
		}

		public override void OnDecrement(bool fast)
		{
			m_Field.SetValue(value: false);
			UpdateValue();
		}

		public override void OnAction()
		{
			bool value = !m_Field.GetValue();
			m_Field.SetValue(value);
			UpdateValue();
		}

		private void UpdateValue()
		{
			valueToggle.isOn = m_Field.GetValue();
		}

		public override global::UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
		{
			if (!m_Field.GetValue() || m_Container == null)
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
