namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerToggleHistory : global::UnityEngine.Rendering.UI.DebugUIHandlerToggle
	{
		private global::UnityEngine.UI.Toggle[] historyToggles;

		private const float k_XOffset = 230f;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			int num = (widget as global::UnityEngine.Rendering.DebugUI.HistoryBoolField)?.historyDepth ?? 0;
			historyToggles = new global::UnityEngine.UI.Toggle[num];
			float num2 = ((num > 0) ? (230f / (float)num) : 0f);
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.UI.Toggle toggle = global::UnityEngine.Object.Instantiate(valueToggle, base.transform);
				global::UnityEngine.Vector3 position = toggle.transform.position;
				position.x += (float)(i + 1) * num2;
				toggle.transform.position = position;
				global::UnityEngine.UI.Image component = toggle.transform.GetChild(0).GetComponent<global::UnityEngine.UI.Image>();
				component.sprite = global::UnityEngine.Sprite.Create(global::UnityEngine.Texture2D.whiteTexture, new global::UnityEngine.Rect(-1f, -1f, 2f, 2f), global::UnityEngine.Vector2.zero);
				component.color = new global::UnityEngine.Color32(50, 50, 50, 120);
				component.transform.GetChild(0).GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color32(110, 110, 110, byte.MaxValue);
				historyToggles[i] = toggle.GetComponent<global::UnityEngine.UI.Toggle>();
			}
			base.SetWidget(widget);
		}

		protected internal override void UpdateValueLabel()
		{
			base.UpdateValueLabel();
			global::UnityEngine.Rendering.DebugUI.HistoryBoolField historyBoolField = m_Field as global::UnityEngine.Rendering.DebugUI.HistoryBoolField;
			int num = historyBoolField?.historyDepth ?? 0;
			for (int i = 0; i < num; i++)
			{
				if (i < historyToggles.Length && historyToggles[i] != null)
				{
					historyToggles[i].isOn = historyBoolField.GetHistoryValue(i);
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
			valueToggle.isOn = m_Field.getter();
		}
	}
}
