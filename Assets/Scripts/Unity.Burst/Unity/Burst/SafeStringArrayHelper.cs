namespace Unity.Burst
{
	internal static class SafeStringArrayHelper
	{
		public static string SerialiseStringArraySafe(string[] array)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			foreach (string text in array)
			{
				stringBuilder.Append($"{(global::System.Text.Encoding.UTF8.GetByteCount(text))}]");
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		public static string[] DeserialiseStringArraySafe(string input)
		{
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(input);
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			int num = 0;
			int num2 = bytes.Length;
			while (num < num2)
			{
				int num3 = 0;
				while (true)
				{
					if (num >= num2)
					{
						throw new global::System.FormatException("Invalid input `" + input + "`: reached end while reading length");
					}
					byte b = bytes[num];
					switch (b)
					{
					case 93:
						break;
					default:
						throw new global::System.FormatException($"Invalid input `{input}` at {num}: Got non-digit character while reading length");
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 57:
						goto IL_006b;
					}
					break;
					IL_006b:
					num3 = num3 * 10 + (b - 48);
					num++;
				}
				num++;
				list.Add(global::System.Text.Encoding.UTF8.GetString(bytes, num, num3));
				num += num3;
			}
			return list.ToArray();
		}
	}
}
