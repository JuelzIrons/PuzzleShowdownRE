namespace TMPro
{
	public interface ITextElement
	{
		global::UnityEngine.Material sharedMaterial { get; }

		void Rebuild(global::UnityEngine.UI.CanvasUpdate update);

		int GetInstanceID();
	}
}
