namespace Newtonsoft.Json.Serialization
{
	public class CamelCasePropertyNamesContractResolver : global::Newtonsoft.Json.Serialization.DefaultContractResolver
	{
		private static readonly object TypeContractCacheLock = new object();

		private static readonly global::Newtonsoft.Json.DefaultJsonNameTable NameTable = new global::Newtonsoft.Json.DefaultJsonNameTable();

		private static global::System.Collections.Generic.Dictionary<global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type>, global::Newtonsoft.Json.Serialization.JsonContract>? _contractCache;

		public CamelCasePropertyNamesContractResolver()
		{
			base.NamingStrategy = new global::Newtonsoft.Json.Serialization.CamelCaseNamingStrategy
			{
				ProcessDictionaryKeys = true,
				OverrideSpecifiedNames = true
			};
		}

		public override global::Newtonsoft.Json.Serialization.JsonContract ResolveContract(global::System.Type type)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type> key = new global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type>(GetType(), type);
			global::System.Collections.Generic.Dictionary<global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type>, global::Newtonsoft.Json.Serialization.JsonContract> contractCache = _contractCache;
			if (contractCache == null || !contractCache.TryGetValue(key, out var value))
			{
				value = CreateContract(type);
				lock (TypeContractCacheLock)
				{
					contractCache = _contractCache;
					global::System.Collections.Generic.Dictionary<global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type>, global::Newtonsoft.Json.Serialization.JsonContract> obj = ((contractCache != null) ? new global::System.Collections.Generic.Dictionary<global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type>, global::Newtonsoft.Json.Serialization.JsonContract>(contractCache) : new global::System.Collections.Generic.Dictionary<global::Newtonsoft.Json.Utilities.StructMultiKey<global::System.Type, global::System.Type>, global::Newtonsoft.Json.Serialization.JsonContract>());
					obj[key] = value;
					_contractCache = obj;
				}
			}
			return value;
		}

		internal override global::Newtonsoft.Json.DefaultJsonNameTable GetNameTable()
		{
			return NameTable;
		}
	}
}
