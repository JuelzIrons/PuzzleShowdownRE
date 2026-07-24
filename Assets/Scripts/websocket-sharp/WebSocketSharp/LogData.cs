namespace WebSocketSharp
{
	public class LogData
	{
		private global::System.Diagnostics.StackFrame _caller;

		private global::System.DateTime _date;

		private global::WebSocketSharp.LogLevel _level;

		private string _message;

		public global::System.Diagnostics.StackFrame Caller => _caller;

		public global::System.DateTime Date => _date;

		public global::WebSocketSharp.LogLevel Level => _level;

		public string Message => _message;

		internal LogData(global::WebSocketSharp.LogLevel level, global::System.Diagnostics.StackFrame caller, string message)
		{
			_level = level;
			_caller = caller;
			_message = message ?? string.Empty;
			_date = global::System.DateTime.Now;
		}

		public override string ToString()
		{
			string text = $"{_date}|{_level,-5}|";
			global::System.Reflection.MethodBase method = _caller.GetMethod();
			global::System.Type declaringType = method.DeclaringType;
			string arg = $"{text}{declaringType.Name}.{method.Name}|";
			string[] array = _message.Replace("\r\n", "\n").TrimEnd(new char[1] { '\n' }).Split(new char[1] { '\n' });
			if (array.Length <= 1)
			{
				return $"{arg}{_message}";
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder($"{arg}{array[0]}\n", 64);
			string format = $"{{0,{text.Length}}}{{1}}\n";
			for (int i = 1; i < array.Length; i++)
			{
				stringBuilder.AppendFormat(format, "", array[i]);
			}
			stringBuilder.Length--;
			return stringBuilder.ToString();
		}
	}
}
