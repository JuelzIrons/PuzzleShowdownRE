namespace UnityWebSocketSharp
{
	internal class LogData
	{
		private global::System.Diagnostics.StackFrame _caller;

		private global::System.DateTime _date;

		private global::UnityWebSocketSharp.LogLevel _level;

		private string _message;

		public global::System.Diagnostics.StackFrame Caller => _caller;

		public global::System.DateTime Date => _date;

		public global::UnityWebSocketSharp.LogLevel Level => _level;

		public string Message => _message;

		internal LogData(global::UnityWebSocketSharp.LogLevel level, global::System.Diagnostics.StackFrame caller, string message)
		{
			_level = level;
			_caller = caller;
			_message = message ?? string.Empty;
			_date = global::System.DateTime.Now;
		}

		public override string ToString()
		{
			string text = $"[{_date}]";
			string text2 = $"{_level.ToString().ToUpper(),-5}";
			global::System.Reflection.MethodBase method = _caller.GetMethod();
			global::System.Type declaringType = method.DeclaringType;
			string text3 = $"{declaringType.Name}.{method.Name}";
			string[] array = _message.Replace("\r\n", "\n").TrimEnd('\n').Split('\n');
			if (array.Length <= 1)
			{
				return $"{text} {text2} {text3} {_message}";
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(64);
			stringBuilder.AppendFormat("{0} {1} {2}\n\n", text, text2, text3);
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.AppendFormat("  {0}\n", array[i]);
			}
			return stringBuilder.ToString();
		}
	}
}
