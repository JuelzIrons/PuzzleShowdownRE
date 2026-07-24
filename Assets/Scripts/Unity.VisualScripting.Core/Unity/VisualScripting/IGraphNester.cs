namespace Unity.VisualScripting
{
	public interface IGraphNester : global::Unity.VisualScripting.IGraphParent
	{
		global::Unity.VisualScripting.IGraphNest nest { get; }

		void InstantiateNest();

		void UninstantiateNest();
	}
}
