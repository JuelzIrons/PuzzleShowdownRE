namespace Newtonsoft.Json
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field, AllowMultiple = false)]
	public class JsonExtensionDataAttribute : global::System.Attribute
	{
		public bool WriteData { get; set; }

		public bool ReadData { get; set; }

		public JsonExtensionDataAttribute()
		{
			WriteData = true;
			ReadData = true;
		}
	}
}
