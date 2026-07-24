namespace Newtonsoft.Json.Utilities
{
	internal class ReflectionMember
	{
		public global::System.Type? MemberType { get; set; }

		public global::System.Func<object, object?>? Getter { get; set; }

		public global::System.Action<object, object?>? Setter { get; set; }
	}
}
