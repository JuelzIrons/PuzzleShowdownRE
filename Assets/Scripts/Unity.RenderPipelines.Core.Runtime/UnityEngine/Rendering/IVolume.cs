namespace UnityEngine.Rendering
{
	public interface IVolume
	{
		bool isGlobal { get; set; }

		global::System.Collections.Generic.List<global::UnityEngine.Collider> colliders { get; }
	}
}
