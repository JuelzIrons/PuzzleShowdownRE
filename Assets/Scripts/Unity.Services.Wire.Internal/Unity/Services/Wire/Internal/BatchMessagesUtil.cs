namespace Unity.Services.Wire.Internal
{
	internal static class BatchMessagesUtil
	{
		private static global::System.Collections.Generic.IEnumerable<string> SplitMessages(string message)
		{
			string[] pubs = message.Split(new string[1] { "}\n{" }, global::System.StringSplitOptions.None);
			if (pubs.Length > 1)
			{
				FixJsonSplit(ref pubs);
			}
			return pubs;
		}

		public static global::System.Collections.Generic.IEnumerable<string> SplitMessages(byte[] byteMessage)
		{
			return SplitMessages(global::System.Text.Encoding.UTF8.GetString(byteMessage));
		}

		private static void FixJsonSplit(ref string[] pubs)
		{
			for (int i = 0; i < pubs.Length; i++)
			{
				if (i > 0)
				{
					pubs[i] = "{" + pubs[i];
				}
				if (i < pubs.Length - 1)
				{
					pubs[i] += "}";
				}
			}
		}
	}
}
