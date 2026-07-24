namespace UnityEngine.Rendering
{
	public interface IBitArray
	{
		uint capacity { get; }

		bool allFalse { get; }

		bool allTrue { get; }

		bool this[uint index] { get; set; }

		string humanizedData { get; }

		global::UnityEngine.Rendering.IBitArray BitAnd(global::UnityEngine.Rendering.IBitArray other);

		global::UnityEngine.Rendering.IBitArray BitOr(global::UnityEngine.Rendering.IBitArray other);

		global::UnityEngine.Rendering.IBitArray BitNot();
	}
}
