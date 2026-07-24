namespace Unity.VisualScripting.Antlr3.Runtime.Misc
{
	public class ErrorManager
	{
		public static void InternalError(object error, global::System.Exception e)
		{
			global::System.Diagnostics.StackFrame lastNonErrorManagerCodeLocation = GetLastNonErrorManagerCodeLocation(e);
			string arg = string.Concat("Exception ", e, "@", lastNonErrorManagerCodeLocation, ": ", error);
			Error(arg);
		}

		public static void InternalError(object error)
		{
			global::System.Diagnostics.StackFrame lastNonErrorManagerCodeLocation = GetLastNonErrorManagerCodeLocation(new global::System.Exception());
			string arg = string.Concat(lastNonErrorManagerCodeLocation, ": ", error);
			Error(arg);
		}

		private static global::System.Diagnostics.StackFrame GetLastNonErrorManagerCodeLocation(global::System.Exception e)
		{
			global::System.Diagnostics.StackTrace stackTrace = new global::System.Diagnostics.StackTrace(e);
			int i;
			for (i = 0; i < stackTrace.FrameCount; i++)
			{
				global::System.Diagnostics.StackFrame frame = stackTrace.GetFrame(i);
				if (frame.ToString().IndexOf("ErrorManager") < 0)
				{
					break;
				}
			}
			return stackTrace.GetFrame(i);
		}

		public static void Error(object arg)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.AppendFormat("internal error: {0} ", arg);
		}
	}
}
