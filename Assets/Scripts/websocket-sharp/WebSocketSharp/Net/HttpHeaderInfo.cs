namespace WebSocketSharp.Net
{
	internal class HttpHeaderInfo
	{
		private string _headerName;

		private global::WebSocketSharp.Net.HttpHeaderType _headerType;

		internal bool IsMultiValueInRequest
		{
			get
			{
				global::WebSocketSharp.Net.HttpHeaderType httpHeaderType = _headerType & global::WebSocketSharp.Net.HttpHeaderType.MultiValueInRequest;
				return httpHeaderType == global::WebSocketSharp.Net.HttpHeaderType.MultiValueInRequest;
			}
		}

		internal bool IsMultiValueInResponse
		{
			get
			{
				global::WebSocketSharp.Net.HttpHeaderType httpHeaderType = _headerType & global::WebSocketSharp.Net.HttpHeaderType.MultiValueInResponse;
				return httpHeaderType == global::WebSocketSharp.Net.HttpHeaderType.MultiValueInResponse;
			}
		}

		public string HeaderName => _headerName;

		public global::WebSocketSharp.Net.HttpHeaderType HeaderType => _headerType;

		public bool IsRequest
		{
			get
			{
				global::WebSocketSharp.Net.HttpHeaderType httpHeaderType = _headerType & global::WebSocketSharp.Net.HttpHeaderType.Request;
				return httpHeaderType == global::WebSocketSharp.Net.HttpHeaderType.Request;
			}
		}

		public bool IsResponse
		{
			get
			{
				global::WebSocketSharp.Net.HttpHeaderType httpHeaderType = _headerType & global::WebSocketSharp.Net.HttpHeaderType.Response;
				return httpHeaderType == global::WebSocketSharp.Net.HttpHeaderType.Response;
			}
		}

		internal HttpHeaderInfo(string headerName, global::WebSocketSharp.Net.HttpHeaderType headerType)
		{
			_headerName = headerName;
			_headerType = headerType;
		}

		public bool IsMultiValue(bool response)
		{
			global::WebSocketSharp.Net.HttpHeaderType httpHeaderType = _headerType & global::WebSocketSharp.Net.HttpHeaderType.MultiValue;
			if (httpHeaderType != global::WebSocketSharp.Net.HttpHeaderType.MultiValue)
			{
				return response ? IsMultiValueInResponse : IsMultiValueInRequest;
			}
			return response ? IsResponse : IsRequest;
		}

		public bool IsRestricted(bool response)
		{
			global::WebSocketSharp.Net.HttpHeaderType httpHeaderType = _headerType & global::WebSocketSharp.Net.HttpHeaderType.Restricted;
			if (httpHeaderType != global::WebSocketSharp.Net.HttpHeaderType.Restricted)
			{
				return false;
			}
			return response ? IsResponse : IsRequest;
		}
	}
}
