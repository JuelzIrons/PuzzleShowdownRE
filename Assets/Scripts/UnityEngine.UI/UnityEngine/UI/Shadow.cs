namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Effects/Shadow", 80)]
	public class Shadow : global::UnityEngine.UI.BaseMeshEffect
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_EffectColor = new global::UnityEngine.Color(0f, 0f, 0f, 0.5f);

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2 m_EffectDistance = new global::UnityEngine.Vector2(1f, -1f);

		[global::UnityEngine.SerializeField]
		private bool m_UseGraphicAlpha = true;

		private const float kMaxEffectDistance = 600f;

		public global::UnityEngine.Color effectColor
		{
			get
			{
				return m_EffectColor;
			}
			set
			{
				m_EffectColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		public global::UnityEngine.Vector2 effectDistance
		{
			get
			{
				return m_EffectDistance;
			}
			set
			{
				if (value.x > 600f)
				{
					value.x = 600f;
				}
				if (value.x < -600f)
				{
					value.x = -600f;
				}
				if (value.y > 600f)
				{
					value.y = 600f;
				}
				if (value.y < -600f)
				{
					value.y = -600f;
				}
				if (!(m_EffectDistance == value))
				{
					m_EffectDistance = value;
					if (base.graphic != null)
					{
						base.graphic.SetVerticesDirty();
					}
				}
			}
		}

		public bool useGraphicAlpha
		{
			get
			{
				return m_UseGraphicAlpha;
			}
			set
			{
				m_UseGraphicAlpha = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		protected Shadow()
		{
		}

		protected void ApplyShadowZeroAlloc(global::System.Collections.Generic.List<global::UnityEngine.UIVertex> verts, global::UnityEngine.Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count + end - start;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			for (int i = start; i < end; i++)
			{
				global::UnityEngine.UIVertex uIVertex = verts[i];
				verts.Add(uIVertex);
				global::UnityEngine.Vector3 position = uIVertex.position;
				position.x += x;
				position.y += y;
				uIVertex.position = position;
				global::UnityEngine.Color32 color2 = color;
				if (m_UseGraphicAlpha)
				{
					color2.a = (byte)(color2.a * verts[i].color.a / 255);
				}
				uIVertex.color = color2;
				verts[i] = uIVertex;
			}
		}

		protected void ApplyShadow(global::System.Collections.Generic.List<global::UnityEngine.UIVertex> verts, global::UnityEngine.Color32 color, int start, int end, float x, float y)
		{
			ApplyShadowZeroAlloc(verts, color, start, end, x, y);
		}

		public override void ModifyMesh(global::UnityEngine.UI.VertexHelper vh)
		{
			if (IsActive())
			{
				global::System.Collections.Generic.List<global::UnityEngine.UIVertex> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UIVertex>, global::UnityEngine.UIVertex>.Get();
				vh.GetUIVertexStream(list);
				ApplyShadow(list, effectColor, 0, list.Count, effectDistance.x, effectDistance.y);
				vh.Clear();
				vh.AddUIVertexTriangleStream(list);
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UIVertex>, global::UnityEngine.UIVertex>.Release(list);
			}
		}
	}
}
