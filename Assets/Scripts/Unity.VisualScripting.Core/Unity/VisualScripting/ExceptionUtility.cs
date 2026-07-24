namespace Unity.VisualScripting
{
	public static class ExceptionUtility
	{
		public static global::System.Exception Relevant(this global::System.Exception ex)
		{
			if (ex is global::System.Reflection.TargetInvocationException)
			{
				return ex.InnerException;
			}
			return ex;
		}
	}
}
