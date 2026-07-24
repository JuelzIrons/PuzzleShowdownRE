namespace UnityWebSocketSharp
{
	internal class WebSocketFrame : global::System.Collections.Generic.IEnumerable<byte>, global::System.Collections.IEnumerable
	{
		private static readonly int _defaultHeaderLength;

		private static readonly int _defaultMaskingKeyLength;

		private byte[] _extPayloadLength;

		private global::UnityWebSocketSharp.Fin _fin;

		private global::UnityWebSocketSharp.Mask _mask;

		private byte[] _maskingKey;

		private global::UnityWebSocketSharp.Opcode _opcode;

		private global::UnityWebSocketSharp.PayloadData _payloadData;

		private byte _payloadLength;

		private global::UnityWebSocketSharp.Rsv _rsv1;

		private global::UnityWebSocketSharp.Rsv _rsv2;

		private global::UnityWebSocketSharp.Rsv _rsv3;

		internal ulong ExactPayloadLength
		{
			get
			{
				if (_payloadLength >= 126)
				{
					if (_payloadLength != 126)
					{
						return _extPayloadLength.ToUInt64(global::UnityWebSocketSharp.ByteOrder.Big);
					}
					return _extPayloadLength.ToUInt16(global::UnityWebSocketSharp.ByteOrder.Big);
				}
				return _payloadLength;
			}
		}

		internal int ExtendedPayloadLengthWidth
		{
			get
			{
				if (_payloadLength >= 126)
				{
					if (_payloadLength != 126)
					{
						return 8;
					}
					return 2;
				}
				return 0;
			}
		}

		public byte[] ExtendedPayloadLength => _extPayloadLength;

		public global::UnityWebSocketSharp.Fin Fin => _fin;

		public bool IsBinary => _opcode == global::UnityWebSocketSharp.Opcode.Binary;

		public bool IsClose => _opcode == global::UnityWebSocketSharp.Opcode.Close;

		public bool IsCompressed => _rsv1 == global::UnityWebSocketSharp.Rsv.On;

		public bool IsContinuation => _opcode == global::UnityWebSocketSharp.Opcode.Cont;

		public bool IsControl => (int)_opcode >= 8;

		public bool IsData
		{
			get
			{
				if (_opcode != global::UnityWebSocketSharp.Opcode.Text)
				{
					return _opcode == global::UnityWebSocketSharp.Opcode.Binary;
				}
				return true;
			}
		}

		public bool IsFinal => _fin == global::UnityWebSocketSharp.Fin.Final;

		public bool IsFragment
		{
			get
			{
				if (_fin != global::UnityWebSocketSharp.Fin.More)
				{
					return _opcode == global::UnityWebSocketSharp.Opcode.Cont;
				}
				return true;
			}
		}

		public bool IsMasked => _mask == global::UnityWebSocketSharp.Mask.On;

		public bool IsPing => _opcode == global::UnityWebSocketSharp.Opcode.Ping;

		public bool IsPong => _opcode == global::UnityWebSocketSharp.Opcode.Pong;

		public bool IsText => _opcode == global::UnityWebSocketSharp.Opcode.Text;

		public ulong Length => (ulong)(_defaultHeaderLength + _extPayloadLength.Length + _maskingKey.Length) + _payloadData.Length;

		public global::UnityWebSocketSharp.Mask Mask => _mask;

		public byte[] MaskingKey => _maskingKey;

		public global::UnityWebSocketSharp.Opcode Opcode => _opcode;

		public global::UnityWebSocketSharp.PayloadData PayloadData => _payloadData;

		public byte PayloadLength => _payloadLength;

		public global::UnityWebSocketSharp.Rsv Rsv1 => _rsv1;

		public global::UnityWebSocketSharp.Rsv Rsv2 => _rsv2;

		public global::UnityWebSocketSharp.Rsv Rsv3 => _rsv3;

		static WebSocketFrame()
		{
			_defaultHeaderLength = 2;
			_defaultMaskingKeyLength = 4;
		}

		private WebSocketFrame()
		{
		}

		internal WebSocketFrame(global::UnityWebSocketSharp.Fin fin, global::UnityWebSocketSharp.Opcode opcode, byte[] data, bool compressed, bool mask)
			: this(fin, opcode, new global::UnityWebSocketSharp.PayloadData(data), compressed, mask)
		{
		}

		internal WebSocketFrame(global::UnityWebSocketSharp.Fin fin, global::UnityWebSocketSharp.Opcode opcode, global::UnityWebSocketSharp.PayloadData payloadData, bool compressed, bool mask)
		{
			_fin = fin;
			_opcode = opcode;
			_rsv1 = (compressed ? global::UnityWebSocketSharp.Rsv.On : global::UnityWebSocketSharp.Rsv.Off);
			_rsv2 = global::UnityWebSocketSharp.Rsv.Off;
			_rsv3 = global::UnityWebSocketSharp.Rsv.Off;
			ulong length = payloadData.Length;
			if (length < 126)
			{
				_payloadLength = (byte)length;
				_extPayloadLength = global::UnityWebSocketSharp.WebSocket.EmptyBytes;
			}
			else if (length < 65536)
			{
				_payloadLength = 126;
				_extPayloadLength = ((ushort)length).ToByteArray(global::UnityWebSocketSharp.ByteOrder.Big);
			}
			else
			{
				_payloadLength = 127;
				_extPayloadLength = length.ToByteArray(global::UnityWebSocketSharp.ByteOrder.Big);
			}
			if (mask)
			{
				_mask = global::UnityWebSocketSharp.Mask.On;
				_maskingKey = createMaskingKey();
				payloadData.Mask(_maskingKey);
			}
			else
			{
				_mask = global::UnityWebSocketSharp.Mask.Off;
				_maskingKey = global::UnityWebSocketSharp.WebSocket.EmptyBytes;
			}
			_payloadData = payloadData;
		}

		private static byte[] createMaskingKey()
		{
			byte[] array = new byte[_defaultMaskingKeyLength];
			global::UnityWebSocketSharp.WebSocket.RandomNumber.GetBytes(array);
			return array;
		}

		private static global::UnityWebSocketSharp.WebSocketFrame processHeader(byte[] header)
		{
			if (header.Length != _defaultHeaderLength)
			{
				throw new global::UnityWebSocketSharp.WebSocketException("The header part of a frame could not be read.");
			}
			global::UnityWebSocketSharp.Fin fin = (((header[0] & 0x80) == 128) ? global::UnityWebSocketSharp.Fin.Final : global::UnityWebSocketSharp.Fin.More);
			global::UnityWebSocketSharp.Rsv rsv = (((header[0] & 0x40) == 64) ? global::UnityWebSocketSharp.Rsv.On : global::UnityWebSocketSharp.Rsv.Off);
			global::UnityWebSocketSharp.Rsv rsv2 = (((header[0] & 0x20) == 32) ? global::UnityWebSocketSharp.Rsv.On : global::UnityWebSocketSharp.Rsv.Off);
			global::UnityWebSocketSharp.Rsv rsv3 = (((header[0] & 0x10) == 16) ? global::UnityWebSocketSharp.Rsv.On : global::UnityWebSocketSharp.Rsv.Off);
			byte opcode = (byte)(header[0] & 0xF);
			global::UnityWebSocketSharp.Mask mask = (((header[1] & 0x80) == 128) ? global::UnityWebSocketSharp.Mask.On : global::UnityWebSocketSharp.Mask.Off);
			byte payloadLength = (byte)(header[1] & 0x7F);
			if (!opcode.IsSupportedOpcode())
			{
				string message = "The opcode of a frame is not supported.";
				throw new global::UnityWebSocketSharp.WebSocketException(global::UnityWebSocketSharp.CloseStatusCode.UnsupportedData, message);
			}
			return new global::UnityWebSocketSharp.WebSocketFrame
			{
				_fin = fin,
				_rsv1 = rsv,
				_rsv2 = rsv2,
				_rsv3 = rsv3,
				_opcode = (global::UnityWebSocketSharp.Opcode)opcode,
				_mask = mask,
				_payloadLength = payloadLength
			};
		}

		private static global::UnityWebSocketSharp.WebSocketFrame readExtendedPayloadLength(global::System.IO.Stream stream, global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			int extendedPayloadLengthWidth = frame.ExtendedPayloadLengthWidth;
			if (extendedPayloadLengthWidth == 0)
			{
				frame._extPayloadLength = global::UnityWebSocketSharp.WebSocket.EmptyBytes;
				return frame;
			}
			byte[] array = stream.ReadBytes(extendedPayloadLengthWidth);
			if (array.Length != extendedPayloadLengthWidth)
			{
				throw new global::UnityWebSocketSharp.WebSocketException("The extended payload length of a frame could not be read.");
			}
			frame._extPayloadLength = array;
			return frame;
		}

		private static void readExtendedPayloadLengthAsync(global::System.IO.Stream stream, global::UnityWebSocketSharp.WebSocketFrame frame, global::System.Action<global::UnityWebSocketSharp.WebSocketFrame> completed, global::System.Action<global::System.Exception> error)
		{
			int len = frame.ExtendedPayloadLengthWidth;
			if (len == 0)
			{
				frame._extPayloadLength = global::UnityWebSocketSharp.WebSocket.EmptyBytes;
				completed(frame);
				return;
			}
			stream.ReadBytesAsync(len, delegate(byte[] bytes)
			{
				if (bytes.Length != len)
				{
					throw new global::UnityWebSocketSharp.WebSocketException("The extended payload length of a frame could not be read.");
				}
				frame._extPayloadLength = bytes;
				completed(frame);
			}, error);
		}

		private static global::UnityWebSocketSharp.WebSocketFrame readHeader(global::System.IO.Stream stream)
		{
			return processHeader(stream.ReadBytes(_defaultHeaderLength));
		}

		private static void readHeaderAsync(global::System.IO.Stream stream, global::System.Action<global::UnityWebSocketSharp.WebSocketFrame> completed, global::System.Action<global::System.Exception> error)
		{
			stream.ReadBytesAsync(_defaultHeaderLength, delegate(byte[] bytes)
			{
				global::UnityWebSocketSharp.WebSocketFrame obj = processHeader(bytes);
				completed(obj);
			}, error);
		}

		private static global::UnityWebSocketSharp.WebSocketFrame readMaskingKey(global::System.IO.Stream stream, global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			if (!frame.IsMasked)
			{
				frame._maskingKey = global::UnityWebSocketSharp.WebSocket.EmptyBytes;
				return frame;
			}
			byte[] array = stream.ReadBytes(_defaultMaskingKeyLength);
			if (array.Length != _defaultMaskingKeyLength)
			{
				throw new global::UnityWebSocketSharp.WebSocketException("The masking key of a frame could not be read.");
			}
			frame._maskingKey = array;
			return frame;
		}

		private static void readMaskingKeyAsync(global::System.IO.Stream stream, global::UnityWebSocketSharp.WebSocketFrame frame, global::System.Action<global::UnityWebSocketSharp.WebSocketFrame> completed, global::System.Action<global::System.Exception> error)
		{
			if (!frame.IsMasked)
			{
				frame._maskingKey = global::UnityWebSocketSharp.WebSocket.EmptyBytes;
				completed(frame);
				return;
			}
			stream.ReadBytesAsync(_defaultMaskingKeyLength, delegate(byte[] bytes)
			{
				if (bytes.Length != _defaultMaskingKeyLength)
				{
					throw new global::UnityWebSocketSharp.WebSocketException("The masking key of a frame could not be read.");
				}
				frame._maskingKey = bytes;
				completed(frame);
			}, error);
		}

		private static global::UnityWebSocketSharp.WebSocketFrame readPayloadData(global::System.IO.Stream stream, global::UnityWebSocketSharp.WebSocketFrame frame)
		{
			ulong exactPayloadLength = frame.ExactPayloadLength;
			if (exactPayloadLength > global::UnityWebSocketSharp.PayloadData.MaxLength)
			{
				string message = "The payload data of a frame is too big.";
				throw new global::UnityWebSocketSharp.WebSocketException(global::UnityWebSocketSharp.CloseStatusCode.TooBig, message);
			}
			if (exactPayloadLength == 0L)
			{
				frame._payloadData = global::UnityWebSocketSharp.PayloadData.Empty;
				return frame;
			}
			long num = (long)exactPayloadLength;
			byte[] array = ((frame._payloadLength > 126) ? stream.ReadBytes(num, 1024) : stream.ReadBytes((int)num));
			if (array.LongLength != num)
			{
				throw new global::UnityWebSocketSharp.WebSocketException("The payload data of a frame could not be read.");
			}
			frame._payloadData = new global::UnityWebSocketSharp.PayloadData(array, num);
			return frame;
		}

		private static void readPayloadDataAsync(global::System.IO.Stream stream, global::UnityWebSocketSharp.WebSocketFrame frame, global::System.Action<global::UnityWebSocketSharp.WebSocketFrame> completed, global::System.Action<global::System.Exception> error)
		{
			ulong exactPayloadLength = frame.ExactPayloadLength;
			if (exactPayloadLength > global::UnityWebSocketSharp.PayloadData.MaxLength)
			{
				string message = "The payload data of a frame is too big.";
				throw new global::UnityWebSocketSharp.WebSocketException(global::UnityWebSocketSharp.CloseStatusCode.TooBig, message);
			}
			if (exactPayloadLength == 0L)
			{
				frame._payloadData = global::UnityWebSocketSharp.PayloadData.Empty;
				completed(frame);
				return;
			}
			long len = (long)exactPayloadLength;
			global::System.Action<byte[]> completed2 = delegate(byte[] bytes)
			{
				if (bytes.LongLength != len)
				{
					throw new global::UnityWebSocketSharp.WebSocketException("The payload data of a frame could not be read.");
				}
				frame._payloadData = new global::UnityWebSocketSharp.PayloadData(bytes, len);
				completed(frame);
			};
			if (frame._payloadLength > 126)
			{
				stream.ReadBytesAsync(len, 1024, completed2, error);
			}
			else
			{
				stream.ReadBytesAsync((int)len, completed2, error);
			}
		}

		private string toDumpString()
		{
			ulong length = Length;
			long num = (long)(length / 4);
			int num2 = (int)(length % 4);
			string arg;
			string arg2;
			if (num < 10000)
			{
				arg = "{0,4}";
				arg2 = "{0,4}";
			}
			else if (num < 65536)
			{
				arg = "{0,4}";
				arg2 = "{0,4:X}";
			}
			else if (num < 4294967296L)
			{
				arg = "{0,8}";
				arg2 = "{0,8:X}";
			}
			else
			{
				arg = "{0,16}";
				arg2 = "{0,16:X}";
			}
			string format = "{0} 01234567 89ABCDEF 01234567 89ABCDEF\n{0}+--------+--------+--------+--------+\n";
			string format2 = string.Format(format, arg);
			format = "{0}|{{1,8}} {{2,8}} {{3,8}} {{4,8}}|\n";
			string lineFmt = string.Format(format, arg2);
			format = "{0}+--------+--------+--------+--------+";
			string format3 = string.Format(format, arg);
			global::System.Text.StringBuilder buff = new global::System.Text.StringBuilder(64);
			global::System.Action<string, string, string, string> action = ((global::System.Func<global::System.Action<string, string, string, string>>)delegate
			{
				long lineCnt = 0L;
				return delegate(string text, string text2, string text3, string text4)
				{
					buff.AppendFormat(lineFmt, ++lineCnt, text, text2, text3, text4);
				};
			})();
			byte[] array = ToArray();
			buff.AppendFormat(format2, string.Empty);
			for (long num3 = 0L; num3 <= num; num3++)
			{
				long num4 = num3 * 4;
				if (num3 < num)
				{
					string arg3 = global::System.Convert.ToString(array[num4], 2).PadLeft(8, '0');
					string arg4 = global::System.Convert.ToString(array[num4 + 1], 2).PadLeft(8, '0');
					string arg5 = global::System.Convert.ToString(array[num4 + 2], 2).PadLeft(8, '0');
					string arg6 = global::System.Convert.ToString(array[num4 + 3], 2).PadLeft(8, '0');
					action(arg3, arg4, arg5, arg6);
				}
				else if (num2 > 0)
				{
					string arg7 = global::System.Convert.ToString(array[num4], 2).PadLeft(8, '0');
					string arg8 = ((num2 >= 2) ? global::System.Convert.ToString(array[num4 + 1], 2).PadLeft(8, '0') : string.Empty);
					string arg9 = ((num2 == 3) ? global::System.Convert.ToString(array[num4 + 2], 2).PadLeft(8, '0') : string.Empty);
					action(arg7, arg8, arg9, string.Empty);
				}
			}
			buff.AppendFormat(format3, string.Empty);
			return buff.ToString();
		}

		private string toString()
		{
			string text = ((_payloadLength >= 126) ? ExactPayloadLength.ToString() : string.Empty);
			string text2 = ((_mask == global::UnityWebSocketSharp.Mask.On) ? global::System.BitConverter.ToString(_maskingKey) : string.Empty);
			string text3 = ((_payloadLength >= 126) ? "***" : ((_payloadLength > 0) ? _payloadData.ToString() : string.Empty));
			return $"                    FIN: {_fin}\n                   RSV1: {_rsv1}\n                   RSV2: {_rsv2}\n                   RSV3: {_rsv3}\n                 Opcode: {_opcode}\n                   MASK: {_mask}\n         Payload Length: {_payloadLength}\nExtended Payload Length: {text}\n            Masking Key: {text2}\n           Payload Data: {text3}";
		}

		internal static global::UnityWebSocketSharp.WebSocketFrame CreateCloseFrame(global::UnityWebSocketSharp.PayloadData payloadData, bool mask)
		{
			return new global::UnityWebSocketSharp.WebSocketFrame(global::UnityWebSocketSharp.Fin.Final, global::UnityWebSocketSharp.Opcode.Close, payloadData, compressed: false, mask);
		}

		internal static global::UnityWebSocketSharp.WebSocketFrame CreatePingFrame(bool mask)
		{
			return new global::UnityWebSocketSharp.WebSocketFrame(global::UnityWebSocketSharp.Fin.Final, global::UnityWebSocketSharp.Opcode.Ping, global::UnityWebSocketSharp.PayloadData.Empty, compressed: false, mask);
		}

		internal static global::UnityWebSocketSharp.WebSocketFrame CreatePingFrame(byte[] data, bool mask)
		{
			return new global::UnityWebSocketSharp.WebSocketFrame(global::UnityWebSocketSharp.Fin.Final, global::UnityWebSocketSharp.Opcode.Ping, new global::UnityWebSocketSharp.PayloadData(data), compressed: false, mask);
		}

		internal static global::UnityWebSocketSharp.WebSocketFrame CreatePongFrame(global::UnityWebSocketSharp.PayloadData payloadData, bool mask)
		{
			return new global::UnityWebSocketSharp.WebSocketFrame(global::UnityWebSocketSharp.Fin.Final, global::UnityWebSocketSharp.Opcode.Pong, payloadData, compressed: false, mask);
		}

		internal static global::UnityWebSocketSharp.WebSocketFrame ReadFrame(global::System.IO.Stream stream, bool unmask)
		{
			global::UnityWebSocketSharp.WebSocketFrame webSocketFrame = readHeader(stream);
			readExtendedPayloadLength(stream, webSocketFrame);
			readMaskingKey(stream, webSocketFrame);
			readPayloadData(stream, webSocketFrame);
			if (unmask)
			{
				webSocketFrame.Unmask();
			}
			return webSocketFrame;
		}

		internal static void ReadFrameAsync(global::System.IO.Stream stream, bool unmask, global::System.Action<global::UnityWebSocketSharp.WebSocketFrame> completed, global::System.Action<global::System.Exception> error)
		{
			readHeaderAsync(stream, delegate(global::UnityWebSocketSharp.WebSocketFrame frame)
			{
				readExtendedPayloadLengthAsync(stream, frame, delegate(global::UnityWebSocketSharp.WebSocketFrame frame2)
				{
					readMaskingKeyAsync(stream, frame2, delegate(global::UnityWebSocketSharp.WebSocketFrame frame3)
					{
						readPayloadDataAsync(stream, frame3, delegate(global::UnityWebSocketSharp.WebSocketFrame webSocketFrame)
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

		internal string ToString(bool dump)
		{
			if (!dump)
			{
				return toString();
			}
			return toDumpString();
		}

		internal void Unmask()
		{
			if (_mask != global::UnityWebSocketSharp.Mask.Off)
			{
				_payloadData.Mask(_maskingKey);
				_maskingKey = global::UnityWebSocketSharp.WebSocket.EmptyBytes;
				_mask = global::UnityWebSocketSharp.Mask.Off;
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

		public byte[] ToArray()
		{
			using global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			byte[] buffer = ((ushort)(((((((int)((uint)_fin << 1) + (int)_rsv1 << 1) + (int)_rsv2 << 1) + (int)_rsv3 << 4) + (int)_opcode << 1) + (int)_mask << 7) + _payloadLength)).ToByteArray(global::UnityWebSocketSharp.ByteOrder.Big);
			memoryStream.Write(buffer, 0, _defaultHeaderLength);
			if (_payloadLength >= 126)
			{
				memoryStream.Write(_extPayloadLength, 0, _extPayloadLength.Length);
			}
			if (_mask == global::UnityWebSocketSharp.Mask.On)
			{
				memoryStream.Write(_maskingKey, 0, _defaultMaskingKeyLength);
			}
			if (_payloadLength > 0)
			{
				byte[] array = _payloadData.ToArray();
				if (_payloadLength > 126)
				{
					memoryStream.WriteBytes(array, 1024);
				}
				else
				{
					memoryStream.Write(array, 0, array.Length);
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
