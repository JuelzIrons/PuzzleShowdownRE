namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerMessageBox : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		private global::UnityEngine.Rendering.DebugUI.MessageBox m_Field;

		private static global::UnityEngine.Color32 k_WarningBackgroundColor = new global::UnityEngine.Color32(231, 180, 3, 30);

		private static global::UnityEngine.Color32 k_WarningTextColor = new global::UnityEngine.Color32(231, 180, 3, byte.MaxValue);

		private static global::UnityEngine.Color32 k_ErrorBackgroundColor = new global::UnityEngine.Color32(231, 75, 3, 30);

		private static global::UnityEngine.Color32 k_ErrorTextColor = new global::UnityEngine.Color32(231, 75, 3, byte.MaxValue);

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.MessageBox>();
			nameLabel.text = m_Field.displayName;
			global::UnityEngine.UI.Image component = GetComponent<global::UnityEngine.UI.Image>();
			switch (m_Field.style)
			{
			case global::UnityEngine.Rendering.DebugUI.MessageBox.Style.Warning:
				component.color = k_WarningBackgroundColor;
				break;
			case global::UnityEngine.Rendering.DebugUI.MessageBox.Style.Error:
				component.color = k_ErrorBackgroundColor;
				break;
			}
		}

		private void Update()
		{
			nameLabel.text = m_Field.message;
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			return false;
		}
	}
}
