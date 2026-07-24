namespace DG.Tweening.Core
{
	public static class Extensions
	{
		public static T SetSpecialStartupMode<T>(this T t, global::DG.Tweening.Core.Enums.SpecialStartupMode mode) where T : global::DG.Tweening.Tween
		{
			t.specialStartupMode = mode;
			return t;
		}

		public static global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> Blendable<T1, T2, TPlugOptions>(this global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			t.isBlendable = true;
			return t;
		}

		public static global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> NoFrom<T1, T2, TPlugOptions>(this global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			t.isFromAllowed = false;
			return t;
		}
	}
}
