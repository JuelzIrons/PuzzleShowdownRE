namespace UnityEngine.UI
{
	public interface IClippable
	{
		global::UnityEngine.GameObject gameObject { get; }

		global::UnityEngine.RectTransform rectTransform { get; }

		void RecalculateClipping();

		void Cull(global::UnityEngine.Rect clipRect, bool validRect);

		void SetClipRect(global::UnityEngine.Rect value, bool validRect);

		void SetClipSoftness(global::UnityEngine.Vector2 clipSoftness);
	}
}
