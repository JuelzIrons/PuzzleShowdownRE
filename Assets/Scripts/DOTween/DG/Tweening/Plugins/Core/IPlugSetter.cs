namespace DG.Tweening.Plugins.Core
{
	public interface IPlugSetter<T1, out T2, TPlugin, out TPlugOptions>
	{
		global::DG.Tweening.Core.DOGetter<T1> Getter();

		global::DG.Tweening.Core.DOSetter<T1> Setter();

		T2 EndValue();

		TPlugOptions GetOptions();
	}
}
