namespace Unity.VisualScripting
{
	public static class EventBus
	{
		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.EventHook, global::System.Collections.Generic.HashSet<global::System.Delegate>> events;

		internal static global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.EventHook, global::System.Collections.Generic.HashSet<global::System.Delegate>> testAccessEvents => events;

		static EventBus()
		{
			events = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.EventHook, global::System.Collections.Generic.HashSet<global::System.Delegate>>(new global::Unity.VisualScripting.EventHookComparer());
		}

		public static void Register<TArgs>(global::Unity.VisualScripting.EventHook hook, global::System.Action<TArgs> handler)
		{
			if (!events.TryGetValue(hook, out var value))
			{
				value = new global::System.Collections.Generic.HashSet<global::System.Delegate>();
				events.Add(hook, value);
			}
			value.Add(handler);
		}

		public static void Unregister(global::Unity.VisualScripting.EventHook hook, global::System.Delegate handler)
		{
			if (events.TryGetValue(hook, out var value) && value.Remove(handler) && value.Count == 0)
			{
				events.Remove(hook);
			}
		}

		public static void Trigger<TArgs>(global::Unity.VisualScripting.EventHook hook, TArgs args)
		{
			global::System.Collections.Generic.HashSet<global::System.Action<TArgs>> hashSet = null;
			if (events.TryGetValue(hook, out var value))
			{
				foreach (global::System.Delegate item2 in value)
				{
					if (item2 is global::System.Action<TArgs> item)
					{
						if (hashSet == null)
						{
							hashSet = global::Unity.VisualScripting.HashSetPool<global::System.Action<TArgs>>.New();
						}
						hashSet.Add(item);
					}
				}
			}
			if (hashSet == null)
			{
				return;
			}
			foreach (global::System.Action<TArgs> item3 in hashSet)
			{
				if (value.Contains(item3))
				{
					item3(args);
				}
			}
			hashSet.Free();
		}

		public static void Trigger<TArgs>(string name, global::UnityEngine.GameObject target, TArgs args)
		{
			Trigger(new global::Unity.VisualScripting.EventHook(name, target), args);
		}

		public static void Trigger(global::Unity.VisualScripting.EventHook hook)
		{
			Trigger(hook, default(global::Unity.VisualScripting.EmptyEventArgs));
		}

		public static void Trigger(string name, global::UnityEngine.GameObject target)
		{
			Trigger(new global::Unity.VisualScripting.EventHook(name, target));
		}
	}
}
