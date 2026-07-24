namespace UnityEngine.UI
{
	public static class DefaultControls
	{
		public interface IFactoryControls
		{
			global::UnityEngine.GameObject CreateGameObject(string name, params global::System.Type[] components);
		}

		private class DefaultRuntimeFactory : global::UnityEngine.UI.DefaultControls.IFactoryControls
		{
			public static global::UnityEngine.UI.DefaultControls.IFactoryControls Default = new global::UnityEngine.UI.DefaultControls.DefaultRuntimeFactory();

			public global::UnityEngine.GameObject CreateGameObject(string name, params global::System.Type[] components)
			{
				return new global::UnityEngine.GameObject(name, components);
			}
		}

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

		private static global::UnityEngine.UI.DefaultControls.IFactoryControls m_CurrentFactory = global::UnityEngine.UI.DefaultControls.DefaultRuntimeFactory.Default;

		private const float kWidth = 160f;

		private const float kThickHeight = 30f;

		private const float kThinHeight = 20f;

		private static global::UnityEngine.Vector2 s_ThickElementSize = new global::UnityEngine.Vector2(160f, 30f);

		private static global::UnityEngine.Vector2 s_ThinElementSize = new global::UnityEngine.Vector2(160f, 20f);

		private static global::UnityEngine.Vector2 s_ImageElementSize = new global::UnityEngine.Vector2(100f, 100f);

		private static global::UnityEngine.Color s_DefaultSelectableColor = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

		private static global::UnityEngine.Color s_PanelColor = new global::UnityEngine.Color(1f, 1f, 1f, 0.392f);

		private static global::UnityEngine.Color s_TextColor = new global::UnityEngine.Color(10f / 51f, 10f / 51f, 10f / 51f, 1f);

		public static global::UnityEngine.UI.DefaultControls.IFactoryControls factory => m_CurrentFactory;

		private static global::UnityEngine.GameObject CreateUIElementRoot(string name, global::UnityEngine.Vector2 size, params global::System.Type[] components)
		{
			global::UnityEngine.GameObject gameObject = factory.CreateGameObject(name, components);
			gameObject.GetComponent<global::UnityEngine.RectTransform>().sizeDelta = size;
			return gameObject;
		}

		private static global::UnityEngine.GameObject CreateUIObject(string name, global::UnityEngine.GameObject parent, params global::System.Type[] components)
		{
			global::UnityEngine.GameObject gameObject = factory.CreateGameObject(name, components);
			SetParentAndAlign(gameObject, parent);
			return gameObject;
		}

		private static void SetDefaultTextValues(global::UnityEngine.UI.Text lbl)
		{
			lbl.color = s_TextColor;
			if (lbl.font == null)
			{
				lbl.AssignDefaultFont();
			}
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

		public static global::UnityEngine.GameObject CreatePanel(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Panel", s_ThickElementSize, typeof(global::UnityEngine.UI.Image));
			global::UnityEngine.RectTransform component = gameObject.GetComponent<global::UnityEngine.RectTransform>();
			component.anchorMin = global::UnityEngine.Vector2.zero;
			component.anchorMax = global::UnityEngine.Vector2.one;
			component.anchoredPosition = global::UnityEngine.Vector2.zero;
			component.sizeDelta = global::UnityEngine.Vector2.zero;
			global::UnityEngine.UI.Image component2 = gameObject.GetComponent<global::UnityEngine.UI.Image>();
			component2.sprite = resources.background;
			component2.type = global::UnityEngine.UI.Image.Type.Sliced;
			component2.color = s_PanelColor;
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateButton(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Button (Legacy)", s_ThickElementSize, typeof(global::UnityEngine.UI.Image), typeof(global::UnityEngine.UI.Button));
			global::UnityEngine.GameObject gameObject2 = CreateUIObject("Text (Legacy)", gameObject, typeof(global::UnityEngine.UI.Text));
			global::UnityEngine.UI.Image component = gameObject.GetComponent<global::UnityEngine.UI.Image>();
			component.sprite = resources.standard;
			component.type = global::UnityEngine.UI.Image.Type.Sliced;
			component.color = s_DefaultSelectableColor;
			SetDefaultColorTransitionValues(gameObject.GetComponent<global::UnityEngine.UI.Button>());
			global::UnityEngine.UI.Text component2 = gameObject2.GetComponent<global::UnityEngine.UI.Text>();
			component2.text = "Button";
			component2.alignment = global::UnityEngine.TextAnchor.MiddleCenter;
			SetDefaultTextValues(component2);
			global::UnityEngine.RectTransform component3 = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component3.anchorMin = global::UnityEngine.Vector2.zero;
			component3.anchorMax = global::UnityEngine.Vector2.one;
			component3.sizeDelta = global::UnityEngine.Vector2.zero;
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateText(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Text (Legacy)", s_ThickElementSize, typeof(global::UnityEngine.UI.Text));
			global::UnityEngine.UI.Text component = gameObject.GetComponent<global::UnityEngine.UI.Text>();
			component.text = "New Text";
			SetDefaultTextValues(component);
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateImage(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			return CreateUIElementRoot("Image", s_ImageElementSize, typeof(global::UnityEngine.UI.Image));
		}

		public static global::UnityEngine.GameObject CreateRawImage(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			return CreateUIElementRoot("RawImage", s_ImageElementSize, typeof(global::UnityEngine.UI.RawImage));
		}

		public static global::UnityEngine.GameObject CreateSlider(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Slider", s_ThinElementSize, typeof(global::UnityEngine.UI.Slider));
			global::UnityEngine.GameObject gameObject2 = CreateUIObject("Background", gameObject, typeof(global::UnityEngine.UI.Image));
			global::UnityEngine.GameObject gameObject3 = CreateUIObject("Fill Area", gameObject, typeof(global::UnityEngine.RectTransform));
			global::UnityEngine.GameObject gameObject4 = CreateUIObject("Fill", gameObject3, typeof(global::UnityEngine.UI.Image));
			global::UnityEngine.GameObject gameObject5 = CreateUIObject("Handle Slide Area", gameObject, typeof(global::UnityEngine.RectTransform));
			global::UnityEngine.GameObject gameObject6 = CreateUIObject("Handle", gameObject5, typeof(global::UnityEngine.UI.Image));
			global::UnityEngine.UI.Image component = gameObject2.GetComponent<global::UnityEngine.UI.Image>();
			component.sprite = resources.background;
			component.type = global::UnityEngine.UI.Image.Type.Sliced;
			component.color = s_DefaultSelectableColor;
			global::UnityEngine.RectTransform component2 = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component2.anchorMin = new global::UnityEngine.Vector2(0f, 0.25f);
			component2.anchorMax = new global::UnityEngine.Vector2(1f, 0.75f);
			component2.sizeDelta = new global::UnityEngine.Vector2(0f, 0f);
			global::UnityEngine.RectTransform component3 = gameObject3.GetComponent<global::UnityEngine.RectTransform>();
			component3.anchorMin = new global::UnityEngine.Vector2(0f, 0.25f);
			component3.anchorMax = new global::UnityEngine.Vector2(1f, 0.75f);
			component3.anchoredPosition = new global::UnityEngine.Vector2(-5f, 0f);
			component3.sizeDelta = new global::UnityEngine.Vector2(-20f, 0f);
			global::UnityEngine.UI.Image component4 = gameObject4.GetComponent<global::UnityEngine.UI.Image>();
			component4.sprite = resources.standard;
			component4.type = global::UnityEngine.UI.Image.Type.Sliced;
			component4.color = s_DefaultSelectableColor;
			gameObject4.GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(10f, 0f);
			global::UnityEngine.RectTransform component5 = gameObject5.GetComponent<global::UnityEngine.RectTransform>();
			component5.sizeDelta = new global::UnityEngine.Vector2(-20f, 0f);
			component5.anchorMin = new global::UnityEngine.Vector2(0f, 0f);
			component5.anchorMax = new global::UnityEngine.Vector2(1f, 1f);
			global::UnityEngine.UI.Image component6 = gameObject6.GetComponent<global::UnityEngine.UI.Image>();
			component6.sprite = resources.knob;
			component6.color = s_DefaultSelectableColor;
			gameObject6.GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(20f, 0f);
			global::UnityEngine.UI.Slider component7 = gameObject.GetComponent<global::UnityEngine.UI.Slider>();
			component7.fillRect = gameObject4.GetComponent<global::UnityEngine.RectTransform>();
			component7.handleRect = gameObject6.GetComponent<global::UnityEngine.RectTransform>();
			component7.targetGraphic = component6;
			component7.direction = global::UnityEngine.UI.Slider.Direction.LeftToRight;
			SetDefaultColorTransitionValues(component7);
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateScrollbar(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Scrollbar", s_ThinElementSize, typeof(global::UnityEngine.UI.Image), typeof(global::UnityEngine.UI.Scrollbar));
			global::UnityEngine.GameObject gameObject2 = CreateUIObject("Sliding Area", gameObject, typeof(global::UnityEngine.RectTransform));
			global::UnityEngine.GameObject gameObject3 = CreateUIObject("Handle", gameObject2, typeof(global::UnityEngine.UI.Image));
			global::UnityEngine.UI.Image component = gameObject.GetComponent<global::UnityEngine.UI.Image>();
			component.sprite = resources.background;
			component.type = global::UnityEngine.UI.Image.Type.Sliced;
			component.color = s_DefaultSelectableColor;
			global::UnityEngine.UI.Image component2 = gameObject3.GetComponent<global::UnityEngine.UI.Image>();
			component2.sprite = resources.standard;
			component2.type = global::UnityEngine.UI.Image.Type.Sliced;
			component2.color = s_DefaultSelectableColor;
			global::UnityEngine.RectTransform component3 = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component3.sizeDelta = new global::UnityEngine.Vector2(-20f, -20f);
			component3.anchorMin = global::UnityEngine.Vector2.zero;
			component3.anchorMax = global::UnityEngine.Vector2.one;
			global::UnityEngine.RectTransform component4 = gameObject3.GetComponent<global::UnityEngine.RectTransform>();
			component4.sizeDelta = new global::UnityEngine.Vector2(20f, 20f);
			global::UnityEngine.UI.Scrollbar component5 = gameObject.GetComponent<global::UnityEngine.UI.Scrollbar>();
			component5.handleRect = component4;
			component5.targetGraphic = component2;
			SetDefaultColorTransitionValues(component5);
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateToggle(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Toggle", s_ThinElementSize, typeof(global::UnityEngine.UI.Toggle));
			global::UnityEngine.GameObject gameObject2 = CreateUIObject("Background", gameObject, typeof(global::UnityEngine.UI.Image));
			global::UnityEngine.GameObject gameObject3 = CreateUIObject("Checkmark", gameObject2, typeof(global::UnityEngine.UI.Image));
			global::UnityEngine.GameObject gameObject4 = CreateUIObject("Label", gameObject, typeof(global::UnityEngine.UI.Text));
			global::UnityEngine.UI.Toggle component = gameObject.GetComponent<global::UnityEngine.UI.Toggle>();
			component.isOn = true;
			global::UnityEngine.UI.Image component2 = gameObject2.GetComponent<global::UnityEngine.UI.Image>();
			component2.sprite = resources.standard;
			component2.type = global::UnityEngine.UI.Image.Type.Sliced;
			component2.color = s_DefaultSelectableColor;
			global::UnityEngine.UI.Image component3 = gameObject3.GetComponent<global::UnityEngine.UI.Image>();
			component3.sprite = resources.checkmark;
			global::UnityEngine.UI.Text component4 = gameObject4.GetComponent<global::UnityEngine.UI.Text>();
			component4.text = "Toggle";
			SetDefaultTextValues(component4);
			component.graphic = component3;
			component.targetGraphic = component2;
			SetDefaultColorTransitionValues(component);
			global::UnityEngine.RectTransform component5 = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component5.anchorMin = new global::UnityEngine.Vector2(0f, 1f);
			component5.anchorMax = new global::UnityEngine.Vector2(0f, 1f);
			component5.anchoredPosition = new global::UnityEngine.Vector2(10f, -10f);
			component5.sizeDelta = new global::UnityEngine.Vector2(20f, 20f);
			global::UnityEngine.RectTransform component6 = gameObject3.GetComponent<global::UnityEngine.RectTransform>();
			component6.anchorMin = new global::UnityEngine.Vector2(0.5f, 0.5f);
			component6.anchorMax = new global::UnityEngine.Vector2(0.5f, 0.5f);
			component6.anchoredPosition = global::UnityEngine.Vector2.zero;
			component6.sizeDelta = new global::UnityEngine.Vector2(20f, 20f);
			global::UnityEngine.RectTransform component7 = gameObject4.GetComponent<global::UnityEngine.RectTransform>();
			component7.anchorMin = new global::UnityEngine.Vector2(0f, 0f);
			component7.anchorMax = new global::UnityEngine.Vector2(1f, 1f);
			component7.offsetMin = new global::UnityEngine.Vector2(23f, 1f);
			component7.offsetMax = new global::UnityEngine.Vector2(-5f, -2f);
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateInputField(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("InputField (Legacy)", s_ThickElementSize, typeof(global::UnityEngine.UI.Image), typeof(global::UnityEngine.UI.InputField));
			global::UnityEngine.GameObject gameObject2 = CreateUIObject("Placeholder", gameObject, typeof(global::UnityEngine.UI.Text));
			global::UnityEngine.GameObject gameObject3 = CreateUIObject("Text (Legacy)", gameObject, typeof(global::UnityEngine.UI.Text));
			global::UnityEngine.UI.Image component = gameObject.GetComponent<global::UnityEngine.UI.Image>();
			component.sprite = resources.inputField;
			component.type = global::UnityEngine.UI.Image.Type.Sliced;
			component.color = s_DefaultSelectableColor;
			global::UnityEngine.UI.InputField component2 = gameObject.GetComponent<global::UnityEngine.UI.InputField>();
			SetDefaultColorTransitionValues(component2);
			global::UnityEngine.UI.Text component3 = gameObject3.GetComponent<global::UnityEngine.UI.Text>();
			component3.text = "";
			component3.supportRichText = false;
			SetDefaultTextValues(component3);
			global::UnityEngine.UI.Text component4 = gameObject2.GetComponent<global::UnityEngine.UI.Text>();
			component4.text = "Enter text...";
			component4.fontStyle = global::UnityEngine.FontStyle.Italic;
			global::UnityEngine.Color color = component3.color;
			color.a *= 0.5f;
			component4.color = color;
			global::UnityEngine.RectTransform component5 = gameObject3.GetComponent<global::UnityEngine.RectTransform>();
			component5.anchorMin = global::UnityEngine.Vector2.zero;
			component5.anchorMax = global::UnityEngine.Vector2.one;
			component5.sizeDelta = global::UnityEngine.Vector2.zero;
			component5.offsetMin = new global::UnityEngine.Vector2(10f, 6f);
			component5.offsetMax = new global::UnityEngine.Vector2(-10f, -7f);
			global::UnityEngine.RectTransform component6 = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component6.anchorMin = global::UnityEngine.Vector2.zero;
			component6.anchorMax = global::UnityEngine.Vector2.one;
			component6.sizeDelta = global::UnityEngine.Vector2.zero;
			component6.offsetMin = new global::UnityEngine.Vector2(10f, 6f);
			component6.offsetMax = new global::UnityEngine.Vector2(-10f, -7f);
			component2.textComponent = component3;
			component2.placeholder = component4;
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateDropdown(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Dropdown (Legacy)", s_ThickElementSize, typeof(global::UnityEngine.UI.Image), typeof(global::UnityEngine.UI.Dropdown));
			global::UnityEngine.GameObject gameObject2 = CreateUIObject("Label", gameObject, typeof(global::UnityEngine.UI.Text));
			global::UnityEngine.GameObject gameObject3 = CreateUIObject("Arrow", gameObject, typeof(global::UnityEngine.UI.Image));
			global::UnityEngine.GameObject gameObject4 = CreateUIObject("Template", gameObject, typeof(global::UnityEngine.UI.Image), typeof(global::UnityEngine.UI.ScrollRect));
			global::UnityEngine.GameObject gameObject5 = CreateUIObject("Viewport", gameObject4, typeof(global::UnityEngine.UI.Image), typeof(global::UnityEngine.UI.Mask));
			global::UnityEngine.GameObject gameObject6 = CreateUIObject("Content", gameObject5, typeof(global::UnityEngine.RectTransform));
			global::UnityEngine.GameObject gameObject7 = CreateUIObject("Item", gameObject6, typeof(global::UnityEngine.UI.Toggle));
			global::UnityEngine.GameObject gameObject8 = CreateUIObject("Item Background", gameObject7, typeof(global::UnityEngine.UI.Image));
			global::UnityEngine.GameObject gameObject9 = CreateUIObject("Item Checkmark", gameObject7, typeof(global::UnityEngine.UI.Image));
			global::UnityEngine.GameObject gameObject10 = CreateUIObject("Item Label", gameObject7, typeof(global::UnityEngine.UI.Text));
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
			global::UnityEngine.UI.Text component3 = gameObject10.GetComponent<global::UnityEngine.UI.Text>();
			SetDefaultTextValues(component3);
			component3.alignment = global::UnityEngine.TextAnchor.MiddleLeft;
			global::UnityEngine.UI.Image component4 = gameObject8.GetComponent<global::UnityEngine.UI.Image>();
			component4.color = new global::UnityEngine.Color32(245, 245, 245, byte.MaxValue);
			global::UnityEngine.UI.Image component5 = gameObject9.GetComponent<global::UnityEngine.UI.Image>();
			component5.sprite = resources.checkmark;
			global::UnityEngine.UI.Toggle component6 = gameObject7.GetComponent<global::UnityEngine.UI.Toggle>();
			component6.targetGraphic = component4;
			component6.graphic = component5;
			component6.isOn = true;
			global::UnityEngine.UI.Image component7 = gameObject4.GetComponent<global::UnityEngine.UI.Image>();
			component7.sprite = resources.standard;
			component7.type = global::UnityEngine.UI.Image.Type.Sliced;
			global::UnityEngine.UI.ScrollRect component8 = gameObject4.GetComponent<global::UnityEngine.UI.ScrollRect>();
			component8.content = gameObject6.GetComponent<global::UnityEngine.RectTransform>();
			component8.viewport = gameObject5.GetComponent<global::UnityEngine.RectTransform>();
			component8.horizontal = false;
			component8.movementType = global::UnityEngine.UI.ScrollRect.MovementType.Clamped;
			component8.verticalScrollbar = component;
			component8.verticalScrollbarVisibility = global::UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			component8.verticalScrollbarSpacing = -3f;
			gameObject5.GetComponent<global::UnityEngine.UI.Mask>().showMaskGraphic = false;
			global::UnityEngine.UI.Image component9 = gameObject5.GetComponent<global::UnityEngine.UI.Image>();
			component9.sprite = resources.mask;
			component9.type = global::UnityEngine.UI.Image.Type.Sliced;
			global::UnityEngine.UI.Text component10 = gameObject2.GetComponent<global::UnityEngine.UI.Text>();
			SetDefaultTextValues(component10);
			component10.alignment = global::UnityEngine.TextAnchor.MiddleLeft;
			gameObject3.GetComponent<global::UnityEngine.UI.Image>().sprite = resources.dropdown;
			global::UnityEngine.UI.Image component11 = gameObject.GetComponent<global::UnityEngine.UI.Image>();
			component11.sprite = resources.standard;
			component11.color = s_DefaultSelectableColor;
			component11.type = global::UnityEngine.UI.Image.Type.Sliced;
			global::UnityEngine.UI.Dropdown component12 = gameObject.GetComponent<global::UnityEngine.UI.Dropdown>();
			component12.targetGraphic = component11;
			SetDefaultColorTransitionValues(component12);
			component12.template = gameObject4.GetComponent<global::UnityEngine.RectTransform>();
			component12.captionText = component10;
			component12.itemText = component3;
			component3.text = "Option A";
			component12.options.Add(new global::UnityEngine.UI.Dropdown.OptionData
			{
				text = "Option A"
			});
			component12.options.Add(new global::UnityEngine.UI.Dropdown.OptionData
			{
				text = "Option B"
			});
			component12.options.Add(new global::UnityEngine.UI.Dropdown.OptionData
			{
				text = "Option C"
			});
			component12.RefreshShownValue();
			global::UnityEngine.RectTransform component13 = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component13.anchorMin = global::UnityEngine.Vector2.zero;
			component13.anchorMax = global::UnityEngine.Vector2.one;
			component13.offsetMin = new global::UnityEngine.Vector2(10f, 6f);
			component13.offsetMax = new global::UnityEngine.Vector2(-25f, -7f);
			global::UnityEngine.RectTransform component14 = gameObject3.GetComponent<global::UnityEngine.RectTransform>();
			component14.anchorMin = new global::UnityEngine.Vector2(1f, 0.5f);
			component14.anchorMax = new global::UnityEngine.Vector2(1f, 0.5f);
			component14.sizeDelta = new global::UnityEngine.Vector2(20f, 20f);
			component14.anchoredPosition = new global::UnityEngine.Vector2(-15f, 0f);
			global::UnityEngine.RectTransform component15 = gameObject4.GetComponent<global::UnityEngine.RectTransform>();
			component15.anchorMin = new global::UnityEngine.Vector2(0f, 0f);
			component15.anchorMax = new global::UnityEngine.Vector2(1f, 0f);
			component15.pivot = new global::UnityEngine.Vector2(0.5f, 1f);
			component15.anchoredPosition = new global::UnityEngine.Vector2(0f, 2f);
			component15.sizeDelta = new global::UnityEngine.Vector2(0f, 150f);
			global::UnityEngine.RectTransform component16 = gameObject5.GetComponent<global::UnityEngine.RectTransform>();
			component16.anchorMin = new global::UnityEngine.Vector2(0f, 0f);
			component16.anchorMax = new global::UnityEngine.Vector2(1f, 1f);
			component16.sizeDelta = new global::UnityEngine.Vector2(-18f, 0f);
			component16.pivot = new global::UnityEngine.Vector2(0f, 1f);
			global::UnityEngine.RectTransform component17 = gameObject6.GetComponent<global::UnityEngine.RectTransform>();
			component17.anchorMin = new global::UnityEngine.Vector2(0f, 1f);
			component17.anchorMax = new global::UnityEngine.Vector2(1f, 1f);
			component17.pivot = new global::UnityEngine.Vector2(0.5f, 1f);
			component17.anchoredPosition = new global::UnityEngine.Vector2(0f, 0f);
			component17.sizeDelta = new global::UnityEngine.Vector2(0f, 28f);
			global::UnityEngine.RectTransform component18 = gameObject7.GetComponent<global::UnityEngine.RectTransform>();
			component18.anchorMin = new global::UnityEngine.Vector2(0f, 0.5f);
			component18.anchorMax = new global::UnityEngine.Vector2(1f, 0.5f);
			component18.sizeDelta = new global::UnityEngine.Vector2(0f, 20f);
			global::UnityEngine.RectTransform component19 = gameObject8.GetComponent<global::UnityEngine.RectTransform>();
			component19.anchorMin = global::UnityEngine.Vector2.zero;
			component19.anchorMax = global::UnityEngine.Vector2.one;
			component19.sizeDelta = global::UnityEngine.Vector2.zero;
			global::UnityEngine.RectTransform component20 = gameObject9.GetComponent<global::UnityEngine.RectTransform>();
			component20.anchorMin = new global::UnityEngine.Vector2(0f, 0.5f);
			component20.anchorMax = new global::UnityEngine.Vector2(0f, 0.5f);
			component20.sizeDelta = new global::UnityEngine.Vector2(20f, 20f);
			component20.anchoredPosition = new global::UnityEngine.Vector2(10f, 0f);
			global::UnityEngine.RectTransform component21 = gameObject10.GetComponent<global::UnityEngine.RectTransform>();
			component21.anchorMin = global::UnityEngine.Vector2.zero;
			component21.anchorMax = global::UnityEngine.Vector2.one;
			component21.offsetMin = new global::UnityEngine.Vector2(20f, 1f);
			component21.offsetMax = new global::UnityEngine.Vector2(-10f, -2f);
			gameObject4.SetActive(value: false);
			return gameObject;
		}

		public static global::UnityEngine.GameObject CreateScrollView(global::UnityEngine.UI.DefaultControls.Resources resources)
		{
			global::UnityEngine.GameObject gameObject = CreateUIElementRoot("Scroll View", new global::UnityEngine.Vector2(200f, 200f), typeof(global::UnityEngine.UI.Image), typeof(global::UnityEngine.UI.ScrollRect));
			global::UnityEngine.GameObject gameObject2 = CreateUIObject("Viewport", gameObject, typeof(global::UnityEngine.UI.Image), typeof(global::UnityEngine.UI.Mask));
			global::UnityEngine.GameObject gameObject3 = CreateUIObject("Content", gameObject2, typeof(global::UnityEngine.RectTransform));
			global::UnityEngine.GameObject gameObject4 = CreateScrollbar(resources);
			gameObject4.name = "Scrollbar Horizontal";
			SetParentAndAlign(gameObject4, gameObject);
			global::UnityEngine.RectTransform component = gameObject4.GetComponent<global::UnityEngine.RectTransform>();
			component.anchorMin = global::UnityEngine.Vector2.zero;
			component.anchorMax = global::UnityEngine.Vector2.right;
			component.pivot = global::UnityEngine.Vector2.zero;
			component.sizeDelta = new global::UnityEngine.Vector2(0f, component.sizeDelta.y);
			global::UnityEngine.GameObject gameObject5 = CreateScrollbar(resources);
			gameObject5.name = "Scrollbar Vertical";
			SetParentAndAlign(gameObject5, gameObject);
			gameObject5.GetComponent<global::UnityEngine.UI.Scrollbar>().SetDirection(global::UnityEngine.UI.Scrollbar.Direction.BottomToTop, includeRectLayouts: true);
			global::UnityEngine.RectTransform component2 = gameObject5.GetComponent<global::UnityEngine.RectTransform>();
			component2.anchorMin = global::UnityEngine.Vector2.right;
			component2.anchorMax = global::UnityEngine.Vector2.one;
			component2.pivot = global::UnityEngine.Vector2.one;
			component2.sizeDelta = new global::UnityEngine.Vector2(component2.sizeDelta.x, 0f);
			global::UnityEngine.RectTransform component3 = gameObject2.GetComponent<global::UnityEngine.RectTransform>();
			component3.anchorMin = global::UnityEngine.Vector2.zero;
			component3.anchorMax = global::UnityEngine.Vector2.one;
			component3.sizeDelta = global::UnityEngine.Vector2.zero;
			component3.pivot = global::UnityEngine.Vector2.up;
			global::UnityEngine.RectTransform component4 = gameObject3.GetComponent<global::UnityEngine.RectTransform>();
			component4.anchorMin = global::UnityEngine.Vector2.up;
			component4.anchorMax = global::UnityEngine.Vector2.one;
			component4.sizeDelta = new global::UnityEngine.Vector2(0f, 300f);
			component4.pivot = global::UnityEngine.Vector2.up;
			global::UnityEngine.UI.ScrollRect component5 = gameObject.GetComponent<global::UnityEngine.UI.ScrollRect>();
			component5.content = component4;
			component5.viewport = component3;
			component5.horizontalScrollbar = gameObject4.GetComponent<global::UnityEngine.UI.Scrollbar>();
			component5.verticalScrollbar = gameObject5.GetComponent<global::UnityEngine.UI.Scrollbar>();
			component5.horizontalScrollbarVisibility = global::UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			component5.verticalScrollbarVisibility = global::UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			component5.horizontalScrollbarSpacing = -3f;
			component5.verticalScrollbarSpacing = -3f;
			global::UnityEngine.UI.Image component6 = gameObject.GetComponent<global::UnityEngine.UI.Image>();
			component6.sprite = resources.background;
			component6.type = global::UnityEngine.UI.Image.Type.Sliced;
			component6.color = s_PanelColor;
			gameObject2.GetComponent<global::UnityEngine.UI.Mask>().showMaskGraphic = false;
			global::UnityEngine.UI.Image component7 = gameObject2.GetComponent<global::UnityEngine.UI.Image>();
			component7.sprite = resources.mask;
			component7.type = global::UnityEngine.UI.Image.Type.Sliced;
			return gameObject;
		}
	}
}
