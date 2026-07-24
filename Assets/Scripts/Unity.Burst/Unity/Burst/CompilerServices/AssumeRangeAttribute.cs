namespace Unity.Burst.CompilerServices
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Parameter | global::System.AttributeTargets.ReturnValue)]
	public class AssumeRangeAttribute : global::System.Attribute
	{
		public AssumeRangeAttribute(long min, long max)
		{
		}

		public AssumeRangeAttribute(ulong min, ulong max)
		{
		}
	}
}
