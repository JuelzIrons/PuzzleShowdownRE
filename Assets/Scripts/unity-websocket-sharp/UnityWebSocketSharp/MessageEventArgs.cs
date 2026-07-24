namespace UnityWebSocketSharp
{
	internal class MessageEventArgs : global::System.EventArgs
	{
		private string _data;

		private bool _dataSet;

		private global::UnityWebSocketSharp.Opcode _opcode;

		private byte[] _rawData;

		internal global::UnityWebSocketSharp.Opcode Opcode => _opcode;

		public string Data
		{
			get
			{
				setData();
				return _data;
			}
		}

		public bool IsBinary => _opcode == global::UnityWebSocketSharp.Opcode.Binary;

		public bool IsPing => _opcode == global::UnityWebSocketSharp.Opcode.Ping;

		public bool IsText => _opcode == global::UnityWebSocketSharp.Opcode.Text;

		public byte[] RawData
		{
			get
			{
				setData();
				return _rawData;
			}
		}

		internal MessageEventArgs(global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			_opcode = frame.Opcode;
			_rawData = frame.PayloadData.ApplicationData;
		}

		internal MessageEventArgs(global::UnityWebSocketSharp.Opcode opcode, byte[] rawData)
		{
			if ((ulong)rawData.LongLength > global::UnityWebSocketSharp.PayloadData.MaxLength)
			{
				throw new global::UnityWebSocketSharp.WebSocketException(global::UnityWebSocketSharp.CloseStatusCode.TooBig);
			}
			_opcode = opcode;
			_rawData = rawData;
		}

		private void setData()
		{
			if (_dataSet)
			{
				return;
			}
			if (_opcode == global::UnityWebSocketSharp.Opcode.Binary)
			{
				_dataSet = true;
				return;
			}
			if (_rawData.TryGetUTF8DecodedString(out var s))
			{
				_data = s;
			}
			_dataSet = true;
		}
	}
}
