namespace Unity.Services.Authentication.Shared
{
	internal abstract class AbstractOpenAPISchema
	{
		public static readonly global::Newtonsoft.Json.JsonSerializerSettings SerializerSettings = new global::Newtonsoft.Json.JsonSerializerSettings
		{
			ConstructorHandling = global::Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor,
			MissingMemberHandling = global::Newtonsoft.Json.MissingMemberHandling.Error,
			ContractResolver = new global::Newtonsoft.Json.Serialization.DefaultContractResolver
			{
				NamingStrategy = new global::Newtonsoft.Json.Serialization.CamelCaseNamingStrategy
				{
					OverrideSpecifiedNames = false
				}
			}
		};

		public static readonly global::Newtonsoft.Json.JsonSerializerSettings AdditionalPropertiesSerializerSettings = new global::Newtonsoft.Json.JsonSerializerSettings
		{
			ConstructorHandling = global::Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor,
			MissingMemberHandling = global::Newtonsoft.Json.MissingMemberHandling.Ignore,
			ContractResolver = new global::Newtonsoft.Json.Serialization.DefaultContractResolver
			{
				NamingStrategy = new global::Newtonsoft.Json.Serialization.CamelCaseNamingStrategy
				{
					OverrideSpecifiedNames = false
				}
			}
		};

		public abstract object ActualInstance { get; set; }

		public bool IsNullable { get; protected set; }

		public string SchemaType { get; protected set; }

		public abstract string ToJson();
	}
}
