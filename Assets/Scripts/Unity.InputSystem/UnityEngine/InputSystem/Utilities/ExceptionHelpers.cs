namespace UnityEngine.InputSystem.Utilities
{
	internal static class ExceptionHelpers
	{
		public static bool IsExceptionIndicatingBugInCode(this global::System.Exception exception)
		{
			if (!(exception is global::System.NullReferenceException) && !(exception is global::System.IndexOutOfRangeException))
			{
				return exception is global::System.ArgumentException;
			}
			return true;
		}
	}
}
