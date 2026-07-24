namespace UnityEngine.UI
{
	public interface ICanvasElement
	{
		global::UnityEngine.Transform transform { get; }

		void Rebuild(global::UnityEngine.UI.CanvasUpdate executing);

		void LayoutComplete();

		void GraphicUpdateComplete();

		bool IsDestroyed();
	}
}
