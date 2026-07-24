namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.DisableAnnotation]
	[global::UnityEngine.AddComponentMenu("")]
	[global::Unity.VisualScripting.IncludeInSettings(false)]
	public abstract class MessageListener : global::UnityEngine.MonoBehaviour
	{
		private static global::System.Type[] _listenerTypes;

		[global::System.Obsolete("listenerTypes is deprecated", false)]
		public static global::System.Type[] listenerTypes
		{
			get
			{
				if (_listenerTypes == null)
				{
					_listenerTypes = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(global::Unity.VisualScripting.RuntimeCodebase.types, (global::System.Type t) => typeof(global::Unity.VisualScripting.MessageListener).IsAssignableFrom(t) && t.IsConcrete() && !global::System.Attribute.IsDefined(t, typeof(global::System.ObsoleteAttribute))));
				}
				return _listenerTypes;
			}
		}

		[global::System.Obsolete("Use the overload with a messageListenerType parameter instead", false)]
		public static void AddTo(global::UnityEngine.GameObject gameObject)
		{
			global::System.Type[] array = listenerTypes;
			foreach (global::System.Type type in array)
			{
				if (gameObject.GetComponent(type) == null)
				{
					gameObject.AddComponent(type);
				}
			}
		}

		public static void AddTo(global::System.Type messageListenerType, global::UnityEngine.GameObject gameObject)
		{
			if (!gameObject.TryGetComponent(messageListenerType, out var _))
			{
				gameObject.AddComponent(messageListenerType);
			}
		}
	}
}
