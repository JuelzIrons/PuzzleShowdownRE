namespace UnityEngine.Rendering
{
	public sealed class VolumeStack : global::System.IDisposable
	{
		internal readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::UnityEngine.Rendering.VolumeComponent> components = new global::System.Collections.Generic.Dictionary<global::System.Type, global::UnityEngine.Rendering.VolumeComponent>();

		internal global::UnityEngine.Rendering.VolumeParameter[] parameters;

		internal bool requiresReset = true;

		internal bool requiresResetForAllProperties = true;

		public bool isValid { get; private set; }

		internal VolumeStack()
		{
		}

		internal void Clear()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::UnityEngine.Rendering.VolumeComponent> component in components)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(component.Value);
			}
			components.Clear();
			parameters = null;
		}

		internal void Reload(global::System.Type[] componentTypes)
		{
			Clear();
			requiresReset = true;
			requiresResetForAllProperties = true;
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeParameter> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeParameter>();
			foreach (global::System.Type type in componentTypes)
			{
				global::UnityEngine.Rendering.VolumeComponent volumeComponent = (global::UnityEngine.Rendering.VolumeComponent)global::UnityEngine.ScriptableObject.CreateInstance(type);
				components.Add(type, volumeComponent);
				list.AddRange(volumeComponent.parameters);
			}
			parameters = list.ToArray();
			isValid = true;
		}

		public T GetComponent<T>() where T : global::UnityEngine.Rendering.VolumeComponent
		{
			return (T)GetComponent(typeof(T));
		}

		public global::UnityEngine.Rendering.VolumeComponent GetComponent(global::System.Type type)
		{
			components.TryGetValue(type, out var value);
			return value;
		}

		public void Dispose()
		{
			Clear();
			isValid = false;
		}
	}
}
