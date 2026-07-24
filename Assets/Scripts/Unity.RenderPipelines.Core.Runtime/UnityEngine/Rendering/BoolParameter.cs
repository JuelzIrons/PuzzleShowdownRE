namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class BoolParameter : global::UnityEngine.Rendering.VolumeParameter<bool>
	{
		public enum DisplayType
		{
			Checkbox = 0,
			EnumPopup = 1
		}

		[global::System.NonSerialized]
		public global::UnityEngine.Rendering.BoolParameter.DisplayType displayType;

		public BoolParameter(bool value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		public BoolParameter(bool value, global::UnityEngine.Rendering.BoolParameter.DisplayType displayType, bool overrideState = false)
			: base(value, overrideState)
		{
			this.displayType = displayType;
		}
	}
}
