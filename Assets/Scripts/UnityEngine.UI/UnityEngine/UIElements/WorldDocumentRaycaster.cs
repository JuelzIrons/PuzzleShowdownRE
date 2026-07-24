namespace UnityEngine.UIElements
{
	[global::UnityEngine.AddComponentMenu("UI Toolkit/World Document Raycaster (UI Toolkit)")]
	public class WorldDocumentRaycaster : global::UnityEngine.EventSystems.BaseRaycaster
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Camera m_EventCamera;

		private static global::UnityEngine.UIElements.PhysicsDocumentPicker worldPicker = new global::UnityEngine.UIElements.PhysicsDocumentPicker();

		public override global::UnityEngine.Camera eventCamera => m_EventCamera;

		public global::UnityEngine.Camera camera
		{
			get
			{
				return m_EventCamera;
			}
			set
			{
				m_EventCamera = value;
			}
		}

		public override void Raycast(global::UnityEngine.EventSystems.PointerEventData eventData, global::System.Collections.Generic.List<global::UnityEngine.EventSystems.RaycastResult> resultAppendList)
		{
			global::UnityEngine.EventSystems.BaseInputModule baseInputModule = ((global::UnityEngine.EventSystems.EventSystem.current != null) ? global::UnityEngine.EventSystems.EventSystem.current.currentInputModule : null);
			if (baseInputModule == null || !GetWorldRay(eventData, out var worldRay, out var maxDistance, out var layerMask))
			{
				return;
			}
			maxDistance = global::UnityEngine.Mathf.Min(maxDistance, global::UnityEngine.EventSystems.EventSystem.current.uiToolkitInterop.worldPickingMaxDistance);
			layerMask &= global::UnityEngine.EventSystems.EventSystem.current.uiToolkitInterop.worldPickingLayers;
			int pointerId = baseInputModule.ConvertUIToolkitPointerId(eventData);
			global::UnityEngine.Camera cameraWithSoftPointerCapture = global::UnityEngine.UIElements.PointerDeviceState.GetCameraWithSoftPointerCapture(pointerId);
			if (cameraWithSoftPointerCapture != null)
			{
				global::UnityEngine.Camera camera = ((m_EventCamera != null) ? m_EventCamera : global::UnityEngine.Camera.main);
				if (cameraWithSoftPointerCapture != camera)
				{
					return;
				}
			}
			if (worldPicker.TryPickWithCapture(pointerId, worldRay, maxDistance, layerMask, out var _, out var document, out var elementUnderPointer, out var distance, out var captured))
			{
				resultAppendList.Add(new global::UnityEngine.EventSystems.RaycastResult
				{
					gameObject = ((document == null) ? base.gameObject : document.containerPanel.selectableGameObject),
					origin = worldRay.origin,
					worldPosition = worldRay.origin + distance * worldRay.direction,
					document = document,
					element = elementUnderPointer,
					module = this,
					distance = distance,
					sortingOrder = (captured ? int.MaxValue : 0)
				});
			}
		}

		protected virtual bool GetWorldRay(global::UnityEngine.EventSystems.PointerEventData eventData, out global::UnityEngine.Ray worldRay, out float maxDistance, out int layerMask)
		{
			global::UnityEngine.Camera camera = ((m_EventCamera != null) ? m_EventCamera : global::UnityEngine.Camera.main);
			if (camera == null)
			{
				worldRay = default(global::UnityEngine.Ray);
				maxDistance = 0f;
				layerMask = 0;
				return false;
			}
			maxDistance = camera.farClipPlane;
			layerMask = camera.cullingMask;
			global::UnityEngine.Vector3 relativeMousePositionForRaycast = global::UnityEngine.UI.MultipleDisplayUtilities.GetRelativeMousePositionForRaycast(eventData);
			if ((int)relativeMousePositionForRaycast.z != camera.targetDisplay)
			{
				worldRay = default(global::UnityEngine.Ray);
				return false;
			}
			worldRay = camera.ScreenPointToRay(relativeMousePositionForRaycast);
			return true;
		}
	}
}
