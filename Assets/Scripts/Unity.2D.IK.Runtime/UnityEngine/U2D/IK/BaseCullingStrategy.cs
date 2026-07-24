namespace UnityEngine.U2D.IK
{
	internal abstract class BaseCullingStrategy
	{
		private bool m_IsCullingEnabled;

		private global::System.Collections.Generic.HashSet<object> m_RequestingManagers;

		public bool enabled => m_IsCullingEnabled;

		public abstract bool AreBonesVisible(global::System.Collections.Generic.IList<int> transformIds);

		public void AddRequestingObject(object requestingObject)
		{
			if (!m_IsCullingEnabled)
			{
				m_IsCullingEnabled = true;
				Initialize();
			}
			m_RequestingManagers.Add(requestingObject);
		}

		public void RemoveRequestingObject(object requestingObject)
		{
			if (m_RequestingManagers.Remove(requestingObject) && m_RequestingManagers.Count == 0)
			{
				m_IsCullingEnabled = false;
				Disable();
			}
		}

		public void Initialize()
		{
			m_RequestingManagers = new global::System.Collections.Generic.HashSet<object>();
			OnInitialize();
		}

		public void Update()
		{
			OnUpdate();
		}

		public void Disable()
		{
			OnDisable();
		}

		protected virtual void OnInitialize()
		{
		}

		protected virtual void OnUpdate()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
