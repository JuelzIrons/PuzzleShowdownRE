namespace Unity.VisualScripting
{
	public abstract class SerializedPropertyProvider<T> : global::UnityEngine.ScriptableObject, global::Unity.VisualScripting.ISerializedPropertyProvider
	{
		[global::UnityEngine.SerializeField]
		protected T item;

		object global::Unity.VisualScripting.ISerializedPropertyProvider.item
		{
			get
			{
				return item;
			}
			set
			{
				item = (T)value;
			}
		}
	}
}
