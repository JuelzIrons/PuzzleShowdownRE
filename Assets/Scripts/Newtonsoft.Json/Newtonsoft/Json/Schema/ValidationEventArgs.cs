namespace Newtonsoft.Json.Schema
{
	[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class ValidationEventArgs : global::System.EventArgs
	{
		private readonly global::Newtonsoft.Json.Schema.JsonSchemaException _ex;

		public global::Newtonsoft.Json.Schema.JsonSchemaException Exception => _ex;

		public string Path => _ex.Path;

		public string Message => _ex.Message;

		internal ValidationEventArgs(global::Newtonsoft.Json.Schema.JsonSchemaException ex)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(ex, "ex");
			_ex = ex;
		}
	}
}
