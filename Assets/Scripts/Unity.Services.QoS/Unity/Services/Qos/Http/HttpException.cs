namespace Unity.Services.Qos.Http
{
	[global::System.Serializable]
	[global::UnityEngine.Scripting.Preserve]
	internal class HttpException : global::System.Exception
	{
		[global::UnityEngine.Scripting.Preserve]
		public global::Unity.Services.Qos.Http.HttpClientResponse Response;

		[global::UnityEngine.Scripting.Preserve]
		public HttpException()
		{
		}

		[global::UnityEngine.Scripting.Preserve]
		public HttpException(string message)
			: base(message)
		{
		}

		[global::UnityEngine.Scripting.Preserve]
		public HttpException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}

		[global::UnityEngine.Scripting.Preserve]
		public HttpException(global::Unity.Services.Qos.Http.HttpClientResponse response)
			: base($"({response.StatusCode}) {response.ErrorMessage}")
		{
			Response = response;
		}
	}
	[global::System.Serializable]
	[global::UnityEngine.Scripting.Preserve]
	internal class HttpException<T> : global::Unity.Services.Qos.Http.HttpException
	{
		[global::UnityEngine.Scripting.Preserve]
		public T ActualError;

		[global::UnityEngine.Scripting.Preserve]
		public HttpException()
		{
		}

		[global::UnityEngine.Scripting.Preserve]
		public HttpException(string message)
			: base(message)
		{
		}

		[global::UnityEngine.Scripting.Preserve]
		public HttpException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}

		[global::UnityEngine.Scripting.Preserve]
		public HttpException(global::Unity.Services.Qos.Http.HttpClientResponse response, T actualError)
			: base(response)
		{
			ActualError = actualError;
		}
	}
}
