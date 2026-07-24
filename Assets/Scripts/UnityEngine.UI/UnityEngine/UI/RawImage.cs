namespace UnityEngine.UI
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.CanvasRenderer))]
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Raw Image", 12)]
	public class RawImage : global::UnityEngine.UI.MaskableGraphic
	{
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_Tex")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Texture m_Texture;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rect m_UVRect = new global::UnityEngine.Rect(0f, 0f, 1f, 1f);

		public override global::UnityEngine.Texture mainTexture
		{
			get
			{
				if (m_Texture == null)
				{
					if (material != null && material.mainTexture != null)
					{
						return material.mainTexture;
					}
					return global::UnityEngine.UI.Graphic.s_WhiteTexture;
				}
				return m_Texture as global::UnityEngine.Texture;
			}
		}

		public global::UnityEngine.Texture texture
		{
			get
			{
				return m_Texture;
			}
			set
			{
				if (!(m_Texture == value))
				{
					m_Texture = value;
					SetVerticesDirty();
					SetMaterialDirty();
				}
			}
		}

		public global::UnityEngine.Rect uvRect
		{
			get
			{
				return m_UVRect;
			}
			set
			{
				if (!(m_UVRect == value))
				{
					m_UVRect = value;
					SetVerticesDirty();
				}
			}
		}

		protected RawImage()
		{
			base.useLegacyMeshGeneration = false;
		}

		public override void SetNativeSize()
		{
			global::UnityEngine.Texture texture = mainTexture;
			if (texture != null)
			{
				int num = global::UnityEngine.Mathf.RoundToInt((float)texture.width * uvRect.width);
				int num2 = global::UnityEngine.Mathf.RoundToInt((float)texture.height * uvRect.height);
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new global::UnityEngine.Vector2(num, num2);
			}
		}

		protected override void OnPopulateMesh(global::UnityEngine.UI.VertexHelper vh)
		{
			global::UnityEngine.Texture texture = mainTexture;
			vh.Clear();
			if (texture != null)
			{
				global::UnityEngine.Rect pixelAdjustedRect = GetPixelAdjustedRect();
				global::UnityEngine.Vector4 vector = new global::UnityEngine.Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
				global::UnityEngine.Vector2 texelSize = texture.texelSize;
				float num = (float)texture.width * texelSize.x;
				float num2 = (float)texture.height * texelSize.y;
				global::UnityEngine.Color32 color = this.color;
				vh.AddVert(new global::UnityEngine.Vector3(vector.x, vector.y), color, new global::UnityEngine.Vector4(m_UVRect.xMin * num, m_UVRect.yMin * num2));
				vh.AddVert(new global::UnityEngine.Vector3(vector.x, vector.w), color, new global::UnityEngine.Vector4(m_UVRect.xMin * num, m_UVRect.yMax * num2));
				vh.AddVert(new global::UnityEngine.Vector3(vector.z, vector.w), color, new global::UnityEngine.Vector4(m_UVRect.xMax * num, m_UVRect.yMax * num2));
				vh.AddVert(new global::UnityEngine.Vector3(vector.z, vector.y), color, new global::UnityEngine.Vector4(m_UVRect.xMax * num, m_UVRect.yMin * num2));
				vh.AddTriangle(0, 1, 2);
				vh.AddTriangle(2, 3, 0);
			}
		}

		protected override void OnDidApplyAnimationProperties()
		{
			SetMaterialDirty();
			SetVerticesDirty();
			SetRaycastDirty();
		}
	}
}
