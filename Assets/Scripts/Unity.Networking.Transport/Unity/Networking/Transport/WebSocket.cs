namespace Unity.Networking.Transport
{
	internal static class WebSocket
	{
		public enum Opcode
		{
			Continuation = 0,
			TextData = 1,
			BinaryData = 2,
			Close = 8,
			Ping = 9,
			Pong = 10
		}

		public enum StatusCode
		{
			Normal = 1000,
			ProtocolError = 1002,
			UnsupportedDataType = 1003,
			MessageTooBig = 1009,
			InternalError = 1011
		}

		public enum State
		{
			None = 0,
			ClosedAndFlushed = 1,
			Closed = 2,
			Closing = 3,
			Opening = 4,
			Open = 5
		}

		public enum Role
		{
			Server = 0,
			Client = 1
		}

		public struct Keys
		{
			public unsafe fixed uint Key[4];
		}

		public struct Buffer
		{
			public const int Capacity = 2944;

			public unsafe fixed byte Data[2944];

			public int Length;

			public int Available => 2944 - Length;
		}

		public struct Payload
		{
			public const int Capacity = 1458;

			public unsafe fixed byte Data[1458];

			public int Length;

			public int Available => 1458 - Length;
		}

		public struct Settings
		{
			public global::Unity.Collections.FixedString128Bytes Path;

			public int ConnectTimeoutMS;

			public int DisconnectTimeoutMS;

			public int HeartbeatTimeoutMS;
		}

		private struct SHA1
		{
			private unsafe fixed uint words[80];

			private uint h0;

			private uint h1;

			private uint h2;

			private uint h3;

			private uint h4;

			private unsafe void UpdateABCDE(int i, ref uint a, ref uint b, ref uint c, ref uint d, ref uint e, uint f, uint k)
			{
				uint num = ((a << 5) | (a >> 27)) + e + f + k + words[i];
				e = d;
				d = c;
				c = (b << 30) | (b >> 2);
				b = a;
				a = num;
			}

			private unsafe void UpdateHash()
			{
				for (int i = 16; i < 80; i++)
				{
					words[i] = words[i - 3] ^ words[i - 8] ^ words[i - 14] ^ words[i - 16];
					words[i] = (words[i] << 1) | (words[i] >> 31);
				}
				uint a = h0;
				uint b = h1;
				uint c = h2;
				uint d = h3;
				uint e = h4;
				for (int j = 0; j < 20; j++)
				{
					uint f = (b & c) | (~b & d);
					uint k = 1518500249u;
					UpdateABCDE(j, ref a, ref b, ref c, ref d, ref e, f, k);
				}
				for (int l = 20; l < 40; l++)
				{
					uint f2 = b ^ c ^ d;
					uint k2 = 1859775393u;
					UpdateABCDE(l, ref a, ref b, ref c, ref d, ref e, f2, k2);
				}
				for (int m = 40; m < 60; m++)
				{
					uint f3 = (b & c) | (b & d) | (c & d);
					uint k3 = 2400959708u;
					UpdateABCDE(m, ref a, ref b, ref c, ref d, ref e, f3, k3);
				}
				for (int n = 60; n < 80; n++)
				{
					uint f4 = b ^ c ^ d;
					uint k4 = 3395469782u;
					UpdateABCDE(n, ref a, ref b, ref c, ref d, ref e, f4, k4);
				}
				h0 += a;
				h1 += b;
				h2 += c;
				h3 += d;
				h4 += e;
			}

			public unsafe SHA1(in global::Unity.Collections.FixedString512Bytes str)
			{
				h0 = 1732584193u;
				h1 = 4023233417u;
				h2 = 2562383102u;
				h3 = 271733878u;
				h4 = 3285377520u;
				int num = str.Length << 3;
				int num2 = num >> 9;
				byte* ptr = str.GetUnsafePtr();
				for (int i = 0; i < num2; i++)
				{
					for (int j = 0; j < 16; j++)
					{
						words[j] = (uint)((*ptr << 24) | (ptr[1] << 16) | (ptr[2] << 8) | ptr[3]);
						ptr += 4;
					}
					UpdateHash();
				}
				int num3 = num & 0x1FF;
				int num4 = num3 >> 3;
				int num5 = num4 >> 2;
				for (int k = 0; k < num5; k++)
				{
					words[k] = (uint)((*ptr << 24) | (ptr[1] << 16) | (ptr[2] << 8) | ptr[3]);
					ptr += 4;
				}
				switch (num4 & 3)
				{
				case 3:
					words[num5] = (uint)((ulong)((*ptr << 24) | (ptr[1] << 16) | (ptr[2] << 8)) | 0x80uL);
					ptr += 3;
					break;
				case 2:
					words[num5] = (uint)((ulong)((*ptr << 24) | (ptr[1] << 16)) | 0x8000uL);
					ptr += 2;
					break;
				case 1:
					words[num5] = (uint)((ulong)(*ptr << 24) | 0x800000uL);
					ptr++;
					break;
				case 0:
					words[num5] = 2147483648u;
					break;
				}
				num5++;
				if (num3 >= 448)
				{
					for (int l = num5; l < 16; l++)
					{
						words[l] = 0u;
					}
					UpdateHash();
					for (int m = 0; m < 15; m++)
					{
						words[m] = 0u;
					}
					words[15] = (uint)num;
					UpdateHash();
				}
				else
				{
					for (int n = num5; n < 15; n++)
					{
						words[n] = 0u;
					}
					words[15] = (uint)num;
					UpdateHash();
				}
			}

			public global::Unity.Collections.FixedString32Bytes ToBase64()
			{
				global::Unity.Collections.FixedString32Bytes @base = default(global::Unity.Collections.FixedString32Bytes);
				AppendBase64(ref @base, (byte)(h0 >> 24), (byte)(h0 >> 16), (byte)(h0 >> 8));
				AppendBase64(ref @base, (byte)h0, (byte)(h1 >> 24), (byte)(h1 >> 16));
				AppendBase64(ref @base, (byte)(h1 >> 8), (byte)h1, (byte)(h2 >> 24));
				AppendBase64(ref @base, (byte)(h2 >> 16), (byte)(h2 >> 8), (byte)h2);
				AppendBase64(ref @base, (byte)(h3 >> 24), (byte)(h3 >> 16), (byte)(h3 >> 8));
				AppendBase64(ref @base, (byte)h3, (byte)(h4 >> 24), (byte)(h4 >> 16));
				AppendBase64(ref @base, (byte)(h4 >> 8), (byte)h4);
				return @base;
			}
		}

		public const int MaxHeaderSize = 14;

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void Warn(string msg)
		{
			global::UnityEngine.Debug.LogWarning(msg);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void WarnIf(bool condition, string msg)
		{
			if (condition)
			{
				global::UnityEngine.Debug.LogWarning(msg);
			}
		}

		public unsafe static void Connect(ref global::Unity.Networking.Transport.WebSocket.Buffer buffer, ref global::Unity.Networking.Transport.NetworkEndpoint remoteEndpoint, ref global::Unity.Networking.Transport.WebSocket.Keys keys, ref global::Unity.Collections.FixedString128Bytes path)
		{
			global::Unity.Collections.FixedString32Bytes input = "\r\n";
			global::Unity.Collections.FixedString512Bytes fs = "GET ";
			global::Unity.Collections.FixedStringMethods.Append(ref fs, in path);
			global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString512Bytes, global::Unity.Collections.FixedString128Bytes>(ref fs, (global::Unity.Collections.FixedString128Bytes)" HTTP/1.1\r\nUpgrade: websocket\r\nConnection: Upgrade\r\nSec-WebSocket-Version: 13\r\n");
			global::Unity.Collections.FixedString32Bytes input2 = "Sec-WebSocket-Key: ";
			global::Unity.Collections.FixedStringMethods.Append(ref fs, in input2);
			GenerateBase64Key(out input2, ref keys);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, in input2);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, in input);
			global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString512Bytes, global::Unity.Collections.FixedString128Bytes>(ref fs, (global::Unity.Collections.FixedString128Bytes)"Host: ");
			global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString512Bytes, global::Unity.Collections.FixedString512Bytes>(ref fs, remoteEndpoint.ToFixedString512Bytes());
			global::Unity.Collections.FixedStringMethods.Append(ref fs, in input);
			global::Unity.Collections.FixedStringMethods.Append(ref fs, in input);
			fixed (byte* data = buffer.Data)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(data, fs.GetUnsafePtr(), (uint)fs.Length);
			}
			buffer.Length = fs.Length;
		}

		public unsafe static global::Unity.Networking.Transport.WebSocket.State Handshake(ref global::Unity.Networking.Transport.WebSocket.Buffer recvbuffer, ref global::Unity.Networking.Transport.WebSocket.Buffer sendbuffer, bool isClient, ref global::Unity.Networking.Transport.WebSocket.Keys keys, ref global::Unity.Collections.FixedString128Bytes path)
		{
			if (recvbuffer.Length == 0)
			{
				return global::Unity.Networking.Transport.WebSocket.State.Opening;
			}
			bool flag = false;
			int i;
			for (i = 0; i < recvbuffer.Length - 3; i++)
			{
				if (flag)
				{
					break;
				}
				flag = recvbuffer.Data[i] == 13 && recvbuffer.Data[i + 1] == 10 && recvbuffer.Data[i + 2] == 13 && recvbuffer.Data[i + 3] == 10;
			}
			if (!flag)
			{
				return global::Unity.Networking.Transport.WebSocket.State.Opening;
			}
			i += 3;
			fixed (byte* data = recvbuffer.Data)
			{
				byte* ptr = data;
				int num = i;
				int num2 = 0;
				int j;
				for (j = 0; ptr[j] != 13 || ptr[j + 1] != 10; j++)
				{
				}
				int num3 = j;
				global::Unity.Collections.NativeParallelHashMap<global::Unity.Collections.FixedString128Bytes, global::Unity.Collections.FixedString512Bytes> nativeParallelHashMap = new global::Unity.Collections.NativeParallelHashMap<global::Unity.Collections.FixedString128Bytes, global::Unity.Collections.FixedString512Bytes>(8, global::Unity.Collections.Allocator.Temp);
				global::Unity.Collections.FixedString128Bytes b = "connection";
				global::Unity.Collections.FixedString128Bytes b2 = "upgrade";
				global::Unity.Collections.FixedString128Bytes b3 = "sec-websocket-protocol";
				global::Unity.Collections.FixedString128Bytes b4 = "sec-websocket-extensions";
				global::Unity.Collections.FixedString128Bytes b5 = "sec-websocket-accept";
				global::Unity.Collections.FixedString128Bytes b6 = "host";
				global::Unity.Collections.FixedString128Bytes b7 = "sec-websocket-key";
				global::Unity.Collections.FixedString128Bytes b8 = "sec-websocket-version";
				while (true)
				{
					j += 2;
					num2 = j;
					for (; ptr[j] != 13 || ptr[j + 1] != 10; j++)
					{
					}
					if (num2 == j)
					{
						break;
					}
					int k;
					for (k = num2; ptr[k] == 32 || ptr[k] == 9; k++)
					{
					}
					int l = k;
					global::Unity.Collections.FixedString128Bytes a = default(global::Unity.Collections.FixedString128Bytes);
					for (; ptr[l] != 58 && ptr[l] != 32 && ptr[l] != 9 && ptr[l] != 13 && ptr[l] != 10; l++)
					{
						byte value = ptr[l];
						if (value >= 65 && value <= 90)
						{
							value = (byte)(value + 97 - 65);
						}
						a.Add(in value);
					}
					bool flag2 = a == b || a == b2 || a == b3 || a == b4 || a == b5 || a == b6 || a == b7 || a == b8;
					int m;
					for (m = l; ptr[m] != 58 && (ptr[m] == 32 || ptr[m] == 9 || ptr[m] == 13 || ptr[m] == 10); m++)
					{
					}
					if (ptr[m] != 58)
					{
						continue;
					}
					for (m++; ptr[m] == 32 || ptr[m] == 9; m++)
					{
					}
					global::Unity.Collections.FixedString512Bytes item = default(global::Unity.Collections.FixedString512Bytes);
					int num4 = m;
					while (num4 < num && ptr[num4] != 13)
					{
						if (!flag2)
						{
							num4++;
							continue;
						}
						if (item.Length == item.Capacity)
						{
							return global::Unity.Networking.Transport.WebSocket.State.Closed;
						}
						item.Add(in ptr[num4]);
						num4++;
					}
					if (flag2)
					{
						while (item.Length > 0 && (item[item.Length - 1] == 32 || item[item.Length - 1] == 9))
						{
							item.Length--;
						}
						nativeParallelHashMap.TryAdd(a, item);
					}
				}
				global::Unity.Collections.FixedString128Bytes input = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
				global::Unity.Collections.FixedString512Bytes item2;
				bool flag3 = !nativeParallelHashMap.TryGetValue(b, out item2) || item2.Length < 7;
				if (!flag3)
				{
					flag3 = true;
					int n = 0;
					int num5 = 0;
					while ((num5 = item2.Length - n) >= 7)
					{
						flag3 = (item2[n] | 0x20) != 117 || (item2[n + 1] | 0x20) != 112 || (item2[n + 2] | 0x20) != 103 || (item2[n + 3] | 0x20) != 114 || (item2[n + 4] | 0x20) != 97 || (item2[n + 5] | 0x20) != 100 || (item2[n + 6] | 0x20) != 101;
						if (!flag3)
						{
							if (num5 == 7 || item2[n + 7] == 44 || item2[n + 7] == 32 || item2[n + 7] == 9)
							{
								break;
							}
							flag3 = true;
						}
						for (; n < item2.Length && item2[n] != 44; n++)
						{
						}
						for (n++; n < item2.Length && (item2[n] == 32 || item2[n] == 9); n++)
						{
						}
					}
				}
				bool flag4 = !nativeParallelHashMap.TryGetValue(b2, out item2) || item2.Length != 9 || (item2[0] | 0x20) != 119 || (item2[1] | 0x20) != 101 || (item2[2] | 0x20) != 98 || (item2[3] | 0x20) != 115 || (item2[4] | 0x20) != 111 || (item2[5] | 0x20) != 99 || (item2[6] | 0x20) != 107 || (item2[7] | 0x20) != 101 || (item2[8] | 0x20) != 116;
				if (isClient)
				{
					if (num3 < 14 || *ptr != 72 || ptr[1] != 84 || ptr[2] != 84 || ptr[3] != 80 || ptr[4] != 47 || ptr[5] != 49 || ptr[6] != 46 || ptr[7] != 49 || ptr[8] != 32 || ptr[9] != 49 || ptr[10] != 48 || ptr[11] != 49 || ptr[12] != 32)
					{
						return global::Unity.Networking.Transport.WebSocket.State.Closed;
					}
					if (flag3 || flag4 || nativeParallelHashMap.ContainsKey(b3) || nativeParallelHashMap.ContainsKey(b4) || !nativeParallelHashMap.TryGetValue(b5, out var a2))
					{
						return global::Unity.Networking.Transport.WebSocket.State.Closed;
					}
					global::Unity.Collections.FixedString512Bytes fs = default(global::Unity.Collections.FixedString512Bytes);
					GenerateBase64Key(out var key, ref keys);
					global::Unity.Collections.FixedStringMethods.Append(ref fs, in key);
					global::Unity.Collections.FixedStringMethods.Append(ref fs, in input);
					if (a2 != ILSpyHelper_AsRefReadOnly(new global::Unity.Networking.Transport.WebSocket.SHA1(in fs).ToBase64()))
					{
						return global::Unity.Networking.Transport.WebSocket.State.Closed;
					}
				}
				else
				{
					bool num6 = num3 < 14 || *ptr != 71 || ptr[1] != 69 || ptr[2] != 84 || ptr[3] != 32 || ptr[num3 - 9] != 32 || ptr[num3 - 8] != 72 || ptr[num3 - 7] != 84 || ptr[num3 - 6] != 84 || ptr[num3 - 5] != 80 || ptr[num3 - 4] != 47 || ptr[num3 - 3] != 49 || ptr[num3 - 2] != 46 || ptr[num3 - 1] != 49;
					bool flag5 = !nativeParallelHashMap.TryGetValue(b8, out item2) || item2.Length != 2 || item2[0] != 49 || item2[1] != 51;
					global::Unity.Collections.FixedString512Bytes item3;
					bool flag6 = !nativeParallelHashMap.TryGetValue(b7, out item3) || item3.Length != 24;
					global::Unity.Collections.FixedString512Bytes fixedString512Bytes;
					if (num6 || !nativeParallelHashMap.ContainsKey(b6) || flag6 || flag5 || flag3 || flag4)
					{
						fixedString512Bytes = "HTTP/1.1 400 Bad Request\r\nSec-WebSocket-Version: 13\r\n\r\n";
						if (sendbuffer.Available >= fixedString512Bytes.Length)
						{
							fixed (byte* data2 = sendbuffer.Data)
							{
								global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(data2 + sendbuffer.Length, fixedString512Bytes.GetUnsafePtr(), fixedString512Bytes.Length);
							}
							sendbuffer.Length += fixedString512Bytes.Length;
						}
						return global::Unity.Networking.Transport.WebSocket.State.Closed;
					}
					if (num3 != 13 + path.Length || global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr + 4, path.GetUnsafePtr(), path.Length) != 0)
					{
						fixedString512Bytes = "HTTP/1.1 404 Not Found\r\n\r\n";
						if (sendbuffer.Available >= fixedString512Bytes.Length)
						{
							fixed (byte* data2 = sendbuffer.Data)
							{
								global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(data2 + sendbuffer.Length, fixedString512Bytes.GetUnsafePtr(), fixedString512Bytes.Length);
							}
							sendbuffer.Length += fixedString512Bytes.Length;
						}
						return global::Unity.Networking.Transport.WebSocket.State.Closed;
					}
					fixedString512Bytes = "HTTP/1.1 101 Switching Protocols\r\nUpgrade: websocket\r\nConnection: Upgrade\r\n";
					global::Unity.Collections.FixedStringMethods.Append(ref item3, in input);
					global::Unity.Networking.Transport.WebSocket.SHA1 sHA = new global::Unity.Networking.Transport.WebSocket.SHA1((global::Unity.Collections.FixedString512Bytes)new global::Unity.Collections.FixedString128Bytes(in item3));
					global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString512Bytes, global::Unity.Collections.FixedString128Bytes>(ref fixedString512Bytes, (global::Unity.Collections.FixedString128Bytes)"Sec-WebSocket-Accept: ");
					global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString512Bytes, global::Unity.Collections.FixedString32Bytes>(ref fixedString512Bytes, sHA.ToBase64());
					global::Unity.Collections.FixedString32Bytes input2 = "\r\n";
					global::Unity.Collections.FixedStringMethods.Append(ref fixedString512Bytes, in input2);
					global::Unity.Collections.FixedStringMethods.Append(ref fixedString512Bytes, in input2);
					if (sendbuffer.Available < fixedString512Bytes.Length)
					{
						return global::Unity.Networking.Transport.WebSocket.State.Closed;
					}
					fixed (byte* data2 = sendbuffer.Data)
					{
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(data2 + sendbuffer.Length, fixedString512Bytes.GetUnsafePtr(), fixedString512Bytes.Length);
					}
					sendbuffer.Length += fixedString512Bytes.Length;
				}
				int num7 = recvbuffer.Length - i;
				if (num7 > 0)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(data, data + i, num7);
				}
				recvbuffer.Length = num7;
			}
			return global::Unity.Networking.Transport.WebSocket.State.Open;
			static ref readonly T ILSpyHelper_AsRefReadOnly<T>(in T temp)
			{
				//ILSpy generated this function to help ensure overload resolution can pick the overload using 'in'
				return ref temp;
			}
		}

		public unsafe static bool Close(ref global::Unity.Networking.Transport.WebSocket.Buffer buffer, global::Unity.Networking.Transport.WebSocket.StatusCode status, bool useMask, uint mask)
		{
			int length = buffer.Length;
			ushort num = (ushort)(global::System.BitConverter.IsLittleEndian ? ((((ushort)status & 0xFF) << 8) | (((ushort)status >> 8) & 0xFF)) : ((ushort)status));
			if (Binary(ref buffer, &num, 2, useMask, mask))
			{
				buffer.Data[length] = 136;
				return true;
			}
			return false;
		}

		public unsafe static bool Ping(ref global::Unity.Networking.Transport.WebSocket.Buffer buffer, bool useMask, uint mask)
		{
			int length = buffer.Length;
			if (Binary(ref buffer, null, 0, useMask, mask))
			{
				buffer.Data[length] = 137;
				return true;
			}
			return false;
		}

		public unsafe static bool Pong(ref global::Unity.Networking.Transport.WebSocket.Buffer buffer, byte* payload, int payloadSize, bool useMask, uint mask)
		{
			int length = buffer.Length;
			if (Binary(ref buffer, payload, payloadSize, useMask, mask))
			{
				buffer.Data[length] = 138;
				return true;
			}
			return false;
		}

		public unsafe static bool Binary(ref global::Unity.Networking.Transport.PacketProcessor packet, bool useMask, uint mask)
		{
			byte* unsafePayloadPtr = (byte*)packet.GetUnsafePayloadPtr();
			int offset = packet.Offset;
			int capacity = packet.Capacity;
			int length = packet.Length;
			int num = Header(unsafePayloadPtr, offset, capacity, length, useMask, mask);
			if (num > 0)
			{
				unsafePayloadPtr += offset;
				offset -= num;
				length += num;
				if (useMask)
				{
					byte* ptr = unsafePayloadPtr - 4;
					for (int i = 0; i < length; i++)
					{
						byte* num2 = unsafePayloadPtr + i;
						*num2 ^= ptr[i & 3];
					}
				}
				packet.SetUnsafeMetadata(length, offset);
				return true;
			}
			return false;
		}

		private unsafe static bool Binary(ref global::Unity.Networking.Transport.WebSocket.Buffer buffer, void* payload, int payloadLen, bool useMask, uint mask)
		{
			int padding = HeaderLen(payloadLen, useMask);
			fixed (byte* data = buffer.Data)
			{
				byte* ptr = data + buffer.Length;
				int available = buffer.Available;
				int num = Header(ptr, padding, available, payloadLen, useMask, mask);
				if (num <= 0)
				{
					return false;
				}
				ptr += num;
				if (useMask)
				{
					byte* ptr2 = ptr - 4;
					for (int i = 0; i < payloadLen; i++)
					{
						ptr[i] = (byte)(((byte*)payload)[i] ^ ptr2[i & 3]);
					}
				}
				else if (payloadLen > 0)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, payload, payloadLen);
				}
				buffer.Length += num + payloadLen;
				return true;
			}
		}

		private static int HeaderLen(int payloadLen, bool useMask)
		{
			int num = ((payloadLen < 126) ? 2 : ((payloadLen > 65535) ? 10 : 4));
			if (useMask)
			{
				num += 4;
			}
			return num;
		}

		private unsafe static int Header(byte* destination, int padding, int capacity, int payloadLen, bool useMask, uint mask)
		{
			int num = HeaderLen(payloadLen, useMask);
			if (capacity < padding || capacity < num + payloadLen || num > padding)
			{
				return 0;
			}
			destination += padding - num;
			*(destination++) = 130;
			byte b = (byte)(useMask ? 128 : 0);
			if (payloadLen < 126)
			{
				*(destination++) = (byte)(b | payloadLen);
			}
			else if (payloadLen <= 65535)
			{
				*(destination++) = (byte)(b | 0x7E);
				*(destination++) = (byte)(payloadLen >> 8);
				*(destination++) = (byte)(payloadLen & 0xFF);
			}
			else
			{
				*(destination++) = (byte)(b | 0x7F);
				*(destination++) = 0;
				*(destination++) = 0;
				*(destination++) = 0;
				*(destination++) = 0;
				*(destination++) = (byte)((payloadLen >> 24) & 0xFF);
				*(destination++) = (byte)((payloadLen >> 16) & 0xFF);
				*(destination++) = (byte)((payloadLen >> 8) & 0xFF);
				*(destination++) = (byte)(payloadLen & 0xFF);
			}
			if (useMask)
			{
				*(destination++) = (byte)(mask >> 24);
				*(destination++) = (byte)((mask >> 16) & 0xFF);
				*(destination++) = (byte)((mask >> 8) & 0xFF);
				*(destination++) = (byte)(mask & 0xFF);
			}
			return num;
		}

		private unsafe static void GenerateBase64Key(out global::Unity.Collections.FixedString32Bytes key, ref global::Unity.Networking.Transport.WebSocket.Keys keys)
		{
			key = default(global::Unity.Collections.FixedString32Bytes);
			AppendBase64(ref key, (byte)(keys.Key[0] >> 24), (byte)(keys.Key[0] >> 16), (byte)(keys.Key[0] >> 8));
			AppendBase64(ref key, (byte)keys.Key[0], (byte)(keys.Key[1] >> 24), (byte)(keys.Key[1] >> 16));
			AppendBase64(ref key, (byte)(keys.Key[1] >> 8), (byte)keys.Key[1], (byte)(keys.Key[2] >> 24));
			AppendBase64(ref key, (byte)(keys.Key[2] >> 16), (byte)(keys.Key[2] >> 8), (byte)keys.Key[2]);
			AppendBase64(ref key, (byte)(keys.Key[3] >> 24), (byte)(keys.Key[3] >> 16), (byte)(keys.Key[3] >> 8));
			AppendBase64(ref key, (byte)keys.Key[3]);
		}

		private static void AppendBase64(ref global::Unity.Collections.FixedString32Bytes base64, byte b0, byte b1, byte b2)
		{
			byte value = ApplyTable((byte)(b0 >> 2));
			byte value2 = ApplyTable((byte)(((b0 & 3) << 4) | (b1 >> 4)));
			byte value3 = ApplyTable((byte)(((b1 & 0xF) << 2) | (b2 >> 6)));
			byte value4 = ApplyTable((byte)(b2 & 0x3F));
			base64.Add(in value);
			base64.Add(in value2);
			base64.Add(in value3);
			base64.Add(in value4);
		}

		private static void AppendBase64(ref global::Unity.Collections.FixedString32Bytes base64, byte b0, byte b1)
		{
			byte value = ApplyTable((byte)(b0 >> 2));
			byte value2 = ApplyTable((byte)(((b0 & 3) << 4) | (b1 >> 4)));
			byte value3 = ApplyTable((byte)((b1 & 0xF) << 2));
			base64.Add(in value);
			base64.Add(in value2);
			base64.Add(in value3);
			base64.Add((byte)61);
		}

		private static void AppendBase64(ref global::Unity.Collections.FixedString32Bytes base64, byte b0)
		{
			byte value = ApplyTable((byte)(b0 >> 2));
			byte value2 = ApplyTable((byte)((b0 & 3) << 4));
			base64.Add(in value);
			base64.Add(in value2);
			base64.Add((byte)61);
			base64.Add((byte)61);
		}

		private static byte ApplyTable(byte val)
		{
			if (val < 26)
			{
				return (byte)(val + 65);
			}
			if (val < 52)
			{
				return (byte)(val + 97 - 26);
			}
			if (val < 62)
			{
				return (byte)(val + 48 - 52);
			}
			if (val == 62)
			{
				return 43;
			}
			return 47;
		}
	}
}
