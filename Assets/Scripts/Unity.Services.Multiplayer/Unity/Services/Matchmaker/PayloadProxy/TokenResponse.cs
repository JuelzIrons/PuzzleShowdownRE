namespace Unity.Services.Matchmaker.PayloadProxy
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "TokenResponse")]
	public class TokenResponse
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "token", IsRequired = true, EmitDefaultValue = true)]
		public string Token { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "error", IsRequired = true, EmitDefaultValue = true)]
		public string Error { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		public TokenResponse()
		{
			Token = string.Empty;
			Error = string.Empty;
		}

		[global::UnityEngine.Scripting.Preserve]
		public TokenResponse(string token, string error)
		{
			Token = token;
			Error = error;
		}
	}
}
