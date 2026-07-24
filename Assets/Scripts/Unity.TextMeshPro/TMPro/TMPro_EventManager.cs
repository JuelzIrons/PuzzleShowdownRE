namespace TMPro
{
	public static class TMPro_EventManager
	{
		public static readonly global::TMPro.FastAction<object, global::TMPro.Compute_DT_EventArgs> COMPUTE_DT_EVENT = new global::TMPro.FastAction<object, global::TMPro.Compute_DT_EventArgs>();

		public static readonly global::TMPro.FastAction<bool, global::UnityEngine.Material> MATERIAL_PROPERTY_EVENT = new global::TMPro.FastAction<bool, global::UnityEngine.Material>();

		public static readonly global::TMPro.FastAction<bool, global::UnityEngine.Object> FONT_PROPERTY_EVENT = new global::TMPro.FastAction<bool, global::UnityEngine.Object>();

		public static readonly global::TMPro.FastAction<bool, global::UnityEngine.Object> SPRITE_ASSET_PROPERTY_EVENT = new global::TMPro.FastAction<bool, global::UnityEngine.Object>();

		public static readonly global::TMPro.FastAction<bool, global::UnityEngine.Object> TEXTMESHPRO_PROPERTY_EVENT = new global::TMPro.FastAction<bool, global::UnityEngine.Object>();

		public static readonly global::TMPro.FastAction<global::UnityEngine.GameObject, global::UnityEngine.Material, global::UnityEngine.Material> DRAG_AND_DROP_MATERIAL_EVENT = new global::TMPro.FastAction<global::UnityEngine.GameObject, global::UnityEngine.Material, global::UnityEngine.Material>();

		public static readonly global::TMPro.FastAction<bool> TEXT_STYLE_PROPERTY_EVENT = new global::TMPro.FastAction<bool>();

		public static readonly global::TMPro.FastAction<global::UnityEngine.Object> COLOR_GRADIENT_PROPERTY_EVENT = new global::TMPro.FastAction<global::UnityEngine.Object>();

		public static readonly global::TMPro.FastAction TMP_SETTINGS_PROPERTY_EVENT = new global::TMPro.FastAction();

		public static readonly global::TMPro.FastAction RESOURCE_LOAD_EVENT = new global::TMPro.FastAction();

		public static readonly global::TMPro.FastAction<bool, global::UnityEngine.Object> TEXTMESHPRO_UGUI_PROPERTY_EVENT = new global::TMPro.FastAction<bool, global::UnityEngine.Object>();

		public static readonly global::TMPro.FastAction<global::UnityEngine.Object> TEXT_CHANGED_EVENT = new global::TMPro.FastAction<global::UnityEngine.Object>();

		public static void ON_MATERIAL_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Material mat)
		{
			MATERIAL_PROPERTY_EVENT.Call(isChanged, mat);
		}

		public static void ON_FONT_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			FONT_PROPERTY_EVENT.Call(isChanged, obj);
		}

		public static void ON_SPRITE_ASSET_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			SPRITE_ASSET_PROPERTY_EVENT.Call(isChanged, obj);
		}

		public static void ON_TEXTMESHPRO_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TEXTMESHPRO_PROPERTY_EVENT.Call(isChanged, obj);
		}

		public static void ON_DRAG_AND_DROP_MATERIAL_CHANGED(global::UnityEngine.GameObject sender, global::UnityEngine.Material currentMaterial, global::UnityEngine.Material newMaterial)
		{
			DRAG_AND_DROP_MATERIAL_EVENT.Call(sender, currentMaterial, newMaterial);
		}

		public static void ON_TEXT_STYLE_PROPERTY_CHANGED(bool isChanged)
		{
			TEXT_STYLE_PROPERTY_EVENT.Call(isChanged);
		}

		public static void ON_COLOR_GRADIENT_PROPERTY_CHANGED(global::UnityEngine.Object obj)
		{
			COLOR_GRADIENT_PROPERTY_EVENT.Call(obj);
		}

		public static void ON_TEXT_CHANGED(global::UnityEngine.Object obj)
		{
			TEXT_CHANGED_EVENT.Call(obj);
		}

		public static void ON_TMP_SETTINGS_CHANGED()
		{
			TMP_SETTINGS_PROPERTY_EVENT.Call();
		}

		public static void ON_RESOURCES_LOADED()
		{
			RESOURCE_LOAD_EVENT.Call();
		}

		public static void ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TEXTMESHPRO_UGUI_PROPERTY_EVENT.Call(isChanged, obj);
		}

		public static void ON_COMPUTE_DT_EVENT(object Sender, global::TMPro.Compute_DT_EventArgs e)
		{
			COMPUTE_DT_EVENT.Call(Sender, e);
		}
	}
}
