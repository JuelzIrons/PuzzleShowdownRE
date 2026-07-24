namespace UnityEngine.Timeline
{
	public interface ILayerable
	{
		global::UnityEngine.Playables.Playable CreateLayerMixer(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, int inputCount);
	}
}
