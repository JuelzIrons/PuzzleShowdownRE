namespace Unity.VisualScripting
{
	public class RayConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.Ray>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.Ray model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success + SerializeMember(serialized, null, "origin", model.origin) + SerializeMember(serialized, null, "direction", model.direction);
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.Ray model)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::UnityEngine.Vector3 value = model.origin;
			global::Unity.VisualScripting.FullSerializer.fsResult obj = success + DeserializeMember<global::UnityEngine.Vector3>(data, null, "origin", out value);
			model.origin = value;
			global::UnityEngine.Vector3 value2 = model.direction;
			global::Unity.VisualScripting.FullSerializer.fsResult result = obj + DeserializeMember<global::UnityEngine.Vector3>(data, null, "direction", out value2);
			model.direction = value2;
			return result;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return default(global::UnityEngine.Ray);
		}
	}
}
