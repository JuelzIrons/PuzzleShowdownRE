namespace Newtonsoft.Json.Schema
{
	[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNodeCollection : global::System.Collections.ObjectModel.KeyedCollection<string, global::Newtonsoft.Json.Schema.JsonSchemaNode>
	{
		protected override string GetKeyForItem(global::Newtonsoft.Json.Schema.JsonSchemaNode item)
		{
			return item.Id;
		}
	}
}
