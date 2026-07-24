namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class SingletonAttribute : global::System.Attribute
	{
		public bool Persistent { get; set; }

		public bool Automatic { get; set; }

		public global::UnityEngine.HideFlags HideFlags { get; set; }

		public string Name { get; set; }

		public SingletonAttribute()
		{
			HideFlags = global::UnityEngine.HideFlags.None;
		}
	}
}
