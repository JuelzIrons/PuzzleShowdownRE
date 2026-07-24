namespace Unity.VisualScripting
{
	public static class ComponentHolderProtocol
	{
		public static bool IsComponentHolderType(global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			if (!typeof(global::UnityEngine.GameObject).IsAssignableFrom(type))
			{
				return typeof(global::UnityEngine.Component).IsAssignableFrom(type);
			}
			return true;
		}

		public static bool IsComponentHolder(this global::UnityEngine.Object uo)
		{
			if (!(uo is global::UnityEngine.GameObject))
			{
				return uo is global::UnityEngine.Component;
			}
			return true;
		}

		public static global::UnityEngine.GameObject GameObject(this global::UnityEngine.Object uo)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return (global::UnityEngine.GameObject)uo;
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).gameObject;
			}
			return null;
		}

		public static T AddComponent<T>(this global::UnityEngine.Object uo) where T : global::UnityEngine.Component
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).AddComponent<T>();
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).gameObject.AddComponent<T>();
			}
			throw new global::System.NotSupportedException();
		}

		public static T GetOrAddComponent<T>(this global::UnityEngine.Object uo) where T : global::UnityEngine.Component
		{
			T val = uo.GetComponent<T>();
			if (!val)
			{
				val = uo.AddComponent<T>();
			}
			return val;
		}

		public static T GetComponent<T>(this global::UnityEngine.Object uo)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponent<T>();
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponent<T>();
			}
			throw new global::System.NotSupportedException();
		}

		public static T GetComponentInChildren<T>(this global::UnityEngine.Object uo)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponentInChildren<T>();
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponentInChildren<T>();
			}
			throw new global::System.NotSupportedException();
		}

		public static T GetComponentInParent<T>(this global::UnityEngine.Object uo)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponentInParent<T>();
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponentInParent<T>();
			}
			throw new global::System.NotSupportedException();
		}

		public static T[] GetComponents<T>(this global::UnityEngine.Object uo)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponents<T>();
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponents<T>();
			}
			throw new global::System.NotSupportedException();
		}

		public static T[] GetComponentsInChildren<T>(this global::UnityEngine.Object uo)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponentsInChildren<T>();
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponentsInChildren<T>();
			}
			throw new global::System.NotSupportedException();
		}

		public static T[] GetComponentsInParent<T>(this global::UnityEngine.Object uo)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponentsInParent<T>();
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponentsInParent<T>();
			}
			throw new global::System.NotSupportedException();
		}

		public static global::UnityEngine.Component GetComponent(this global::UnityEngine.Object uo, global::System.Type type)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponent(type);
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponent(type);
			}
			throw new global::System.NotSupportedException();
		}

		public static global::UnityEngine.Component GetComponentInChildren(this global::UnityEngine.Object uo, global::System.Type type)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponentInChildren(type);
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponentInChildren(type);
			}
			throw new global::System.NotSupportedException();
		}

		public static global::UnityEngine.Component GetComponentInParent(this global::UnityEngine.Object uo, global::System.Type type)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponentInParent(type);
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponentInParent(type);
			}
			throw new global::System.NotSupportedException();
		}

		public static global::UnityEngine.Component[] GetComponents(this global::UnityEngine.Object uo, global::System.Type type)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponents(type);
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponents(type);
			}
			throw new global::System.NotSupportedException();
		}

		public static global::UnityEngine.Component[] GetComponentsInChildren(this global::UnityEngine.Object uo, global::System.Type type)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponentsInChildren(type);
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponentsInChildren(type);
			}
			throw new global::System.NotSupportedException();
		}

		public static global::UnityEngine.Component[] GetComponentsInParent(this global::UnityEngine.Object uo, global::System.Type type)
		{
			if (uo is global::UnityEngine.GameObject)
			{
				return ((global::UnityEngine.GameObject)uo).GetComponentsInParent(type);
			}
			if (uo is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)uo).GetComponentsInParent(type);
			}
			throw new global::System.NotSupportedException();
		}
	}
}
