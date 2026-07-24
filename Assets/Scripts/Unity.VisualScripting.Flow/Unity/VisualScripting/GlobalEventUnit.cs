namespace Unity.VisualScripting
{
	public abstract class GlobalEventUnit<TArgs> : global::Unity.VisualScripting.EventUnit<TArgs>
	{
		protected override bool register => true;

		protected virtual string hookName
		{
			get
			{
				throw new global::Unity.VisualScripting.InvalidImplementationException();
			}
		}

		public override global::Unity.VisualScripting.EventHook GetHook(global::Unity.VisualScripting.GraphReference reference)
		{
			return hookName;
		}
	}
}
