namespace Unity.Networking.Transport.TLS
{
	internal struct UnityTLSConfiguration : global::System.IDisposable
	{
		private global::Unity.Collections.NativeReference<global::Unity.TLS.LowLevel.Binding.unitytls_client_config> m_Config;

		private global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.CallbackContext> m_Callbacks;

		private global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter> m_SecureParameters;

		private global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.Relay.RelayNetworkParameter> m_RelayParameters;

		public unsafe global::Unity.TLS.LowLevel.Binding.unitytls_client_config* ConfigPtr => global::Unity.Collections.LowLevel.Unsafe.NativeReferenceUnsafeUtility.GetUnsafePtr(m_Config);

		public unsafe global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.CallbackContext* CallbackContextPtr => global::Unity.Collections.LowLevel.Unsafe.NativeReferenceUnsafeUtility.GetUnsafePtr(m_Callbacks);

		public bool IsCreated => m_Config.IsCreated;

		private unsafe static void InitializeFromSecureParameters(global::Unity.TLS.LowLevel.Binding.unitytls_client_config* config, ref global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameters)
		{
			config->clientAuth = (uint)parameters.ClientAuthenticationPolicy;
			if (parameters.Hostname != ILSpyHelper_AsRefReadOnly(default(global::Unity.Collections.FixedString32Bytes)))
			{
				config->hostname = parameters.Hostname.GetUnsafePtr();
			}
			if (parameters.CACertificate.Length > 0)
			{
				config->caPEM = new global::Unity.TLS.LowLevel.Binding.unitytls_dataRef
				{
					dataPtr = parameters.CACertificate.GetUnsafePtr(),
					dataLen = new global::System.UIntPtr((uint)parameters.CACertificate.Length)
				};
			}
			if (parameters.Certificate.Length > 0 && parameters.PrivateKey.Length > 0)
			{
				config->serverPEM = new global::Unity.TLS.LowLevel.Binding.unitytls_dataRef
				{
					dataPtr = parameters.Certificate.GetUnsafePtr(),
					dataLen = new global::System.UIntPtr((uint)parameters.Certificate.Length)
				};
				config->privateKeyPEM = new global::Unity.TLS.LowLevel.Binding.unitytls_dataRef
				{
					dataPtr = parameters.PrivateKey.GetUnsafePtr(),
					dataLen = new global::System.UIntPtr((uint)parameters.PrivateKey.Length)
				};
			}
			static ref readonly T ILSpyHelper_AsRefReadOnly<T>(in T temp)
			{
				//ILSpy generated this function to help ensure overload resolution can pick the overload using 'in'
				return ref temp;
			}
		}

		private unsafe static void InitializeFromRelayParameters(global::Unity.TLS.LowLevel.Binding.unitytls_client_config* config, ref global::Unity.Networking.Transport.Relay.RelayNetworkParameter parameters)
		{
			config->hostname = parameters.ServerData.HostString.GetUnsafePtr();
			if (config->transportProtocol == 1)
			{
				fixed (byte* value = parameters.ServerData.HMACKey.Value)
				{
					config->psk = new global::Unity.TLS.LowLevel.Binding.unitytls_dataRef
					{
						dataPtr = value,
						dataLen = new global::System.UIntPtr(64u)
					};
				}
				fixed (byte* value2 = parameters.ServerData.AllocationId.Value)
				{
					config->pskIdentity = new global::Unity.TLS.LowLevel.Binding.unitytls_dataRef
					{
						dataPtr = value2,
						dataLen = new global::System.UIntPtr(16u)
					};
				}
			}
		}

		public unsafe UnityTLSConfiguration(ref global::Unity.Networking.Transport.NetworkSettings settings, global::Unity.Networking.Transport.TLS.SecureTransportProtocol protocol, ushort mtu = 0)
		{
			global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.Initialize();
			m_Config = new global::Unity.Collections.NativeReference<global::Unity.TLS.LowLevel.Binding.unitytls_client_config>(global::Unity.Collections.Allocator.Persistent);
			m_Callbacks = new global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.CallbackContext>(global::Unity.Collections.Allocator.Persistent);
			m_SecureParameters = default(global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter>);
			m_RelayParameters = default(global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.Relay.RelayNetworkParameter>);
			global::Unity.TLS.LowLevel.Binding.unitytls_client_init_config(ConfigPtr);
			global::Unity.Networking.Transport.NetworkConfigParameter networkConfigParameters = settings.GetNetworkConfigParameters();
			ConfigPtr->ssl_handshake_timeout_min = (uint)networkConfigParameters.connectTimeoutMS;
			ConfigPtr->ssl_handshake_timeout_max = (uint)(networkConfigParameters.maxConnectAttempts * networkConfigParameters.connectTimeoutMS);
			ConfigPtr->transportProtocol = (uint)protocol;
			ConfigPtr->transportUserData = (global::System.IntPtr)CallbackContextPtr;
			ConfigPtr->dataSendCB = global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.SendCallbackPtr;
			ConfigPtr->dataReceiveCB = global::Unity.Networking.Transport.TLS.UnityTLSCallbacks.ReceiveCallbackPtr;
			ConfigPtr->mtu = mtu;
			if (settings.TryGet<global::Unity.Networking.Transport.Relay.RelayNetworkParameter>(out var parameter))
			{
				m_RelayParameters = new global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.Relay.RelayNetworkParameter>(parameter, global::Unity.Collections.Allocator.Persistent);
				global::Unity.Networking.Transport.Relay.RelayNetworkParameter* unsafePtr = global::Unity.Collections.LowLevel.Unsafe.NativeReferenceUnsafeUtility.GetUnsafePtr(m_RelayParameters);
				InitializeFromRelayParameters(ConfigPtr, ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<global::Unity.Networking.Transport.Relay.RelayNetworkParameter>(unsafePtr));
			}
			if (settings.TryGet<global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter>(out var parameter2))
			{
				m_SecureParameters = new global::Unity.Collections.NativeReference<global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter>(global::Unity.Collections.Allocator.Persistent);
				global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter* unsafePtr2 = global::Unity.Collections.LowLevel.Unsafe.NativeReferenceUnsafeUtility.GetUnsafePtr(m_SecureParameters);
				*unsafePtr2 = parameter2;
				InitializeFromSecureParameters(ConfigPtr, ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter>(unsafePtr2));
			}
		}

		public void Dispose()
		{
			if (IsCreated)
			{
				m_Config.Dispose();
				m_Callbacks.Dispose();
			}
			if (m_SecureParameters.IsCreated)
			{
				m_SecureParameters.Dispose();
			}
			if (m_RelayParameters.IsCreated)
			{
				m_RelayParameters.Dispose();
			}
		}
	}
}
