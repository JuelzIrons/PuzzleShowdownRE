namespace Unity.Burst.Intrinsics
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Method, Inherited = false)]
	[global::Unity.Burst.BurstRuntime.Preserve]
	internal sealed class BurstTargetCpuAttribute : global::System.Attribute
	{
		public readonly global::Unity.Burst.BurstTargetCpu TargetCpu;

		public BurstTargetCpuAttribute(global::Unity.Burst.BurstTargetCpu TargetCpu)
		{
			this.TargetCpu = TargetCpu;
		}
	}
}
