namespace WebSocketSharp.Net
{
	[global::System.Serializable]
	public class CookieException : global::System.FormatException, global::System.Runtime.Serialization.ISerializable
	{
		internal CookieException(string message)
			: base(message)
		{
		}

		internal CookieException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}

		protected CookieException(global::System.Runtime.Serialization.SerializationInfo serializationInfo, global::System.Runtime.Serialization.StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		public CookieException()
		{
		}

		[global::System.Security.Permissions.SecurityPermission(global::System.Security.Permissions.SecurityAction.LinkDemand, Flags = global::System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(global::System.Runtime.Serialization.SerializationInfo serializationInfo, global::System.Runtime.Serialization.StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		[global::System.Security.Permissions.SecurityPermission(global::System.Security.Permissions.SecurityAction.LinkDemand, Flags = global::System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void global::System.Runtime.Serialization.ISerializable.GetObjectData(global::System.Runtime.Serialization.SerializationInfo serializationInfo, global::System.Runtime.Serialization.StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
