namespace Unity.Services.Core.Internal
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = true)]
	public class VisibilityAttribute : global::UnityEngine.PropertyAttribute
	{
		public string PropertyName { get; private set; }

		public object Value { get; private set; }

		public VisibilityAttribute(string propertyName, object value)
		{
			PropertyName = propertyName;
			Value = value;
		}
	}
}
