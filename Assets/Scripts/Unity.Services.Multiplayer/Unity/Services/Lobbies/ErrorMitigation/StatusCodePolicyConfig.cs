namespace Unity.Services.Lobbies.ErrorMitigation
{
	internal class StatusCodePolicyConfig
	{
		private global::System.Collections.Generic.IDictionary<long, bool> _statusCodesToHandleDict = new global::System.Collections.Generic.Dictionary<long, bool>
		{
			{ 408L, true },
			{ 429L, true },
			{ 502L, true },
			{ 503L, true },
			{ 504L, true }
		};

		public void HandleStatusCode(long code)
		{
			if (_statusCodesToHandleDict.ContainsKey(code))
			{
				_statusCodesToHandleDict[code] = true;
			}
			else
			{
				_statusCodesToHandleDict.Add(new global::System.Collections.Generic.KeyValuePair<long, bool>(code, value: true));
			}
		}

		public void DontHandleStatusCode(long code)
		{
			if (_statusCodesToHandleDict.ContainsKey(code))
			{
				_statusCodesToHandleDict[code] = false;
			}
			else
			{
				_statusCodesToHandleDict.Add(new global::System.Collections.Generic.KeyValuePair<long, bool>(code, value: false));
			}
		}

		public void Clear()
		{
			_statusCodesToHandleDict.Clear();
		}

		public bool IsHandledStatusCode(long code)
		{
			return _statusCodesToHandleDict.Contains(new global::System.Collections.Generic.KeyValuePair<long, bool>(code, value: true));
		}
	}
}
