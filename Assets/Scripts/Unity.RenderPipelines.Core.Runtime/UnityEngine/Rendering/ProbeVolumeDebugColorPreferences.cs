namespace UnityEngine.Rendering
{
	internal class ProbeVolumeDebugColorPreferences
	{
		internal static global::System.Func<global::UnityEngine.Color> GetDetailSubdivisionColor;

		internal static global::System.Func<global::UnityEngine.Color> GetMediumSubdivisionColor;

		internal static global::System.Func<global::UnityEngine.Color> GetLowSubdivisionColor;

		internal static global::System.Func<global::UnityEngine.Color> GetVeryLowSubdivisionColor;

		internal static global::System.Func<global::UnityEngine.Color> GetSparseSubdivisionColor;

		internal static global::System.Func<global::UnityEngine.Color> GetSparsestSubdivisionColor;

		internal static global::UnityEngine.Color s_DetailSubdivision;

		internal static global::UnityEngine.Color s_MediumSubdivision;

		internal static global::UnityEngine.Color s_LowSubdivision;

		internal static global::UnityEngine.Color s_VeryLowSubdivision;

		internal static global::UnityEngine.Color s_SparseSubdivision;

		internal static global::UnityEngine.Color s_SparsestSubdivision;

		static ProbeVolumeDebugColorPreferences()
		{
			s_DetailSubdivision = new global::UnityEngine.Color32(135, 35, byte.MaxValue, byte.MaxValue);
			s_MediumSubdivision = new global::UnityEngine.Color32(54, 208, 228, byte.MaxValue);
			s_LowSubdivision = new global::UnityEngine.Color32(byte.MaxValue, 100, 45, byte.MaxValue);
			s_VeryLowSubdivision = new global::UnityEngine.Color32(52, 87, byte.MaxValue, byte.MaxValue);
			s_SparseSubdivision = new global::UnityEngine.Color32(byte.MaxValue, 71, 97, byte.MaxValue);
			s_SparsestSubdivision = new global::UnityEngine.Color32(200, 227, 39, byte.MaxValue);
		}
	}
}
