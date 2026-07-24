namespace Newtonsoft.Json
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct | global::System.AttributeTargets.Enum | global::System.AttributeTargets.Property | global::System.AttributeTargets.Field | global::System.AttributeTargets.Interface | global::System.AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : global::System.Attribute
	{
		private readonly global::System.Type _converterType;

		public global::System.Type ConverterType => _converterType;

		public object[]? ConverterParameters { get; }

		public JsonConverterAttribute(global::System.Type converterType)
		{
			if (converterType == null)
			{
				throw new global::System.ArgumentNullException("converterType");
			}
			_converterType = converterType;
		}

		public JsonConverterAttribute(global::System.Type converterType, params object[] converterParameters)
			: this(converterType)
		{
			ConverterParameters = converterParameters;
		}
	}
}
