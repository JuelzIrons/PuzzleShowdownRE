namespace Unity.VisualScripting.FullSerializer
{
	public class fsConfig
	{
		public global::System.Type[] SerializeAttributes = new global::System.Type[4]
		{
			typeof(global::UnityEngine.SerializeField),
			typeof(global::Unity.VisualScripting.FullSerializer.fsPropertyAttribute),
			typeof(global::Unity.VisualScripting.SerializeAttribute),
			typeof(global::Unity.VisualScripting.SerializeAsAttribute)
		};

		public global::System.Type[] IgnoreSerializeAttributes = new global::System.Type[3]
		{
			typeof(global::System.NonSerializedAttribute),
			typeof(global::Unity.VisualScripting.FullSerializer.fsIgnoreAttribute),
			typeof(global::Unity.VisualScripting.DoNotSerializeAttribute)
		};

		public global::Unity.VisualScripting.FullSerializer.fsMemberSerialization DefaultMemberSerialization = global::Unity.VisualScripting.FullSerializer.fsMemberSerialization.Default;

		public global::System.Func<string, global::System.Reflection.MemberInfo, string> GetJsonNameFromMemberName = (string name, global::System.Reflection.MemberInfo info) => name;

		public bool EnablePropertySerialization = true;

		public bool SerializeNonAutoProperties;

		public bool SerializeNonPublicSetProperties = true;

		public string CustomDateTimeFormatString;

		public bool Serialize64BitIntegerAsString;

		public bool SerializeEnumsAsInteger;
	}
}
