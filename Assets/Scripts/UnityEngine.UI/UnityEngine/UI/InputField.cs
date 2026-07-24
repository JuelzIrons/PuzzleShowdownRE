namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Legacy/Input Field", 103)]
	public class InputField : global::UnityEngine.UI.Selectable, global::UnityEngine.EventSystems.IUpdateSelectedHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IBeginDragHandler, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IEndDragHandler, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.ISubmitHandler, global::UnityEngine.UI.ICanvasElement, global::UnityEngine.UI.ILayoutElement
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
			Integer = 1,
			Decimal = 2,
			Alphanumeric = 3,
			Name = 4,
			EmailAddress = 5
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
		public class EndEditEvent : global::UnityEngine.Events.UnityEvent<string>
		{
		}

		[global::System.Serializable]
		public class OnChangeEvent : global::UnityEngine.Events.UnityEvent<string>
		{
		}

		protected enum EditState
		{
			Continue = 0,
			Finish = 1
		}

		protected global::UnityEngine.TouchScreenKeyboard m_Keyboard;

		private static readonly char[] kSeparators = new char[6] { ' ', '.', ',', '\t', '\r', '\n' };

		private static bool s_IsQuestDevice = false;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("text")]
		protected global::UnityEngine.UI.Text m_TextComponent;

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.UI.Graphic m_Placeholder;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.InputField.ContentType m_ContentType;

		[global::UnityEngine.Serialization.FormerlySerializedAs("inputType")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.InputField.InputType m_InputType;

		[global::UnityEngine.Serialization.FormerlySerializedAs("asteriskChar")]
		[global::UnityEngine.SerializeField]
		private char m_AsteriskChar = '*';

		[global::UnityEngine.Serialization.FormerlySerializedAs("keyboardType")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.TouchScreenKeyboardType m_KeyboardType;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.InputField.LineType m_LineType;

		[global::UnityEngine.Serialization.FormerlySerializedAs("hideMobileInput")]
		[global::UnityEngine.SerializeField]
		private bool m_HideMobileInput;

		[global::UnityEngine.Serialization.FormerlySerializedAs("validation")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.InputField.CharacterValidation m_CharacterValidation;

		[global::UnityEngine.Serialization.FormerlySerializedAs("characterLimit")]
		[global::UnityEngine.SerializeField]
		private int m_CharacterLimit;

		[global::UnityEngine.Serialization.FormerlySerializedAs("onSubmit")]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_OnSubmit")]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_EndEdit")]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_OnEndEdit")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.InputField.SubmitEvent m_OnSubmit = new global::UnityEngine.UI.InputField.SubmitEvent();

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.InputField.EndEditEvent m_OnDidEndEdit = new global::UnityEngine.UI.InputField.EndEditEvent();

		[global::UnityEngine.Serialization.FormerlySerializedAs("onValueChange")]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_OnValueChange")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.InputField.OnChangeEvent m_OnValueChanged = new global::UnityEngine.UI.InputField.OnChangeEvent();

		[global::UnityEngine.Serialization.FormerlySerializedAs("onValidateInput")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.InputField.OnValidateInput m_OnValidateInput;

		[global::UnityEngine.Serialization.FormerlySerializedAs("selectionColor")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_CaretColor = new global::UnityEngine.Color(10f / 51f, 10f / 51f, 10f / 51f, 1f);

		[global::UnityEngine.SerializeField]
		private bool m_CustomCaretColor;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color m_SelectionColor = new global::UnityEngine.Color(56f / 85f, 0.80784315f, 1f, 64f / 85f);

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Multiline]
		[global::UnityEngine.Serialization.FormerlySerializedAs("mValue")]
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
		private bool m_ShouldActivateOnSelect = true;

		protected int m_CaretPosition;

		protected int m_CaretSelectPosition;

		private global::UnityEngine.RectTransform caretRectTrans;

		protected global::UnityEngine.UIVertex[] m_CursorVerts;

		private global::UnityEngine.TextGenerator m_InputTextCache;

		private global::UnityEngine.CanvasRenderer m_CachedInputRenderer;

		private bool m_PreventFontCallback;

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

		protected int m_DrawStart;

		protected int m_DrawEnd;

		private global::UnityEngine.Coroutine m_DragCoroutine;

		private string m_OriginalText = "";

		private bool m_WasCanceled;

		private bool m_HasDoneFocusTransition;

		private global::UnityEngine.WaitForSecondsRealtime m_WaitForSecondsRealtime;

		private bool m_TouchKeyboardAllowsInPlaceEditing;

		private bool m_IsCompositionActive;

		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		private const string kOculusQuestDeviceModel = "Oculus Quest";

		private global::UnityEngine.Event m_ProcessingEvent = new global::UnityEngine.Event();

		private const int k_MaxTextLength = 16382;

		private global::UnityEngine.EventSystems.BaseInput input
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
				if (!(input != null))
				{
					return global::UnityEngine.Input.compositionString;
				}
				return input.compositionString;
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

		protected global::UnityEngine.TextGenerator cachedInputTextGenerator
		{
			get
			{
				if (m_InputTextCache == null)
				{
					m_InputTextCache = new global::UnityEngine.TextGenerator();
				}
				return m_InputTextCache;
			}
		}

		public bool shouldHideMobileInput
		{
			get
			{
				global::UnityEngine.RuntimePlatform platform = global::UnityEngine.Application.platform;
				if (platform == global::UnityEngine.RuntimePlatform.IPhonePlayer || platform == global::UnityEngine.RuntimePlatform.Android || platform == global::UnityEngine.RuntimePlatform.tvOS)
				{
					return m_HideMobileInput;
				}
				return true;
			}
			set
			{
				global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_HideMobileInput, value);
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
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_CaretBlinkRate, value) && m_AllowInput)
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
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_CaretWidth, value))
				{
					MarkGeometryAsDirty();
				}
			}
		}

		public global::UnityEngine.UI.Text textComponent
		{
			get
			{
				return m_TextComponent;
			}
			set
			{
				if (m_TextComponent != null)
				{
					m_TextComponent.UnregisterDirtyVerticesCallback(MarkGeometryAsDirty);
					m_TextComponent.UnregisterDirtyVerticesCallback(UpdateLabel);
					m_TextComponent.UnregisterDirtyMaterialCallback(UpdateCaretMaterial);
				}
				if (global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_TextComponent, value))
				{
					EnforceTextHOverflow();
					if (m_TextComponent != null)
					{
						m_TextComponent.RegisterDirtyVerticesCallback(MarkGeometryAsDirty);
						m_TextComponent.RegisterDirtyVerticesCallback(UpdateLabel);
						m_TextComponent.RegisterDirtyMaterialCallback(UpdateCaretMaterial);
					}
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
				global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_Placeholder, value);
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
				if (global::UnityEngine.UI.SetPropertyUtility.SetColor(ref m_CaretColor, value))
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
				if (global::UnityEngine.UI.SetPropertyUtility.SetColor(ref m_SelectionColor, value))
				{
					MarkGeometryAsDirty();
				}
			}
		}

		public global::UnityEngine.UI.InputField.EndEditEvent onEndEdit
		{
			get
			{
				return m_OnDidEndEdit;
			}
			set
			{
				global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_OnDidEndEdit, value);
			}
		}

		public global::UnityEngine.UI.InputField.SubmitEvent onSubmit
		{
			get
			{
				return m_OnSubmit;
			}
			set
			{
				global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_OnSubmit, value);
			}
		}

		[global::System.Obsolete("onValueChange has been renamed to onValueChanged")]
		public global::UnityEngine.UI.InputField.OnChangeEvent onValueChange
		{
			get
			{
				return onValueChanged;
			}
			set
			{
				onValueChanged = value;
			}
		}

		public global::UnityEngine.UI.InputField.OnChangeEvent onValueChanged
		{
			get
			{
				return m_OnValueChanged;
			}
			set
			{
				global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_OnValueChanged, value);
			}
		}

		public global::UnityEngine.UI.InputField.OnValidateInput onValidateInput
		{
			get
			{
				return m_OnValidateInput;
			}
			set
			{
				global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_OnValidateInput, value);
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
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_CharacterLimit, global::System.Math.Max(0, value)))
				{
					UpdateLabel();
					if (m_Keyboard != null)
					{
						m_Keyboard.characterLimit = value;
					}
				}
			}
		}

		public global::UnityEngine.UI.InputField.ContentType contentType
		{
			get
			{
				return m_ContentType;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_ContentType, value))
				{
					EnforceContentType();
				}
			}
		}

		public global::UnityEngine.UI.InputField.LineType lineType
		{
			get
			{
				return m_LineType;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_LineType, value))
				{
					SetToCustomIfContentTypeIsNot(global::UnityEngine.UI.InputField.ContentType.Standard, global::UnityEngine.UI.InputField.ContentType.Autocorrected);
					EnforceTextHOverflow();
				}
			}
		}

		public global::UnityEngine.UI.InputField.InputType inputType
		{
			get
			{
				return m_InputType;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_InputType, value))
				{
					SetToCustom();
				}
			}
		}

		public global::UnityEngine.TouchScreenKeyboard touchScreenKeyboard => m_Keyboard;

		public global::UnityEngine.TouchScreenKeyboardType keyboardType
		{
			get
			{
				return m_KeyboardType;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_KeyboardType, value))
				{
					SetToCustom();
				}
			}
		}

		public global::UnityEngine.UI.InputField.CharacterValidation characterValidation
		{
			get
			{
				return m_CharacterValidation;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_CharacterValidation, value))
				{
					SetToCustom();
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

		public bool multiLine
		{
			get
			{
				if (m_LineType != global::UnityEngine.UI.InputField.LineType.MultiLineNewline)
				{
					return lineType == global::UnityEngine.UI.InputField.LineType.MultiLineSubmit;
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
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_AsteriskChar, value))
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
				return m_CaretPosition + compositionString.Length;
			}
			set
			{
				m_CaretPosition = value;
				ClampPos(ref m_CaretPosition);
			}
		}

		protected int caretSelectPositionInternal
		{
			get
			{
				return m_CaretSelectPosition + compositionString.Length;
			}
			set
			{
				m_CaretSelectPosition = value;
				ClampPos(ref m_CaretSelectPosition);
			}
		}

		private bool hasSelection => caretPositionInternal != caretSelectPositionInternal;

		public int caretPosition
		{
			get
			{
				return m_CaretSelectPosition + compositionString.Length;
			}
			set
			{
				selectionAnchorPosition = value;
				selectionFocusPosition = value;
			}
		}

		public int selectionAnchorPosition
		{
			get
			{
				return m_CaretPosition + compositionString.Length;
			}
			set
			{
				if (compositionString.Length == 0)
				{
					m_CaretPosition = value;
					ClampPos(ref m_CaretPosition);
				}
			}
		}

		public int selectionFocusPosition
		{
			get
			{
				return m_CaretSelectPosition + compositionString.Length;
			}
			set
			{
				if (compositionString.Length == 0)
				{
					m_CaretSelectPosition = value;
					ClampPos(ref m_CaretSelectPosition);
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

		public virtual float minWidth => 5f;

		public virtual float preferredWidth
		{
			get
			{
				if (textComponent == null)
				{
					return 0f;
				}
				global::UnityEngine.TextGenerationSettings generationSettings = textComponent.GetGenerationSettings(global::UnityEngine.Vector2.zero);
				return textComponent.cachedTextGeneratorForLayout.GetPreferredWidth(m_Text, generationSettings) / textComponent.pixelsPerUnit;
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
				global::UnityEngine.TextGenerationSettings generationSettings = textComponent.GetGenerationSettings(new global::UnityEngine.Vector2(textComponent.rectTransform.rect.size.x, 0f));
				return textComponent.cachedTextGeneratorForLayout.GetPreferredHeight(m_Text, generationSettings) / textComponent.pixelsPerUnit;
			}
		}

		public virtual float flexibleHeight => -1f;

		public virtual int layoutPriority => 1;

		global::UnityEngine.Transform global::UnityEngine.UI.ICanvasElement.transform => base.transform;

		protected InputField()
		{
			EnforceTextHOverflow();
		}

		public void SetTextWithoutNotify(string input)
		{
			SetText(input, sendCallback: false);
		}

		private void SetText(string value, bool sendCallback = true)
		{
			if (text == value)
			{
				return;
			}
			if (value == null)
			{
				value = "";
			}
			value = value.Replace("\0", string.Empty);
			if (m_LineType == global::UnityEngine.UI.InputField.LineType.SingleLine)
			{
				value = value.Replace("\n", "").Replace("\t", "");
			}
			if (this.onValidateInput != null || characterValidation != global::UnityEngine.UI.InputField.CharacterValidation.None)
			{
				m_Text = "";
				global::UnityEngine.UI.InputField.OnValidateInput onValidateInput = this.onValidateInput ?? new global::UnityEngine.UI.InputField.OnValidateInput(Validate);
				m_CaretPosition = (m_CaretSelectPosition = value.Length);
				int num = ((characterLimit > 0) ? global::System.Math.Min(characterLimit, value.Length) : value.Length);
				for (int i = 0; i < num; i++)
				{
					char c = onValidateInput(m_Text, m_Text.Length, value[i]);
					if (c != 0)
					{
						m_Text += c;
					}
				}
			}
			else
			{
				m_Text = ((characterLimit > 0 && value.Length > characterLimit) ? value.Substring(0, characterLimit) : value);
			}
			if (m_Keyboard != null)
			{
				m_Keyboard.text = m_Text;
			}
			if (m_CaretPosition > m_Text.Length)
			{
				m_CaretPosition = (m_CaretSelectPosition = m_Text.Length);
			}
			else if (m_CaretSelectPosition > m_Text.Length)
			{
				m_CaretSelectPosition = m_Text.Length;
			}
			if (sendCallback)
			{
				SendOnValueChanged();
			}
			UpdateLabel();
		}

		protected void ClampPos(ref int pos)
		{
			if (pos < 0)
			{
				pos = 0;
			}
			else if (pos > text.Length)
			{
				pos = text.Length;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (m_Text == null)
			{
				m_Text = string.Empty;
			}
			m_DrawStart = 0;
			m_DrawEnd = m_Text.Length;
			if (m_CachedInputRenderer != null)
			{
				m_CachedInputRenderer.SetMaterial(m_TextComponent.GetModifiedMaterial(global::UnityEngine.UI.Graphic.defaultGraphicMaterial), global::UnityEngine.Texture2D.whiteTexture);
			}
			if (m_TextComponent != null)
			{
				m_TextComponent.RegisterDirtyVerticesCallback(MarkGeometryAsDirty);
				m_TextComponent.RegisterDirtyVerticesCallback(UpdateLabel);
				m_TextComponent.RegisterDirtyMaterialCallback(UpdateCaretMaterial);
				UpdateLabel();
			}
		}

		protected override void OnDisable()
		{
			m_BlinkCoroutine = null;
			DeactivateInputField();
			if (m_TextComponent != null)
			{
				m_TextComponent.UnregisterDirtyVerticesCallback(MarkGeometryAsDirty);
				m_TextComponent.UnregisterDirtyVerticesCallback(UpdateLabel);
				m_TextComponent.UnregisterDirtyMaterialCallback(UpdateCaretMaterial);
			}
			global::UnityEngine.UI.CanvasUpdateRegistry.DisableCanvasElementForRebuild(this);
			if (m_CachedInputRenderer != null)
			{
				m_CachedInputRenderer.Clear();
			}
			if (m_Mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(m_Mesh);
			}
			m_Mesh = null;
			base.OnDisable();
		}

		protected override void OnDestroy()
		{
			global::UnityEngine.UI.CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			base.OnDestroy();
		}

		private global::System.Collections.IEnumerator CaretBlink()
		{
			m_CaretVisible = true;
			yield return null;
			while (isFocused && m_CaretBlinkRate > 0f)
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

		private void UpdateCaretMaterial()
		{
			if (m_TextComponent != null && m_CachedInputRenderer != null)
			{
				m_CachedInputRenderer.SetMaterial(m_TextComponent.GetModifiedMaterial(global::UnityEngine.UI.Graphic.defaultGraphicMaterial), global::UnityEngine.Texture2D.whiteTexture);
			}
		}

		protected void OnFocus()
		{
			SelectAll();
		}

		protected void SelectAll()
		{
			caretPositionInternal = text.Length;
			caretSelectPositionInternal = 0;
		}

		public void MoveTextEnd(bool shift)
		{
			int length = text.Length;
			if (shift)
			{
				caretSelectPositionInternal = length;
			}
			else
			{
				caretPositionInternal = length;
				caretSelectPositionInternal = caretPositionInternal;
			}
			UpdateLabel();
		}

		public void MoveTextStart(bool shift)
		{
			int num = 0;
			if (shift)
			{
				caretSelectPositionInternal = num;
			}
			else
			{
				caretPositionInternal = num;
				caretSelectPositionInternal = caretPositionInternal;
			}
			UpdateLabel();
		}

		private bool TouchScreenKeyboardShouldBeUsed()
		{
			switch (global::UnityEngine.Application.platform)
			{
			case global::UnityEngine.RuntimePlatform.Android:
				if (s_IsQuestDevice)
				{
					return global::UnityEngine.TouchScreenKeyboard.isSupported;
				}
				return !global::UnityEngine.TouchScreenKeyboard.isInPlaceEditingAllowed;
			case global::UnityEngine.RuntimePlatform.WebGLPlayer:
				return !global::UnityEngine.TouchScreenKeyboard.isInPlaceEditingAllowed;
			default:
				return global::UnityEngine.TouchScreenKeyboard.isSupported;
			}
		}

		private bool InPlaceEditing()
		{
			if (global::UnityEngine.TouchScreenKeyboard.isSupported)
			{
				return m_TouchKeyboardAllowsInPlaceEditing;
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

		private global::UnityEngine.RangeInt GetInternalSelection()
		{
			int start = global::UnityEngine.Mathf.Min(caretSelectPositionInternal, caretPositionInternal);
			int length = global::UnityEngine.Mathf.Abs(caretSelectPositionInternal - caretPositionInternal);
			return new global::UnityEngine.RangeInt(start, length);
		}

		private void UpdateKeyboardCaret()
		{
			if (m_HideMobileInput && m_Keyboard != null && m_Keyboard.canSetSelection && (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.IPhonePlayer || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.tvOS))
			{
				m_Keyboard.selection = GetInternalSelection();
			}
		}

		private void UpdateCaretFromKeyboard()
		{
			global::UnityEngine.RangeInt selection = m_Keyboard.selection;
			int start = selection.start;
			int end = selection.end;
			bool flag = false;
			if (caretPositionInternal != start)
			{
				flag = true;
				caretPositionInternal = start;
			}
			if (caretSelectPositionInternal != end)
			{
				caretSelectPositionInternal = end;
				flag = true;
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
			AssignPositioningIfNeeded();
			if (isFocused && InPlaceEditingChanged())
			{
				if (m_CachedInputRenderer != null)
				{
					using (global::UnityEngine.UI.VertexHelper vertexHelper = new global::UnityEngine.UI.VertexHelper())
					{
						vertexHelper.FillMesh(mesh);
					}
					m_CachedInputRenderer.SetMesh(mesh);
				}
				DeactivateInputField();
			}
			if (!isFocused || InPlaceEditing())
			{
				return;
			}
			if (m_Keyboard == null || m_Keyboard.status != global::UnityEngine.TouchScreenKeyboard.Status.Visible)
			{
				if (m_Keyboard != null)
				{
					if (!m_ReadOnly)
					{
						this.text = m_Keyboard.text;
					}
					if (m_Keyboard.status == global::UnityEngine.TouchScreenKeyboard.Status.Canceled)
					{
						m_WasCanceled = true;
					}
					else if (m_Keyboard.status == global::UnityEngine.TouchScreenKeyboard.Status.Done)
					{
						SendOnSubmit();
					}
				}
				return;
			}
			string text = m_Keyboard.text;
			if (m_Text != text)
			{
				if (m_ReadOnly)
				{
					m_Keyboard.text = m_Text;
				}
				else
				{
					m_Text = "";
					for (int i = 0; i < text.Length; i++)
					{
						char c = text[i];
						if (c == '\r' || c == '\u0003')
						{
							c = '\n';
						}
						if (onValidateInput != null)
						{
							c = onValidateInput(m_Text, m_Text.Length, c);
						}
						else if (characterValidation != global::UnityEngine.UI.InputField.CharacterValidation.None)
						{
							c = Validate(m_Text, m_Text.Length, c);
						}
						if (lineType != global::UnityEngine.UI.InputField.LineType.MultiLineNewline && c == '\n')
						{
							UpdateLabel();
							SendOnSubmit();
							OnDeselect(null);
							return;
						}
						if (c != 0)
						{
							m_Text += c;
						}
					}
					if (characterLimit > 0 && m_Text.Length > characterLimit)
					{
						m_Text = m_Text.Substring(0, characterLimit);
					}
					if (m_Keyboard.canGetSelection)
					{
						UpdateCaretFromKeyboard();
					}
					else
					{
						int num = (caretSelectPositionInternal = m_Text.Length);
						caretPositionInternal = num;
					}
					if (m_Text != text)
					{
						m_Keyboard.text = m_Text;
					}
					SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (m_HideMobileInput && m_Keyboard != null && m_Keyboard.canSetSelection && global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.IPhonePlayer && global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.tvOS)
			{
				m_Keyboard.selection = GetInternalSelection();
			}
			else if (m_Keyboard != null && m_Keyboard.canGetSelection)
			{
				UpdateCaretFromKeyboard();
			}
			if (m_Keyboard.status != global::UnityEngine.TouchScreenKeyboard.Status.Visible)
			{
				if (m_Keyboard.status == global::UnityEngine.TouchScreenKeyboard.Status.Canceled)
				{
					m_WasCanceled = true;
				}
				else if (m_Keyboard.status == global::UnityEngine.TouchScreenKeyboard.Status.Done)
				{
					SendOnSubmit();
				}
				OnDeselect(null);
			}
		}

		[global::System.Obsolete("This function is no longer used. Please use RectTransformUtility.ScreenPointToLocalPointInRectangle() instead.")]
		public global::UnityEngine.Vector2 ScreenToLocal(global::UnityEngine.Vector2 screen)
		{
			global::UnityEngine.Canvas canvas = m_TextComponent.canvas;
			if (canvas == null)
			{
				return screen;
			}
			global::UnityEngine.Vector3 vector = global::UnityEngine.Vector3.zero;
			if (canvas.renderMode == global::UnityEngine.RenderMode.ScreenSpaceOverlay)
			{
				vector = m_TextComponent.transform.InverseTransformPoint(screen);
			}
			else if (canvas.worldCamera != null)
			{
				global::UnityEngine.Ray ray = canvas.worldCamera.ScreenPointToRay(screen);
				new global::UnityEngine.Plane(m_TextComponent.transform.forward, m_TextComponent.transform.position).Raycast(ray, out var enter);
				vector = m_TextComponent.transform.InverseTransformPoint(ray.GetPoint(enter));
			}
			return new global::UnityEngine.Vector2(vector.x, vector.y);
		}

		private int GetUnclampedCharacterLineFromPosition(global::UnityEngine.Vector2 pos, global::UnityEngine.TextGenerator generator)
		{
			if (!multiLine)
			{
				return 0;
			}
			float num = pos.y * m_TextComponent.pixelsPerUnit;
			float num2 = 0f;
			for (int i = 0; i < generator.lineCount; i++)
			{
				float topY = generator.lines[i].topY;
				float num3 = topY - (float)generator.lines[i].height;
				if (num > topY)
				{
					float num4 = topY - num2;
					if (num > topY - 0.5f * num4)
					{
						return i - 1;
					}
					return i;
				}
				if (num > num3)
				{
					return i;
				}
				num2 = num3;
			}
			return generator.lineCount;
		}

		protected int GetCharacterIndexFromPosition(global::UnityEngine.Vector2 pos)
		{
			global::UnityEngine.TextGenerator cachedTextGenerator = m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator.lineCount == 0)
			{
				return 0;
			}
			int unclampedCharacterLineFromPosition = GetUnclampedCharacterLineFromPosition(pos, cachedTextGenerator);
			if (unclampedCharacterLineFromPosition < 0)
			{
				return 0;
			}
			if (unclampedCharacterLineFromPosition >= cachedTextGenerator.lineCount)
			{
				return cachedTextGenerator.characterCountVisible;
			}
			int startCharIdx = cachedTextGenerator.lines[unclampedCharacterLineFromPosition].startCharIdx;
			int lineEndPosition = GetLineEndPosition(cachedTextGenerator, unclampedCharacterLineFromPosition);
			for (int i = startCharIdx; i < lineEndPosition && i < cachedTextGenerator.characterCountVisible; i++)
			{
				global::UnityEngine.UICharInfo uICharInfo = cachedTextGenerator.characters[i];
				global::UnityEngine.Vector2 vector = uICharInfo.cursorPos / m_TextComponent.pixelsPerUnit;
				float num = pos.x - vector.x;
				float num2 = vector.x + uICharInfo.charWidth / m_TextComponent.pixelsPerUnit - pos.x;
				if (num < num2)
				{
					return i;
				}
			}
			return lineEndPosition;
		}

		private bool MayDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (IsActive() && IsInteractable() && eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left && m_TextComponent != null)
			{
				if (!InPlaceEditing())
				{
					return m_HideMobileInput;
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
			global::UnityEngine.Vector2 position = global::UnityEngine.Vector2.zero;
			if (global::UnityEngine.UI.MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref position))
			{
				global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(textComponent.rectTransform, position, eventData.pressEventCamera, out var localPoint);
				caretSelectPositionInternal = GetCharacterIndexFromPosition(localPoint) + m_DrawStart;
				MarkGeometryAsDirty();
				m_DragPositionOutOfBounds = !global::UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(textComponent.rectTransform, eventData.position, eventData.pressEventCamera);
				if (m_DragPositionOutOfBounds && m_DragCoroutine == null)
				{
					m_DragCoroutine = StartCoroutine(MouseDragOutsideRect(eventData));
				}
				UpdateKeyboardCaret();
				eventData.Use();
			}
		}

		private global::System.Collections.IEnumerator MouseDragOutsideRect(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			while (m_UpdateDrag && m_DragPositionOutOfBounds)
			{
				global::UnityEngine.Vector2 position = global::UnityEngine.Vector2.zero;
				if (!global::UnityEngine.UI.MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref position))
				{
					break;
				}
				global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(textComponent.rectTransform, position, eventData.pressEventCamera, out var localPoint);
				global::UnityEngine.Rect rect = textComponent.rectTransform.rect;
				if (multiLine)
				{
					if (localPoint.y > rect.yMax)
					{
						MoveUp(shift: true, goToFirstChar: true);
					}
					else if (localPoint.y < rect.yMin)
					{
						MoveDown(shift: true, goToLastChar: true);
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
			if (!InPlaceEditing() && (m_Keyboard == null || !m_Keyboard.active))
			{
				OnSelect(eventData);
				return;
			}
			if (allowInput)
			{
				global::UnityEngine.RectTransformUtility.ScreenPointToLocalPointInRectangle(textComponent.rectTransform, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out var localPoint);
				int num = (caretPositionInternal = GetCharacterIndexFromPosition(localPoint) + m_DrawStart);
				caretSelectPositionInternal = num;
			}
			UpdateLabel();
			UpdateKeyboardCaret();
			eventData.Use();
		}

		protected global::UnityEngine.UI.InputField.EditState KeyPressed(global::UnityEngine.Event evt)
		{
			global::UnityEngine.EventModifiers modifiers = evt.modifiers;
			bool flag = ((global::UnityEngine.SystemInfo.operatingSystemFamily == global::UnityEngine.OperatingSystemFamily.MacOSX) ? ((modifiers & global::UnityEngine.EventModifiers.Command) != 0) : ((modifiers & global::UnityEngine.EventModifiers.Control) != 0));
			bool flag2 = (modifiers & global::UnityEngine.EventModifiers.Shift) != 0;
			bool flag3 = (modifiers & global::UnityEngine.EventModifiers.Alt) != 0;
			bool flag4 = flag && !flag3 && !flag2;
			bool flag5 = flag2 && !flag && !flag3;
			switch (evt.keyCode)
			{
			case global::UnityEngine.KeyCode.Backspace:
				Backspace();
				return global::UnityEngine.UI.InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.Delete:
				ForwardSpace();
				return global::UnityEngine.UI.InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.Home:
				MoveTextStart(flag2);
				return global::UnityEngine.UI.InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.End:
				MoveTextEnd(flag2);
				return global::UnityEngine.UI.InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.A:
				if (flag4)
				{
					SelectAll();
					return global::UnityEngine.UI.InputField.EditState.Continue;
				}
				break;
			case global::UnityEngine.KeyCode.C:
				if (flag4)
				{
					if (inputType != global::UnityEngine.UI.InputField.InputType.Password)
					{
						clipboard = GetSelectedString();
					}
					else
					{
						clipboard = "";
					}
					return global::UnityEngine.UI.InputField.EditState.Continue;
				}
				break;
			case global::UnityEngine.KeyCode.V:
				if (flag4)
				{
					Append(clipboard);
					UpdateLabel();
					return global::UnityEngine.UI.InputField.EditState.Continue;
				}
				break;
			case global::UnityEngine.KeyCode.X:
				if (flag4)
				{
					if (inputType != global::UnityEngine.UI.InputField.InputType.Password)
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
					return global::UnityEngine.UI.InputField.EditState.Continue;
				}
				break;
			case global::UnityEngine.KeyCode.Insert:
				if (flag4)
				{
					if (inputType != global::UnityEngine.UI.InputField.InputType.Password)
					{
						clipboard = GetSelectedString();
					}
					else
					{
						clipboard = "";
					}
					return global::UnityEngine.UI.InputField.EditState.Continue;
				}
				if (flag5)
				{
					Append(clipboard);
					UpdateLabel();
					return global::UnityEngine.UI.InputField.EditState.Continue;
				}
				break;
			case global::UnityEngine.KeyCode.LeftArrow:
				MoveLeft(flag2, flag);
				return global::UnityEngine.UI.InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.RightArrow:
				MoveRight(flag2, flag);
				return global::UnityEngine.UI.InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.UpArrow:
				MoveUp(flag2);
				return global::UnityEngine.UI.InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.DownArrow:
				MoveDown(flag2);
				return global::UnityEngine.UI.InputField.EditState.Continue;
			case global::UnityEngine.KeyCode.Return:
			case global::UnityEngine.KeyCode.KeypadEnter:
				if (lineType != global::UnityEngine.UI.InputField.LineType.MultiLineNewline)
				{
					return global::UnityEngine.UI.InputField.EditState.Finish;
				}
				break;
			case global::UnityEngine.KeyCode.Escape:
				m_WasCanceled = true;
				return global::UnityEngine.UI.InputField.EditState.Finish;
			}
			char c = evt.character;
			if (!multiLine && (c == '\t' || c == '\r' || c == '\n'))
			{
				return global::UnityEngine.UI.InputField.EditState.Continue;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (IsValidChar(c))
			{
				Append(c);
			}
			if (c == '\0' && compositionString.Length > 0)
			{
				UpdateLabel();
			}
			return global::UnityEngine.UI.InputField.EditState.Continue;
		}

		private bool IsValidChar(char c)
		{
			switch (c)
			{
			case '\0':
				return false;
			case '\u007f':
				return false;
			case '\t':
			case '\n':
				return true;
			default:
				return m_TextComponent.font.HasCharacter(c);
			}
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
				if (m_ProcessingEvent.rawType == global::UnityEngine.EventType.KeyDown)
				{
					flag = true;
					if (m_IsCompositionActive && compositionString.Length == 0 && m_ProcessingEvent.character == '\0' && m_ProcessingEvent.modifiers == global::UnityEngine.EventModifiers.None)
					{
						continue;
					}
					if (KeyPressed(m_ProcessingEvent) == global::UnityEngine.UI.InputField.EditState.Finish)
					{
						if (!m_WasCanceled)
						{
							SendOnSubmit();
						}
						DeactivateInputField();
						continue;
					}
					UpdateLabel();
				}
				global::UnityEngine.EventType type = m_ProcessingEvent.type;
				if ((uint)(type - 13) <= 1u && m_ProcessingEvent.commandName == "SelectAll")
				{
					SelectAll();
					flag = true;
				}
			}
			if (flag)
			{
				UpdateLabel();
			}
			eventData.Use();
		}

		private string GetSelectedString()
		{
			if (!hasSelection)
			{
				return "";
			}
			int num = caretPositionInternal;
			int num2 = caretSelectPositionInternal;
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			return text.Substring(num, num2 - num);
		}

		private int FindtNextWordBegin()
		{
			if (caretSelectPositionInternal + 1 >= text.Length)
			{
				return text.Length;
			}
			int num = text.IndexOfAny(kSeparators, caretSelectPositionInternal + 1);
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
				num = (caretSelectPositionInternal = global::UnityEngine.Mathf.Max(caretPositionInternal, caretSelectPositionInternal));
				caretPositionInternal = num;
				return;
			}
			int num3 = ((!ctrl) ? (caretSelectPositionInternal + 1) : FindtNextWordBegin());
			if (shift)
			{
				caretSelectPositionInternal = num3;
				return;
			}
			num = (caretPositionInternal = num3);
			caretSelectPositionInternal = num;
		}

		private int FindtPrevWordBegin()
		{
			if (caretSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int num = text.LastIndexOfAny(kSeparators, caretSelectPositionInternal - 2);
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
				num = (caretSelectPositionInternal = global::UnityEngine.Mathf.Min(caretPositionInternal, caretSelectPositionInternal));
				caretPositionInternal = num;
				return;
			}
			int num3 = ((!ctrl) ? (caretSelectPositionInternal - 1) : FindtPrevWordBegin());
			if (shift)
			{
				caretSelectPositionInternal = num3;
				return;
			}
			num = (caretPositionInternal = num3);
			caretSelectPositionInternal = num;
		}

		private int DetermineCharacterLine(int charPos, global::UnityEngine.TextGenerator generator)
		{
			for (int i = 0; i < generator.lineCount - 1; i++)
			{
				if (generator.lines[i + 1].startCharIdx > charPos)
				{
					return i;
				}
			}
			return generator.lineCount - 1;
		}

		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= cachedInputTextGenerator.characters.Count)
			{
				return 0;
			}
			global::UnityEngine.UICharInfo uICharInfo = cachedInputTextGenerator.characters[originalPos];
			int num = DetermineCharacterLine(originalPos, cachedInputTextGenerator);
			if (num <= 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			int num2 = cachedInputTextGenerator.lines[num].startCharIdx - 1;
			for (int i = cachedInputTextGenerator.lines[num - 1].startCharIdx; i < num2; i++)
			{
				if (cachedInputTextGenerator.characters[i].cursorPos.x >= uICharInfo.cursorPos.x)
				{
					return i;
				}
			}
			return num2;
		}

		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= cachedInputTextGenerator.characterCountVisible)
			{
				return text.Length;
			}
			global::UnityEngine.UICharInfo uICharInfo = cachedInputTextGenerator.characters[originalPos];
			int num = DetermineCharacterLine(originalPos, cachedInputTextGenerator);
			if (num + 1 >= cachedInputTextGenerator.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return text.Length;
			}
			int lineEndPosition = GetLineEndPosition(cachedInputTextGenerator, num + 1);
			for (int i = cachedInputTextGenerator.lines[num + 1].startCharIdx; i < lineEndPosition; i++)
			{
				if (cachedInputTextGenerator.characters[i].cursorPos.x >= uICharInfo.cursorPos.x)
				{
					return i;
				}
			}
			return lineEndPosition;
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
			int num3 = (multiLine ? LineDownCharacterPosition(caretSelectPositionInternal, goToLastChar) : text.Length);
			if (shift)
			{
				caretSelectPositionInternal = num3;
				return;
			}
			num = (caretSelectPositionInternal = num3);
			caretPositionInternal = num;
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
				return;
			}
			num = (caretPositionInternal = num3);
			caretSelectPositionInternal = num;
		}

		private void Delete()
		{
			if (!m_ReadOnly && caretPositionInternal != caretSelectPositionInternal)
			{
				if (caretPositionInternal < caretSelectPositionInternal)
				{
					m_Text = text.Substring(0, caretPositionInternal) + text.Substring(caretSelectPositionInternal, text.Length - caretSelectPositionInternal);
					caretSelectPositionInternal = caretPositionInternal;
				}
				else
				{
					m_Text = text.Substring(0, caretSelectPositionInternal) + text.Substring(caretPositionInternal, text.Length - caretPositionInternal);
					caretPositionInternal = caretSelectPositionInternal;
				}
			}
		}

		private void ForwardSpace()
		{
			if (!m_ReadOnly)
			{
				if (hasSelection)
				{
					Delete();
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChangedAndUpdateLabel();
				}
				else if (caretPositionInternal < text.Length)
				{
					m_Text = text.Remove(caretPositionInternal, 1);
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChangedAndUpdateLabel();
				}
			}
		}

		private void Backspace()
		{
			if (!m_ReadOnly)
			{
				if (hasSelection)
				{
					Delete();
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChangedAndUpdateLabel();
				}
				else if (caretPositionInternal > 0 && caretPositionInternal - 1 < text.Length)
				{
					m_Text = text.Remove(caretPositionInternal - 1, 1);
					caretSelectPositionInternal = --caretPositionInternal;
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChangedAndUpdateLabel();
				}
			}
		}

		private void Insert(char c)
		{
			if (!m_ReadOnly)
			{
				string text = c.ToString();
				Delete();
				if (characterLimit <= 0 || this.text.Length < characterLimit)
				{
					m_Text = this.text.Insert(m_CaretPosition, text);
					caretSelectPositionInternal = (caretPositionInternal += text.Length);
					UpdateTouchKeyboardFromEditChanges();
					SendOnValueChanged();
				}
			}
		}

		private void UpdateTouchKeyboardFromEditChanges()
		{
			if (m_Keyboard != null && InPlaceEditing())
			{
				m_Keyboard.text = m_Text;
			}
		}

		private void SendOnValueChangedAndUpdateLabel()
		{
			SendOnValueChanged();
			UpdateLabel();
		}

		private void SendOnValueChanged()
		{
			global::UnityEngine.UISystemProfilerApi.AddMarker("InputField.value", this);
			if (onValueChanged != null)
			{
				onValueChanged.Invoke(text);
			}
		}

		protected void SendOnEndEdit()
		{
			global::UnityEngine.UISystemProfilerApi.AddMarker("InputField.onEndEdit", this);
			if (onEndEdit != null)
			{
				onEndEdit.Invoke(m_Text);
			}
		}

		protected void SendOnSubmit()
		{
			global::UnityEngine.UISystemProfilerApi.AddMarker("InputField.onSubmit", this);
			if (onSubmit != null)
			{
				onSubmit.Invoke(m_Text);
			}
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
				if (c >= ' ' || c == '\t' || c == '\r' || c == '\n' || c == '\n')
				{
					Append(c);
				}
			}
		}

		protected virtual void Append(char input)
		{
			if (!char.IsSurrogate(input) && !m_ReadOnly && this.text.Length < 16382 && InPlaceEditing())
			{
				int num = global::System.Math.Min(selectionFocusPosition, selectionAnchorPosition);
				string text = this.text;
				if (selectionFocusPosition != selectionAnchorPosition)
				{
					text = ((caretPositionInternal >= caretSelectPositionInternal) ? (this.text.Substring(0, caretSelectPositionInternal) + this.text.Substring(caretPositionInternal, this.text.Length - caretPositionInternal)) : (this.text.Substring(0, caretPositionInternal) + this.text.Substring(caretSelectPositionInternal, this.text.Length - caretSelectPositionInternal)));
				}
				if (onValidateInput != null)
				{
					input = onValidateInput(text, num, input);
				}
				else if (characterValidation != global::UnityEngine.UI.InputField.CharacterValidation.None)
				{
					input = Validate(text, num, input);
				}
				if (input != 0)
				{
					Insert(input);
				}
			}
		}

		protected void UpdateLabel()
		{
			if (m_TextComponent != null && m_TextComponent.font != null && !m_PreventFontCallback)
			{
				m_PreventFontCallback = true;
				string text;
				if (global::UnityEngine.EventSystems.EventSystem.current != null && base.gameObject == global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject && compositionString.Length > 0)
				{
					m_IsCompositionActive = true;
					text = this.text.Substring(0, m_CaretPosition) + compositionString + this.text.Substring(m_CaretPosition);
				}
				else
				{
					m_IsCompositionActive = false;
					text = this.text;
				}
				string text2 = ((inputType != global::UnityEngine.UI.InputField.InputType.Password) ? text : new string(asteriskChar, text.Length));
				bool flag = string.IsNullOrEmpty(text);
				if (m_Placeholder != null)
				{
					m_Placeholder.enabled = flag;
				}
				if (!m_AllowInput)
				{
					m_DrawStart = 0;
					m_DrawEnd = m_Text.Length;
				}
				textComponent.SetLayoutDirty();
				if (!flag)
				{
					global::UnityEngine.Vector2 size = m_TextComponent.rectTransform.rect.size;
					global::UnityEngine.TextGenerationSettings generationSettings = m_TextComponent.GetGenerationSettings(size);
					generationSettings.generateOutOfBounds = true;
					cachedInputTextGenerator.PopulateWithErrors(text2, generationSettings, base.gameObject);
					SetDrawRangeToContainCaretPosition(caretSelectPositionInternal);
					text2 = text2.Substring(m_DrawStart, global::UnityEngine.Mathf.Min(m_DrawEnd, text2.Length) - m_DrawStart);
					SetCaretVisible();
				}
				m_TextComponent.text = text2;
				MarkGeometryAsDirty();
				m_PreventFontCallback = false;
			}
		}

		private bool IsSelectionVisible()
		{
			if (m_DrawStart > caretPositionInternal || m_DrawStart > caretSelectPositionInternal)
			{
				return false;
			}
			if (m_DrawEnd < caretPositionInternal || m_DrawEnd < caretSelectPositionInternal)
			{
				return false;
			}
			return true;
		}

		private static int GetLineStartPosition(global::UnityEngine.TextGenerator gen, int line)
		{
			line = global::UnityEngine.Mathf.Clamp(line, 0, gen.lines.Count - 1);
			return gen.lines[line].startCharIdx;
		}

		private static int GetLineEndPosition(global::UnityEngine.TextGenerator gen, int line)
		{
			line = global::UnityEngine.Mathf.Max(line, 0);
			if (line + 1 < gen.lines.Count)
			{
				return gen.lines[line + 1].startCharIdx - 1;
			}
			return gen.characterCountVisible;
		}

		private void SetDrawRangeToContainCaretPosition(int caretPos)
		{
			if (cachedInputTextGenerator.lineCount <= 0)
			{
				return;
			}
			global::UnityEngine.Vector2 size = cachedInputTextGenerator.rectExtents.size;
			if (multiLine)
			{
				global::System.Collections.Generic.IList<global::UnityEngine.UILineInfo> lines = cachedInputTextGenerator.lines;
				int num = DetermineCharacterLine(caretPos, cachedInputTextGenerator);
				if (caretPos > m_DrawEnd)
				{
					m_DrawEnd = GetLineEndPosition(cachedInputTextGenerator, num);
					float num2 = lines[num].topY - (float)lines[num].height;
					if (num == lines.Count - 1)
					{
						num2 += lines[num].leading;
					}
					int num3 = num;
					while (num3 > 0 && !(lines[num3 - 1].topY - num2 > size.y))
					{
						num3--;
					}
					m_DrawStart = GetLineStartPosition(cachedInputTextGenerator, num3);
					return;
				}
				if (caretPos < m_DrawStart)
				{
					m_DrawStart = GetLineStartPosition(cachedInputTextGenerator, num);
				}
				int num4 = DetermineCharacterLine(m_DrawStart, cachedInputTextGenerator);
				int i = num4;
				float topY = lines[num4].topY;
				float num5 = lines[i].topY - (float)lines[i].height;
				if (i == lines.Count - 1)
				{
					num5 += lines[i].leading;
				}
				for (; i < lines.Count - 1; i++)
				{
					num5 = lines[i + 1].topY - (float)lines[i + 1].height;
					if (i + 1 == lines.Count - 1)
					{
						num5 += lines[i + 1].leading;
					}
					if (topY - num5 > size.y)
					{
						break;
					}
				}
				m_DrawEnd = GetLineEndPosition(cachedInputTextGenerator, i);
				while (num4 > 0)
				{
					topY = lines[num4 - 1].topY;
					if (topY - num5 > size.y)
					{
						break;
					}
					num4--;
				}
				m_DrawStart = GetLineStartPosition(cachedInputTextGenerator, num4);
				return;
			}
			global::System.Collections.Generic.IList<global::UnityEngine.UICharInfo> characters = cachedInputTextGenerator.characters;
			if (m_DrawEnd > cachedInputTextGenerator.characterCountVisible)
			{
				m_DrawEnd = cachedInputTextGenerator.characterCountVisible;
			}
			float num6 = 0f;
			if (caretPos > m_DrawEnd || (caretPos == m_DrawEnd && m_DrawStart > 0))
			{
				m_DrawEnd = caretPos;
				m_DrawStart = m_DrawEnd - 1;
				while (m_DrawStart >= 0 && !(num6 + characters[m_DrawStart].charWidth > size.x))
				{
					num6 += characters[m_DrawStart].charWidth;
					m_DrawStart--;
				}
				m_DrawStart++;
			}
			else
			{
				if (caretPos < m_DrawStart)
				{
					m_DrawStart = caretPos;
				}
				m_DrawEnd = m_DrawStart;
			}
			while (m_DrawEnd < cachedInputTextGenerator.characterCountVisible)
			{
				num6 += characters[m_DrawEnd].charWidth;
				if (!(num6 > size.x))
				{
					m_DrawEnd++;
					continue;
				}
				break;
			}
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
			if (InPlaceEditing() || shouldHideMobileInput)
			{
				if (m_CachedInputRenderer == null && m_TextComponent != null)
				{
					global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject(base.transform.name + " Input Caret", typeof(global::UnityEngine.RectTransform), typeof(global::UnityEngine.CanvasRenderer));
					gameObject.hideFlags = global::UnityEngine.HideFlags.DontSave;
					gameObject.transform.SetParent(m_TextComponent.transform.parent);
					gameObject.transform.SetAsFirstSibling();
					gameObject.layer = base.gameObject.layer;
					caretRectTrans = gameObject.GetComponent<global::UnityEngine.RectTransform>();
					m_CachedInputRenderer = gameObject.GetComponent<global::UnityEngine.CanvasRenderer>();
					m_CachedInputRenderer.SetMaterial(m_TextComponent.GetModifiedMaterial(global::UnityEngine.UI.Graphic.defaultGraphicMaterial), global::UnityEngine.Texture2D.whiteTexture);
					gameObject.AddComponent<global::UnityEngine.UI.LayoutElement>().ignoreLayout = true;
					AssignPositioningIfNeeded();
				}
				if (!(m_CachedInputRenderer == null))
				{
					OnFillVBO(mesh);
					m_CachedInputRenderer.SetMesh(mesh);
				}
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
			if (!isFocused)
			{
				vertexHelper.FillMesh(vbo);
				return;
			}
			global::UnityEngine.Vector2 roundingOffset = m_TextComponent.PixelAdjustPoint(global::UnityEngine.Vector2.zero);
			if (!hasSelection)
			{
				GenerateCaret(vertexHelper, roundingOffset);
			}
			else
			{
				GenerateHighlight(vertexHelper, roundingOffset);
			}
			vertexHelper.FillMesh(vbo);
		}

		private void GenerateCaret(global::UnityEngine.UI.VertexHelper vbo, global::UnityEngine.Vector2 roundingOffset)
		{
			if (!m_CaretVisible)
			{
				return;
			}
			if (m_CursorVerts == null)
			{
				CreateCursorVerts();
			}
			float num = m_CaretWidth;
			int num2 = global::UnityEngine.Mathf.Max(0, caretPositionInternal - m_DrawStart);
			global::UnityEngine.TextGenerator cachedTextGenerator = m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator == null || cachedTextGenerator.lineCount == 0)
			{
				return;
			}
			global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
			if (num2 < cachedTextGenerator.characters.Count)
			{
				zero.x = cachedTextGenerator.characters[num2].cursorPos.x;
			}
			zero.x /= m_TextComponent.pixelsPerUnit;
			if (zero.x > m_TextComponent.rectTransform.rect.xMax)
			{
				zero.x = m_TextComponent.rectTransform.rect.xMax;
			}
			int index = DetermineCharacterLine(num2, cachedTextGenerator);
			zero.y = cachedTextGenerator.lines[index].topY / m_TextComponent.pixelsPerUnit;
			float num3 = (float)cachedTextGenerator.lines[index].height / m_TextComponent.pixelsPerUnit;
			for (int i = 0; i < m_CursorVerts.Length; i++)
			{
				m_CursorVerts[i].color = caretColor;
			}
			m_CursorVerts[0].position = new global::UnityEngine.Vector3(zero.x, zero.y - num3, 0f);
			m_CursorVerts[1].position = new global::UnityEngine.Vector3(zero.x + num, zero.y - num3, 0f);
			m_CursorVerts[2].position = new global::UnityEngine.Vector3(zero.x + num, zero.y, 0f);
			m_CursorVerts[3].position = new global::UnityEngine.Vector3(zero.x, zero.y, 0f);
			if (roundingOffset != global::UnityEngine.Vector2.zero)
			{
				for (int j = 0; j < m_CursorVerts.Length; j++)
				{
					global::UnityEngine.UIVertex uIVertex = m_CursorVerts[j];
					uIVertex.position.x += roundingOffset.x;
					uIVertex.position.y += roundingOffset.y;
				}
			}
			vbo.AddUIVertexQuad(m_CursorVerts);
			int num4 = global::UnityEngine.Screen.height;
			int targetDisplay = m_TextComponent.canvas.targetDisplay;
			if (targetDisplay > 0 && targetDisplay < global::UnityEngine.Display.displays.Length)
			{
				num4 = global::UnityEngine.Display.displays[targetDisplay].renderingHeight;
			}
			global::UnityEngine.Camera cam = ((m_TextComponent.canvas.renderMode != global::UnityEngine.RenderMode.ScreenSpaceOverlay) ? m_TextComponent.canvas.worldCamera : null);
			global::UnityEngine.Vector3 worldPoint = m_CachedInputRenderer.gameObject.transform.TransformPoint(m_CursorVerts[0].position);
			global::UnityEngine.Vector2 compositionCursorPos = global::UnityEngine.RectTransformUtility.WorldToScreenPoint(cam, worldPoint);
			compositionCursorPos.y = (float)num4 - compositionCursorPos.y;
			if (input != null)
			{
				input.compositionCursorPos = compositionCursorPos;
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
			int num = global::UnityEngine.Mathf.Max(0, caretPositionInternal - m_DrawStart);
			int num2 = global::UnityEngine.Mathf.Max(0, caretSelectPositionInternal - m_DrawStart);
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			num2--;
			global::UnityEngine.TextGenerator cachedTextGenerator = m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator.lineCount <= 0)
			{
				return;
			}
			int num4 = DetermineCharacterLine(num, cachedTextGenerator);
			int lineEndPosition = GetLineEndPosition(cachedTextGenerator, num4);
			global::UnityEngine.UIVertex simpleVert = global::UnityEngine.UIVertex.simpleVert;
			simpleVert.uv0 = global::UnityEngine.Vector2.zero;
			simpleVert.color = selectionColor;
			for (int i = num; i <= num2 && i < cachedTextGenerator.characterCount; i++)
			{
				if (i == lineEndPosition || i == num2)
				{
					global::UnityEngine.UICharInfo uICharInfo = cachedTextGenerator.characters[num];
					global::UnityEngine.UICharInfo uICharInfo2 = cachedTextGenerator.characters[i];
					global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(uICharInfo.cursorPos.x / m_TextComponent.pixelsPerUnit, cachedTextGenerator.lines[num4].topY / m_TextComponent.pixelsPerUnit);
					global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2((uICharInfo2.cursorPos.x + uICharInfo2.charWidth) / m_TextComponent.pixelsPerUnit, vector.y - (float)cachedTextGenerator.lines[num4].height / m_TextComponent.pixelsPerUnit);
					if (vector2.x > m_TextComponent.rectTransform.rect.xMax || vector2.x < m_TextComponent.rectTransform.rect.xMin)
					{
						vector2.x = m_TextComponent.rectTransform.rect.xMax;
					}
					int currentVertCount = vbo.currentVertCount;
					simpleVert.position = new global::UnityEngine.Vector3(vector.x, vector2.y, 0f) + (global::UnityEngine.Vector3)roundingOffset;
					vbo.AddVert(simpleVert);
					simpleVert.position = new global::UnityEngine.Vector3(vector2.x, vector2.y, 0f) + (global::UnityEngine.Vector3)roundingOffset;
					vbo.AddVert(simpleVert);
					simpleVert.position = new global::UnityEngine.Vector3(vector2.x, vector.y, 0f) + (global::UnityEngine.Vector3)roundingOffset;
					vbo.AddVert(simpleVert);
					simpleVert.position = new global::UnityEngine.Vector3(vector.x, vector.y, 0f) + (global::UnityEngine.Vector3)roundingOffset;
					vbo.AddVert(simpleVert);
					vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
					vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
					num = i + 1;
					num4++;
					lineEndPosition = GetLineEndPosition(cachedTextGenerator, num4);
				}
			}
		}

		protected char Validate(string text, int pos, char ch)
		{
			if (characterValidation == global::UnityEngine.UI.InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (characterValidation == global::UnityEngine.UI.InputField.CharacterValidation.Integer || characterValidation == global::UnityEngine.UI.InputField.CharacterValidation.Decimal)
			{
				bool num = pos == 0 && text.Length > 0 && text[0] == '-';
				bool flag = text.Length > 0 && text[0] == '-' && ((caretPositionInternal == 0 && caretSelectPositionInternal > 0) || (caretSelectPositionInternal == 0 && caretPositionInternal > 0));
				bool flag2 = caretPositionInternal == 0 || caretSelectPositionInternal == 0;
				if (!num || flag)
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && (pos == 0 || flag2) && !text.Contains('-'))
					{
						return ch;
					}
					if ((ch == '.' || ch == ',') && characterValidation == global::UnityEngine.UI.InputField.CharacterValidation.Decimal && text.IndexOfAny(new char[2] { '.', ',' }) == -1)
					{
						return ch;
					}
					if (characterValidation == global::UnityEngine.UI.InputField.CharacterValidation.Integer && ch == '.' && (pos == 0 || flag2) && !text.Contains('-'))
					{
						return '-';
					}
				}
			}
			else if (characterValidation == global::UnityEngine.UI.InputField.CharacterValidation.Alphanumeric)
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
			else if (characterValidation == global::UnityEngine.UI.InputField.CharacterValidation.Name)
			{
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && (pos == 0 || text[pos - 1] == ' ' || text[pos - 1] == '-'))
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && pos > 0 && text[pos - 1] != ' ' && text[pos - 1] != '\'' && text[pos - 1] != '-')
					{
						return char.ToLower(ch);
					}
					return ch;
				}
				if (ch == '\'' && !text.Contains("'") && (pos <= 0 || (text[pos - 1] != ' ' && text[pos - 1] != '\'' && text[pos - 1] != '-')) && (pos >= text.Length || (text[pos] != ' ' && text[pos] != '\'' && text[pos] != '-')))
				{
					return ch;
				}
				if ((ch == ' ' || ch == '-') && pos != 0 && (pos <= 0 || (text[pos - 1] != ' ' && text[pos - 1] != '\'' && text[pos - 1] != '-')) && (pos >= text.Length || (text[pos] != ' ' && text[pos] != '\'' && text[pos - 1] != '-')))
				{
					return ch;
				}
			}
			else if (characterValidation == global::UnityEngine.UI.InputField.CharacterValidation.EmailAddress)
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
					char c = ((text.Length > 0) ? text[global::UnityEngine.Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
					if (num2 != '.' && c != '.')
					{
						return ch;
					}
				}
			}
			return '\0';
		}

		public void ActivateInputField()
		{
			if (!(m_TextComponent == null) && !(m_TextComponent.font == null) && IsActive() && IsInteractable())
			{
				if (isFocused && m_Keyboard != null && !m_Keyboard.active)
				{
					m_Keyboard.active = true;
					m_Keyboard.text = m_Text;
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
			if (TouchScreenKeyboardShouldBeUsed())
			{
				if (input != null && input.touchSupported)
				{
					global::UnityEngine.TouchScreenKeyboard.hideInput = shouldHideMobileInput;
				}
				m_Keyboard = ((inputType == global::UnityEngine.UI.InputField.InputType.Password) ? global::UnityEngine.TouchScreenKeyboard.Open(m_Text, keyboardType, autocorrection: false, multiLine, secure: true, alert: false, "", characterLimit) : global::UnityEngine.TouchScreenKeyboard.Open(m_Text, keyboardType, inputType == global::UnityEngine.UI.InputField.InputType.AutoCorrect, multiLine, secure: false, alert: false, "", characterLimit));
				if (!m_TouchKeyboardAllowsInPlaceEditing)
				{
					MoveTextEnd(shift: false);
				}
			}
			if (!global::UnityEngine.TouchScreenKeyboard.isSupported || m_TouchKeyboardAllowsInPlaceEditing)
			{
				if (input != null)
				{
					input.imeCompositionMode = global::UnityEngine.IMECompositionMode.On;
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

		public void DeactivateInputField()
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
				if (m_WasCanceled)
				{
					text = m_OriginalText;
				}
				SendOnEndEdit();
				if (m_Keyboard != null)
				{
					m_Keyboard.active = false;
					m_Keyboard = null;
				}
				m_CaretPosition = (m_CaretSelectPosition = 0);
				if (input != null)
				{
					input.imeCompositionMode = global::UnityEngine.IMECompositionMode.Auto;
				}
			}
			MarkGeometryAsDirty();
		}

		public override void OnDeselect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			DeactivateInputField();
			base.OnDeselect(eventData);
		}

		public virtual void OnSubmit(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			if (IsActive() && IsInteractable() && !isFocused)
			{
				m_ShouldActivateNextUpdate = true;
			}
		}

		private void EnforceContentType()
		{
			switch (contentType)
			{
			case global::UnityEngine.UI.InputField.ContentType.Standard:
				m_InputType = global::UnityEngine.UI.InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.Default;
				m_CharacterValidation = global::UnityEngine.UI.InputField.CharacterValidation.None;
				break;
			case global::UnityEngine.UI.InputField.ContentType.Autocorrected:
				m_InputType = global::UnityEngine.UI.InputField.InputType.AutoCorrect;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.Default;
				m_CharacterValidation = global::UnityEngine.UI.InputField.CharacterValidation.None;
				break;
			case global::UnityEngine.UI.InputField.ContentType.IntegerNumber:
				m_LineType = global::UnityEngine.UI.InputField.LineType.SingleLine;
				m_InputType = global::UnityEngine.UI.InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.NumbersAndPunctuation;
				m_CharacterValidation = global::UnityEngine.UI.InputField.CharacterValidation.Integer;
				break;
			case global::UnityEngine.UI.InputField.ContentType.DecimalNumber:
				m_LineType = global::UnityEngine.UI.InputField.LineType.SingleLine;
				m_InputType = global::UnityEngine.UI.InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.NumbersAndPunctuation;
				m_CharacterValidation = global::UnityEngine.UI.InputField.CharacterValidation.Decimal;
				break;
			case global::UnityEngine.UI.InputField.ContentType.Alphanumeric:
				m_LineType = global::UnityEngine.UI.InputField.LineType.SingleLine;
				m_InputType = global::UnityEngine.UI.InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.ASCIICapable;
				m_CharacterValidation = global::UnityEngine.UI.InputField.CharacterValidation.Alphanumeric;
				break;
			case global::UnityEngine.UI.InputField.ContentType.Name:
				m_LineType = global::UnityEngine.UI.InputField.LineType.SingleLine;
				m_InputType = global::UnityEngine.UI.InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.NamePhonePad;
				m_CharacterValidation = global::UnityEngine.UI.InputField.CharacterValidation.Name;
				break;
			case global::UnityEngine.UI.InputField.ContentType.EmailAddress:
				m_LineType = global::UnityEngine.UI.InputField.LineType.SingleLine;
				m_InputType = global::UnityEngine.UI.InputField.InputType.Standard;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.EmailAddress;
				m_CharacterValidation = global::UnityEngine.UI.InputField.CharacterValidation.EmailAddress;
				break;
			case global::UnityEngine.UI.InputField.ContentType.Password:
				m_LineType = global::UnityEngine.UI.InputField.LineType.SingleLine;
				m_InputType = global::UnityEngine.UI.InputField.InputType.Password;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.Default;
				m_CharacterValidation = global::UnityEngine.UI.InputField.CharacterValidation.None;
				break;
			case global::UnityEngine.UI.InputField.ContentType.Pin:
				m_LineType = global::UnityEngine.UI.InputField.LineType.SingleLine;
				m_InputType = global::UnityEngine.UI.InputField.InputType.Password;
				m_KeyboardType = global::UnityEngine.TouchScreenKeyboardType.NumberPad;
				m_CharacterValidation = global::UnityEngine.UI.InputField.CharacterValidation.Integer;
				break;
			}
			EnforceTextHOverflow();
		}

		private void EnforceTextHOverflow()
		{
			if (m_TextComponent != null)
			{
				if (multiLine)
				{
					m_TextComponent.horizontalOverflow = global::UnityEngine.HorizontalWrapMode.Wrap;
				}
				else
				{
					m_TextComponent.horizontalOverflow = global::UnityEngine.HorizontalWrapMode.Overflow;
				}
			}
		}

		private void SetToCustomIfContentTypeIsNot(params global::UnityEngine.UI.InputField.ContentType[] allowedContentTypes)
		{
			if (contentType == global::UnityEngine.UI.InputField.ContentType.Custom)
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
			contentType = global::UnityEngine.UI.InputField.ContentType.Custom;
		}

		private void SetToCustom()
		{
			if (contentType != global::UnityEngine.UI.InputField.ContentType.Custom)
			{
				contentType = global::UnityEngine.UI.InputField.ContentType.Custom;
			}
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
	}
}
