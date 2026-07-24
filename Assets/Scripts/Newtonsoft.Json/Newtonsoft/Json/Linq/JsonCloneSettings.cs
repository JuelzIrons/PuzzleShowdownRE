namespace Newtonsoft.Json.Linq
{
	public class JsonCloneSettings
	{
		internal static readonly global::Newtonsoft.Json.Linq.JsonCloneSettings SkipCopyAnnotations = new global::Newtonsoft.Json.Linq.JsonCloneSettings
		{
			CopyAnnotations = false
		};

		public bool CopyAnnotations { get; set; }

		public JsonCloneSettings()
		{
			CopyAnnotations = true;
		}
	}
}
