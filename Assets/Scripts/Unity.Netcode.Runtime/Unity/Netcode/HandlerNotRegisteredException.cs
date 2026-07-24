namespace Unity.Netcode
{
	internal class HandlerNotRegisteredException : global::System.SystemException
	{
		public HandlerNotRegisteredException()
		{
		}

		public HandlerNotRegisteredException(string issue)
			: base(issue)
		{
		}
	}
}
