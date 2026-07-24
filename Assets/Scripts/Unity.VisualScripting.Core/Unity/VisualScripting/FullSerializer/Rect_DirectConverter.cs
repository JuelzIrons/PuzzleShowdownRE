namespace Unity.VisualScripting.FullSerializer
{
	public class Rect_DirectConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.Rect>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.Rect model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success + SerializeMember(serialized, null, "xMin", model.xMin) + SerializeMember(serialized, null, "yMin", model.yMin) + SerializeMember(serialized, null, "xMax", model.xMax) + SerializeMember(serialized, null, "yMax", model.yMax);
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.Rect model)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			float value = model.xMin;
			global::Unity.VisualScripting.FullSerializer.fsResult obj = success + DeserializeMember<float>(data, null, "xMin", out value);
			model.xMin = value;
			float value2 = model.yMin;
			global::Unity.VisualScripting.FullSerializer.fsResult obj2 = obj + DeserializeMember<float>(data, null, "yMin", out value2);
			model.yMin = value2;
			float value3 = model.xMax;
			global::Unity.VisualScripting.FullSerializer.fsResult obj3 = obj2 + DeserializeMember<float>(data, null, "xMax", out value3);
			model.xMax = value3;
			float value4 = model.yMax;
			global::Unity.VisualScripting.FullSerializer.fsResult result = obj3 + DeserializeMember<float>(data, null, "yMax", out value4);
			model.yMax = value4;
			return result;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return default(global::UnityEngine.Rect);
		}
	}
}
