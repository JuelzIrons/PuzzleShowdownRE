namespace Unity.VisualScripting.FullSerializer
{
	public class LayerMask_DirectConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.LayerMask>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.LayerMask model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success + SerializeMember(serialized, null, "value", model.value);
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.LayerMask model)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			int value = model.value;
			global::Unity.VisualScripting.FullSerializer.fsResult result = success + DeserializeMember<int>(data, null, "value", out value);
			model.value = value;
			return result;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return default(global::UnityEngine.LayerMask);
		}
	}
}
