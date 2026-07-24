namespace UnityEngine.Rendering.Universal
{
	internal enum DecalTechniqueOption
	{
		[global::UnityEngine.Tooltip("Automatically selects technique based on build platform.")]
		Automatic = 0,
		[global::UnityEngine.Tooltip("Renders decals into DBuffer and then applied during opaque rendering. Requires DepthNormal prepass which makes not viable solution for the tile based renderers common on mobile.")]
		[global::UnityEngine.InspectorName("DBuffer")]
		DBuffer = 1,
		[global::UnityEngine.Tooltip("Renders decals after opaque objects with normal reconstructed from depth. The decals are simply rendered as mesh on top of opaque ones, as result does not support blending per single surface data (etc. normal blending only).")]
		ScreenSpace = 2
	}
}
