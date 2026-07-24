namespace UnityEngine.Timeline
{
	internal static class WeightUtility
	{
		public static float NormalizeMixer(global::UnityEngine.Playables.Playable mixer)
		{
			if (!global::UnityEngine.Playables.PlayableExtensions.IsValid(mixer))
			{
				return 0f;
			}
			int inputCount = global::UnityEngine.Playables.PlayableExtensions.GetInputCount(mixer);
			float num = 0f;
			for (int i = 0; i < inputCount; i++)
			{
				num += global::UnityEngine.Playables.PlayableExtensions.GetInputWeight(mixer, i);
			}
			if (num > global::UnityEngine.Mathf.Epsilon && num < 1f)
			{
				for (int j = 0; j < inputCount; j++)
				{
					global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(mixer, j, global::UnityEngine.Playables.PlayableExtensions.GetInputWeight(mixer, j) / num);
				}
			}
			return global::UnityEngine.Mathf.Clamp01(num);
		}
	}
}
