namespace UnityWebSocketSharp.Net
{
	internal class HttpHeaderInfo
	{
		private string _headerName;

		private global::UnityWebSocketSharp.Net.HttpHeaderType _headerType;

		internal bool IsMultiValueInRequest => (_headerType & global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValueInRequest) == global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValueInRequest;

		internal bool IsMultiValueInResponse => (_headerType & global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValueInResponse) == global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValueInResponse;

		public string HeaderName => _headerName;

		public global::UnityWebSocketSharp.Net.HttpHeaderType HeaderType => _headerType;

		public bool IsRequest => (_headerType & global::UnityWebSocketSharp.Net.HttpHeaderType.Request) == global::UnityWebSocketSharp.Net.HttpHeaderType.Request;

		public bool IsResponse => (_headerType & global::UnityWebSocketSharp.Net.HttpHeaderType.Response) == global::UnityWebSocketSharp.Net.HttpHeaderType.Response;

		internal HttpHeaderInfo(string headerName, global::UnityWebSocketSharp.Net.HttpHeaderType headerType)
		{
			_headerName = headerName;
			_headerType = headerType;
		}

		public bool IsMultiValue(bool response)
		{
			if ((_headerType & global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue) != global::UnityWebSocketSharp.Net.HttpHeaderType.MultiValue)
			{
				if (!response)
				{
					return IsMultiValueInRequest;
				}
				return IsMultiValueInResponse;
			}
			if (!response)
			{
				return IsRequest;
			}
			return IsResponse;
		}

		public bool IsRestricted(bool response)
		{
			if ((_headerType & global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted) != global::UnityWebSocketSharp.Net.HttpHeaderType.Restricted)
			{
				return false;
			}
			if (!response)
			{
				return IsRequest;
			}
			return IsResponse;
		}
	}
}
