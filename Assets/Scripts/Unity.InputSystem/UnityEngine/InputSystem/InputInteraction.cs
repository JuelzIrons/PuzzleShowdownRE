namespace UnityEngine.InputSystem
{
	internal static class InputInteraction
	{
		public static global::UnityEngine.InputSystem.Utilities.TypeTable s_Interactions;

		public static global::System.Type GetValueType(global::System.Type interactionType)
		{
			if (interactionType == null)
			{
				throw new global::System.ArgumentNullException("interactionType");
			}
			return global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetGenericTypeArgumentFromHierarchy(interactionType, typeof(global::UnityEngine.InputSystem.IInputInteraction<>), 0);
		}

		public static string GetDisplayName(string interaction)
		{
			if (string.IsNullOrEmpty(interaction))
			{
				throw new global::System.ArgumentNullException("interaction");
			}
			global::System.Type type = s_Interactions.LookupTypeRegistration(interaction);
			if (type == null)
			{
				return interaction;
			}
			return GetDisplayName(type);
		}

		public static string GetDisplayName(global::System.Type interactionType)
		{
			if (interactionType == null)
			{
				throw new global::System.ArgumentNullException("interactionType");
			}
			global::System.ComponentModel.DisplayNameAttribute customAttribute = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::System.ComponentModel.DisplayNameAttribute>(interactionType);
			if (customAttribute == null)
			{
				if (interactionType.Name.EndsWith("Interaction"))
				{
					return interactionType.Name.Substring(0, interactionType.Name.Length - "Interaction".Length);
				}
				return interactionType.Name;
			}
			return customAttribute.DisplayName;
		}
	}
}
