namespace Unity.VisualScripting
{
	public sealed class ValueInputDefinition : global::Unity.VisualScripting.ValuePortDefinition, global::Unity.VisualScripting.IUnitInputPortDefinition, global::Unity.VisualScripting.IUnitPortDefinition
	{
		[global::Unity.VisualScripting.SerializeAs("defaultValue")]
		private object _defaultvalue;

		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.DoNotSerialize]
		public override global::System.Type type
		{
			get
			{
				return base.type;
			}
			set
			{
				base.type = value;
				if (!type.IsAssignableFrom(defaultValue))
				{
					if (global::Unity.VisualScripting.ValueInput.SupportsDefaultValue(type))
					{
						_defaultvalue = type.PseudoDefault();
						return;
					}
					hasDefaultValue = false;
					_defaultvalue = null;
				}
			}
		}

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		public bool hasDefaultValue { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.Inspectable]
		public object defaultValue
		{
			get
			{
				return _defaultvalue;
			}
			set
			{
				if (type == null)
				{
					throw new global::System.InvalidOperationException("A type must be defined before setting the default value.");
				}
				if (!global::Unity.VisualScripting.ValueInput.SupportsDefaultValue(type))
				{
					throw new global::System.InvalidOperationException("The selected type does not support default values.");
				}
				global::Unity.VisualScripting.Ensure.That("value").IsOfType(value, type);
				_defaultvalue = value;
			}
		}
	}
}
