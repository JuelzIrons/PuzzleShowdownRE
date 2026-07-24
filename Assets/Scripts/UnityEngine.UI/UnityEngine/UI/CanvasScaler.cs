namespace UnityEngine.UI
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Canvas))]
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.AddComponentMenu("Layout/Canvas Scaler", 101)]
	[global::UnityEngine.DisallowMultipleComponent]
	public class CanvasScaler : global::UnityEngine.EventSystems.UIBehaviour
	{
		public enum ScaleMode
		{
			ConstantPixelSize = 0,
			ScaleWithScreenSize = 1,
			ConstantPhysicalSize = 2
		}

		public enum ScreenMatchMode
		{
			MatchWidthOrHeight = 0,
			Expand = 1,
			Shrink = 2
		}

		public enum Unit
		{
			Centimeters = 0,
			Millimeters = 1,
			Inches = 2,
			Points = 3,
			Picas = 4
		}

		[global::UnityEngine.Tooltip("Determines how UI elements in the Canvas are scaled.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.CanvasScaler.ScaleMode m_UiScaleMode;

		[global::UnityEngine.Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		[global::UnityEngine.SerializeField]
		protected float m_ReferencePixelsPerUnit = 100f;

		[global::UnityEngine.Tooltip("Scales all UI elements in the Canvas by this factor.")]
		[global::UnityEngine.SerializeField]
		protected float m_ScaleFactor = 1f;

		[global::UnityEngine.Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it's smaller, the UI will be scaled down. This is done in accordance with the Screen Match Mode.")]
		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.Vector2 m_ReferenceResolution = new global::UnityEngine.Vector2(800f, 600f);

		[global::UnityEngine.Tooltip("A mode used to scale the canvas area if the aspect ratio of the current resolution doesn't fit the reference resolution.")]
		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.UI.CanvasScaler.ScreenMatchMode m_ScreenMatchMode;

		[global::UnityEngine.Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		protected float m_MatchWidthOrHeight;

		private const float kLogBase = 2f;

		[global::UnityEngine.Tooltip("The physical unit to specify positions and sizes in.")]
		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.UI.CanvasScaler.Unit m_PhysicalUnit = global::UnityEngine.UI.CanvasScaler.Unit.Points;

		[global::UnityEngine.Tooltip("The DPI to assume if the screen DPI is not known.")]
		[global::UnityEngine.SerializeField]
		protected float m_FallbackScreenDPI = 96f;

		[global::UnityEngine.Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		[global::UnityEngine.SerializeField]
		protected float m_DefaultSpriteDPI = 96f;

		[global::UnityEngine.Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		[global::UnityEngine.SerializeField]
		protected float m_DynamicPixelsPerUnit = 1f;

		private global::UnityEngine.Canvas m_Canvas;

		[global::System.NonSerialized]
		private float m_PrevScaleFactor = 1f;

		[global::System.NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100f;

		[global::UnityEngine.SerializeField]
		protected bool m_PresetInfoIsWorld;

		public global::UnityEngine.UI.CanvasScaler.ScaleMode uiScaleMode
		{
			get
			{
				return m_UiScaleMode;
			}
			set
			{
				m_UiScaleMode = value;
			}
		}

		public float referencePixelsPerUnit
		{
			get
			{
				return m_ReferencePixelsPerUnit;
			}
			set
			{
				m_ReferencePixelsPerUnit = value;
			}
		}

		public float scaleFactor
		{
			get
			{
				return m_ScaleFactor;
			}
			set
			{
				m_ScaleFactor = global::UnityEngine.Mathf.Max(0.01f, value);
			}
		}

		public global::UnityEngine.Vector2 referenceResolution
		{
			get
			{
				return m_ReferenceResolution;
			}
			set
			{
				m_ReferenceResolution = value;
				if (m_ReferenceResolution.x > -1E-05f && m_ReferenceResolution.x < 1E-05f)
				{
					m_ReferenceResolution.x = 1E-05f * global::UnityEngine.Mathf.Sign(m_ReferenceResolution.x);
				}
				if (m_ReferenceResolution.y > -1E-05f && m_ReferenceResolution.y < 1E-05f)
				{
					m_ReferenceResolution.y = 1E-05f * global::UnityEngine.Mathf.Sign(m_ReferenceResolution.y);
				}
			}
		}

		public global::UnityEngine.UI.CanvasScaler.ScreenMatchMode screenMatchMode
		{
			get
			{
				return m_ScreenMatchMode;
			}
			set
			{
				m_ScreenMatchMode = value;
			}
		}

		public float matchWidthOrHeight
		{
			get
			{
				return m_MatchWidthOrHeight;
			}
			set
			{
				m_MatchWidthOrHeight = value;
			}
		}

		public global::UnityEngine.UI.CanvasScaler.Unit physicalUnit
		{
			get
			{
				return m_PhysicalUnit;
			}
			set
			{
				m_PhysicalUnit = value;
			}
		}

		public float fallbackScreenDPI
		{
			get
			{
				return m_FallbackScreenDPI;
			}
			set
			{
				m_FallbackScreenDPI = value;
			}
		}

		public float defaultSpriteDPI
		{
			get
			{
				return m_DefaultSpriteDPI;
			}
			set
			{
				m_DefaultSpriteDPI = global::UnityEngine.Mathf.Max(1f, value);
			}
		}

		public float dynamicPixelsPerUnit
		{
			get
			{
				return m_DynamicPixelsPerUnit;
			}
			set
			{
				m_DynamicPixelsPerUnit = value;
			}
		}

		protected CanvasScaler()
		{
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_Canvas = GetComponent<global::UnityEngine.Canvas>();
			Handle();
			global::UnityEngine.Canvas.preWillRenderCanvases += Canvas_preWillRenderCanvases;
		}

		private void Canvas_preWillRenderCanvases()
		{
			Handle();
		}

		protected override void OnDisable()
		{
			SetScaleFactor(1f);
			SetReferencePixelsPerUnit(100f);
			global::UnityEngine.Canvas.preWillRenderCanvases -= Canvas_preWillRenderCanvases;
			base.OnDisable();
		}

		protected virtual void Handle()
		{
			if (m_Canvas == null || !m_Canvas.isRootCanvas)
			{
				return;
			}
			if (m_Canvas.renderMode == global::UnityEngine.RenderMode.WorldSpace)
			{
				HandleWorldCanvas();
				return;
			}
			switch (m_UiScaleMode)
			{
			case global::UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize:
				HandleConstantPixelSize();
				break;
			case global::UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize:
				HandleScaleWithScreenSize();
				break;
			case global::UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPhysicalSize:
				HandleConstantPhysicalSize();
				break;
			}
		}

		protected virtual void HandleWorldCanvas()
		{
			SetScaleFactor(m_DynamicPixelsPerUnit);
			SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit);
		}

		protected virtual void HandleConstantPixelSize()
		{
			SetScaleFactor(m_ScaleFactor);
			SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit);
		}

		protected virtual void HandleScaleWithScreenSize()
		{
			global::UnityEngine.Vector2 vector = m_Canvas.renderingDisplaySize;
			int targetDisplay = m_Canvas.targetDisplay;
			if (targetDisplay > 0 && targetDisplay < global::UnityEngine.Display.displays.Length)
			{
				global::UnityEngine.Display display = global::UnityEngine.Display.displays[targetDisplay];
				vector = new global::UnityEngine.Vector2(display.renderingWidth, display.renderingHeight);
			}
			float num = 0f;
			switch (m_ScreenMatchMode)
			{
			case global::UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
			{
				float a = global::UnityEngine.Mathf.Log(vector.x / m_ReferenceResolution.x, 2f);
				float b = global::UnityEngine.Mathf.Log(vector.y / m_ReferenceResolution.y, 2f);
				float p = global::UnityEngine.Mathf.Lerp(a, b, m_MatchWidthOrHeight);
				num = global::UnityEngine.Mathf.Pow(2f, p);
				break;
			}
			case global::UnityEngine.UI.CanvasScaler.ScreenMatchMode.Expand:
				num = global::UnityEngine.Mathf.Min(vector.x / m_ReferenceResolution.x, vector.y / m_ReferenceResolution.y);
				break;
			case global::UnityEngine.UI.CanvasScaler.ScreenMatchMode.Shrink:
				num = global::UnityEngine.Mathf.Max(vector.x / m_ReferenceResolution.x, vector.y / m_ReferenceResolution.y);
				break;
			}
			SetScaleFactor(num);
			SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit);
		}

		protected virtual void HandleConstantPhysicalSize()
		{
			float dpi = global::UnityEngine.Screen.dpi;
			float num = ((dpi == 0f) ? m_FallbackScreenDPI : dpi);
			float num2 = 1f;
			switch (m_PhysicalUnit)
			{
			case global::UnityEngine.UI.CanvasScaler.Unit.Centimeters:
				num2 = 2.54f;
				break;
			case global::UnityEngine.UI.CanvasScaler.Unit.Millimeters:
				num2 = 25.4f;
				break;
			case global::UnityEngine.UI.CanvasScaler.Unit.Inches:
				num2 = 1f;
				break;
			case global::UnityEngine.UI.CanvasScaler.Unit.Points:
				num2 = 72f;
				break;
			case global::UnityEngine.UI.CanvasScaler.Unit.Picas:
				num2 = 6f;
				break;
			}
			SetScaleFactor(num / num2);
			SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit * num2 / m_DefaultSpriteDPI);
		}

		protected void SetScaleFactor(float scaleFactor)
		{
			if (!(global::UnityEngine.Mathf.Abs(scaleFactor - m_PrevScaleFactor) < 5E-06f))
			{
				m_Canvas.scaleFactor = scaleFactor;
				m_PrevScaleFactor = scaleFactor;
			}
		}

		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit != m_PrevReferencePixelsPerUnit)
			{
				m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
				m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
			}
		}
	}
}
