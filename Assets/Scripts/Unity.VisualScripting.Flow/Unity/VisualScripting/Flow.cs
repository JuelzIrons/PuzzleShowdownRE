namespace Unity.VisualScripting
{
	public sealed class Flow : global::Unity.VisualScripting.IPoolable, global::System.IDisposable
	{
		private struct RecursionNode : global::System.IEquatable<global::Unity.VisualScripting.Flow.RecursionNode>
		{
			public global::Unity.VisualScripting.IUnitPort port { get; }

			public global::Unity.VisualScripting.IGraphParent context { get; }

			public RecursionNode(global::Unity.VisualScripting.IUnitPort port, global::Unity.VisualScripting.GraphPointer pointer)
			{
				this.port = port;
				context = pointer.parent;
			}

			public bool Equals(global::Unity.VisualScripting.Flow.RecursionNode other)
			{
				if (other.port == port)
				{
					return other.context == context;
				}
				return false;
			}

			public override bool Equals(object obj)
			{
				if (obj is global::Unity.VisualScripting.Flow.RecursionNode other)
				{
					return Equals(other);
				}
				return false;
			}

			public override int GetHashCode()
			{
				return global::Unity.VisualScripting.HashUtility.GetHashCode(port, context);
			}
		}

		private global::Unity.VisualScripting.Recursion<global::Unity.VisualScripting.Flow.RecursionNode> recursion;

		private readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IUnitValuePort, object> locals = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IUnitValuePort, object>();

		public readonly global::Unity.VisualScripting.VariableDeclarations variables = new global::Unity.VisualScripting.VariableDeclarations();

		private readonly global::System.Collections.Generic.Stack<int> loops = new global::System.Collections.Generic.Stack<int>();

		private readonly global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphStack> preservedStacks = new global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphStack>();

		private global::System.Collections.Generic.ICollection<global::Unity.VisualScripting.Flow> activeCoroutinesRegistry;

		private bool coroutineStopRequested;

		private global::System.Collections.IEnumerator coroutineEnumerator;

		private bool disposed;

		public int loopIdentifier = -1;

		public global::Unity.VisualScripting.GraphStack stack { get; private set; }

		public global::UnityEngine.MonoBehaviour coroutineRunner { get; private set; }

		public bool isCoroutine { get; private set; }

		public bool isPrediction { get; private set; }

		public bool enableDebug
		{
			get
			{
				if (isPrediction)
				{
					return false;
				}
				if (!stack.hasDebugData)
				{
					return false;
				}
				return true;
			}
		}

		public static global::System.Func<global::Unity.VisualScripting.GraphPointer, bool> isInspectedBinding { get; set; }

		public bool isInspected => isInspectedBinding?.Invoke(stack) ?? false;

		public int currentLoop
		{
			get
			{
				if (loops.Count > 0)
				{
					return loops.Peek();
				}
				return -1;
			}
		}

		private Flow()
		{
		}

		public static global::Unity.VisualScripting.Flow New(global::Unity.VisualScripting.GraphReference reference)
		{
			global::Unity.VisualScripting.Ensure.That("reference").IsNotNull(reference);
			global::Unity.VisualScripting.Flow flow = global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.Flow>.New(() => new global::Unity.VisualScripting.Flow());
			flow.stack = reference.ToStackPooled();
			return flow;
		}

		void global::Unity.VisualScripting.IPoolable.New()
		{
			disposed = false;
			recursion = global::Unity.VisualScripting.Recursion<global::Unity.VisualScripting.Flow.RecursionNode>.New();
		}

		public void Dispose()
		{
			if (disposed)
			{
				throw new global::System.ObjectDisposedException(ToString());
			}
			global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.Flow>.Free(this);
		}

		void global::Unity.VisualScripting.IPoolable.Free()
		{
			stack?.Dispose();
			recursion?.Dispose();
			locals.Clear();
			loops.Clear();
			variables.Clear();
			foreach (global::Unity.VisualScripting.GraphStack preservedStack in preservedStacks)
			{
				preservedStack.Dispose();
			}
			preservedStacks.Clear();
			loopIdentifier = -1;
			stack = null;
			recursion = null;
			isCoroutine = false;
			coroutineEnumerator = null;
			coroutineRunner = null;
			activeCoroutinesRegistry?.Remove(this);
			activeCoroutinesRegistry = null;
			coroutineStopRequested = false;
			isPrediction = false;
			disposed = true;
		}

		public global::Unity.VisualScripting.GraphStack PreserveStack()
		{
			global::Unity.VisualScripting.GraphStack graphStack = stack.Clone();
			preservedStacks.Add(graphStack);
			return graphStack;
		}

		public void RestoreStack(global::Unity.VisualScripting.GraphStack stack)
		{
			this.stack.CopyFrom(stack);
		}

		public void DisposePreservedStack(global::Unity.VisualScripting.GraphStack stack)
		{
			stack.Dispose();
			preservedStacks.Remove(stack);
		}

		public bool LoopIsNotBroken(int loop)
		{
			return currentLoop == loop;
		}

		public int EnterLoop()
		{
			int num = ++loopIdentifier;
			loops.Push(num);
			return num;
		}

		public void BreakLoop()
		{
			if (currentLoop < 0)
			{
				throw new global::System.InvalidOperationException("No active loop to break.");
			}
			loops.Pop();
		}

		public void ExitLoop(int loop)
		{
			if (loop == currentLoop)
			{
				loops.Pop();
			}
		}

		public void Run(global::Unity.VisualScripting.ControlOutput port)
		{
			Invoke(port);
			Dispose();
		}

		public void StartCoroutine(global::Unity.VisualScripting.ControlOutput port, global::System.Collections.Generic.ICollection<global::Unity.VisualScripting.Flow> registry = null)
		{
			isCoroutine = true;
			coroutineRunner = stack.component;
			if (coroutineRunner == null)
			{
				coroutineRunner = global::Unity.VisualScripting.CoroutineRunner.instance;
			}
			activeCoroutinesRegistry = registry;
			activeCoroutinesRegistry?.Add(this);
			coroutineEnumerator = Coroutine(port);
			coroutineRunner.StartCoroutine(coroutineEnumerator);
		}

		public void StopCoroutine(bool disposeInstantly)
		{
			if (!isCoroutine)
			{
				throw new global::System.NotSupportedException("Stop may only be called on coroutines.");
			}
			if (disposeInstantly)
			{
				StopCoroutineImmediate();
			}
			else
			{
				coroutineStopRequested = true;
			}
		}

		internal void StopCoroutineImmediate()
		{
			if ((bool)coroutineRunner && coroutineEnumerator != null)
			{
				coroutineRunner.StopCoroutine(coroutineEnumerator);
				((global::System.IDisposable)coroutineEnumerator).Dispose();
			}
		}

		private global::System.Collections.IEnumerator Coroutine(global::Unity.VisualScripting.ControlOutput startPort)
		{
			try
			{
				foreach (object item in InvokeCoroutine(startPort))
				{
					if (coroutineStopRequested)
					{
						yield break;
					}
					yield return item;
					if (coroutineStopRequested)
					{
						yield break;
					}
				}
			}
			finally
			{
				global::Unity.VisualScripting.Flow flow = this;
				if (!flow.disposed)
				{
					flow.Dispose();
				}
			}
		}

		public void Invoke(global::Unity.VisualScripting.ControlOutput output)
		{
			global::Unity.VisualScripting.Ensure.That("output").IsNotNull(output);
			global::Unity.VisualScripting.ControlConnection connection = output.connection;
			if (connection == null)
			{
				return;
			}
			global::Unity.VisualScripting.ControlInput destination = connection.destination;
			global::Unity.VisualScripting.Flow.RecursionNode recursionNode = new global::Unity.VisualScripting.Flow.RecursionNode(output, stack);
			BeforeInvoke(output, recursionNode);
			try
			{
				global::Unity.VisualScripting.ControlOutput controlOutput = InvokeDelegate(destination);
				if (controlOutput != null)
				{
					Invoke(controlOutput);
				}
			}
			finally
			{
				AfterInvoke(output, recursionNode);
			}
		}

		private global::System.Collections.IEnumerable InvokeCoroutine(global::Unity.VisualScripting.ControlOutput output)
		{
			global::Unity.VisualScripting.ControlConnection connection = output.connection;
			if (connection == null)
			{
				yield break;
			}
			global::Unity.VisualScripting.ControlInput destination = connection.destination;
			global::Unity.VisualScripting.Flow.RecursionNode recursionNode = new global::Unity.VisualScripting.Flow.RecursionNode(output, stack);
			BeforeInvoke(output, recursionNode);
			if (destination.supportsCoroutine)
			{
				foreach (object item in InvokeCoroutineDelegate(destination))
				{
					if (item is global::Unity.VisualScripting.ControlOutput)
					{
						foreach (object item2 in InvokeCoroutine((global::Unity.VisualScripting.ControlOutput)item))
						{
							yield return item2;
						}
					}
					else
					{
						yield return item;
					}
				}
			}
			else
			{
				global::Unity.VisualScripting.ControlOutput controlOutput = InvokeDelegate(destination);
				if (controlOutput != null)
				{
					foreach (object item3 in InvokeCoroutine(controlOutput))
					{
						yield return item3;
					}
				}
			}
			AfterInvoke(output, recursionNode);
		}

		private global::Unity.VisualScripting.Flow.RecursionNode BeforeInvoke(global::Unity.VisualScripting.ControlOutput output, global::Unity.VisualScripting.Flow.RecursionNode recursionNode)
		{
			try
			{
				recursion?.Enter(recursionNode);
			}
			catch (global::System.StackOverflowException ex)
			{
				output.unit.HandleException(stack, ex);
				throw;
			}
			global::Unity.VisualScripting.ControlConnection connection = output.connection;
			global::Unity.VisualScripting.ControlInput destination = connection.destination;
			if (enableDebug)
			{
				global::Unity.VisualScripting.IUnitConnectionDebugData elementDebugData = stack.GetElementDebugData<global::Unity.VisualScripting.IUnitConnectionDebugData>(connection);
				global::Unity.VisualScripting.IUnitDebugData elementDebugData2 = stack.GetElementDebugData<global::Unity.VisualScripting.IUnitDebugData>(destination.unit);
				elementDebugData.lastInvokeFrame = global::Unity.VisualScripting.EditorTimeBinding.frame;
				elementDebugData.lastInvokeTime = global::Unity.VisualScripting.EditorTimeBinding.time;
				elementDebugData2.lastInvokeFrame = global::Unity.VisualScripting.EditorTimeBinding.frame;
				elementDebugData2.lastInvokeTime = global::Unity.VisualScripting.EditorTimeBinding.time;
			}
			return recursionNode;
		}

		private void AfterInvoke(global::Unity.VisualScripting.ControlOutput output, global::Unity.VisualScripting.Flow.RecursionNode recursionNode)
		{
			recursion?.Exit(recursionNode);
		}

		private global::Unity.VisualScripting.ControlOutput InvokeDelegate(global::Unity.VisualScripting.ControlInput input)
		{
			try
			{
				if (input.requiresCoroutine)
				{
					throw new global::System.InvalidOperationException($"Port '{input.key}' on '{input.unit}' can only be triggered in a coroutine.");
				}
				return input.action(this);
			}
			catch (global::System.Exception ex)
			{
				input.unit.HandleException(stack, ex);
				throw;
			}
		}

		private global::System.Collections.IEnumerable InvokeCoroutineDelegate(global::Unity.VisualScripting.ControlInput input)
		{
			global::System.Collections.IEnumerator instructions = input.coroutineAction(this);
			while (true)
			{
				object current;
				try
				{
					if (!instructions.MoveNext())
					{
						break;
					}
					current = instructions.Current;
				}
				catch (global::System.Exception ex)
				{
					input.unit.HandleException(stack, ex);
					throw;
				}
				yield return current;
			}
		}

		public bool IsLocal(global::Unity.VisualScripting.IUnitValuePort port)
		{
			global::Unity.VisualScripting.Ensure.That("port").IsNotNull(port);
			return locals.ContainsKey(port);
		}

		public void SetValue(global::Unity.VisualScripting.IUnitValuePort port, object value)
		{
			global::Unity.VisualScripting.Ensure.That("port").IsNotNull(port);
			global::Unity.VisualScripting.Ensure.That("value").IsOfType(value, port.type);
			if (locals.ContainsKey(port))
			{
				locals[port] = value;
			}
			else
			{
				locals.Add(port, value);
			}
		}

		public object GetValue(global::Unity.VisualScripting.ValueInput input)
		{
			if (locals.TryGetValue(input, out var value))
			{
				return value;
			}
			global::Unity.VisualScripting.ValueConnection connection = input.connection;
			if (connection != null)
			{
				if (enableDebug)
				{
					global::Unity.VisualScripting.IUnitConnectionDebugData elementDebugData = stack.GetElementDebugData<global::Unity.VisualScripting.IUnitConnectionDebugData>(connection);
					elementDebugData.lastInvokeFrame = global::Unity.VisualScripting.EditorTimeBinding.frame;
					elementDebugData.lastInvokeTime = global::Unity.VisualScripting.EditorTimeBinding.time;
				}
				global::Unity.VisualScripting.ValueOutput source = connection.source;
				object value2 = GetValue(source);
				if (enableDebug)
				{
					global::Unity.VisualScripting.ValueConnection.DebugData elementDebugData2 = stack.GetElementDebugData<global::Unity.VisualScripting.ValueConnection.DebugData>(connection);
					elementDebugData2.lastValue = value2;
					elementDebugData2.assignedLastValue = true;
				}
				return value2;
			}
			if (TryGetDefaultValue(input, out var defaultValue))
			{
				return defaultValue;
			}
			throw new global::Unity.VisualScripting.MissingValuePortInputException(input.key);
		}

		private object GetValue(global::Unity.VisualScripting.ValueOutput output)
		{
			if (locals.TryGetValue(output, out var value))
			{
				return value;
			}
			if (!output.supportsFetch)
			{
				throw new global::System.InvalidOperationException($"The value of '{output.key}' on '{output.unit}' cannot be fetched dynamically, it must be assigned.");
			}
			global::Unity.VisualScripting.Flow.RecursionNode o = new global::Unity.VisualScripting.Flow.RecursionNode(output, stack);
			try
			{
				recursion?.Enter(o);
			}
			catch (global::System.StackOverflowException ex)
			{
				output.unit.HandleException(stack, ex);
				throw;
			}
			try
			{
				if (enableDebug)
				{
					global::Unity.VisualScripting.IUnitDebugData elementDebugData = stack.GetElementDebugData<global::Unity.VisualScripting.IUnitDebugData>(output.unit);
					elementDebugData.lastInvokeFrame = global::Unity.VisualScripting.EditorTimeBinding.frame;
					elementDebugData.lastInvokeTime = global::Unity.VisualScripting.EditorTimeBinding.time;
				}
				return GetValueDelegate(output);
			}
			finally
			{
				recursion?.Exit(o);
			}
		}

		public object GetValue(global::Unity.VisualScripting.ValueInput input, global::System.Type type)
		{
			return global::Unity.VisualScripting.ConversionUtility.Convert(GetValue(input), type);
		}

		public T GetValue<T>(global::Unity.VisualScripting.ValueInput input)
		{
			return (T)GetValue(input, typeof(T));
		}

		public object GetConvertedValue(global::Unity.VisualScripting.ValueInput input)
		{
			return GetValue(input, input.type);
		}

		private object GetDefaultValue(global::Unity.VisualScripting.ValueInput input)
		{
			if (!TryGetDefaultValue(input, out var defaultValue))
			{
				throw new global::System.InvalidOperationException("Value input port does not have a default value.");
			}
			return defaultValue;
		}

		public bool TryGetDefaultValue(global::Unity.VisualScripting.ValueInput input, out object defaultValue)
		{
			if (!input.unit.defaultValues.TryGetValue(input.key, out defaultValue))
			{
				return false;
			}
			if (input.nullMeansSelf && defaultValue == null)
			{
				defaultValue = stack.self;
			}
			return true;
		}

		private object GetValueDelegate(global::Unity.VisualScripting.ValueOutput output)
		{
			try
			{
				return output.getValue(this);
			}
			catch (global::System.Exception ex)
			{
				output.unit.HandleException(stack, ex);
				throw;
			}
		}

		public static object FetchValue(global::Unity.VisualScripting.ValueInput input, global::Unity.VisualScripting.GraphReference reference)
		{
			global::Unity.VisualScripting.Flow flow = New(reference);
			object value = flow.GetValue(input);
			flow.Dispose();
			return value;
		}

		public static object FetchValue(global::Unity.VisualScripting.ValueInput input, global::System.Type type, global::Unity.VisualScripting.GraphReference reference)
		{
			return global::Unity.VisualScripting.ConversionUtility.Convert(FetchValue(input, reference), type);
		}

		public static T FetchValue<T>(global::Unity.VisualScripting.ValueInput input, global::Unity.VisualScripting.GraphReference reference)
		{
			return (T)FetchValue(input, typeof(T), reference);
		}

		public static bool CanPredict(global::Unity.VisualScripting.IUnitValuePort port, global::Unity.VisualScripting.GraphReference reference)
		{
			global::Unity.VisualScripting.Ensure.That("port").IsNotNull(port);
			global::Unity.VisualScripting.Flow flow = New(reference);
			flow.isPrediction = true;
			bool result;
			if (port is global::Unity.VisualScripting.ValueInput)
			{
				result = flow.CanPredict((global::Unity.VisualScripting.ValueInput)port);
			}
			else
			{
				if (!(port is global::Unity.VisualScripting.ValueOutput))
				{
					throw new global::System.NotSupportedException();
				}
				result = flow.CanPredict((global::Unity.VisualScripting.ValueOutput)port);
			}
			flow.Dispose();
			return result;
		}

		private bool CanPredict(global::Unity.VisualScripting.ValueInput input)
		{
			if (!input.hasValidConnection)
			{
				if (!TryGetDefaultValue(input, out var defaultValue))
				{
					return false;
				}
				if (typeof(global::UnityEngine.Component).IsAssignableFrom(input.type))
				{
					defaultValue = defaultValue?.ConvertTo(input.type);
				}
				if (!input.allowsNull && defaultValue == null)
				{
					return false;
				}
				return true;
			}
			global::Unity.VisualScripting.ValueOutput output = global::System.Linq.Enumerable.Single(input.validConnectedPorts);
			if (!CanPredict(output))
			{
				return false;
			}
			object obj = GetValue(output);
			if (!global::Unity.VisualScripting.ConversionUtility.CanConvert(obj, input.type, guaranteed: false))
			{
				return false;
			}
			if (typeof(global::UnityEngine.Component).IsAssignableFrom(input.type))
			{
				obj = obj?.ConvertTo(input.type);
			}
			if (!input.allowsNull && obj == null)
			{
				return false;
			}
			return true;
		}

		private bool CanPredict(global::Unity.VisualScripting.ValueOutput output)
		{
			if (!output.supportsPrediction)
			{
				return false;
			}
			global::Unity.VisualScripting.Flow.RecursionNode o = new global::Unity.VisualScripting.Flow.RecursionNode(output, stack);
			global::Unity.VisualScripting.Recursion<global::Unity.VisualScripting.Flow.RecursionNode> obj = recursion;
			if (obj != null && !obj.TryEnter(o))
			{
				return false;
			}
			foreach (global::Unity.VisualScripting.IUnitRelation item in output.unit.relations.WithDestination(output))
			{
				if (item.source is global::Unity.VisualScripting.ValueInput)
				{
					global::Unity.VisualScripting.ValueInput input = (global::Unity.VisualScripting.ValueInput)item.source;
					if (!CanPredict(input))
					{
						recursion?.Exit(o);
						return false;
					}
				}
			}
			bool result = CanPredictDelegate(output);
			global::Unity.VisualScripting.Recursion<global::Unity.VisualScripting.Flow.RecursionNode> obj2 = recursion;
			if (obj2 != null)
			{
				obj2.Exit(o);
				return result;
			}
			return result;
		}

		private bool CanPredictDelegate(global::Unity.VisualScripting.ValueOutput output)
		{
			try
			{
				return output.canPredictValue(this);
			}
			catch (global::System.Exception arg)
			{
				global::UnityEngine.Debug.LogWarning($"Prediction check failed for '{output.key}' on '{output.unit}':\n{arg}");
				return false;
			}
		}

		public static object Predict(global::Unity.VisualScripting.IUnitValuePort port, global::Unity.VisualScripting.GraphReference reference)
		{
			global::Unity.VisualScripting.Ensure.That("port").IsNotNull(port);
			global::Unity.VisualScripting.Flow flow = New(reference);
			flow.isPrediction = true;
			object value;
			if (port is global::Unity.VisualScripting.ValueInput)
			{
				value = flow.GetValue((global::Unity.VisualScripting.ValueInput)port);
			}
			else
			{
				if (!(port is global::Unity.VisualScripting.ValueOutput))
				{
					throw new global::System.NotSupportedException();
				}
				value = flow.GetValue((global::Unity.VisualScripting.ValueOutput)port);
			}
			flow.Dispose();
			return value;
		}

		public static object Predict(global::Unity.VisualScripting.IUnitValuePort port, global::Unity.VisualScripting.GraphReference reference, global::System.Type type)
		{
			return global::Unity.VisualScripting.ConversionUtility.Convert(Predict(port, reference), type);
		}

		public static T Predict<T>(global::Unity.VisualScripting.IUnitValuePort port, global::Unity.VisualScripting.GraphReference pointer)
		{
			return (T)Predict(port, pointer, typeof(T));
		}
	}
}
