namespace Newtonsoft.Json.Serialization
{
	public class JsonContainerContract : global::Newtonsoft.Json.Serialization.JsonContract
	{
		private global::Newtonsoft.Json.Serialization.JsonContract? _itemContract;

		private global::Newtonsoft.Json.Serialization.JsonContract? _finalItemContract;

		internal global::Newtonsoft.Json.Serialization.JsonContract? ItemContract
		{
			get
			{
				return _itemContract;
			}
			set
			{
				_itemContract = value;
				if (_itemContract != null)
				{
					_finalItemContract = (global::Newtonsoft.Json.Utilities.TypeExtensions.IsSealed(_itemContract.UnderlyingType) ? _itemContract : null);
				}
				else
				{
					_finalItemContract = null;
				}
			}
		}

		internal global::Newtonsoft.Json.Serialization.JsonContract? FinalItemContract => _finalItemContract;

		public global::Newtonsoft.Json.JsonConverter? ItemConverter { get; set; }

		public bool? ItemIsReference { get; set; }

		public global::Newtonsoft.Json.ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		public global::Newtonsoft.Json.TypeNameHandling? ItemTypeNameHandling { get; set; }

		internal JsonContainerContract(global::System.Type underlyingType)
			: base(underlyingType)
		{
			global::Newtonsoft.Json.JsonContainerAttribute cachedAttribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetCachedAttribute<global::Newtonsoft.Json.JsonContainerAttribute>(underlyingType);
			if (cachedAttribute != null)
			{
				if (cachedAttribute.ItemConverterType != null)
				{
					ItemConverter = global::Newtonsoft.Json.Serialization.JsonTypeReflector.CreateJsonConverterInstance(cachedAttribute.ItemConverterType, cachedAttribute.ItemConverterParameters);
				}
				ItemIsReference = cachedAttribute._itemIsReference;
				ItemReferenceLoopHandling = cachedAttribute._itemReferenceLoopHandling;
				ItemTypeNameHandling = cachedAttribute._itemTypeNameHandling;
			}
		}
	}
}
