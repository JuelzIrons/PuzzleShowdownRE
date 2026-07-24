namespace UnityEngine.InputSystem.Users
{
	public struct InputUserAccountHandle : global::System.IEquatable<global::UnityEngine.InputSystem.Users.InputUserAccountHandle>
	{
		private string m_ApiName;

		private ulong m_Handle;

		public string apiName => m_ApiName;

		public ulong handle => m_Handle;

		public InputUserAccountHandle(string apiName, ulong handle)
		{
			if (string.IsNullOrEmpty(apiName))
			{
				throw new global::System.ArgumentNullException("apiName");
			}
			m_ApiName = apiName;
			m_Handle = handle;
		}

		public override string ToString()
		{
			if (m_ApiName == null)
			{
				return base.ToString();
			}
			return $"{m_ApiName}({m_Handle})";
		}

		public bool Equals(global::UnityEngine.InputSystem.Users.InputUserAccountHandle other)
		{
			if (string.Equals(apiName, other.apiName))
			{
				return object.Equals(handle, other.handle);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.InputSystem.Users.InputUserAccountHandle)
			{
				return Equals((global::UnityEngine.InputSystem.Users.InputUserAccountHandle)obj);
			}
			return false;
		}

		public static bool operator ==(global::UnityEngine.InputSystem.Users.InputUserAccountHandle left, global::UnityEngine.InputSystem.Users.InputUserAccountHandle right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.InputSystem.Users.InputUserAccountHandle left, global::UnityEngine.InputSystem.Users.InputUserAccountHandle right)
		{
			return !left.Equals(right);
		}

		public override int GetHashCode()
		{
			return (((apiName != null) ? apiName.GetHashCode() : 0) * 397) ^ handle.GetHashCode();
		}
	}
}
