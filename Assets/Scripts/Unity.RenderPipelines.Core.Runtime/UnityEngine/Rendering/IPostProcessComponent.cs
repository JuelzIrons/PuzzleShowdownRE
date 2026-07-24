namespace UnityEngine.Rendering
{
	public interface IPostProcessComponent
	{
		bool IsActive();

		[global::System.Obsolete("Unused #from(2023.1)")]
		bool IsTileCompatible()
		{
			return false;
		}
	}
}
