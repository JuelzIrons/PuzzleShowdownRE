namespace Newtonsoft.Json
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field | global::System.AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonPropertyAttribute : global::System.Attribute
	{
		internal global::Newtonsoft.Json.NullValueHandling? _nullValueHandling;

		internal global::Newtonsoft.Json.DefaultValueHandling? _defaultValueHandling;

		internal global::Newtonsoft.Json.ReferenceLoopHandling? _referenceLoopHandling;

		internal global::Newtonsoft.Json.ObjectCreationHandling? _objectCreationHandling;

		internal global::Newtonsoft.Json.TypeNameHandling? _typeNameHandling;

		internal bool? _isReference;

		internal int? _order;

		internal global::Newtonsoft.Json.Required? _required;

		internal bool? _itemIsReference;

		internal global::Newtonsoft.Json.ReferenceLoopHandling? _itemReferenceLoopHandling;

		internal global::Newtonsoft.Json.TypeNameHandling? _itemTypeNameHandling;

		public global::System.Type? ItemConverterType { get; set; }

		public object[]? ItemConverterParameters { get; set; }

		public global::System.Type? NamingStrategyType { get; set; }

		public object[]? NamingStrategyParameters { get; set; }

		public global::Newtonsoft.Json.NullValueHandling NullValueHandling
		{
			get
			{
				return _nullValueHandling.GetValueOrDefault();
			}
			set
			{
				_nullValueHandling = value;
			}
		}

		public global::Newtonsoft.Json.DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return _defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				_defaultValueHandling = value;
			}
		}

		public global::Newtonsoft.Json.ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return _referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				_referenceLoopHandling = value;
			}
		}

		public global::Newtonsoft.Json.ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return _objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				_objectCreationHandling = value;
			}
		}

		public global::Newtonsoft.Json.TypeNameHandling TypeNameHandling
		{
			get
			{
				return _typeNameHandling.GetValueOrDefault();
			}
			set
			{
				_typeNameHandling = value;
			}
		}

		public bool IsReference
		{
			get
			{
				return _isReference == true;
			}
			set
			{
				_isReference = value;
			}
		}

		public int Order
		{
			get
			{
				return _order.GetValueOrDefault();
			}
			set
			{
				_order = value;
			}
		}

		public global::Newtonsoft.Json.Required Required
		{
			get
			{
				return _required.GetValueOrDefault();
			}
			set
			{
				_required = value;
			}
		}

		public string? PropertyName { get; set; }

		public global::Newtonsoft.Json.ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				return _itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				_itemReferenceLoopHandling = value;
			}
		}

		public global::Newtonsoft.Json.TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				return _itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				_itemTypeNameHandling = value;
			}
		}

		public bool ItemIsReference
		{
			get
			{
				return _itemIsReference == true;
			}
			set
			{
				_itemIsReference = value;
			}
		}

		public JsonPropertyAttribute()
		{
		}

		public JsonPropertyAttribute(string propertyName)
		{
			PropertyName = propertyName;
		}
	}
}
