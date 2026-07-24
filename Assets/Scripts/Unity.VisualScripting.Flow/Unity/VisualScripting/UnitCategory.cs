namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	[global::Unity.VisualScripting.FullSerializer.fsObject(Converter = typeof(global::Unity.VisualScripting.UnitCategoryConverter))]
	public class UnitCategory : global::System.Attribute
	{
		public global::Unity.VisualScripting.UnitCategory root { get; }

		public global::Unity.VisualScripting.UnitCategory parent { get; }

		public string fullName { get; }

		public string name { get; }

		public bool isRoot { get; }

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.UnitCategory> ancestors
		{
			get
			{
				global::Unity.VisualScripting.UnitCategory ancestor = parent;
				while (ancestor != null)
				{
					yield return ancestor;
					ancestor = ancestor.parent;
				}
			}
		}

		public UnitCategory(string fullName)
		{
			global::Unity.VisualScripting.Ensure.That("fullName").IsNotNull(fullName);
			fullName = fullName.Replace('\\', '/');
			this.fullName = fullName;
			string[] array = fullName.Split('/');
			name = array[^1];
			if (array.Length > 1)
			{
				root = new global::Unity.VisualScripting.UnitCategory(array[0]);
				parent = new global::Unity.VisualScripting.UnitCategory(fullName.Substring(0, fullName.LastIndexOf('/')));
			}
			else
			{
				root = this;
				isRoot = true;
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.UnitCategory> AndAncestors()
		{
			yield return this;
			foreach (global::Unity.VisualScripting.UnitCategory ancestor in ancestors)
			{
				yield return ancestor;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is global::Unity.VisualScripting.UnitCategory)
			{
				return ((global::Unity.VisualScripting.UnitCategory)obj).fullName == fullName;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return fullName.GetHashCode();
		}

		public override string ToString()
		{
			return fullName;
		}

		public static bool operator ==(global::Unity.VisualScripting.UnitCategory a, global::Unity.VisualScripting.UnitCategory b)
		{
			if ((object)a == b)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.Equals(b);
		}

		public static bool operator !=(global::Unity.VisualScripting.UnitCategory a, global::Unity.VisualScripting.UnitCategory b)
		{
			return !(a == b);
		}
	}
}
