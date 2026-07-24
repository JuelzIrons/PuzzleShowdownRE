namespace WebSocketSharp
{
	internal class WebSocketFrame : global::System.Collections.Generic.IEnumerable<byte>, global::System.Collections.IEnumerable
	{
		private byte[] _extPayloadLength;

		private global::WebSocketSharp.Fin _fin;

		private global::WebSocketSharp.Mask _mask;

		private byte[] _maskingKey;

		private global::WebSocketSharp.Opcode _opcode;

		private global::WebSocketSharp.PayloadData _payloadData;

		private byte _payloadLength;

		private global::WebSocketSharp.Rsv _rsv1;

		private global::WebSocketSharp.Rsv _rsv2;

		private global::WebSocketSharp.Rsv _rsv3;

		internal static readonly byte[] EmptyPingBytes;

		internal ulong ExactPayloadLength => (_payloadLength < 126) ? _payloadLength : ((_payloadLength == 126) ? _extPayloadLength.ToUInt16(global::WebSocketSharp.ByteOrder.Big) : _extPayloadLength.ToUInt64(global::WebSocketSharp.ByteOrder.Big));

		internal int ExtendedPayloadLengthWidth => (_payloadLength >= 126) ? ((_payloadLength == 126) ? 2 : 8) : 0;

		public byte[] ExtendedPayloadLength => _extPayloadLength;

		public global::WebSocketSharp.Fin Fin => _fin;

		public bool IsBinary => _opcode == global::WebSocketSharp.Opcode.Binary;

		public bool IsClose => _opcode == global::WebSocketSharp.Opcode.Close;

		public bool IsCompressed => _rsv1 == global::WebSocketSharp.Rsv.On;

		public bool IsContinuation => _opcode == global::WebSocketSharp.Opcode.Cont;

		public bool IsControl => (int)_opcode >= 8;

		public bool IsData => _opcode == global::WebSocketSharp.Opcode.Text || _opcode == global::WebSocketSharp.Opcode.Binary;

		public bool IsFinal => _fin == global::WebSocketSharp.Fin.Final;

		public bool IsFragment => _fin == global::WebSocketSharp.Fin.More || _opcode == global::WebSocketSharp.Opcode.Cont;

		public bool IsMasked => _mask == global::WebSocketSharp.Mask.On;

		public bool IsPing => _opcode == global::WebSocketSharp.Opcode.Ping;

		public bool IsPong => _opcode == global::WebSocketSharp.Opcode.Pong;

		public bool IsText => _opcode == global::WebSocketSharp.Opcode.Text;

		public ulong Length => (ulong)(2L + (long)(_extPayloadLength.Length + _maskingKey.Length)) + _payloadData.Length;

		public global::WebSocketSharp.Mask Mask => _mask;

		public byte[] MaskingKey => _maskingKey;

		public global::WebSocketSharp.Opcode Opcode => _opcode;

		public global::WebSocketSharp.PayloadData PayloadData => _payloadData;

		public byte PayloadLength => _payloadLength;

		public global::WebSocketSharp.Rsv Rsv1 => _rsv1;

		public global::WebSocketSharp.Rsv Rsv2 => _rsv2;

		public global::WebSocketSharp.Rsv Rsv3 => _rsv3;

		static WebSocketFrame()
		{
			EmptyPingBytes = CreatePingFrame(mask: false).ToArray();
		}

		private WebSocketFrame()
		{
		}

		internal WebSocketFrame(global::WebSocketSharp.Opcode opcode, global::WebSocketSharp.PayloadData payloadData, bool mask)
			: this(global::WebSocketSharp.Fin.Final, opcode, payloadData, compressed: false, mask)
		{
		}

		internal WebSocketFrame(global::WebSocketSharp.Fin fin, global::WebSocketSharp.Opcode opcode, byte[] data, bool compressed, bool mask)
			: this(fin, opcode, new global::WebSocketSharp.PayloadData(data), compressed, mask)
		{
		}

		internal WebSocketFrame(global::WebSocketSharp.Fin fin, global::WebSocketSharp.Opcode opcode, global::WebSocketSharp.PayloadData payloadData, bool compressed, bool mask)
		{
			_fin = fin;
			_opcode = opcode;
			_rsv1 = ((opcode.IsData() && compressed) ? global::WebSocketSharp.Rsv.On : global::WebSocketSharp.Rsv.Off);
			_rsv2 = global::WebSocketSharp.Rsv.Off;
			_rsv3 = global::WebSocketSharp.Rsv.Off;
			ulong length = payloadData.Length;
			if (length < 126)
			{
				_payloadLength = (byte)length;
				_extPayloadLength = global::WebSocketSharp.WebSocket.EmptyBytes;
			}
			else if (length < 65536)
			{
				_payloadLength = 126;
				_extPayloadLength = ((ushort)length).InternalToByteArray(global::WebSocketSharp.ByteOrder.Big);
			}
			else
			{
				_payloadLength = 127;
				_extPayloadLength = length.InternalToByteArray(global::WebSocketSharp.ByteOrder.Big);
			}
			if (mask)
			{
				_mask = global::WebSocketSharp.Mask.On;
				_maskingKey = createMaskingKey();
				payloadData.Mask(_maskingKey);
			}
			else
			{
				_mask = global::WebSocketSharp.Mask.Off;
				_maskingKey = global::WebSocketSharp.WebSocket.EmptyBytes;
			}
			_payloadData = payloadData;
		}

		private static byte[] createMaskingKey()
		{
			byte[] array = new byte[4];
			global::WebSocketSharp.WebSocket.RandomNumber.GetBytes(array);
			return array;
		}

		private static string dump(global::WebSocketSharp.WebSocketFrame frame)
		{
			ulong length = frame.Length;
			long num = (long)(length / 4);
			int num2 = (int)(length % 4);
			int num3;
			string arg;
			if (num < 10000)
			{
				num3 = 4;
				arg = "{0,4}";
			}
			else if (num < 65536)
			{
				num3 = 4;
				arg = "{0,4:X}";
			}
			else if (num < 4294967296L)
			{
				num3 = 8;
				arg = "{0,8:X}";
			}
			else
			{
				num3 = 16;
				arg = "{0,16:X}";
			}
			string arg2 = $"{{0,{num3}}}";
			string format = string.Format("\n{0} 01234567 89ABCDEF 01234567 89ABCDEF\n{0}+--------+--------+--------+--------+\\n", arg2);
			string lineFmt = $"{arg}|{{1,8}} {{2,8}} {{3,8}} {{4,8}}|\n";
			string format2 = $"{arg2}+--------+--------+--------+--------+";
			global::System.Text.StringBuilder buff = new global::System.Text.StringBuilder(64);
			global::System.Func<global::System.Action<string, string, string, string>> func = delegate
			{
				long lineCnt = 0L;
				return delegate(string text, string text2, string text3, string text4)
				{
					buff.AppendFormat(lineFmt, ++lineCnt, text, text2, text3, text4);
				};
			};
			global::System.Action<string, string, string, string> action = func();
			byte[] array = frame.ToArray();
			buff.AppendFormat(format, string.Empty);
			for (long num4 = 0L; num4 <= num; num4++)
			{
				long num5 = num4 * 4;
				if (num4 < num)
				{
					action(global::System.Convert.ToString(array[num5], 2).PadLeft(8, '0'), global::System.Convert.ToString(array[num5 + 1], 2).PadLeft(8, '0'), global::System.Convert.ToString(array[num5 + 2], 2).PadLeft(8, '0'), global::System.Convert.ToString(array[num5 + 3], 2).PadLeft(8, '0'));
				}
				else if (num2 > 0)
				{
					action(global::System.Convert.ToString(array[num5], 2).PadLeft(8, '0'), (num2 >= 2) ? global::System.Convert.ToString(array[num5 + 1], 2).PadLeft(8, '0') : string.Empty, (num2 == 3) ? global::System.Convert.ToString(array[num5 + 2], 2).PadLeft(8, '0') : string.Empty, string.Empty);
				}
			}
			buff.AppendFormat(format2, string.Empty);
			return buff.ToString();
		}

		private static string print(global::WebSocketSharp.WebSocketFrame frame)
		{
			byte payloadLength = frame._payloadLength;
			string text = ((payloadLength > 125) ? frame.ExactPayloadLength.ToString() : string.Empty);
			string text2 = global::System.BitConverter.ToString(frame._maskingKey);
			string text3 = ((payloadLength == 0) ? string.Empty : ((payloadLength > 125) ? "---" : ((!frame.IsText || frame.IsFragment || frame.IsMasked || frame.IsCompressed) ? frame._payloadData.ToString() : utf8Decode(frame._payloadData.ApplicationData))));
			string format = "\n                    FIN: {0}\n                   RSV1: {1}\n                   RSV2: {2}\n                   RSV3: {3}\n                 Opcode: {4}\n                   MASK: {5}\n         Payload Length: {6}\nExtended Payload Length: {7}\n            Masking Key: {8}\n           Payload Data: {9}";
			return string.Format(format, frame._fin, frame._rsv1, frame._rsv2, frame._rsv3, frame._opcode, frame._mask, payloadLength, text, text2, text3);
		}

		private static global::WebSocketSharp.WebSocketFrame processHeader(byte[] header)
		{
			if (header.Length != 2)
			{
				string message = "The header part of a frame could not be read.";
				throw new global::WebSocketSharp.WebSocketException(message);
			}
			global::WebSocketSharp.Fin fin = (((header[0] & 0x80) == 128) ? global::WebSocketSharp.Fin.Final : global::WebSocketSharp.Fin.More);
			global::WebSocketSharp.Rsv rsv = (((header[0] & 0x40) == 64) ? global::WebSocketSharp.Rsv.On : global::WebSocketSharp.Rsv.Off);
			global::WebSocketSharp.Rsv rsv2 = (((header[0] & 0x20) == 32) ? global::WebSocketSharp.Rsv.On : global::WebSocketSharp.Rsv.Off);
			global::WebSocketSharp.Rsv rsv3 = (((header[0] & 0x10) == 16) ? global::WebSocketSharp.Rsv.On : global::WebSocketSharp.Rsv.Off);
			byte opcode = (byte)(header[0] & 0xF);
			global::WebSocketSharp.Mask mask = (((header[1] & 0x80) == 128) ? global::WebSocketSharp.Mask.On : global::WebSocketSharp.Mask.Off);
			byte b = (byte)(header[1] & 0x7F);
			if (!opcode.IsSupported())
			{
				string message2 = "A frame has an unsupported opcode.";
				throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.ProtocolError, message2);
			}
			if (!opcode.IsData() && rsv == global::WebSocketSharp.Rsv.On)
			{
				string message3 = "A non data frame is compressed.";
				throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.ProtocolError, message3);
			}
			if (opcode.IsControl())
			{
				if (fin == global::WebSocketSharp.Fin.More)
				{
					string message4 = "A control frame is fragmented.";
					throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.ProtocolError, message4);
				}
				if (b > 125)
				{
					string message5 = "A control frame has too long payload length.";
					throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.ProtocolError, message5);
				}
			}
			global::WebSocketSharp.WebSocketFrame webSocketFrame = new global::WebSocketSharp.WebSocketFrame();
			webSocketFrame._fin = fin;
			webSocketFrame._rsv1 = rsv;
			webSocketFrame._rsv2 = rsv2;
			webSocketFrame._rsv3 = rsv3;
			webSocketFrame._opcode = (global::WebSocketSharp.Opcode)opcode;
			webSocketFrame._mask = mask;
			webSocketFrame._payloadLength = b;
			return webSocketFrame;
		}

		private static global::WebSocketSharp.WebSocketFrame readExtendedPayloadLength(global::System.IO.Stream stream, global::WebSocketSharp.WebSocketFrame frame)
		{
			int extendedPayloadLengthWidth = frame.ExtendedPayloadLengthWidth;
			if (extendedPayloadLengthWidth == 0)
			{
				frame._extPayloadLength = global::WebSocketSharp.WebSocket.EmptyBytes;
				return frame;
			}
			byte[] array = stream.ReadBytes(extendedPayloadLengthWidth);
			if (array.Length != extendedPayloadLengthWidth)
			{
				string message = "The extended payload length of a frame could not be read.";
				throw new global::WebSocketSharp.WebSocketException(message);
			}
			frame._extPayloadLength = array;
			return frame;
		}

		private static void readExtendedPayloadLengthAsync(global::System.IO.Stream stream, global::WebSocketSharp.WebSocketFrame frame, global::System.Action<global::WebSocketSharp.WebSocketFrame> completed, global::System.Action<global::System.Exception> error)
		{
			int len = frame.ExtendedPayloadLengthWidth;
			if (len == 0)
			{
				frame._extPayloadLength = global::WebSocketSharp.WebSocket.EmptyBytes;
				completed(frame);
				return;
			}
			stream.ReadBytesAsync(len, delegate(byte[] bytes)
			{
				if (bytes.Length != len)
				{
					string message = "The extended payload length of a frame could not be read.";
					throw new global::WebSocketSharp.WebSocketException(message);
				}
				frame._extPayloadLength = bytes;
				completed(frame);
			}, error);
		}

		private static global::WebSocketSharp.WebSocketFrame readHeader(global::System.IO.Stream stream)
		{
			return processHeader(stream.ReadBytes(2));
		}

		private static void readHeaderAsync(global::System.IO.Stream stream, global::System.Action<global::WebSocketSharp.WebSocketFrame> completed, global::System.Action<global::System.Exception> error)
		{
			stream.ReadBytesAsync(2, delegate(byte[] bytes)
			{
				completed(processHeader(bytes));
			}, error);
		}

		private static global::WebSocketSharp.WebSocketFrame readMaskingKey(global::System.IO.Stream stream, global::WebSocketSharp.WebSocketFrame frame)
		{
			if (!frame.IsMasked)
			{
				frame._maskingKey = global::WebSocketSharp.WebSocket.EmptyBytes;
				return frame;
			}
			int num = 4;
			byte[] array = stream.ReadBytes(num);
			if (array.Length != num)
			{
				string message = "The masking key of a frame could not be read.";
				throw new global::WebSocketSharp.WebSocketException(message);
			}
			frame._maskingKey = array;
			return frame;
		}

		private static void readMaskingKeyAsync(global::System.IO.Stream stream, global::WebSocketSharp.WebSocketFrame frame, global::System.Action<global::WebSocketSharp.WebSocketFrame> completed, global::System.Action<global::System.Exception> error)
		{
			if (!frame.IsMasked)
			{
				frame._maskingKey = global::WebSocketSharp.WebSocket.EmptyBytes;
				completed(frame);
				return;
			}
			int len = 4;
			stream.ReadBytesAsync(len, delegate(byte[] bytes)
			{
				if (bytes.Length != len)
				{
					string message = "The masking key of a frame could not be read.";
					throw new global::WebSocketSharp.WebSocketException(message);
				}
				frame._maskingKey = bytes;
				completed(frame);
			}, error);
		}

		private static global::WebSocketSharp.WebSocketFrame readPayloadData(global::System.IO.Stream stream, global::WebSocketSharp.WebSocketFrame frame)
		{
			ulong exactPayloadLength = frame.ExactPayloadLength;
			if (exactPayloadLength > global::WebSocketSharp.PayloadData.MaxLength)
			{
				string message = "A frame has too long payload length.";
				throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.TooBig, message);
			}
			if (exactPayloadLength == 0)
			{
				frame._payloadData = global::WebSocketSharp.PayloadData.Empty;
				return frame;
			}
			long num = (long)exactPayloadLength;
			byte[] array = ((frame._payloadLength < 127) ? stream.ReadBytes((int)exactPayloadLength) : stream.ReadBytes(num, 1024));
			if (array.LongLength != num)
			{
				string message2 = "The payload data of a frame could not be read.";
				throw new global::WebSocketSharp.WebSocketException(message2);
			}
			frame._payloadData = new global::WebSocketSharp.PayloadData(array, num);
			return frame;
		}

		private static void readPayloadDataAsync(global::System.IO.Stream stream, global::WebSocketSharp.WebSocketFrame frame, global::System.Action<global::WebSocketSharp.WebSocketFrame> completed, global::System.Action<global::System.Exception> error)
		{
			ulong exactPayloadLength = frame.ExactPayloadLength;
			if (exactPayloadLength > global::WebSocketSharp.PayloadData.MaxLength)
			{
				string message = "A frame has too long payload length.";
				throw new global::WebSocketSharp.WebSocketException(global::WebSocketSharp.CloseStatusCode.TooBig, message);
			}
			if (exactPayloadLength == 0)
			{
				frame._payloadData = global::WebSocketSharp.PayloadData.Empty;
				completed(frame);
				return;
			}
			long len = (long)exactPayloadLength;
			global::System.Action<byte[]> completed2 = delegate(byte[] bytes)
			{
				if (bytes.LongLength != len)
				{
					string message2 = "The payload data of a frame could not be read.";
					throw new global::WebSocketSharp.WebSocketException(message2);
				}
				frame._payloadData = new global::WebSocketSharp.PayloadData(bytes, len);
				completed(frame);
			};
			if (frame._payloadLength < 127)
			{
				stream.ReadBytesAsync((int)exactPayloadLength, completed2, error);
			}
			else
			{
				stream.ReadBytesAsync(len, 1024, completed2, error);
			}
		}

		private static string utf8Decode(byte[] bytes)
		{
			try
			{
				return global::System.Text.Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				return null;
			}
		}

		internal static global::WebSocketSharp.WebSocketFrame CreateCloseFrame(global::WebSocketSharp.PayloadData payloadData, bool mask)
		{
			return new global::WebSocketSharp.WebSocketFrame(global::WebSocketSharp.Fin.Final, global::WebSocketSharp.Opcode.Close, payloadData, compressed: false, mask);
		}

		internal static global::WebSocketSharp.WebSocketFrame CreatePingFrame(bool mask)
		{
			return new global::WebSocketSharp.WebSocketFrame(global::WebSocketSharp.Fin.Final, global::WebSocketSharp.Opcode.Ping, global::WebSocketSharp.PayloadData.Empty, compressed: false, mask);
		}

		internal static global::WebSocketSharp.WebSocketFrame CreatePingFrame(byte[] data, bool mask)
		{
			return new global::WebSocketSharp.WebSocketFrame(global::WebSocketSharp.Fin.Final, global::WebSocketSharp.Opcode.Ping, new global::WebSocketSharp.PayloadData(data), compressed: false, mask);
		}

		internal static global::WebSocketSharp.WebSocketFrame CreatePongFrame(global::WebSocketSharp.PayloadData payloadData, bool mask)
		{
			return new global::WebSocketSharp.WebSocketFrame(global::WebSocketSharp.Fin.Final, global::WebSocketSharp.Opcode.Pong, payloadData, compressed: false, mask);
		}

		internal static global::WebSocketSharp.WebSocketFrame ReadFrame(global::System.IO.Stream stream, bool unmask)
		{
			global::WebSocketSharp.WebSocketFrame webSocketFrame = readHeader(stream);
			readExtendedPayloadLength(stream, webSocketFrame);
			readMaskingKey(stream, webSocketFrame);
			readPayloadData(stream, webSocketFrame);
			if (unmask)
			{
				webSocketFrame.Unmask();
			}
			return webSocketFrame;
		}

		internal static void ReadFrameAsync(global::System.IO.Stream stream, bool unmask, global::System.Action<global::WebSocketSharp.WebSocketFrame> completed, global::System.Action<global::System.Exception> error)
		{
			readHeaderAsync(stream, delegate(global::WebSocketSharp.WebSocketFrame frame)
			{
				readExtendedPayloadLengthAsync(stream, frame, delegate(global::WebSocketSharp.WebSocketFrame frame2)
				{
					readMaskingKeyAsync(stream, frame2, delegate(global::WebSocketSharp.WebSocketFrame frame3)
					{
						readPayloadDataAsync(stream, frame3, delegate(global::WebSocketSharp.WebSocketFrame webSocketFrame)
						{
							if (unmask)
							{
								webSocketFrame.Unmask();
							}
							completed(webSocketFrame);
						}, error);
					}, error);
				}, error);
			}, error);
		}

		internal void Unmask()
		{
			if (_mask != global::WebSocketSharp.Mask.Off)
			{
				_mask = global::WebSocketSharp.Mask.Off;
				_payloadData.Mask(_maskingKey);
				_maskingKey = global::WebSocketSharp.WebSocket.EmptyBytes;
			}
		}

		public global::System.Collections.Generic.IEnumerator<byte> GetEnumerator()
		{
			byte[] array = ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				yield return array[i];
			}
		}

		public void Print(bool dumped)
		{
			global::System.Console.WriteLine(dumped ? dump(this) : print(this));
		}

		public string PrintToString(bool dumped)
		{
			return dumped ? dump(this) : print(this);
		}

		public byte[] ToArray()
		{
			using global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			int fin = (int)_fin;
			fin = (fin << 1) + (int)_rsv1;
			fin = (fin << 1) + (int)_rsv2;
			fin = (fin << 1) + (int)_rsv3;
			fin = (fin << 4) + (int)_opcode;
			fin = (fin << 1) + (int)_mask;
			fin = (fin << 7) + _payloadLength;
			memoryStream.Write(((ushort)fin).InternalToByteArray(global::WebSocketSharp.ByteOrder.Big), 0, 2);
			if (_payloadLength > 125)
			{
				memoryStream.Write(_extPayloadLength, 0, (_payloadLength == 126) ? 2 : 8);
			}
			if (_mask == global::WebSocketSharp.Mask.On)
			{
				memoryStream.Write(_maskingKey, 0, 4);
			}
			if (_payloadLength > 0)
			{
				byte[] array = _payloadData.ToArray();
				if (_payloadLength < 127)
				{
					memoryStream.Write(array, 0, array.Length);
				}
				else
				{
					memoryStream.WriteBytes(array, 1024);
				}
			}
			memoryStream.Close();
			return memoryStream.ToArray();
		}

		public override string ToString()
		{
			return global::System.BitConverter.ToString(ToArray());
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
