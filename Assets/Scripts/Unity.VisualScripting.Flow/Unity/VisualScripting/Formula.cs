namespace Unity.VisualScripting
{
	public sealed class Formula : global::Unity.VisualScripting.MultiInputUnit<object>
	{
		[global::Unity.VisualScripting.SerializeAs("Formula")]
		private string _formula;

		private global::Unity.VisualScripting.Dependencies.NCalc.Expression ncalc;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable]
		[global::Unity.VisualScripting.InspectorTextArea]
		public string formula
		{
			get
			{
				return _formula;
			}
			set
			{
				_formula = value;
				InitializeNCalc();
			}
		}

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable(order = int.MaxValue)]
		[global::Unity.VisualScripting.InspectorExpandTooltip]
		public bool cacheArguments { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput result { get; private set; }

		protected override int minInputCount => 0;

		protected override void Definition()
		{
			base.Definition();
			result = ValueOutput("result", Evaluate);
			InputsAllowNull();
			foreach (global::Unity.VisualScripting.ValueInput multiInput in base.multiInputs)
			{
				Requirement(multiInput, result);
			}
			InitializeNCalc();
		}

		private void InitializeNCalc()
		{
			if (string.IsNullOrEmpty(formula))
			{
				ncalc = null;
				return;
			}
			ncalc = new global::Unity.VisualScripting.Dependencies.NCalc.Expression(formula);
			ncalc.Options = global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions.IgnoreCase;
			ncalc.EvaluateParameter += EvaluateTreeParameter;
			ncalc.EvaluateFunction += EvaluateTreeFunction;
		}

		private object Evaluate(global::Unity.VisualScripting.Flow flow)
		{
			if (ncalc == null)
			{
				throw new global::System.InvalidOperationException("No formula provided.");
			}
			ncalc.UpdateUnityTimeParameters();
			return ncalc.Evaluate(flow);
		}

		private void EvaluateTreeFunction(global::Unity.VisualScripting.Flow flow, string name, global::Unity.VisualScripting.Dependencies.NCalc.FunctionArgs args)
		{
			switch (name)
			{
			case "v2":
			case "V2":
				if (args.Parameters.Length != 2)
				{
					throw new global::System.ArgumentException($"v2() takes at exactly 2 arguments. {args.Parameters.Length} provided.");
				}
				args.Result = new global::UnityEngine.Vector2(global::Unity.VisualScripting.ConversionUtility.Convert<float>(args.Parameters[0].Evaluate(flow)), global::Unity.VisualScripting.ConversionUtility.Convert<float>(args.Parameters[1].Evaluate(flow)));
				break;
			case "v3":
			case "V3":
				if (args.Parameters.Length != 3)
				{
					throw new global::System.ArgumentException($"v3() takes at exactly 3 arguments. {args.Parameters.Length} provided.");
				}
				args.Result = new global::UnityEngine.Vector3(global::Unity.VisualScripting.ConversionUtility.Convert<float>(args.Parameters[0].Evaluate(flow)), global::Unity.VisualScripting.ConversionUtility.Convert<float>(args.Parameters[1].Evaluate(flow)), global::Unity.VisualScripting.ConversionUtility.Convert<float>(args.Parameters[2].Evaluate(flow)));
				break;
			case "v4":
			case "V4":
				if (args.Parameters.Length != 4)
				{
					throw new global::System.ArgumentException($"v4() takes at exactly 4 arguments. {args.Parameters.Length} provided.");
				}
				args.Result = new global::UnityEngine.Vector4(global::Unity.VisualScripting.ConversionUtility.Convert<float>(args.Parameters[0].Evaluate(flow)), global::Unity.VisualScripting.ConversionUtility.Convert<float>(args.Parameters[1].Evaluate(flow)), global::Unity.VisualScripting.ConversionUtility.Convert<float>(args.Parameters[2].Evaluate(flow)), global::Unity.VisualScripting.ConversionUtility.Convert<float>(args.Parameters[3].Evaluate(flow)));
				break;
			}
		}

		public object GetParameterValue(global::Unity.VisualScripting.Flow flow, string name)
		{
			if (name.Length == 1)
			{
				char c = name[0];
				if (char.IsLetter(c))
				{
					c = char.ToLower(c);
					int argumentIndex = GetArgumentIndex(c);
					if (argumentIndex < base.multiInputs.Count)
					{
						global::Unity.VisualScripting.ValueInput valueInput = base.multiInputs[argumentIndex];
						if (cacheArguments && !flow.IsLocal(valueInput))
						{
							flow.SetValue(valueInput, flow.GetValue<object>(valueInput));
						}
						return flow.GetValue<object>(valueInput);
					}
				}
			}
			else
			{
				if (global::Unity.VisualScripting.Variables.Graph(flow.stack).IsDefined(name))
				{
					return global::Unity.VisualScripting.Variables.Graph(flow.stack).Get(name);
				}
				global::UnityEngine.GameObject self = flow.stack.self;
				if (self != null && global::Unity.VisualScripting.Variables.Object(self).IsDefined(name))
				{
					return global::Unity.VisualScripting.Variables.Object(self).Get(name);
				}
				global::UnityEngine.SceneManagement.Scene? scene = flow.stack.scene;
				if (scene.HasValue && global::Unity.VisualScripting.Variables.Scene(scene).IsDefined(name))
				{
					return global::Unity.VisualScripting.Variables.Scene(scene).Get(name);
				}
				if (global::Unity.VisualScripting.Variables.Application.IsDefined(name))
				{
					return global::Unity.VisualScripting.Variables.Application.Get(name);
				}
				if (global::Unity.VisualScripting.Variables.Saved.IsDefined(name))
				{
					return global::Unity.VisualScripting.Variables.Saved.Get(name);
				}
			}
			throw new global::System.InvalidOperationException("Unknown expression tree parameter: '" + name + "'.\nSupported parameter names are alphabetical indices and variable names.");
		}

		private void EvaluateTreeParameter(global::Unity.VisualScripting.Flow flow, string name, global::Unity.VisualScripting.Dependencies.NCalc.ParameterArgs args)
		{
			if (name.Contains("."))
			{
				string[] array = name.Split('.');
				if (array.Length != 2)
				{
					throw new global::System.InvalidOperationException("Cannot parse expression tree parameter: [" + name + "]");
				}
				string text = array[0];
				string text2 = array[1].TrimEnd("()");
				object parameterValue = GetParameterValue(flow, text);
				global::Unity.VisualScripting.Member member = new global::Unity.VisualScripting.Member(parameterValue.GetType(), text2, global::System.Type.EmptyTypes);
				object target = parameterValue;
				if (member.isInvocable)
				{
					args.Result = member.Invoke(target);
					return;
				}
				if (!member.isGettable)
				{
					throw new global::System.InvalidOperationException("Cannot get or invoke expression tree parameter: [" + text + "." + text2 + "]");
				}
				args.Result = member.Get(target);
			}
			else
			{
				args.Result = GetParameterValue(flow, name);
			}
		}

		public static string GetArgumentName(int index)
		{
			if (index > 25)
			{
				throw new global::System.NotImplementedException("Argument indices above 26 are not yet supported.");
			}
			return ((char)(97 + index)).ToString();
		}

		public static int GetArgumentIndex(char name)
		{
			if (name < 'a' || name > 'z')
			{
				throw new global::System.NotImplementedException("Unalphabetical argument names are not yet supported.");
			}
			return name - 97;
		}
	}
}
