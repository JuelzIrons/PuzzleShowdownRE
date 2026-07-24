namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public class SerializableEnum
	{
		[global::UnityEngine.SerializeField]
		private string m_EnumValueAsString;

		[global::UnityEngine.SerializeField]
		private string m_EnumTypeAsString;

		public global::System.Enum value
		{
			get
			{
				if (string.IsNullOrEmpty(m_EnumTypeAsString) || !global::System.Enum.TryParse(global::System.Type.GetType(m_EnumTypeAsString), m_EnumValueAsString, out var result))
				{
					return null;
				}
				return (global::System.Enum)result;
			}
			set
			{
				m_EnumValueAsString = value.ToString();
			}
		}

		public SerializableEnum(global::System.Type enumType)
		{
			m_EnumTypeAsString = enumType.AssemblyQualifiedName;
			m_EnumValueAsString = global::System.Enum.GetNames(enumType)[0];
		}
	}
}
