namespace Unity.VisualScripting
{
	public interface IGraphEventHandler<TArgs>
	{
		global::Unity.VisualScripting.EventHook GetHook(global::Unity.VisualScripting.GraphReference reference);

		void Trigger(global::Unity.VisualScripting.GraphReference reference, TArgs args);

		bool IsListening(global::Unity.VisualScripting.GraphPointer pointer);
	}
}
