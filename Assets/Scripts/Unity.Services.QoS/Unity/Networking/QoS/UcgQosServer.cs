namespace Unity.Networking.QoS
{
	[global::System.Serializable]
	internal struct UcgQosServer
	{
		internal string regionid;

		internal string ipv4;

		internal string ipv6;

		internal ushort port;

		[global::System.NonSerialized]
		[global::UnityEngine.HideInInspector]
		internal global::System.DateTime BackoffUntilUtc;

		public override string ToString()
		{
			if (!string.IsNullOrEmpty(ipv6))
			{
				return $"[{ipv6}]:{port}, {regionid}, {BackoffUntilUtc}";
			}
			if (!string.IsNullOrEmpty(ipv4))
			{
				return $"{ipv4}:{port}, {regionid}, {BackoffUntilUtc}";
			}
			return "";
		}
	}
}
