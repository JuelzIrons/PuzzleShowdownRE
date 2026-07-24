namespace Unity.Services.Authentication.Generated
{
	[global::System.Runtime.Serialization.DataContract(Name = "UpdateNameRequest")]
	[global::UnityEngine.Scripting.Preserve]
	internal class UpdateNameRequest
	{
		[global::System.Runtime.Serialization.DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
		[global::UnityEngine.Scripting.Preserve]
		public string Name { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		public UpdateNameRequest(string name = null)
		{
			if (name == null)
			{
				throw new global::System.ArgumentNullException("name is a required property for UpdateNameRequest and cannot be null");
			}
			Name = name;
		}
	}
}
