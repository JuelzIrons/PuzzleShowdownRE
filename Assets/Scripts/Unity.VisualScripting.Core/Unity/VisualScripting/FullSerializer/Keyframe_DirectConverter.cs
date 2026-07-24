namespace Unity.VisualScripting.FullSerializer
{
	public class Keyframe_DirectConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.Keyframe>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.Keyframe model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success + SerializeMember(serialized, null, "time", model.time) + SerializeMember(serialized, null, "value", model.value) + SerializeMember(serialized, null, "tangentMode", model.tangentMode) + SerializeMember(serialized, null, "inTangent", model.inTangent) + SerializeMember(serialized, null, "outTangent", model.outTangent);
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.Keyframe model)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			float value = model.time;
			global::Unity.VisualScripting.FullSerializer.fsResult obj = success + DeserializeMember<float>(data, null, "time", out value);
			model.time = value;
			float value2 = model.value;
			global::Unity.VisualScripting.FullSerializer.fsResult obj2 = obj + DeserializeMember<float>(data, null, "value", out value2);
			model.value = value2;
			int value3 = model.tangentMode;
			global::Unity.VisualScripting.FullSerializer.fsResult obj3 = obj2 + DeserializeMember<int>(data, null, "tangentMode", out value3);
			model.tangentMode = value3;
			float value4 = model.inTangent;
			global::Unity.VisualScripting.FullSerializer.fsResult obj4 = obj3 + DeserializeMember<float>(data, null, "inTangent", out value4);
			model.inTangent = value4;
			float value5 = model.outTangent;
			global::Unity.VisualScripting.FullSerializer.fsResult result = obj4 + DeserializeMember<float>(data, null, "outTangent", out value5);
			model.outTangent = value5;
			return result;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return default(global::UnityEngine.Keyframe);
		}
	}
}
