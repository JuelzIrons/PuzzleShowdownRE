namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerWidget : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.Color colorDefault = new global::UnityEngine.Color(0.8f, 0.8f, 0.8f, 1f);

		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.Color colorSelected = new global::UnityEngine.Color(0.25f, 0.65f, 0.8f, 1f);

		protected global::UnityEngine.Rendering.DebugUI.Widget m_Widget;

		public global::UnityEngine.Rendering.UI.DebugUIHandlerWidget parentUIHandler { get; set; }

		public global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previousUIHandler { get; set; }

		public global::UnityEngine.Rendering.UI.DebugUIHandlerWidget nextUIHandler { get; set; }

		protected virtual void OnEnable()
		{
		}

		internal virtual void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			m_Widget = widget;
		}

		internal global::UnityEngine.Rendering.DebugUI.Widget GetWidget()
		{
			return m_Widget;
		}

		protected T CastWidget<T>() where T : global::UnityEngine.Rendering.DebugUI.Widget
		{
			T obj = m_Widget as T;
			string text = ((m_Widget == null) ? "null" : m_Widget.GetType().ToString());
			if (obj == null)
			{
				throw new global::System.InvalidOperationException("Can't cast " + text + " to " + typeof(T));
			}
			return obj;
		}

		public virtual bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			return true;
		}

		public virtual void OnDeselection()
		{
		}

		public virtual void OnAction()
		{
		}

		public virtual void OnIncrement(bool fast)
		{
		}

		public virtual void OnDecrement(bool fast)
		{
		}

		public virtual global::UnityEngine.Rendering.UI.DebugUIHandlerWidget Previous()
		{
			if (!(previousUIHandler != null))
			{
				return parentUIHandler;
			}
			return previousUIHandler;
		}

		public virtual global::UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
		{
			if (nextUIHandler != null)
			{
				return nextUIHandler;
			}
			if (parentUIHandler != null)
			{
				global::UnityEngine.Rendering.UI.DebugUIHandlerWidget debugUIHandlerWidget = parentUIHandler;
				while (debugUIHandlerWidget != null)
				{
					global::UnityEngine.Rendering.UI.DebugUIHandlerWidget debugUIHandlerWidget2 = debugUIHandlerWidget.nextUIHandler;
					if (debugUIHandlerWidget2 != null)
					{
						return debugUIHandlerWidget2;
					}
					debugUIHandlerWidget = debugUIHandlerWidget.parentUIHandler;
				}
			}
			return null;
		}
	}
}
