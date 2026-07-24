namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerVBox : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		private global::UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Container = GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerContainer>();
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
