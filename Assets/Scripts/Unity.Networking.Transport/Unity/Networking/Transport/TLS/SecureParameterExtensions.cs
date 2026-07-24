namespace Unity.Networking.Transport.TLS
{
	public static class SecureParameterExtensions
	{
		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureClientParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Collections.FixedString512Bytes serverName)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				Certificate = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				PrivateKey = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				Hostname = serverName,
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureClientParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, string serverName)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				Certificate = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				PrivateKey = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				Hostname = serverName,
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureClientParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Collections.FixedString4096Bytes caCertificate, ref global::Unity.Collections.FixedString512Bytes serverName)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(ref caCertificate),
				Certificate = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				PrivateKey = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				Hostname = serverName,
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureClientParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.TLS.FixedPEMString caCertificate, ref global::Unity.Collections.FixedString512Bytes serverName)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = caCertificate,
				Certificate = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				PrivateKey = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				Hostname = serverName,
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureClientParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, string caCertificate, string serverName)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(caCertificate),
				Certificate = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				PrivateKey = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				Hostname = serverName,
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureClientParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Collections.FixedString4096Bytes certificate, ref global::Unity.Collections.FixedString4096Bytes privateKey, ref global::Unity.Collections.FixedString4096Bytes caCertificate, ref global::Unity.Collections.FixedString512Bytes serverName)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(ref caCertificate),
				Certificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(ref certificate),
				PrivateKey = new global::Unity.Networking.Transport.TLS.FixedPEMString(ref privateKey),
				Hostname = serverName,
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureClientParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.TLS.FixedPEMString certificate, ref global::Unity.Networking.Transport.TLS.FixedPEMString privateKey, ref global::Unity.Networking.Transport.TLS.FixedPEMString caCertificate, ref global::Unity.Collections.FixedString512Bytes serverName)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = caCertificate,
				Certificate = certificate,
				PrivateKey = privateKey,
				Hostname = serverName,
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureClientParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, string certificate, string privateKey, string caCertificate, string serverName)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(caCertificate),
				Certificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(certificate),
				PrivateKey = new global::Unity.Networking.Transport.TLS.FixedPEMString(privateKey),
				Hostname = serverName,
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureServerParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Collections.FixedString4096Bytes certificate, ref global::Unity.Collections.FixedString4096Bytes privateKey)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				Certificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(ref certificate),
				PrivateKey = new global::Unity.Networking.Transport.TLS.FixedPEMString(ref privateKey),
				Hostname = default(global::Unity.Collections.FixedString512Bytes),
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureServerParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.TLS.FixedPEMString certificate, ref global::Unity.Networking.Transport.TLS.FixedPEMString privateKey)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				Certificate = certificate,
				PrivateKey = privateKey,
				Hostname = default(global::Unity.Collections.FixedString512Bytes),
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureServerParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, string certificate, string privateKey)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = default(global::Unity.Networking.Transport.TLS.FixedPEMString),
				Certificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(certificate),
				PrivateKey = new global::Unity.Networking.Transport.TLS.FixedPEMString(privateKey),
				Hostname = default(global::Unity.Collections.FixedString512Bytes),
				ClientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.None
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureServerParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Collections.FixedString4096Bytes certificate, ref global::Unity.Collections.FixedString4096Bytes privateKey, ref global::Unity.Collections.FixedString4096Bytes caCertificate, ref global::Unity.Collections.FixedString512Bytes clientName, global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy clientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.Required)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(ref caCertificate),
				Certificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(ref certificate),
				PrivateKey = new global::Unity.Networking.Transport.TLS.FixedPEMString(ref privateKey),
				Hostname = clientName,
				ClientAuthenticationPolicy = clientAuthenticationPolicy
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureServerParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, ref global::Unity.Networking.Transport.TLS.FixedPEMString certificate, ref global::Unity.Networking.Transport.TLS.FixedPEMString privateKey, ref global::Unity.Networking.Transport.TLS.FixedPEMString caCertificate, ref global::Unity.Collections.FixedString512Bytes clientName, global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy clientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.Required)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = caCertificate,
				Certificate = certificate,
				PrivateKey = privateKey,
				Hostname = clientName,
				ClientAuthenticationPolicy = clientAuthenticationPolicy
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static ref global::Unity.Networking.Transport.NetworkSettings WithSecureServerParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, string certificate, string privateKey, string caCertificate, string clientName, global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy clientAuthenticationPolicy = global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy.Required)
		{
			global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter parameter = new global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter
			{
				CACertificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(caCertificate),
				Certificate = new global::Unity.Networking.Transport.TLS.FixedPEMString(certificate),
				PrivateKey = new global::Unity.Networking.Transport.TLS.FixedPEMString(privateKey),
				Hostname = clientName,
				ClientAuthenticationPolicy = clientAuthenticationPolicy
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter GetSecureParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings)
		{
			if (!settings.TryGet<global::Unity.Networking.Transport.TLS.SecureNetworkProtocolParameter>(out var parameter))
			{
				throw new global::System.InvalidOperationException("Can't extract Secure parameters: SecureNetworkProtocolParameter must be provided to the NetworkSettings");
			}
			return parameter;
		}
	}
}
