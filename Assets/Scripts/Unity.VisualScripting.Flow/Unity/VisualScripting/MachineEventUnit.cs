namespace Unity.VisualScripting
{
	public abstract class MachineEventUnit<TArgs> : global::Unity.VisualScripting.EventUnit<TArgs>
	{
		protected sealed override bool register => true;

		protected virtual string hookName
		{
			get
			{
				throw new global::Unity.VisualScripting.InvalidImplementationException($"Missing event hook for '{this}'.");
			}
		}

		public override global::Unity.VisualScripting.EventHook GetHook(global::Unity.VisualScripting.GraphReference reference)
		{
			return new global::Unity.VisualScripting.EventHook(hookName, reference.machine);
		}
	}
}
