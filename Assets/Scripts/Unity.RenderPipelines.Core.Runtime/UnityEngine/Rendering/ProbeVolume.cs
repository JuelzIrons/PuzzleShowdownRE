namespace UnityEngine.Rendering
{
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.AddComponentMenu("Rendering/Adaptive Probe Volume")]
	public class ProbeVolume : global::UnityEngine.MonoBehaviour
	{
		public enum Mode
		{
			Global = 0,
			Scene = 1,
			Local = 2
		}

		private enum Version
		{
			Initial = 0,
			LocalMode = 1,
			InvertOverrideLevels = 2,
			Count = 3
		}

		[global::UnityEngine.Tooltip("When set to Global this Probe Volume considers all renderers with Contribute Global Illumination enabled. Local only considers renderers in the scene.\nThis list updates every time the Scene is saved or the lighting is baked.")]
		public global::UnityEngine.Rendering.ProbeVolume.Mode mode = global::UnityEngine.Rendering.ProbeVolume.Mode.Local;

		public global::UnityEngine.Vector3 size = new global::UnityEngine.Vector3(10f, 10f, 10f);

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.Min(0f)]
		public bool overrideRendererFilters;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.Min(0f)]
		public float minRendererVolumeSize = 0.1f;

		public global::UnityEngine.LayerMask objectLayerMask = -1;

		[global::UnityEngine.HideInInspector]
		public int lowestSubdivLevelOverride;

		[global::UnityEngine.HideInInspector]
		public int highestSubdivLevelOverride = 7;

		[global::UnityEngine.HideInInspector]
		public bool overridesSubdivLevels;

		[global::UnityEngine.SerializeField]
		internal bool mightNeedRebaking;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Matrix4x4 cachedTransform;

		[global::UnityEngine.SerializeField]
		internal int cachedHashCode;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.Tooltip("Whether Unity should fill empty space between renderers with bricks at the highest subdivision level.")]
		public bool fillEmptySpaces;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.ProbeVolume.Version version;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Use mode instead. #from(2023.1)")]
		public bool globalVolume;

		private void Awake()
		{
			if (version != global::UnityEngine.Rendering.ProbeVolume.Version.Count)
			{
				if (version == global::UnityEngine.Rendering.ProbeVolume.Version.Initial)
				{
					mode = (globalVolume ? global::UnityEngine.Rendering.ProbeVolume.Mode.Scene : global::UnityEngine.Rendering.ProbeVolume.Mode.Local);
					version++;
				}
				if (version == global::UnityEngine.Rendering.ProbeVolume.Version.LocalMode)
				{
					version++;
				}
			}
		}
	}
}
