namespace Newtonsoft.Json.Linq
{
	public class JTokenReader : global::Newtonsoft.Json.JsonReader, global::Newtonsoft.Json.IJsonLineInfo
	{
		private readonly global::Newtonsoft.Json.Linq.JToken _root;

		private string? _initialPath;

		private global::Newtonsoft.Json.Linq.JToken? _parent;

		private global::Newtonsoft.Json.Linq.JToken? _current;

		public global::Newtonsoft.Json.Linq.JToken? CurrentToken => _current;

		int global::Newtonsoft.Json.IJsonLineInfo.LineNumber
		{
			get
			{
				if (base.CurrentState == global::Newtonsoft.Json.JsonReader.State.Start)
				{
					return 0;
				}
				return ((global::Newtonsoft.Json.IJsonLineInfo)_current)?.LineNumber ?? 0;
			}
		}

		int global::Newtonsoft.Json.IJsonLineInfo.LinePosition
		{
			get
			{
				if (base.CurrentState == global::Newtonsoft.Json.JsonReader.State.Start)
				{
					return 0;
				}
				return ((global::Newtonsoft.Json.IJsonLineInfo)_current)?.LinePosition ?? 0;
			}
		}

		public override string Path
		{
			get
			{
				string text = base.Path;
				if (_initialPath == null)
				{
					_initialPath = _root.Path;
				}
				if (!global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(_initialPath))
				{
					if (global::Newtonsoft.Json.Utilities.StringUtils.IsNullOrEmpty(text))
					{
						return _initialPath;
					}
					text = ((!global::Newtonsoft.Json.Utilities.StringUtils.StartsWith(text, '[')) ? (_initialPath + "." + text) : (_initialPath + text));
				}
				return text;
			}
		}

		public JTokenReader(global::Newtonsoft.Json.Linq.JToken token)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(token, "token");
			_root = token;
		}

		public JTokenReader(global::Newtonsoft.Json.Linq.JToken token, string initialPath)
			: this(token)
		{
			_initialPath = initialPath;
		}

		public override bool Read()
		{
			if (base.CurrentState != global::Newtonsoft.Json.JsonReader.State.Start)
			{
				if (_current == null)
				{
					return false;
				}
				if (_current is global::Newtonsoft.Json.Linq.JContainer jContainer && _parent != jContainer)
				{
					return ReadInto(jContainer);
				}
				return ReadOver(_current);
			}
			if (_current == _root)
			{
				return false;
			}
			_current = _root;
			SetToken(_current);
			return true;
		}

		private bool ReadOver(global::Newtonsoft.Json.Linq.JToken t)
		{
			if (t == _root)
			{
				return ReadToEnd();
			}
			global::Newtonsoft.Json.Linq.JToken next = t.Next;
			if (next == null || next == t || t == t.Parent.Last)
			{
				if (t.Parent == null)
				{
					return ReadToEnd();
				}
				return SetEnd(t.Parent);
			}
			_current = next;
			SetToken(_current);
			return true;
		}

		private bool ReadToEnd()
		{
			_current = null;
			SetToken(global::Newtonsoft.Json.JsonToken.None);
			return false;
		}

		private global::Newtonsoft.Json.JsonToken? GetEndToken(global::Newtonsoft.Json.Linq.JContainer c)
		{
			return c.Type switch
			{
				global::Newtonsoft.Json.Linq.JTokenType.Object => global::Newtonsoft.Json.JsonToken.EndObject, 
				global::Newtonsoft.Json.Linq.JTokenType.Array => global::Newtonsoft.Json.JsonToken.EndArray, 
				global::Newtonsoft.Json.Linq.JTokenType.Constructor => global::Newtonsoft.Json.JsonToken.EndConstructor, 
				global::Newtonsoft.Json.Linq.JTokenType.Property => null, 
				_ => throw global::Newtonsoft.Json.Utilities.MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", c.Type, "Unexpected JContainer type."), 
			};
		}

		private bool ReadInto(global::Newtonsoft.Json.Linq.JContainer c)
		{
			global::Newtonsoft.Json.Linq.JToken first = c.First;
			if (first == null)
			{
				return SetEnd(c);
			}
			SetToken(first);
			_current = first;
			_parent = c;
			return true;
		}

		private bool SetEnd(global::Newtonsoft.Json.Linq.JContainer c)
		{
			global::Newtonsoft.Json.JsonToken? endToken = GetEndToken(c);
			if (endToken.HasValue)
			{
				SetToken(endToken.GetValueOrDefault());
				_current = c;
				_parent = c;
				return true;
			}
			return ReadOver(c);
		}

		private void SetToken(global::Newtonsoft.Json.Linq.JToken token)
		{
			switch (token.Type)
			{
			case global::Newtonsoft.Json.Linq.JTokenType.Object:
				SetToken(global::Newtonsoft.Json.JsonToken.StartObject);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Array:
				SetToken(global::Newtonsoft.Json.JsonToken.StartArray);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Constructor:
				SetToken(global::Newtonsoft.Json.JsonToken.StartConstructor, ((global::Newtonsoft.Json.Linq.JConstructor)token).Name);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Property:
				SetToken(global::Newtonsoft.Json.JsonToken.PropertyName, ((global::Newtonsoft.Json.Linq.JProperty)token).Name);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Comment:
				SetToken(global::Newtonsoft.Json.JsonToken.Comment, ((global::Newtonsoft.Json.Linq.JValue)token).Value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Integer:
				SetToken(global::Newtonsoft.Json.JsonToken.Integer, ((global::Newtonsoft.Json.Linq.JValue)token).Value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Float:
				SetToken(global::Newtonsoft.Json.JsonToken.Float, ((global::Newtonsoft.Json.Linq.JValue)token).Value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.String:
				SetToken(global::Newtonsoft.Json.JsonToken.String, ((global::Newtonsoft.Json.Linq.JValue)token).Value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Boolean:
				SetToken(global::Newtonsoft.Json.JsonToken.Boolean, ((global::Newtonsoft.Json.Linq.JValue)token).Value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Null:
				SetToken(global::Newtonsoft.Json.JsonToken.Null, ((global::Newtonsoft.Json.Linq.JValue)token).Value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Undefined:
				SetToken(global::Newtonsoft.Json.JsonToken.Undefined, ((global::Newtonsoft.Json.Linq.JValue)token).Value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Date:
			{
				object obj = ((global::Newtonsoft.Json.Linq.JValue)token).Value;
				if (obj is global::System.DateTime value2)
				{
					obj = global::Newtonsoft.Json.Utilities.DateTimeUtils.EnsureDateTime(value2, base.DateTimeZoneHandling);
				}
				SetToken(global::Newtonsoft.Json.JsonToken.Date, obj);
				break;
			}
			case global::Newtonsoft.Json.Linq.JTokenType.Raw:
				SetToken(global::Newtonsoft.Json.JsonToken.Raw, ((global::Newtonsoft.Json.Linq.JValue)token).Value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Bytes:
				SetToken(global::Newtonsoft.Json.JsonToken.Bytes, ((global::Newtonsoft.Json.Linq.JValue)token).Value);
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Guid:
				SetToken(global::Newtonsoft.Json.JsonToken.String, SafeToString(((global::Newtonsoft.Json.Linq.JValue)token).Value));
				break;
			case global::Newtonsoft.Json.Linq.JTokenType.Uri:
			{
				object value = ((global::Newtonsoft.Json.Linq.JValue)token).Value;
				SetToken(global::Newtonsoft.Json.JsonToken.String, (value is global::System.Uri uri) ? uri.OriginalString : SafeToString(value));
				break;
			}
			case global::Newtonsoft.Json.Linq.JTokenType.TimeSpan:
				SetToken(global::Newtonsoft.Json.JsonToken.String, SafeToString(((global::Newtonsoft.Json.Linq.JValue)token).Value));
				break;
			default:
				throw global::Newtonsoft.Json.Utilities.MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", token.Type, "Unexpected JTokenType.");
			}
		}

		private string? SafeToString(object? value)
		{
			return value?.ToString();
		}

		bool global::Newtonsoft.Json.IJsonLineInfo.HasLineInfo()
		{
			if (base.CurrentState == global::Newtonsoft.Json.JsonReader.State.Start)
			{
				return false;
			}
			return ((global::Newtonsoft.Json.IJsonLineInfo)_current)?.HasLineInfo() ?? false;
		}
	}
}
