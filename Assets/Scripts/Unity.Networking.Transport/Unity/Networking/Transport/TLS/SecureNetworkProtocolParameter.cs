namespace Unity.Networking.Transport.TLS
{
	[global::System.Serializable]
	public struct SecureNetworkProtocolParameter : global::Unity.Networking.Transport.INetworkParameter
	{
		public global::Unity.Networking.Transport.TLS.FixedPEMString CACertificate;

		public global::Unity.Networking.Transport.TLS.FixedPEMString Certificate;

		public global::Unity.Networking.Transport.TLS.FixedPEMString PrivateKey;

		public global::Unity.Collections.FixedString512Bytes Hostname;

		public global::Unity.Networking.Transport.TLS.SecureClientAuthPolicy ClientAuthenticationPolicy;

		public bool Validate()
		{
			return true;
		}
	}
}
