namespace Unity.Netcode
{
	internal class InvalidMessageStructureException : global::System.SystemException
	{
		public InvalidMessageStructureException()
		{
		}

		public InvalidMessageStructureException(string issue)
			: base(issue)
		{
		}
	}
}
