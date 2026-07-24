namespace Steamworks
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Struct, AllowMultiple = false)]
	internal class CallbackIdentityAttribute : global::System.Attribute
	{
		public int Identity { get; set; }

		public CallbackIdentityAttribute(int callbackNum)
		{
			Identity = callbackNum;
		}
	}
}
