namespace Unity.Services.Authentication.Generated
{
	[global::System.Runtime.Serialization.DataContract(Name = "ErrorStatus")]
	[global::UnityEngine.Scripting.Preserve]
	internal class ErrorStatus
	{
		[global::System.Runtime.Serialization.DataMember(Name = "status", IsRequired = true, EmitDefaultValue = true)]
		[global::UnityEngine.Scripting.Preserve]
		public int Status { get; set; }

		[global::System.Runtime.Serialization.DataMember(Name = "title", IsRequired = true, EmitDefaultValue = true)]
		[global::UnityEngine.Scripting.Preserve]
		public string Title { get; set; }

		[global::System.Runtime.Serialization.DataMember(Name = "detail", IsRequired = true, EmitDefaultValue = true)]
		[global::UnityEngine.Scripting.Preserve]
		public string Detail { get; set; }

		[global::System.Runtime.Serialization.DataMember(Name = "code", IsRequired = true, EmitDefaultValue = true)]
		[global::UnityEngine.Scripting.Preserve]
		public int Code { get; set; }

		[global::System.Runtime.Serialization.DataMember(Name = "details", EmitDefaultValue = false)]
		[global::UnityEngine.Scripting.Preserve]
		public global::System.Collections.Generic.List<global::Unity.Services.Authentication.Generated.Detail> Details { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		public ErrorStatus(int status = 0, string title = null, string detail = null, int code = 0, global::System.Collections.Generic.List<global::Unity.Services.Authentication.Generated.Detail> details = null)
		{
			Status = status;
			if (title == null)
			{
				throw new global::System.ArgumentNullException("title is a required property for ErrorStatus and cannot be null");
			}
			Title = title;
			if (detail == null)
			{
				throw new global::System.ArgumentNullException("detail is a required property for ErrorStatus and cannot be null");
			}
			Detail = detail;
			Code = code;
			Details = details;
		}
	}
}
