namespace Unity.VisualScripting.FullSerializer
{
	public class fsWeakReferenceConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			return type == typeof(global::System.WeakReference);
		}

		public override bool RequestCycleSupport(global::System.Type storageType)
		{
			return false;
		}

		public override bool RequestInheritanceSupport(global::System.Type storageType)
		{
			return false;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			global::System.WeakReference weakReference = (global::System.WeakReference)instance;
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			serialized = global::Unity.VisualScripting.FullSerializer.fsData.CreateDictionary();
			if (weakReference.IsAlive)
			{
				global::Unity.VisualScripting.FullSerializer.fsData data;
				global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += Serializer.TrySerialize(weakReference.Target, out data));
				if (fsResult2.Failed)
				{
					return success;
				}
				serialized.AsDictionary["Target"] = data;
				serialized.AsDictionary["TrackResurrection"] = new global::Unity.VisualScripting.FullSerializer.fsData(weakReference.TrackResurrection);
			}
			return success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += CheckType(data, global::Unity.VisualScripting.FullSerializer.fsDataType.Object));
			if (fsResult2.Failed)
			{
				return success;
			}
			if (data.AsDictionary.ContainsKey("Target"))
			{
				global::Unity.VisualScripting.FullSerializer.fsData data2 = data.AsDictionary["Target"];
				object result = null;
				fsResult2 = (success += Serializer.TryDeserialize(data2, typeof(object), ref result));
				if (fsResult2.Failed)
				{
					return success;
				}
				bool trackResurrection = false;
				if (data.AsDictionary.ContainsKey("TrackResurrection") && data.AsDictionary["TrackResurrection"].IsBool)
				{
					trackResurrection = data.AsDictionary["TrackResurrection"].AsBool;
				}
				instance = new global::System.WeakReference(result, trackResurrection);
			}
			return success;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return new global::System.WeakReference(null);
		}
	}
}
