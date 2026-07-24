namespace Newtonsoft.Json.Schema
{
	[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public static class Extensions
	{
		[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		public static bool IsValid(this global::Newtonsoft.Json.Linq.JToken source, global::Newtonsoft.Json.Schema.JsonSchema schema)
		{
			bool valid = true;
			source.Validate(schema, delegate
			{
				valid = false;
			});
			return valid;
		}

		[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		public static bool IsValid(this global::Newtonsoft.Json.Linq.JToken source, global::Newtonsoft.Json.Schema.JsonSchema schema, out global::System.Collections.Generic.IList<string> errorMessages)
		{
			global::System.Collections.Generic.IList<string> errors = new global::System.Collections.Generic.List<string>();
			source.Validate(schema, delegate(object sender, global::Newtonsoft.Json.Schema.ValidationEventArgs args)
			{
				errors.Add(args.Message);
			});
			errorMessages = errors;
			return errorMessages.Count == 0;
		}

		[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		public static void Validate(this global::Newtonsoft.Json.Linq.JToken source, global::Newtonsoft.Json.Schema.JsonSchema schema)
		{
			source.Validate(schema, null);
		}

		[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		public static void Validate(this global::Newtonsoft.Json.Linq.JToken source, global::Newtonsoft.Json.Schema.JsonSchema schema, global::Newtonsoft.Json.Schema.ValidationEventHandler validationEventHandler)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(source, "source");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(schema, "schema");
			using global::Newtonsoft.Json.JsonValidatingReader jsonValidatingReader = new global::Newtonsoft.Json.JsonValidatingReader(source.CreateReader());
			jsonValidatingReader.Schema = schema;
			if (validationEventHandler != null)
			{
				jsonValidatingReader.ValidationEventHandler += validationEventHandler;
			}
			while (jsonValidatingReader.Read())
			{
			}
		}
	}
}
