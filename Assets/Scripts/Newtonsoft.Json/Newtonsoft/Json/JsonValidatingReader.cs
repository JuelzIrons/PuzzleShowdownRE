namespace Newtonsoft.Json
{
	[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonValidatingReader : global::Newtonsoft.Json.JsonReader, global::Newtonsoft.Json.IJsonLineInfo
	{
		private class SchemaScope
		{
			private readonly global::Newtonsoft.Json.Linq.JTokenType _tokenType;

			private readonly global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> _schemas;

			private readonly global::System.Collections.Generic.Dictionary<string, bool> _requiredProperties;

			public string CurrentPropertyName { get; set; }

			public int ArrayItemCount { get; set; }

			public bool IsUniqueArray { get; }

			public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> UniqueArrayItems { get; }

			public global::Newtonsoft.Json.Linq.JTokenWriter CurrentItemWriter { get; set; }

			public global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> Schemas => _schemas;

			public global::System.Collections.Generic.Dictionary<string, bool> RequiredProperties => _requiredProperties;

			public global::Newtonsoft.Json.Linq.JTokenType TokenType => _tokenType;

			public SchemaScope(global::Newtonsoft.Json.Linq.JTokenType tokenType, global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> schemas)
			{
				_tokenType = tokenType;
				_schemas = schemas;
				_requiredProperties = global::System.Linq.Enumerable.ToDictionary(global::System.Linq.Enumerable.Distinct(global::System.Linq.Enumerable.SelectMany(schemas, GetRequiredProperties)), (string p) => p, (string p) => false);
				if (tokenType == global::Newtonsoft.Json.Linq.JTokenType.Array && global::System.Linq.Enumerable.Any(schemas, (global::Newtonsoft.Json.Schema.JsonSchemaModel s) => s.UniqueItems))
				{
					IsUniqueArray = true;
					UniqueArrayItems = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JToken>();
				}
			}

			private global::System.Collections.Generic.IEnumerable<string> GetRequiredProperties(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
			{
				if (schema?.Properties == null)
				{
					return global::System.Linq.Enumerable.Empty<string>();
				}
				return global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(schema.Properties, (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Schema.JsonSchemaModel> p) => p.Value.Required), (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Schema.JsonSchemaModel> p) => p.Key);
			}
		}

		private readonly global::Newtonsoft.Json.JsonReader _reader;

		private readonly global::System.Collections.Generic.Stack<global::Newtonsoft.Json.JsonValidatingReader.SchemaScope> _stack;

		private global::Newtonsoft.Json.Schema.JsonSchema _schema;

		private global::Newtonsoft.Json.Schema.JsonSchemaModel _model;

		private global::Newtonsoft.Json.JsonValidatingReader.SchemaScope _currentScope;

		private static readonly global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> EmptySchemaList = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchemaModel>();

		public override object Value => _reader.Value;

		public override int Depth => _reader.Depth;

		public override string Path => _reader.Path;

		public override char QuoteChar
		{
			get
			{
				return _reader.QuoteChar;
			}
			protected internal set
			{
			}
		}

		public override global::Newtonsoft.Json.JsonToken TokenType => _reader.TokenType;

		public override global::System.Type ValueType => _reader.ValueType;

		private global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> CurrentSchemas => _currentScope.Schemas;

		private global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> CurrentMemberSchemas
		{
			get
			{
				if (_currentScope == null)
				{
					return new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchemaModel>(new global::Newtonsoft.Json.Schema.JsonSchemaModel[1] { _model });
				}
				if (_currentScope.Schemas == null || _currentScope.Schemas.Count == 0)
				{
					return EmptySchemaList;
				}
				switch (_currentScope.TokenType)
				{
				case global::Newtonsoft.Json.Linq.JTokenType.None:
					return _currentScope.Schemas;
				case global::Newtonsoft.Json.Linq.JTokenType.Object:
				{
					if (_currentScope.CurrentPropertyName == null)
					{
						throw new global::Newtonsoft.Json.JsonReaderException("CurrentPropertyName has not been set on scope.");
					}
					global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> list2 = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchemaModel>();
					{
						foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentSchema in CurrentSchemas)
						{
							if (currentSchema.Properties != null && currentSchema.Properties.TryGetValue(_currentScope.CurrentPropertyName, out var value))
							{
								list2.Add(value);
							}
							if (currentSchema.PatternProperties != null)
							{
								foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Schema.JsonSchemaModel> patternProperty in currentSchema.PatternProperties)
								{
									if (global::System.Text.RegularExpressions.Regex.IsMatch(_currentScope.CurrentPropertyName, patternProperty.Key))
									{
										list2.Add(patternProperty.Value);
									}
								}
							}
							if (list2.Count == 0 && currentSchema.AllowAdditionalProperties && currentSchema.AdditionalProperties != null)
							{
								list2.Add(currentSchema.AdditionalProperties);
							}
						}
						return list2;
					}
				}
				case global::Newtonsoft.Json.Linq.JTokenType.Array:
				{
					global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> list = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchemaModel>();
					{
						foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentSchema2 in CurrentSchemas)
						{
							if (!currentSchema2.PositionalItemsValidation)
							{
								if (currentSchema2.Items != null && currentSchema2.Items.Count > 0)
								{
									list.Add(currentSchema2.Items[0]);
								}
								continue;
							}
							if (currentSchema2.Items != null && currentSchema2.Items.Count > 0 && currentSchema2.Items.Count > _currentScope.ArrayItemCount - 1)
							{
								list.Add(currentSchema2.Items[_currentScope.ArrayItemCount - 1]);
							}
							if (currentSchema2.AllowAdditionalItems && currentSchema2.AdditionalItems != null)
							{
								list.Add(currentSchema2.AdditionalItems);
							}
						}
						return list;
					}
				}
				case global::Newtonsoft.Json.Linq.JTokenType.Constructor:
					return EmptySchemaList;
				default:
					throw new global::System.ArgumentOutOfRangeException("TokenType", global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected token type: {0}", global::System.Globalization.CultureInfo.InvariantCulture, _currentScope.TokenType));
				}
			}
		}

		public global::Newtonsoft.Json.Schema.JsonSchema Schema
		{
			get
			{
				return _schema;
			}
			set
			{
				if (TokenType != global::Newtonsoft.Json.JsonToken.None)
				{
					throw new global::System.InvalidOperationException("Cannot change schema while validating JSON.");
				}
				_schema = value;
				_model = null;
			}
		}

		public global::Newtonsoft.Json.JsonReader Reader => _reader;

		int global::Newtonsoft.Json.IJsonLineInfo.LineNumber
		{
			get
			{
				if (!(_reader is global::Newtonsoft.Json.IJsonLineInfo jsonLineInfo))
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		int global::Newtonsoft.Json.IJsonLineInfo.LinePosition
		{
			get
			{
				if (!(_reader is global::Newtonsoft.Json.IJsonLineInfo jsonLineInfo))
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		public event global::Newtonsoft.Json.Schema.ValidationEventHandler ValidationEventHandler;

		private void Push(global::Newtonsoft.Json.JsonValidatingReader.SchemaScope scope)
		{
			_stack.Push(scope);
			_currentScope = scope;
		}

		private global::Newtonsoft.Json.JsonValidatingReader.SchemaScope Pop()
		{
			global::Newtonsoft.Json.JsonValidatingReader.SchemaScope result = _stack.Pop();
			_currentScope = ((_stack.Count != 0) ? _stack.Peek() : null);
			return result;
		}

		private void RaiseError(string message, global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			string message2 = (((global::Newtonsoft.Json.IJsonLineInfo)this).HasLineInfo() ? (message + global::Newtonsoft.Json.Utilities.StringUtils.FormatWith(" Line {0}, position {1}.", global::System.Globalization.CultureInfo.InvariantCulture, ((global::Newtonsoft.Json.IJsonLineInfo)this).LineNumber, ((global::Newtonsoft.Json.IJsonLineInfo)this).LinePosition)) : message);
			OnValidationEvent(new global::Newtonsoft.Json.Schema.JsonSchemaException(message2, null, Path, ((global::Newtonsoft.Json.IJsonLineInfo)this).LineNumber, ((global::Newtonsoft.Json.IJsonLineInfo)this).LinePosition));
		}

		private void OnValidationEvent(global::Newtonsoft.Json.Schema.JsonSchemaException exception)
		{
			global::Newtonsoft.Json.Schema.ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
			if (validationEventHandler != null)
			{
				validationEventHandler(this, new global::Newtonsoft.Json.Schema.ValidationEventArgs(exception));
				return;
			}
			throw exception;
		}

		public JsonValidatingReader(global::Newtonsoft.Json.JsonReader reader)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			_reader = reader;
			_stack = new global::System.Collections.Generic.Stack<global::Newtonsoft.Json.JsonValidatingReader.SchemaScope>();
		}

		public override void Close()
		{
			base.Close();
			if (base.CloseInput)
			{
				_reader?.Close();
			}
		}

		private void ValidateNotDisallowed(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema != null)
			{
				global::Newtonsoft.Json.Schema.JsonSchemaType? currentNodeSchemaType = GetCurrentNodeSchemaType();
				if (currentNodeSchemaType.HasValue && global::Newtonsoft.Json.Schema.JsonSchemaGenerator.HasFlag(schema.Disallow, currentNodeSchemaType.GetValueOrDefault()))
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Type {0} is disallowed.", global::System.Globalization.CultureInfo.InvariantCulture, currentNodeSchemaType), schema);
				}
			}
		}

		private global::Newtonsoft.Json.Schema.JsonSchemaType? GetCurrentNodeSchemaType()
		{
			return _reader.TokenType switch
			{
				global::Newtonsoft.Json.JsonToken.StartObject => global::Newtonsoft.Json.Schema.JsonSchemaType.Object, 
				global::Newtonsoft.Json.JsonToken.StartArray => global::Newtonsoft.Json.Schema.JsonSchemaType.Array, 
				global::Newtonsoft.Json.JsonToken.Integer => global::Newtonsoft.Json.Schema.JsonSchemaType.Integer, 
				global::Newtonsoft.Json.JsonToken.Float => global::Newtonsoft.Json.Schema.JsonSchemaType.Float, 
				global::Newtonsoft.Json.JsonToken.String => global::Newtonsoft.Json.Schema.JsonSchemaType.String, 
				global::Newtonsoft.Json.JsonToken.Boolean => global::Newtonsoft.Json.Schema.JsonSchemaType.Boolean, 
				global::Newtonsoft.Json.JsonToken.Null => global::Newtonsoft.Json.Schema.JsonSchemaType.Null, 
				_ => null, 
			};
		}

		public override int? ReadAsInt32()
		{
			int? result = _reader.ReadAsInt32();
			ValidateCurrentToken();
			return result;
		}

		public override byte[] ReadAsBytes()
		{
			byte[]? result = _reader.ReadAsBytes();
			ValidateCurrentToken();
			return result;
		}

		public override decimal? ReadAsDecimal()
		{
			decimal? result = _reader.ReadAsDecimal();
			ValidateCurrentToken();
			return result;
		}

		public override double? ReadAsDouble()
		{
			double? result = _reader.ReadAsDouble();
			ValidateCurrentToken();
			return result;
		}

		public override bool? ReadAsBoolean()
		{
			bool? result = _reader.ReadAsBoolean();
			ValidateCurrentToken();
			return result;
		}

		public override string ReadAsString()
		{
			string? result = _reader.ReadAsString();
			ValidateCurrentToken();
			return result;
		}

		public override global::System.DateTime? ReadAsDateTime()
		{
			global::System.DateTime? result = _reader.ReadAsDateTime();
			ValidateCurrentToken();
			return result;
		}

		public override global::System.DateTimeOffset? ReadAsDateTimeOffset()
		{
			global::System.DateTimeOffset? result = _reader.ReadAsDateTimeOffset();
			ValidateCurrentToken();
			return result;
		}

		public override bool Read()
		{
			if (!_reader.Read())
			{
				return false;
			}
			if (_reader.TokenType == global::Newtonsoft.Json.JsonToken.Comment)
			{
				return true;
			}
			ValidateCurrentToken();
			return true;
		}

		private void ValidateCurrentToken()
		{
			if (_model == null)
			{
				global::Newtonsoft.Json.Schema.JsonSchemaModelBuilder jsonSchemaModelBuilder = new global::Newtonsoft.Json.Schema.JsonSchemaModelBuilder();
				_model = jsonSchemaModelBuilder.Build(_schema);
				if (!global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsStartToken(_reader.TokenType))
				{
					Push(new global::Newtonsoft.Json.JsonValidatingReader.SchemaScope(global::Newtonsoft.Json.Linq.JTokenType.None, CurrentMemberSchemas));
				}
			}
			switch (_reader.TokenType)
			{
			case global::Newtonsoft.Json.JsonToken.StartObject:
			{
				ProcessValue();
				global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> schemas2 = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(CurrentMemberSchemas, ValidateObject));
				Push(new global::Newtonsoft.Json.JsonValidatingReader.SchemaScope(global::Newtonsoft.Json.Linq.JTokenType.Object, schemas2));
				WriteToken(CurrentSchemas);
				break;
			}
			case global::Newtonsoft.Json.JsonToken.StartArray:
			{
				ProcessValue();
				global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> schemas = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(CurrentMemberSchemas, ValidateArray));
				Push(new global::Newtonsoft.Json.JsonValidatingReader.SchemaScope(global::Newtonsoft.Json.Linq.JTokenType.Array, schemas));
				WriteToken(CurrentSchemas);
				break;
			}
			case global::Newtonsoft.Json.JsonToken.StartConstructor:
				ProcessValue();
				Push(new global::Newtonsoft.Json.JsonValidatingReader.SchemaScope(global::Newtonsoft.Json.Linq.JTokenType.Constructor, null));
				WriteToken(CurrentSchemas);
				break;
			case global::Newtonsoft.Json.JsonToken.PropertyName:
				WriteToken(CurrentSchemas);
				{
					foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentSchema in CurrentSchemas)
					{
						ValidatePropertyName(currentSchema);
					}
					break;
				}
			case global::Newtonsoft.Json.JsonToken.Raw:
				ProcessValue();
				break;
			case global::Newtonsoft.Json.JsonToken.Integer:
				ProcessValue();
				WriteToken(CurrentMemberSchemas);
				{
					foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentMemberSchema in CurrentMemberSchemas)
					{
						ValidateInteger(currentMemberSchema);
					}
					break;
				}
			case global::Newtonsoft.Json.JsonToken.Float:
				ProcessValue();
				WriteToken(CurrentMemberSchemas);
				{
					foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentMemberSchema2 in CurrentMemberSchemas)
					{
						ValidateFloat(currentMemberSchema2);
					}
					break;
				}
			case global::Newtonsoft.Json.JsonToken.String:
				ProcessValue();
				WriteToken(CurrentMemberSchemas);
				{
					foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentMemberSchema3 in CurrentMemberSchemas)
					{
						ValidateString(currentMemberSchema3);
					}
					break;
				}
			case global::Newtonsoft.Json.JsonToken.Boolean:
				ProcessValue();
				WriteToken(CurrentMemberSchemas);
				{
					foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentMemberSchema4 in CurrentMemberSchemas)
					{
						ValidateBoolean(currentMemberSchema4);
					}
					break;
				}
			case global::Newtonsoft.Json.JsonToken.Null:
				ProcessValue();
				WriteToken(CurrentMemberSchemas);
				{
					foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentMemberSchema5 in CurrentMemberSchemas)
					{
						ValidateNull(currentMemberSchema5);
					}
					break;
				}
			case global::Newtonsoft.Json.JsonToken.EndObject:
				WriteToken(CurrentSchemas);
				foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentSchema2 in CurrentSchemas)
				{
					ValidateEndObject(currentSchema2);
				}
				Pop();
				break;
			case global::Newtonsoft.Json.JsonToken.EndArray:
				WriteToken(CurrentSchemas);
				foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentSchema3 in CurrentSchemas)
				{
					ValidateEndArray(currentSchema3);
				}
				Pop();
				break;
			case global::Newtonsoft.Json.JsonToken.EndConstructor:
				WriteToken(CurrentSchemas);
				Pop();
				break;
			case global::Newtonsoft.Json.JsonToken.Undefined:
			case global::Newtonsoft.Json.JsonToken.Date:
			case global::Newtonsoft.Json.JsonToken.Bytes:
				WriteToken(CurrentMemberSchemas);
				break;
			default:
				throw new global::System.ArgumentOutOfRangeException();
			case global::Newtonsoft.Json.JsonToken.None:
				break;
			}
		}

		private void WriteToken(global::System.Collections.Generic.IList<global::Newtonsoft.Json.Schema.JsonSchemaModel> schemas)
		{
			foreach (global::Newtonsoft.Json.JsonValidatingReader.SchemaScope item in _stack)
			{
				bool flag = item.TokenType == global::Newtonsoft.Json.Linq.JTokenType.Array && item.IsUniqueArray && item.ArrayItemCount > 0;
				if (!flag && !global::System.Linq.Enumerable.Any(schemas, (global::Newtonsoft.Json.Schema.JsonSchemaModel s) => s.Enum != null))
				{
					continue;
				}
				if (item.CurrentItemWriter == null)
				{
					if (global::Newtonsoft.Json.Utilities.JsonTokenUtils.IsEndToken(_reader.TokenType))
					{
						continue;
					}
					item.CurrentItemWriter = new global::Newtonsoft.Json.Linq.JTokenWriter();
				}
				item.CurrentItemWriter.WriteToken(_reader, writeChildren: false);
				if (item.CurrentItemWriter.Top != 0 || _reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
				{
					continue;
				}
				global::Newtonsoft.Json.Linq.JToken token = item.CurrentItemWriter.Token;
				item.CurrentItemWriter = null;
				if (flag)
				{
					if (global::System.Linq.Enumerable.Contains<global::Newtonsoft.Json.Linq.JToken>(item.UniqueArrayItems, token, global::Newtonsoft.Json.Linq.JToken.EqualityComparer))
					{
						RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Non-unique array item at index {0}.", global::System.Globalization.CultureInfo.InvariantCulture, item.ArrayItemCount - 1), global::System.Linq.Enumerable.First(item.Schemas, (global::Newtonsoft.Json.Schema.JsonSchemaModel s) => s.UniqueItems));
					}
					item.UniqueArrayItems.Add(token);
				}
				else
				{
					if (!global::System.Linq.Enumerable.Any(schemas, (global::Newtonsoft.Json.Schema.JsonSchemaModel s) => s.Enum != null))
					{
						continue;
					}
					foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel schema in schemas)
					{
						if (schema.Enum != null && !global::Newtonsoft.Json.Utilities.CollectionUtils.ContainsValue(schema.Enum, token, global::Newtonsoft.Json.Linq.JToken.EqualityComparer))
						{
							global::System.IO.StringWriter stringWriter = new global::System.IO.StringWriter(global::System.Globalization.CultureInfo.InvariantCulture);
							token.WriteTo(new global::Newtonsoft.Json.JsonTextWriter(stringWriter));
							RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Value {0} is not defined in enum.", global::System.Globalization.CultureInfo.InvariantCulture, stringWriter.ToString()), schema);
						}
					}
				}
			}
		}

		private void ValidateEndObject(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			global::System.Collections.Generic.Dictionary<string, bool> requiredProperties = _currentScope.RequiredProperties;
			if (requiredProperties != null && global::System.Linq.Enumerable.Any(requiredProperties.Values, (bool v) => !v))
			{
				global::System.Collections.Generic.IEnumerable<string> values = global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(requiredProperties, (global::System.Collections.Generic.KeyValuePair<string, bool> kv) => !kv.Value), (global::System.Collections.Generic.KeyValuePair<string, bool> kv) => kv.Key);
				RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Required properties are missing from object: {0}.", global::System.Globalization.CultureInfo.InvariantCulture, string.Join(", ", values)), schema);
			}
		}

		private void ValidateEndArray(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema != null)
			{
				int arrayItemCount = _currentScope.ArrayItemCount;
				if (schema.MaximumItems.HasValue && arrayItemCount > schema.MaximumItems)
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Array item count {0} exceeds maximum count of {1}.", global::System.Globalization.CultureInfo.InvariantCulture, arrayItemCount, schema.MaximumItems), schema);
				}
				if (schema.MinimumItems.HasValue && arrayItemCount < schema.MinimumItems)
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Array item count {0} is less than minimum count of {1}.", global::System.Globalization.CultureInfo.InvariantCulture, arrayItemCount, schema.MinimumItems), schema);
				}
			}
		}

		private void ValidateNull(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema != null && TestType(schema, global::Newtonsoft.Json.Schema.JsonSchemaType.Null))
			{
				ValidateNotDisallowed(schema);
			}
		}

		private void ValidateBoolean(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema != null && TestType(schema, global::Newtonsoft.Json.Schema.JsonSchemaType.Boolean))
			{
				ValidateNotDisallowed(schema);
			}
		}

		private void ValidateString(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema == null || !TestType(schema, global::Newtonsoft.Json.Schema.JsonSchemaType.String))
			{
				return;
			}
			ValidateNotDisallowed(schema);
			string text = _reader.Value.ToString();
			if (schema.MaximumLength.HasValue && text.Length > schema.MaximumLength)
			{
				RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("String '{0}' exceeds maximum length of {1}.", global::System.Globalization.CultureInfo.InvariantCulture, text, schema.MaximumLength), schema);
			}
			if (schema.MinimumLength.HasValue && text.Length < schema.MinimumLength)
			{
				RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("String '{0}' is less than minimum length of {1}.", global::System.Globalization.CultureInfo.InvariantCulture, text, schema.MinimumLength), schema);
			}
			if (schema.Patterns == null)
			{
				return;
			}
			foreach (string pattern in schema.Patterns)
			{
				if (!global::System.Text.RegularExpressions.Regex.IsMatch(text, pattern))
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("String '{0}' does not match regex pattern '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, text, pattern), schema);
				}
			}
		}

		private void ValidateInteger(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema == null || !TestType(schema, global::Newtonsoft.Json.Schema.JsonSchemaType.Integer))
			{
				return;
			}
			ValidateNotDisallowed(schema);
			object value = _reader.Value;
			if (schema.Maximum.HasValue)
			{
				if (global::Newtonsoft.Json.Linq.JValue.Compare(global::Newtonsoft.Json.Linq.JTokenType.Integer, value, schema.Maximum) > 0)
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Integer {0} exceeds maximum value of {1}.", global::System.Globalization.CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum && global::Newtonsoft.Json.Linq.JValue.Compare(global::Newtonsoft.Json.Linq.JTokenType.Integer, value, schema.Maximum) == 0)
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Integer {0} equals maximum value of {1} and exclusive maximum is true.", global::System.Globalization.CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
			}
			if (schema.Minimum.HasValue)
			{
				if (global::Newtonsoft.Json.Linq.JValue.Compare(global::Newtonsoft.Json.Linq.JTokenType.Integer, value, schema.Minimum) < 0)
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Integer {0} is less than minimum value of {1}.", global::System.Globalization.CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum && global::Newtonsoft.Json.Linq.JValue.Compare(global::Newtonsoft.Json.Linq.JTokenType.Integer, value, schema.Minimum) == 0)
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Integer {0} equals minimum value of {1} and exclusive minimum is true.", global::System.Globalization.CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
			}
			if (schema.DivisibleBy.HasValue && ((!(value is global::System.Numerics.BigInteger bigInteger)) ? (!IsZero((double)global::System.Convert.ToInt64(value, global::System.Globalization.CultureInfo.InvariantCulture) % schema.DivisibleBy.GetValueOrDefault())) : (global::System.Math.Abs(schema.DivisibleBy.Value - global::System.Math.Truncate(schema.DivisibleBy.Value)).Equals(0.0) ? (bigInteger % new global::System.Numerics.BigInteger(schema.DivisibleBy.Value) != 0L) : (bigInteger != 0L))))
			{
				RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Integer {0} is not evenly divisible by {1}.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.JsonConvert.ToString(value), schema.DivisibleBy), schema);
			}
		}

		private void ProcessValue()
		{
			if (_currentScope == null || _currentScope.TokenType != global::Newtonsoft.Json.Linq.JTokenType.Array)
			{
				return;
			}
			_currentScope.ArrayItemCount++;
			foreach (global::Newtonsoft.Json.Schema.JsonSchemaModel currentSchema in CurrentSchemas)
			{
				if (currentSchema != null && currentSchema.PositionalItemsValidation && !currentSchema.AllowAdditionalItems && (currentSchema.Items == null || _currentScope.ArrayItemCount - 1 >= currentSchema.Items.Count))
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Index {0} has not been defined and the schema does not allow additional items.", global::System.Globalization.CultureInfo.InvariantCulture, _currentScope.ArrayItemCount), currentSchema);
				}
			}
		}

		private void ValidateFloat(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema == null || !TestType(schema, global::Newtonsoft.Json.Schema.JsonSchemaType.Float))
			{
				return;
			}
			ValidateNotDisallowed(schema);
			double num = global::System.Convert.ToDouble(_reader.Value, global::System.Globalization.CultureInfo.InvariantCulture);
			if (schema.Maximum.HasValue)
			{
				if (num > schema.Maximum)
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Float {0} exceeds maximum value of {1}.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.JsonConvert.ToString(num), schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum && num == schema.Maximum)
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Float {0} equals maximum value of {1} and exclusive maximum is true.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.JsonConvert.ToString(num), schema.Maximum), schema);
				}
			}
			if (schema.Minimum.HasValue)
			{
				if (num < schema.Minimum)
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Float {0} is less than minimum value of {1}.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.JsonConvert.ToString(num), schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum && num == schema.Minimum)
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Float {0} equals minimum value of {1} and exclusive minimum is true.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.JsonConvert.ToString(num), schema.Minimum), schema);
				}
			}
			if (schema.DivisibleBy.HasValue && !IsZero(FloatingPointRemainder(num, schema.DivisibleBy.GetValueOrDefault())))
			{
				RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Float {0} is not evenly divisible by {1}.", global::System.Globalization.CultureInfo.InvariantCulture, global::Newtonsoft.Json.JsonConvert.ToString(num), schema.DivisibleBy), schema);
			}
		}

		private static double FloatingPointRemainder(double dividend, double divisor)
		{
			return dividend - global::System.Math.Floor(dividend / divisor) * divisor;
		}

		private static bool IsZero(double value)
		{
			return global::System.Math.Abs(value) < 4.440892098500626E-15;
		}

		private void ValidatePropertyName(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema != null)
			{
				string text = global::System.Convert.ToString(_reader.Value, global::System.Globalization.CultureInfo.InvariantCulture);
				if (_currentScope.RequiredProperties.ContainsKey(text))
				{
					_currentScope.RequiredProperties[text] = true;
				}
				if (!schema.AllowAdditionalProperties && !IsPropertyDefinied(schema, text))
				{
					RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Property '{0}' has not been defined and the schema does not allow additional properties.", global::System.Globalization.CultureInfo.InvariantCulture, text), schema);
				}
				_currentScope.CurrentPropertyName = text;
			}
		}

		private bool IsPropertyDefinied(global::Newtonsoft.Json.Schema.JsonSchemaModel schema, string propertyName)
		{
			if (schema.Properties != null && schema.Properties.ContainsKey(propertyName))
			{
				return true;
			}
			if (schema.PatternProperties != null)
			{
				foreach (string key in schema.PatternProperties.Keys)
				{
					if (global::System.Text.RegularExpressions.Regex.IsMatch(propertyName, key))
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool ValidateArray(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return true;
			}
			return TestType(schema, global::Newtonsoft.Json.Schema.JsonSchemaType.Array);
		}

		private bool ValidateObject(global::Newtonsoft.Json.Schema.JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return true;
			}
			return TestType(schema, global::Newtonsoft.Json.Schema.JsonSchemaType.Object);
		}

		private bool TestType(global::Newtonsoft.Json.Schema.JsonSchemaModel currentSchema, global::Newtonsoft.Json.Schema.JsonSchemaType currentType)
		{
			if (!global::Newtonsoft.Json.Schema.JsonSchemaGenerator.HasFlag(currentSchema.Type, currentType))
			{
				RaiseError(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid type. Expected {0} but got {1}.", global::System.Globalization.CultureInfo.InvariantCulture, currentSchema.Type, currentType), currentSchema);
				return false;
			}
			return true;
		}

		bool global::Newtonsoft.Json.IJsonLineInfo.HasLineInfo()
		{
			if (_reader is global::Newtonsoft.Json.IJsonLineInfo jsonLineInfo)
			{
				return jsonLineInfo.HasLineInfo();
			}
			return false;
		}
	}
}
