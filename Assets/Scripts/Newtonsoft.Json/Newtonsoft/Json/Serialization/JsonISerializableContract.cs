namespace Newtonsoft.Json.Serialization
{
	public class JsonISerializableContract : global::Newtonsoft.Json.Serialization.JsonContainerContract
	{
		public global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? ISerializableCreator { get; set; }

		public JsonISerializableContract(global::System.Type underlyingType)
			: base(underlyingType)
		{
			ContractType = global::Newtonsoft.Json.Serialization.JsonContractType.Serializable;
		}
	}
}
