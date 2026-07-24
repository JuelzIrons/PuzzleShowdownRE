namespace UnityEngine.EventSystems
{
	public abstract class BaseRaycaster : global::UnityEngine.EventSystems.UIBehaviour
	{
		private global::UnityEngine.EventSystems.BaseRaycaster m_RootRaycaster;

		public abstract global::UnityEngine.Camera eventCamera { get; }

		[global::System.Obsolete("Please use sortOrderPriority and renderOrderPriority", false)]
		public virtual int priority => 0;

		public virtual int sortOrderPriority => int.MinValue;

		public virtual int renderOrderPriority => int.MinValue;

		public global::UnityEngine.EventSystems.BaseRaycaster rootRaycaster
		{
			get
			{
				if (m_RootRaycaster == null)
				{
					global::UnityEngine.EventSystems.BaseRaycaster[] componentsInParent = GetComponentsInParent<global::UnityEngine.EventSystems.BaseRaycaster>();
					if (componentsInParent.Length != 0)
					{
						m_RootRaycaster = componentsInParent[^1];
					}
				}
				return m_RootRaycaster;
			}
		}

		public abstract void Raycast(global::UnityEngine.EventSystems.PointerEventData eventData, global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> resultAppendList);

		public override string ToString()
		{
			return "Name: " + base.gameObject?.ToString() + "\neventCamera: " + eventCamera?.ToString() + "\nsortOrderPriority: " + sortOrderPriority + "\nrenderOrderPriority: " + renderOrderPriority;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			global::UnityEngine.EventSystems.RaycasterManager.AddRaycaster(this);
		}

		protected override void OnDisable()
		{
			global::UnityEngine.EventSystems.RaycasterManager.RemoveRaycasters(this);
			base.OnDisable();
		}

		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			m_RootRaycaster = null;
		}

		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			m_RootRaycaster = null;
		}
	}
}
