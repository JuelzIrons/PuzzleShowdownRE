namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	public abstract class Unit : global::Unity.VisualScripting.GraphElement<global::Unity.VisualScripting.FlowGraph>, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		public class DebugData : global::Unity.VisualScripting.IUnitDebugData, global::Unity.VisualScripting.IGraphElementDebugData
		{
			public int lastInvokeFrame { get; set; }

			public float lastInvokeTime { get; set; }

			public global::System.Exception runtimeException { get; set; }
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public virtual bool canDefine => true;

		[global::Unity.VisualScripting.DoNotSerialize]
		public bool failedToDefine => definitionException != null;

		[global::Unity.VisualScripting.DoNotSerialize]
		public bool isDefined { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.ControlInput> controlInputs { get; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.ControlOutput> controlOutputs { get; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.ValueInput> valueInputs { get; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.ValueOutput> valueOutputs { get; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.InvalidInput> invalidInputs { get; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.InvalidOutput> invalidOutputs { get; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitInputPort> inputs => global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitInputPort>(new global::System.Collections.IEnumerable[3] { controlInputs, valueInputs, invalidInputs });

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitOutputPort> outputs => global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitOutputPort>(new global::System.Collections.IEnumerable[3] { controlOutputs, valueOutputs, invalidOutputs });

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitInputPort> validInputs => global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitInputPort>(new global::System.Collections.IEnumerable[2] { controlInputs, valueInputs });

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitOutputPort> validOutputs => global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitOutputPort>(new global::System.Collections.IEnumerable[2] { controlOutputs, valueOutputs });

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitPort> ports => global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitPort>(new global::System.Collections.IEnumerable[2] { inputs, outputs });

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitPort> invalidPorts => global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitPort>(new global::System.Collections.IEnumerable[2] { invalidInputs, invalidOutputs });

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitPort> validPorts => global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitPort>(new global::System.Collections.IEnumerable[2] { validInputs, validOutputs });

		[global::Unity.VisualScripting.Serialize]
		public global::System.Collections.Generic.Dictionary<string, object> defaultValues { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.IConnectionCollection<global::Unity.VisualScripting.IUnitRelation, global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IUnitPort> relations { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitConnection> connections => global::System.Linq.Enumerable.SelectMany(ports, (global::Unity.VisualScripting.IUnitPort p) => p.connections);

		[global::Unity.VisualScripting.DoNotSerialize]
		public virtual bool isControlRoot { get; protected set; }

		[global::Unity.VisualScripting.Serialize]
		public global::UnityEngine.Vector2 position { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Exception definitionException { get; protected set; }

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		public event global::System.Action onPortsChanged;

		protected Unit()
		{
			controlInputs = new global::Unity.VisualScripting.UnitPortCollection<global::Unity.VisualScripting.ControlInput>(this);
			controlOutputs = new global::Unity.VisualScripting.UnitPortCollection<global::Unity.VisualScripting.ControlOutput>(this);
			valueInputs = new global::Unity.VisualScripting.UnitPortCollection<global::Unity.VisualScripting.ValueInput>(this);
			valueOutputs = new global::Unity.VisualScripting.UnitPortCollection<global::Unity.VisualScripting.ValueOutput>(this);
			invalidInputs = new global::Unity.VisualScripting.UnitPortCollection<global::Unity.VisualScripting.InvalidInput>(this);
			invalidOutputs = new global::Unity.VisualScripting.UnitPortCollection<global::Unity.VisualScripting.InvalidOutput>(this);
			relations = new global::Unity.VisualScripting.ConnectionCollection<global::Unity.VisualScripting.IUnitRelation, global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IUnitPort>();
			defaultValues = new global::System.Collections.Generic.Dictionary<string, object>();
		}

		public virtual global::Unity.VisualScripting.IGraphElementDebugData CreateDebugData()
		{
			return new global::Unity.VisualScripting.Unit.DebugData();
		}

		public override void AfterAdd()
		{
			Define();
			base.AfterAdd();
		}

		public override void BeforeRemove()
		{
			base.BeforeRemove();
			Disconnect();
		}

		public override void Instantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			base.Instantiate(instance);
			if (this is global::Unity.VisualScripting.IGraphEventListener listener && global::Unity.VisualScripting.XGraphEventListener.IsHierarchyListening(instance))
			{
				listener.StartListening(instance);
			}
		}

		public override void Uninstantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			if (this is global::Unity.VisualScripting.IGraphEventListener listener)
			{
				listener.StopListening(instance);
			}
			base.Uninstantiate(instance);
		}

		protected void CopyFrom(global::Unity.VisualScripting.Unit source)
		{
			CopyFrom((global::Unity.VisualScripting.GraphElement<global::Unity.VisualScripting.FlowGraph>)source);
			defaultValues = source.defaultValues;
		}

		protected abstract void Definition();

		protected virtual void AfterDefine()
		{
		}

		protected virtual void BeforeUndefine()
		{
		}

		private void Undefine()
		{
			if (isDefined)
			{
				BeforeUndefine();
			}
			Disconnect();
			defaultValues.Clear();
			controlInputs.Clear();
			controlOutputs.Clear();
			valueInputs.Clear();
			valueOutputs.Clear();
			invalidInputs.Clear();
			invalidOutputs.Clear();
			relations.Clear();
			isDefined = false;
		}

		public void EnsureDefined()
		{
			if (!isDefined)
			{
				Define();
			}
		}

		public void Define()
		{
			global::Unity.VisualScripting.UnitPreservation unitPreservation = global::Unity.VisualScripting.UnitPreservation.Preserve(this);
			Undefine();
			if (canDefine)
			{
				try
				{
					Definition();
					isDefined = true;
					definitionException = null;
					AfterDefine();
				}
				catch (global::System.Exception arg)
				{
					Undefine();
					definitionException = arg;
					global::UnityEngine.Debug.LogWarning($"Failed to define {this}:\n{arg}");
				}
			}
			unitPreservation.RestoreTo(this);
		}

		public void RemoveUnconnectedInvalidPorts()
		{
			global::Unity.VisualScripting.InvalidInput[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(invalidInputs, (global::Unity.VisualScripting.InvalidInput p) => !p.hasAnyConnection));
			foreach (global::Unity.VisualScripting.InvalidInput item in array)
			{
				invalidInputs.Remove(item);
			}
			global::Unity.VisualScripting.InvalidOutput[] array2 = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(invalidOutputs, (global::Unity.VisualScripting.InvalidOutput p) => !p.hasAnyConnection));
			foreach (global::Unity.VisualScripting.InvalidOutput item2 in array2)
			{
				invalidOutputs.Remove(item2);
			}
		}

		public void PortsChanged()
		{
			this.onPortsChanged?.Invoke();
		}

		public void Disconnect()
		{
			while (global::System.Linq.Enumerable.Any(ports, (global::Unity.VisualScripting.IUnitPort p) => p.hasAnyConnection))
			{
				global::System.Linq.Enumerable.First(ports, (global::Unity.VisualScripting.IUnitPort p) => p.hasAnyConnection).Disconnect();
			}
		}

		protected void EnsureUniqueInput(string key)
		{
			if (controlInputs.Contains(key) || valueInputs.Contains(key) || invalidInputs.Contains(key))
			{
				throw new global::System.ArgumentException($"Duplicate input for '{key}' in {GetType()}.");
			}
		}

		protected void EnsureUniqueOutput(string key)
		{
			if (controlOutputs.Contains(key) || valueOutputs.Contains(key) || invalidOutputs.Contains(key))
			{
				throw new global::System.ArgumentException($"Duplicate output for '{key}' in {GetType()}.");
			}
		}

		protected global::Unity.VisualScripting.ControlInput ControlInput(string key, global::System.Func<global::Unity.VisualScripting.Flow, global::Unity.VisualScripting.ControlOutput> action)
		{
			EnsureUniqueInput(key);
			global::Unity.VisualScripting.ControlInput controlInput = new global::Unity.VisualScripting.ControlInput(key, action);
			controlInputs.Add(controlInput);
			return controlInput;
		}

		protected global::Unity.VisualScripting.ControlInput ControlInputCoroutine(string key, global::System.Func<global::Unity.VisualScripting.Flow, global::System.Collections.IEnumerator> coroutineAction)
		{
			EnsureUniqueInput(key);
			global::Unity.VisualScripting.ControlInput controlInput = new global::Unity.VisualScripting.ControlInput(key, coroutineAction);
			controlInputs.Add(controlInput);
			return controlInput;
		}

		protected global::Unity.VisualScripting.ControlInput ControlInputCoroutine(string key, global::System.Func<global::Unity.VisualScripting.Flow, global::Unity.VisualScripting.ControlOutput> action, global::System.Func<global::Unity.VisualScripting.Flow, global::System.Collections.IEnumerator> coroutineAction)
		{
			EnsureUniqueInput(key);
			global::Unity.VisualScripting.ControlInput controlInput = new global::Unity.VisualScripting.ControlInput(key, action, coroutineAction);
			controlInputs.Add(controlInput);
			return controlInput;
		}

		protected global::Unity.VisualScripting.ControlOutput ControlOutput(string key)
		{
			EnsureUniqueOutput(key);
			global::Unity.VisualScripting.ControlOutput controlOutput = new global::Unity.VisualScripting.ControlOutput(key);
			controlOutputs.Add(controlOutput);
			return controlOutput;
		}

		protected global::Unity.VisualScripting.ValueInput ValueInput(global::System.Type type, string key)
		{
			EnsureUniqueInput(key);
			global::Unity.VisualScripting.ValueInput valueInput = new global::Unity.VisualScripting.ValueInput(key, type);
			valueInputs.Add(valueInput);
			return valueInput;
		}

		protected global::Unity.VisualScripting.ValueInput ValueInput<T>(string key)
		{
			return ValueInput(typeof(T), key);
		}

		protected global::Unity.VisualScripting.ValueInput ValueInput<T>(string key, T @default)
		{
			global::Unity.VisualScripting.ValueInput valueInput = ValueInput<T>(key);
			valueInput.SetDefaultValue(@default);
			return valueInput;
		}

		protected global::Unity.VisualScripting.ValueOutput ValueOutput(global::System.Type type, string key)
		{
			EnsureUniqueOutput(key);
			global::Unity.VisualScripting.ValueOutput valueOutput = new global::Unity.VisualScripting.ValueOutput(key, type);
			valueOutputs.Add(valueOutput);
			return valueOutput;
		}

		protected global::Unity.VisualScripting.ValueOutput ValueOutput(global::System.Type type, string key, global::System.Func<global::Unity.VisualScripting.Flow, object> getValue)
		{
			EnsureUniqueOutput(key);
			global::Unity.VisualScripting.ValueOutput valueOutput = new global::Unity.VisualScripting.ValueOutput(key, type, getValue);
			valueOutputs.Add(valueOutput);
			return valueOutput;
		}

		protected global::Unity.VisualScripting.ValueOutput ValueOutput<T>(string key)
		{
			return ValueOutput(typeof(T), key);
		}

		protected global::Unity.VisualScripting.ValueOutput ValueOutput<T>(string key, global::System.Func<global::Unity.VisualScripting.Flow, T> getValue)
		{
			return ValueOutput(typeof(T), key, (global::Unity.VisualScripting.Flow recursion) => getValue(recursion));
		}

		private void Relation(global::Unity.VisualScripting.IUnitPort source, global::Unity.VisualScripting.IUnitPort destination)
		{
			relations.Add(new global::Unity.VisualScripting.UnitRelation(source, destination));
		}

		protected void Requirement(global::Unity.VisualScripting.ValueInput source, global::Unity.VisualScripting.ControlInput destination)
		{
			Relation(source, destination);
		}

		protected void Requirement(global::Unity.VisualScripting.ValueInput source, global::Unity.VisualScripting.ValueOutput destination)
		{
			Relation(source, destination);
		}

		protected void Assignment(global::Unity.VisualScripting.ControlInput source, global::Unity.VisualScripting.ValueOutput destination)
		{
			Relation(source, destination);
		}

		protected void Succession(global::Unity.VisualScripting.ControlInput source, global::Unity.VisualScripting.ControlOutput destination)
		{
			Relation(source, destination);
		}

		public override global::Unity.VisualScripting.AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			global::Unity.VisualScripting.AnalyticsIdentifier obj = new global::Unity.VisualScripting.AnalyticsIdentifier
			{
				Identifier = GetType().FullName,
				Namespace = GetType().Namespace
			};
			obj.Hashcode = obj.Identifier.GetHashCode();
			return obj;
		}
	}
}
