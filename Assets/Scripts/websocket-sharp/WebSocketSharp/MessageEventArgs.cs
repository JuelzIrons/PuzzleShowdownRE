namespace WebSocketSharp
{
	public class MessageEventArgs : global::System.EventArgs
	{
		private string _data;

		private bool _dataSet;

		private global::WebSocketSharp.Opcode _opcode;

		private byte[] _rawData;

		internal global::WebSocketSharp.Opcode Opcode => _opcode;

		public string Data
		{
			get
			{
				setData();
				return _data;
			}
		}

		public bool IsBinary => _opcode == global::WebSocketSharp.Opcode.Binary;

		public bool IsPing => _opcode == global::WebSocketSharp.Opcode.Ping;

		public bool IsText => _opcode == global::WebSocketSharp.Opcode.Text;

		public byte[] RawData
		{
			get
			{
				setData();
				return _rawData;
			}
		}

		internal MessageEventArgs(global::WebSocketSharp.WebSocketFrame frame)
		{
			_opcode = frame.Opcode;
			_rawData = frame.PayloadData.ApplicationData;
		}

		internal MessageEventArgs(global::WebSocketSharp.Opcode opcode, byte[] rawData)
		{
			if ((ulong)rawData.LongLength > global::WebSocketSharp.PayloadData.MaxLength)
			{
				throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.TooBig);
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
			if (_opcode == global::WebSocketSharp.Opcode.Binary)
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
