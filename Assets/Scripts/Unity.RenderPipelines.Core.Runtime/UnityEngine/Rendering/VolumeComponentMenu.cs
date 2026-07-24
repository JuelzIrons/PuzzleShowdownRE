namespace UnityEngine.Rendering
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false)]
	public class VolumeComponentMenu : global::System.Attribute
	{
		public readonly string menu;

		public VolumeComponentMenu(string menu)
		{
			this.menu = menu;
		}
	}
}
