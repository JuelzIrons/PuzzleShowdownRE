namespace Newtonsoft.Json.Serialization
{
	public class ReflectionAttributeProvider : global::Newtonsoft.Json.Serialization.IAttributeProvider
	{
		private readonly object _attributeProvider;

		public ReflectionAttributeProvider(object attributeProvider)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			_attributeProvider = attributeProvider;
		}

		public global::System.Collections.Generic.IList<global::System.Attribute> GetAttributes(bool inherit)
		{
			return global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttributes(_attributeProvider, null, inherit);
		}

		public global::System.Collections.Generic.IList<global::System.Attribute> GetAttributes(global::System.Type attributeType, bool inherit)
		{
			return global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttributes(_attributeProvider, attributeType, inherit);
		}
	}
}
