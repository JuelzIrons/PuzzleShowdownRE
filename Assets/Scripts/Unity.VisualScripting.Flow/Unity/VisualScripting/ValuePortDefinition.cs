namespace Unity.VisualScripting
{
	public abstract class ValuePortDefinition : global::Unity.VisualScripting.UnitPortDefinition, global::Unity.VisualScripting.IUnitValuePortDefinition, global::Unity.VisualScripting.IUnitPortDefinition
	{
		[global::Unity.VisualScripting.SerializeAs("_type")]
		private global::System.Type _type { get; set; }

		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.DoNotSerialize]
		public virtual global::System.Type type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		public override bool isValid
		{
			get
			{
				if (base.isValid)
				{
					return type != null;
				}
				return false;
			}
		}
	}
}
