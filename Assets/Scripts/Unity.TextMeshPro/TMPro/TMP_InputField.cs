namespace TMPro
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/TextMeshPro - Input Field", 11)]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/index.html")]
	public class TMP_InputField : global::UnityEngine.UI.Selectable, global::UnityEngine.EventSystems.IUpdateSelectedHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IBeginDragHandler, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IEndDragHandler, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.ISubmitHandler, global::UnityEngine.EventSystems.ICancelHandler, global::UnityEngine.UI.ICanvasElement, global::UnityEngine.UI.ILayoutElement, global::UnityEngine.EventSystems.IScrollHandler
	{
		public enum ContentType
		{
			Standard = 0,
			Autocorrected = 1,
			IntegerNumber = 2,
			DecimalNumber = 3,
			Alphanumeric = 4,
			Name = 5,
			EmailAddress = 6,
			Password = 7,
			Pin = 8,
			Custom = 9
		}

		public enum InputType
		{
			Standard = 0,
			AutoCorrect = 1,
			Password = 2
		}

		public enum CharacterValidation
		{
			None = 0,
			Digit = 1,
			Integer = 2,
			Decimal = 3,
			Alphanumeric = 4,
			Name = 5,
			Regex = 6,
			EmailAddress = 7,
			CustomValidator = 8
		}

		public enum LineType
		{
			SingleLine = 0,
			MultiLineSubmit = 1,
			MultiLineNewline = 2
		}

		public delegate char OnValidateInput(string text, int charIndex, char addedChar);

		[global::System.Serializable]
		public class SubmitEvent : global::UnityEngine.Events.UnityEvent<string>
		{
		}

		[global::System.Serializable]
		public class OnChangeEvent : global::UnityEngine.Events.UnityEvent<string>
		{
		}

		[global::System.Serializable]
		public class SelectionEvent : global::UnityEngine.Events.UnityEvent<string>
		{
		}

		[global::System.Serializable]
		public class TextSelectionEvent : global::UnityEngine.Events.UnityEvent<string, int, int>
		{
		}

		[global::System.Serializable]
		public class TouchScreenKeyboardEvent : global::UnityEngine.Events.UnityEvent<global::UnityEngine.TouchScreenKeyboard.Status>
		{
		}

		protected enum EditState
		{
			Continue = 0,
			Finish = 1
		}

		protected global::UnityEngine.TouchScreenKeyboard m_SoftKeyboard;

		private static readonly char[] kSeparators = new char[6] { ' ', '.', ',', '\t', '\r', '\n' };

		private static bool s_IsQuestDevice = false;

		protected global::UnityEngine.RectTransform m_RectTransform;

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.RectTransform m_TextViewport;

		protected global::UnityEngine.UI.RectMask2D m_TextComponentRectMask;

		protected global::UnityEngine.UI.RectMask2D m_TextViewportRectMask;

		[global::UnityEngine.SerializeField]
		protected global::TMPro.TMP_Text m_TextComponent;

		protected global::UnityEngine.RectTransform m_TextComponentRectTransform;

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.UI.Graphic m_Placeholder;

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.UI.Scrollbar m_VerticalScrollbar;

		[global::UnityEngine.SerializeField]
		protected global::TMPro.TMP_ScrollbarEventHandler m_VerticalScrollbarEventHandler;

		private bool m_IsDrivenByLayoutComponents;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.LayoutGroup m_LayoutGroup;

		private global::UnityEngine.EventSystems.IScrollHandler m_IScrollHandlerParent;

		private float m_ScrollPosition;

		[global::UnityEngine.SerializeField]
		protected float m_ScrollSensitivity = 1f;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.ContentType m_ContentType;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.InputType m_InputType;

		[global::UnityEngine.SerializeField]
		private char m_AsteriskChar = '*';

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.TouchScreenKeyboardType m_KeyboardType;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.LineType m_LineType;

		[global::UnityEngine.SerializeField]
		private bool m_HideMobileInput;

		[global::UnityEngine.SerializeField]
		private bool m_HideSoftKeyboard;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.CharacterValidation m_CharacterValidation;

		[global::UnityEngine.SerializeField]
		private string m_RegexValue = string.Empty;

		[global::UnityEngine.SerializeField]
		private float m_GlobalPointSize = 14f;

		[global::UnityEngine.SerializeField]
		private int m_CharacterLimit;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.SubmitEvent m_OnEndEdit = new global::TMPro.TMP_InputField.SubmitEvent();

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.SubmitEvent m_OnSubmit = new global::TMPro.TMP_InputField.SubmitEvent();

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.SelectionEvent m_OnSelect = new global::TMPro.TMP_InputField.SelectionEvent();

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.SelectionEvent m_OnDeselect = new global::TMPro.TMP_InputField.SelectionEvent();

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.TextSelectionEvent m_OnTextSelection = new global::TMPro.TMP_InputField.TextSelectionEvent();

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.TextSelectionEvent m_OnEndTextSelection = new global::TMPro.TMP_InputField.TextSelectionEvent();

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.OnChangeEvent m_OnValueChanged = new global::TMPro.TMP_InputField.OnChangeEvent();

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.TouchScreenKeyboardEvent m_OnTouchScreenKeyboardStatusChanged = new global::TMPro.TMP_InputField.TouchScreenKeyboardEvent();

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_InputField.OnValidateInput m_OnValidateInput;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_CaretColor = new global::UnityEngine.Color(10f / 51f, 10f / 51f, 10f / 51f, 1f);

		[global::UnityEngine.SerializeField]
		private bool m_CustomCaretColor;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_SelectionColor = new global::UnityEngine.Color(56f / 85f, 0.80784315f, 1f, 64f / 85f);

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.TextArea(5, 10)]
		protected string m_Text = string.Empty;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0f, 4f)]
		private float m_CaretBlinkRate = 0.85f;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(1f, 5f)]
		private int m_CaretWidth = 1;

		[global::UnityEngine.SerializeField]
		private bool m_ReadOnly;

		[global::UnityEngine.SerializeField]
		private bool m_RichText = true;

		protected int m_StringPosition;

		protected int m_StringSelectPosition;

		protected int m_CaretPosition;

		protected int m_CaretSelectPosition;

		private global::UnityEngine.RectTransform caretRectTrans;

		protected global::UnityEngine.UIVertex[] m_CursorVerts;

		private global::UnityEngine.CanvasRenderer m_CachedInputRenderer;

		private global::UnityEngine.Vector2 m_LastPosition;

		[global::System.NonSerialized]
		protected global::UnityEngine.Mesh m_Mesh;

		private bool m_AllowInput;

		private bool m_ShouldActivateNextUpdate;

		private bool m_UpdateDrag;

		private bool m_DragPositionOutOfBounds;

		private const float kHScrollSpeed = 0.05f;

		private const float kVScrollSpeed = 0.1f;

		protected bool m_CaretVisible;

		private global::UnityEngine.Coroutine m_BlinkCoroutine;

		private float m_BlinkStartTime;

		private global::UnityEngine.Coroutine m_DragCoroutine;

		private string m_OriginalText = "";

		private bool m_WasCanceled;

		private bool m_HasDoneFocusTransition;

		private global::UnityEngine.WaitForSecondsRealtime m_WaitForSecondsRealtime;

		private bool m_PreventCallback;

		private bool m_TouchKeyboardAllowsInPlaceEditing;

		private bool m_IsTextComponentUpdateRequired;

		private bool m_HasTextBeenRemoved;

		private float m_PointerDownClickStartTime;

		private float m_KeyDownStartTime;

		private float m_DoubleClickDelay = 0.5f;

		private bool m_IsApplePlatform;

		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		private const string kOculusQuestDeviceModel = "Oculus Quest";

		private bool m_IsCompositionActive;

		private bool m_ShouldUpdateIMEWindowPosition;

		private int m_PreviousIMEInsertionLine;

		[global::UnityEngine.SerializeField]
		protected global::TMPro.TMP_FontAsset m_GlobalFontAsset;

		[global::UnityEngine.SerializeField]
		protected bool m_OnFocusSelectAll = true;

		protected bool m_isSelectAll;

		[global::UnityEngine.SerializeField]
		protected bool m_ResetOnDeActivation = true;

		private bool m_SelectionStillActive;

		private bool m_ReleaseSelection;

		private global::UnityEngine.KeyCode m_LastKeyCode;

		private global::UnityEngine.GameObject m_PreviouslySelectedObject;

		[global::UnityEngine.SerializeField]
		private bool m_KeepTextSelectionVisible;

		[global::UnityEngine.SerializeField]
		private bool m_RestoreOriginalTextOnEscape = true;

		[global::UnityEngine.SerializeField]
		protected bool m_isRichTextEditingAllowed;

		[global::UnityEngine.SerializeField]
		protected int m_LineLimit;

		public bool isAlert;

		[global::UnityEngine.SerializeField]
		protected global::TMPro.TMP_InputValidator m_InputValidator;

		[global::UnityEngine.SerializeField]
		private bool m_ShouldActivateOnSelect = true;

		private bool m_isSelected;

		private bool m_IsStringPositionDirty;

		private bool m_IsCaretPositionDirty;

		private bool m_forceRectTransformAdjustment;

		private bool m_IsKeyboardBeingClosedInHoloLens;

		private global::UnityEngine.Event m_ProcessingEvent = new global::UnityEngine.Event();

		private global::UnityEngine.EventSystems.BaseInput inputSystem
		{
			get
			{
				if ((bool)global::UnityEngine.EventSystems.EventSystem.current && (bool)global::UnityEngine.EventSystems.EventSystem.current.currentInputModule)
				{
					return global::UnityEngine.EventSystems.EventSystem.current.currentInputModule.input;
				}
				return null;
			}
		}

		private string compositionString
		{
			get
			{
				if (!(inputSystem != null))
				{
					return global::UnityEngine.Input.compositionString;
				}
				return inputSystem.compositionString;
			}
		}

		private int compositionLength
		{
			get
			{
				if (m_ReadOnly)
				{
					return 0;
				}
				return compositionString.Length;
			}
		}

		protected global::UnityEngine.Mesh mesh
		{
			get
			{
				if (m_Mesh == null)
				{
					m_Mesh = new global::UnityEngine.Mesh();
				}
				return m_Mesh;
			}
		}

		public virtual bool shouldActivateOnSelect
		{
			get
			{
				if (m_ShouldActivateOnSelect)
				{
					return global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.tvOS;
				}
				return false;
			}
			set
			{
				m_ShouldActivateOnSelect = value;
			}
		}

		public bool shouldHideMobileInput
		{
			get
			{
				switch (global::UnityEngine.Application.platform)
				{
				case global::UnityEngine.RuntimePlatform.IPhonePlayer:
				case global::UnityEngine.RuntimePlatform.Android:
				case global::UnityEngine.RuntimePlatform.WebGLPlayer:
				case global::UnityEngine.RuntimePlatform.tvOS:
					return m_HideMobileInput;
				default:
					return true;
				}
			}
			set
			{
				switch (global::UnityEngine.Application.platform)
				{
				case global::UnityEngine.RuntimePlatform.IPhonePlayer:
				case global::UnityEngine.RuntimePlatform.Android:
				case global::UnityEngine.RuntimePlatform.WebGLPlayer:
				case global::UnityEngine.RuntimePlatform.tvOS:
					global::TMPro.SetPropertyUtility.SetStruct(ref m_HideMobileInput, value);
					break;
				default:
					m_HideMobileInput = true;
					break;
				}
			}
		}

		public bool shouldHideSoftKeyboard
		{
			get
			{
				switch (global::UnityEngine.Application.platform)
				{
				case global::UnityEngine.RuntimePlatform.IPhonePlayer:
				case global::UnityEngine.RuntimePlatform.Android:
				case global::UnityEngine.RuntimePlatform.WebGLPlayer:
				case global::UnityEngine.RuntimePlatform.MetroPlayerX86:
				case global::UnityEngine.RuntimePlatform.MetroPlayerX64:
				case global::UnityEngine.RuntimePlatform.MetroPlayerARM:
				case global::UnityEngine.RuntimePlatform.PS4:
				case global::UnityEngine.RuntimePlatform.tvOS:
				case global::UnityEngine.RuntimePlatform.Switch:
				case global::UnityEngine.RuntimePlatform.GameCoreXboxSeries:
				case global::UnityEngine.RuntimePlatform.GameCoreXboxOne:
				case global::UnityEngine.RuntimePlatform.PS5:
				case global::UnityEngine.RuntimePlatform.VisionOS:
				case global::UnityEngine.RuntimePlatform.Switch2:
					return m_HideSoftKeyboard;
				default:
					return true;
				}
			}
			set
			{
				switch (global::UnityEngine.Application.platform)
				{
				case global::UnityEngine.RuntimePlatform.IPhonePlayer:
				case global::UnityEngine.RuntimePlatform.Android:
				case global::UnityEngine.RuntimePlatform.WebGLPlayer:
				case global::UnityEngine.RuntimePlatform.MetroPlayerX86:
				case global::UnityEngine.RuntimePlatform.MetroPlayerX64:
				case global::UnityEngine.RuntimePlatform.MetroPlayerARM:
				case global::UnityEngine.RuntimePlatform.PS4:
				case global::UnityEngine.RuntimePlatform.tvOS:
				case global::UnityEngine.RuntimePlatform.Switch:
				case global::UnityEngine.RuntimePlatform.GameCoreXboxSeries:
				case global::UnityEngine.RuntimePlatform.GameCoreXboxOne:
				case global::UnityEngine.RuntimePlatform.PS5:
				case global::UnityEngine.RuntimePlatform.VisionOS:
				case global::UnityEngine.RuntimePlatform.Switch2:
					global::TMPro.SetPropertyUtility.SetStruct(ref m_HideSoftKeyboard, value);
					break;
				default:
					m_HideSoftKeyboard = true;
					break;
				}
				if (m_HideSoftKeyboard && m_SoftKeyboard != null && global::UnityEngine.TouchScreenKeyboard.isSupported && m_SoftKeyboard.active)
				{
					m_SoftKeyboard.active = false;
					m_SoftKeyboard = null;
				}
			}
		}

		public string text
		{
			get
			{
				return m_Text;
			}
			set
			{
				SetText(value);
			}
		}

		public bool isFocused => m_AllowInput;

		public float caretBlinkRate
		{
			get
			{
				return m_CaretBlinkRate;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_CaretBlinkRate, value) && m_AllowInput)
				{
					SetCaretActive();
				}
			}
		}

		public int caretWidth
		{
			get
			{
				return m_CaretWidth;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_CaretWidth, value))
				{
					MarkGeometryAsDirty();
				}
			}
		}

		public global::UnityEngine.RectTransform textViewport
		{
			get
			{
				return m_TextViewport;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_TextViewport, value);
			}
		}

		public global::TMPro.TMP_Text textComponent
		{
			get
			{
				return m_TextComponent;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetClass(ref m_TextComponent, value))
				{
					SetTextComponentWrapMode();
				}
			}
		}

		public global::UnityEngine.UI.Graphic placeholder
		{
			get
			{
				return m_Placeholder;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_Placeholder, value);
			}
		}

		public global::UnityEngine.UI.Scrollbar verticalScrollbar
		{
			get
			{
				return m_VerticalScrollbar;
			}
			set
			{
				if (m_VerticalScrollbar != null)
				{
					m_VerticalScrollbar.onValueChanged.RemoveListener(OnScrollbarValueChange);
				}
				global::TMPro.SetPropertyUtility.SetClass(ref m_VerticalScrollbar, value);
				if ((bool)m_VerticalScrollbar)
				{
					m_VerticalScrollbar.onValueChanged.AddListener(OnScrollbarValueChange);
				}
			}
		}

		public float scrollSensitivity
		{
			get
			{
				return m_ScrollSensitivity;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_ScrollSensitivity, value))
				{
					MarkGeometryAsDirty();
				}
			}
		}

		public global::UnityEngine.Color caretColor
		{
			get
			{
				if (!customCaretColor)
				{
					return textComponent.color;
				}
				return m_CaretColor;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetColor(ref m_CaretColor, value))
				{
					MarkGeometryAsDirty();
				}
			}
		}

		public bool customCaretColor
		{
			get
			{
				return m_CustomCaretColor;
			}
			set
			{
				if (m_CustomCaretColor != value)
				{
					m_CustomCaretColor = value;
					MarkGeometryAsDirty();
				}
			}
		}

		public global::UnityEngine.Color selectionColor
		{
			get
			{
				return m_SelectionColor;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetColor(ref m_SelectionColor, value))
				{
					MarkGeometryAsDirty();
				}
			}
		}

		public global::TMPro.TMP_InputField.SubmitEvent onEndEdit
		{
			get
			{
				return m_OnEndEdit;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_OnEndEdit, value);
			}
		}

		public global::TMPro.TMP_InputField.SubmitEvent onSubmit
		{
			get
			{
				return m_OnSubmit;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_OnSubmit, value);
			}
		}

		public global::TMPro.TMP_InputField.SelectionEvent onSelect
		{
			get
			{
				return m_OnSelect;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_OnSelect, value);
			}
		}

		public global::TMPro.TMP_InputField.SelectionEvent onDeselect
		{
			get
			{
				return m_OnDeselect;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_OnDeselect, value);
			}
		}

		public global::TMPro.TMP_InputField.TextSelectionEvent onTextSelection
		{
			get
			{
				return m_OnTextSelection;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_OnTextSelection, value);
			}
		}

		public global::TMPro.TMP_InputField.TextSelectionEvent onEndTextSelection
		{
			get
			{
				return m_OnEndTextSelection;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_OnEndTextSelection, value);
			}
		}

		public global::TMPro.TMP_InputField.OnChangeEvent onValueChanged
		{
			get
			{
				return m_OnValueChanged;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_OnValueChanged, value);
			}
		}

		public global::TMPro.TMP_InputField.TouchScreenKeyboardEvent onTouchScreenKeyboardStatusChanged
		{
			get
			{
				return m_OnTouchScreenKeyboardStatusChanged;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_OnTouchScreenKeyboardStatusChanged, value);
			}
		}

		public global::TMPro.TMP_InputField.OnValidateInput onValidateInput
		{
			get
			{
				return m_OnValidateInput;
			}
			set
			{
				global::TMPro.SetPropertyUtility.SetClass(ref m_OnValidateInput, value);
			}
		}

		public int characterLimit
		{
			get
			{
				return m_CharacterLimit;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_CharacterLimit, global::System.Math.Max(0, value)))
				{
					UpdateLabel();
					if (m_SoftKeyboard != null)
					{
						m_SoftKeyboard.characterLimit = value;
					}
				}
			}
		}

		public float pointSize
		{
			get
			{
				return m_GlobalPointSize;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_GlobalPointSize, global::System.Math.Max(0f, value)))
				{
					SetGlobalPointSize(m_GlobalPointSize);
					UpdateLabel();
				}
			}
		}

		public global::TMPro.TMP_FontAsset fontAsset
		{
			get
			{
				return m_GlobalFontAsset;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetClass(ref m_GlobalFontAsset, value))
				{
					SetGlobalFontAsset(m_GlobalFontAsset);
					UpdateLabel();
				}
			}
		}

		public bool onFocusSelectAll
		{
			get
			{
				return m_OnFocusSelectAll;
			}
			set
			{
				m_OnFocusSelectAll = value;
			}
		}

		public bool resetOnDeActivation
		{
			get
			{
				return m_ResetOnDeActivation;
			}
			set
			{
				m_ResetOnDeActivation = value;
			}
		}

		public bool keepTextSelectionVisible
		{
			get
			{
				return m_KeepTextSelectionVisible;
			}
			set
			{
				m_KeepTextSelectionVisible = value;
			}
		}

		public bool restoreOriginalTextOnEscape
		{
			get
			{
				return m_RestoreOriginalTextOnEscape;
			}
			set
			{
				m_RestoreOriginalTextOnEscape = value;
			}
		}

		public bool isRichTextEditingAllowed
		{
			get
			{
				return m_isRichTextEditingAllowed;
			}
			set
			{
				m_isRichTextEditingAllowed = value;
			}
		}

		public global::TMPro.TMP_InputField.ContentType contentType
		{
			get
			{
				return m_ContentType;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_ContentType, value))
				{
					EnforceContentType();
				}
			}
		}

		public global::TMPro.TMP_InputField.LineType lineType
		{
			get
			{
				return m_LineType;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_LineType, value))
				{
					SetToCustomIfContentTypeIsNot(global::TMPro.TMP_InputField.ContentType.Standard, global::TMPro.TMP_InputField.ContentType.Autocorrected);
					SetTextComponentWrapMode();
				}
			}
		}

		public int lineLimit
		{
			get
			{
				return m_LineLimit;
			}
			set
			{
				if (m_LineType == global::TMPro.TMP_InputField.LineType.SingleLine)
				{
					m_LineLimit = 1;
				}
				else
				{
					global::TMPro.SetPropertyUtility.SetStruct(ref m_LineLimit, value);
				}
			}
		}

		public global::TMPro.TMP_InputField.InputType inputType
		{
			get
			{
				return m_InputType;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_InputType, value))
				{
					SetToCustom();
				}
			}
		}

		public global::UnityEngine.TouchScreenKeyboard touchScreenKeyboard => m_SoftKeyboard;

		public global::UnityEngine.TouchScreenKeyboardType keyboardType
		{
			get
			{
				return m_KeyboardType;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_KeyboardType, value))
				{
					SetToCustom();
				}
			}
		}

		public global::TMPro.TMP_InputField.CharacterValidation characterValidation
		{
			get
			{
				return m_CharacterValidation;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_CharacterValidation, value))
				{
					SetToCustom();
				}
			}
		}

		public global::TMPro.TMP_InputValidator inputValidator
		{
			get
			{
				return m_InputValidator;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetClass(ref m_InputValidator, value))
				{
					SetToCustom(global::TMPro.TMP_InputField.CharacterValidation.CustomValidator);
				}
			}
		}

		public bool readOnly
		{
			get
			{
				return m_ReadOnly;
			}
			set
			{
				m_ReadOnly = value;
			}
		}

		public bool richText
		{
			get
			{
				return m_RichText;
			}
			set
			{
				m_RichText = value;
				SetTextComponentRichTextMode();
			}
		}

		public bool multiLine
		{
			get
			{
				if (m_LineType != global::TMPro.TMP_InputField.LineType.MultiLineNewline)
				{
					return lineType == global::TMPro.TMP_InputField.LineType.MultiLineSubmit;
				}
				return true;
			}
		}

		public char asteriskChar
		{
			get
			{
				return m_AsteriskChar;
			}
			set
			{
				if (global::TMPro.SetPropertyUtility.SetStruct(ref m_AsteriskChar, value))
				{
					UpdateLabel();
				}
			}
		}

		public bool wasCanceled => m_WasCanceled;

		protected int caretPositionInternal
		{
			get
			{
				return m_CaretPosition + compositionLength;
			}
			set
			{
				m_CaretPosition = value;
				ClampCaretPos(ref m_CaretPosition);
			}
		}

		protected int stringPositionInternal
		{
			get
			{
				return m_StringPosition + compositionLength;
			}
			set
			{
				m_StringPosition = value;
				ClampStringPos(ref m_StringPosition);
			}
		}

		protected int caretSelectPositionInternal
		{
			get
			{
				return m_CaretSelectPosition + compositionLength;
			}
			set
			{
				m_CaretSelectPosition = value;
				ClampCaretPos(ref m_CaretSelectPosition);
			}
		}

		protected int stringSelectPositionInternal
		{
			get
			{
				return m_StringSelectPosition + compositionLength;
			}
			set
			{
				m_StringSelectPosition = value;
				ClampStringPos(ref m_StringSelectPosition);
			}
		}

		private bool hasSelection => stringPositionInternal != stringSelectPositionInternal;

		public int caretPosition
		{
			get
			{
				return caretSelectPositionInternal;
			}
			set
			{
				selectionAnchorPosition = value;
				selectionFocusPosition = value;
				UpdateStringIndexFromCaretPosition();
			}
		}

		public int selectionAnchorPosition
		{
			get
			{
				return caretPositionInternal;
			}
			set
			{
				if (compositionLength == 0)
				{
					caretPositionInternal = value;
					m_IsStringPositionDirty = true;
				}
			}
		}

		public int selectionFocusPosition
		{
			get
			{
				return caretSelectPositionInternal;
			}
			set
			{
				if (compositionLength == 0)
				{
					caretSelectPositionInternal = value;
					m_IsStringPositionDirty = true;
				}
			}
		}

		public int stringPosition
		{
			get
			{
				return stringSelectPositionInternal;
			}
			set
			{
				selectionStringAnchorPosition = value;
				selectionStringFocusPosition = value;
				UpdateCaretPositionFromStringIndex();
			}
		}

		public int selectionStringAnchorPosition
		{
			get
			{
				return stringPositionInternal;
			}
			set
			{
				if (compositionLength == 0)
				{
					stringPositionInternal = value;
					m_IsCaretPositionDirty = true;
				}
			}
		}

		public int selectionStringFocusPosition
		{
			get
			{
				return stringSelectPositionInternal;
			}
			set
			{
				if (compositionLength == 0)
				{
					stringSelectPositionInternal = value;
					m_IsCaretPositionDirty = true;
				}
			}
		}

		private static string clipboard
		{
			get
			{
				return global::UnityEngine.GUIUtility.systemCopyBuffer;
			}
			set
			{
				global::UnityEngine.GUIUtility.systemCopyBuffer = value;
			}
		}

		public virtual float minWidth => 0f;

		public virtual float preferredWidth
		{
			get
			{
				if (textComponent == null)
				{
					return 0f;
				}
				float num = 0f;
				if (m_LayoutGroup != null)
				{
					num = m_LayoutGroup.padding.horizontal;
				}
				if (m_TextViewport != null)
				{
					num += m_TextViewport.offsetMin.x - m_TextViewport.offsetMax.x;
				}
				return m_TextComponent.preferredWidth + num;
			}
		}

		public virtual float flexibleWidth => -1f;

		public virtual float minHeight => 0f;

		public virtual float preferredHeight
		{
			get
			{
				if (textComponent == null)
				{
					return 0f;
				}
				float num = 0f;
				if (m_LayoutGroup != null)
				{
					num = m_LayoutGroup.padding.vertical;
				}
				if (m_TextViewport != null)
				{
					num += m_TextViewport.offsetMin.y - m_TextViewport.offsetMax.y;
				}
				return m_TextComponent.preferredHeight + num;
			}
		}

		public virtual float flexibleHeight => -1f;

		public virtual int layoutPriority => 1;

		global::UnityEngine.Transform global::UnityEngine.UI.ICanvasElement.transform => base.transform;

		protected TMP_InputField()
		{
			SetTextComponentWrapMode();
		}

		private bool isKeyboardUsingEvents()
		{
			switch (global::UnityEngine.Application.platform)
			{
			case global::UnityEngine.RuntimePlatform.Android:
				if (InPlaceEditing())
				{
					return m_HideSoftKeyboard;
				}
				return false;
			case global::UnityEngine.RuntimePlatform.IPhonePlayer:
			case global::UnityEngine.RuntimePlatform.tvOS:
			case global::UnityEngine.RuntimePlatform.VisionOS:
				return m_HideSoftKeyboard;
			case global::UnityEngine.RuntimePlatform.PS4:
			case global::UnityEngine.RuntimePlatform.Switch:
			case global::UnityEngine.RuntimePlatform.GameCoreXboxSeries:
			case global::UnityEngine.RuntimePlatform.GameCoreXboxOne:
			case global::UnityEngine.RuntimePlatform.PS5:
			case global::UnityEngine.RuntimePlatform.Switch2:
				return false;
			case global::UnityEngine.RuntimePlatform.WebGLPlayer:
				if (m_SoftKeyboard != null)
				{
					return !m_SoftKeyboard.active;
				}
				return true;
			default:
				return true;
			}
		}

		private bool isUWP()
		{
			if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.MetroPlayerX86 && global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.MetroPlayerX64)
			{
				return global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.MetroPlayerARM;
			}
			return true;
		}

		public void SetTextWithoutNotify(string input)
		{
			SetText(input, sendCallback: false);
		}

		private void SetText(string value, bool sendCallback = true)
		{
			if (!(text == value))
			{
				if (value == null)
				{
					value = "";
				}
				value = value.Replace("\0", string.Empty);
				m_Text = value;
				if (m_SoftKeyboard != null)
				{
					m_SoftKeyboard.text = m_Text;
				}
				if (m_StringPosition > m_Text.Length)
				{
					m_StringPosition = (m_StringSelectPosition = m_Text.Length);
				}
				else if (m_StringSelectPosition > m_Text.Length)
				{
					m_StringSelectPosition = m_Text.Length;
				}
				m_forceRectTransformAdjustment = true;
				m_IsTextComponentUpdateRequired = true;
				UpdateLabel();
				if (sendCallback)
				{
					SendOnValueChanged();
				}
			}
		}

		protected void ClampStringPos(ref int pos)
		{
			if (pos <= 0)
			{
				pos = 0;
			}
			else if (pos > text.Length)
			{
				pos = text.Length;
			}
		}

		protected void ClampCaretPos(ref int pos)
		{
			if (pos > m_TextComponent.textInfo.characterCount - 1)
			{
				pos = m_TextComponent.textInfo.characterCount - 1;
			}
			if (pos <= 0)
			{
				pos = 0;
			}
		}

		private int ClampArrayIndex(int index)
		{
			if (index < 0)
			{
				return 0;
			}
			return index;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (m_Text == null)
			{
				m_Text = string.Empty;
			}
			m_IsApplePlatform = global::UnityEngine.SystemInfo.operatingSystemFamily == global::UnityEngine.OperatingSystemFamily.MacOSX || global::UnityEngine.SystemInfo.operatingSystem.Contains("iOS") || global::UnityEngine.SystemInfo.operatingSystem.Contains("tvOS");
			if (GetComponent<global::UnityEngine.UI.ILayoutController>() != null)
			{
				m_IsDrivenByLayoutComponents = true;
				m_LayoutGroup = GetComponent<global::UnityEngine.UI.LayoutGroup>();
			}
			else
			{
				m_IsDrivenByLayoutComponents = false;
			}
			if (global::UnityEngine.Application.isPlaying && m_CachedInputRenderer == null && m_TextComponent != null)
			{
				global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("Caret", typeof(global::TMPro.TMP_SelectionCaret));
				gameObject.hideFlags = global::UnityEngine.HideFlags.DontSave;
				gameObject.transform.SetParent(m_TextComponent.transform.parent);
				gameObject.transform.SetAsFirstSibling();
				gameObject.layer = base.gameObject.layer;
				caretRectTrans = gameObject.GetComponent<global::UnityEngine.RectTransform>();
				m_CachedInputRenderer = gameObject.GetComponent<global::UnityEngine.CanvasRenderer>();
				m_CachedInputRenderer.SetMaterial(global::UnityEngine.UI.Graphic.defaultGraphicMaterial, global::UnityEngine.Texture2D.whiteTexture);
				gameObject.AddComponent<global::UnityEngine.UI.LayoutElement>().ignoreLayout = true;
				AssignPositioningIfNeeded();
			}
			m_RectTransform = GetComponent<global::UnityEngine.RectTransform>();
			global::UnityEngine.EventSystems.IScrollHandler[] componentsInParent = GetComponentsInParent<global::UnityEngine.EventSystems.IScrollHandler>();
			if (componentsInParent.Length > 1)
			{
				m_IScrollHandlerParent = componentsInParent[1] as global::UnityEngine.UI.ScrollRect;
			}
			if (m_TextViewport != null)
			{
				m_TextViewportRectMask = m_TextViewport.GetComponent<global::UnityEngine.UI.RectMask2D>();
				UpdateMaskRegions();
			}
			if (m_CachedInputRenderer != null)
			{
				m_CachedInputRenderer.SetMaterial(global::UnityEngine.UI.Graphic.defaultGraphicMaterial, global::UnityEngine.Texture2D.whiteTexture);
			}
			if (m_TextComponent != null)
			{
				m_TextComponent.RegisterDirtyVerticesCallback(MarkGeometryAsDirty);
				m_TextComponent.RegisterDirtyVerticesCallback(UpdateLabel);
				if (m_VerticalScrollbar != null)
				{
					m_VerticalScrollbar.onValueChanged.AddListener(OnScrollbarValueChange);
				}
				UpdateLabel();
			}
			m_TouchKeyboardAllowsInPlaceEditing = global::UnityEngine.TouchScreenKeyboard.isInPlaceEditingAllowed;
			global::TMPro.TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
		}

		protected override void OnDisable()
		{
			m_BlinkCoroutine = null;
			DeactivateInputField();
			if (m_TextComponent != null)
			{
				m_TextComponent.UnregisterDirtyVerticesCallback(MarkGeometryAsDirty);
				m_TextComponent.UnregisterDirtyVerticesCallback(UpdateLabel);
				if (m_VerticalScrollbar != null)
				{
					m_VerticalScrollbar.onValueChanged.RemoveListener(OnScrollbarValueChange);
				}
			}
			global::UnityEngine.UI.CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (m_CachedInputRenderer != null)
			{
				m_CachedInputRenderer.Clear();
			}
			if (m_Mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(m_Mesh);
			}
			m_Mesh = null;
			global::TMPro.TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
			base.OnDisable();
		}

		private void ON_TEXT_CHANGED(global::UnityEngine.Object obj)
		{
			if (obj == m_TextComponent && !m_IsStringPositionDirty)
			{
				if (global::UnityEngine.Application.isPlaying && compositionLength == 0)
				{
					UpdateCaretPositionFromStringIndex();
				}
				if ((bool)m_VerticalScrollbar)
				{
					UpdateScrollbar();
				}
			}
		}

		private global::System.Collections.IEnumerator CaretBlink()
		{
			m_CaretVisible = true;
			yield return null;
			while ((isFocused || m_SelectionStillActive) && m_CaretBlinkRate > 0f)
			{
				float num = 1f / m_CaretBlinkRate;
				bool flag = (global::UnityEngine.Time.unscaledTime - m_BlinkStartTime) % num < num / 2f;
				if (m_CaretVisible != flag)
				{
					m_CaretVisible = flag;
					if (!hasSelection)
					{
						MarkGeometryAsDirty();
					}
				}
				yield return null;
			}
			m_BlinkCoroutine = null;
		}

		private void SetCaretVisible()
		{
			if (m_AllowInput)
			{
				m_CaretVisible = true;
				m_BlinkStartTime = global::UnityEngine.Time.unscaledTime;
				SetCaretActive();
			}
		}

		private void SetCaretActive()
		{
			if (!m_AllowInput)
			{
				return;
			}
			if (m_CaretBlinkRate > 0f)
			{
				if (m_BlinkCoroutine == null)
				{
					m_BlinkCoroutine = StartCoroutine(CaretBlink());
				}
			}
			else
			{
				m_CaretVisible = true;
			}
		}

		protected void OnFocus()
		{
			if (m_OnFocusSelectAll)
			{
				SelectAll();
			}
		}

		protected void SelectAll()
		{
			m_isSelectAll = true;
			stringPositionInternal = text.Length;
			stringSelectPositionInternal = 0;
		}

		public void MoveTextEnd(bool shift)
		{
			if (m_isRichTextEditingAllowed)
			{
				int length = text.Length;
				if (shift)
				{
					stringSelectPositionInternal = length;
				}
				else
				{
					stringPositionInternal = length;
					stringSelectPositionInternal = stringPositionInternal;
				}
			}
			else
			{
				int num = m_TextComponent.textInfo.characterCount - 1;
				if (shift)
				{
					caretSelectPositionInternal = num;
					stringSelectPositionInternal = GetStringIndexFromCaretPosition(num);
				}
				else
				{
					int num2 = (caretSelectPositionInternal = num);
					caretPositionInternal = num2;
					num2 = (stringPositionInternal = GetStringIndexFromCaretPosition(num));
					stringSelectPositionInternal = num2;
				}
			}
			UpdateLabel();
		}

		public void MoveTextStart(bool shift)
		{
			if (m_isRichTextEditingAllowed)
			{
				int num = 0;
				if (shift)
				{
					stringSelectPositionInternal = num;
				}
				else
				{
					stringPositionInternal = num;
					stringSelectPositionInternal = stringPositionInternal;
				}
			}
			else
			{
				int num2 = 0;
				if (shift)
				{
					caretSelectPositionInternal = num2;
					stringSelectPositionInternal = GetStringIndexFromCaretPosition(num2);
				}
				else
				{
					int num3 = (caretSelectPositionInternal = num2);
					caretPositionInternal = num3;
					num3 = (stringPositionInternal = GetStringIndexFromCaretPosition(num2));
					stringSelectPositionInternal = num3;
				}
			}
			UpdateLabel();
		}

		public void MoveToEndOfLine(bool shift, bool ctrl)
		{
			int lineNumber = m_TextComponent.textInfo.characterInfo[caretPositionInternal].lineNumber;
			int num = (ctrl ? (m_TextComponent.textInfo.characterCount - 1) : m_TextComponent.textInfo.lineInfo[lineNumber].lastCharacterIndex);
			int index = m_TextComponent.textInfo.characterInfo[num].index;
			if (shift)
			{
				stringSelectPositionInternal = index;
				caretSelectPositionInternal = num;
			}
			else
			{
				stringPositionInternal = index;
				stringSelectPositionInternal = stringPositionInternal;
				int num2 = (caretPositionInternal = num);
				caretSelectPositionInternal = num2;
			}
			UpdateLabel();
		}

		public void MoveToStartOfLine(bool shift, bool ctrl)
		{
			int lineNumber = m_TextComponent.textInfo.characterInfo[caretPositionInternal].lineNumber;
			int num = ((!ctrl) ? m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex : 0);
			int num2 = 0;
			if (num > 0)
			{
				num2 = m_TextComponent.textInfo.characterInfo[num - 1].index + m_TextComponent.textInfo.characterInfo[num - 1].stringLength;
			}
			if (shift)
			{
				stringSelectPositionInternal = num2;
				caretSelectPositionInternal = num;
			}
			else
			{
				stringPositionInternal = num2;
				stringSelectPositionInternal = stringPositionInternal;
				int num3 = (caretPositionInternal = num);
				caretSelectPositionInternal = num3;
			}
			UpdateLabel();
		}

		private bool InPlaceEditing()
		{
			if (m_TouchKeyboardAllowsInPlaceEditing)
			{
				return true;
			}
			if (isUWP())
			{
				return !global::UnityEngine.TouchScreenKeyboard.isSupported;
			}
			if (global::UnityEngine.TouchScreenKeyboard.isSupported && shouldHideSoftKeyboard)
			{
				return true;
			}
			if (global::UnityEngine.TouchScreenKeyboard.isSupported && !shouldHideSoftKeyboard && !shouldHideMobileInput)
			{
				return false;
			}
			return true;
		}

		private bool InPlaceEditingChanged()
		{
			if (!s_IsQuestDevice)
			{
				return m_TouchKeyboardAllowsInPlaceEditing != global::UnityEngine.TouchScreenKeyboard.isInPlaceEditingAllowed;
			}
			return false;
		}

		private bool TouchScreenKeyboardShouldBeUsed()
		{
			global::UnityEngine.RuntimePlatform platform = global::UnityEngine.Application.platform;
			if (platform == global::UnityEngine.RuntimePlatform.Android || platform == global::UnityEngine.RuntimePlatform.WebGLPlayer)
			{
				if (s_IsQuestDevice)
				{
					return global::UnityEngine.TouchScreenKeyboard.isSupported;
				}
				return !global::UnityEngine.TouchScreenKeyboard.isInPlaceEditingAllowed;
			}
			return global::UnityEngine.TouchScreenKeyboard.isSupported;
		}

		private void UpdateKeyboardStringPosition()
		{
			if (m_HideMobileInput && m_SoftKeyboard != null && m_SoftKeyboard.canSetSelection && (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.IPhonePlayer || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.tvOS))
			{
				int start = global::UnityEngine.Mathf.Min(stringSelectPositionInternal, stringPositionInternal);
				int length = global::UnityEngine.Mathf.Abs(stringSelectPositionInternal - stringPositionInternal);
				m_SoftKeyboard.selection = new global::UnityEngine.RangeInt(start, length);
			}
		}

		private void UpdateStringPositionFromKeyboard()
		{
			global::UnityEngine.RangeInt selection = m_SoftKeyboard.selection;
			int start = selection.start;
			int end = selection.end;
			bool flag = false;
			if (stringPositionInternal != start)
			{
				flag = true;
				stringPositionInternal = start;
				caretPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal);
			}
			if (stringSelectPositionInternal != end)
			{
				stringSelectPositionInternal = end;
				flag = true;
				caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
			}
			if (flag)
			{
				m_BlinkStartTime = global::UnityEngine.Time.unscaledTime;
				UpdateLabel();
			}
		}

		protected virtual void LateUpdate()
		{
			if (m_ShouldActivateNextUpdate)
			{
				if (!isFocused)
				{
					ActivateInputFieldInternal();
					m_ShouldActivateNextUpdate = false;
					return;
				}
				m_ShouldActivateNextUpdate = false;
			}
			if (isFocused && InPlaceEditingChanged())
			{
				DeactivateInputField();
			}
			if (!isFocused && m_SelectionStillActive)
			{
				global::UnityEngine.GameObject gameObject = ((global::UnityEngine.EventSystems.EventSystem.current != null) ? global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject : null);
				if (gameObject == null && m_ResetOnDeActivation)
				{
					ReleaseSelection();
					return;
				}
				if (gameObject != null && gameObject != base.gameObject)
				{
					if (gameObject == m_PreviouslySelectedObject)
					{
						return;
					}
					m_PreviouslySelectedObject = gameObject;
					if (!m_VerticalScrollbar || !(gameObject == m_VerticalScrollbar.gameObject))
					{
						if (m_ResetOnDeActivation)
						{
							ReleaseSelection();
						}
						else if (!m_KeepTextSelectionVisible && gameObject.GetComponent<global::TMPro.TMP_InputField>() != null)
						{
							ReleaseSelection();
						}
					}
					return;
				}
				if (m_ProcessingEvent != null && m_ProcessingEvent.rawType == global::UnityEngine.EventType.MouseDown && m_ProcessingEvent.button == 0)
				{
					bool flag = false;
					float unscaledTime = global::UnityEngine.Time.unscaledTime;
					if (m_KeyDownStartTime + m_DoubleClickDelay > unscaledTime)
					{
						flag = true;
					}
					m_KeyDownStartTime = unscaledTime;
					if (flag)
					{
						ReleaseSelection();
						return;
					}
				}
			}
			UpdateMaskRegions();
			if ((InPlaceEditing() && isKeyboardUsingEvents()) || !isFocused)
			{
				return;
			}
			AssignPositioningIfNeeded();
			if (m_SoftKeyboard == null || m_SoftKeyboard.status != global::UnityEngine.TouchScreenKeyboard.Status.Visible)
			{
				if (m_SoftKeyboard != null)
				{
					if (!m_ReadOnly)
					{
						this.text = m_SoftKeyboard.text;
					}
					global::UnityEngine.TouchScreenKeyboard.Status status = m_SoftKeyboard.status;
					if (m_LastKeyCode != global::UnityEngine.KeyCode.Return && status == global::UnityEngine.TouchScreenKeyboard.Status.Done && isUWP())
					{
						status = global::UnityEngine.TouchScreenKeyboard.Status.Canceled;
						m_IsKeyboardBeingClosedInHoloLens = true;
					}
					switch (status)
					{
					case global::UnityEngine.TouchScreenKeyboard.Status.LostFocus:
						SendTouchScreenKeyboardStatusChanged();
						break;
					case global::UnityEngine.TouchScreenKeyboard.Status.Canceled:
						m_ReleaseSelection = true;
						m_WasCanceled = true;
						SendTouchScreenKeyboardStatusChanged();
						break;
					case global::UnityEngine.TouchScreenKeyboard.Status.Done:
						m_ReleaseSelection = true;
						SendTouchScreenKeyboardStatusChanged();
						OnSubmit(null);
						break;
					}
				}
				return;
			}
			string text = m_SoftKeyboard.text;
			if (m_Text != text)
			{
				if (m_ReadOnly)
				{
					m_SoftKeyboard.text = m_Text;
				}
				else
				{
					m_Text = "";
					for (int i = 0; i < text.Length; i++)
					{
						char c = text[i];
						bool flag2 = false;
						if (c == '\r' || c == '\u0003')
						{
							c = '\n';
						}
						if (onValidateInput != null)
						{
							c = onValidateInput(m_Text, m_Text.Length, c);
						}
						else if (characterValidation != global::TMPro.TMP_InputField.CharacterValidation.None)
						{
							string obj = m_Text;
							c = Validate(m_Text, m_Text.Length, c);
							flag2 = obj != m_Text;
						}
						if (lineType != global::TMPro.TMP_InputField.LineType.MultiLineNewline && c == '\n')
						{
							UpdateLabel();
							OnSubmit(null);
							OnDeselect(null);
							return;
						}
						if (c != 0 && (characterValidation != global::TMPro.TMP_InputField.CharacterValidation.CustomValidator || !flag2))
						{
							m_Text += c;
						}
					}
					if (characterLimit > 0 && m_Text.Length > characterLimit)
					{
						m_Text = m_Text.Substring(0, characterLimit);
					}
					UpdateStringPositionFromKeyboard();
					if (m_Text != text)
					{
						m_SoftKeyboard.text = m_Text;
					}
					SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (m_HideMobileInput && m_SoftKeyboard != null && m_SoftKeyboard.canSetSelection && global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.IPhonePlayer && global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.tvOS)
			{
				int start = global::UnityEngine.Mathf.Min(stringSelectPositionInternal, stringPositionInternal);
				int length = global::UnityEngine.Mathf.Abs(stringSelectPositionInternal - stringPositionInternal);
				m_SoftKeyboard.selection = new global::UnityEngine.RangeInt(start, length);
			}
			else if ((m_HideMobileInput && global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android) || (m_SoftKeyboard.canSetSelection && (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.IPhonePlayer || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.tvOS)))
			{
				UpdateStringPositionFromKeyboard();
			}
			if (m_SoftKeyboard != null && m_SoftKeyboard.status != global::UnityEngine.TouchScreenKeyboard.Status.Visible)
			{
				if (m_SoftKeyboard.status == global::UnityEngine.TouchScreenKeyboard.Status.Canceled)
				{
					m_WasCanceled = true;
				}
				OnDeselect(null);
			}
		}

		private bool MayDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (IsActive() && IsInteractable() && eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left && m_TextComponent != null)
			{
				if (m_SoftKeyboard != null && !shouldHideSoftKeyboard)
				{
					return shouldHideMobileInput;
				}
				return true;
			}
			return false;
		}

		public virtual void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (MayDrag(eventData))
			{
				m_UpdateDrag = true;
			}
		}

		public virtual void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!MayDrag(eventData))
			{
				return;
			}
			global::TMPro.CaretPosition cursor;
			int cursorIndexFromPosition = global::TMPro.TMP_TextUtilities.GetCursorIndexFromPosition(m_TextComponent, eventData.position, eventData.pressEventCamera, out cursor);
			if (m_isRichTextEditingAllowed)
			{
				switch (cursor)
				{
				case global::TMPro.CaretPosition.Left:
					stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
					break;
				case global::TMPro.CaretPosition.Right:
					stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					break;
				}
			}
			else
			{
				switch (cursor)
				{
				case global::TMPro.CaretPosition.Left:
					stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
					break;
				case global::TMPro.CaretPosition.Right:
					stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					break;
				}
			}
			caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
			MarkGeometryAsDirty();
			m_DragPositionOutOfBounds = !global::UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(textViewport, eventData.position, eventData.pressEventCamera);
			if (m_DragPositionOutOfBounds && m_DragCoroutine == null)
			{
				m_DragCoroutine = StartCoroutine(MouseDragOutsideRect(eventData));
			}
			UpdateKeyboardStringPosition();
			eventData.Use();
		}

		private global::System.Collections.IEnumerator MouseDragOutsideRect(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			while (m_UpdateDrag && m_DragPositionOutOfBounds)
			{
				global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(textViewport, eventData.position, eventData.pressEventCamera, out var localPoint);
				global::UnityEngine.Rect rect = textViewport.rect;
				if (multiLine)
				{
					if (localPoint.y > rect.yMax)
					{
						MoveUp(shift: true, goToFirstChar: false);
					}
					else if (localPoint.y < rect.yMin)
					{
						MoveDown(shift: true, goToLastChar: false);
					}
				}
				else if (localPoint.x < rect.xMin)
				{
					MoveLeft(shift: true, ctrl: false);
				}
				else if (localPoint.x > rect.xMax)
				{
					MoveRight(shift: true, ctrl: false);
				}
				UpdateLabel();
				float num = (multiLine ? 0.1f : 0.05f);
				if (m_WaitForSecondsRealtime == null)
				{
					m_WaitForSecondsRealtime = new global::UnityEngine.WaitForSecondsRealtime(num);
				}
				else
				{
					m_WaitForSecondsRealtime.waitTime = num;
				}
				yield return m_WaitForSecondsRealtime;
			}
			m_DragCoroutine = null;
		}

		public virtual void OnEndDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (MayDrag(eventData))
			{
				m_UpdateDrag = false;
			}
		}

		public override void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (!MayDrag(eventData))
			{
				return;
			}
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool allowInput = m_AllowInput;
			base.OnPointerDown(eventData);
			if (!InPlaceEditing() && (m_SoftKeyboard == null || !m_SoftKeyboard.active))
			{
				OnSelect(eventData);
				return;
			}
			global::UnityEngine.Event.PopEvent(m_ProcessingEvent);
			bool flag = m_ProcessingEvent != null && (m_ProcessingEvent.modifiers & global::UnityEngine.EventModifiers.Shift) != 0;
			bool flag2 = false;
			float unscaledTime = global::UnityEngine.Time.unscaledTime;
			if (m_PointerDownClickStartTime + m_DoubleClickDelay > unscaledTime)
			{
				flag2 = true;
			}
			m_PointerDownClickStartTime = unscaledTime;
			if (allowInput || !m_OnFocusSelectAll)
			{
				global::TMPro.CaretPosition cursor;
				int cursorIndexFromPosition = global::TMPro.TMP_TextUtilities.GetCursorIndexFromPosition(m_TextComponent, eventData.position, eventData.pressEventCamera, out cursor);
				if (flag)
				{
					if (m_isRichTextEditingAllowed)
					{
						switch (cursor)
						{
						case global::TMPro.CaretPosition.Left:
							stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
							break;
						case global::TMPro.CaretPosition.Right:
							stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
							break;
						}
					}
					else
					{
						switch (cursor)
						{
						case global::TMPro.CaretPosition.Left:
							stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
							break;
						case global::TMPro.CaretPosition.Right:
							stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
							break;
						}
					}
				}
				else if (m_isRichTextEditingAllowed)
				{
					switch (cursor)
					{
					case global::TMPro.CaretPosition.Left:
					{
						int num = (stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index);
						stringPositionInternal = num;
						break;
					}
					case global::TMPro.CaretPosition.Right:
					{
						int num = (stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength);
						stringPositionInternal = num;
						break;
					}
					}
				}
				else
				{
					switch (cursor)
					{
					case global::TMPro.CaretPosition.Left:
					{
						int num = (stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? m_TextComponent.textInfo.characterInfo[0].index : (m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength)));
						stringPositionInternal = num;
						break;
					}
					case global::TMPro.CaretPosition.Right:
					{
						int num = (stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength);
						stringPositionInternal = num;
						break;
					}
					}
				}
				if (flag2)
				{
					int num5 = global::TMPro.TMP_TextUtilities.FindIntersectingWord(m_TextComponent, eventData.position, eventData.pressEventCamera);
					if (num5 != -1)
					{
						caretPositionInternal = m_TextComponent.textInfo.wordInfo[num5].firstCharacterIndex;
						caretSelectPositionInternal = m_TextComponent.textInfo.wordInfo[num5].lastCharacterIndex + 1;
						stringPositionInternal = m_TextComponent.textInfo.characterInfo[caretPositionInternal].index;
						stringSelectPositionInternal = m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].stringLength;
					}
					else
					{
						caretPositionInternal = cursorIndexFromPosition;
						caretSelectPositionInternal = caretPositionInternal + 1;
						stringPositionInternal = m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
						stringSelectPositionInternal = stringPositionInternal + m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					}
				}
				else
				{
					int num = (caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal));
					caretPositionInternal = num;
				}
				m_isSelectAll = false;
			}
			UpdateLabel();
			UpdateKeyboardStringPosition();
			eventData.Use();
		}

		protected global::TMPro.TMP_InputField.EditState KeyPressed(global::UnityEngine.Event evt)
		{
			global::UnityEngine.EventModifiers modifiers = evt.modifiers;
			bool flag = (m_IsApplePlatform ? ((modifiers & global::UnityEngine.EventModifiers.Command) != 0) : ((modifiers & global::UnityEngine.EventModifiers.Control) != 0));
			bool flag2 = (modifiers & global::UnityEngine.EventModifiers.Shift) != 0;
			bool flag3 = (modifiers & global::UnityEngine.EventModifiers.Alt) != 0;
			bool flag4 = flag && !flag3 && !flag2;
			m_LastKeyCode = evt.keyCode;
			switch (evt.keyCode)
			{
			case global::UnityEngine.KeyCode.Backspace:
				Backspace();
				return global::TMPro.TMP_InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.Delete:
				DeleteKey();
				return global::TMPro.TMP_InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.Home:
				MoveToStartOfLine(flag2, flag);
				return global::TMPro.TMP_InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.End:
				MoveToEndOfLine(flag2, flag);
				return global::TMPro.TMP_InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.A:
				if (flag4)
				{
					SelectAll();
					return global::TMPro.TMP_InputField.EditState.Continue;
				}
				break;
			case global::UnityEngine.KeyCode.C:
				if (flag4)
				{
					if (inputType != global::TMPro.TMP_InputField.InputType.Password)
					{
						clipboard = GetSelectedString();
					}
					else
					{
						clipboard = "";
					}
					return global::TMPro.TMP_InputField.EditState.Continue;
				}
				break;
			case global::UnityEngine.KeyCode.V:
				if (flag4)
				{
					Append(clipboard);
					return global::TMPro.TMP_InputField.EditState.Continue;
				}
				break;
			case global::UnityEngine.KeyCode.X:
				if (flag4)
				{
					if (inputType != global::TMPro.TMP_InputField.InputType.Password)
					{
						clipboard = GetSelectedString();
					}
					else
					{
						clipboard = "";
					}
					Delete();
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChangedAndUpdateLabel();
					return global::TMPro.TMP_InputField.EditState.Continue;
				}
				break;
			case global::UnityEngine.KeyCode.LeftArrow:
				MoveLeft(flag2, flag);
				return global::TMPro.TMP_InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.RightArrow:
				MoveRight(flag2, flag);
				return global::TMPro.TMP_InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.UpArrow:
				MoveUp(flag2);
				return global::TMPro.TMP_InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.DownArrow:
				MoveDown(flag2);
				return global::TMPro.TMP_InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.PageUp:
				MovePageUp(flag2);
				return global::TMPro.TMP_InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.PageDown:
				MovePageDown(flag2);
				return global::TMPro.TMP_InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.Return:
			case global::UnityEngine.KeyCode.KeypadEnter:
			{
				if (lineType != global::TMPro.TMP_InputField.LineType.MultiLineNewline)
				{
					m_ReleaseSelection = true;
					return global::TMPro.TMP_InputField.EditState.Finish;
				}
				global::TMPro.TMP_TextInfo textInfo = m_TextComponent.textInfo;
				if (m_LineLimit > 0 && textInfo != null && textInfo.lineCount >= m_LineLimit)
				{
					m_ReleaseSelection = true;
					return global::TMPro.TMP_InputField.EditState.Finish;
				}
				break;
			}
			case global::UnityEngine.KeyCode.Escape:
				m_ReleaseSelection = true;
				m_WasCanceled = true;
				return global::TMPro.TMP_InputField.EditState.Finish;
			}
			char c = evt.character;
			if (!multiLine && (c == '\t' || c == '\r' || c == '\n'))
			{
				return global::TMPro.TMP_InputField.EditState.Continue;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (flag2 && c == '\n')
			{
				c = '\v';
			}
			if (IsValidChar(c))
			{
				Append(c);
			}
			if (c == '\0' && compositionLength > 0)
			{
				UpdateLabel();
			}
			return global::TMPro.TMP_InputField.EditState.Continue;
		}

		protected virtual bool IsValidChar(char c)
		{
			if (c == '\u007f')
			{
				return false;
			}
			if (c == '\t' || c == '\n')
			{
				return true;
			}
			if (c < ' ')
			{
				return false;
			}
			return true;
		}

		public void ProcessEvent(global::UnityEngine.Event e)
		{
			KeyPressed(e);
		}

		public virtual void OnUpdateSelected(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			if (!isFocused)
			{
				return;
			}
			bool flag = false;
			while (global::UnityEngine.Event.PopEvent(m_ProcessingEvent))
			{
				switch (m_ProcessingEvent.rawType)
				{
				case global::UnityEngine.EventType.KeyDown:
					break;
				case global::UnityEngine.EventType.ValidateCommand:
				case global::UnityEngine.EventType.ExecuteCommand:
					if (m_ProcessingEvent.commandName == "SelectAll")
					{
						SelectAll();
						flag = true;
					}
					continue;
				default:
					continue;
				}
				flag = true;
				if (m_IsCompositionActive && compositionLength == 0 && m_ProcessingEvent.character == '\0' && m_ProcessingEvent.modifiers == global::UnityEngine.EventModifiers.None)
				{
					continue;
				}
				if (KeyPressed(m_ProcessingEvent) == global::TMPro.TMP_InputField.EditState.Finish)
				{
					if (!m_WasCanceled)
					{
						SendOnSubmit();
					}
					DeactivateInputField();
					break;
				}
				m_IsTextComponentUpdateRequired = true;
				UpdateLabel();
			}
			if (flag || (m_IsCompositionActive && compositionLength > 0))
			{
				UpdateLabel();
				eventData.Use();
			}
		}

		public virtual void OnScroll(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (m_LineType == global::TMPro.TMP_InputField.LineType.SingleLine)
			{
				if (m_IScrollHandlerParent != null)
				{
					m_IScrollHandlerParent.OnScroll(eventData);
				}
			}
			else if (!(m_TextComponent.preferredHeight < m_TextViewport.rect.height))
			{
				float num = 0f - eventData.scrollDelta.y;
				m_ScrollPosition = GetScrollPositionRelativeToViewport();
				m_ScrollPosition += 1f / (float)m_TextComponent.textInfo.lineCount * num * m_ScrollSensitivity;
				m_ScrollPosition = global::UnityEngine.Mathf.Clamp01(m_ScrollPosition);
				AdjustTextPositionRelativeToViewport(m_ScrollPosition);
				if ((bool)m_VerticalScrollbar)
				{
					m_VerticalScrollbar.value = m_ScrollPosition;
				}
			}
		}

		private float GetScrollPositionRelativeToViewport()
		{
			global::UnityEngine.Rect rect = m_TextViewport.rect;
			return (float)(int)((m_TextComponent.textInfo.lineInfo[0].ascender + m_TextComponent.margin.y + m_TextComponent.margin.w - rect.yMax + m_TextComponent.rectTransform.anchoredPosition.y) / (m_TextComponent.preferredHeight - rect.height) * 1000f + 0.5f) / 1000f;
		}

		private string GetSelectedString()
		{
			if (!hasSelection)
			{
				return "";
			}
			int num = stringPositionInternal;
			int num2 = stringSelectPositionInternal;
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			return text.Substring(num, num2 - num);
		}

		private int FindNextWordBegin()
		{
			if (stringSelectPositionInternal + 1 >= text.Length)
			{
				return text.Length;
			}
			int num = text.IndexOfAny(kSeparators, stringSelectPositionInternal + 1);
			if (num == -1)
			{
				return text.Length;
			}
			return num + 1;
		}

		private void MoveRight(bool shift, bool ctrl)
		{
			int num;
			if (hasSelection && !shift)
			{
				num = (stringSelectPositionInternal = global::UnityEngine.Mathf.Max(stringPositionInternal, stringSelectPositionInternal));
				stringPositionInternal = num;
				num = (caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
				caretPositionInternal = num;
				return;
			}
			int num3 = (ctrl ? FindNextWordBegin() : (m_isRichTextEditingAllowed ? ((stringSelectPositionInternal >= text.Length || !char.IsHighSurrogate(text[stringSelectPositionInternal])) ? (stringSelectPositionInternal + 1) : (stringSelectPositionInternal + 2)) : ((m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal].character != '\r' || m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal + 1].character != '\n') ? (m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal].stringLength) : (m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal + 1].index + m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal + 1].stringLength))));
			if (shift)
			{
				stringSelectPositionInternal = num3;
				caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
				return;
			}
			num = (stringPositionInternal = num3);
			stringSelectPositionInternal = num;
			if (stringPositionInternal >= m_TextComponent.textInfo.characterInfo[caretPositionInternal].index + m_TextComponent.textInfo.characterInfo[caretPositionInternal].stringLength)
			{
				num = (caretPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
				caretSelectPositionInternal = num;
			}
		}

		private int FindPrevWordBegin()
		{
			if (stringSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int num = text.LastIndexOfAny(kSeparators, stringSelectPositionInternal - 2);
			if (num == -1)
			{
				return 0;
			}
			return num + 1;
		}

		private void MoveLeft(bool shift, bool ctrl)
		{
			int num;
			if (hasSelection && !shift)
			{
				num = (stringSelectPositionInternal = global::UnityEngine.Mathf.Min(stringPositionInternal, stringSelectPositionInternal));
				stringPositionInternal = num;
				num = (caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
				caretPositionInternal = num;
				return;
			}
			int num3;
			if (ctrl)
			{
				num3 = FindPrevWordBegin();
			}
			else if (m_isRichTextEditingAllowed)
			{
				num3 = ((stringSelectPositionInternal <= 0 || !char.IsLowSurrogate(text[stringSelectPositionInternal - 1])) ? (stringSelectPositionInternal - 1) : (stringSelectPositionInternal - 2));
			}
			else
			{
				num3 = ((caretSelectPositionInternal < 1) ? m_TextComponent.textInfo.characterInfo[0].index : m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].index);
				if (num3 > 0 && m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 1].character == '\n' && m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 2].character == '\r')
				{
					num3 = m_TextComponent.textInfo.characterInfo[caretSelectPositionInternal - 2].index;
				}
			}
			if (shift)
			{
				stringSelectPositionInternal = num3;
				caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
				return;
			}
			num = (stringPositionInternal = num3);
			stringSelectPositionInternal = num;
			if (caretPositionInternal > 0 && stringPositionInternal <= m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1].index)
			{
				num = (caretPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal));
				caretSelectPositionInternal = num;
			}
		}

		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			global::TMPro.TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tMP_CharacterInfo.lineNumber;
			if (lineNumber - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			int num = m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex - 1;
			int num2 = -1;
			float num3 = 32767f;
			float num4 = 0f;
			for (int i = m_TextComponent.textInfo.lineInfo[lineNumber - 1].firstCharacterIndex; i < num; i++)
			{
				global::TMPro.TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[i];
				float num5 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
				float num6 = num5 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
				if (num6 >= 0f && num6 <= 1f)
				{
					if (num6 < 0.5f)
					{
						return i;
					}
					return i + 1;
				}
				num5 = global::UnityEngine.Mathf.Abs(num5);
				if (num5 < num3)
				{
					num2 = i;
					num3 = num5;
					num4 = num6;
				}
			}
			if (num2 == -1)
			{
				return num;
			}
			if (num4 < 0.5f)
			{
				return num2;
			}
			return num2 + 1;
		}

		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= m_TextComponent.textInfo.characterCount)
			{
				return m_TextComponent.textInfo.characterCount - 1;
			}
			global::TMPro.TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tMP_CharacterInfo.lineNumber;
			if (lineNumber + 1 >= m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return m_TextComponent.textInfo.characterCount - 1;
			}
			int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[lineNumber + 1].lastCharacterIndex;
			int num = -1;
			float num2 = 32767f;
			float num3 = 0f;
			for (int i = m_TextComponent.textInfo.lineInfo[lineNumber + 1].firstCharacterIndex; i < lastCharacterIndex; i++)
			{
				global::TMPro.TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[i];
				float num4 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
				float num5 = num4 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
				if (num5 >= 0f && num5 <= 1f)
				{
					if (num5 < 0.5f)
					{
						return i;
					}
					return i + 1;
				}
				num4 = global::UnityEngine.Mathf.Abs(num4);
				if (num4 < num2)
				{
					num = i;
					num2 = num4;
					num3 = num5;
				}
			}
			if (num == -1)
			{
				return lastCharacterIndex;
			}
			if (num3 < 0.5f)
			{
				return num;
			}
			return num + 1;
		}

		private int PageUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			global::TMPro.TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tMP_CharacterInfo.lineNumber;
			if (lineNumber - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			float height = m_TextViewport.rect.height;
			int num = lineNumber - 1;
			while (num > 0 && !(m_TextComponent.textInfo.lineInfo[num].baseline > m_TextComponent.textInfo.lineInfo[lineNumber].baseline + height))
			{
				num--;
			}
			int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[num].lastCharacterIndex;
			int num2 = -1;
			float num3 = 32767f;
			float num4 = 0f;
			for (int i = m_TextComponent.textInfo.lineInfo[num].firstCharacterIndex; i < lastCharacterIndex; i++)
			{
				global::TMPro.TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[i];
				float num5 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
				float num6 = num5 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
				if (num6 >= 0f && num6 <= 1f)
				{
					if (num6 < 0.5f)
					{
						return i;
					}
					return i + 1;
				}
				num5 = global::UnityEngine.Mathf.Abs(num5);
				if (num5 < num3)
				{
					num2 = i;
					num3 = num5;
					num4 = num6;
				}
			}
			if (num2 == -1)
			{
				return lastCharacterIndex;
			}
			if (num4 < 0.5f)
			{
				return num2;
			}
			return num2 + 1;
		}

		private int PageDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= m_TextComponent.textInfo.characterCount)
			{
				return m_TextComponent.textInfo.characterCount - 1;
			}
			global::TMPro.TMP_CharacterInfo tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tMP_CharacterInfo.lineNumber;
			if (lineNumber + 1 >= m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return m_TextComponent.textInfo.characterCount - 1;
			}
			float height = m_TextViewport.rect.height;
			int i;
			for (i = lineNumber + 1; i < m_TextComponent.textInfo.lineCount - 1 && !(m_TextComponent.textInfo.lineInfo[i].baseline < m_TextComponent.textInfo.lineInfo[lineNumber].baseline - height); i++)
			{
			}
			int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[i].lastCharacterIndex;
			int num = -1;
			float num2 = 32767f;
			float num3 = 0f;
			for (int j = m_TextComponent.textInfo.lineInfo[i].firstCharacterIndex; j < lastCharacterIndex; j++)
			{
				global::TMPro.TMP_CharacterInfo tMP_CharacterInfo2 = m_TextComponent.textInfo.characterInfo[j];
				float num4 = tMP_CharacterInfo.origin - tMP_CharacterInfo2.origin;
				float num5 = num4 / (tMP_CharacterInfo2.xAdvance - tMP_CharacterInfo2.origin);
				if (num5 >= 0f && num5 <= 1f)
				{
					if (num5 < 0.5f)
					{
						return j;
					}
					return j + 1;
				}
				num4 = global::UnityEngine.Mathf.Abs(num4);
				if (num4 < num2)
				{
					num = j;
					num2 = num4;
					num3 = num5;
				}
			}
			if (num == -1)
			{
				return lastCharacterIndex;
			}
			if (num3 < 0.5f)
			{
				return num;
			}
			return num + 1;
		}

		private void MoveDown(bool shift)
		{
			MoveDown(shift, goToLastChar: true);
		}

		private void MoveDown(bool shift, bool goToLastChar)
		{
			int num;
			if (hasSelection && !shift)
			{
				num = (caretSelectPositionInternal = global::UnityEngine.Mathf.Max(caretPositionInternal, caretSelectPositionInternal));
				caretPositionInternal = num;
			}
			int num3 = (multiLine ? LineDownCharacterPosition(caretSelectPositionInternal, goToLastChar) : (m_TextComponent.textInfo.characterCount - 1));
			if (shift)
			{
				caretSelectPositionInternal = num3;
				stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
				return;
			}
			num = (caretPositionInternal = num3);
			caretSelectPositionInternal = num;
			num = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
			stringSelectPositionInternal = num;
		}

		private void MoveUp(bool shift)
		{
			MoveUp(shift, goToFirstChar: true);
		}

		private void MoveUp(bool shift, bool goToFirstChar)
		{
			int num;
			if (hasSelection && !shift)
			{
				num = (caretSelectPositionInternal = global::UnityEngine.Mathf.Min(caretPositionInternal, caretSelectPositionInternal));
				caretPositionInternal = num;
			}
			int num3 = (multiLine ? LineUpCharacterPosition(caretSelectPositionInternal, goToFirstChar) : 0);
			if (shift)
			{
				caretSelectPositionInternal = num3;
				stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
				return;
			}
			num = (caretPositionInternal = num3);
			caretSelectPositionInternal = num;
			num = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
			stringSelectPositionInternal = num;
		}

		private void MovePageUp(bool shift)
		{
			MovePageUp(shift, goToFirstChar: true);
		}

		private void MovePageUp(bool shift, bool goToFirstChar)
		{
			if (hasSelection && !shift)
			{
				int num = (caretSelectPositionInternal = global::UnityEngine.Mathf.Min(caretPositionInternal, caretSelectPositionInternal));
				caretPositionInternal = num;
			}
			int num3 = (multiLine ? PageUpCharacterPosition(caretSelectPositionInternal, goToFirstChar) : 0);
			if (shift)
			{
				caretSelectPositionInternal = num3;
				stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
			}
			else
			{
				int num = (caretPositionInternal = num3);
				caretSelectPositionInternal = num;
				num = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
				stringSelectPositionInternal = num;
			}
			if (m_LineType != global::TMPro.TMP_InputField.LineType.SingleLine)
			{
				float height = m_TextViewport.rect.height;
				float num5 = m_TextComponent.rectTransform.position.y + m_TextComponent.textBounds.max.y;
				float num6 = m_TextViewport.position.y + m_TextViewport.rect.yMax;
				height = ((num6 > num5 + height) ? height : (num6 - num5));
				m_TextComponent.rectTransform.anchoredPosition += new global::UnityEngine.Vector2(0f, height);
				AssignPositioningIfNeeded();
			}
		}

		private void MovePageDown(bool shift)
		{
			MovePageDown(shift, goToLastChar: true);
		}

		private void MovePageDown(bool shift, bool goToLastChar)
		{
			if (hasSelection && !shift)
			{
				int num = (caretSelectPositionInternal = global::UnityEngine.Mathf.Max(caretPositionInternal, caretSelectPositionInternal));
				caretPositionInternal = num;
			}
			int num3 = (multiLine ? PageDownCharacterPosition(caretSelectPositionInternal, goToLastChar) : (m_TextComponent.textInfo.characterCount - 1));
			if (shift)
			{
				caretSelectPositionInternal = num3;
				stringSelectPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal);
			}
			else
			{
				int num = (caretPositionInternal = num3);
				caretSelectPositionInternal = num;
				num = (stringPositionInternal = GetStringIndexFromCaretPosition(caretSelectPositionInternal));
				stringSelectPositionInternal = num;
			}
			if (m_LineType != global::TMPro.TMP_InputField.LineType.SingleLine)
			{
				float height = m_TextViewport.rect.height;
				float num5 = m_TextComponent.rectTransform.position.y + m_TextComponent.textBounds.min.y;
				float num6 = m_TextViewport.position.y + m_TextViewport.rect.yMin;
				height = ((num6 > num5 + height) ? height : (num6 - num5));
				m_TextComponent.rectTransform.anchoredPosition += new global::UnityEngine.Vector2(0f, height);
				AssignPositioningIfNeeded();
			}
		}

		private void Delete()
		{
			if (m_ReadOnly || m_StringPosition == m_StringSelectPosition)
			{
				return;
			}
			if (m_isRichTextEditingAllowed || m_isSelectAll)
			{
				if (m_StringPosition < m_StringSelectPosition)
				{
					m_Text = text.Remove(m_StringPosition, m_StringSelectPosition - m_StringPosition);
					m_StringSelectPosition = m_StringPosition;
				}
				else
				{
					m_Text = text.Remove(m_StringSelectPosition, m_StringPosition - m_StringSelectPosition);
					m_StringPosition = m_StringSelectPosition;
				}
				if (m_isSelectAll)
				{
					m_CaretPosition = (m_CaretSelectPosition = 0);
					m_isSelectAll = false;
				}
			}
			else if (m_CaretPosition < m_CaretSelectPosition)
			{
				int num = ClampArrayIndex(m_CaretSelectPosition - 1);
				m_StringPosition = m_TextComponent.textInfo.characterInfo[m_CaretPosition].index;
				m_StringSelectPosition = m_TextComponent.textInfo.characterInfo[num].index + m_TextComponent.textInfo.characterInfo[num].stringLength;
				m_Text = text.Remove(m_StringPosition, m_StringSelectPosition - m_StringPosition);
				m_StringSelectPosition = m_StringPosition;
				m_CaretSelectPosition = m_CaretPosition;
			}
			else
			{
				int num2 = ClampArrayIndex(m_CaretPosition - 1);
				m_StringPosition = m_TextComponent.textInfo.characterInfo[num2].index + m_TextComponent.textInfo.characterInfo[num2].stringLength;
				m_StringSelectPosition = m_TextComponent.textInfo.characterInfo[m_CaretSelectPosition].index;
				m_Text = text.Remove(m_StringSelectPosition, m_StringPosition - m_StringSelectPosition);
				m_StringPosition = m_StringSelectPosition;
				m_CaretPosition = m_CaretSelectPosition;
			}
		}

		private void DeleteKey()
		{
			if (m_ReadOnly)
			{
				return;
			}
			if (hasSelection)
			{
				m_HasTextBeenRemoved = true;
				Delete();
				UpdateTouchKeyboardFromEditChanges();
				SendOnValueChangedAndUpdateLabel();
			}
			else if (m_isRichTextEditingAllowed)
			{
				if (stringPositionInternal < text.Length)
				{
					if (char.IsHighSurrogate(text[stringPositionInternal]))
					{
						m_Text = text.Remove(stringPositionInternal, 2);
					}
					else
					{
						m_Text = text.Remove(stringPositionInternal, 1);
					}
					m_HasTextBeenRemoved = true;
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (caretPositionInternal < m_TextComponent.textInfo.characterCount - 1)
			{
				int num = m_TextComponent.textInfo.characterInfo[caretPositionInternal].stringLength;
				if (m_TextComponent.textInfo.characterInfo[caretPositionInternal].character == '\r' && m_TextComponent.textInfo.characterInfo[caretPositionInternal + 1].character == '\n')
				{
					num += m_TextComponent.textInfo.characterInfo[caretPositionInternal + 1].stringLength;
				}
				int index = m_TextComponent.textInfo.characterInfo[caretPositionInternal].index;
				m_Text = text.Remove(index, num);
				m_HasTextBeenRemoved = true;
				SendOnValueChangedAndUpdateLabel();
			}
		}

		private void Backspace()
		{
			if (m_ReadOnly)
			{
				return;
			}
			if (hasSelection)
			{
				m_HasTextBeenRemoved = true;
				Delete();
				UpdateTouchKeyboardFromEditChanges();
				SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (m_isRichTextEditingAllowed)
			{
				if (stringPositionInternal > 0)
				{
					int num = 1;
					if (char.IsLowSurrogate(text[stringPositionInternal - 1]))
					{
						num = 2;
					}
					stringSelectPositionInternal = (stringPositionInternal -= num);
					m_Text = text.Remove(stringPositionInternal, num);
					caretSelectPositionInternal = --caretPositionInternal;
					m_HasTextBeenRemoved = true;
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChangedAndUpdateLabel();
				}
				return;
			}
			if (caretPositionInternal > 0)
			{
				int num2 = caretPositionInternal - 1;
				int num3 = m_TextComponent.textInfo.characterInfo[num2].stringLength;
				if (num2 > 0 && m_TextComponent.textInfo.characterInfo[num2].character == '\n' && m_TextComponent.textInfo.characterInfo[num2 - 1].character == '\r')
				{
					num3 += m_TextComponent.textInfo.characterInfo[num2 - 1].stringLength;
					num2--;
				}
				m_Text = text.Remove(m_TextComponent.textInfo.characterInfo[num2].index, num3);
				int num4 = (stringPositionInternal = ((caretPositionInternal < 1) ? m_TextComponent.textInfo.characterInfo[0].index : m_TextComponent.textInfo.characterInfo[num2].index));
				stringSelectPositionInternal = num4;
				num4 = (caretPositionInternal = num2);
				caretSelectPositionInternal = num4;
			}
			m_HasTextBeenRemoved = true;
			UpdateTouchKeyboardFromEditChanges();
			SendOnValueChangedAndUpdateLabel();
		}

		protected virtual void Append(string input)
		{
			if (m_ReadOnly || !InPlaceEditing())
			{
				return;
			}
			int i = 0;
			for (int length = input.Length; i < length; i++)
			{
				char c = input[i];
				if (c >= ' ' || c == '\t' || c == '\r' || c == '\n')
				{
					Append(c);
				}
			}
		}

		protected virtual void Append(char input)
		{
			if (m_ReadOnly || !InPlaceEditing())
			{
				return;
			}
			int num = global::UnityEngine.Mathf.Min(stringPositionInternal, stringSelectPositionInternal);
			string text = this.text;
			if (selectionFocusPosition != selectionAnchorPosition)
			{
				m_HasTextBeenRemoved = true;
				if (m_isRichTextEditingAllowed || m_isSelectAll)
				{
					text = ((m_StringPosition >= m_StringSelectPosition) ? this.text.Remove(m_StringSelectPosition, m_StringPosition - m_StringSelectPosition) : this.text.Remove(m_StringPosition, m_StringSelectPosition - m_StringPosition));
				}
				else if (m_CaretPosition < m_CaretSelectPosition)
				{
					m_StringPosition = m_TextComponent.textInfo.characterInfo[m_CaretPosition].index;
					m_StringSelectPosition = m_TextComponent.textInfo.characterInfo[m_CaretSelectPosition - 1].index + m_TextComponent.textInfo.characterInfo[m_CaretSelectPosition - 1].stringLength;
					text = this.text.Remove(m_StringPosition, m_StringSelectPosition - m_StringPosition);
				}
				else
				{
					m_StringPosition = m_TextComponent.textInfo.characterInfo[m_CaretPosition - 1].index + m_TextComponent.textInfo.characterInfo[m_CaretPosition - 1].stringLength;
					m_StringSelectPosition = m_TextComponent.textInfo.characterInfo[m_CaretSelectPosition].index;
					text = this.text.Remove(m_StringSelectPosition, m_StringPosition - m_StringSelectPosition);
				}
			}
			if (onValidateInput != null)
			{
				input = onValidateInput(text, num, input);
			}
			else
			{
				if (characterValidation == global::TMPro.TMP_InputField.CharacterValidation.CustomValidator)
				{
					input = Validate(text, num, input);
					if (input != 0)
					{
						if (!char.IsHighSurrogate(input))
						{
							m_CaretSelectPosition = ++m_CaretPosition;
						}
						SendOnValueChanged();
						UpdateLabel();
					}
					return;
				}
				if (characterValidation != global::TMPro.TMP_InputField.CharacterValidation.None)
				{
					input = Validate(text, num, input);
				}
			}
			if (input != 0)
			{
				Insert(input);
			}
		}

		private void Insert(char c)
		{
			if (m_ReadOnly)
			{
				return;
			}
			string value = c.ToString();
			Delete();
			if (characterLimit <= 0 || text.Length < characterLimit)
			{
				m_Text = text.Insert(m_StringPosition, value);
				if (!char.IsHighSurrogate(c))
				{
					m_CaretSelectPosition = ++m_CaretPosition;
				}
				m_StringSelectPosition = ++m_StringPosition;
				UpdateTouchKeyboardFromEditChanges();
				SendOnValueChanged();
			}
		}

		private void UpdateTouchKeyboardFromEditChanges()
		{
			if (m_SoftKeyboard != null && InPlaceEditing())
			{
				m_SoftKeyboard.text = m_Text;
			}
		}

		private void SendOnValueChangedAndUpdateLabel()
		{
			UpdateLabel();
			SendOnValueChanged();
		}

		private void SendOnValueChanged()
		{
			if (onValueChanged != null)
			{
				onValueChanged.Invoke(text);
			}
		}

		protected void SendOnEndEdit()
		{
			if (onEndEdit != null)
			{
				onEndEdit.Invoke(m_Text);
			}
		}

		protected void SendOnSubmit()
		{
			if (onSubmit != null)
			{
				onSubmit.Invoke(m_Text);
			}
		}

		protected void SendOnFocus()
		{
			if (onSelect != null)
			{
				onSelect.Invoke(m_Text);
			}
		}

		protected void SendOnFocusLost()
		{
			if (onDeselect != null)
			{
				onDeselect.Invoke(m_Text);
			}
		}

		protected void SendOnTextSelection()
		{
			m_isSelected = true;
			if (onTextSelection != null)
			{
				onTextSelection.Invoke(m_Text, stringPositionInternal, stringSelectPositionInternal);
			}
		}

		protected void SendOnEndTextSelection()
		{
			if (m_isSelected)
			{
				if (onEndTextSelection != null)
				{
					onEndTextSelection.Invoke(m_Text, stringPositionInternal, stringSelectPositionInternal);
				}
				m_isSelected = false;
			}
		}

		protected void SendTouchScreenKeyboardStatusChanged()
		{
			if (m_SoftKeyboard != null && onTouchScreenKeyboardStatusChanged != null)
			{
				onTouchScreenKeyboardStatusChanged.Invoke(m_SoftKeyboard.status);
			}
		}

		protected void UpdateLabel()
		{
			if (!(m_TextComponent != null) || !(m_TextComponent.font != null) || m_PreventCallback)
			{
				return;
			}
			m_PreventCallback = true;
			string text;
			if (compositionLength > 0 && !m_ReadOnly)
			{
				Delete();
				text = ((!m_RichText) ? (this.text.Substring(0, m_StringPosition) + compositionString + this.text.Substring(m_StringPosition)) : (this.text.Substring(0, m_StringPosition) + "<u>" + compositionString + "</u>" + this.text.Substring(m_StringPosition)));
				m_IsCompositionActive = true;
			}
			else
			{
				text = this.text;
				m_IsCompositionActive = false;
				m_ShouldUpdateIMEWindowPosition = true;
			}
			string text2 = ((inputType != global::TMPro.TMP_InputField.InputType.Password) ? text : new string(asteriskChar, text.Length));
			bool flag = string.IsNullOrEmpty(text);
			if (m_Placeholder != null)
			{
				m_Placeholder.enabled = flag;
			}
			if (!flag && !m_ReadOnly)
			{
				SetCaretVisible();
			}
			m_TextComponent.text = text2 + "\u200b";
			if (m_IsDrivenByLayoutComponents)
			{
				global::UnityEngine.UI.LayoutRebuilder.MarkLayoutForRebuild(m_RectTransform);
			}
			if (m_LineLimit > 0)
			{
				m_TextComponent.ForceMeshUpdate();
				global::TMPro.TMP_TextInfo textInfo = m_TextComponent.textInfo;
				if (textInfo != null && textInfo.lineCount > m_LineLimit)
				{
					int lastCharacterIndex = textInfo.lineInfo[m_LineLimit - 1].lastCharacterIndex;
					int num = textInfo.characterInfo[lastCharacterIndex].index + textInfo.characterInfo[lastCharacterIndex].stringLength;
					this.text = text2.Remove(num, text2.Length - num);
					m_TextComponent.text = this.text + "\u200b";
				}
			}
			if (m_IsTextComponentUpdateRequired || ((bool)m_VerticalScrollbar && (!m_IsCaretPositionDirty || !m_IsStringPositionDirty)))
			{
				m_IsTextComponentUpdateRequired = false;
				m_TextComponent.ForceMeshUpdate();
			}
			MarkGeometryAsDirty();
			m_PreventCallback = false;
		}

		private void UpdateScrollbar()
		{
			if ((bool)m_VerticalScrollbar)
			{
				float size = m_TextViewport.rect.height / m_TextComponent.preferredHeight;
				m_VerticalScrollbar.size = size;
				m_VerticalScrollbar.value = GetScrollPositionRelativeToViewport();
			}
		}

		private void OnScrollbarValueChange(float value)
		{
			if (!(value < 0f) && !(value > 1f))
			{
				AdjustTextPositionRelativeToViewport(value);
				m_ScrollPosition = value;
			}
		}

		private void UpdateMaskRegions()
		{
		}

		private void AdjustTextPositionRelativeToViewport(float relativePosition)
		{
			if (m_TextViewport == null)
			{
				return;
			}
			global::TMPro.TMP_TextInfo textInfo = m_TextComponent.textInfo;
			if (textInfo != null && textInfo.lineInfo != null && textInfo.lineCount != 0 && textInfo.lineCount <= textInfo.lineInfo.Length)
			{
				float num = 0f;
				float y = m_TextComponent.preferredHeight;
				switch (m_TextComponent.verticalAlignment)
				{
				case global::TMPro.VerticalAlignmentOptions.Top:
					num = 0f;
					break;
				case global::TMPro.VerticalAlignmentOptions.Middle:
					num = 0.5f;
					break;
				case global::TMPro.VerticalAlignmentOptions.Bottom:
					num = 1f;
					break;
				case global::TMPro.VerticalAlignmentOptions.Geometry:
					num = 0.5f;
					y = m_TextComponent.bounds.size.y;
					break;
				case global::TMPro.VerticalAlignmentOptions.Capline:
					num = 0.5f;
					break;
				}
				m_TextComponent.rectTransform.anchoredPosition = new global::UnityEngine.Vector2(m_TextComponent.rectTransform.anchoredPosition.x, (y - m_TextViewport.rect.height) * (relativePosition - num));
				AssignPositioningIfNeeded();
			}
		}

		private int GetCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return characterCount;
		}

		private int GetMinCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (stringIndex < m_TextComponent.textInfo.characterInfo[i].index + m_TextComponent.textInfo.characterInfo[i].stringLength)
				{
					return i;
				}
			}
			return characterCount;
		}

		private int GetMaxCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return characterCount;
		}

		private int GetStringIndexFromCaretPosition(int caretPosition)
		{
			ClampCaretPos(ref caretPosition);
			return m_TextComponent.textInfo.characterInfo[caretPosition].index;
		}

		private void UpdateStringIndexFromCaretPosition()
		{
			stringPositionInternal = GetStringIndexFromCaretPosition(m_CaretPosition);
			stringSelectPositionInternal = GetStringIndexFromCaretPosition(m_CaretSelectPosition);
			m_IsStringPositionDirty = false;
		}

		private void UpdateCaretPositionFromStringIndex()
		{
			caretPositionInternal = GetCaretPositionFromStringIndex(stringPositionInternal);
			caretSelectPositionInternal = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
			m_IsCaretPositionDirty = false;
		}

		public void ForceLabelUpdate()
		{
			UpdateLabel();
		}

		private void MarkGeometryAsDirty()
		{
			global::UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		public virtual void Rebuild(global::UnityEngine.UI.CanvasUpdate update)
		{
			if (update == global::UnityEngine.UI.CanvasUpdate.LatePreRender)
			{
				UpdateGeometry();
			}
		}

		public virtual void LayoutComplete()
		{
		}

		public virtual void GraphicUpdateComplete()
		{
		}

		private void UpdateGeometry()
		{
			if ((InPlaceEditing() || isUWP()) && !(m_CachedInputRenderer == null))
			{
				OnFillVBO(mesh);
				m_CachedInputRenderer.SetMesh(mesh);
			}
		}

		private void AssignPositioningIfNeeded()
		{
			if (m_TextComponent != null && caretRectTrans != null && (caretRectTrans.localPosition != m_TextComponent.rectTransform.localPosition || caretRectTrans.localRotation != m_TextComponent.rectTransform.localRotation || caretRectTrans.localScale != m_TextComponent.rectTransform.localScale || caretRectTrans.anchorMin != m_TextComponent.rectTransform.anchorMin || caretRectTrans.anchorMax != m_TextComponent.rectTransform.anchorMax || caretRectTrans.anchoredPosition != m_TextComponent.rectTransform.anchoredPosition || caretRectTrans.sizeDelta != m_TextComponent.rectTransform.sizeDelta || caretRectTrans.pivot != m_TextComponent.rectTransform.pivot))
			{
				caretRectTrans.localPosition = m_TextComponent.rectTransform.localPosition;
				caretRectTrans.localRotation = m_TextComponent.rectTransform.localRotation;
				caretRectTrans.localScale = m_TextComponent.rectTransform.localScale;
				caretRectTrans.anchorMin = m_TextComponent.rectTransform.anchorMin;
				caretRectTrans.anchorMax = m_TextComponent.rectTransform.anchorMax;
				caretRectTrans.anchoredPosition = m_TextComponent.rectTransform.anchoredPosition;
				caretRectTrans.sizeDelta = m_TextComponent.rectTransform.sizeDelta;
				caretRectTrans.pivot = m_TextComponent.rectTransform.pivot;
			}
		}

		private void OnFillVBO(global::UnityEngine.Mesh vbo)
		{
			using global::UnityEngine.UI.VertexHelper vertexHelper = new global::UnityEngine.UI.VertexHelper();
			if (!isFocused && !m_SelectionStillActive)
			{
				vertexHelper.FillMesh(vbo);
				return;
			}
			if (m_IsStringPositionDirty)
			{
				UpdateStringIndexFromCaretPosition();
			}
			if (m_IsCaretPositionDirty)
			{
				UpdateCaretPositionFromStringIndex();
			}
			if (!hasSelection)
			{
				GenerateCaret(vertexHelper, global::UnityEngine.Vector2.zero);
				SendOnEndTextSelection();
			}
			else
			{
				GenerateHighlight(vertexHelper, global::UnityEngine.Vector2.zero);
				SendOnTextSelection();
			}
			vertexHelper.FillMesh(vbo);
		}

		private void GenerateCaret(global::UnityEngine.UI.VertexHelper vbo, global::UnityEngine.Vector2 roundingOffset)
		{
			if (!m_CaretVisible || m_TextComponent.canvas == null || m_ReadOnly)
			{
				return;
			}
			if (m_CursorVerts == null)
			{
				CreateCursorVerts();
			}
			global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
			float num = 0f;
			if (caretPositionInternal >= m_TextComponent.textInfo.characterInfo.Length || caretPositionInternal < 0)
			{
				return;
			}
			int lineNumber = m_TextComponent.textInfo.characterInfo[caretPositionInternal].lineNumber;
			global::TMPro.TMP_CharacterInfo tMP_CharacterInfo;
			if (caretPositionInternal == m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex)
			{
				tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[caretPositionInternal];
				num = tMP_CharacterInfo.ascender - tMP_CharacterInfo.descender;
				zero = ((m_TextComponent.verticalAlignment != global::TMPro.VerticalAlignmentOptions.Geometry) ? new global::UnityEngine.Vector2(tMP_CharacterInfo.origin, tMP_CharacterInfo.descender) : new global::UnityEngine.Vector2(tMP_CharacterInfo.origin, 0f - num / 2f));
			}
			else
			{
				tMP_CharacterInfo = m_TextComponent.textInfo.characterInfo[caretPositionInternal - 1];
				num = tMP_CharacterInfo.ascender - tMP_CharacterInfo.descender;
				zero = ((m_TextComponent.verticalAlignment != global::TMPro.VerticalAlignmentOptions.Geometry) ? new global::UnityEngine.Vector2(tMP_CharacterInfo.xAdvance, tMP_CharacterInfo.descender) : new global::UnityEngine.Vector2(tMP_CharacterInfo.xAdvance, 0f - num / 2f));
			}
			if (m_SoftKeyboard != null && compositionLength == 0)
			{
				int num2 = m_StringPosition;
				int num3 = ((m_SoftKeyboard.text != null) ? m_SoftKeyboard.text.Length : 0);
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num2 > num3)
				{
					num2 = num3;
				}
				m_SoftKeyboard.selection = new global::UnityEngine.RangeInt(num2, 0);
			}
			if ((isFocused && zero != m_LastPosition) || m_forceRectTransformAdjustment || m_HasTextBeenRemoved)
			{
				AdjustRectTransformRelativeToViewport(zero, num, tMP_CharacterInfo.isVisible);
			}
			m_LastPosition = zero;
			float num4 = zero.y + num;
			float y = num4 - num;
			global::TMPro.TMP_FontAsset font = m_TextComponent.font;
			float num5 = m_TextComponent.fontSize / font.m_FaceInfo.pointSize * font.m_FaceInfo.scale;
			float a = (float)m_CaretWidth * font.faceInfo.lineHeight * num5 * 0.05f;
			a = global::UnityEngine.Mathf.Max(a, 1f);
			m_CursorVerts[0].position = new global::UnityEngine.Vector3(zero.x, y, 0f);
			m_CursorVerts[1].position = new global::UnityEngine.Vector3(zero.x, num4, 0f);
			m_CursorVerts[2].position = new global::UnityEngine.Vector3(zero.x + a, num4, 0f);
			m_CursorVerts[3].position = new global::UnityEngine.Vector3(zero.x + a, y, 0f);
			m_CursorVerts[0].color = caretColor;
			m_CursorVerts[1].color = caretColor;
			m_CursorVerts[2].color = caretColor;
			m_CursorVerts[3].color = caretColor;
			vbo.AddUIVertexQuad(m_CursorVerts);
			if (!m_ShouldUpdateIMEWindowPosition && lineNumber == m_PreviousIMEInsertionLine)
			{
				return;
			}
			m_ShouldUpdateIMEWindowPosition = false;
			m_PreviousIMEInsertionLine = lineNumber;
			global::UnityEngine.Camera camera;
			if (m_TextComponent.canvas.renderMode == global::UnityEngine.RenderMode.ScreenSpaceOverlay)
			{
				camera = null;
			}
			else
			{
				camera = m_TextComponent.canvas.worldCamera;
				if (camera == null)
				{
					camera = global::UnityEngine.Camera.current;
				}
			}
			global::UnityEngine.Vector3 worldPoint = m_CachedInputRenderer.gameObject.transform.TransformPoint(m_CursorVerts[0].position);
			global::UnityEngine.Vector2 compositionCursorPos = global::UnityEngine.RectTransformUtility.WorldToScreenPoint(camera, worldPoint);
			compositionCursorPos.y = (float)global::UnityEngine.Screen.height - compositionCursorPos.y;
			if (inputSystem != null)
			{
				inputSystem.compositionCursorPos = compositionCursorPos;
			}
		}

		private void CreateCursorVerts()
		{
			m_CursorVerts = new global::UnityEngine.UIVertex[4];
			for (int i = 0; i < m_CursorVerts.Length; i++)
			{
				m_CursorVerts[i] = global::UnityEngine.UIVertex.simpleVert;
				m_CursorVerts[i].uv0 = global::UnityEngine.Vector2.zero;
			}
		}

		private void GenerateHighlight(global::UnityEngine.UI.VertexHelper vbo, global::UnityEngine.Vector2 roundingOffset)
		{
			UpdateMaskRegions();
			global::TMPro.TMP_TextInfo textInfo = m_TextComponent.textInfo;
			if (textInfo.characterCount == 0)
			{
				return;
			}
			m_CaretPosition = GetCaretPositionFromStringIndex(stringPositionInternal);
			m_CaretSelectPosition = GetCaretPositionFromStringIndex(stringSelectPositionInternal);
			if (m_SoftKeyboard != null && compositionLength == 0)
			{
				int num = ((m_CaretPosition < m_CaretSelectPosition) ? textInfo.characterInfo[m_CaretPosition].index : textInfo.characterInfo[m_CaretSelectPosition].index);
				int length = ((m_CaretPosition < m_CaretSelectPosition) ? (stringSelectPositionInternal - num) : (stringPositionInternal - num));
				m_SoftKeyboard.selection = new global::UnityEngine.RangeInt(num, length);
			}
			float num2 = 0f;
			global::UnityEngine.Vector2 startPosition;
			if (m_CaretSelectPosition < textInfo.characterCount)
			{
				startPosition = new global::UnityEngine.Vector2(textInfo.characterInfo[m_CaretSelectPosition].origin, textInfo.characterInfo[m_CaretSelectPosition].descender);
				num2 = textInfo.characterInfo[m_CaretSelectPosition].ascender - textInfo.characterInfo[m_CaretSelectPosition].descender;
			}
			else
			{
				startPosition = new global::UnityEngine.Vector2(textInfo.characterInfo[m_CaretSelectPosition - 1].xAdvance, textInfo.characterInfo[m_CaretSelectPosition - 1].descender);
				num2 = textInfo.characterInfo[m_CaretSelectPosition - 1].ascender - textInfo.characterInfo[m_CaretSelectPosition - 1].descender;
			}
			AdjustRectTransformRelativeToViewport(startPosition, num2, isCharVisible: true);
			int num3 = global::UnityEngine.Mathf.Max(0, m_CaretPosition);
			int num4 = global::UnityEngine.Mathf.Max(0, m_CaretSelectPosition);
			if (num3 > num4)
			{
				int num5 = num3;
				num3 = num4;
				num4 = num5;
			}
			num4--;
			int num6 = textInfo.characterInfo[num3].lineNumber;
			int lastCharacterIndex = textInfo.lineInfo[num6].lastCharacterIndex;
			global::UnityEngine.UIVertex simpleVert = global::UnityEngine.UIVertex.simpleVert;
			simpleVert.uv0 = global::UnityEngine.Vector2.zero;
			simpleVert.color = selectionColor;
			for (int i = num3; i <= num4 && i < textInfo.characterCount; i++)
			{
				if (i == lastCharacterIndex || i == num4)
				{
					global::TMPro.TMP_CharacterInfo tMP_CharacterInfo = textInfo.characterInfo[num3];
					global::TMPro.TMP_CharacterInfo tMP_CharacterInfo2 = textInfo.characterInfo[i];
					if (i > 0 && tMP_CharacterInfo2.character == '\n' && textInfo.characterInfo[i - 1].character == '\r')
					{
						tMP_CharacterInfo2 = textInfo.characterInfo[i - 1];
					}
					global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(tMP_CharacterInfo.origin, textInfo.lineInfo[num6].ascender);
					global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2(tMP_CharacterInfo2.xAdvance, textInfo.lineInfo[num6].descender);
					int currentVertCount = vbo.currentVertCount;
					simpleVert.position = new global::UnityEngine.Vector3(vector.x, vector2.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new global::UnityEngine.Vector3(vector2.x, vector2.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new global::UnityEngine.Vector3(vector2.x, vector.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new global::UnityEngine.Vector3(vector.x, vector.y, 0f);
					vbo.AddVert(simpleVert);
					vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
					vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
					num3 = i + 1;
					num6++;
					if (num6 < textInfo.lineCount)
					{
						lastCharacterIndex = textInfo.lineInfo[num6].lastCharacterIndex;
					}
				}
			}
		}

		private void AdjustRectTransformRelativeToViewport(global::UnityEngine.Vector2 startPosition, float height, bool isCharVisible)
		{
			if (m_TextViewport == null)
			{
				return;
			}
			global::UnityEngine.Vector3 localPosition = base.transform.localPosition;
			global::UnityEngine.Vector3 localPosition2 = m_TextComponent.rectTransform.localPosition;
			global::UnityEngine.Vector3 localPosition3 = m_TextViewport.localPosition;
			global::UnityEngine.Rect rect = m_TextViewport.rect;
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(startPosition.x + localPosition2.x + localPosition3.x + localPosition.x, startPosition.y + localPosition2.y + localPosition3.y + localPosition.y);
			global::UnityEngine.Rect rect2 = new global::UnityEngine.Rect(localPosition.x + localPosition3.x + rect.x, localPosition.y + localPosition3.y + rect.y, rect.width, rect.height);
			float num = rect2.xMax - (vector.x + m_TextComponent.margin.z + (float)m_CaretWidth);
			if (num < 0f && (!multiLine || (multiLine && isCharVisible)))
			{
				m_TextComponent.rectTransform.anchoredPosition += new global::UnityEngine.Vector2(num, 0f);
				AssignPositioningIfNeeded();
			}
			float num2 = vector.x - m_TextComponent.margin.x - rect2.xMin;
			if (num2 < 0f)
			{
				m_TextComponent.rectTransform.anchoredPosition += new global::UnityEngine.Vector2(0f - num2, 0f);
				AssignPositioningIfNeeded();
			}
			if (m_LineType != global::TMPro.TMP_InputField.LineType.SingleLine)
			{
				float num3 = rect2.yMax - (vector.y + height);
				if (num3 < -0.0001f)
				{
					m_TextComponent.rectTransform.anchoredPosition += new global::UnityEngine.Vector2(0f, num3);
					AssignPositioningIfNeeded();
				}
				float num4 = vector.y - rect2.yMin;
				if (num4 < 0f)
				{
					m_TextComponent.rectTransform.anchoredPosition -= new global::UnityEngine.Vector2(0f, num4);
					AssignPositioningIfNeeded();
				}
			}
			if (m_HasTextBeenRemoved)
			{
				float x = m_TextComponent.rectTransform.anchoredPosition.x;
				float num5 = localPosition.x + localPosition3.x + localPosition2.x + m_TextComponent.textInfo.characterInfo[0].origin - m_TextComponent.margin.x;
				int num6 = ClampArrayIndex(m_TextComponent.textInfo.characterCount - 1);
				float num7 = localPosition.x + localPosition3.x + localPosition2.x + m_TextComponent.textInfo.characterInfo[num6].origin + m_TextComponent.margin.z + (float)m_CaretWidth;
				if (x > 0.0001f && num5 > rect2.xMin)
				{
					float num8 = rect2.xMin - num5;
					if (x < 0f - num8)
					{
						num8 = 0f - x;
					}
					m_TextComponent.rectTransform.anchoredPosition += new global::UnityEngine.Vector2(num8, 0f);
					AssignPositioningIfNeeded();
				}
				else if (x < -0.0001f && num7 < rect2.xMax)
				{
					float num9 = rect2.xMax - num7;
					if (0f - x < num9)
					{
						num9 = 0f - x;
					}
					m_TextComponent.rectTransform.anchoredPosition += new global::UnityEngine.Vector2(num9, 0f);
					AssignPositioningIfNeeded();
				}
				m_HasTextBeenRemoved = false;
			}
			m_forceRectTransformAdjustment = false;
		}

		protected char Validate(string text, int pos, char ch)
		{
			if (characterValidation == global::TMPro.TMP_InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (characterValidation == global::TMPro.TMP_InputField.CharacterValidation.Integer || characterValidation == global::TMPro.TMP_InputField.CharacterValidation.Decimal)
			{
				bool num = pos == 0 && text.Length > 0 && text[0] == '-';
				bool flag = stringPositionInternal == 0 || stringSelectPositionInternal == 0;
				if (!num)
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && (pos == 0 || flag) && !text.Contains('-'))
					{
						return ch;
					}
					string numberDecimalSeparator = global::System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
					if (ch == global::System.Convert.ToChar(numberDecimalSeparator) && characterValidation == global::TMPro.TMP_InputField.CharacterValidation.Decimal && !text.Contains(numberDecimalSeparator))
					{
						return ch;
					}
					if (characterValidation == global::TMPro.TMP_InputField.CharacterValidation.Integer && ch == '.' && (pos == 0 || flag) && !text.Contains('-'))
					{
						return '-';
					}
				}
			}
			else if (characterValidation == global::TMPro.TMP_InputField.CharacterValidation.Digit)
			{
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (characterValidation == global::TMPro.TMP_InputField.CharacterValidation.Alphanumeric)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (characterValidation == global::TMPro.TMP_InputField.CharacterValidation.Name)
			{
				char c = ((text.Length > 0) ? text[global::UnityEngine.Mathf.Clamp(pos - 1, 0, text.Length - 1)] : ' ');
				char c2 = ((text.Length > 0) ? text[global::UnityEngine.Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
				char c3 = ((text.Length > 0) ? text[global::UnityEngine.Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && pos == 0)
					{
						return char.ToUpper(ch);
					}
					if (char.IsLower(ch) && (c == ' ' || c == '-'))
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && pos > 0 && c != ' ' && c != '\'' && c != '-' && !char.IsLower(c))
					{
						return char.ToLower(ch);
					}
					if (char.IsUpper(ch) && char.IsUpper(c2))
					{
						return '\0';
					}
					return ch;
				}
				if (ch == '\'' && c2 != ' ' && c2 != '\'' && c3 != '\'' && !text.Contains("'"))
				{
					return ch;
				}
				if (char.IsLetter(c) && ch == '-' && c2 != '-')
				{
					return ch;
				}
				if ((ch == ' ' || ch == '-') && pos != 0 && c != ' ' && c != '\'' && c != '-' && c2 != ' ' && c2 != '\'' && c2 != '-' && c3 != ' ' && c3 != '\'' && c3 != '-')
				{
					return ch;
				}
			}
			else if (characterValidation == global::TMPro.TMP_InputField.CharacterValidation.EmailAddress)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
				if (ch == '@' && text.IndexOf('@') == -1)
				{
					return ch;
				}
				if ("!#$%&'*+-/=?^_`{|}~".IndexOf(ch) != -1)
				{
					return ch;
				}
				if (ch == '.')
				{
					char num2 = ((text.Length > 0) ? text[global::UnityEngine.Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
					char c4 = ((text.Length > 0) ? text[global::UnityEngine.Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
					if (num2 != '.' && c4 != '.')
					{
						return ch;
					}
				}
			}
			else if (characterValidation == global::TMPro.TMP_InputField.CharacterValidation.Regex)
			{
				if (global::System.Text.RegularExpressions.Regex.IsMatch(ch.ToString(), m_RegexValue))
				{
					return ch;
				}
			}
			else if (characterValidation == global::TMPro.TMP_InputField.CharacterValidation.CustomValidator && m_InputValidator != null)
			{
				char result = m_InputValidator.Validate(ref text, ref pos, ch);
				m_Text = text;
				int num3 = (stringPositionInternal = pos);
				stringSelectPositionInternal = num3;
				return result;
			}
			return '\0';
		}

		public void ActivateInputField()
		{
			if (!(m_TextComponent == null) && !(m_TextComponent.font == null) && IsActive() && IsInteractable())
			{
				if (isFocused && m_SoftKeyboard != null && !m_SoftKeyboard.active)
				{
					m_SoftKeyboard.active = true;
					m_SoftKeyboard.text = m_Text;
				}
				m_ShouldActivateNextUpdate = true;
			}
		}

		private void ActivateInputFieldInternal()
		{
			if (global::UnityEngine.EventSystems.EventSystem.current == null)
			{
				return;
			}
			if (global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != base.gameObject)
			{
				global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
			m_TouchKeyboardAllowsInPlaceEditing = !s_IsQuestDevice && global::UnityEngine.TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (TouchScreenKeyboardShouldBeUsed() && !shouldHideSoftKeyboard)
			{
				if (inputSystem != null && inputSystem.touchSupported)
				{
					global::UnityEngine.TouchScreenKeyboard.hideInput = shouldHideMobileInput;
				}
				if (!shouldHideSoftKeyboard && !m_ReadOnly)
				{
					m_SoftKeyboard = ((inputType == global::TMPro.TMP_InputField.InputType.Password) ? global::UnityEngine.TouchScreenKeyboard.Open(m_Text, keyboardType, autocorrection: false, multiLine, secure: true, isAlert, "", characterLimit) : global::UnityEngine.TouchScreenKeyboard.Open(m_Text, keyboardType, inputType == global::TMPro.TMP_InputField.InputType.AutoCorrect, multiLine, secure: false, isAlert, "", characterLimit));
					OnFocus();
					if (m_SoftKeyboard != null)
					{
						int length = ((stringPositionInternal < stringSelectPositionInternal) ? (stringSelectPositionInternal - stringPositionInternal) : (stringPositionInternal - stringSelectPositionInternal));
						m_SoftKeyboard.selection = new global::UnityEngine.RangeInt((stringPositionInternal < stringSelectPositionInternal) ? stringPositionInternal : stringSelectPositionInternal, length);
					}
				}
			}
			else
			{
				if (!TouchScreenKeyboardShouldBeUsed() && !m_ReadOnly && inputSystem != null)
				{
					inputSystem.imeCompositionMode = global::UnityEngine.IMECompositionMode.On;
				}
				OnFocus();
			}
			m_AllowInput = true;
			m_OriginalText = text;
			m_WasCanceled = false;
			SetCaretVisible();
			UpdateLabel();
		}

		public override void OnSelect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			base.OnSelect(eventData);
			SendOnFocus();
			if (shouldActivateOnSelect)
			{
				ActivateInputField();
			}
		}

		public virtual void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left)
			{
				ActivateInputField();
			}
		}

		public void OnControlClick()
		{
		}

		public void ReleaseSelection()
		{
			m_SelectionStillActive = false;
			m_ReleaseSelection = false;
			m_PreviouslySelectedObject = null;
			MarkGeometryAsDirty();
			SendOnEndEdit();
			SendOnEndTextSelection();
		}

		public void DeactivateInputField(bool clearSelection = false)
		{
			if (!m_AllowInput)
			{
				return;
			}
			m_HasDoneFocusTransition = false;
			m_AllowInput = false;
			if (m_Placeholder != null)
			{
				m_Placeholder.enabled = string.IsNullOrEmpty(m_Text);
			}
			if (m_TextComponent != null && IsInteractable())
			{
				if (m_WasCanceled && m_RestoreOriginalTextOnEscape && !m_IsKeyboardBeingClosedInHoloLens)
				{
					text = m_OriginalText;
				}
				if (m_SoftKeyboard != null)
				{
					m_SoftKeyboard.active = false;
					m_SoftKeyboard = null;
				}
				m_SelectionStillActive = true;
				if ((m_ResetOnDeActivation || m_ReleaseSelection || clearSelection) && m_VerticalScrollbar == null)
				{
					ReleaseSelection();
				}
				if (inputSystem != null)
				{
					inputSystem.imeCompositionMode = global::UnityEngine.IMECompositionMode.Auto;
				}
				m_IsKeyboardBeingClosedInHoloLens = false;
			}
			MarkGeometryAsDirty();
		}

		public override void OnDeselect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			DeactivateInputField();
			base.OnDeselect(eventData);
			SendOnFocusLost();
		}

		public virtual void OnSubmit(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			if (IsActive() && IsInteractable())
			{
				if (!isFocused)
				{
					m_ShouldActivateNextUpdate = true;
				}
				SendOnSubmit();
				DeactivateInputField();
				eventData?.Use();
			}
		}

		public virtual void OnCancel(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			if (IsActive() && IsInteractable())
			{
				if (!isFocused)
				{
					m_ShouldActivateNextUpdate = true;
				}
				m_WasCanceled = true;
				DeactivateInputField();
				eventData.Use();
			}
		}

		public override void OnMove(global::UnityEngine.EventSystems.AxisEventData eventData)
		{
			if (!m_AllowInput)
			{
				base.OnMove(eventData);
			}
		}

		private void EnforceContentType()
		{
			switch (contentType)
			{
			case global::TMPro.TMP_InputField.ContentType.Standard:
				m_InputType = global::TMPro.TMP_InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.Default;
				m_CharacterValidation = global::TMPro.TMP_InputField.CharacterValidation.None;
				break;
			case global::TMPro.TMP_InputField.ContentType.Autocorrected:
				m_InputType = global::TMPro.TMP_InputField.InputType.AutoCorrect;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.Default;
				m_CharacterValidation = global::TMPro.TMP_InputField.CharacterValidation.None;
				break;
			case global::TMPro.TMP_InputField.ContentType.IntegerNumber:
				m_LineType = global::TMPro.TMP_InputField.LineType.SingleLine;
				m_InputType = global::TMPro.TMP_InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.NumbersAndPunctuation;
				m_CharacterValidation = global::TMPro.TMP_InputField.CharacterValidation.Integer;
				break;
			case global::TMPro.TMP_InputField.ContentType.DecimalNumber:
				m_LineType = global::TMPro.TMP_InputField.LineType.SingleLine;
				m_InputType = global::TMPro.TMP_InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.NumbersAndPunctuation;
				m_CharacterValidation = global::TMPro.TMP_InputField.CharacterValidation.Decimal;
				break;
			case global::TMPro.TMP_InputField.ContentType.Alphanumeric:
				m_LineType = global::TMPro.TMP_InputField.LineType.SingleLine;
				m_InputType = global::TMPro.TMP_InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.ASCIICapable;
				m_CharacterValidation = global::TMPro.TMP_InputField.CharacterValidation.Alphanumeric;
				break;
			case global::TMPro.TMP_InputField.ContentType.Name:
				m_LineType = global::TMPro.TMP_InputField.LineType.SingleLine;
				m_InputType = global::TMPro.TMP_InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.Default;
				m_CharacterValidation = global::TMPro.TMP_InputField.CharacterValidation.Name;
				break;
			case global::TMPro.TMP_InputField.ContentType.EmailAddress:
				m_LineType = global::TMPro.TMP_InputField.LineType.SingleLine;
				m_InputType = global::TMPro.TMP_InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.EmailAddress;
				m_CharacterValidation = global::TMPro.TMP_InputField.CharacterValidation.EmailAddress;
				break;
			case global::TMPro.TMP_InputField.ContentType.Password:
				m_LineType = global::TMPro.TMP_InputField.LineType.SingleLine;
				m_InputType = global::TMPro.TMP_InputField.InputType.Password;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.Default;
				m_CharacterValidation = global::TMPro.TMP_InputField.CharacterValidation.None;
				break;
			case global::TMPro.TMP_InputField.ContentType.Pin:
				m_LineType = global::TMPro.TMP_InputField.LineType.SingleLine;
				m_InputType = global::TMPro.TMP_InputField.InputType.Password;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.NumberPad;
				m_CharacterValidation = global::TMPro.TMP_InputField.CharacterValidation.Digit;
				break;
			}
			SetTextComponentWrapMode();
		}

		private void SetTextComponentWrapMode()
		{
			if (!(m_TextComponent == null))
			{
				if (multiLine)
				{
					m_TextComponent.textWrappingMode = global::TMPro.TextWrappingModes.Normal;
				}
				else
				{
					m_TextComponent.textWrappingMode = global::TMPro.TextWrappingModes.PreserveWhitespaceNoWrap;
				}
			}
		}

		private void SetTextComponentRichTextMode()
		{
			if (!(m_TextComponent == null))
			{
				m_TextComponent.richText = m_RichText;
			}
		}

		private void SetToCustomIfContentTypeIsNot(params global::TMPro.TMP_InputField.ContentType[] allowedContentTypes)
		{
			if (contentType == global::TMPro.TMP_InputField.ContentType.Custom)
			{
				return;
			}
			for (int i = 0; i < allowedContentTypes.Length; i++)
			{
				if (contentType == allowedContentTypes[i])
				{
					return;
				}
			}
			contentType = global::TMPro.TMP_InputField.ContentType.Custom;
		}

		private void SetToCustom()
		{
			if (contentType != global::TMPro.TMP_InputField.ContentType.Custom)
			{
				contentType = global::TMPro.TMP_InputField.ContentType.Custom;
			}
		}

		private void SetToCustom(global::TMPro.TMP_InputField.CharacterValidation characterValidation)
		{
			if (contentType == global::TMPro.TMP_InputField.ContentType.Custom)
			{
				characterValidation = global::TMPro.TMP_InputField.CharacterValidation.CustomValidator;
				return;
			}
			contentType = global::TMPro.TMP_InputField.ContentType.Custom;
			characterValidation = global::TMPro.TMP_InputField.CharacterValidation.CustomValidator;
		}

		protected override void DoStateTransition(global::UnityEngine.UI.Selectable.SelectionState state, bool instant)
		{
			if (m_HasDoneFocusTransition)
			{
				state = global::UnityEngine.UI.Selectable.SelectionState.Selected;
			}
			else if (state == global::UnityEngine.UI.Selectable.SelectionState.Pressed)
			{
				m_HasDoneFocusTransition = true;
			}
			base.DoStateTransition(state, instant);
		}

		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		public virtual void CalculateLayoutInputVertical()
		{
		}

		public void SetGlobalPointSize(float pointSize)
		{
			global::TMPro.TMP_Text tMP_Text = m_Placeholder as global::TMPro.TMP_Text;
			if (tMP_Text != null)
			{
				tMP_Text.fontSize = pointSize;
			}
			textComponent.fontSize = pointSize;
		}

		public void SetGlobalFontAsset(global::TMPro.TMP_FontAsset fontAsset)
		{
			global::TMPro.TMP_Text tMP_Text = m_Placeholder as global::TMPro.TMP_Text;
			if (tMP_Text != null)
			{
				tMP_Text.font = fontAsset;
			}
			textComponent.font = fontAsset;
		}
	}
}
