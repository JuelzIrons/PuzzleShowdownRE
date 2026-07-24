namespace UnityEngine.UI
{
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	[global::UnityEngine.ExecuteAlways]
	public abstract class Graphic : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.UI.ICanvasElement
	{
		protected static global::UnityEngine.Material s_DefaultUI = null;

		protected static global::UnityEngine.Texture2D s_WhiteTexture = null;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_Mat")]
		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.Material m_Material;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_Color = global::UnityEngine.Color.white;

		[global::System.NonSerialized]
		protected bool m_SkipLayoutUpdate;

		[global::System.NonSerialized]
		protected bool m_SkipMaterialUpdate;

		[global::UnityEngine.SerializeField]
		private bool m_RaycastTarget = true;

		private bool m_RaycastTargetCache = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector4 m_RaycastPadding;

		[global::System.NonSerialized]
		private global::UnityEngine.RectTransform m_RectTransform;

		[global::System.NonSerialized]
		private global::UnityEngine.CanvasRenderer m_CanvasRenderer;

		[global::System.NonSerialized]
		private global::UnityEngine.Canvas m_Canvas;

		[global::System.NonSerialized]
		private bool m_VertsDirty;

		[global::System.NonSerialized]
		private bool m_MaterialDirty;

		[global::System.NonSerialized]
		protected global::UnityEngine.Events.UnityAction m_OnDirtyLayoutCallback;

		[global::System.NonSerialized]
		protected global::UnityEngine.Events.UnityAction m_OnDirtyVertsCallback;

		[global::System.NonSerialized]
		protected global::UnityEngine.Events.UnityAction m_OnDirtyMaterialCallback;

		[global::System.NonSerialized]
		protected static global::UnityEngine.Mesh s_Mesh;

		[global::System.NonSerialized]
		private static readonly global::UnityEngine.UI.VertexHelper s_VertexHelper = new global::UnityEngine.UI.VertexHelper();

		[global::System.NonSerialized]
		protected global::UnityEngine.Mesh m_CachedMesh;

		[global::System.NonSerialized]
		protected global::UnityEngine.Vector2[] m_CachedUvs;

		[global::System.NonSerialized]
		private readonly global::UnityEngine.UI.CoroutineTween.TweenRunner<global::UnityEngine.UI.CoroutineTween.ColorTween> m_ColorTweenRunner;

		public static global::UnityEngine.Material defaultGraphicMaterial
		{
			get
			{
				if (s_DefaultUI == null)
				{
					s_DefaultUI = global::UnityEngine.Canvas.GetDefaultCanvasMaterial();
				}
				return s_DefaultUI;
			}
		}

		public virtual global::UnityEngine.Color color
		{
			get
			{
				return m_Color;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetColor(ref m_Color, value))
				{
					SetVerticesDirty();
				}
			}
		}

		public virtual bool raycastTarget
		{
			get
			{
				return m_RaycastTarget;
			}
			set
			{
				if (value != m_RaycastTarget)
				{
					if (m_RaycastTarget)
					{
						global::UnityEngine.UI.GraphicRegistry.UnregisterRaycastGraphicForCanvas(canvas, this);
					}
					m_RaycastTarget = value;
					if (m_RaycastTarget && base.isActiveAndEnabled)
					{
						global::UnityEngine.UI.GraphicRegistry.RegisterRaycastGraphicForCanvas(canvas, this);
					}
				}
				m_RaycastTargetCache = value;
			}
		}

		public global::UnityEngine.Vector4 raycastPadding
		{
			get
			{
				return m_RaycastPadding;
			}
			set
			{
				m_RaycastPadding = value;
			}
		}

		protected bool useLegacyMeshGeneration { get; set; }

		public int depth => canvasRenderer.absoluteDepth;

		public global::UnityEngine.RectTransform rectTransform
		{
			get
			{
				if ((object)m_RectTransform == null)
				{
					m_RectTransform = GetComponent<global::UnityEngine.RectTransform>();
				}
				return m_RectTransform;
			}
		}

		public global::UnityEngine.Canvas canvas
		{
			get
			{
				if (m_Canvas == null)
				{
					CacheCanvas();
				}
				return m_Canvas;
			}
		}

		public global::UnityEngine.CanvasRenderer canvasRenderer
		{
			get
			{
				if ((object)m_CanvasRenderer == null)
				{
					m_CanvasRenderer = GetComponent<global::UnityEngine.CanvasRenderer>();
					if ((object)m_CanvasRenderer == null)
					{
						m_CanvasRenderer = base.gameObject.AddComponent<global::UnityEngine.CanvasRenderer>();
					}
				}
				return m_CanvasRenderer;
			}
		}

		public virtual global::UnityEngine.Material defaultMaterial => defaultGraphicMaterial;

		public virtual global::UnityEngine.Material material
		{
			get
			{
				if (!(m_Material != null))
				{
					return defaultMaterial;
				}
				return m_Material;
			}
			set
			{
				if (!(m_Material == value))
				{
					m_Material = value;
					SetMaterialDirty();
				}
			}
		}

		public virtual global::UnityEngine.Material materialForRendering
		{
			get
			{
				global::System.Collections.Generic.List<global::UnityEngine.UI.IMaterialModifier> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UI.IMaterialModifier>, global::UnityEngine.UI.IMaterialModifier>.Get();
				GetComponents(list);
				global::UnityEngine.Material modifiedMaterial = material;
				for (int i = 0; i < list.Count; i++)
				{
					modifiedMaterial = list[i].GetModifiedMaterial(modifiedMaterial);
				}
				global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.UI.IMaterialModifier>, global::UnityEngine.UI.IMaterialModifier>.Release(list);
				return modifiedMaterial;
			}
		}

		public virtual global::UnityEngine.Texture mainTexture => s_WhiteTexture;

		protected static global::UnityEngine.Mesh workerMesh
		{
			get
			{
				if (s_Mesh == null)
				{
					s_Mesh = new global::UnityEngine.Mesh();
					s_Mesh.name = "Shared UI Mesh";
				}
				return s_Mesh;
			}
		}

		global::UnityEngine.Transform global::UnityEngine.UI.ICanvasElement.transform => base.transform;

		protected Graphic()
		{
			if (m_ColorTweenRunner == null)
			{
				m_ColorTweenRunner = new global::UnityEngine.UI.CoroutineTween.TweenRunner<global::UnityEngine.UI.CoroutineTween.ColorTween>();
			}
			m_ColorTweenRunner.Init(this);
			useLegacyMeshGeneration = true;
		}

		public virtual void SetAllDirty()
		{
			if (m_SkipLayoutUpdate)
			{
				m_SkipLayoutUpdate = false;
			}
			else
			{
				SetLayoutDirty();
			}
			if (m_SkipMaterialUpdate)
			{
				m_SkipMaterialUpdate = false;
			}
			else
			{
				SetMaterialDirty();
			}
			SetVerticesDirty();
			SetRaycastDirty();
		}

		public virtual void SetLayoutDirty()
		{
			if (IsActive())
			{
				global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
				if (m_OnDirtyLayoutCallback != null)
				{
					m_OnDirtyLayoutCallback();
				}
			}
		}

		public virtual void SetVerticesDirty()
		{
			if (IsActive())
			{
				m_VertsDirty = true;
				global::UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
				if (m_OnDirtyVertsCallback != null)
				{
					m_OnDirtyVertsCallback();
				}
			}
		}

		public virtual void SetMaterialDirty()
		{
			if (IsActive())
			{
				m_MaterialDirty = true;
				global::UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
				if (m_OnDirtyMaterialCallback != null)
				{
					m_OnDirtyMaterialCallback();
				}
			}
		}

		public void SetRaycastDirty()
		{
			if (m_RaycastTargetCache != m_RaycastTarget)
			{
				if (m_RaycastTarget && base.isActiveAndEnabled)
				{
					global::UnityEngine.UI.GraphicRegistry.RegisterRaycastGraphicForCanvas(canvas, this);
				}
				else if (!m_RaycastTarget)
				{
					global::UnityEngine.UI.GraphicRegistry.UnregisterRaycastGraphicForCanvas(canvas, this);
				}
			}
			m_RaycastTargetCache = m_RaycastTarget;
		}

		protected override void OnRectTransformDimensionsChange()
		{
			if (base.gameObject.activeInHierarchy)
			{
				if (global::UnityEngine.UI.CanvasUpdateRegistry.IsRebuildingLayout())
				{
					SetVerticesDirty();
					return;
				}
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}

		protected override void OnBeforeTransformParentChanged()
		{
			global::UnityEngine.UI.GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
			global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
		}

		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			m_Canvas = null;
			if (IsActive())
			{
				CacheCanvas();
				global::UnityEngine.UI.GraphicRegistry.RegisterGraphicForCanvas(canvas, this);
				SetAllDirty();
			}
		}

		private void CacheCanvas()
		{
			global::System.Collections.Generic.List<global::UnityEngine.Canvas> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Get();
			base.gameObject.GetComponentsInParent(includeInactive: false, list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].isActiveAndEnabled)
					{
						m_Canvas = list[i];
						break;
					}
					if (i == list.Count - 1)
					{
						m_Canvas = null;
					}
				}
			}
			else
			{
				m_Canvas = null;
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Release(list);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			CacheCanvas();
			global::UnityEngine.UI.GraphicRegistry.RegisterGraphicForCanvas(canvas, this);
			if (s_WhiteTexture == null)
			{
				s_WhiteTexture = global::UnityEngine.Texture2D.whiteTexture;
			}
			SetAllDirty();
		}

		protected override void OnDisable()
		{
			global::UnityEngine.UI.GraphicRegistry.DisableGraphicForCanvas(canvas, this);
			global::UnityEngine.UI.CanvasUpdateRegistry.DisableCanvasElementForRebuild(this);
			if (canvasRenderer != null)
			{
				canvasRenderer.Clear();
			}
			global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			base.OnDisable();
		}

		protected override void OnDestroy()
		{
			global::UnityEngine.UI.GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
			global::UnityEngine.UI.CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if ((bool)m_CachedMesh)
			{
				global::UnityEngine.Object.Destroy(m_CachedMesh);
			}
			m_CachedMesh = null;
			base.OnDestroy();
		}

		protected override void OnCanvasHierarchyChanged()
		{
			global::UnityEngine.Canvas canvas = m_Canvas;
			m_Canvas = null;
			if (!IsActive())
			{
				global::UnityEngine.UI.GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
				return;
			}
			CacheCanvas();
			if (canvas != m_Canvas)
			{
				global::UnityEngine.UI.GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
				if (IsActive())
				{
					global::UnityEngine.UI.GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
				}
			}
		}

		public virtual void OnCullingChanged()
		{
			if (!canvasRenderer.cull && (m_VertsDirty || m_MaterialDirty))
			{
				global::UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			}
		}

		public virtual void Rebuild(global::UnityEngine.UI.CanvasUpdate update)
		{
			if (!(canvasRenderer == null) && !canvasRenderer.cull && update == global::UnityEngine.UI.CanvasUpdate.PreRender)
			{
				if (m_VertsDirty)
				{
					UpdateGeometry();
					m_VertsDirty = false;
				}
				if (m_MaterialDirty)
				{
					UpdateMaterial();
					m_MaterialDirty = false;
				}
			}
		}

		public virtual void LayoutComplete()
		{
		}

		public virtual void GraphicUpdateComplete()
		{
		}

		protected virtual void UpdateMaterial()
		{
			if (IsActive())
			{
				canvasRenderer.materialCount = 1;
				canvasRenderer.SetMaterial(materialForRendering, 0);
				canvasRenderer.SetTexture(mainTexture);
			}
		}

		protected virtual void UpdateGeometry()
		{
			if (useLegacyMeshGeneration)
			{
				DoLegacyMeshGeneration();
			}
			else
			{
				DoMeshGeneration();
			}
		}

		private void DoMeshGeneration()
		{
			if (rectTransform != null && rectTransform.rect.width >= 0f && rectTransform.rect.height >= 0f)
			{
				OnPopulateMesh(s_VertexHelper);
			}
			else
			{
				s_VertexHelper.Clear();
			}
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			GetComponents(typeof(global::UnityEngine.UI.IMeshModifier), list);
			for (int i = 0; i < list.Count; i++)
			{
				((global::UnityEngine.UI.IMeshModifier)list[i]).ModifyMesh(s_VertexHelper);
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
			s_VertexHelper.FillMesh(workerMesh);
			canvasRenderer.SetMesh(workerMesh);
		}

		private void DoLegacyMeshGeneration()
		{
			if (rectTransform != null && rectTransform.rect.width >= 0f && rectTransform.rect.height >= 0f)
			{
				OnPopulateMesh(workerMesh);
			}
			else
			{
				workerMesh.Clear();
			}
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			GetComponents(typeof(global::UnityEngine.UI.IMeshModifier), list);
			for (int i = 0; i < list.Count; i++)
			{
				((global::UnityEngine.UI.IMeshModifier)list[i]).ModifyMesh(workerMesh);
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
			canvasRenderer.SetMesh(workerMesh);
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("Use OnPopulateMesh instead.", true)]
		protected virtual void OnFillVBO(global::System.Collections.Generic.List<global::UnityEngine.UIVertex> vbo)
		{
		}

		[global::System.Obsolete("Use OnPopulateMesh(VertexHelper vh) instead.", false)]
		protected virtual void OnPopulateMesh(global::UnityEngine.Mesh m)
		{
			OnPopulateMesh(s_VertexHelper);
			s_VertexHelper.FillMesh(m);
		}

		protected virtual void OnPopulateMesh(global::UnityEngine.UI.VertexHelper vh)
		{
			global::UnityEngine.Rect pixelAdjustedRect = GetPixelAdjustedRect();
			global::UnityEngine.Vector4 vector = new global::UnityEngine.Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
			global::UnityEngine.Color32 color = this.color;
			vh.Clear();
			vh.AddVert(new global::UnityEngine.Vector3(vector.x, vector.y), color, new global::UnityEngine.Vector2(0f, 0f));
			vh.AddVert(new global::UnityEngine.Vector3(vector.x, vector.w), color, new global::UnityEngine.Vector2(0f, 1f));
			vh.AddVert(new global::UnityEngine.Vector3(vector.z, vector.w), color, new global::UnityEngine.Vector2(1f, 1f));
			vh.AddVert(new global::UnityEngine.Vector3(vector.z, vector.y), color, new global::UnityEngine.Vector2(1f, 0f));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}

		protected override void OnDidApplyAnimationProperties()
		{
			SetAllDirty();
		}

		public virtual void SetNativeSize()
		{
		}

		public virtual bool Raycast(global::UnityEngine.Vector2 sp, global::UnityEngine.Camera eventCamera)
		{
			return Raycast(sp, eventCamera, ignoreMasks: false);
		}

		protected bool Raycast(global::UnityEngine.Vector2 sp, global::UnityEngine.Camera eventCamera, bool ignoreMasks)
		{
			if (!base.isActiveAndEnabled)
			{
				return false;
			}
			global::UnityEngine.Transform transform = base.transform;
			global::System.Collections.Generic.List<global::UnityEngine.Component> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Get();
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			while (transform != null)
			{
				bool flag4 = true;
				bool flag5 = false;
				bool flag6 = true;
				transform.GetComponents(list);
				for (int i = 0; i < list.Count; i++)
				{
					global::UnityEngine.Component component = list[i];
					global::UnityEngine.Canvas canvas = component as global::UnityEngine.Canvas;
					if (canvas != null && canvas.overrideSorting)
					{
						flag2 = false;
					}
					if (!(component is global::UnityEngine.ICanvasRaycastFilter canvasRaycastFilter) || (ignoreMasks && (component is global::UnityEngine.UI.Mask || component is global::UnityEngine.UI.RectMask2D)))
					{
						continue;
					}
					if (component is global::UnityEngine.CanvasGroup canvasGroup)
					{
						if (canvasGroup.enabled && !flag)
						{
							if (canvasGroup.ignoreParentGroups)
							{
								flag = true;
							}
							flag4 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
							if (!flag4)
							{
								break;
							}
						}
					}
					else
					{
						if (flag3 && component is global::UnityEngine.UI.Graphic { raycastTarget: false })
						{
							continue;
						}
						flag5 = flag5 || component is global::UnityEngine.UI.Mask;
						flag4 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
						if (!flag4)
						{
							if (!flag3 || !(component is global::UnityEngine.UI.MaskableGraphic))
							{
								break;
							}
							flag6 = flag4;
							if (!ignoreMasks && flag5)
							{
								break;
							}
							flag4 = true;
						}
					}
				}
				if (!flag4 || (flag5 && !flag6))
				{
					global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
					return false;
				}
				transform = (flag2 ? transform.parent : null);
				flag3 = true;
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Component>, global::UnityEngine.Component>.Release(list);
			return true;
		}

		public global::UnityEngine.Vector2 PixelAdjustPoint(global::UnityEngine.Vector2 point)
		{
			if (!canvas || canvas.renderMode == global::UnityEngine.RenderMode.WorldSpace || canvas.scaleFactor == 0f || !canvas.pixelPerfect)
			{
				return point;
			}
			return global::UnityEngine.RectTransformUtility.PixelAdjustPoint(point, base.transform, canvas);
		}

		public global::UnityEngine.Rect GetPixelAdjustedRect()
		{
			if (!canvas || canvas.renderMode == global::UnityEngine.RenderMode.WorldSpace || canvas.scaleFactor == 0f || !canvas.pixelPerfect)
			{
				return rectTransform.rect;
			}
			return global::UnityEngine.RectTransformUtility.PixelAdjustRect(rectTransform, canvas);
		}

		public virtual void CrossFadeColor(global::UnityEngine.Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, useRGB: true);
		}

		public virtual void CrossFadeColor(global::UnityEngine.Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
		{
			if (!(canvasRenderer == null) && (useRGB || useAlpha))
			{
				if (canvasRenderer.GetColor().Equals(targetColor))
				{
					m_ColorTweenRunner.StopTween();
					return;
				}
				global::UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenMode tweenMode = ((!(useRGB && useAlpha)) ? (useRGB ? global::UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenMode.RGB : global::UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenMode.Alpha) : global::UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenMode.All);
				global::UnityEngine.UI.CoroutineTween.ColorTween info = new global::UnityEngine.UI.CoroutineTween.ColorTween
				{
					duration = duration,
					startColor = canvasRenderer.GetColor(),
					targetColor = targetColor
				};
				info.AddOnChangedCallback(canvasRenderer.SetColor);
				info.ignoreTimeScale = ignoreTimeScale;
				info.tweenMode = tweenMode;
				m_ColorTweenRunner.StartTween(info);
			}
		}

		private static global::UnityEngine.Color CreateColorFromAlpha(float alpha)
		{
			global::UnityEngine.Color black = global::UnityEngine.Color.black;
			black.a = alpha;
			return black;
		}

		public virtual void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			CrossFadeColor(CreateColorFromAlpha(alpha), duration, ignoreTimeScale, useAlpha: true, useRGB: false);
		}

		public void RegisterDirtyLayoutCallback(global::UnityEngine.Events.UnityAction action)
		{
			m_OnDirtyLayoutCallback = (global::UnityEngine.Events.UnityAction)global::System.Delegate.Combine(m_OnDirtyLayoutCallback, action);
		}

		public void UnregisterDirtyLayoutCallback(global::UnityEngine.Events.UnityAction action)
		{
			m_OnDirtyLayoutCallback = (global::UnityEngine.Events.UnityAction)global::System.Delegate.Remove(m_OnDirtyLayoutCallback, action);
		}

		public void RegisterDirtyVerticesCallback(global::UnityEngine.Events.UnityAction action)
		{
			m_OnDirtyVertsCallback = (global::UnityEngine.Events.UnityAction)global::System.Delegate.Combine(m_OnDirtyVertsCallback, action);
		}

		public void UnregisterDirtyVerticesCallback(global::UnityEngine.Events.UnityAction action)
		{
			m_OnDirtyVertsCallback = (global::UnityEngine.Events.UnityAction)global::System.Delegate.Remove(m_OnDirtyVertsCallback, action);
		}

		public void RegisterDirtyMaterialCallback(global::UnityEngine.Events.UnityAction action)
		{
			m_OnDirtyMaterialCallback = (global::UnityEngine.Events.UnityAction)global::System.Delegate.Combine(m_OnDirtyMaterialCallback, action);
		}

		public void UnregisterDirtyMaterialCallback(global::UnityEngine.Events.UnityAction action)
		{
			m_OnDirtyMaterialCallback = (global::UnityEngine.Events.UnityAction)global::System.Delegate.Remove(m_OnDirtyMaterialCallback, action);
		}
	}
}
