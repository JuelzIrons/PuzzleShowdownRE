namespace Unity.VisualScripting.Antlr3.Runtime.Collections
{
	public class CollectionUtils
	{
		public static string ListToString(global::System.Collections.IList coll)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			if (coll != null)
			{
				stringBuilder.Append("[");
				for (int i = 0; i < coll.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					object obj = coll[i];
					if (obj == null)
					{
						stringBuilder.Append("null");
					}
					else if (obj is global::System.Collections.IDictionary)
					{
						stringBuilder.Append(DictionaryToString((global::System.Collections.IDictionary)obj));
					}
					else if (obj is global::System.Collections.IList)
					{
						stringBuilder.Append(ListToString((global::System.Collections.IList)obj));
					}
					else
					{
						stringBuilder.Append(obj.ToString());
					}
				}
				stringBuilder.Append("]");
			}
			else
			{
				stringBuilder.Insert(0, "null");
			}
			return stringBuilder.ToString();
		}

		public static string DictionaryToString(global::System.Collections.IDictionary dict)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			if (dict != null)
			{
				stringBuilder.Append("{");
				int num = 0;
				foreach (global::System.Collections.DictionaryEntry item in dict)
				{
					if (num > 0)
					{
						stringBuilder.Append(", ");
					}
					if (item.Value is global::System.Collections.IDictionary)
					{
						stringBuilder.AppendFormat("{0}={1}", item.Key.ToString(), DictionaryToString((global::System.Collections.IDictionary)item.Value));
					}
					else if (item.Value is global::System.Collections.IList)
					{
						stringBuilder.AppendFormat("{0}={1}", item.Key.ToString(), ListToString((global::System.Collections.IList)item.Value));
					}
					else
					{
						stringBuilder.AppendFormat("{0}={1}", item.Key.ToString(), item.Value.ToString());
					}
					num++;
				}
				stringBuilder.Append("}");
			}
			else
			{
				stringBuilder.Insert(0, "null");
			}
			return stringBuilder.ToString();
		}
	}
}
