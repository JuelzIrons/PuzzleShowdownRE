namespace Unity.Services.Authentication.Generated
{
	[global::System.Runtime.Serialization.DataContract(Name = "Detail")]
	[global::UnityEngine.Scripting.Preserve]
	internal class Detail
	{
		[global::System.Runtime.Serialization.DataMember(Name = "errorType", IsRequired = true, EmitDefaultValue = true)]
		[global::UnityEngine.Scripting.Preserve]
		public string ErrorType { get; set; }

		[global::System.Runtime.Serialization.DataMember(Name = "message", IsRequired = true, EmitDefaultValue = true)]
		[global::UnityEngine.Scripting.Preserve]
		public string Message { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		public Detail(string errorType = null, string message = null)
		{
			if (errorType == null)
			{
				throw new global::System.ArgumentNullException("errorType is a required property for Detail and cannot be null");
			}
			ErrorType = errorType;
			if (message == null)
			{
				throw new global::System.ArgumentNullException("message is a required property for Detail and cannot be null");
			}
			Message = message;
		}
	}
}
