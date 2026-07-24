namespace System.IO.Hashing
{
	public abstract class NonCryptographicHashAlgorithm
	{
		private sealed class CopyToDestinationStream : global::System.IO.Stream
		{
			public override bool CanWrite => true;

			public override bool CanRead => false;

			public override bool CanSeek => false;

			public override long Length
			{
				get
				{
					throw new global::System.NotSupportedException();
				}
			}

			public override long Position
			{
				get
				{
					throw new global::System.NotSupportedException();
				}
				set
				{
					throw new global::System.NotSupportedException();
				}
			}

			public CopyToDestinationStream(global::System.IO.Hashing.NonCryptographicHashAlgorithm hash)
			{
				_003Chash_003EP = hash;
				base._002Ector();
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
				_003Chash_003EP.Append(buffer.AsSpan(offset, count));
			}

			public override void WriteByte(byte value)
			{
				_003Chash_003EP.Append(new global::System.ReadOnlySpan<byte>(new byte[1] { value }));
			}

			public override global::System.Threading.Tasks.Task WriteAsync(byte[] buffer, int offset, int count, global::System.Threading.CancellationToken cancellationToken)
			{
				_003Chash_003EP.Append(buffer.AsSpan(offset, count));
				return global::System.Threading.Tasks.Task.CompletedTask;
			}

			public override void Flush()
			{
			}

			public override global::System.Threading.Tasks.Task FlushAsync(global::System.Threading.CancellationToken cancellationToken)
			{
				return global::System.Threading.Tasks.Task.CompletedTask;
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new global::System.NotSupportedException();
			}

			public override long Seek(long offset, global::System.IO.SeekOrigin origin)
			{
				throw new global::System.NotSupportedException();
			}

			public override void SetLength(long value)
			{
				throw new global::System.NotSupportedException();
			}
		}

		public int HashLengthInBytes { get; }

		protected NonCryptographicHashAlgorithm(int hashLengthInBytes)
		{
			if (hashLengthInBytes < 1)
			{
				throw new global::System.ArgumentOutOfRangeException("hashLengthInBytes");
			}
			HashLengthInBytes = hashLengthInBytes;
		}

		public abstract void Append(global::System.ReadOnlySpan<byte> source);

		public abstract void Reset();

		protected abstract void GetCurrentHashCore(global::System.Span<byte> destination);

		public void Append(byte[] source)
		{
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			Append(new global::System.ReadOnlySpan<byte>(source));
		}

		public void Append(global::System.IO.Stream stream)
		{
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			stream.CopyTo(new global::System.IO.Hashing.NonCryptographicHashAlgorithm.CopyToDestinationStream(this));
		}

		public global::System.Threading.Tasks.Task AppendAsync(global::System.IO.Stream stream, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (stream == null)
			{
				throw new global::System.ArgumentNullException("stream");
			}
			return stream.CopyToAsync(new global::System.IO.Hashing.NonCryptographicHashAlgorithm.CopyToDestinationStream(this), 81920, cancellationToken);
		}

		public byte[] GetCurrentHash()
		{
			byte[] array = new byte[HashLengthInBytes];
			GetCurrentHashCore(array);
			return array;
		}

		public bool TryGetCurrentHash(global::System.Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length < HashLengthInBytes)
			{
				bytesWritten = 0;
				return false;
			}
			GetCurrentHashCore(destination.Slice(0, HashLengthInBytes));
			bytesWritten = HashLengthInBytes;
			return true;
		}

		public int GetCurrentHash(global::System.Span<byte> destination)
		{
			if (destination.Length < HashLengthInBytes)
			{
				ThrowDestinationTooShort();
			}
			GetCurrentHashCore(destination.Slice(0, HashLengthInBytes));
			return HashLengthInBytes;
		}

		public byte[] GetHashAndReset()
		{
			byte[] array = new byte[HashLengthInBytes];
			GetHashAndResetCore(array);
			return array;
		}

		public bool TryGetHashAndReset(global::System.Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length < HashLengthInBytes)
			{
				bytesWritten = 0;
				return false;
			}
			GetHashAndResetCore(destination.Slice(0, HashLengthInBytes));
			bytesWritten = HashLengthInBytes;
			return true;
		}

		public int GetHashAndReset(global::System.Span<byte> destination)
		{
			if (destination.Length < HashLengthInBytes)
			{
				ThrowDestinationTooShort();
			}
			GetHashAndResetCore(destination.Slice(0, HashLengthInBytes));
			return HashLengthInBytes;
		}

		protected virtual void GetHashAndResetCore(global::System.Span<byte> destination)
		{
			GetCurrentHashCore(destination);
			Reset();
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("Use GetCurrentHash() to retrieve the computed hash code.", true)]
		public override int GetHashCode()
		{
			throw new global::System.NotSupportedException(global::System.SR.NotSupported_GetHashCode);
		}

		[global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
		private protected static void ThrowDestinationTooShort()
		{
			throw new global::System.ArgumentException(global::System.SR.Argument_DestinationTooShort, "destination");
		}
	}
}
