namespace Unity.VisualScripting
{
	public class MemberInfoComparer : global::System.Collections.Generic.EqualityComparer<global::System.Reflection.MemberInfo>
	{
		public override bool Equals(global::System.Reflection.MemberInfo x, global::System.Reflection.MemberInfo y)
		{
			return x?.MetadataToken == y?.MetadataToken;
		}

		public override int GetHashCode(global::System.Reflection.MemberInfo obj)
		{
			return obj.MetadataToken;
		}
	}
}
