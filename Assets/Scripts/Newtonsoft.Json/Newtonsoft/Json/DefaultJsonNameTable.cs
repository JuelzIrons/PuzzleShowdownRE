namespace Newtonsoft.Json
{
	public class DefaultJsonNameTable : global::Newtonsoft.Json.JsonNameTable
	{
		private class Entry
		{
			internal readonly string Value;

			internal readonly int HashCode;

			internal global::Newtonsoft.Json.DefaultJsonNameTable.Entry Next;

			internal Entry(string value, int hashCode, global::Newtonsoft.Json.DefaultJsonNameTable.Entry next)
			{
				Value = value;
				HashCode = hashCode;
				Next = next;
			}
		}

		private static readonly int HashCodeRandomizer;

		private int _count;

		private global::Newtonsoft.Json.DefaultJsonNameTable.Entry[] _entries;

		private int _mask = 31;

		static DefaultJsonNameTable()
		{
			HashCodeRandomizer = global::System.Environment.TickCount;
		}

		public DefaultJsonNameTable()
		{
			_entries = new global::Newtonsoft.Json.DefaultJsonNameTable.Entry[_mask + 1];
		}

		public override string? Get(char[] key, int start, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			int num = length + HashCodeRandomizer;
			num += (num << 7) ^ key[start];
			int num2 = start + length;
			for (int i = start + 1; i < num2; i++)
			{
				num += (num << 7) ^ key[i];
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			int num3 = global::System.Threading.Volatile.Read(ref _mask);
			int num4 = num & num3;
			for (global::Newtonsoft.Json.DefaultJsonNameTable.Entry entry = _entries[num4]; entry != null; entry = entry.Next)
			{
				if (entry.HashCode == num && TextEquals(entry.Value, key, start, length))
				{
					return entry.Value;
				}
			}
			return null;
		}

		public string Add(string key)
		{
			if (key == null)
			{
				throw new global::System.ArgumentNullException("key");
			}
			int length = key.Length;
			if (length == 0)
			{
				return string.Empty;
			}
			int num = length + HashCodeRandomizer;
			for (int i = 0; i < key.Length; i++)
			{
				num += (num << 7) ^ key[i];
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (global::Newtonsoft.Json.DefaultJsonNameTable.Entry entry = _entries[num & _mask]; entry != null; entry = entry.Next)
			{
				if (entry.HashCode == num && entry.Value.Equals(key, global::System.StringComparison.Ordinal))
				{
					return entry.Value;
				}
			}
			return AddEntry(key, num);
		}

		private string AddEntry(string str, int hashCode)
		{
			int num = hashCode & _mask;
			global::Newtonsoft.Json.DefaultJsonNameTable.Entry entry = new global::Newtonsoft.Json.DefaultJsonNameTable.Entry(str, hashCode, _entries[num]);
			_entries[num] = entry;
			if (_count++ == _mask)
			{
				Grow();
			}
			return entry.Value;
		}

		private void Grow()
		{
			global::Newtonsoft.Json.DefaultJsonNameTable.Entry[] entries = _entries;
			int num = _mask * 2 + 1;
			global::Newtonsoft.Json.DefaultJsonNameTable.Entry[] array = new global::Newtonsoft.Json.DefaultJsonNameTable.Entry[num + 1];
			for (int i = 0; i < entries.Length; i++)
			{
				global::Newtonsoft.Json.DefaultJsonNameTable.Entry entry = entries[i];
				while (entry != null)
				{
					int num2 = entry.HashCode & num;
					global::Newtonsoft.Json.DefaultJsonNameTable.Entry next = entry.Next;
					entry.Next = array[num2];
					array[num2] = entry;
					entry = next;
				}
			}
			_entries = array;
			global::System.Threading.Volatile.Write(ref _mask, num);
		}

		private static bool TextEquals(string str1, char[] str2, int str2Start, int str2Length)
		{
			if (str1.Length != str2Length)
			{
				return false;
			}
			for (int i = 0; i < str1.Length; i++)
			{
				if (str1[i] != str2[str2Start + i])
				{
					return false;
				}
			}
			return true;
		}
	}
}
