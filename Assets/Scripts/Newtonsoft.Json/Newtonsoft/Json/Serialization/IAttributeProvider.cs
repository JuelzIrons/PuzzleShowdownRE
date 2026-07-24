namespace Newtonsoft.Json.Serialization
{
	public interface IAttributeProvider
	{
		global::System.Collections.Generic.IList<global::System.Attribute> GetAttributes(bool inherit);

		global::System.Collections.Generic.IList<global::System.Attribute> GetAttributes(global::System.Type attributeType, bool inherit);
	}
}
