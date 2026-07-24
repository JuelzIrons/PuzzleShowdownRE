namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerEnumHistory : global::UnityEngine.Rendering.UI.DebugUIHandlerEnumField
	{
		private global::UnityEngine.UI.Text[] historyValues;

		private const float k_XOffset = 230f;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			int num = (widget as global::UnityEngine.Rendering.DebugUI.HistoryEnumField)?.historyDepth ?? 0;
			historyValues = new global::UnityEngine.UI.Text[num];
			float num2 = ((num > 0) ? (230f / (float)num) : 0f);
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.UI.Text text = global::UnityEngine.Object.Instantiate(valueLabel, base.transform);
				global::UnityEngine.Vector3 position = text.transform.position;
				position.x += (float)(i + 1) * num2;
				text.transform.position = position;
				global::UnityEngine.UI.Text component = text.GetComponent<global::UnityEngine.UI.Text>();
				component.color = new global::UnityEngine.Color32(110, 110, 110, byte.MaxValue);
				historyValues[i] = component;
			}
			base.SetWidget(widget);
		}

		public override void UpdateValueLabel()
		{
			int num = m_Field.currentIndex;
			if (num < 0)
			{
				num = 0;
			}
			valueLabel.text = m_Field.enumNames[num].text;
			global::UnityEngine.Rendering.DebugUI.HistoryEnumField historyEnumField = m_Field as global::UnityEngine.Rendering.DebugUI.HistoryEnumField;
			int num2 = historyEnumField?.historyDepth ?? 0;
			for (int i = 0; i < num2; i++)
			{
				if (i < historyValues.Length && historyValues[i] != null)
				{
					historyValues[i].text = historyEnumField.enumNames[historyEnumField.GetHistoryValue(i)].text;
				}
			}
			if (base.isActiveAndEnabled)
			{
				StartCoroutine(RefreshAfterSanitization());
			}
		}

		private global::System.Collections.IEnumerator RefreshAfterSanitization()
		{
			yield return null;
			m_Field.currentIndex = m_Field.getIndex();
			valueLabel.text = m_Field.enumNames[m_Field.currentIndex].text;
		}
	}
}
