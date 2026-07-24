namespace Newtonsoft.Json.Schema
{
	[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchema
	{
		private readonly string _internalId = global::System.Guid.NewGuid().ToString("N");

		public string Id { get; set; }

		public string Title { get; set; }

		public bool? Required { get; set; }

		public bool? ReadOnly { get; set; }

		public bool? Hidden { get; set; }

		public bool? Transient { get; set; }

		public string Description { get; set; }

		public global::Newtonsoft.Json.Schema.JsonSchemaType? Type { get; set; }

		public string Pattern { get; set; }

		public int? MinimumLength { get; set; }

		public int? MaximumLength { get; set; }

		public double? DivisibleBy { get; set; }

		public double? Minimum { get; set; }

		public double? Maximum { get; set; }

		public bool? ExclusiveMinimum { get; set; }

		public bool? ExclusiveMaximum { get; set; }

		public int? MinimumItems { get; set; }

		public int? MaximumItems { get; set; }

		public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchema> Items { get; set; }

		public bool PositionalItemsValidation { get; set; }

		public global::Newtonsoft.Json.Schema.JsonSchema AdditionalItems { get; set; }

		public bool AllowAdditionalItems { get; set; }

		public bool UniqueItems { get; set; }

		public global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Schema.JsonSchema> Properties { get; set; }

		public global::Newtonsoft.Json.Schema.JsonSchema AdditionalProperties { get; set; }

		public global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Schema.JsonSchema> PatternProperties { get; set; }

		public bool AllowAdditionalProperties { get; set; }

		public string Requires { get; set; }

		public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> Enum { get; set; }

		public global::Newtonsoft.Json.Schema.JsonSchemaType? Disallow { get; set; }

		public global::Newtonsoft.Json.Linq.JToken Default { get; set; }

		public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchema> Extends { get; set; }

		public string Format { get; set; }

		internal string Location { get; set; }

		internal string InternalId => _internalId;

		internal string DeferredReference { get; set; }

		internal bool ReferencesResolved { get; set; }

		public JsonSchema()
		{
			AllowAdditionalProperties = true;
			AllowAdditionalItems = true;
		}

		public static global::Newtonsoft.Json.Schema.JsonSchema Read(global::Newtonsoft.Json.JsonReader reader)
		{
			return Read(reader, new global::Newtonsoft.Json.Schema.JsonSchemaResolver());
		}

		public static global::Newtonsoft.Json.Schema.JsonSchema Read(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Schema.JsonSchemaResolver resolver)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(resolver, "resolver");
			return new global::Newtonsoft.Json.Schema.JsonSchemaBuilder(resolver).Read(reader);
		}

		public static global::Newtonsoft.Json.Schema.JsonSchema Parse(string json)
		{
			return Parse(json, new global::Newtonsoft.Json.Schema.JsonSchemaResolver());
		}

		public static global::Newtonsoft.Json.Schema.JsonSchema Parse(string json, global::Newtonsoft.Json.Schema.JsonSchemaResolver resolver)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(json, "json");
			using global::Newtonsoft.Json.JsonReader reader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(json));
			return Read(reader, resolver);
		}

		public void WriteTo(global::Newtonsoft.Json.JsonWriter writer)
		{
			WriteTo(writer, new global::Newtonsoft.Json.Schema.JsonSchemaResolver());
		}

		public void WriteTo(global::Newtonsoft.Json.JsonWriter writer, global::Newtonsoft.Json.Schema.JsonSchemaResolver resolver)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(writer, "writer");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(resolver, "resolver");
			new global::Newtonsoft.Json.Schema.JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		public override string ToString()
		{
			global::System.IO.StringWriter stringWriter = new global::System.IO.StringWriter(global::System.Globalization.CultureInfo.InvariantCulture);
			WriteTo(new global::Newtonsoft.Json.JsonTextWriter(stringWriter)
			{
				Formatting = global::Newtonsoft.Json.Formatting.Indented
			});
			return stringWriter.ToString();
		}
	}
}
