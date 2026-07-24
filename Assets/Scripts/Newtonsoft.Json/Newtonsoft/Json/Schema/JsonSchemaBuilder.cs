namespace Newtonsoft.Json.Schema
{
	[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaBuilder
	{
		private readonly global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchema> _stack;

		private readonly global::Newtonsoft.Json.Schema.JsonSchemaResolver _resolver;

		private readonly global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Schema.JsonSchema> _documentSchemas;

		private global::Newtonsoft.Json.Schema.JsonSchema _currentSchema;

		private global::Newtonsoft.Json.Linq.JObject _rootSchema;

		private global::Newtonsoft.Json.Schema.JsonSchema CurrentSchema => _currentSchema;

		public JsonSchemaBuilder(global::Newtonsoft.Json.Schema.JsonSchemaResolver resolver)
		{
			_stack = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchema>();
			_documentSchemas = new global::System.Collections.Generic.Dictionary<string, global::Newtonsoft.Json.Schema.JsonSchema>();
			_resolver = resolver;
		}

		private void Push(global::Newtonsoft.Json.Schema.JsonSchema value)
		{
			_currentSchema = value;
			_stack.Add(value);
			_resolver.LoadedSchemas.Add(value);
			_documentSchemas.Add(value.Location, value);
		}

		private global::Newtonsoft.Json.Schema.JsonSchema Pop()
		{
			global::Newtonsoft.Json.Schema.JsonSchema currentSchema = _currentSchema;
			_stack.RemoveAt(_stack.Count - 1);
			_currentSchema = global::System.Linq.Enumerable.LastOrDefault(_stack);
			return currentSchema;
		}

		internal global::Newtonsoft.Json.Schema.JsonSchema Read(global::Newtonsoft.Json.JsonReader reader)
		{
			global::Newtonsoft.Json.Linq.JToken jToken = global::Newtonsoft.Json.Linq.JToken.ReadFrom(reader);
			_rootSchema = jToken as global::Newtonsoft.Json.Linq.JObject;
			global::Newtonsoft.Json.Schema.JsonSchema jsonSchema = BuildSchema(jToken);
			ResolveReferences(jsonSchema);
			return jsonSchema;
		}

		private string UnescapeReference(string reference)
		{
			return global::Newtonsoft.Json.Utilities.StringUtils.Replace(global::Newtonsoft.Json.Utilities.StringUtils.Replace(global::System.Uri.UnescapeDataString(reference), "~1", "/"), "~0", "~");
		}

		private global::Newtonsoft.Json.Schema.JsonSchema ResolveReferences(global::Newtonsoft.Json.Schema.JsonSchema schema)
		{
			if (schema.DeferredReference != null)
			{
				string text = schema.DeferredReference;
				bool flag = text.StartsWith("#", global::System.StringComparison.Ordinal);
				if (flag)
				{
					text = UnescapeReference(text);
				}
				global::Newtonsoft.Json.Schema.JsonSchema jsonSchema = _resolver.GetSchema(text);
				if (jsonSchema == null)
				{
					if (flag)
					{
						string[] array = schema.DeferredReference.TrimStart(new char[1] { '#' }).Split(new char[1] { '/' }, global::System.StringSplitOptions.RemoveEmptyEntries);
						global::Newtonsoft.Json.Linq.JToken jToken = _rootSchema;
						string[] array2 = array;
						foreach (string reference in array2)
						{
							string text2 = UnescapeReference(reference);
							if (jToken.Type == global::Newtonsoft.Json.Linq.JTokenType.Object)
							{
								jToken = jToken[text2];
							}
							else if (jToken.Type == global::Newtonsoft.Json.Linq.JTokenType.Array || jToken.Type == global::Newtonsoft.Json.Linq.JTokenType.Constructor)
							{
								jToken = ((!int.TryParse(text2, out var result) || result < 0 || result >= global::System.Linq.Enumerable.Count(jToken)) ? null : jToken[result]);
							}
							if (jToken == null)
							{
								break;
							}
						}
						if (jToken != null)
						{
							jsonSchema = BuildSchema(jToken);
						}
					}
					if (jsonSchema == null)
					{
						throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not resolve schema reference '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, schema.DeferredReference));
					}
				}
				schema = jsonSchema;
			}
			if (schema.ReferencesResolved)
			{
				return schema;
			}
			schema.ReferencesResolved = true;
			if (schema.Extends != null)
			{
				for (int j = 0; j < schema.Extends.Count; j++)
				{
					schema.Extends[j] = ResolveReferences(schema.Extends[j]);
				}
			}
			if (schema.Items != null)
			{
				for (int k = 0; k < schema.Items.Count; k++)
				{
					schema.Items[k] = ResolveReferences(schema.Items[k]);
				}
			}
			if (schema.AdditionalItems != null)
			{
				schema.AdditionalItems = ResolveReferences(schema.AdditionalItems);
			}
			if (schema.PatternProperties != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Schema.JsonSchema> item in global::System.Linq.Enumerable.ToList(schema.PatternProperties))
				{
					schema.PatternProperties[item.Key] = ResolveReferences(item.Value);
				}
			}
			if (schema.Properties != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Schema.JsonSchema> item2 in global::System.Linq.Enumerable.ToList(schema.Properties))
				{
					schema.Properties[item2.Key] = ResolveReferences(item2.Value);
				}
			}
			if (schema.AdditionalProperties != null)
			{
				schema.AdditionalProperties = ResolveReferences(schema.AdditionalProperties);
			}
			return schema;
		}

		private global::Newtonsoft.Json.Schema.JsonSchema BuildSchema(global::Newtonsoft.Json.Linq.JToken token)
		{
			if (!(token is global::Newtonsoft.Json.Linq.JObject jObject))
			{
				throw global::Newtonsoft.Json.JsonException.Create(token, token.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Expected object while parsing schema object, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, token.Type));
			}
			if (jObject.TryGetValue("$ref", out global::Newtonsoft.Json.Linq.JToken value))
			{
				return new global::Newtonsoft.Json.Schema.JsonSchema
				{
					DeferredReference = (string?)value
				};
			}
			string path = token.Path;
			path = global::Newtonsoft.Json.Utilities.StringUtils.Replace(path, ".", "/");
			path = global::Newtonsoft.Json.Utilities.StringUtils.Replace(path, "[", "/");
			path = global::Newtonsoft.Json.Utilities.StringUtils.Replace(path, "]", string.Empty);
			if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(path))
			{
				path = "/" + path;
			}
			path = "#" + path;
			if (_documentSchemas.TryGetValue(path, out var value2))
			{
				return value2;
			}
			Push(new global::Newtonsoft.Json.Schema.JsonSchema
			{
				Location = path
			});
			ProcessSchemaProperties(jObject);
			return Pop();
		}

		private void ProcessSchemaProperties(global::Newtonsoft.Json.Linq.JObject schemaObject)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken> item in schemaObject)
			{
				switch (item.Key)
				{
				case "type":
					CurrentSchema.Type = ProcessType(item.Value);
					break;
				case "id":
					CurrentSchema.Id = (string?)item.Value;
					break;
				case "title":
					CurrentSchema.Title = (string?)item.Value;
					break;
				case "description":
					CurrentSchema.Description = (string?)item.Value;
					break;
				case "properties":
					CurrentSchema.Properties = ProcessProperties(item.Value);
					break;
				case "items":
					ProcessItems(item.Value);
					break;
				case "additionalProperties":
					ProcessAdditionalProperties(item.Value);
					break;
				case "additionalItems":
					ProcessAdditionalItems(item.Value);
					break;
				case "patternProperties":
					CurrentSchema.PatternProperties = ProcessProperties(item.Value);
					break;
				case "required":
					CurrentSchema.Required = (bool)item.Value;
					break;
				case "requires":
					CurrentSchema.Requires = (string?)item.Value;
					break;
				case "minimum":
					CurrentSchema.Minimum = (double)item.Value;
					break;
				case "maximum":
					CurrentSchema.Maximum = (double)item.Value;
					break;
				case "exclusiveMinimum":
					CurrentSchema.ExclusiveMinimum = (bool)item.Value;
					break;
				case "exclusiveMaximum":
					CurrentSchema.ExclusiveMaximum = (bool)item.Value;
					break;
				case "maxLength":
					CurrentSchema.MaximumLength = (int)item.Value;
					break;
				case "minLength":
					CurrentSchema.MinimumLength = (int)item.Value;
					break;
				case "maxItems":
					CurrentSchema.MaximumItems = (int)item.Value;
					break;
				case "minItems":
					CurrentSchema.MinimumItems = (int)item.Value;
					break;
				case "divisibleBy":
					CurrentSchema.DivisibleBy = (double)item.Value;
					break;
				case "disallow":
					CurrentSchema.Disallow = ProcessType(item.Value);
					break;
				case "default":
					CurrentSchema.Default = item.Value.DeepClone();
					break;
				case "hidden":
					CurrentSchema.Hidden = (bool)item.Value;
					break;
				case "readonly":
					CurrentSchema.ReadOnly = (bool)item.Value;
					break;
				case "format":
					CurrentSchema.Format = (string?)item.Value;
					break;
				case "pattern":
					CurrentSchema.Pattern = (string?)item.Value;
					break;
				case "enum":
					ProcessEnum(item.Value);
					break;
				case "extends":
					ProcessExtends(item.Value);
					break;
				case "uniqueItems":
					CurrentSchema.UniqueItems = (bool)item.Value;
					break;
				}
			}
		}

		private void ProcessExtends(global::Newtonsoft.Json.Linq.JToken token)
		{
			global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchema> list = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchema>();
			if (token.Type == global::Newtonsoft.Json.Linq.JTokenType.Array)
			{
				foreach (global::Newtonsoft.Json.Linq.JToken item in (global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>)token)
				{
					list.Add(BuildSchema(item));
				}
			}
			else
			{
				global::Newtonsoft.Json.Schema.JsonSchema jsonSchema = BuildSchema(token);
				if (jsonSchema != null)
				{
					list.Add(jsonSchema);
				}
			}
			if (list.Count > 0)
			{
				CurrentSchema.Extends = list;
			}
		}

		private void ProcessEnum(global::Newtonsoft.Json.Linq.JToken token)
		{
			if (token.Type != global::Newtonsoft.Json.Linq.JTokenType.Array)
			{
				throw global::Newtonsoft.Json.JsonException.Create(token, token.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Expected Array token while parsing enum values, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, token.Type));
			}
			CurrentSchema.Enum = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JToken>();
			foreach (global::Newtonsoft.Json.Linq.JToken item in (global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>)token)
			{
				CurrentSchema.Enum.Add(item.DeepClone());
			}
		}

		private void ProcessAdditionalProperties(global::Newtonsoft.Json.Linq.JToken token)
		{
			if (token.Type == global::Newtonsoft.Json.Linq.JTokenType.Boolean)
			{
				CurrentSchema.AllowAdditionalProperties = (bool)token;
			}
			else
			{
				CurrentSchema.AdditionalProperties = BuildSchema(token);
			}
		}

		private void ProcessAdditionalItems(global::Newtonsoft.Json.Linq.JToken token)
		{
			if (token.Type == global::Newtonsoft.Json.Linq.JTokenType.Boolean)
			{
				CurrentSchema.AllowAdditionalItems = (bool)token;
			}
			else
			{
				CurrentSchema.AdditionalItems = BuildSchema(token);
			}
		}

		private global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Schema.JsonSchema> ProcessProperties(global::Newtonsoft.Json.Linq.JToken token)
		{
			global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Schema.JsonSchema> dictionary = new global::System.Collections.Generic.Dictionary<string, global::Newtonsoft.Json.Schema.JsonSchema>();
			if (token.Type != global::Newtonsoft.Json.Linq.JTokenType.Object)
			{
				throw global::Newtonsoft.Json.JsonException.Create(token, token.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Expected Object token while parsing schema properties, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, token.Type));
			}
			foreach (global::Newtonsoft.Json.Linq.JProperty item in (global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>)token)
			{
				if (dictionary.ContainsKey(item.Name))
				{
					throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Property {0} has already been defined in schema.", global::System.Globalization.CultureInfo.InvariantCulture, item.Name));
				}
				dictionary.Add(item.Name, BuildSchema(item.Value));
			}
			return dictionary;
		}

		private void ProcessItems(global::Newtonsoft.Json.Linq.JToken token)
		{
			CurrentSchema.Items = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchema>();
			switch (token.Type)
			{
			case global::Newtonsoft.Json.Linq.JTokenType.Object:
				CurrentSchema.Items.Add(BuildSchema(token));
				CurrentSchema.PositionalItemsValidation = false;
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Array:
				CurrentSchema.PositionalItemsValidation = true;
				{
					foreach (global::Newtonsoft.Json.Linq.JToken item in (global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>)token)
					{
						CurrentSchema.Items.Add(BuildSchema(item));
					}
					break;
				}
			default:
				throw global::Newtonsoft.Json.JsonException.Create(token, token.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Expected array or JSON schema object, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, token.Type));
			}
		}

		private global::Newtonsoft.Json.Schema.JsonSchemaType? ProcessType(global::Newtonsoft.Json.Linq.JToken token)
		{
			switch (token.Type)
			{
			case global::Newtonsoft.Json.Linq.JTokenType.Array:
			{
				global::Newtonsoft.Json.Schema.JsonSchemaType? jsonSchemaType = global::Newtonsoft.Json.Schema.JsonSchemaType.None;
				{
					foreach (global::Newtonsoft.Json.Linq.JToken item in (global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>)token)
					{
						if (item.Type != global::Newtonsoft.Json.Linq.JTokenType.String)
						{
							throw global::Newtonsoft.Json.JsonException.Create(item, item.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Expected JSON schema type string token, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, token.Type));
						}
						jsonSchemaType |= MapType((string?)item);
					}
					return jsonSchemaType;
				}
			}
			case global::Newtonsoft.Json.Linq.JTokenType.String:
				return MapType((string?)token);
			default:
				throw global::Newtonsoft.Json.JsonException.Create(token, token.Path, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Expected array or JSON schema type string token, got {0}.", global::System.Globalization.CultureInfo.InvariantCulture, token.Type));
			}
		}

		internal static global::Newtonsoft.Json.Schema.JsonSchemaType MapType(string type)
		{
			if (!global::Newtonsoft.Json.Schema.JsonSchemaConstants.JsonSchemaTypeMapping.TryGetValue(type, out var value))
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid JSON schema type: {0}", global::System.Globalization.CultureInfo.InvariantCulture, type));
			}
			return value;
		}

		internal static string MapType(global::Newtonsoft.Json.Schema.JsonSchemaType type)
		{
			return global::System.Linq.Enumerable.Single(global::Newtonsoft.Json.Schema.JsonSchemaConstants.JsonSchemaTypeMapping, (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Schema.JsonSchemaType> kv) => kv.Value == type).Key;
		}
	}
}
