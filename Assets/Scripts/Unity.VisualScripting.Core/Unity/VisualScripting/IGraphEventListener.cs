namespace Unity.VisualScripting
{
	public interface IGraphEventListener
	{
		void StartListening(global::Unity.VisualScripting.GraphStack stack);

		void StopListening(global::Unity.VisualScripting.GraphStack stack);

		bool IsListening(global::Unity.VisualScripting.GraphPointer pointer);
	}
}
