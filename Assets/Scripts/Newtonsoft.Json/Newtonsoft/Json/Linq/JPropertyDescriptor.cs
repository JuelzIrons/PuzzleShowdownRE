namespace Newtonsoft.Json.Linq
{
	public class JPropertyDescriptor : global::System.ComponentModel.PropertyDescriptor
	{
		public override global::System.Type ComponentType => typeof(global::Newtonsoft.Json.Linq.JObject);

		public override bool IsReadOnly => false;

		public override global::System.Type PropertyType => typeof(object);

		protected override int NameHashCode => base.NameHashCode;

		public JPropertyDescriptor(string name)
			: base(name, null)
		{
		}

		private static global::Newtonsoft.Json.Linq.JObject CastInstance(object instance)
		{
			return (global::Newtonsoft.Json.Linq.JObject)instance;
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override object? GetValue(object? component)
		{
			return (component as global::Newtonsoft.Json.Linq.JObject)?[Name];
		}

		public override void ResetValue(object component)
		{
		}

		public override void SetValue(object? component, object? value)
		{
			if (component is global::Newtonsoft.Json.Linq.JObject jObject)
			{
				global::Newtonsoft.Json.Linq.JToken value2 = (value as global::Newtonsoft.Json.Linq.JToken) ?? new global::Newtonsoft.Json.Linq.JValue(value);
				jObject[Name] = value2;
			}
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}
