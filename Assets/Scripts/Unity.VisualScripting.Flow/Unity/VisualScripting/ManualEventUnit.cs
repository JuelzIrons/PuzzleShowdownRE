namespace Unity.VisualScripting
{
	public abstract class ManualEventUnit<TArgs> : global::Unity.VisualScripting.EventUnit<TArgs>
	{
		protected sealed override bool register => false;

		protected abstract string hookName { get; }

		public sealed override global::Unity.VisualScripting.EventHook GetHook(global::Unity.VisualScripting.GraphReference reference)
		{
			return hookName;
		}
	}
}
