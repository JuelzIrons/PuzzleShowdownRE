namespace Unity.VisualScripting
{
	public sealed class UnitPreservation : global::Unity.VisualScripting.IPoolable
	{
		private struct UnitPortPreservation
		{
			public readonly global::Unity.VisualScripting.IUnit unit;

			public readonly string key;

			public UnitPortPreservation(global::Unity.VisualScripting.IUnitPort port)
			{
				unit = port.unit;
				key = port.key;
			}

			public UnitPortPreservation(global::Unity.VisualScripting.IUnit unit, string key)
			{
				this.unit = unit;
				this.key = key;
			}

			public global::Unity.VisualScripting.IUnitPort GetOrCreateInput(out global::Unity.VisualScripting.InvalidInput newInvalidInput)
			{
				string key = this.key;
				if (!global::System.Linq.Enumerable.Any(unit.inputs, (global::Unity.VisualScripting.IUnitInputPort p) => p.key == key))
				{
					newInvalidInput = new global::Unity.VisualScripting.InvalidInput(key);
					unit.invalidInputs.Add(newInvalidInput);
				}
				else
				{
					newInvalidInput = null;
				}
				return global::System.Linq.Enumerable.Single(unit.inputs, (global::Unity.VisualScripting.IUnitInputPort p) => p.key == key);
			}

			public global::Unity.VisualScripting.IUnitPort GetOrCreateOutput(out global::Unity.VisualScripting.InvalidOutput newInvalidOutput)
			{
				string key = this.key;
				if (!global::System.Linq.Enumerable.Any(unit.outputs, (global::Unity.VisualScripting.IUnitOutputPort p) => p.key == key))
				{
					newInvalidOutput = new global::Unity.VisualScripting.InvalidOutput(key);
					unit.invalidOutputs.Add(newInvalidOutput);
				}
				else
				{
					newInvalidOutput = null;
				}
				return global::System.Linq.Enumerable.Single(unit.outputs, (global::Unity.VisualScripting.IUnitOutputPort p) => p.key == key);
			}
		}

		private readonly global::System.Collections.Generic.Dictionary<string, object> defaultValues = new global::System.Collections.Generic.Dictionary<string, object>();

		private readonly global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>> inputConnections = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>>();

		private readonly global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>> outputConnections = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>>();

		private bool disposed;

		void global::Unity.VisualScripting.IPoolable.New()
		{
			disposed = false;
		}

		void global::Unity.VisualScripting.IPoolable.Free()
		{
			disposed = true;
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>> inputConnection in inputConnections)
			{
				global::Unity.VisualScripting.ListPool<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>.Free(inputConnection.Value);
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>> outputConnection in outputConnections)
			{
				global::Unity.VisualScripting.ListPool<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>.Free(outputConnection.Value);
			}
			defaultValues.Clear();
			inputConnections.Clear();
			outputConnections.Clear();
		}

		private UnitPreservation()
		{
		}

		public static global::Unity.VisualScripting.UnitPreservation Preserve(global::Unity.VisualScripting.IUnit unit)
		{
			global::Unity.VisualScripting.UnitPreservation unitPreservation = global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.UnitPreservation>.New(() => new global::Unity.VisualScripting.UnitPreservation());
			foreach (global::System.Collections.Generic.KeyValuePair<string, object> defaultValue in unit.defaultValues)
			{
				unitPreservation.defaultValues.Add(defaultValue.Key, defaultValue.Value);
			}
			foreach (global::Unity.VisualScripting.IUnitInputPort input in unit.inputs)
			{
				if (!input.hasAnyConnection)
				{
					continue;
				}
				unitPreservation.inputConnections.Add(input.key, global::Unity.VisualScripting.ListPool<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>.New());
				foreach (global::Unity.VisualScripting.IUnitPort connectedPort in input.connectedPorts)
				{
					unitPreservation.inputConnections[input.key].Add(new global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation(connectedPort));
				}
			}
			foreach (global::Unity.VisualScripting.IUnitOutputPort output in unit.outputs)
			{
				if (!output.hasAnyConnection)
				{
					continue;
				}
				unitPreservation.outputConnections.Add(output.key, global::Unity.VisualScripting.ListPool<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>.New());
				foreach (global::Unity.VisualScripting.IUnitPort connectedPort2 in output.connectedPorts)
				{
					unitPreservation.outputConnections[output.key].Add(new global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation(connectedPort2));
				}
			}
			return unitPreservation;
		}

		public void RestoreTo(global::Unity.VisualScripting.IUnit unit)
		{
			if (disposed)
			{
				throw new global::System.ObjectDisposedException(ToString());
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, object> defaultValue in defaultValues)
			{
				if (unit.defaultValues.ContainsKey(defaultValue.Key) && unit.valueInputs.Contains(defaultValue.Key) && unit.valueInputs[defaultValue.Key].type.IsAssignableFrom(defaultValue.Value))
				{
					unit.defaultValues[defaultValue.Key] = defaultValue.Value;
				}
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>> inputConnection in inputConnections)
			{
				global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation destinationPreservation = new global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation(unit, inputConnection.Key);
				foreach (global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation item in inputConnection.Value)
				{
					RestoreConnection(item, destinationPreservation);
				}
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation>> outputConnection in outputConnections)
			{
				global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation sourcePreservation = new global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation(unit, outputConnection.Key);
				foreach (global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation item2 in outputConnection.Value)
				{
					RestoreConnection(sourcePreservation, item2);
				}
			}
			global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.UnitPreservation>.Free(this);
		}

		private void RestoreConnection(global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation sourcePreservation, global::Unity.VisualScripting.UnitPreservation.UnitPortPreservation destinationPreservation)
		{
			global::Unity.VisualScripting.InvalidOutput newInvalidOutput;
			global::Unity.VisualScripting.IUnitPort orCreateOutput = sourcePreservation.GetOrCreateOutput(out newInvalidOutput);
			global::Unity.VisualScripting.InvalidInput newInvalidInput;
			global::Unity.VisualScripting.IUnitPort orCreateInput = destinationPreservation.GetOrCreateInput(out newInvalidInput);
			if (orCreateOutput.CanValidlyConnectTo(orCreateInput))
			{
				orCreateOutput.ValidlyConnectTo(orCreateInput);
				return;
			}
			if (orCreateOutput.CanInvalidlyConnectTo(orCreateInput))
			{
				orCreateOutput.InvalidlyConnectTo(orCreateInput);
				return;
			}
			if (newInvalidOutput != null)
			{
				sourcePreservation.unit.invalidOutputs.Remove(newInvalidOutput);
			}
			if (newInvalidInput != null)
			{
				destinationPreservation.unit.invalidInputs.Remove(newInvalidInput);
			}
		}
	}
}
