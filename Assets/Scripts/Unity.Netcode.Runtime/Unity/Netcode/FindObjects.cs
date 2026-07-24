namespace Unity.Netcode
{
	internal static class FindObjects
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static T[] ByType<T>(bool includeInactive = false, bool orderByIdentifier = false) where T : global::UnityEngine.Object
		{
			return global::UnityEngine.Object.FindObjectsByType<T>(includeInactive ? global::UnityEngine.FindObjectsInactive.Include : global::UnityEngine.FindObjectsInactive.Exclude, orderByIdentifier ? global::UnityEngine.FindObjectsSortMode.InstanceID : global::UnityEngine.FindObjectsSortMode.None);
		}
	}
}
