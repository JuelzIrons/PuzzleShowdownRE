namespace Unity.Collections
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Constructor | global::System.AttributeTargets.Method | global::System.AttributeTargets.Property)]
	public class ExcludeFromBurstCompatTestingAttribute : global::System.Attribute
	{
		public string Reason { get; set; }

		public ExcludeFromBurstCompatTestingAttribute(string _reason)
		{
			Reason = _reason;
		}
	}
}
