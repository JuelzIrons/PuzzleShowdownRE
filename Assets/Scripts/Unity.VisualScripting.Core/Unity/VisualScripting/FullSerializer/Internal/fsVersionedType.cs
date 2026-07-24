namespace Unity.VisualScripting.FullSerializer.Internal
{
	public struct fsVersionedType
	{
		public global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType[] Ancestors;

		public string VersionString;

		public global::System.Type ModelType;

		public object Migrate(object ancestorInstance)
		{
			return global::System.Activator.CreateInstance(ModelType, ancestorInstance);
		}

		public override string ToString()
		{
			return "fsVersionedType [ModelType=" + ModelType?.ToString() + ", VersionString=" + VersionString + ", Ancestors.Length=" + Ancestors.Length + "]";
		}

		public static bool operator ==(global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType a, global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType b)
		{
			return a.ModelType == b.ModelType;
		}

		public static bool operator !=(global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType a, global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType b)
		{
			return a.ModelType != b.ModelType;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType)
			{
				return ModelType == ((global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType)obj).ModelType;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return ModelType.GetHashCode();
		}
	}
}
