namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerRow : global::UnityEngine.Rendering.UI.DebugUIHandlerFoldout
	{
		private float m_Timer;

		protected override void OnEnable()
		{
			m_Timer = 0f;
		}

		private global::UnityEngine.GameObject GetChild(int index)
		{
			if (index < 0)
			{
				return null;
			}
			if (base.gameObject.transform != null)
			{
				global::UnityEngine.Transform child = base.gameObject.transform.GetChild(1);
				if (child != null && child.childCount > index)
				{
					return child.GetChild(index).gameObject;
				}
			}
			return null;
		}

		private bool TryGetChild(int index, out global::UnityEngine.GameObject child)
		{
			child = GetChild(index);
			return child != null;
		}

		private bool IsActive(global::UnityEngine.Rendering.DebugUI.Table table, int index, global::UnityEngine.GameObject child)
		{
			if (table == null || !table.GetColumnVisibility(index))
			{
				return false;
			}
			global::UnityEngine.Transform transform = child.transform.Find("Value");
			if (transform != null && transform.TryGetComponent<global::UnityEngine.UI.Text>(out var component))
			{
				return !string.IsNullOrEmpty(component.text);
			}
			return true;
		}

		protected void Update()
		{
			global::UnityEngine.Rendering.DebugUI.Table.Row row = CastWidget<global::UnityEngine.Rendering.DebugUI.Table.Row>();
			global::UnityEngine.Rendering.DebugUI.Table table = row.parent as global::UnityEngine.Rendering.DebugUI.Table;
			float num = 0.1f;
			bool flag = m_Timer >= num;
			if (flag)
			{
				m_Timer -= num;
			}
			m_Timer += global::UnityEngine.Time.deltaTime;
			for (int i = 0; i < row.children.Count; i++)
			{
				if (!TryGetChild(i, out var child))
				{
					continue;
				}
				bool flag2 = IsActive(table, i, child);
				if (child != null)
				{
					child.SetActive(flag2);
				}
				if (flag2 && flag)
				{
					if (child.TryGetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerColor>(out var component))
					{
						component.UpdateColor();
					}
					if (child.TryGetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerToggle>(out var component2))
					{
						component2.UpdateValueLabel();
					}
					if (child.TryGetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerObjectList>(out var component3))
					{
						component3.UpdateValueLabel();
					}
				}
			}
			global::UnityEngine.Rendering.UI.DebugUIHandlerWidget debugUIHandlerWidget = GetChild(0).GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerWidget>();
			global::UnityEngine.Rendering.UI.DebugUIHandlerWidget debugUIHandlerWidget2 = null;
			for (int j = 0; j < row.children.Count; j++)
			{
				debugUIHandlerWidget.previousUIHandler = debugUIHandlerWidget2;
				if (!TryGetChild(j, out var child2))
				{
					continue;
				}
				if (IsActive(table, j, child2))
				{
					debugUIHandlerWidget2 = debugUIHandlerWidget;
				}
				bool flag3 = false;
				for (int k = j + 1; k < row.children.Count; k++)
				{
					if (TryGetChild(k, out var child3) && IsActive(table, k, child3))
					{
						global::UnityEngine.Rendering.UI.DebugUIHandlerWidget debugUIHandlerWidget3 = (debugUIHandlerWidget.nextUIHandler = child2.GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerWidget>());
						debugUIHandlerWidget = debugUIHandlerWidget3;
						j = k - 1;
						flag3 = true;
						break;
					}
				}
				if (!flag3)
				{
					debugUIHandlerWidget.nextUIHandler = null;
					break;
				}
			}
		}
	}
}
