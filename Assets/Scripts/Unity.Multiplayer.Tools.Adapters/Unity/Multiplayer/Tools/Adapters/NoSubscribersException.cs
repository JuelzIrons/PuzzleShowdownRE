namespace Unity.Multiplayer.Tools.Adapters
{
	internal class NoSubscribersException : global::System.Exception
	{
		public NoSubscribersException(string resourceName, string subscriptionMethodName)
			: base("Attempt to use " + resourceName + " without any subscribers. Subscribe using " + subscriptionMethodName + ", so that the adapter knows to compute and store this information.")
		{
		}
	}
}
