namespace UnityEngine.Rendering
{
	public sealed class VolumeProfile : global::UnityEngine.ScriptableObject
	{
		[global::System.Flags]
		internal enum DirtyState
		{
			None = 0,
			DirtyByComponentChange = 1,
			DirtyByProfileReset = 2,
			Other = 4
		}

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeComponent> components = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeComponent>();

		internal global::UnityEngine.Rendering.VolumeProfile.DirtyState dirtyState;

		[global::System.Obsolete("This field was only public for editor access. #from(6000.0)")]
		public bool isDirty
		{
			get
			{
				return dirtyState != global::UnityEngine.Rendering.VolumeProfile.DirtyState.None;
			}
			set
			{
				if (value)
				{
					dirtyState |= global::UnityEngine.Rendering.VolumeProfile.DirtyState.Other;
				}
				else
				{
					dirtyState &= ~global::UnityEngine.Rendering.VolumeProfile.DirtyState.Other;
				}
			}
		}

		private void OnEnable()
		{
			components.RemoveAll((global::UnityEngine.Rendering.VolumeComponent x) => x == null);
		}

		internal void OnDisable()
		{
			if (components == null)
			{
				return;
			}
			for (int i = 0; i < components.Count; i++)
			{
				if (components[i] != null)
				{
					components[i].Release();
				}
			}
		}

		public void Reset()
		{
			dirtyState |= global::UnityEngine.Rendering.VolumeProfile.DirtyState.DirtyByProfileReset;
		}

		public T Add<T>(bool overrides = false) where T : global::UnityEngine.Rendering.VolumeComponent
		{
			return (T)Add(typeof(T), overrides);
		}

		public global::UnityEngine.Rendering.VolumeComponent Add(global::System.Type type, bool overrides = false)
		{
			if (Has(type))
			{
				throw new global::System.InvalidOperationException("Component already exists in the volume");
			}
			global::UnityEngine.Rendering.VolumeComponent volumeComponent = (global::UnityEngine.Rendering.VolumeComponent)global::UnityEngine.ScriptableObject.CreateInstance(type);
			volumeComponent.SetAllOverridesTo(overrides);
			components.Add(volumeComponent);
			dirtyState |= global::UnityEngine.Rendering.VolumeProfile.DirtyState.DirtyByComponentChange;
			return volumeComponent;
		}

		public void Remove<T>() where T : global::UnityEngine.Rendering.VolumeComponent
		{
			Remove(typeof(T));
		}

		public void Remove(global::System.Type type)
		{
			int num = -1;
			for (int i = 0; i < components.Count; i++)
			{
				if (components[i].GetType() == type)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				components.RemoveAt(num);
				dirtyState |= global::UnityEngine.Rendering.VolumeProfile.DirtyState.DirtyByComponentChange;
			}
		}

		public bool Has<T>() where T : global::UnityEngine.Rendering.VolumeComponent
		{
			return Has(typeof(T));
		}

		public bool Has(global::System.Type type)
		{
			foreach (global::UnityEngine.Rendering.VolumeComponent component in components)
			{
				if (component.GetType() == type)
				{
					return true;
				}
			}
			return false;
		}

		public bool HasSubclassOf(global::System.Type type)
		{
			foreach (global::UnityEngine.Rendering.VolumeComponent component in components)
			{
				if (component.GetType().IsSubclassOf(type))
				{
					return true;
				}
			}
			return false;
		}

		public bool TryGet<T>(out T component) where T : global::UnityEngine.Rendering.VolumeComponent
		{
			return TryGet<T>(typeof(T), out component);
		}

		public bool TryGet<T>(global::System.Type type, out T component) where T : global::UnityEngine.Rendering.VolumeComponent
		{
			component = null;
			foreach (global::UnityEngine.Rendering.VolumeComponent component2 in components)
			{
				if (component2.GetType() == type)
				{
					component = (T)component2;
					return true;
				}
			}
			return false;
		}

		public bool TryGetSubclassOf<T>(global::System.Type type, out T component) where T : global::UnityEngine.Rendering.VolumeComponent
		{
			component = null;
			foreach (global::UnityEngine.Rendering.VolumeComponent component2 in components)
			{
				if (component2.GetType().IsSubclassOf(type))
				{
					component = (T)component2;
					return true;
				}
			}
			return false;
		}

		public bool TryGetAllSubclassOf<T>(global::System.Type type, global::System.Collections.Generic.List<T> result) where T : global::UnityEngine.Rendering.VolumeComponent
		{
			int count = result.Count;
			foreach (global::UnityEngine.Rendering.VolumeComponent component in components)
			{
				if (component.GetType().IsSubclassOf(type))
				{
					result.Add((T)component);
				}
			}
			return count != result.Count;
		}

		public override int GetHashCode()
		{
			int num = 17;
			for (int i = 0; i < components.Count; i++)
			{
				num = num * 23 + components[i].GetHashCode();
			}
			return num;
		}

		internal int GetComponentListHashCode()
		{
			int num = 17;
			for (int i = 0; i < components.Count; i++)
			{
				num = num * 23 + components[i].GetType().GetHashCode();
			}
			return num;
		}

		internal void Sanitize()
		{
			for (int num = components.Count - 1; num >= 0; num--)
			{
				if (components[num] == null)
				{
					components.RemoveAt(num);
				}
			}
		}
	}
}
