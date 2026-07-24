namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class PortKeyAttribute : global::System.Attribute
	{
		public string key { get; }

		public PortKeyAttribute(string key)
		{
			global::Unity.VisualScripting.Ensure.That("key").IsNotNull(key);
			this.key = key;
		}
	}
}
