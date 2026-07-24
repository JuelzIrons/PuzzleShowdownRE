namespace Newtonsoft.Json.Serialization
{
	public class JsonStringContract : global::Newtonsoft.Json.Serialization.JsonPrimitiveContract
	{
		public JsonStringContract(global::System.Type underlyingType)
			: base(underlyingType)
		{
			ContractType = global::Newtonsoft.Json.Serialization.JsonContractType.String;
		}
	}
}
