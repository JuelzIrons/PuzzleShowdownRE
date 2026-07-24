namespace Unity.Services.Core.Networking.Internal
{
	internal static class HttpRequestExtensions
	{
		public static global::Unity.Services.Core.Networking.Internal.HttpRequest AsGet(this global::Unity.Services.Core.Networking.Internal.HttpRequest self)
		{
			return self.SetMethod("GET");
		}

		public static global::Unity.Services.Core.Networking.Internal.HttpRequest AsPost(this global::Unity.Services.Core.Networking.Internal.HttpRequest self)
		{
			return self.SetMethod("POST");
		}

		public static global::Unity.Services.Core.Networking.Internal.HttpRequest AsPut(this global::Unity.Services.Core.Networking.Internal.HttpRequest self)
		{
			return self.SetMethod("PUT");
		}

		public static global::Unity.Services.Core.Networking.Internal.HttpRequest AsDelete(this global::Unity.Services.Core.Networking.Internal.HttpRequest self)
		{
			return self.SetMethod("DELETE");
		}

		public static global::Unity.Services.Core.Networking.Internal.HttpRequest AsPatch(this global::Unity.Services.Core.Networking.Internal.HttpRequest self)
		{
			return self.SetMethod("PATCH");
		}

		public static global::Unity.Services.Core.Networking.Internal.HttpRequest AsHead(this global::Unity.Services.Core.Networking.Internal.HttpRequest self)
		{
			return self.SetMethod("HEAD");
		}

		public static global::Unity.Services.Core.Networking.Internal.HttpRequest AsConnect(this global::Unity.Services.Core.Networking.Internal.HttpRequest self)
		{
			return self.SetMethod("CONNECT");
		}

		public static global::Unity.Services.Core.Networking.Internal.HttpRequest AsOptions(this global::Unity.Services.Core.Networking.Internal.HttpRequest self)
		{
			return self.SetMethod("OPTIONS");
		}

		public static global::Unity.Services.Core.Networking.Internal.HttpRequest AsTrace(this global::Unity.Services.Core.Networking.Internal.HttpRequest self)
		{
			return self.SetMethod("TRACE");
		}
	}
}
