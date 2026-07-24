namespace Unity.VisualScripting
{
	public sealed class InvokeMember : global::Unity.VisualScripting.MemberUnit
	{
		private bool useExpandedParameters;

		[global::Unity.VisualScripting.DoNotSerialize]
		private int parameterCount;

		[global::Unity.VisualScripting.Serialize]
		private global::System.Collections.Generic.List<string> parameterNames;

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.InspectableIf("supportsChaining")]
		public bool chainable { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public bool supportsChaining => base.member.requiresTarget;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.MemberFilter(Methods = true, Constructors = true)]
		public global::Unity.VisualScripting.Member invocation
		{
			get
			{
				return base.member;
			}
			set
			{
				base.member = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.Dictionary<int, global::Unity.VisualScripting.ValueInput> inputParameters { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Target")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput targetOutput { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput result { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.Dictionary<int, global::Unity.VisualScripting.ValueOutput> outputParameters { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		public InvokeMember()
		{
		}

		public InvokeMember(global::Unity.VisualScripting.Member member)
			: base(member)
		{
		}

		public override bool HandleDependencies()
		{
			if (!base.HandleDependencies())
			{
				return false;
			}
			if (parameterNames == null && base.member.parameterTypes.Length == global::System.Linq.Enumerable.Count(base.defaultValues, (global::System.Collections.Generic.KeyValuePair<string, object> d) => d.Key != "target"))
			{
				parameterNames = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(base.defaultValues, (global::System.Collections.Generic.KeyValuePair<string, object> d) => d.Key != "target"), (global::System.Collections.Generic.KeyValuePair<string, object> defaultValue) => defaultValue.Key.Substring(1)));
			}
			return true;
		}

		protected override void Definition()
		{
			base.Definition();
			inputParameters = new global::System.Collections.Generic.Dictionary<int, global::Unity.VisualScripting.ValueInput>();
			outputParameters = new global::System.Collections.Generic.Dictionary<int, global::Unity.VisualScripting.ValueOutput>();
			useExpandedParameters = true;
			enter = ControlInput("enter", Enter);
			exit = ControlOutput("exit");
			Succession(enter, exit);
			if (base.member.requiresTarget)
			{
				Requirement(base.target, enter);
			}
			if (supportsChaining && chainable)
			{
				targetOutput = ValueOutput(base.member.targetType, "targetOutput");
				Assignment(enter, targetOutput);
			}
			if (base.member.isGettable)
			{
				result = ValueOutput(base.member.type, "result", Result);
				if (base.member.requiresTarget)
				{
					Requirement(base.target, result);
				}
			}
			global::System.Reflection.ParameterInfo[] array = global::System.Linq.Enumerable.ToArray(base.member.GetParameterInfos());
			parameterCount = array.Length;
			bool flag = false;
			for (int i = 0; i < parameterCount; i++)
			{
				global::System.Reflection.ParameterInfo parameterInfo = array[i];
				global::System.Type type = parameterInfo.UnderlyingParameterType();
				if (!parameterInfo.HasOutModifier())
				{
					string key = "%" + parameterInfo.Name;
					if (parameterNames != null && parameterNames[i] != parameterInfo.Name)
					{
						key = "%" + parameterNames[i];
						flag = true;
					}
					global::Unity.VisualScripting.ValueInput valueInput = ValueInput(type, key);
					inputParameters.Add(i, valueInput);
					valueInput.SetDefaultValue(parameterInfo.PseudoDefaultValue());
					if (parameterInfo.AllowsNull())
					{
						valueInput.AllowsNull();
					}
					Requirement(valueInput, enter);
					if (base.member.isGettable)
					{
						Requirement(valueInput, result);
					}
				}
				if (parameterInfo.ParameterType.IsByRef || parameterInfo.IsOut)
				{
					string key2 = "&" + parameterInfo.Name;
					if (parameterNames != null && parameterNames[i] != parameterInfo.Name)
					{
						key2 = "&" + parameterNames[i];
						flag = true;
					}
					global::Unity.VisualScripting.ValueOutput valueOutput = ValueOutput(type, key2);
					outputParameters.Add(i, valueOutput);
					Assignment(enter, valueOutput);
					useExpandedParameters = false;
				}
			}
			if (inputParameters.Count > 5)
			{
				useExpandedParameters = false;
			}
			if (parameterNames == null)
			{
				parameterNames = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(array, (global::System.Reflection.ParameterInfo pInfo) => pInfo.Name));
			}
		}

		private void PostDeserializeRemapParameterNames()
		{
			global::System.Reflection.ParameterInfo[] array = global::System.Linq.Enumerable.ToArray(base.member.GetParameterInfos());
			if (parameterNames?.Count != array.Length)
			{
				return;
			}
			global::System.Collections.Generic.List<(global::Unity.VisualScripting.ValueInput, global::Unity.VisualScripting.ValueOutput[])> list = null;
			global::System.Collections.Generic.List<(global::Unity.VisualScripting.ValueOutput, global::Unity.VisualScripting.ValueInput[])> list2 = null;
			global::System.Collections.Generic.List<(string, object)> list3 = null;
			for (int i = 0; i < array.Length; i++)
			{
				global::System.Reflection.ParameterInfo parameterInfo = array[i];
				string text = parameterNames[i];
				if (!(parameterInfo.Name != text))
				{
					continue;
				}
				global::Unity.VisualScripting.ValueOutput value3;
				if (base.valueInputs.TryGetValue("%" + text, out var value))
				{
					global::Unity.VisualScripting.ValueOutput[] array2 = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(value.validConnections, (global::Unity.VisualScripting.ValueConnection con) => con.source));
					global::Unity.VisualScripting.ValueOutput[] array3 = array2;
					for (int num = 0; num < array3.Length; num++)
					{
						array3[num].DisconnectFromValid(value);
					}
					base.valueInputs.Remove(value);
					if (list == null)
					{
						list = new global::System.Collections.Generic.List<(global::Unity.VisualScripting.ValueInput, global::Unity.VisualScripting.ValueOutput[])>(1);
					}
					list.Add((new global::Unity.VisualScripting.ValueInput("%" + parameterInfo.Name, parameterInfo.ParameterType), array2));
					if (base.defaultValues.TryGetValue(value.key, out var value2))
					{
						base.defaultValues.Remove(value.key);
						if (list3 == null)
						{
							list3 = new global::System.Collections.Generic.List<(string, object)>(1);
						}
						list3.Add(("%" + parameterInfo.Name, value2));
					}
				}
				else if (base.valueOutputs.TryGetValue("&" + text, out value3))
				{
					global::Unity.VisualScripting.ValueInput[] array4 = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(value3.validConnections, (global::Unity.VisualScripting.ValueConnection con) => con.destination));
					global::Unity.VisualScripting.ValueInput[] array5 = array4;
					for (int num = 0; num < array5.Length; num++)
					{
						array5[num].DisconnectFromValid(value3);
					}
					base.valueOutputs.Remove(value3);
					if (list2 == null)
					{
						list2 = new global::System.Collections.Generic.List<(global::Unity.VisualScripting.ValueOutput, global::Unity.VisualScripting.ValueInput[])>(1);
					}
					list2.Add((new global::Unity.VisualScripting.ValueOutput("&" + parameterInfo.Name, parameterInfo.ParameterType), array4));
				}
				parameterNames[i] = parameterInfo.Name;
			}
			if (list != null)
			{
				foreach (var item in list)
				{
					base.valueInputs.Add(item.Item1);
					global::Unity.VisualScripting.ValueOutput[] array3 = item.Item2;
					for (int num = 0; num < array3.Length; num++)
					{
						array3[num].ConnectToValid(item.Item1);
					}
				}
				if (list3 != null)
				{
					foreach (var item2 in list3)
					{
						base.defaultValues[item2.Item1] = item2.Item2;
					}
				}
			}
			if (list2 != null)
			{
				foreach (var item3 in list2)
				{
					base.valueOutputs.Add(item3.Item1);
					global::Unity.VisualScripting.ValueInput[] array5 = item3.Item2;
					for (int num = 0; num < array5.Length; num++)
					{
						array5[num].ConnectToValid(item3.Item1);
					}
				}
			}
			if (list != null || list2 != null)
			{
				Define();
			}
		}

		protected override bool IsMemberValid(global::Unity.VisualScripting.Member member)
		{
			return member.isInvocable;
		}

		private object Invoke(object target, global::Unity.VisualScripting.Flow flow)
		{
			if (useExpandedParameters)
			{
				return inputParameters.Count switch
				{
					0 => base.member.Invoke(target), 
					1 => base.member.Invoke(target, flow.GetConvertedValue(inputParameters[0])), 
					2 => base.member.Invoke(target, flow.GetConvertedValue(inputParameters[0]), flow.GetConvertedValue(inputParameters[1])), 
					3 => base.member.Invoke(target, flow.GetConvertedValue(inputParameters[0]), flow.GetConvertedValue(inputParameters[1]), flow.GetConvertedValue(inputParameters[2])), 
					4 => base.member.Invoke(target, flow.GetConvertedValue(inputParameters[0]), flow.GetConvertedValue(inputParameters[1]), flow.GetConvertedValue(inputParameters[2]), flow.GetConvertedValue(inputParameters[3])), 
					5 => base.member.Invoke(target, flow.GetConvertedValue(inputParameters[0]), flow.GetConvertedValue(inputParameters[1]), flow.GetConvertedValue(inputParameters[2]), flow.GetConvertedValue(inputParameters[3]), flow.GetConvertedValue(inputParameters[4])), 
					_ => throw new global::System.NotSupportedException(), 
				};
			}
			object[] array = new object[parameterCount];
			for (int i = 0; i < parameterCount; i++)
			{
				if (inputParameters.TryGetValue(i, out var value))
				{
					array[i] = flow.GetConvertedValue(value);
				}
			}
			object obj = base.member.Invoke(target, array);
			for (int j = 0; j < parameterCount; j++)
			{
				if (outputParameters.TryGetValue(j, out var value2))
				{
					flow.SetValue(value2, array[j]);
				}
			}
			return obj;
		}

		private object GetAndChainTarget(global::Unity.VisualScripting.Flow flow)
		{
			if (base.member.requiresTarget)
			{
				object value = flow.GetValue(base.target, base.member.targetType);
				if (supportsChaining && chainable)
				{
					flow.SetValue(targetOutput, value);
				}
				return value;
			}
			return null;
		}

		private object Result(global::Unity.VisualScripting.Flow flow)
		{
			object andChainTarget = GetAndChainTarget(flow);
			return Invoke(andChainTarget, flow);
		}

		private global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow)
		{
			object andChainTarget = GetAndChainTarget(flow);
			object value = Invoke(andChainTarget, flow);
			if (result != null)
			{
				flow.SetValue(result, value);
			}
			return exit;
		}

		public override global::Unity.VisualScripting.AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			string text = base.member.targetType.FullName + "." + base.member.name;
			if (base.member.parameterTypes != null)
			{
				text += "(";
				for (int i = 0; i < base.member.parameterTypes.Length; i++)
				{
					if (i >= 5)
					{
						text += $"->{i}";
						break;
					}
					text += base.member.parameterTypes[i].FullName;
					if (i < base.member.parameterTypes.Length - 1)
					{
						text += ", ";
					}
				}
				text += ")";
			}
			global::Unity.VisualScripting.AnalyticsIdentifier obj = new global::Unity.VisualScripting.AnalyticsIdentifier
			{
				Identifier = text,
				Namespace = base.member.targetType.Namespace
			};
			obj.Hashcode = obj.Identifier.GetHashCode();
			return obj;
		}
	}
}
