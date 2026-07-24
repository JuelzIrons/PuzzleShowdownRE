namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerContainer : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		public global::UnityEngine.RectTransform contentHolder;

		internal global::UnityEngine.Rendering.UI.DebugUIHandlerWidget GetFirstItem()
		{
			if (contentHolder.childCount == 0)
			{
				return null;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.UI.DebugUIHandlerWidget> activeChildren = GetActiveChildren();
			if (activeChildren.Count == 0)
			{
				return null;
			}
			return activeChildren[0];
		}

		internal global::UnityEngine.Rendering.UI.DebugUIHandlerWidget GetLastItem()
		{
			if (contentHolder.childCount == 0)
			{
				return null;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.UI.DebugUIHandlerWidget> activeChildren = GetActiveChildren();
			if (activeChildren.Count == 0)
			{
				return null;
			}
			return activeChildren[activeChildren.Count - 1];
		}

		internal bool IsDirectChild(global::UnityEngine.Rendering.UI.DebugUIHandlerWidget widget)
		{
			if (contentHolder.childCount == 0)
			{
				return false;
			}
			return global::System.Linq.Enumerable.Count(GetActiveChildren(), (global::UnityEngine.Rendering.UI.DebugUIHandlerWidget x) => x == widget) > 0;
		}

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.UI.DebugUIHandlerWidget> GetActiveChildren()
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.UI.DebugUIHandlerWidget> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.UI.DebugUIHandlerWidget>();
			foreach (global::UnityEngine.Transform item in contentHolder)
			{
				if (item.gameObject.activeInHierarchy && item.TryGetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerWidget>(out var component))
				{
					list.Add(component);
				}
			}
			return list;
		}
	}
}
