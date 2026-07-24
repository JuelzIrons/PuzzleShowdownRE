namespace Newtonsoft.Json
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Interface, AllowMultiple = false)]
	public abstract class JsonContainerAttribute : global::System.Attribute
	{
		internal bool? _isReference;

		internal bool? _itemIsReference;

		internal global::Newtonsoft.Json.ReferenceLoopHandling? _itemReferenceLoopHandling;

		internal global::Newtonsoft.Json.TypeNameHandling? _itemTypeNameHandling;

		private global::System.Type? _namingStrategyType;

		private object[]? _namingStrategyParameters;

		public string? Id { get; set; }

		public string? Title { get; set; }

		public string? Description { get; set; }

		public global::System.Type? ItemConverterType { get; set; }

		public object[]? ItemConverterParameters { get; set; }

		public global::System.Type? NamingStrategyType
		{
			get
			{
				return _namingStrategyType;
			}
			set
			{
				_namingStrategyType = value;
				NamingStrategyInstance = null;
			}
		}

		public object[]? NamingStrategyParameters
		{
			get
			{
				return _namingStrategyParameters;
			}
			set
			{
				_namingStrategyParameters = value;
				NamingStrategyInstance = null;
			}
		}

		internal global::Newtonsoft.Json.Serialization.NamingStrategy? NamingStrategyInstance { get; set; }

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

		protected JsonContainerAttribute()
		{
		}

		protected JsonContainerAttribute(string id)
		{
			Id = id;
		}
	}
}
