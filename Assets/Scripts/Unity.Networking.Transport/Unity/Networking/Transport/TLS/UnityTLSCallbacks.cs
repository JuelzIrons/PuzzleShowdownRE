namespace Unity.Networking.Transport.TLS
{
	internal static class UnityTLSCallbacks
	{
		public struct CallbackContext
		{
			public global::Unity.Networking.Transport.PacketProcessor ReceivedPacket;

			public global::Unity.Networking.Transport.PacketsQueue SendQueue;

			public int SendQueueIndex;

			public int PacketPadding;

			public global::Unity.Networking.Transport.NetworkEndpoint NewPacketsEndpoint;

			public global::Unity.Networking.Transport.ConnectionId NewPacketsConnection;
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct FunctionPointersKey
		{
		}

		private static global::Unity.TLS.LowLevel.Binding.unitytls_client_data_send_callback s_SendCallbackDelegate;

		private static global::Unity.TLS.LowLevel.Binding.unitytls_client_data_receive_callback s_ReceiveCallbackDelegate;

		private static global::Unity.TLS.LowLevel.Binding.unitytls_client_log_callback s_LogCallbackDelegate;

		private static readonly global::Unity.Burst.SharedStatic<global::Unity.Burst.FunctionPointer<global::Unity.TLS.LowLevel.Binding.unitytls_client_data_send_callback>> s_SendCallbackPtr = global::Unity.Burst.SharedStatic<global::Unity.Burst.FunctionPointer<global::Unity.TLS.LowLevel.Binding.unitytls_client_data_send_callback>>.GetOrCreateUnsafe(0u, -5978926962771261641L, 3670757805540321176L);

		private static readonly global::Unity.Burst.SharedStatic<global::Unity.Burst.FunctionPointer<global::Unity.TLS.LowLevel.Binding.unitytls_client_data_receive_callback>> s_ReceiveCallbackPtr = global::Unity.Burst.SharedStatic<global::Unity.Burst.FunctionPointer<global::Unity.TLS.LowLevel.Binding.unitytls_client_data_receive_callback>>.GetOrCreateUnsafe(0u, -5365965991464889048L, 3670757805540321176L);

		private static readonly global::Unity.Burst.SharedStatic<global::Unity.Burst.FunctionPointer<global::Unity.TLS.LowLevel.Binding.unitytls_client_log_callback>> s_LogCallbackPtr = global::Unity.Burst.SharedStatic<global::Unity.Burst.FunctionPointer<global::Unity.TLS.LowLevel.Binding.unitytls_client_log_callback>>.GetOrCreateUnsafe(0u, -3783631547108426640L, 3670757805540321176L);

		private static bool s_Initialized;

		private const int UNITYTLS_ERR_SSL_WANT_READ = -26880;

		private const int UNITYTLS_ERR_SSL_WANT_WRITE = -26752;

		public static global::System.IntPtr SendCallbackPtr
		{
			get
			{
				if (!s_Initialized)
				{
					return global::System.IntPtr.Zero;
				}
				return s_SendCallbackPtr.Data.Value;
			}
		}

		public static global::System.IntPtr ReceiveCallbackPtr
		{
			get
			{
				if (!s_Initialized)
				{
					return global::System.IntPtr.Zero;
				}
				return s_ReceiveCallbackPtr.Data.Value;
			}
		}

		public static global::System.IntPtr LogCallbackPtr
		{
			get
			{
				if (!s_Initialized)
				{
					return global::System.IntPtr.Zero;
				}
				return s_LogCallbackPtr.Data.Value;
			}
		}

		public unsafe static void Initialize()
		{
			if (!s_Initialized)
			{
				s_Initialized = true;
				s_SendCallbackDelegate = SendCallback;
				s_ReceiveCallbackDelegate = ReceiveCallback;
				s_LogCallbackDelegate = LogCallback;
				global::System.IntPtr functionPointerForDelegate = global::System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(s_SendCallbackDelegate);
				s_SendCallbackPtr.Data = new global::Unity.Burst.FunctionPointer<global::Unity.TLS.LowLevel.Binding.unitytls_client_data_send_callback>(functionPointerForDelegate);
				global::System.IntPtr functionPointerForDelegate2 = global::System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(s_ReceiveCallbackDelegate);
				s_ReceiveCallbackPtr.Data = new global::Unity.Burst.FunctionPointer<global::Unity.TLS.LowLevel.Binding.unitytls_client_data_receive_callback>(functionPointerForDelegate2);
				global::System.IntPtr functionPointerForDelegate3 = global::System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(s_LogCallbackDelegate);
				s_LogCallbackPtr.Data = new global::Unity.Burst.FunctionPointer<global::Unity.TLS.LowLevel.Binding.unitytls_client_log_callback>(functionPointerForDelegate3);
			}
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.TLS.LowLevel.Binding.unitytls_client_data_send_callback))]
		private unsafe static int SendCallback(global::System.IntPtr userData, byte* data, global::System.UIntPtr dataLength, uint status)
		{
			global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.CallbackContext* ptr = (global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.CallbackContext*)(void*)userData;
			int num = (int)dataLength.ToUInt32();
			if (ptr->SendQueueIndex >= 0)
			{
				global::Unity.Networking.Transport.PacketProcessor packetProcessor = ptr->SendQueue[ptr->SendQueueIndex];
				int num2 = packetProcessor.Offset - ptr->PacketPadding;
				if (num2 < 0)
				{
					global::UnityEngine.Debug.LogError($"Invalid offset in packet processor ({packetProcessor.Offset}, should be >={ptr->PacketPadding}).");
					return -26752;
				}
				packetProcessor.SetUnsafeMetadata(0, num2);
				packetProcessor.AppendToPayload(data, num);
			}
			else
			{
				int num3;
				for (int i = 0; i < num; i += num3)
				{
					if (!ptr->SendQueue.EnqueuePacket(out var packetProcessor2))
					{
						if (i <= 0)
						{
							return -26752;
						}
						return i;
					}
					packetProcessor2.EndpointRef = ptr->NewPacketsEndpoint;
					packetProcessor2.ConnectionRef = ptr->NewPacketsConnection;
					num3 = global::Unity.Mathematics.math.min(num - i, packetProcessor2.BytesAvailableAtEnd);
					packetProcessor2.AppendToPayload(data + i, num3);
				}
			}
			return num;
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.TLS.LowLevel.Binding.unitytls_client_data_receive_callback))]
		private unsafe static int ReceiveCallback(global::System.IntPtr userData, byte* data, global::System.UIntPtr dataLength, uint status)
		{
			global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.CallbackContext* ptr = (global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.CallbackContext*)(void*)userData;
			if (!ptr->ReceivedPacket.IsCreated || ptr->ReceivedPacket.Length == 0)
			{
				return -26880;
			}
			int num = global::Unity.Mathematics.math.min((int)dataLength.ToUInt32(), ptr->ReceivedPacket.Length);
			ptr->ReceivedPacket.RemoveFromPayloadStart(data, num);
			return num;
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.TLS.LowLevel.Binding.unitytls_client_log_callback))]
		private unsafe static void LogCallback(int level, byte* file, global::System.UIntPtr line, byte* function, byte* message, global::System.UIntPtr messageLength)
		{
			global::Unity.Collections.FixedString512Bytes fs = "[UnityTLS";
			if (file != null && RawStringLength(file) != 0)
			{
				global::Unity.Collections.FixedStringMethods.Append(ref fs, ':');
				global::Unity.Collections.FixedStringMethods.Append(ref fs, file, RawStringLength(file));
				global::Unity.Collections.FixedStringMethods.Append(ref fs, ':');
				global::Unity.Collections.FixedStringMethods.Append(ref fs, line.ToUInt32());
			}
			if (function != null && RawStringLength(function) != 0)
			{
				global::Unity.Collections.FixedStringMethods.Append(ref fs, ':');
				global::Unity.Collections.FixedStringMethods.Append(ref fs, function, RawStringLength(function));
			}
			global::Unity.Collections.FixedStringMethods.Append(ref fs, ']');
			global::Unity.Collections.FixedStringMethods.Append(ref fs, ' ');
			global::Unity.Collections.FixedStringMethods.Append(ref fs, message, (int)messageLength.ToUInt32());
			global::UnityEngine.Debug.Log(fs);
		}

		private unsafe static int RawStringLength(byte* str)
		{
			int i;
			for (i = 0; str[i] != 0; i++)
			{
			}
			return i;
		}
	}
}
