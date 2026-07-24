namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.FlowGraph))]
	[global::Unity.VisualScripting.UnitCategory("Nesting")]
	[global::Unity.VisualScripting.UnitTitle("Subgraph")]
	[global::Unity.VisualScripting.RenamedFrom("Bolt.SuperUnit")]
	[global::Unity.VisualScripting.RenamedFrom("Unity.VisualScripting.SuperUnit")]
	[global::System.ComponentModel.DisplayName("Subgraph Node")]
	public sealed class SubgraphUnit : global::Unity.VisualScripting.NesterUnit<global::Unity.VisualScripting.FlowGraph, global::Unity.VisualScripting.ScriptGraphAsset>, global::Unity.VisualScripting.IGraphEventListener, global::Unity.VisualScripting.IGraphElementWithData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		public sealed class Data : global::Unity.VisualScripting.IGraphElementData
		{
			public bool isListening;
		}

		public global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.SubgraphUnit.Data();
		}

		public SubgraphUnit()
		{
		}

		public SubgraphUnit(global::Unity.VisualScripting.ScriptGraphAsset macro)
			: base(macro)
		{
		}

		public static global::Unity.VisualScripting.SubgraphUnit WithInputOutput()
		{
			global::Unity.VisualScripting.SubgraphUnit subgraphUnit = new global::Unity.VisualScripting.SubgraphUnit();
			subgraphUnit.nest.source = global::Unity.VisualScripting.GraphSource.Embed;
			subgraphUnit.nest.embed = global::Unity.VisualScripting.FlowGraph.WithInputOutput();
			return subgraphUnit;
		}

		public static global::Unity.VisualScripting.SubgraphUnit WithStartUpdate()
		{
			global::Unity.VisualScripting.SubgraphUnit subgraphUnit = new global::Unity.VisualScripting.SubgraphUnit();
			subgraphUnit.nest.source = global::Unity.VisualScripting.GraphSource.Embed;
			subgraphUnit.nest.embed = global::Unity.VisualScripting.FlowGraph.WithStartUpdate();
			return subgraphUnit;
		}

		public override global::Unity.VisualScripting.FlowGraph DefaultGraph()
		{
			return global::Unity.VisualScripting.FlowGraph.WithInputOutput();
		}

		protected override void Definition()
		{
			isControlRoot = true;
			foreach (global::Unity.VisualScripting.IUnitPortDefinition validPortDefinition in base.nest.graph.validPortDefinitions)
			{
				if (validPortDefinition is global::Unity.VisualScripting.ControlInputDefinition)
				{
					global::Unity.VisualScripting.ControlInputDefinition controlInputDefinition = (global::Unity.VisualScripting.ControlInputDefinition)validPortDefinition;
					string key = controlInputDefinition.key;
					ControlInput(key, delegate(global::Unity.VisualScripting.Flow flow)
					{
						foreach (global::Unity.VisualScripting.IUnit unit in base.nest.graph.units)
						{
							if (unit is global::Unity.VisualScripting.GraphInput)
							{
								global::Unity.VisualScripting.GraphInput obj2 = (global::Unity.VisualScripting.GraphInput)unit;
								flow.stack.EnterParentElement(this);
								return obj2.controlOutputs[key];
							}
						}
						return (global::Unity.VisualScripting.ControlOutput)null;
					});
				}
				else if (validPortDefinition is global::Unity.VisualScripting.ValueInputDefinition)
				{
					global::Unity.VisualScripting.ValueInputDefinition obj = (global::Unity.VisualScripting.ValueInputDefinition)validPortDefinition;
					string key2 = obj.key;
					global::System.Type type = obj.type;
					bool hasDefaultValue = obj.hasDefaultValue;
					object defaultValue = obj.defaultValue;
					global::Unity.VisualScripting.ValueInput valueInput = ValueInput(type, key2);
					if (hasDefaultValue)
					{
						valueInput.SetDefaultValue(defaultValue);
					}
				}
				else if (validPortDefinition is global::Unity.VisualScripting.ControlOutputDefinition)
				{
					string key3 = ((global::Unity.VisualScripting.ControlOutputDefinition)validPortDefinition).key;
					ControlOutput(key3);
				}
				else
				{
					if (!(validPortDefinition is global::Unity.VisualScripting.ValueOutputDefinition))
					{
						continue;
					}
					global::Unity.VisualScripting.ValueOutputDefinition valueOutputDefinition = (global::Unity.VisualScripting.ValueOutputDefinition)validPortDefinition;
					string key4 = valueOutputDefinition.key;
					global::System.Type type2 = valueOutputDefinition.type;
					ValueOutput(type2, key4, delegate(global::Unity.VisualScripting.Flow flow)
					{
						flow.stack.EnterParentElement(this);
						foreach (global::Unity.VisualScripting.IUnit unit2 in base.nest.graph.units)
						{
							if (unit2 is global::Unity.VisualScripting.GraphOutput)
							{
								global::Unity.VisualScripting.GraphOutput graphOutput = (global::Unity.VisualScripting.GraphOutput)unit2;
								object value = flow.GetValue(graphOutput.valueInputs[key4]);
								flow.stack.ExitParentElement();
								return value;
							}
						}
						flow.stack.ExitParentElement();
						throw new global::System.InvalidOperationException("Missing output node when to get value.");
					});
				}
			}
		}

		public void StartListening(global::Unity.VisualScripting.GraphStack stack)
		{
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StartListening(stack);
				stack.ExitParentElement();
			}
			stack.GetElementData<global::Unity.VisualScripting.SubgraphUnit.Data>(this).isListening = true;
		}

		public void StopListening(global::Unity.VisualScripting.GraphStack stack)
		{
			stack.GetElementData<global::Unity.VisualScripting.SubgraphUnit.Data>(this).isListening = false;
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StopListening(stack);
				stack.ExitParentElement();
			}
		}

		public bool IsListening(global::Unity.VisualScripting.GraphPointer pointer)
		{
			return pointer.GetElementData<global::Unity.VisualScripting.SubgraphUnit.Data>(this).isListening;
		}

		public override void AfterAdd()
		{
			base.AfterAdd();
			base.nest.beforeGraphChange += StopWatchingPortDefinitions;
			base.nest.afterGraphChange += StartWatchingPortDefinitions;
			StartWatchingPortDefinitions();
		}

		public override void BeforeRemove()
		{
			base.BeforeRemove();
			StopWatchingPortDefinitions();
			base.nest.beforeGraphChange -= StopWatchingPortDefinitions;
			base.nest.afterGraphChange -= StartWatchingPortDefinitions;
		}

		private void StopWatchingPortDefinitions()
		{
			if (base.nest.graph != null)
			{
				base.nest.graph.onPortDefinitionsChanged -= Define;
			}
		}

		private void StartWatchingPortDefinitions()
		{
			if (base.nest.graph != null)
			{
				base.nest.graph.onPortDefinitionsChanged += Define;
			}
		}
	}
}
