namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::UnityEngine.Timeline.TrackBindingType(typeof(global::UnityEngine.GameObject))]
	[global::UnityEngine.Timeline.HideInMenu]
	[global::UnityEngine.ExcludeFromPreset]
	public class MarkerTrack : global::UnityEngine.Timeline.TrackAsset
	{
		public override global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableBinding> outputs
		{
			get
			{
				if (!(this == base.timelineAsset?.markerTrack))
				{
					return base.outputs;
				}
				return new global::System.Collections.Generic.List<global::UnityEngine.Playables.PlayableBinding> { global::UnityEngine.Playables.ScriptPlayableBinding.Create(base.name, null, typeof(global::UnityEngine.GameObject)) };
			}
		}
	}
}
