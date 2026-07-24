namespace Newtonsoft.Json.Utilities
{
	internal struct StringBuffer
	{
		private char[]? _buffer;

		private int _position;

		public int Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

		public bool IsEmpty => _buffer == null;

		public char[]? InternalBuffer => _buffer;

		public StringBuffer(global::Newtonsoft.Json.IArrayPool<char>? bufferPool, int initalSize)
			: this(global::Newtonsoft.Json.Utilities.BufferUtils.RentBuffer(bufferPool, initalSize))
		{
		}

		private StringBuffer(char[] buffer)
		{
			_buffer = buffer;
			_position = 0;
		}

		public void Append(global::Newtonsoft.Json.IArrayPool<char>? bufferPool, char value)
		{
			if (_position == _buffer.Length)
			{
				EnsureSize(bufferPool, 1);
			}
			_buffer[_position++] = value;
		}

		public void Append(global::Newtonsoft.Json.IArrayPool<char>? bufferPool, char[] buffer, int startIndex, int count)
		{
			if (_position + count >= _buffer.Length)
			{
				EnsureSize(bufferPool, count);
			}
			global::System.Array.Copy(buffer, startIndex, _buffer, _position, count);
			_position += count;
		}

		public void Clear(global::Newtonsoft.Json.IArrayPool<char>? bufferPool)
		{
			if (_buffer != null)
			{
				global::Newtonsoft.Json.Utilities.BufferUtils.ReturnBuffer(bufferPool, _buffer);
				_buffer = null;
			}
			_position = 0;
		}

		private void EnsureSize(global::Newtonsoft.Json.IArrayPool<char>? bufferPool, int appendLength)
		{
			char[] array = global::Newtonsoft.Json.Utilities.BufferUtils.RentBuffer(bufferPool, (_position + appendLength) * 2);
			if (_buffer != null)
			{
				global::System.Array.Copy(_buffer, array, _position);
				global::Newtonsoft.Json.Utilities.BufferUtils.ReturnBuffer(bufferPool, _buffer);
			}
			_buffer = array;
		}

		public override string ToString()
		{
			return ToString(0, _position);
		}

		public string ToString(int start, int length)
		{
			return new string(_buffer, start, length);
		}
	}
}
