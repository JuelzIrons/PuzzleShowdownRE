namespace Unity.Services.Authentication.Shared
{
	internal class OpenAPIDateConverter : global::Newtonsoft.Json.Converters.IsoDateTimeConverter
	{
		[global::UnityEngine.Scripting.Preserve]
		public OpenAPIDateConverter()
		{
			base.DateTimeFormat = "yyyy-MM-dd";
		}
	}
}
