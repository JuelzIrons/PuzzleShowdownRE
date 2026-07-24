namespace Unity.VisualScripting.FullSerializer
{
	public class AnimationCurve_DirectConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.AnimationCurve>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.AnimationCurve model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success + SerializeMember(serialized, null, "keys", model.keys) + SerializeMember(serialized, null, "preWrapMode", model.preWrapMode) + SerializeMember(serialized, null, "postWrapMode", model.postWrapMode);
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.AnimationCurve model)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::UnityEngine.Keyframe[] value = model.keys;
			global::Unity.VisualScripting.FullSerializer.fsResult obj = success + DeserializeMember<global::UnityEngine.Keyframe[]>(data, null, "keys", out value);
			model.keys = value;
			global::UnityEngine.WrapMode value2 = model.preWrapMode;
			global::Unity.VisualScripting.FullSerializer.fsResult obj2 = obj + DeserializeMember<global::UnityEngine.WrapMode>(data, null, "preWrapMode", out value2);
			model.preWrapMode = value2;
			global::UnityEngine.WrapMode value3 = model.postWrapMode;
			global::Unity.VisualScripting.FullSerializer.fsResult result = obj2 + DeserializeMember<global::UnityEngine.WrapMode>(data, null, "postWrapMode", out value3);
			model.postWrapMode = value3;
			return result;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return new global::UnityEngine.AnimationCurve();
		}
	}
}
