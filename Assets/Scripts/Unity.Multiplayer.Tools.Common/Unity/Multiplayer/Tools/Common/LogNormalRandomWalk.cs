namespace Unity.Multiplayer.Tools.Common
{
	[global::System.Serializable]
	internal class LogNormalRandomWalk
	{
		[field: global::UnityEngine.SerializeField]
		public float Rate { get; set; } = 1f;

		[field: global::UnityEngine.SerializeField]
		public float Min { get; set; } = 0.01f;

		[field: global::UnityEngine.SerializeField]
		public float Max { get; set; } = 10f;

		public float Value { get; private set; } = 1f;

		public float NextFloat(global::System.Random random)
		{
			float num = global::UnityEngine.Mathf.Exp(Rate * (float)(random.NextDouble() - 0.5));
			Value *= num;
			Value = global::UnityEngine.Mathf.Clamp(Value, Min, Max);
			return Value;
		}

		public int NextInt(global::System.Random random)
		{
			return (int)global::UnityEngine.Mathf.Round(NextFloat(random));
		}

		public void Repeat(global::System.Random random, global::System.Action action)
		{
			int num = NextInt(random);
			for (int i = 0; i < num; i++)
			{
				action();
			}
		}
	}
}
