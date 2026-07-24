namespace UnityEngine.UIElements
{
	[global::UnityEngine.AddComponentMenu("UI Toolkit/Panel Raycaster (UI Toolkit)")]
	public class PanelRaycaster : global::UnityEngine.EventSystems.BaseRaycaster, global::UnityEngine.UIElements.IRuntimePanelComponent
	{
		private global::UnityEngine.UIElements.BaseRuntimePanel m_Panel;

		private static global::UnityEngine.UIElements.ScreenOverlayPanelPicker panelPicker = new global::UnityEngine.UIElements.ScreenOverlayPanelPicker();

		public global::UnityEngine.UIElements.IPanel panel
		{
			get
			{
				return m_Panel;
			}
			set
			{
				global::UnityEngine.UIElements.BaseRuntimePanel baseRuntimePanel = (global::UnityEngine.UIElements.BaseRuntimePanel)value;
				if (m_Panel != baseRuntimePanel)
				{
					UnregisterCallbacks();
					m_Panel = baseRuntimePanel;
					RegisterCallbacks();
				}
			}
		}

		private global::UnityEngine.GameObject selectableGameObject => m_Panel?.selectableGameObject;

		public override int sortOrderPriority => global::UnityEngine.Mathf.FloorToInt(m_Panel?.sortingPriority ?? 0f);

		public override int renderOrderPriority => int.MaxValue - (global::UnityEngine.UIElements.UIElementsRuntimeUtility.s_ResolvedSortingIndexMax - (m_Panel?.resolvedSortingIndex ?? 0));

		public override global::UnityEngine.Camera eventCamera => null;

		private void RegisterCallbacks()
		{
			if (m_Panel != null)
			{
				m_Panel.destroyed += OnPanelDestroyed;
			}
		}

		private void UnregisterCallbacks()
		{
			if (m_Panel != null)
			{
				m_Panel.destroyed -= OnPanelDestroyed;
			}
		}

		private void OnPanelDestroyed()
		{
			panel = null;
		}

		public override void Raycast(global::UnityEngine.EventSystems.PointerEventData eventData, global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> resultAppendList)
		{
			if (m_Panel == null || !m_Panel.isFlat)
			{
				return;
			}
			int targetDisplay = m_Panel.targetDisplay;
			global::UnityEngine.Vector3 relativeMousePositionForRaycast = global::UnityEngine.UI.MultipleDisplayUtilities.GetRelativeMousePositionForRaycast(eventData);
			global::UnityEngine.Vector3 vector = relativeMousePositionForRaycast;
			global::UnityEngine.Vector2 delta = eventData.delta;
			float num = global::UnityEngine.Screen.height;
			if (global::UnityEngineInternal.DisplayInternal.IsASecondaryDisplayIndex(targetDisplay))
			{
				num = global::UnityEngine.Display.displays[targetDisplay].systemHeight;
			}
			vector.y = num - vector.y;
			delta.y = 0f - delta.y;
			global::UnityEngine.EventSystems.BaseInputModule currentInputModule = eventData.currentInputModule;
			if (!(currentInputModule == null))
			{
				int pointerId = currentInputModule.ConvertUIToolkitPointerId(eventData);
				if (panelPicker.TryPick((global::UnityEngine.UIElements.RuntimePanel)m_Panel, pointerId, vector, delta, (int)relativeMousePositionForRaycast.z, out var _))
				{
					resultAppendList.Add(new global::UnityEngine.EventSystems.RaycastResult
					{
						gameObject = selectableGameObject,
						module = this,
						screenPosition = relativeMousePositionForRaycast,
						displayIndex = m_Panel.targetDisplay
					});
				}
			}
		}
	}
}
