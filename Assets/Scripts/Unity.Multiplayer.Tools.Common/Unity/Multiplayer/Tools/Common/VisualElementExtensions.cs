namespace Unity.Multiplayer.Tools.Common
{
	internal static class VisualElementExtensions
	{
		public static void AddEventLifecycle(this global::UnityEngine.UIElements.VisualElement visualElement, global::UnityEngine.UIElements.EventCallback<global::UnityEngine.UIElements.AttachToPanelEvent> onAttach, global::UnityEngine.UIElements.EventCallback<global::UnityEngine.UIElements.DetachFromPanelEvent> onDetach)
		{
			if (visualElement.hierarchy.parent != null)
			{
				onAttach(null);
			}
			visualElement.RegisterCallback(onAttach);
			visualElement.RegisterCallback(onDetach);
		}

		public static void SetInclude(this global::UnityEngine.UIElements.VisualElement visualElement, bool includeInLayout)
		{
			visualElement.style.display = ((!includeInLayout) ? global::UnityEngine.UIElements.DisplayStyle.None : global::UnityEngine.UIElements.DisplayStyle.Flex);
		}

		public static bool GetInclude(this global::UnityEngine.UIElements.VisualElement visualElement)
		{
			return visualElement.style.display.value == global::UnityEngine.UIElements.DisplayStyle.Flex;
		}

		public static void QueryRegisterCallback<TEventType>(this global::UnityEngine.UIElements.VisualElement visualElement, global::UnityEngine.UIElements.EventCallback<TEventType> callback, string name = null, string className = null) where TEventType : global::UnityEngine.UIElements.EventBase<TEventType>, new()
		{
			global::UnityEngine.UIElements.UQueryExtensions.Query<global::UnityEngine.UIElements.VisualElement>(visualElement, name, className).Build().ForEach(AddLifecycleEvents);
			void AddLifecycleEvents(global::UnityEngine.UIElements.VisualElement element)
			{
				element.AddEventLifecycle(OnAttach, OnDetach);
				void OnAttach(global::UnityEngine.UIElements.AttachToPanelEvent evt)
				{
					element.RegisterCallback(callback);
				}
				void OnDetach(global::UnityEngine.UIElements.DetachFromPanelEvent evt)
				{
					element.UnregisterCallback(callback);
				}
			}
		}
	}
}
