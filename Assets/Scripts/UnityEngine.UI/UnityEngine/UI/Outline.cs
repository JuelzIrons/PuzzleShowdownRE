namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Effects/Outline", 81)]
	public class Outline : global::UnityEngine.UI.Shadow
	{
		protected Outline()
		{
		}

		public override void ModifyMesh(global::UnityEngine.UI.VertexHelper vh)
		{
			if (IsActive())
			{
				global::System.Collections.Generic.List<global::UnityEngine.UIVertex> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UIVertex>, global::UnityEngine.UIVertex>.Get();
				vh.GetUIVertexStream(list);
				int num = list.Count * 5;
				if (list.Capacity < num)
				{
					list.Capacity = num;
				}
				int start = 0;
				int count = list.Count;
				ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, base.effectDistance.x, base.effectDistance.y);
				start = count;
				int count2 = list.Count;
				ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, base.effectDistance.x, 0f - base.effectDistance.y);
				start = count2;
				int count3 = list.Count;
				ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, 0f - base.effectDistance.x, base.effectDistance.y);
				start = count3;
				_ = list.Count;
				ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, 0f - base.effectDistance.x, 0f - base.effectDistance.y);
				vh.Clear();
				vh.AddUIVertexTriangleStream(list);
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UIVertex>, global::UnityEngine.UIVertex>.Release(list);
			}
		}
	}
}
