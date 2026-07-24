namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerButton : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		private global::UnityEngine.Rendering.DebugUI.Button m_Field;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.Button>();
			nameLabel.text = m_Field.displayName;
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

		public override void OnAction()
		{
			if (m_Field.action != null)
			{
				m_Field.action();
			}
		}
	}
}
