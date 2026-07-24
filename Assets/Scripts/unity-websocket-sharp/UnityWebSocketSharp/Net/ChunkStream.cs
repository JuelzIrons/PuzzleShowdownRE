namespace UnityWebSocketSharp.Net
{
	internal class ChunkStream
	{
		private int _chunkRead;

		private int _chunkSize;

		private global::System.Collections.Generic.List<global::UnityWebSocketSharp.Net.Chunk> _chunks;

		private int _count;

		private byte[] _endBuffer;

		private bool _gotIt;

		private global::UnityWebSocketSharp.Net.WebHeaderCollection _headers;

		private int _offset;

		private global::System.Text.StringBuilder _saved;

		private bool _sawCr;

		private global::UnityWebSocketSharp.Net.InputChunkState _state;

		private int _trailerState;

		internal int Count => _count;

		internal byte[] EndBuffer => _endBuffer;

		internal int Offset => _offset;

		public global::UnityWebSocketSharp.Net.WebHeaderCollection Headers => _headers;

		public bool WantsMore => _state < global::UnityWebSocketSharp.Net.InputChunkState.End;

		public ChunkStream(global::UnityWebSocketSharp.Net.WebHeaderCollection headers)
		{
			_headers = headers;
			_chunkSize = -1;
			_chunks = new global::System.Collections.Generic.List<global::UnityWebSocketSharp.Net.Chunk>();
			_saved = new global::System.Text.StringBuilder();
		}

		private int read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			int count2 = _chunks.Count;
			for (int i = 0; i < count2; i++)
			{
				global::UnityWebSocketSharp.Net.Chunk chunk = _chunks[i];
				if (chunk == null)
				{
					continue;
				}
				if (chunk.ReadLeft == 0)
				{
					_chunks[i] = null;
					continue;
				}
				num += chunk.Read(buffer, offset + num, count - num);
				if (num == count)
				{
					break;
				}
			}
			return num;
		}

		private global::UnityWebSocketSharp.Net.InputChunkState seekCrLf(byte[] buffer, ref int offset, int length)
		{
			if (!_sawCr)
			{
				if (buffer[offset++] != 13)
				{
					throwProtocolViolation("CR is expected.");
				}
				_sawCr = true;
				if (offset == length)
				{
					return global::UnityWebSocketSharp.Net.InputChunkState.DataEnded;
				}
			}
			if (buffer[offset++] != 10)
			{
				throwProtocolViolation("LF is expected.");
			}
			return global::UnityWebSocketSharp.Net.InputChunkState.None;
		}

		private global::UnityWebSocketSharp.Net.InputChunkState setChunkSize(byte[] buffer, ref int offset, int length)
		{
			byte b = 0;
			while (offset < length)
			{
				b = buffer[offset++];
				if (_sawCr)
				{
					if (b != 10)
					{
						throwProtocolViolation("LF is expected.");
					}
					break;
				}
				switch (b)
				{
				case 13:
					_sawCr = true;
					continue;
				case 10:
					throwProtocolViolation("LF is unexpected.");
					break;
				}
				if (!_gotIt)
				{
					if (b == 32 || b == 59)
					{
						_gotIt = true;
					}
					else
					{
						_saved.Append((char)b);
					}
				}
			}
			if (_saved.Length > 20)
			{
				throwProtocolViolation("The chunk size is too big.");
			}
			if (b != 10)
			{
				return global::UnityWebSocketSharp.Net.InputChunkState.None;
			}
			string s = _saved.ToString();
			try
			{
				_chunkSize = int.Parse(s, global::System.Globalization.NumberStyles.HexNumber);
			}
			catch
			{
				throwProtocolViolation("The chunk size cannot be parsed.");
			}
			_chunkRead = 0;
			if (_chunkSize == 0)
			{
				_trailerState = 2;
				return global::UnityWebSocketSharp.Net.InputChunkState.Trailer;
			}
			return global::UnityWebSocketSharp.Net.InputChunkState.Data;
		}

		private global::UnityWebSocketSharp.Net.InputChunkState setTrailer(byte[] buffer, ref int offset, int length)
		{
			while (offset < length && _trailerState != 4)
			{
				byte b = buffer[offset++];
				_saved.Append((char)b);
				if (_trailerState == 1 || _trailerState == 3)
				{
					if (b != 10)
					{
						throwProtocolViolation("LF is expected.");
					}
					_trailerState++;
					continue;
				}
				switch (b)
				{
				case 13:
					_trailerState++;
					continue;
				case 10:
					throwProtocolViolation("LF is unexpected.");
					break;
				}
				_trailerState = 0;
			}
			int length2 = _saved.Length;
			if (length2 > 4196)
			{
				throwProtocolViolation("The trailer is too long.");
			}
			if (_trailerState < 4)
			{
				return global::UnityWebSocketSharp.Net.InputChunkState.Trailer;
			}
			if (length2 == 2)
			{
				return global::UnityWebSocketSharp.Net.InputChunkState.End;
			}
			_saved.Length = length2 - 2;
			global::System.IO.StringReader stringReader = new global::System.IO.StringReader(_saved.ToString());
			while (true)
			{
				string text = stringReader.ReadLine();
				if (text == null || text.Length == 0)
				{
					break;
				}
				_headers.Add(text);
			}
			return global::UnityWebSocketSharp.Net.InputChunkState.End;
		}

		private static void throwProtocolViolation(string message)
		{
			throw new global::System.Net.WebException(message, null, global::System.Net.WebExceptionStatus.ServerProtocolViolation, null);
		}

		private void write(byte[] buffer, int offset, int length)
		{
			if (_state == global::UnityWebSocketSharp.Net.InputChunkState.End)
			{
				throwProtocolViolation("The chunks were ended.");
			}
			if (_state == global::UnityWebSocketSharp.Net.InputChunkState.None)
			{
				_state = setChunkSize(buffer, ref offset, length);
				if (_state == global::UnityWebSocketSharp.Net.InputChunkState.None)
				{
					return;
				}
				_saved.Length = 0;
				_sawCr = false;
				_gotIt = false;
			}
			if (_state == global::UnityWebSocketSharp.Net.InputChunkState.Data)
			{
				if (offset >= length)
				{
					return;
				}
				_state = writeData(buffer, ref offset, length);
				if (_state == global::UnityWebSocketSharp.Net.InputChunkState.Data)
				{
					return;
				}
			}
			if (_state == global::UnityWebSocketSharp.Net.InputChunkState.DataEnded)
			{
				if (offset >= length)
				{
					return;
				}
				_state = seekCrLf(buffer, ref offset, length);
				if (_state == global::UnityWebSocketSharp.Net.InputChunkState.DataEnded)
				{
					return;
				}
				_sawCr = false;
			}
			if (_state == global::UnityWebSocketSharp.Net.InputChunkState.Trailer)
			{
				if (offset >= length)
				{
					return;
				}
				_state = setTrailer(buffer, ref offset, length);
				if (_state == global::UnityWebSocketSharp.Net.InputChunkState.Trailer)
				{
					return;
				}
				_saved.Length = 0;
			}
			if (_state == global::UnityWebSocketSharp.Net.InputChunkState.End)
			{
				_endBuffer = buffer;
				_offset = offset;
				_count = length - offset;
			}
			else if (offset < length)
			{
				write(buffer, offset, length);
			}
		}

		private global::UnityWebSocketSharp.Net.InputChunkState writeData(byte[] buffer, ref int offset, int length)
		{
			int num = length - offset;
			int num2 = _chunkSize - _chunkRead;
			if (num > num2)
			{
				num = num2;
			}
			byte[] array = new byte[num];
			global::System.Buffer.BlockCopy(buffer, offset, array, 0, num);
			global::UnityWebSocketSharp.Net.Chunk item = new global::UnityWebSocketSharp.Net.Chunk(array);
			_chunks.Add(item);
			offset += num;
			_chunkRead += num;
			if (_chunkRead != _chunkSize)
			{
				return global::UnityWebSocketSharp.Net.InputChunkState.Data;
			}
			return global::UnityWebSocketSharp.Net.InputChunkState.DataEnded;
		}

		internal void ResetChunkStore()
		{
			_chunkRead = 0;
			_chunkSize = -1;
			_chunks.Clear();
		}

		public int Read(byte[] buffer, int offset, int count)
		{
			if (count <= 0)
			{
				return 0;
			}
			return read(buffer, offset, count);
		}

		public void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				write(buffer, offset, offset + count);
			}
		}
	}
}
