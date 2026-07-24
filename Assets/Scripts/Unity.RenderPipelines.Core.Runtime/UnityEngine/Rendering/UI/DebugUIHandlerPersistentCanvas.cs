namespace UnityEngine.Rendering.UI
{
	internal class DebugUIHandlerPersistentCanvas : global::UnityEngine.MonoBehaviour
	{
		public global::UnityEngine.RectTransform panel;

		public global::UnityEngine.RectTransform valuePrefab;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.UI.DebugUIHandlerValue> m_Items = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.UI.DebugUIHandlerValue>();

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.ValueTuple> m_ValueTupleWidgets = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.ValueTuple>();

		internal void Toggle(global::UnityEngine.Rendering.DebugUI.Value widget, string displayName = null)
		{
			int num = m_Items.FindIndex((global::UnityEngine.Rendering.UI.DebugUIHandlerValue x) => x.GetWidget() == widget);
			if (num > -1)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(m_Items[num].gameObject);
				m_Items.RemoveAt(num);
				return;
			}
			global::UnityEngine.Rendering.UI.DebugUIHandlerValue component = global::UnityEngine.Object.Instantiate(valuePrefab, panel, worldPositionStays: false).gameObject.GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerValue>();
			component.SetWidget(widget);
			component.nameLabel.text = (string.IsNullOrEmpty(displayName) ? widget.displayName : displayName);
			m_Items.Add(component);
		}

		internal void Toggle(global::UnityEngine.Rendering.DebugUI.ValueTuple widget, int? forceTupleIndex = null)
		{
			global::UnityEngine.Rendering.DebugUI.ValueTuple valueTuple = m_ValueTupleWidgets.Find((global::UnityEngine.Rendering.DebugUI.ValueTuple x) => x == widget);
			int num = valueTuple?.pinnedElementIndex ?? (-1);
			if (valueTuple != null)
			{
				m_ValueTupleWidgets.Remove(valueTuple);
				Toggle(widget.values[num]);
			}
			if (forceTupleIndex.HasValue)
			{
				num = forceTupleIndex.Value;
			}
			if (num + 1 < widget.numElements)
			{
				widget.pinnedElementIndex = num + 1;
				string text = widget.displayName;
				if (widget.parent is global::UnityEngine.Rendering.DebugUI.Foldout)
				{
					string[] columnLabels = (widget.parent as global::UnityEngine.Rendering.DebugUI.Foldout).columnLabels;
					if (columnLabels != null && widget.pinnedElementIndex < columnLabels.Length)
					{
						text = text + " (" + columnLabels[widget.pinnedElementIndex] + ")";
					}
				}
				Toggle(widget.values[widget.pinnedElementIndex], text);
				m_ValueTupleWidgets.Add(widget);
			}
			else
			{
				widget.pinnedElementIndex = -1;
			}
		}

		internal bool IsEmpty()
		{
			return m_Items.Count == 0;
		}

		internal void Clear()
		{
			foreach (global::UnityEngine.Rendering.UI.DebugUIHandlerValue item in m_Items)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(item.gameObject);
			}
			m_Items.Clear();
		}
	}
}
