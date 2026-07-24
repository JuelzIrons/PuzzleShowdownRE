namespace UnityEngine.Timeline
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, Inherited = false)]
	internal class SupportsChildTracksAttribute : global::System.Attribute
	{
		public readonly global::System.Type childType;

		public readonly int levels;

		public SupportsChildTracksAttribute(global::System.Type childType = null, int levels = int.MaxValue)
		{
			this.childType = childType;
			this.levels = levels;
		}
	}
}
