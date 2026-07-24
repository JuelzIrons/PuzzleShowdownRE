namespace UnityWebSocketSharp
{
	internal class CloseEventArgs : global::System.EventArgs
	{
		private bool _clean;

		private global::UnityWebSocketSharp.PayloadData _payloadData;

		public ushort Code => _payloadData.Code;

		public string Reason => _payloadData.Reason;

		public bool WasClean => _clean;

		internal CloseEventArgs(global::UnityWebSocketSharp.PayloadData payloadData, bool clean)
		{
			_payloadData = payloadData;
			_clean = clean;
		}
	}
}
