namespace Unity.VisualScripting
{
	public sealed class FakeSerializationCloner : global::Unity.VisualScripting.ReflectedCloner
	{
		public global::Unity.VisualScripting.FullSerializer.fsConfig config { get; set; } = new global::Unity.VisualScripting.FullSerializer.fsConfig();

		public override void BeforeClone(global::System.Type type, object original)
		{
			(original as global::UnityEngine.ISerializationCallbackReceiver)?.OnBeforeSerialize();
		}

		public override void AfterClone(global::System.Type type, object clone)
		{
			(clone as global::UnityEngine.ISerializationCallbackReceiver)?.OnAfterDeserialize();
		}

		protected override global::System.Collections.Generic.IEnumerable<global::System.Reflection.MemberInfo> GetMembers(global::System.Type type)
		{
			return global::System.Linq.Enumerable.Select(global::Unity.VisualScripting.FullSerializer.fsMetaType.Get(config, type).Properties, (global::Unity.VisualScripting.FullSerializer.fsMetaProperty p) => p._memberInfo);
		}
	}
}
