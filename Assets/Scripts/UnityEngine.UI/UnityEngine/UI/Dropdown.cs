namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Legacy/Dropdown", 102)]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public class Dropdown : global::UnityEngine.UI.Selectable, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.ISubmitHandler, global::UnityEngine.EventSystems.ICancelHandler
	{
		protected internal class DropdownItem : global::UnityEngine.MonoBehaviour, global::UnityEngine.EventSystems.IPointerEnterHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.ICancelHandler
		{
			[global::UnityEngine.SerializeField]
			private global::UnityEngine.UI.Text m_Text;

			[global::UnityEngine.SerializeField]
			private global::UnityEngine.UI.Image m_Image;

			[global::UnityEngine.SerializeField]
			private global::UnityEngine.RectTransform m_RectTransform;

			[global::UnityEngine.SerializeField]
			private global::UnityEngine.UI.Toggle m_Toggle;

			public global::UnityEngine.UI.Text text
			{
				get
				{
					return m_Text;
				}
				set
				{
					m_Text = value;
				}
			}

			public global::UnityEngine.UI.Image image
			{
				get
				{
					return m_Image;
				}
				set
				{
					m_Image = value;
				}
			}

			public global::UnityEngine.RectTransform rectTransform
			{
				get
				{
					return m_RectTransform;
				}
				set
				{
					m_RectTransform = value;
				}
			}

			public global::UnityEngine.UI.Toggle toggle
			{
				get
				{
					return m_Toggle;
				}
				set
				{
					m_Toggle = value;
				}
			}

			public virtual void OnPointerEnter(global::UnityEngine.EventSystems.PointerEventData eventData)
			{
				global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(base.gameObject);
			}

			public virtual void OnCancel(global::UnityEngine.EventSystems.BaseEventData eventData)
			{
				global::UnityEngine.UI.Dropdown componentInParent = GetComponentInParent<global::UnityEngine.UI.Dropdown>();
				if ((bool)componentInParent)
				{
					componentInParent.Hide();
				}
			}
		}

		[global::System.Serializable]
		public class OptionData
		{
			[global::UnityEngine.SerializeField]
			private string m_Text;

			[global::UnityEngine.SerializeField]
			private global::UnityEngine.Sprite m_Image;

			public string text
			{
				get
				{
					return m_Text;
				}
				set
				{
					m_Text = value;
				}
			}

			public global::UnityEngine.Sprite image
			{
				get
				{
					return m_Image;
				}
				set
				{
					m_Image = value;
				}
			}

			public OptionData()
			{
			}

			public OptionData(string text)
			{
				this.text = text;
			}

			public OptionData(global::UnityEngine.Sprite image)
			{
				this.image = image;
			}

			public OptionData(string text, global::UnityEngine.Sprite image)
			{
				this.text = text;
				this.image = image;
			}
		}

		[global::System.Serializable]
		public class OptionDataList
		{
			[global::UnityEngine.SerializeField]
			private global::System.Collections.Generic.List<global::UnityEngine.UI.Dropdown.OptionData> m_Options;

			public global::System.Collections.Generic.List<global::UnityEngine.UI.Dropdown.OptionData> options
			{
				get
				{
					return m_Options;
				}
				set
				{
					m_Options = value;
				}
			}

			public OptionDataList()
			{
				options = new global::System.Collections.Generic.List<global::UnityEngine.UI.Dropdown.OptionData>();
			}
		}

		[global::System.Serializable]
		public class DropdownEvent : global::UnityEngine.Events.UnityEvent<int>
		{
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.RectTransform m_Template;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Text m_CaptionText;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Image m_CaptionImage;

		[global::UnityEngine.Space]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Text m_ItemText;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Image m_ItemImage;

		[global::UnityEngine.Space]
		[global::UnityEngine.SerializeField]
		private int m_Value;

		[global::UnityEngine.Space]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Dropdown.OptionDataList m_Options = new global::UnityEngine.UI.Dropdown.OptionDataList();

		[global::UnityEngine.Space]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Dropdown.DropdownEvent m_OnValueChanged = new global::UnityEngine.UI.Dropdown.DropdownEvent();

		[global::UnityEngine.SerializeField]
		private float m_AlphaFadeSpeed = 0.15f;

		private global::UnityEngine.GameObject m_Dropdown;

		private global::UnityEngine.GameObject m_Blocker;

		private global::System.Collections.Generic.List<global::UnityEngine.UI.Dropdown.DropdownItem> m_Items = new global::System.Collections.Generic.List<global::UnityEngine.UI.Dropdown.DropdownItem>();

		private global::UnityEngine.UI.CoroutineTween.TweenRunner<global::UnityEngine.UI.CoroutineTween.FloatTween> m_AlphaTweenRunner;

		private bool validTemplate;

		private const int kHighSortingLayer = 30000;

		private static global::UnityEngine.UI.Dropdown.OptionData s_NoOptionData = new global::UnityEngine.UI.Dropdown.OptionData();

		public global::UnityEngine.RectTransform template
		{
			get
			{
				return m_Template;
			}
			set
			{
				m_Template = value;
				RefreshShownValue();
			}
		}

		public global::UnityEngine.UI.Text captionText
		{
			get
			{
				return m_CaptionText;
			}
			set
			{
				m_CaptionText = value;
				RefreshShownValue();
			}
		}

		public global::UnityEngine.UI.Image captionImage
		{
			get
			{
				return m_CaptionImage;
			}
			set
			{
				m_CaptionImage = value;
				RefreshShownValue();
			}
		}

		public global::UnityEngine.UI.Text itemText
		{
			get
			{
				return m_ItemText;
			}
			set
			{
				m_ItemText = value;
				RefreshShownValue();
			}
		}

		public global::UnityEngine.UI.Image itemImage
		{
			get
			{
				return m_ItemImage;
			}
			set
			{
				m_ItemImage = value;
				RefreshShownValue();
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.UI.Dropdown.OptionData> options
		{
			get
			{
				return m_Options.options;
			}
			set
			{
				m_Options.options = value;
				RefreshShownValue();
			}
		}

		public global::UnityEngine.UI.Dropdown.DropdownEvent onValueChanged
		{
			get
			{
				return m_OnValueChanged;
			}
			set
			{
				m_OnValueChanged = value;
			}
		}

		public float alphaFadeSpeed
		{
			get
			{
				return m_AlphaFadeSpeed;
			}
			set
			{
				m_AlphaFadeSpeed = value;
			}
		}

		public int value
		{
			get
			{
				return m_Value;
			}
			set
			{
				Set(value);
			}
		}

		public void SetValueWithoutNotify(int input)
		{
			Set(input, sendCallback: false);
		}

		private void Set(int value, bool sendCallback = true)
		{
			if (!global::UnityEngine.Application.isPlaying || (value != m_Value && options.Count != 0))
			{
				m_Value = global::UnityEngine.Mathf.Clamp(value, 0, options.Count - 1);
				RefreshShownValue();
				if (sendCallback)
				{
					global::UnityEngine.UISystemProfilerApi.AddMarker("Dropdown.value", this);
					m_OnValueChanged.Invoke(m_Value);
				}
			}
		}

		protected Dropdown()
		{
		}

		protected override void Awake()
		{
			if ((bool)m_CaptionImage)
			{
				m_CaptionImage.enabled = m_CaptionImage.sprite != null;
			}
			if ((bool)m_Template)
			{
				m_Template.gameObject.SetActive(value: false);
			}
		}

		protected override void Start()
		{
			m_AlphaTweenRunner = new global::UnityEngine.UI.CoroutineTween.TweenRunner<global::UnityEngine.UI.CoroutineTween.FloatTween>();
			m_AlphaTweenRunner.Init(this);
			base.Start();
			RefreshShownValue();
		}

		protected override void OnDisable()
		{
			ImmediateDestroyDropdownList();
			if (m_Blocker != null)
			{
				DestroyBlocker(m_Blocker);
			}
			m_Blocker = null;
			base.OnDisable();
		}

		public void RefreshShownValue()
		{
			global::UnityEngine.UI.Dropdown.OptionData optionData = s_NoOptionData;
			if (options.Count > 0)
			{
				optionData = options[global::UnityEngine.Mathf.Clamp(m_Value, 0, options.Count - 1)];
			}
			if ((bool)m_CaptionText)
			{
				if (optionData != null && optionData.text != null)
				{
					m_CaptionText.text = optionData.text;
				}
				else
				{
					m_CaptionText.text = "";
				}
			}
			if ((bool)m_CaptionImage)
			{
				if (optionData != null)
				{
					m_CaptionImage.sprite = optionData.image;
				}
				else
				{
					m_CaptionImage.sprite = null;
				}
				m_CaptionImage.enabled = m_CaptionImage.sprite != null;
			}
		}

		public void AddOptions(global::System.Collections.Generic.List<global::UnityEngine.UI.Dropdown.OptionData> options)
		{
			this.options.AddRange(options);
			RefreshShownValue();
		}

		public void AddOptions(global::System.Collections.Generic.List<string> options)
		{
			int count = options.Count;
			for (int i = 0; i < count; i++)
			{
				this.options.Add(new global::UnityEngine.UI.Dropdown.OptionData(options[i]));
			}
			RefreshShownValue();
		}

		public void AddOptions(global::System.Collections.Generic.List<global::UnityEngine.Sprite> options)
		{
			int count = options.Count;
			for (int i = 0; i < count; i++)
			{
				this.options.Add(new global::UnityEngine.UI.Dropdown.OptionData(options[i]));
			}
			RefreshShownValue();
		}

		public void ClearOptions()
		{
			options.Clear();
			m_Value = 0;
			RefreshShownValue();
		}

		private void SetupTemplate(global::UnityEngine.Canvas rootCanvas)
		{
			validTemplate = false;
			if (!m_Template)
			{
				global::UnityEngine.Debug.LogError("The dropdown template is not assigned. The template needs to be assigned and must have a child GameObject with a Toggle component serving as the item.", this);
				return;
			}
			global::UnityEngine.GameObject gameObject = m_Template.gameObject;
			gameObject.SetActive(value: true);
			global::UnityEngine.UI.Toggle componentInChildren = m_Template.GetComponentInChildren<global::UnityEngine.UI.Toggle>();
			validTemplate = true;
			if (!componentInChildren || componentInChildren.transform == template)
			{
				validTemplate = false;
				global::UnityEngine.Debug.LogError("The dropdown template is not valid. The template must have a child GameObject with a Toggle component serving as the item.", template);
			}
			else if (!(componentInChildren.transform.parent is global::UnityEngine.RectTransform))
			{
				validTemplate = false;
				global::UnityEngine.Debug.LogError("The dropdown template is not valid. The child GameObject with a Toggle component (the item) must have a RectTransform on its parent.", template);
			}
			else if (itemText != null && !itemText.transform.IsChildOf(componentInChildren.transform))
			{
				validTemplate = false;
				global::UnityEngine.Debug.LogError("The dropdown template is not valid. The Item Text must be on the item GameObject or children of it.", template);
			}
			else if (itemImage != null && !itemImage.transform.IsChildOf(componentInChildren.transform))
			{
				validTemplate = false;
				global::UnityEngine.Debug.LogError("The dropdown template is not valid. The Item Image must be on the item GameObject or children of it.", template);
			}
			if (!validTemplate)
			{
				gameObject.SetActive(value: false);
				return;
			}
			global::UnityEngine.UI.Dropdown.DropdownItem dropdownItem = componentInChildren.gameObject.AddComponent<global::UnityEngine.UI.Dropdown.DropdownItem>();
			dropdownItem.text = m_ItemText;
			dropdownItem.image = m_ItemImage;
			dropdownItem.toggle = componentInChildren;
			dropdownItem.rectTransform = (global::UnityEngine.RectTransform)componentInChildren.transform;
			global::UnityEngine.Canvas canvas = null;
			global::UnityEngine.Transform parent = m_Template.parent;
			while (parent != null)
			{
				canvas = parent.GetComponent<global::UnityEngine.Canvas>();
				if (canvas != null)
				{
					break;
				}
				parent = parent.parent;
			}
			if (!gameObject.TryGetComponent<global::UnityEngine.Canvas>(out var _))
			{
				global::UnityEngine.Canvas canvas2 = gameObject.AddComponent<global::UnityEngine.Canvas>();
				canvas2.overrideSorting = true;
				canvas2.sortingOrder = 30000;
				canvas2.sortingLayerID = rootCanvas.sortingLayerID;
			}
			if (canvas != null)
			{
				global::UnityEngine.Component[] components = canvas.GetComponents<global::UnityEngine.EventSystems.BaseRaycaster>();
				global::UnityEngine.Component[] array = components;
				for (int i = 0; i < array.Length; i++)
				{
					global::System.Type type = array[i].GetType();
					if (gameObject.GetComponent(type) == null)
					{
						gameObject.AddComponent(type);
					}
				}
			}
			else
			{
				GetOrAddComponent<global::UnityEngine.UI.GraphicRaycaster>(gameObject);
			}
			GetOrAddComponent<global::UnityEngine.CanvasGroup>(gameObject);
			gameObject.SetActive(value: false);
			validTemplate = true;
		}

		private static T GetOrAddComponent<T>(global::UnityEngine.GameObject go) where T : global::UnityEngine.Component
		{
			T val = go.GetComponent<T>();
			if (!val)
			{
				val = go.AddComponent<T>();
			}
			return val;
		}

		public virtual void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			Show();
		}

		public virtual void OnSubmit(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			Show();
		}

		public virtual void OnCancel(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			Hide();
		}

		public void Show()
		{
			if (!IsActive() || !IsInteractable() || m_Dropdown != null)
			{
				return;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Canvas> list = global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Get();
			base.gameObject.GetComponentsInParent(includeInactive: false, list);
			if (list.Count == 0)
			{
				return;
			}
			int count = list.Count;
			global::UnityEngine.Canvas canvas = list[count - 1];
			for (int i = 0; i < count; i++)
			{
				if (list[i].isRootCanvas || list[i].overrideSorting)
				{
					canvas = list[i];
					break;
				}
			}
			global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.Canvas>, global::UnityEngine.Canvas>.Release(list);
			if (!validTemplate)
			{
				SetupTemplate(canvas);
				if (!validTemplate)
				{
					return;
				}
			}
			m_Template.gameObject.SetActive(value: true);
			m_Dropdown = CreateDropdownList(m_Template.gameObject);
			m_Dropdown.name = "Dropdown List";
			m_Dropdown.SetActive(value: true);
			global::UnityEngine.RectTransform rectTransform = m_Dropdown.transform as global::UnityEngine.RectTransform;
			rectTransform.SetParent(m_Template.transform.parent, worldPositionStays: false);
			global::UnityEngine.UI.Dropdown.DropdownItem componentInChildren = m_Dropdown.GetComponentInChildren<global::UnityEngine.UI.Dropdown.DropdownItem>();
			global::UnityEngine.RectTransform rectTransform2 = componentInChildren.rectTransform.parent.gameObject.transform as global::UnityEngine.RectTransform;
			componentInChildren.rectTransform.gameObject.SetActive(value: true);
			global::UnityEngine.Rect rect = rectTransform2.rect;
			global::UnityEngine.Rect rect2 = componentInChildren.rectTransform.rect;
			global::UnityEngine.Vector2 vector = rect2.min - rect.min + (global::UnityEngine.Vector2)componentInChildren.rectTransform.localPosition;
			global::UnityEngine.Vector2 vector2 = rect2.max - rect.max + (global::UnityEngine.Vector2)componentInChildren.rectTransform.localPosition;
			global::UnityEngine.Vector2 size = rect2.size;
			m_Items.Clear();
			global::UnityEngine.UI.Toggle toggle = null;
			int count2 = options.Count;
			for (int j = 0; j < count2; j++)
			{
				global::UnityEngine.UI.Dropdown.OptionData data = options[j];
				global::UnityEngine.UI.Dropdown.DropdownItem item = AddItem(data, value == j, componentInChildren, m_Items);
				if (!(item == null))
				{
					item.toggle.isOn = value == j;
					item.toggle.onValueChanged.AddListener(delegate
					{
						OnSelectItem(item.toggle);
					});
					if (item.toggle.isOn)
					{
						item.toggle.Select();
					}
					if (toggle != null)
					{
						global::UnityEngine.UI.Navigation navigation = toggle.navigation;
						global::UnityEngine.UI.Navigation navigation2 = item.toggle.navigation;
						navigation.mode = global::UnityEngine.UI.Navigation.Mode.Explicit;
						navigation2.mode = global::UnityEngine.UI.Navigation.Mode.Explicit;
						navigation.selectOnDown = item.toggle;
						navigation.selectOnRight = item.toggle;
						navigation2.selectOnLeft = toggle;
						navigation2.selectOnUp = toggle;
						toggle.navigation = navigation;
						item.toggle.navigation = navigation2;
					}
					toggle = item.toggle;
				}
			}
			global::UnityEngine.Vector2 sizeDelta = rectTransform2.sizeDelta;
			sizeDelta.y = size.y * (float)m_Items.Count + vector.y - vector2.y;
			rectTransform2.sizeDelta = sizeDelta;
			float num = rectTransform.rect.height - rectTransform2.rect.height;
			if (num > 0f)
			{
				rectTransform.sizeDelta = new global::UnityEngine.Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - num);
			}
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[4];
			rectTransform.GetWorldCorners(array);
			global::UnityEngine.RectTransform rectTransform3 = canvas.transform as global::UnityEngine.RectTransform;
			global::UnityEngine.Rect rect3 = rectTransform3.rect;
			for (int num2 = 0; num2 < 2; num2++)
			{
				bool flag = false;
				for (int num3 = 0; num3 < 4; num3++)
				{
					global::UnityEngine.Vector3 vector3 = rectTransform3.InverseTransformPoint(array[num3]);
					if ((vector3[num2] < rect3.min[num2] && !global::UnityEngine.Mathf.Approximately(vector3[num2], rect3.min[num2])) || (vector3[num2] > rect3.max[num2] && !global::UnityEngine.Mathf.Approximately(vector3[num2], rect3.max[num2])))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					global::UnityEngine.RectTransformUtility.FlipLayoutOnAxis(rectTransform, num2, keepPositioning: false, recursive: false);
				}
			}
			int count3 = m_Items.Count;
			for (int num4 = 0; num4 < count3; num4++)
			{
				global::UnityEngine.RectTransform rectTransform4 = m_Items[num4].rectTransform;
				rectTransform4.anchorMin = new global::UnityEngine.Vector2(rectTransform4.anchorMin.x, 0f);
				rectTransform4.anchorMax = new global::UnityEngine.Vector2(rectTransform4.anchorMax.x, 0f);
				rectTransform4.anchoredPosition = new global::UnityEngine.Vector2(rectTransform4.anchoredPosition.x, vector.y + size.y * (float)(count3 - 1 - num4) + size.y * rectTransform4.pivot.y);
				rectTransform4.sizeDelta = new global::UnityEngine.Vector2(rectTransform4.sizeDelta.x, size.y);
			}
			AlphaFadeList(m_AlphaFadeSpeed, 0f, 1f);
			m_Template.gameObject.SetActive(value: false);
			componentInChildren.gameObject.SetActive(value: false);
			m_Blocker = CreateBlocker(canvas);
		}

		protected virtual global::UnityEngine.GameObject CreateBlocker(global::UnityEngine.Canvas rootCanvas)
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("Blocker");
			gameObject.layer = rootCanvas.gameObject.layer;
			global::UnityEngine.RectTransform rectTransform = gameObject.AddComponent<global::UnityEngine.RectTransform>();
			rectTransform.SetParent(rootCanvas.transform, worldPositionStays: false);
			rectTransform.anchorMin = global::UnityEngine.Vector3.zero;
			rectTransform.anchorMax = global::UnityEngine.Vector3.one;
			rectTransform.sizeDelta = global::UnityEngine.Vector2.zero;
			global::UnityEngine.Canvas canvas = gameObject.AddComponent<global::UnityEngine.Canvas>();
			canvas.overrideSorting = true;
			global::UnityEngine.Canvas component = m_Dropdown.GetComponent<global::UnityEngine.Canvas>();
			canvas.sortingLayerID = component.sortingLayerID;
			canvas.sortingOrder = component.sortingOrder - 1;
			global::UnityEngine.Canvas canvas2 = null;
			global::UnityEngine.Transform parent = m_Template.parent;
			while (parent != null)
			{
				canvas2 = parent.GetComponent<global::UnityEngine.Canvas>();
				if (canvas2 != null)
				{
					break;
				}
				parent = parent.parent;
			}
			if (canvas2 != null)
			{
				global::UnityEngine.Component[] components = canvas2.GetComponents<global::UnityEngine.EventSystems.BaseRaycaster>();
				global::UnityEngine.Component[] array = components;
				for (int i = 0; i < array.Length; i++)
				{
					global::System.Type type = array[i].GetType();
					if (gameObject.GetComponent(type) == null)
					{
						gameObject.AddComponent(type);
					}
				}
			}
			else
			{
				GetOrAddComponent<global::UnityEngine.UI.GraphicRaycaster>(gameObject);
			}
			gameObject.AddComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.clear;
			gameObject.AddComponent<global::UnityEngine.UI.Button>().onClick.AddListener(Hide);
			gameObject.AddComponent<global::UnityEngine.CanvasGroup>().ignoreParentGroups = true;
			return gameObject;
		}

		protected virtual void DestroyBlocker(global::UnityEngine.GameObject blocker)
		{
			global::UnityEngine.Object.Destroy(blocker);
		}

		protected virtual global::UnityEngine.GameObject CreateDropdownList(global::UnityEngine.GameObject template)
		{
			return global::UnityEngine.Object.Instantiate(template);
		}

		protected virtual void DestroyDropdownList(global::UnityEngine.GameObject dropdownList)
		{
			global::UnityEngine.Object.Destroy(dropdownList);
		}

		protected virtual global::UnityEngine.UI.Dropdown.DropdownItem CreateItem(global::UnityEngine.UI.Dropdown.DropdownItem itemTemplate)
		{
			return global::UnityEngine.Object.Instantiate(itemTemplate);
		}

		protected virtual void DestroyItem(global::UnityEngine.UI.Dropdown.DropdownItem item)
		{
		}

		private global::UnityEngine.UI.Dropdown.DropdownItem AddItem(global::UnityEngine.UI.Dropdown.OptionData data, bool selected, global::UnityEngine.UI.Dropdown.DropdownItem itemTemplate, global::System.Collections.Generic.List<global::UnityEngine.UI.Dropdown.DropdownItem> items)
		{
			global::UnityEngine.UI.Dropdown.DropdownItem dropdownItem = CreateItem(itemTemplate);
			dropdownItem.rectTransform.SetParent(itemTemplate.rectTransform.parent, worldPositionStays: false);
			dropdownItem.gameObject.SetActive(value: true);
			dropdownItem.gameObject.name = "Item " + items.Count + ((data.text != null) ? (": " + data.text) : "");
			if (dropdownItem.toggle != null)
			{
				dropdownItem.toggle.isOn = false;
			}
			if ((bool)dropdownItem.text)
			{
				dropdownItem.text.text = data.text;
			}
			if ((bool)dropdownItem.image)
			{
				dropdownItem.image.sprite = data.image;
				dropdownItem.image.enabled = dropdownItem.image.sprite != null;
			}
			items.Add(dropdownItem);
			return dropdownItem;
		}

		private void AlphaFadeList(float duration, float alpha)
		{
			global::UnityEngine.CanvasGroup component = m_Dropdown.GetComponent<global::UnityEngine.CanvasGroup>();
			AlphaFadeList(duration, component.alpha, alpha);
		}

		private void AlphaFadeList(float duration, float start, float end)
		{
			if (!end.Equals(start))
			{
				global::UnityEngine.UI.CoroutineTween.FloatTween info = new global::UnityEngine.UI.CoroutineTween.FloatTween
				{
					duration = duration,
					startValue = start,
					targetValue = end
				};
				info.AddOnChangedCallback(SetAlpha);
				info.ignoreTimeScale = true;
				m_AlphaTweenRunner.StartTween(info);
			}
		}

		private void SetAlpha(float alpha)
		{
			if ((bool)m_Dropdown)
			{
				m_Dropdown.GetComponent<global::UnityEngine.CanvasGroup>().alpha = alpha;
			}
		}

		public void Hide()
		{
			if (m_Dropdown != null)
			{
				AlphaFadeList(m_AlphaFadeSpeed, 0f);
				if (IsActive())
				{
					StartCoroutine(DelayedDestroyDropdownList(m_AlphaFadeSpeed));
				}
			}
			if (m_Blocker != null)
			{
				DestroyBlocker(m_Blocker);
			}
			m_Blocker = null;
			Select();
		}

		private global::System.Collections.IEnumerator DelayedDestroyDropdownList(float delay)
		{
			yield return new global::UnityEngine.WaitForSecondsRealtime(delay);
			ImmediateDestroyDropdownList();
		}

		private void ImmediateDestroyDropdownList()
		{
			int count = m_Items.Count;
			for (int i = 0; i < count; i++)
			{
				if (m_Items[i] != null)
				{
					DestroyItem(m_Items[i]);
				}
			}
			m_Items.Clear();
			if (m_Dropdown != null)
			{
				DestroyDropdownList(m_Dropdown);
			}
			m_Dropdown = null;
		}

		private void OnSelectItem(global::UnityEngine.UI.Toggle toggle)
		{
			if (!toggle.isOn)
			{
				toggle.isOn = true;
			}
			int num = -1;
			global::UnityEngine.Transform transform = toggle.transform;
			global::UnityEngine.Transform parent = transform.parent;
			for (int i = 0; i < parent.childCount; i++)
			{
				if (parent.GetChild(i) == transform)
				{
					num = i - 1;
					break;
				}
			}
			if (num >= 0)
			{
				value = num;
				Hide();
			}
		}
	}
}
