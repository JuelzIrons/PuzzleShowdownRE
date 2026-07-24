namespace Unity.VisualScripting.FullSerializer
{
	public sealed class fsData
	{
		private object _value;

		public static readonly global::Unity.VisualScripting.FullSerializer.fsData True = new global::Unity.VisualScripting.FullSerializer.fsData(boolean: true);

		public static readonly global::Unity.VisualScripting.FullSerializer.fsData False = new global::Unity.VisualScripting.FullSerializer.fsData(boolean: false);

		public static readonly global::Unity.VisualScripting.FullSerializer.fsData Null = new global::Unity.VisualScripting.FullSerializer.fsData();

		public global::Unity.VisualScripting.FullSerializer.fsDataType Type
		{
			get
			{
				if (_value == null)
				{
					return global::Unity.VisualScripting.FullSerializer.fsDataType.Null;
				}
				if (_value is double)
				{
					return global::Unity.VisualScripting.FullSerializer.fsDataType.Double;
				}
				if (_value is long)
				{
					return global::Unity.VisualScripting.FullSerializer.fsDataType.Int64;
				}
				if (_value is bool)
				{
					return global::Unity.VisualScripting.FullSerializer.fsDataType.Boolean;
				}
				if (_value is string)
				{
					return global::Unity.VisualScripting.FullSerializer.fsDataType.String;
				}
				if (_value is global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData>)
				{
					return global::Unity.VisualScripting.FullSerializer.fsDataType.Object;
				}
				if (_value is global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData>)
				{
					return global::Unity.VisualScripting.FullSerializer.fsDataType.Array;
				}
				throw new global::System.InvalidOperationException("unknown JSON data type");
			}
		}

		public bool IsNull => _value == null;

		public bool IsDouble => _value is double;

		public bool IsInt64 => _value is long;

		public bool IsBool => _value is bool;

		public bool IsString => _value is string;

		public bool IsDictionary => _value is global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData>;

		public bool IsList => _value is global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData>;

		[global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
		public double AsDouble => Cast<double>();

		[global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
		public long AsInt64 => Cast<long>();

		[global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
		public bool AsBool => Cast<bool>();

		[global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
		public string AsString => Cast<string>();

		[global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
		public global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> AsDictionary => Cast<global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData>>();

		[global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
		public global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> AsList => Cast<global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData>>();

		public override string ToString()
		{
			return global::Unity.VisualScripting.FullSerializer.fsJsonPrinter.CompressedJson(this);
		}

		public fsData()
		{
			_value = null;
		}

		public fsData(bool boolean)
		{
			_value = boolean;
		}

		public fsData(double f)
		{
			_value = f;
		}

		public fsData(long i)
		{
			_value = i;
		}

		public fsData(string str)
		{
			_value = str;
		}

		public fsData(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> dict)
		{
			_value = dict;
		}

		public fsData(global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> list)
		{
			_value = list;
		}

		public static global::Unity.VisualScripting.FullSerializer.fsData CreateDictionary()
		{
			return new global::Unity.VisualScripting.FullSerializer.fsData(new global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData>(global::Unity.VisualScripting.FullSerializer.fsGlobalConfig.IsCaseSensitive ? global::System.StringComparer.Ordinal : global::System.StringComparer.OrdinalIgnoreCase));
		}

		public static global::Unity.VisualScripting.FullSerializer.fsData CreateList()
		{
			return new global::Unity.VisualScripting.FullSerializer.fsData(new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData>());
		}

		public static global::Unity.VisualScripting.FullSerializer.fsData CreateList(int capacity)
		{
			return new global::Unity.VisualScripting.FullSerializer.fsData(new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData>(capacity));
		}

		internal void BecomeDictionary()
		{
			_value = new global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData>();
		}

		internal global::Unity.VisualScripting.FullSerializer.fsData Clone()
		{
			return new global::Unity.VisualScripting.FullSerializer.fsData
			{
				_value = _value
			};
		}

		private T Cast<T>()
		{
			if (_value is T)
			{
				return (T)_value;
			}
			throw new global::System.InvalidCastException("Unable to cast <" + this?.ToString() + "> (with type = " + _value.GetType()?.ToString() + ") to type " + typeof(T));
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as global::Unity.VisualScripting.FullSerializer.fsData);
		}

		public bool Equals(global::Unity.VisualScripting.FullSerializer.fsData other)
		{
			if (other == null || Type != other.Type)
			{
				return false;
			}
			switch (Type)
			{
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Null:
				return true;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Double:
				if (AsDouble != other.AsDouble)
				{
					return global::System.Math.Abs(AsDouble - other.AsDouble) < double.Epsilon;
				}
				return true;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Int64:
				return AsInt64 == other.AsInt64;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Boolean:
				return AsBool == other.AsBool;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.String:
				return AsString == other.AsString;
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Array:
			{
				global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> asList = AsList;
				global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> asList2 = other.AsList;
				if (asList.Count != asList2.Count)
				{
					return false;
				}
				for (int i = 0; i < asList.Count; i++)
				{
					if (!asList[i].Equals(asList2[i]))
					{
						return false;
					}
				}
				return true;
			}
			case global::Unity.VisualScripting.FullSerializer.fsDataType.Object:
			{
				global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> asDictionary = AsDictionary;
				global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> asDictionary2 = other.AsDictionary;
				if (asDictionary.Count != asDictionary2.Count)
				{
					return false;
				}
				foreach (string key in asDictionary.Keys)
				{
					if (!asDictionary2.ContainsKey(key))
					{
						return false;
					}
					if (!asDictionary[key].Equals(asDictionary2[key]))
					{
						return false;
					}
				}
				return true;
			}
			default:
				throw new global::System.Exception("Unknown data type");
			}
		}

		public static bool operator ==(global::Unity.VisualScripting.FullSerializer.fsData a, global::Unity.VisualScripting.FullSerializer.fsData b)
		{
			if ((object)a == b)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			if (a.IsDouble && b.IsDouble)
			{
				return global::System.Math.Abs(a.AsDouble - b.AsDouble) < double.Epsilon;
			}
			return a.Equals(b);
		}

		public static bool operator !=(global::Unity.VisualScripting.FullSerializer.fsData a, global::Unity.VisualScripting.FullSerializer.fsData b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}
	}
}
