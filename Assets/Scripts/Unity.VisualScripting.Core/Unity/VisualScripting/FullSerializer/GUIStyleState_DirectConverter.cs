namespace Unity.VisualScripting.FullSerializer
{
	public class GUIStyleState_DirectConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.GUIStyleState>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.GUIStyleState model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success + SerializeMember(serialized, null, "background", model.background) + SerializeMember(serialized, null, "textColor", model.textColor);
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.GUIStyleState model)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::UnityEngine.Texture2D value = model.background;
			global::Unity.VisualScripting.FullSerializer.fsResult obj = success + DeserializeMember<global::UnityEngine.Texture2D>(data, null, "background", out value);
			model.background = value;
			global::UnityEngine.Color value2 = model.textColor;
			global::Unity.VisualScripting.FullSerializer.fsResult result = obj + DeserializeMember<global::UnityEngine.Color>(data, null, "textColor", out value2);
			model.textColor = value2;
			return result;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return new global::UnityEngine.GUIStyleState();
		}
	}
}
