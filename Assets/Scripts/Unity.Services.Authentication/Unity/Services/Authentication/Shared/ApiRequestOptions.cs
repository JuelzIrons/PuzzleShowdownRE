namespace Unity.Services.Authentication.Shared
{
	internal class ApiRequestOptions
	{
		public global::System.Collections.Generic.Dictionary<string, string> PathParameters { get; set; }

		public global::Unity.Services.Authentication.Shared.Multimap<string, string> QueryParameters { get; set; }

		public global::Unity.Services.Authentication.Shared.Multimap<string, string> HeaderParameters { get; set; }

		public global::System.Collections.Generic.Dictionary<string, string> FormParameters { get; set; }

		public global::Unity.Services.Authentication.Shared.Multimap<string, global::System.IO.Stream> FileParameters { get; set; }

		public string Operation { get; set; }

		public object Data { get; set; }

		public ApiRequestOptions()
		{
			PathParameters = new global::System.Collections.Generic.Dictionary<string, string>();
			QueryParameters = new global::Unity.Services.Authentication.Shared.Multimap<string, string>();
			HeaderParameters = new global::Unity.Services.Authentication.Shared.Multimap<string, string>();
			FormParameters = new global::System.Collections.Generic.Dictionary<string, string>();
			FileParameters = new global::Unity.Services.Authentication.Shared.Multimap<string, global::System.IO.Stream>();
		}
	}
}
