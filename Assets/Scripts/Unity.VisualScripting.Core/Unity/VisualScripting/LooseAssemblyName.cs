namespace Unity.VisualScripting
{
	public struct LooseAssemblyName
	{
		public readonly string name;

		public LooseAssemblyName(string name)
		{
			if (name == null)
			{
				throw new global::System.ArgumentNullException("name");
			}
			this.name = name;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is global::Unity.VisualScripting.LooseAssemblyName))
			{
				return false;
			}
			return ((global::Unity.VisualScripting.LooseAssemblyName)obj).name == name;
		}

		public override int GetHashCode()
		{
			return global::Unity.VisualScripting.HashUtility.GetHashCode(name);
		}

		public static bool operator ==(global::Unity.VisualScripting.LooseAssemblyName a, global::Unity.VisualScripting.LooseAssemblyName b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(global::Unity.VisualScripting.LooseAssemblyName a, global::Unity.VisualScripting.LooseAssemblyName b)
		{
			return !(a == b);
		}

		public static implicit operator global::Unity.VisualScripting.LooseAssemblyName(string name)
		{
			return new global::Unity.VisualScripting.LooseAssemblyName(name);
		}

		public static implicit operator string(global::Unity.VisualScripting.LooseAssemblyName name)
		{
			return name.name;
		}

		public static explicit operator global::Unity.VisualScripting.LooseAssemblyName(global::System.Reflection.AssemblyName strongAssemblyName)
		{
			return new global::Unity.VisualScripting.LooseAssemblyName(strongAssemblyName.Name);
		}

		public override string ToString()
		{
			return name;
		}
	}
}
