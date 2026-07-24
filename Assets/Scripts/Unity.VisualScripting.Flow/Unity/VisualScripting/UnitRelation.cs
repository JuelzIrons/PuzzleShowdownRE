namespace Unity.VisualScripting
{
	public sealed class UnitRelation : global::Unity.VisualScripting.IUnitRelation, global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IUnitPort>
	{
		public global::Unity.VisualScripting.IUnitPort source { get; }

		public global::Unity.VisualScripting.IUnitPort destination { get; }

		public UnitRelation(global::Unity.VisualScripting.IUnitPort source, global::Unity.VisualScripting.IUnitPort destination)
		{
			global::Unity.VisualScripting.Ensure.That("source").IsNotNull(source);
			global::Unity.VisualScripting.Ensure.That("destination").IsNotNull(destination);
			if (source.unit != destination.unit)
			{
				throw new global::System.NotSupportedException("Cannot create relations across nodes.");
			}
			this.source = source;
			this.destination = destination;
		}
	}
}
