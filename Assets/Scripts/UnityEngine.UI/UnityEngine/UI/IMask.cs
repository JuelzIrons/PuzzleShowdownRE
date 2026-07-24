namespace UnityEngine.UI
{
	[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
	[global::System.Obsolete("Not supported anymore.", true)]
	public interface IMask
	{
		global::UnityEngine.RectTransform rectTransform { get; }

		bool Enabled();
	}
}
