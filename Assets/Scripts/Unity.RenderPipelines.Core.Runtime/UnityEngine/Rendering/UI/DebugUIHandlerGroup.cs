namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerGroup : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.Transform header;

		private global::UnityEngine.Rendering.DebugUI.Container m_Field;

		private global::UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.Container>();
			m_Container = GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerContainer>();
			if (m_Field.hideDisplayName)
			{
				header.gameObject.SetActive(value: false);
			}
			else
			{
				nameLabel.text = m_Field.displayName;
			}
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			if (!fromNext && !m_Container.IsDirectChild(previous))
			{
				global::UnityEngine.Rendering.UI.DebugUIHandlerWidget lastItem = m_Container.GetLastItem();
				global::UnityEngine.Rendering.DebugManager.instance.ChangeSelection(lastItem, fromNext: false);
				return true;
			}
			return false;
		}

		public override global::UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
		{
			if (m_Container == null)
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
