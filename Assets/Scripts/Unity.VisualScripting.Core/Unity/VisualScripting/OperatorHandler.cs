namespace Unity.VisualScripting
{
	public abstract class OperatorHandler
	{
		public string name { get; }

		public string verb { get; }

		public string symbol { get; }

		public string customMethodName { get; }

		protected OperatorHandler(string name, string verb, string symbol, string customMethodName)
		{
			global::Unity.VisualScripting.Ensure.That("name").IsNotNull(name);
			global::Unity.VisualScripting.Ensure.That("verb").IsNotNull(verb);
			global::Unity.VisualScripting.Ensure.That("symbol").IsNotNull(symbol);
			this.name = name;
			this.verb = verb;
			this.symbol = symbol;
			this.customMethodName = customMethodName;
		}
	}
}
