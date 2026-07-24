namespace UnityEngine.UI
{
	public class GraphicRegistry
	{
		private static global::UnityEngine.UI.GraphicRegistry s_Instance;

		private readonly global::System.Collections.Generic.Dictionary<global::UnityEngine.Canvas, global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.Graphic>> m_Graphics = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Canvas, global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.Graphic>>();

		private readonly global::System.Collections.Generic.Dictionary<global::UnityEngine.Canvas, global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.Graphic>> m_RaycastableGraphics = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Canvas, global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.Graphic>>();

		private static readonly global::System.Collections.Generic.List<global::UnityEngine.UI.Graphic> s_EmptyList = new global::System.Collections.Generic.List<global::UnityEngine.UI.Graphic>();

		public static global::UnityEngine.UI.GraphicRegistry instance
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = new global::UnityEngine.UI.GraphicRegistry();
				}
				return s_Instance;
			}
		}

		protected GraphicRegistry()
		{
			global::System.GC.KeepAlive(new global::System.Collections.Generic.Dictionary<global::UnityEngine.UI.Graphic, int>());
			global::System.GC.KeepAlive(new global::System.Collections.Generic.Dictionary<global::UnityEngine.UI.ICanvasElement, int>());
			global::System.GC.KeepAlive(new global::System.Collections.Generic.Dictionary<global::UnityEngine.UI.IClipper, int>());
		}

		public static void RegisterGraphicForCanvas(global::UnityEngine.Canvas c, global::UnityEngine.UI.Graphic graphic)
		{
			if (!(c == null) && !(graphic == null))
			{
				instance.m_Graphics.TryGetValue(c, out var value);
				if (value != null)
				{
					value.AddUnique(graphic);
					RegisterRaycastGraphicForCanvas(c, graphic);
					return;
				}
				value = new global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.Graphic>();
				value.Add(graphic);
				instance.m_Graphics.Add(c, value);
				RegisterRaycastGraphicForCanvas(c, graphic);
			}
		}

		public static void RegisterRaycastGraphicForCanvas(global::UnityEngine.Canvas c, global::UnityEngine.UI.Graphic graphic)
		{
			if (!(c == null) && !(graphic == null) && graphic.raycastTarget)
			{
				instance.m_RaycastableGraphics.TryGetValue(c, out var value);
				if (value != null)
				{
					value.AddUnique(graphic);
					return;
				}
				value = new global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.Graphic>();
				value.Add(graphic);
				instance.m_RaycastableGraphics.Add(c, value);
			}
		}

		public static void UnregisterGraphicForCanvas(global::UnityEngine.Canvas c, global::UnityEngine.UI.Graphic graphic)
		{
			if (!(c == null) && !(graphic == null) && instance.m_Graphics.TryGetValue(c, out var value))
			{
				value.Remove(graphic);
				if (value.Capacity == 0)
				{
					instance.m_Graphics.Remove(c);
				}
				UnregisterRaycastGraphicForCanvas(c, graphic);
			}
		}

		public static void UnregisterRaycastGraphicForCanvas(global::UnityEngine.Canvas c, global::UnityEngine.UI.Graphic graphic)
		{
			if (!(c == null) && !(graphic == null) && instance.m_RaycastableGraphics.TryGetValue(c, out var value))
			{
				value.Remove(graphic);
				if (value.Count == 0)
				{
					instance.m_RaycastableGraphics.Remove(c);
				}
			}
		}

		public static void DisableGraphicForCanvas(global::UnityEngine.Canvas c, global::UnityEngine.UI.Graphic graphic)
		{
			if (!(c == null) && instance.m_Graphics.TryGetValue(c, out var value))
			{
				value.DisableItem(graphic);
				if (value.Capacity == 0)
				{
					instance.m_Graphics.Remove(c);
				}
				DisableRaycastGraphicForCanvas(c, graphic);
			}
		}

		public static void DisableRaycastGraphicForCanvas(global::UnityEngine.Canvas c, global::UnityEngine.UI.Graphic graphic)
		{
			if (!(c == null) && graphic.raycastTarget && instance.m_RaycastableGraphics.TryGetValue(c, out var value))
			{
				value.DisableItem(graphic);
				if (value.Capacity == 0)
				{
					instance.m_RaycastableGraphics.Remove(c);
				}
			}
		}

		public static global::System.Collections.Generic.IList<global::UnityEngine.UI.Graphic> GetGraphicsForCanvas(global::UnityEngine.Canvas canvas)
		{
			if (instance.m_Graphics.TryGetValue(canvas, out var value))
			{
				return value;
			}
			return s_EmptyList;
		}

		public static global::System.Collections.Generic.IList<global::UnityEngine.UI.Graphic> GetRaycastableGraphicsForCanvas(global::UnityEngine.Canvas canvas)
		{
			if (instance.m_RaycastableGraphics.TryGetValue(canvas, out var value))
			{
				return value;
			}
			return s_EmptyList;
		}
	}
}
