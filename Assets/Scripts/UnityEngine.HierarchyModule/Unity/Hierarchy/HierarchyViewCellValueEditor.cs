namespace Unity.Hierarchy
{
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.UIToolkitAuthoringModule" })]
	internal sealed class HierarchyViewCellValueEditor<TModel, TEditor, TValue> where TEditor : global::UnityEngine.UIElements.VisualElement, global::UnityEngine.UIElements.INotifyValueChanged<TValue>, new()
	{
		private readonly global::System.Func<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue> m_GetModelValue;

		private readonly global::System.Action<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue> m_SetModelValue;

		private readonly global::System.Func<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue, bool> m_IsDefaultValue;

		private readonly global::System.Action<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue> m_OnSetEditorValue;

		public TEditor Element;

		public TModel Model { get; private set; }

		public global::Unity.Hierarchy.HierarchyViewCell Cell { get; private set; }

		public HierarchyViewCellValueEditor(global::System.Func<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue> getModelValue, global::System.Action<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue> setModelValue, global::System.Func<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue, bool> isDefaultValue, global::System.Action<global::Unity.Hierarchy.HierarchyViewCellValueEditor<TModel, TEditor, TValue>, TValue> onSetEditorValue = null)
		{
			m_GetModelValue = getModelValue;
			m_SetModelValue = setModelValue;
			m_IsDefaultValue = isDefaultValue;
			m_OnSetEditorValue = onSetEditorValue;
		}

		public void Bind(TModel model, global::Unity.Hierarchy.HierarchyViewCell cell, TEditor editor)
		{
			Model = model;
			Cell = cell;
			Cell.userData = this;
			Element = editor;
			Element.visible = true;
			Element.RegisterCallback<global::UnityEngine.UIElements.ChangeEvent<TValue>>(SetModelValue);
			SyncEditorValueWithoutNotify();
		}

		public void Unbind()
		{
			Cell.userData = null;
			Cell = null;
			Element.visible = false;
			Element.UnregisterCallback<global::UnityEngine.UIElements.ChangeEvent<TValue>>(SetModelValue);
			Element = null;
		}

		public TValue GetModelValue()
		{
			return m_GetModelValue(this);
		}

		public void SetModelValue(TValue value)
		{
			if (Cell != null)
			{
				if (!GetModelValue().Equals(value))
				{
					m_SetModelValue(this, value);
				}
				Cell.IsDefaultValue = IsModelDefaultValue();
			}
		}

		public TValue GetEditorValue()
		{
			return Element.value;
		}

		public void SetModelValue(global::UnityEngine.UIElements.ChangeEvent<TValue> evt)
		{
			SetModelValue(evt.newValue);
		}

		public void SetEditorValueWithoutNotify(TValue value)
		{
			if (!value.Equals(Element.value))
			{
				Element.SetValueWithoutNotify(value);
			}
			m_OnSetEditorValue?.Invoke(this, value);
			Cell.IsDefaultValue = IsModelDefaultValue();
		}

		public void SyncEditorValueWithoutNotify()
		{
			SetEditorValueWithoutNotify(GetModelValue());
		}

		public bool IsModelDefaultValue()
		{
			return m_IsDefaultValue(this, GetModelValue());
		}
	}
}
