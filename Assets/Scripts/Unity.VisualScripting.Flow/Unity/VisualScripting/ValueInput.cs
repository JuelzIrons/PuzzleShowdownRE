namespace Unity.VisualScripting
{
	public sealed class ValueInput : global::Unity.VisualScripting.UnitPort<global::Unity.VisualScripting.ValueOutput, global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.ValueConnection>, global::Unity.VisualScripting.IUnitValuePort, global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.IUnitInputPort
	{
		private static readonly global::System.Collections.Generic.HashSet<global::System.Type> typesWithDefaultValues = new global::System.Collections.Generic.HashSet<global::System.Type>
		{
			typeof(global::UnityEngine.Vector2),
			typeof(global::UnityEngine.Vector3),
			typeof(global::UnityEngine.Vector4),
			typeof(global::UnityEngine.Color),
			typeof(global::UnityEngine.AnimationCurve),
			typeof(global::UnityEngine.Rect),
			typeof(global::UnityEngine.Ray),
			typeof(global::UnityEngine.Ray2D),
			typeof(global::System.Type),
			typeof(global::UnityEngine.InputSystem.InputAction)
		};

		public global::System.Type type { get; }

		public bool hasDefaultValue => base.unit.defaultValues.ContainsKey(base.key);

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ValueConnection> validConnections => base.unit?.graph?.valueConnections.WithDestination(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.ValueConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.InvalidConnection> invalidConnections => base.unit?.graph?.invalidConnections.WithDestination(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.InvalidConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ValueOutput> validConnectedPorts => global::System.Linq.Enumerable.Select(validConnections, (global::Unity.VisualScripting.ValueConnection c) => c.source);

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitOutputPort> invalidConnectedPorts => global::System.Linq.Enumerable.Select(invalidConnections, (global::Unity.VisualScripting.InvalidConnection c) => c.source);

		[global::Unity.VisualScripting.DoNotSerialize]
		internal object _defaultValue
		{
			get
			{
				return base.unit.defaultValues[base.key];
			}
			set
			{
				base.unit.defaultValues[base.key] = value;
			}
		}

		public bool nullMeansSelf { get; private set; }

		public bool allowsNull { get; private set; }

		public global::Unity.VisualScripting.ValueConnection connection => base.unit.graph?.valueConnections.SingleOrDefaultWithDestination(this);

		public override bool hasValidConnection => connection != null;

		public ValueInput(string key, global::System.Type type)
			: base(key)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			this.type = type;
		}

		public void SetDefaultValue(object value)
		{
			global::Unity.VisualScripting.Ensure.That("value").IsOfType(value, type);
			if (SupportsDefaultValue(type))
			{
				if (base.unit.defaultValues.ContainsKey(base.key))
				{
					base.unit.defaultValues[base.key] = value;
				}
				else
				{
					base.unit.defaultValues.Add(base.key, value);
				}
			}
		}

		public override bool CanConnectToValid(global::Unity.VisualScripting.ValueOutput port)
		{
			return port.type.IsConvertibleTo(type, guaranteed: false);
		}

		public override void ConnectToValid(global::Unity.VisualScripting.ValueOutput port)
		{
			Disconnect();
			base.unit.graph.valueConnections.Add(new global::Unity.VisualScripting.ValueConnection(port, this));
		}

		public override void ConnectToInvalid(global::Unity.VisualScripting.IUnitOutputPort port)
		{
			ConnectInvalid(port, this);
		}

		public override void DisconnectFromValid(global::Unity.VisualScripting.ValueOutput port)
		{
			global::Unity.VisualScripting.ValueConnection valueConnection = global::System.Linq.Enumerable.SingleOrDefault(validConnections, (global::Unity.VisualScripting.ValueConnection c) => c.source == port);
			if (valueConnection != null)
			{
				base.unit.graph.valueConnections.Remove(valueConnection);
			}
		}

		public override void DisconnectFromInvalid(global::Unity.VisualScripting.IUnitOutputPort port)
		{
			DisconnectInvalid(port, this);
		}

		public global::Unity.VisualScripting.ValueInput NullMeansSelf()
		{
			if (global::Unity.VisualScripting.ComponentHolderProtocol.IsComponentHolderType(type))
			{
				nullMeansSelf = true;
			}
			return this;
		}

		public global::Unity.VisualScripting.ValueInput AllowsNull()
		{
			if (type.IsNullable())
			{
				allowsNull = true;
			}
			return this;
		}

		public static bool SupportsDefaultValue(global::System.Type type)
		{
			if (!typesWithDefaultValues.Contains(type) && !typesWithDefaultValues.Contains(global::System.Nullable.GetUnderlyingType(type)) && !type.IsBasic())
			{
				return typeof(global::UnityEngine.Object).IsAssignableFrom(type);
			}
			return true;
		}

		public override global::Unity.VisualScripting.IUnitPort CompatiblePort(global::Unity.VisualScripting.IUnit unit)
		{
			if (unit == base.unit)
			{
				return null;
			}
			return unit.CompatibleValueOutput(type);
		}
	}
}
