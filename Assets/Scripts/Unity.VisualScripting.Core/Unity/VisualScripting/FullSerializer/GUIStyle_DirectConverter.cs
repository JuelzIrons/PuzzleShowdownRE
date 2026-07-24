namespace Unity.VisualScripting.FullSerializer
{
	public class GUIStyle_DirectConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.GUIStyle>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.GUIStyle model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success + SerializeMember(serialized, null, "active", model.active) + SerializeMember(serialized, null, "alignment", model.alignment) + SerializeMember(serialized, null, "border", model.border) + SerializeMember(serialized, null, "clipping", model.clipping) + SerializeMember(serialized, null, "contentOffset", model.contentOffset) + SerializeMember(serialized, null, "fixedHeight", model.fixedHeight) + SerializeMember(serialized, null, "fixedWidth", model.fixedWidth) + SerializeMember(serialized, null, "focused", model.focused) + SerializeMember(serialized, null, "font", model.font) + SerializeMember(serialized, null, "fontSize", model.fontSize) + SerializeMember(serialized, null, "fontStyle", model.fontStyle) + SerializeMember(serialized, null, "hover", model.hover) + SerializeMember(serialized, null, "imagePosition", model.imagePosition) + SerializeMember(serialized, null, "margin", model.margin) + SerializeMember(serialized, null, "name", model.name) + SerializeMember(serialized, null, "normal", model.normal) + SerializeMember(serialized, null, "onActive", model.onActive) + SerializeMember(serialized, null, "onFocused", model.onFocused) + SerializeMember(serialized, null, "onHover", model.onHover) + SerializeMember(serialized, null, "onNormal", model.onNormal) + SerializeMember(serialized, null, "overflow", model.overflow) + SerializeMember(serialized, null, "padding", model.padding) + SerializeMember(serialized, null, "richText", model.richText) + SerializeMember(serialized, null, "stretchHeight", model.stretchHeight) + SerializeMember(serialized, null, "stretchWidth", model.stretchWidth) + SerializeMember(serialized, null, "wordWrap", model.wordWrap);
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.GUIStyle model)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::UnityEngine.GUIStyleState value = model.active;
			global::Unity.VisualScripting.FullSerializer.fsResult obj = success + DeserializeMember<global::UnityEngine.GUIStyleState>(data, null, "active", out value);
			model.active = value;
			global::UnityEngine.TextAnchor value2 = model.alignment;
			global::Unity.VisualScripting.FullSerializer.fsResult obj2 = obj + DeserializeMember<global::UnityEngine.TextAnchor>(data, null, "alignment", out value2);
			model.alignment = value2;
			global::UnityEngine.RectOffset value3 = model.border;
			global::Unity.VisualScripting.FullSerializer.fsResult obj3 = obj2 + DeserializeMember<global::UnityEngine.RectOffset>(data, null, "border", out value3);
			model.border = value3;
			global::UnityEngine.TextClipping value4 = model.clipping;
			global::Unity.VisualScripting.FullSerializer.fsResult obj4 = obj3 + DeserializeMember<global::UnityEngine.TextClipping>(data, null, "clipping", out value4);
			model.clipping = value4;
			global::UnityEngine.Vector2 value5 = model.contentOffset;
			global::Unity.VisualScripting.FullSerializer.fsResult obj5 = obj4 + DeserializeMember<global::UnityEngine.Vector2>(data, null, "contentOffset", out value5);
			model.contentOffset = value5;
			float value6 = model.fixedHeight;
			global::Unity.VisualScripting.FullSerializer.fsResult obj6 = obj5 + DeserializeMember<float>(data, null, "fixedHeight", out value6);
			model.fixedHeight = value6;
			float value7 = model.fixedWidth;
			global::Unity.VisualScripting.FullSerializer.fsResult obj7 = obj6 + DeserializeMember<float>(data, null, "fixedWidth", out value7);
			model.fixedWidth = value7;
			global::UnityEngine.GUIStyleState value8 = model.focused;
			global::Unity.VisualScripting.FullSerializer.fsResult obj8 = obj7 + DeserializeMember<global::UnityEngine.GUIStyleState>(data, null, "focused", out value8);
			model.focused = value8;
			global::UnityEngine.Font value9 = model.font;
			global::Unity.VisualScripting.FullSerializer.fsResult obj9 = obj8 + DeserializeMember<global::UnityEngine.Font>(data, null, "font", out value9);
			model.font = value9;
			int value10 = model.fontSize;
			global::Unity.VisualScripting.FullSerializer.fsResult obj10 = obj9 + DeserializeMember<int>(data, null, "fontSize", out value10);
			model.fontSize = value10;
			global::UnityEngine.FontStyle value11 = model.fontStyle;
			global::Unity.VisualScripting.FullSerializer.fsResult obj11 = obj10 + DeserializeMember<global::UnityEngine.FontStyle>(data, null, "fontStyle", out value11);
			model.fontStyle = value11;
			global::UnityEngine.GUIStyleState value12 = model.hover;
			global::Unity.VisualScripting.FullSerializer.fsResult obj12 = obj11 + DeserializeMember<global::UnityEngine.GUIStyleState>(data, null, "hover", out value12);
			model.hover = value12;
			global::UnityEngine.ImagePosition value13 = model.imagePosition;
			global::Unity.VisualScripting.FullSerializer.fsResult obj13 = obj12 + DeserializeMember<global::UnityEngine.ImagePosition>(data, null, "imagePosition", out value13);
			model.imagePosition = value13;
			global::UnityEngine.RectOffset value14 = model.margin;
			global::Unity.VisualScripting.FullSerializer.fsResult obj14 = obj13 + DeserializeMember<global::UnityEngine.RectOffset>(data, null, "margin", out value14);
			model.margin = value14;
			string value15 = model.name;
			global::Unity.VisualScripting.FullSerializer.fsResult obj15 = obj14 + DeserializeMember<string>(data, null, "name", out value15);
			model.name = value15;
			global::UnityEngine.GUIStyleState value16 = model.normal;
			global::Unity.VisualScripting.FullSerializer.fsResult obj16 = obj15 + DeserializeMember<global::UnityEngine.GUIStyleState>(data, null, "normal", out value16);
			model.normal = value16;
			global::UnityEngine.GUIStyleState value17 = model.onActive;
			global::Unity.VisualScripting.FullSerializer.fsResult obj17 = obj16 + DeserializeMember<global::UnityEngine.GUIStyleState>(data, null, "onActive", out value17);
			model.onActive = value17;
			global::UnityEngine.GUIStyleState value18 = model.onFocused;
			global::Unity.VisualScripting.FullSerializer.fsResult obj18 = obj17 + DeserializeMember<global::UnityEngine.GUIStyleState>(data, null, "onFocused", out value18);
			model.onFocused = value18;
			global::UnityEngine.GUIStyleState value19 = model.onHover;
			global::Unity.VisualScripting.FullSerializer.fsResult obj19 = obj18 + DeserializeMember<global::UnityEngine.GUIStyleState>(data, null, "onHover", out value19);
			model.onHover = value19;
			global::UnityEngine.GUIStyleState value20 = model.onNormal;
			global::Unity.VisualScripting.FullSerializer.fsResult obj20 = obj19 + DeserializeMember<global::UnityEngine.GUIStyleState>(data, null, "onNormal", out value20);
			model.onNormal = value20;
			global::UnityEngine.RectOffset value21 = model.overflow;
			global::Unity.VisualScripting.FullSerializer.fsResult obj21 = obj20 + DeserializeMember<global::UnityEngine.RectOffset>(data, null, "overflow", out value21);
			model.overflow = value21;
			global::UnityEngine.RectOffset value22 = model.padding;
			global::Unity.VisualScripting.FullSerializer.fsResult obj22 = obj21 + DeserializeMember<global::UnityEngine.RectOffset>(data, null, "padding", out value22);
			model.padding = value22;
			bool value23 = model.richText;
			global::Unity.VisualScripting.FullSerializer.fsResult obj23 = obj22 + DeserializeMember<bool>(data, null, "richText", out value23);
			model.richText = value23;
			bool value24 = model.stretchHeight;
			global::Unity.VisualScripting.FullSerializer.fsResult obj24 = obj23 + DeserializeMember<bool>(data, null, "stretchHeight", out value24);
			model.stretchHeight = value24;
			bool value25 = model.stretchWidth;
			global::Unity.VisualScripting.FullSerializer.fsResult obj25 = obj24 + DeserializeMember<bool>(data, null, "stretchWidth", out value25);
			model.stretchWidth = value25;
			bool value26 = model.wordWrap;
			global::Unity.VisualScripting.FullSerializer.fsResult result = obj25 + DeserializeMember<bool>(data, null, "wordWrap", out value26);
			model.wordWrap = value26;
			return result;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return new global::UnityEngine.GUIStyle();
		}
	}
}
