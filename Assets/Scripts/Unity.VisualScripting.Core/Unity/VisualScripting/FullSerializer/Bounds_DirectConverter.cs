namespace Unity.VisualScripting.FullSerializer
{
	public class Bounds_DirectConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.Bounds>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.Bounds model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success + SerializeMember(serialized, null, "center", model.center) + SerializeMember(serialized, null, "size", model.size);
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.Bounds model)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::UnityEngine.Vector3 value = model.center;
			global::Unity.VisualScripting.FullSerializer.fsResult obj = success + DeserializeMember<global::UnityEngine.Vector3>(data, null, "center", out value);
			model.center = value;
			global::UnityEngine.Vector3 value2 = model.size;
			global::Unity.VisualScripting.FullSerializer.fsResult result = obj + DeserializeMember<global::UnityEngine.Vector3>(data, null, "size", out value2);
			model.size = value2;
			return result;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return default(global::UnityEngine.Bounds);
		}
	}
}
