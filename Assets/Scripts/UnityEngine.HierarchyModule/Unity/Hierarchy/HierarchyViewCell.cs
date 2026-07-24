namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.UIToolkitAuthoringModule" })]
	internal sealed class HierarchyViewCell : global::UnityEngine.UIElements.VisualElement
	{
		private bool m_IsCellBound;

		private bool m_IsDefaultValue;

		public readonly global::Unity.Hierarchy.HierarchyViewColumn Column;

		public readonly global::Unity.Hierarchy.HierarchyView View;

		public global::Unity.Hierarchy.HierarchyNodeTypeHandler Handler { get; internal set; }

		public global::Unity.Hierarchy.HierarchyNode Node { get; internal set; }

		public int NodeIndex { get; internal set; }

		public global::Unity.Hierarchy.HierarchyViewCellDescriptor Descriptor { get; internal set; }

		public object BoundObject { get; set; }

		public bool IsDefaultValue
		{
			get
			{
				return m_IsDefaultValue;
			}
			set
			{
				if (value)
				{
					RemoveFromClassList("non-default-value");
				}
				else
				{
					AddToClassList("non-default-value");
				}
				m_IsDefaultValue = value;
			}
		}

		internal void BindCell()
		{
			if (!m_IsCellBound)
			{
				Descriptor?.BindCell?.Invoke(this);
				m_IsCellBound = true;
			}
		}

		internal void UnbindCell()
		{
			if (!m_IsCellBound)
			{
				return;
			}
			if (Descriptor != null)
			{
				Descriptor?.UnbindCell?.Invoke(this);
				if (Descriptor.ClearCellContent)
				{
					Clear();
				}
			}
			BoundObject = null;
			Node = global::Unity.Hierarchy.HierarchyNode.Null;
			NodeIndex = -1;
			Handler = null;
			Descriptor = null;
			m_IsCellBound = false;
		}

		internal HierarchyViewCell(global::Unity.Hierarchy.HierarchyView view, global::Unity.Hierarchy.HierarchyViewColumn column)
		{
			View = view;
			Column = column;
			base.name = "HierarchyViewCell";
		}

		public override string ToString()
		{
			string id = Column.Descriptor.Id;
			if (Descriptor != null)
			{
				return Descriptor.ToString();
			}
			return $"{Column.Descriptor} - NoCellDesc";
		}
	}
}
