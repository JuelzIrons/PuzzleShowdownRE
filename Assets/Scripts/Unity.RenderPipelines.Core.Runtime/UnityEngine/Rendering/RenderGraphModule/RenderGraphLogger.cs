namespace UnityEngine.Rendering.RenderGraphModule
{
	internal class RenderGraphLogger
	{
		private global::System.Collections.Generic.Dictionary<string, global::System.Text.StringBuilder> m_LogMap = new global::System.Collections.Generic.Dictionary<string, global::System.Text.StringBuilder>();

		private global::System.Text.StringBuilder m_CurrentBuilder;

		private int m_CurrentIndentation;

		public void Initialize(string logName)
		{
			if (!m_LogMap.TryGetValue(logName, out var value))
			{
				value = new global::System.Text.StringBuilder();
				m_LogMap.Add(logName, value);
			}
			m_CurrentBuilder = value;
			m_CurrentBuilder.Clear();
			m_CurrentIndentation = 0;
		}

		public void IncrementIndentation(int value)
		{
			m_CurrentIndentation += global::System.Math.Abs(value);
		}

		public void DecrementIndentation(int value)
		{
			m_CurrentIndentation = global::System.Math.Max(0, m_CurrentIndentation - global::System.Math.Abs(value));
		}

		public void LogLine(string format, params object[] args)
		{
			for (int i = 0; i < m_CurrentIndentation; i++)
			{
				m_CurrentBuilder.Append('\t');
			}
			m_CurrentBuilder.AppendFormat(format, args);
			m_CurrentBuilder.AppendLine();
		}

		public void FlushLogs()
		{
			string text = "";
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Text.StringBuilder> item in m_LogMap)
			{
				global::System.Text.StringBuilder value = item.Value;
				value.AppendLine();
				text += value.ToString();
			}
			m_LogMap.Clear();
			global::UnityEngine.Debug.Log(text);
		}
	}
}
