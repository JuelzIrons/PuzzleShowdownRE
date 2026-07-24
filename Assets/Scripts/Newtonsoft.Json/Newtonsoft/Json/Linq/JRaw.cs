namespace Newtonsoft.Json.Linq
{
	public class JRaw : global::Newtonsoft.Json.Linq.JValue
	{
		public static async global::System.Threading.Tasks.Task<global::Newtonsoft.Json.Linq.JRaw> CreateAsync(global::Newtonsoft.Json.JsonReader reader, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			using global::System.IO.StringWriter sw = new global::System.IO.StringWriter(global::System.Globalization.CultureInfo.InvariantCulture);
			using global::Newtonsoft.Json.JsonTextWriter jsonWriter = new global::Newtonsoft.Json.JsonTextWriter(sw);
			await jsonWriter.WriteTokenSyncReadingAsync(reader, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return new global::Newtonsoft.Json.Linq.JRaw(sw.ToString());
		}

		public JRaw(global::Newtonsoft.Json.Linq.JRaw other)
			: base(other, null)
		{
		}

		internal JRaw(global::Newtonsoft.Json.Linq.JRaw other, global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
			: base(other, settings)
		{
		}

		public JRaw(object? rawJson)
			: base(rawJson, global::Newtonsoft.Json.Linq.JTokenType.Raw)
		{
		}

		public static global::Newtonsoft.Json.Linq.JRaw Create(global::Newtonsoft.Json.JsonReader reader)
		{
			using global::System.IO.StringWriter stringWriter = new global::System.IO.StringWriter(global::System.Globalization.CultureInfo.InvariantCulture);
			using global::Newtonsoft.Json.JsonTextWriter jsonTextWriter = new global::Newtonsoft.Json.JsonTextWriter(stringWriter);
			jsonTextWriter.WriteToken(reader);
			return new global::Newtonsoft.Json.Linq.JRaw(stringWriter.ToString());
		}

		internal override global::Newtonsoft.Json.Linq.JToken CloneToken(global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
		{
			return new global::Newtonsoft.Json.Linq.JRaw(this, settings);
		}
	}
}
