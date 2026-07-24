namespace Unity.Collections
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct | global::System.AttributeTargets.Constructor | global::System.AttributeTargets.Method | global::System.AttributeTargets.Property, AllowMultiple = true)]
	public class GenerateTestsForBurstCompatibilityAttribute : global::System.Attribute
	{
		public enum BurstCompatibleCompileTarget
		{
			Player = 0,
			Editor = 1,
			PlayerAndEditor = 2
		}

		public string RequiredUnityDefine;

		public global::Unity.Collections.GenerateTestsForBurstCompatibilityAttribute.BurstCompatibleCompileTarget CompileTarget;

		public global::System.Type[] GenericTypeArguments { get; set; }
	}
}
