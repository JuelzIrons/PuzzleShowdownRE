namespace Newtonsoft.Json
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct | global::System.AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonObjectAttribute : global::Newtonsoft.Json.JsonContainerAttribute
	{
		private global::Newtonsoft.Json.MemberSerialization _memberSerialization;

		internal global::Newtonsoft.Json.MissingMemberHandling? _missingMemberHandling;

		internal global::Newtonsoft.Json.Required? _itemRequired;

		internal global::Newtonsoft.Json.NullValueHandling? _itemNullValueHandling;

		public global::Newtonsoft.Json.MemberSerialization MemberSerialization
		{
			get
			{
				return _memberSerialization;
			}
			set
			{
				_memberSerialization = value;
			}
		}

		public global::Newtonsoft.Json.MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return _missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				_missingMemberHandling = value;
			}
		}

		public global::Newtonsoft.Json.NullValueHandling ItemNullValueHandling
		{
			get
			{
				return _itemNullValueHandling.GetValueOrDefault();
			}
			set
			{
				_itemNullValueHandling = value;
			}
		}

		public global::Newtonsoft.Json.Required ItemRequired
		{
			get
			{
				return _itemRequired.GetValueOrDefault();
			}
			set
			{
				_itemRequired = value;
			}
		}

		public JsonObjectAttribute()
		{
		}

		public JsonObjectAttribute(global::Newtonsoft.Json.MemberSerialization memberSerialization)
		{
			MemberSerialization = memberSerialization;
		}

		public JsonObjectAttribute(string id)
			: base(id)
		{
		}
	}
}
