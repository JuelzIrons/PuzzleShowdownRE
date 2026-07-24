namespace Unity.VisualScripting.FullSerializer.Internal.Converters
{
	public class UnityEvent_Converter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			if (typeof(global::UnityEngine.Events.UnityEvent).Resolve().IsAssignableFrom(type.Resolve()))
			{
				return !type.Resolve().IsGenericType;
			}
			return false;
		}

		public override bool RequestCycleSupport(global::System.Type storageType)
		{
			return false;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			global::System.Type type = (global::System.Type)instance;
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			instance = global::UnityEngine.JsonUtility.FromJson(global::Unity.VisualScripting.FullSerializer.fsJsonPrinter.CompressedJson(data), type);
			return success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			serialized = global::Unity.VisualScripting.FullSerializer.fsJsonParser.Parse(global::UnityEngine.JsonUtility.ToJson(instance));
			return success;
		}
	}
}
