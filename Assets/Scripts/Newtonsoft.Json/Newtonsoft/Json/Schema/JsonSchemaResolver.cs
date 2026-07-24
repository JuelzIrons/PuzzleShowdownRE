namespace Newtonsoft.Json.Schema
{
	[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaResolver
	{
		public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchema> LoadedSchemas { get; protected set; }

		public JsonSchemaResolver()
		{
			LoadedSchemas = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchema>();
		}

		public virtual global::Newtonsoft.Json.Schema.JsonSchema GetSchema(string reference)
		{
			global::Newtonsoft.Json.Schema.JsonSchema jsonSchema = global::System.Linq.Enumerable.SingleOrDefault(LoadedSchemas, (global::Newtonsoft.Json.Schema.JsonSchema s) => string.Equals(s.Id, reference, global::System.StringComparison.Ordinal));
			if (jsonSchema == null)
			{
				jsonSchema = global::System.Linq.Enumerable.SingleOrDefault(LoadedSchemas, (global::Newtonsoft.Json.Schema.JsonSchema s) => string.Equals(s.Location, reference, global::System.StringComparison.Ordinal));
			}
			return jsonSchema;
		}
	}
}
