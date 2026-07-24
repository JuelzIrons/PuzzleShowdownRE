namespace Unity.VisualScripting.FullSerializer
{
	public abstract class fsDirectConverter : global::Unity.VisualScripting.FullSerializer.fsBaseConverter
	{
		public abstract global::System.Type ModelType { get; }
	}
	public abstract class fsDirectConverter<TModel> : global::Unity.VisualScripting.FullSerializer.fsDirectConverter
	{
		public override global::System.Type ModelType => typeof(TModel);

		public sealed override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> dictionary = new global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData>();
			global::Unity.VisualScripting.FullSerializer.fsResult result = DoSerialize((TModel)instance, dictionary);
			serialized = new global::Unity.VisualScripting.FullSerializer.fsData(dictionary);
			return result;
		}

		public sealed override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += CheckType(data, global::Unity.VisualScripting.FullSerializer.fsDataType.Object));
			if (fsResult2.Failed)
			{
				return success;
			}
			TModel model = (TModel)instance;
			success += DoDeserialize(data.AsDictionary, ref model);
			instance = model;
			return success;
		}

		protected abstract global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(TModel model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized);

		protected abstract global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref TModel model);
	}
}
