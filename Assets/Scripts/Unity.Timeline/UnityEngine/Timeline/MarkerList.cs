namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	internal struct MarkerList : global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject> m_Objects;

		[global::System.NonSerialized]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.Timeline.IMarker> m_Cache;

		private bool m_CacheDirty;

		private bool m_HasNotifications;

		public global::System.Collections.Generic.List<global::UnityEngine.Timeline.IMarker> markers
		{
			get
			{
				BuildCache();
				return m_Cache;
			}
		}

		public int Count => markers.Count;

		public global::UnityEngine.Timeline.IMarker this[int idx] => markers[idx];

		public MarkerList(int capacity)
		{
			m_Objects = new global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject>(capacity);
			m_Cache = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.IMarker>(capacity);
			m_CacheDirty = true;
			m_HasNotifications = false;
		}

		public void Add(global::UnityEngine.ScriptableObject item)
		{
			if (!(item == null))
			{
				m_Objects.Add(item);
				m_CacheDirty = true;
			}
		}

		public bool Remove(global::UnityEngine.Timeline.IMarker item)
		{
			if (!(item is global::UnityEngine.ScriptableObject))
			{
				throw new global::System.InvalidOperationException("Supplied type must be a ScriptableObject");
			}
			return Remove((global::UnityEngine.ScriptableObject)item, item.parent.timelineAsset, item.parent);
		}

		public bool Remove(global::UnityEngine.ScriptableObject item, global::UnityEngine.Timeline.TimelineAsset timelineAsset, global::UnityEngine.Playables.PlayableAsset thingToDirty)
		{
			if (!m_Objects.Contains(item))
			{
				return false;
			}
			m_Objects.Remove(item);
			m_CacheDirty = true;
			global::UnityEngine.Timeline.TimelineUndo.PushDestroyUndo(timelineAsset, thingToDirty, item);
			return true;
		}

		public void Clear()
		{
			m_Objects.Clear();
			m_CacheDirty = true;
		}

		public bool Contains(global::UnityEngine.ScriptableObject item)
		{
			return m_Objects.Contains(item);
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.IMarker> GetMarkers()
		{
			return markers;
		}

		public global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject> GetRawMarkerList()
		{
			return m_Objects;
		}

		public global::UnityEngine.Timeline.IMarker CreateMarker(global::System.Type type, double time, global::UnityEngine.Timeline.TrackAsset owner)
		{
			if (!typeof(global::UnityEngine.ScriptableObject).IsAssignableFrom(type) || !typeof(global::UnityEngine.Timeline.IMarker).IsAssignableFrom(type))
			{
				throw new global::System.InvalidOperationException("The requested type needs to inherit from ScriptableObject and implement IMarker");
			}
			if (!owner.supportsNotifications && typeof(global::UnityEngine.Playables.INotification).IsAssignableFrom(type))
			{
				throw new global::System.InvalidOperationException("Markers implementing the INotification interface cannot be added on tracks that do not support notifications");
			}
			global::UnityEngine.ScriptableObject scriptableObject = global::UnityEngine.ScriptableObject.CreateInstance(type);
			global::UnityEngine.Timeline.IMarker obj = (global::UnityEngine.Timeline.IMarker)scriptableObject;
			obj.time = time;
			global::UnityEngine.Timeline.TimelineCreateUtilities.SaveAssetIntoObject(scriptableObject, owner);
			Add(scriptableObject);
			obj.Initialize(owner);
			return obj;
		}

		public bool HasNotifications()
		{
			BuildCache();
			return m_HasNotifications;
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			m_CacheDirty = true;
		}

		private void BuildCache()
		{
			if (!m_CacheDirty)
			{
				return;
			}
			m_Cache = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.IMarker>(m_Objects.Count);
			m_HasNotifications = false;
			foreach (global::UnityEngine.ScriptableObject @object in m_Objects)
			{
				if (@object != null)
				{
					m_Cache.Add(@object as global::UnityEngine.Timeline.IMarker);
					if (@object is global::UnityEngine.Playables.INotification)
					{
						m_HasNotifications = true;
					}
				}
			}
			m_CacheDirty = false;
		}
	}
}
