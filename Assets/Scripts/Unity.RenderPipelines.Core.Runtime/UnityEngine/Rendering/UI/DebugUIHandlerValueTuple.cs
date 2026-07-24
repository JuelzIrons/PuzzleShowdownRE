namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerValueTuple : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.UI.Text valueLabel;

		protected internal global::UnityEngine.Rendering.DebugUI.ValueTuple m_Field;

		protected internal global::UnityEngine.UI.Text[] valueElements;

		private const float k_XOffset = 230f;

		private float m_Timer;

		private static readonly global::UnityEngine.Color k_ZeroColor = global::UnityEngine.Color.gray;

		protected override void OnEnable()
		{
			m_Timer = 0f;
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			nameLabel.color = colorSelected;
			return true;
		}

		public override void OnDeselection()
		{
			nameLabel.color = colorDefault;
		}

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			m_Widget = widget;
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.ValueTuple>();
			nameLabel.text = m_Field.displayName;
			int numElements = m_Field.numElements;
			valueElements = new global::UnityEngine.UI.Text[numElements];
			valueElements[0] = valueLabel;
			float num = 230f / (float)numElements;
			for (int i = 1; i < numElements; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(valueLabel.gameObject, base.transform);
				gameObject.AddComponent<global::UnityEngine.UI.LayoutElement>().ignoreLayout = true;
				global::UnityEngine.RectTransform obj = gameObject.transform as global::UnityEngine.RectTransform;
				global::UnityEngine.RectTransform rectTransform = nameLabel.transform as global::UnityEngine.RectTransform;
				global::UnityEngine.Vector2 anchorMax = (obj.anchorMin = new global::UnityEngine.Vector2(0f, 1f));
				obj.anchorMax = anchorMax;
				obj.sizeDelta = new global::UnityEngine.Vector2(100f, 26f);
				global::UnityEngine.Vector3 vector2 = rectTransform.anchoredPosition;
				vector2.x += (float)(i + 1) * num + 200f;
				obj.anchoredPosition = vector2;
				obj.pivot = new global::UnityEngine.Vector2(0f, 1f);
				valueElements[i] = gameObject.GetComponent<global::UnityEngine.UI.Text>();
			}
		}

		internal virtual void UpdateValueLabels()
		{
			for (int i = 0; i < m_Field.numElements; i++)
			{
				if (i < valueElements.Length && valueElements[i] != null)
				{
					object value = m_Field.values[i].GetValue();
					valueElements[i].text = m_Field.values[i].FormatString(value);
					if (value is float)
					{
						valueElements[i].color = (((float)value == 0f) ? k_ZeroColor : colorDefault);
					}
				}
			}
		}

		private void Update()
		{
			if (m_Field != null && m_Timer >= m_Field.refreshRate)
			{
				UpdateValueLabels();
				m_Timer -= m_Field.refreshRate;
			}
			m_Timer += global::UnityEngine.Time.deltaTime;
		}
	}
}
