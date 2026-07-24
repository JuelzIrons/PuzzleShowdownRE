namespace Unity.Services.DistributedAuthority
{
	internal class ConnectPayload
	{
		private const int KeyLength = 256;

		private const byte Version = 0;

		public readonly byte[] Secret = new byte[32];

		public readonly byte[] PlayerId;

		public ConnectPayload(string playerId)
		{
			PlayerId = global::System.Text.Encoding.UTF8.GetBytes(playerId);
			global::System.Security.Cryptography.RandomNumberGenerator.Create().GetNonZeroBytes(Secret);
		}

		public string Base64SecretHash()
		{
			using global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			memoryStream.Write(Secret);
			memoryStream.Write(PlayerId);
			return global::System.Convert.ToBase64String(new global::System.Security.Cryptography.SHA256Managed().ComputeHash(memoryStream.ToArray()));
		}

		public global::Unity.Collections.NativeArray<byte> SerializeToNativeArray()
		{
			int length = 5 + Secret.Length + 4 + PlayerId.Length;
			global::Unity.Collections.DataStreamWriter dataStreamWriter = new global::Unity.Collections.DataStreamWriter(length, global::Unity.Collections.Allocator.Temp);
			dataStreamWriter.WriteByte(0);
			dataStreamWriter.WriteInt(Secret.Length);
			dataStreamWriter.WriteBytes(new global::Unity.Collections.NativeArray<byte>(Secret, global::Unity.Collections.Allocator.Temp));
			dataStreamWriter.WriteInt(PlayerId.Length);
			dataStreamWriter.WriteBytes(new global::Unity.Collections.NativeArray<byte>(PlayerId, global::Unity.Collections.Allocator.Temp));
			return dataStreamWriter.AsNativeArray();
		}
	}
}
