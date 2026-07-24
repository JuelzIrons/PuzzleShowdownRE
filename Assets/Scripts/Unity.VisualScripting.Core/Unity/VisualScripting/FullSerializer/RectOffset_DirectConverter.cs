namespace Unity.VisualScripting.FullSerializer
{
	public class RectOffset_DirectConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.RectOffset>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.RectOffset model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success + SerializeMember(serialized, null, "bottom", model.bottom) + SerializeMember(serialized, null, "left", model.left) + SerializeMember(serialized, null, "right", model.right) + SerializeMember(serialized, null, "top", model.top);
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.RectOffset model)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			int value = model.bottom;
			global::Unity.VisualScripting.FullSerializer.fsResult obj = success + DeserializeMember<int>(data, null, "bottom", out value);
			model.bottom = value;
			int value2 = model.left;
			global::Unity.VisualScripting.FullSerializer.fsResult obj2 = obj + DeserializeMember<int>(data, null, "left", out value2);
			model.left = value2;
			int value3 = model.right;
			global::Unity.VisualScripting.FullSerializer.fsResult obj3 = obj2 + DeserializeMember<int>(data, null, "right", out value3);
			model.right = value3;
			int value4 = model.top;
			global::Unity.VisualScripting.FullSerializer.fsResult result = obj3 + DeserializeMember<int>(data, null, "top", out value4);
			model.top = value4;
			return result;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return new global::UnityEngine.RectOffset();
		}
	}
}
