namespace UnityEngine.Rendering
{
	internal class TProfilingSampler<TEnum> : global::UnityEngine.Rendering.ProfilingSampler where TEnum : global::System.Enum
	{
		internal static global::System.Collections.Generic.Dictionary<TEnum, global::UnityEngine.Rendering.TProfilingSampler<TEnum>> samples;

		static TProfilingSampler()
		{
			samples = new global::System.Collections.Generic.Dictionary<TEnum, global::UnityEngine.Rendering.TProfilingSampler<TEnum>>();
			string[] names = global::System.Enum.GetNames(typeof(TEnum));
			global::System.Array values = global::System.Enum.GetValues(typeof(TEnum));
			for (int i = 0; i < names.Length; i++)
			{
				global::UnityEngine.Rendering.TProfilingSampler<TEnum> value = new global::UnityEngine.Rendering.TProfilingSampler<TEnum>(names[i]);
				samples.Add((TEnum)values.GetValue(i), value);
			}
		}

		public TProfilingSampler(string name)
			: base(name)
		{
		}
	}
}
