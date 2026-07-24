namespace WebSocketSharp
{
	public class CloseEventArgs : global::System.EventArgs
	{
		private bool _clean;

		private global::WebSocketSharp.PayloadData _payloadData;

		public ushort Code => _payloadData.Code;

		public string Reason => _payloadData.Reason;

		public bool WasClean => _clean;

		internal CloseEventArgs(global::WebSocketSharp.PayloadData payloadData, bool clean)
		{
			_payloadData = payloadData;
			_clean = clean;
		}

		internal CloseEventArgs(ushort code, string reason, bool clean)
		{
			_payloadData = new global::WebSocketSharp.PayloadData(code, reason);
			_clean = clean;
		}
	}
}
