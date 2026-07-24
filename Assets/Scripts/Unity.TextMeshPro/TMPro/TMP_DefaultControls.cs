namespace TMPro
{
	public static class TMP_DefaultControls
	{
		public struct Resources
		{
			public global::UnityEngine.Sprite standard;

			public global::UnityEngine.Sprite background;

			public global::UnityEngine.Sprite inputField;

			public global::UnityEngine.Sprite knob;

			public global::UnityEngine.Sprite checkmark;

			public global::UnityEngine.Sprite dropdown;

			public global::UnityEngine.Sprite mask;
		}

		private const float kWidth = 160f;

		private const float kThickHeight = 30f;

		private const float kThinHeight = 20f;

		private static global::UnityEngine.Vector2 s_TextElementSize = new global::UnityEngine.Vector2(100f, 100f);

		private static global::UnityEngine.Vector2 s_ThickElementSize = new global::UnityEngine.Vector2(160f, 30f);

		private static global::UnityEngine.Vector2 s_ThinElementSize = new global::UnityEngine.Vector2(160f, 20f);

		private static global::UnityEngine.Color s_DefaultSelectableColor = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

		private static global::UnityEngine.Color s_TextColor = new global::UnityEngine.Color(10f / 51f, 10f / 51f, 10f / 51f, 1f);

		private static global::UnityEngine.GameObject CreateUIElementRoot(string name, global::UnityEngine.Vector2 size)
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject(name);
			gameObject.AddComponent<global::UnityEngine.RectTransform>().sizeDelta = size;
			return gameObject;
		}

		private static global::UnityEngine.GameObject CreateUIObject(string name, global::UnityEngine.GameObject parent)
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject(name);
			gameObject.AddComponent<global::UnityEngine.RectTransform>();
			SetParentAndAlign(gameObject, parent);
			return gameObject;
		}

		private static void SetDefaultTextValues(global::TMPro.TMP_Text lbl)
		{
			lbl.color = s_TextColor;
			lbl.fontSize = 14f;
		}

		private static void SetDefaultColorTransitionValues(global::UnityEngine.UI.Selectable slider)
		{
			global::UnityEngine.UI.ColorBlock colors = slider.colors;
			colors.highlightedColor = new global::UnityEngine.Color(0.882f, 0.882f, 0.882f);
			colors.pressedColor = new global::UnityEngine.Color(0.698f, 0.698f, 0.698f);
			colors.disabledColor = new global::UnityEngine.Color(0.521f, 0.521f, 0.521f);
		}

		private static void SetParentAndAlign(global::UnityEngine.GameObject child, global::UnityEngine.GameObject parent)
		{
			if (!(parent == null))
			{
				child.transform.SetParent(parent.transform, worldPositionStays: false);
				SetLayerRecursively(child, parent.layer);
			}
		}

		private static void SetLayerRecursively(global::UnityEngine.GameObject go, int layer)
		{
			go.layer = layer;
			global::UnityEngine.Transform transform = go.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				SetLayerRecursively(transform.GetChild(i).gameObject, layer);
			}
		}

		public static global::UnityEngine.GameObject CreateScrollbar(global::TMPro.TMP_DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Scrollbar", s_ThinElementSize);
			global::UnityEngine.GameObject gameObject2 = CreateUIObject("Sliding Area", gameObject);
			global::UnityEngine.GameObject gameObject3 = CreateUIObject("Handle", gameObject2);
			global::UnityEngine.UI.Image image = AddComponent<global::UnityEngine.UI.Image>(gameObject);
			image.sprite = resources.background;
			image.type = global::UnityEngine.UI.Image.Type.Sliced;
			image.color = s_DefaultSelectableColor;
			global::UnityEngine.UI.Image image2 = AddComponent<global::UnityEngine.UI.Image>(gameObject3);
			image2.sprite = resources.standard;
			image2.type = global::UnityEngine.UI.Image.Type.Sliced;
			image2.color = s_DefaultSelectableColor;
			global::UnityEngine.RectTransform component = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component.sizeDelta = new global::UnityEngine.Vector2(-20f, -20f);
			component.anchorMin = global::UnityEngine.Vector2.zero;
			component.anchorMax = global::UnityEngine.Vector2.one;
			global::UnityEngine.RectTransform component2 = gameObject3.GetComponent<global::UnityEngine.RectTransform>();
			component2.sizeDelta = new global::UnityEngine.Vector2(20f, 20f);
			global::UnityEngine.UI.Scrollbar scrollbar = AddComponent<global::UnityEngine.UI.Scrollbar>(gameObject);
			scrollbar.handleRect = component2;
			scrollbar.targetGraphic = image2;
			SetDefaultColorTransitionValues(scrollbar);
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateButton(global::TMPro.TMP_DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Button", s_ThickElementSize);
			global::UnityEngine.GameObject gameObject2 = new global::UnityEngine.GameObject("Text (TMP)");
			gameObject2.AddComponent<global::UnityEngine.RectTransform>();
			SetParentAndAlign(gameObject2, gameObject);
			global::UnityEngine.UI.Image image = AddComponent<global::UnityEngine.UI.Image>(gameObject);
			image.sprite = resources.standard;
			image.type = global::UnityEngine.UI.Image.Type.Sliced;
			image.color = s_DefaultSelectableColor;
			SetDefaultColorTransitionValues(AddComponent<global::UnityEngine.UI.Button>(gameObject));
			global::TMPro.TextMeshProUGUI textMeshProUGUI = AddComponent<global::TMPro.TextMeshProUGUI>(gameObject2);
			textMeshProUGUI.text = "Button";
			textMeshProUGUI.alignment = global::TMPro.TextAlignmentOptions.Center;
			SetDefaultTextValues(textMeshProUGUI);
			global::UnityEngine.RectTransform component = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component.anchorMin = global::UnityEngine.Vector2.zero;
			component.anchorMax = global::UnityEngine.Vector2.one;
			component.sizeDelta = global::UnityEngine.Vector2.zero;
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateText(global::TMPro.TMP_DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Text (TMP)", s_TextElementSize);
			gameObject.AddComponent<global::TMPro.TextMeshProUGUI>();
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateInputField(global::TMPro.TMP_DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("InputField (TMP)", s_ThickElementSize);
			global::UnityEngine.GameObject gameObject2 = CreateUIObject("Text Area", gameObject);
			global::UnityEngine.GameObject gameObject3 = CreateUIObject("Placeholder", gameObject2);
			global::UnityEngine.GameObject gameObject4 = CreateUIObject("Text", gameObject2);
			global::UnityEngine.UI.Image image = AddComponent<global::UnityEngine.UI.Image>(gameObject);
			image.sprite = resources.inputField;
			image.type = global::UnityEngine.UI.Image.Type.Sliced;
			image.color = s_DefaultSelectableColor;
			global::TMPro.TMP_InputField tMP_InputField = AddComponent<global::TMPro.TMP_InputField>(gameObject);
			SetDefaultColorTransitionValues(tMP_InputField);
			AddComponent<global::UnityEngine.UI.RectMask2D>(gameObject2).padding = new global::UnityEngine.Vector4(-8f, -5f, -8f, -5f);
			global::UnityEngine.RectTransform component = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component.anchorMin = global::UnityEngine.Vector2.zero;
			component.anchorMax = global::UnityEngine.Vector2.one;
			component.sizeDelta = global::UnityEngine.Vector2.zero;
			component.offsetMin = new global::UnityEngine.Vector2(10f, 6f);
			component.offsetMax = new global::UnityEngine.Vector2(-10f, -7f);
			global::TMPro.TextMeshProUGUI textMeshProUGUI = AddComponent<global::TMPro.TextMeshProUGUI>(gameObject4);
			textMeshProUGUI.text = "";
			textMeshProUGUI.textWrappingMode = global::TMPro.TextWrappingModes.NoWrap;
			textMeshProUGUI.extraPadding = true;
			textMeshProUGUI.richText = true;
			SetDefaultTextValues(textMeshProUGUI);
			global::TMPro.TextMeshProUGUI textMeshProUGUI2 = AddComponent<global::TMPro.TextMeshProUGUI>(gameObject3);
			textMeshProUGUI2.text = "Enter text...";
			textMeshProUGUI2.fontSize = 14f;
			textMeshProUGUI2.fontStyle = global::TMPro.FontStyles.Italic;
			textMeshProUGUI2.textWrappingMode = global::TMPro.TextWrappingModes.NoWrap;
			textMeshProUGUI2.extraPadding = true;
			global::UnityEngine.Color color = textMeshProUGUI.color;
			color.a *= 0.5f;
			textMeshProUGUI2.color = color;
			AddComponent<global::UnityEngine.UI.LayoutElement>(textMeshProUGUI2.gameObject).ignoreLayout = true;
			global::UnityEngine.RectTransform component2 = gameObject4.GetComponent<global::UnityEngine.RectTransform>();
			component2.anchorMin = global::UnityEngine.Vector2.zero;
			component2.anchorMax = global::UnityEngine.Vector2.one;
			component2.sizeDelta = global::UnityEngine.Vector2.zero;
			component2.offsetMin = new global::UnityEngine.Vector2(0f, 0f);
			component2.offsetMax = new global::UnityEngine.Vector2(0f, 0f);
			global::UnityEngine.RectTransform component3 = gameObject3.GetComponent<global::UnityEngine.RectTransform>();
			component3.anchorMin = global::UnityEngine.Vector2.zero;
			component3.anchorMax = global::UnityEngine.Vector2.one;
			component3.sizeDelta = global::UnityEngine.Vector2.zero;
			component3.offsetMin = new global::UnityEngine.Vector2(0f, 0f);
			component3.offsetMax = new global::UnityEngine.Vector2(0f, 0f);
			tMP_InputField.textViewport = component;
			tMP_InputField.textComponent = textMeshProUGUI;
			tMP_InputField.placeholder = textMeshProUGUI2;
			tMP_InputField.fontAsset = textMeshProUGUI.font;
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateDropdown(global::TMPro.TMP_DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Dropdown", s_ThickElementSize);
			global::UnityEngine.GameObject gameObject2 = CreateUIObject("Label", gameObject);
			global::UnityEngine.GameObject gameObject3 = CreateUIObject("Arrow", gameObject);
			global::UnityEngine.GameObject gameObject4 = CreateUIObject("Template", gameObject);
			global::UnityEngine.GameObject gameObject5 = CreateUIObject("Viewport", gameObject4);
			global::UnityEngine.GameObject gameObject6 = CreateUIObject("Content", gameObject5);
			global::UnityEngine.GameObject gameObject7 = CreateUIObject("Item", gameObject6);
			global::UnityEngine.GameObject gameObject8 = CreateUIObject("Item Background", gameObject7);
			global::UnityEngine.GameObject gameObject9 = CreateUIObject("Item Checkmark", gameObject7);
			global::UnityEngine.GameObject gameObject10 = CreateUIObject("Item Label", gameObject7);
			global::UnityEngine.GameObject gameObject11 = CreateScrollbar(resources);
			gameObject11.name = "Scrollbar";
			SetParentAndAlign(gameObject11, gameObject4);
			global::UnityEngine.UI.Scrollbar component = gameObject11.GetComponent<global::UnityEngine.UI.Scrollbar>();
			component.SetDirection(global::UnityEngine.UI.Scrollbar.Direction.BottomToTop, includeRectLayouts: true);
			global::UnityEngine.RectTransform component2 = gameObject11.GetComponent<global::UnityEngine.RectTransform>();
			component2.anchorMin = global::UnityEngine.Vector2.right;
			component2.anchorMax = global::UnityEngine.Vector2.one;
			component2.pivot = global::UnityEngine.Vector2.one;
			component2.sizeDelta = new global::UnityEngine.Vector2(component2.sizeDelta.x, 0f);
			global::TMPro.TextMeshProUGUI textMeshProUGUI = AddComponent<global::TMPro.TextMeshProUGUI>(gameObject10);
			SetDefaultTextValues(textMeshProUGUI);
			textMeshProUGUI.alignment = global::TMPro.TextAlignmentOptions.Left;
			global::UnityEngine.UI.Image image = AddComponent<global::UnityEngine.UI.Image>(gameObject8);
			image.color = new global::UnityEngine.Color32(245, 245, 245, byte.MaxValue);
			global::UnityEngine.UI.Image image2 = AddComponent<global::UnityEngine.UI.Image>(gameObject9);
			image2.sprite = resources.checkmark;
			global::UnityEngine.UI.Toggle toggle = AddComponent<global::UnityEngine.UI.Toggle>(gameObject7);
			toggle.targetGraphic = image;
			toggle.graphic = image2;
			toggle.isOn = true;
			global::UnityEngine.UI.Image image3 = AddComponent<global::UnityEngine.UI.Image>(gameObject4);
			image3.sprite = resources.standard;
			image3.type = global::UnityEngine.UI.Image.Type.Sliced;
			global::UnityEngine.UI.ScrollRect scrollRect = AddComponent<global::UnityEngine.UI.ScrollRect>(gameObject4);
			scrollRect.content = (global::UnityEngine.RectTransform)gameObject6.transform;
			scrollRect.viewport = (global::UnityEngine.RectTransform)gameObject5.transform;
			scrollRect.horizontal = false;
			scrollRect.movementType = global::UnityEngine.UI.ScrollRect.MovementType.Clamped;
			scrollRect.verticalScrollbar = component;
			scrollRect.verticalScrollbarVisibility = global::UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			scrollRect.verticalScrollbarSpacing = -3f;
			AddComponent<global::UnityEngine.UI.Mask>(gameObject5).showMaskGraphic = false;
			global::UnityEngine.UI.Image image4 = AddComponent<global::UnityEngine.UI.Image>(gameObject5);
			image4.sprite = resources.mask;
			image4.type = global::UnityEngine.UI.Image.Type.Sliced;
			global::TMPro.TextMeshProUGUI textMeshProUGUI2 = AddComponent<global::TMPro.TextMeshProUGUI>(gameObject2);
			SetDefaultTextValues(textMeshProUGUI2);
			textMeshProUGUI2.alignment = global::TMPro.TextAlignmentOptions.Left;
			AddComponent<global::UnityEngine.UI.Image>(gameObject3).sprite = resources.dropdown;
			global::UnityEngine.UI.Image image5 = AddComponent<global::UnityEngine.UI.Image>(gameObject);
			image5.sprite = resources.standard;
			image5.color = s_DefaultSelectableColor;
			image5.type = global::UnityEngine.UI.Image.Type.Sliced;
			global::TMPro.TMP_Dropdown tMP_Dropdown = AddComponent<global::TMPro.TMP_Dropdown>(gameObject);
			tMP_Dropdown.targetGraphic = image5;
			SetDefaultColorTransitionValues(tMP_Dropdown);
			tMP_Dropdown.template = gameObject4.GetComponent<global::UnityEngine.RectTransform>();
			tMP_Dropdown.captionText = textMeshProUGUI2;
			tMP_Dropdown.itemText = textMeshProUGUI;
			textMeshProUGUI.text = "Option A";
			tMP_Dropdown.options.Add(new global::TMPro.TMP_Dropdown.OptionData
			{
				text = "Option A"
			});
			tMP_Dropdown.options.Add(new global::TMPro.TMP_Dropdown.OptionData
			{
				text = "Option B"
			});
			tMP_Dropdown.options.Add(new global::TMPro.TMP_Dropdown.OptionData
			{
				text = "Option C"
			});
			tMP_Dropdown.RefreshShownValue();
			global::UnityEngine.RectTransform component3 = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component3.anchorMin = global::UnityEngine.Vector2.zero;
			component3.anchorMax = global::UnityEngine.Vector2.one;
			component3.offsetMin = new global::UnityEngine.Vector2(10f, 6f);
			component3.offsetMax = new global::UnityEngine.Vector2(-25f, -7f);
			global::UnityEngine.RectTransform component4 = gameObject3.GetComponent<global::UnityEngine.RectTransform>();
			component4.anchorMin = new global::UnityEngine.Vector2(1f, 0.5f);
			component4.anchorMax = new global::UnityEngine.Vector2(1f, 0.5f);
			component4.sizeDelta = new global::UnityEngine.Vector2(20f, 20f);
			component4.anchoredPosition = new global::UnityEngine.Vector2(-15f, 0f);
			global::UnityEngine.RectTransform component5 = gameObject4.GetComponent<global::UnityEngine.RectTransform>();
			component5.anchorMin = new global::UnityEngine.Vector2(0f, 0f);
			component5.anchorMax = new global::UnityEngine.Vector2(1f, 0f);
			component5.pivot = new global::UnityEngine.Vector2(0.5f, 1f);
			component5.anchoredPosition = new global::UnityEngine.Vector2(0f, 2f);
			component5.sizeDelta = new global::UnityEngine.Vector2(0f, 150f);
			global::UnityEngine.RectTransform component6 = gameObject5.GetComponent<global::UnityEngine.RectTransform>();
			component6.anchorMin = new global::UnityEngine.Vector2(0f, 0f);
			component6.anchorMax = new global::UnityEngine.Vector2(1f, 1f);
			component6.sizeDelta = new global::UnityEngine.Vector2(-18f, 0f);
			component6.pivot = new global::UnityEngine.Vector2(0f, 1f);
			global::UnityEngine.RectTransform component7 = gameObject6.GetComponent<global::UnityEngine.RectTransform>();
			component7.anchorMin = new global::UnityEngine.Vector2(0f, 1f);
			component7.anchorMax = new global::UnityEngine.Vector2(1f, 1f);
			component7.pivot = new global::UnityEngine.Vector2(0.5f, 1f);
			component7.anchoredPosition = new global::UnityEngine.Vector2(0f, 0f);
			component7.sizeDelta = new global::UnityEngine.Vector2(0f, 28f);
			global::UnityEngine.RectTransform component8 = gameObject7.GetComponent<global::UnityEngine.RectTransform>();
			component8.anchorMin = new global::UnityEngine.Vector2(0f, 0.5f);
			component8.anchorMax = new global::UnityEngine.Vector2(1f, 0.5f);
			component8.sizeDelta = new global::UnityEngine.Vector2(0f, 20f);
			global::UnityEngine.RectTransform component9 = gameObject8.GetComponent<global::UnityEngine.RectTransform>();
			component9.anchorMin = global::UnityEngine.Vector2.zero;
			component9.anchorMax = global::UnityEngine.Vector2.one;
			component9.sizeDelta = global::UnityEngine.Vector2.zero;
			global::UnityEngine.RectTransform component10 = gameObject9.GetComponent<global::UnityEngine.RectTransform>();
			component10.anchorMin = new global::UnityEngine.Vector2(0f, 0.5f);
			component10.anchorMax = new global::UnityEngine.Vector2(0f, 0.5f);
			component10.sizeDelta = new global::UnityEngine.Vector2(20f, 20f);
			component10.anchoredPosition = new global::UnityEngine.Vector2(10f, 0f);
			global::UnityEngine.RectTransform component11 = gameObject10.GetComponent<global::UnityEngine.RectTransform>();
			component11.anchorMin = global::UnityEngine.Vector2.zero;
			component11.anchorMax = global::UnityEngine.Vector2.one;
			component11.offsetMin = new global::UnityEngine.Vector2(20f, 1f);
			component11.offsetMax = new global::UnityEngine.Vector2(-10f, -2f);
			gameObject4.SetActive(value: false);
			return gameObject;
		}

		private static T AddComponent<T>(global::UnityEngine.GameObject go) where T : global::UnityEngine.Component
		{
			return go.AddComponent<T>();
		}
	}
}
