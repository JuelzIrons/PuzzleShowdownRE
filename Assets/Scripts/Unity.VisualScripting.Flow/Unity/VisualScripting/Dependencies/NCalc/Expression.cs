namespace Unity.VisualScripting.Dependencies.NCalc
{
	public class Expression
	{
		protected readonly string OriginalExpression;

		protected global::System.Collections.Generic.Dictionary<string, global::System.Collections.IEnumerator> ParameterEnumerators;

		private global::System.Collections.Generic.Dictionary<string, object> _parameters;

		private static bool _cacheEnabled = true;

		private static global::System.Collections.Generic.Dictionary<string, global::System.WeakReference> _compiledExpressions = new global::System.Collections.Generic.Dictionary<string, global::System.WeakReference>();

		private static readonly global::System.Threading.ReaderWriterLock Rwl = new global::System.Threading.ReaderWriterLock();

		public global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions Options { get; set; }

		public string Error { get; private set; }

		public global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression ParsedExpression { get; private set; }

		public global::System.Collections.Generic.Dictionary<string, object> Parameters
		{
			get
			{
				return _parameters ?? (_parameters = new global::System.Collections.Generic.Dictionary<string, object>());
			}
			set
			{
				_parameters = value;
			}
		}

		public static bool CacheEnabled
		{
			get
			{
				return _cacheEnabled;
			}
			set
			{
				_cacheEnabled = value;
				if (!CacheEnabled)
				{
					_compiledExpressions = new global::System.Collections.Generic.Dictionary<string, global::System.WeakReference>();
				}
			}
		}

		public event global::Unity.VisualScripting.Dependencies.NCalc.EvaluateFunctionHandler EvaluateFunction;

		public event global::Unity.VisualScripting.Dependencies.NCalc.EvaluateParameterHandler EvaluateParameter;

		private Expression()
		{
			Parameters["null"] = (Parameters["NULL"] = null);
		}

		public Expression(string expression, global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions options = global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions.None)
			: this()
		{
			if (string.IsNullOrEmpty(expression))
			{
				throw new global::System.ArgumentException("Expression can't be empty", "expression");
			}
			expression = expression.Replace('"', '\'');
			OriginalExpression = expression;
			Options = options;
		}

		public Expression(global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression expression, global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions options = global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions.None)
			: this()
		{
			if (expression == null)
			{
				throw new global::System.ArgumentException("Expression can't be null", "expression");
			}
			ParsedExpression = expression;
			Options = options;
		}

		public void UpdateUnityTimeParameters()
		{
			global::System.Collections.Generic.Dictionary<string, object> parameters = Parameters;
			object value = (Parameters["DT"] = global::UnityEngine.Time.deltaTime);
			parameters["dt"] = value;
			global::System.Collections.Generic.Dictionary<string, object> parameters2 = Parameters;
			value = (Parameters["Second"] = 1f / global::UnityEngine.Time.deltaTime);
			parameters2["second"] = value;
		}

		public bool HasErrors()
		{
			try
			{
				if (ParsedExpression == null)
				{
					ParsedExpression = Compile(OriginalExpression, (Options & global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions.NoCache) == global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions.NoCache);
				}
				return ParsedExpression != null && Error != null;
			}
			catch (global::System.Exception ex)
			{
				Error = ex.Message;
				return true;
			}
		}

		public object Evaluate(global::Unity.VisualScripting.Flow flow)
		{
			if (HasErrors())
			{
				throw new global::Unity.VisualScripting.Dependencies.NCalc.EvaluationException(Error);
			}
			if (ParsedExpression == null)
			{
				ParsedExpression = Compile(OriginalExpression, (Options & global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions.NoCache) == global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions.NoCache);
			}
			global::Unity.VisualScripting.Dependencies.NCalc.EvaluationVisitor evaluationVisitor = new global::Unity.VisualScripting.Dependencies.NCalc.EvaluationVisitor(flow, Options);
			evaluationVisitor.EvaluateFunction += this.EvaluateFunction;
			evaluationVisitor.EvaluateParameter += this.EvaluateParameter;
			evaluationVisitor.Parameters = Parameters;
			if ((Options & global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions.IterateParameters) == global::Unity.VisualScripting.Dependencies.NCalc.EvaluateOptions.IterateParameters)
			{
				int num = -1;
				ParameterEnumerators = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.IEnumerator>();
				foreach (object value in Parameters.Values)
				{
					if (!(value is global::System.Collections.IEnumerable enumerable))
					{
						continue;
					}
					int num2 = 0;
					foreach (object item in enumerable)
					{
						_ = item;
						num2++;
					}
					if (num == -1)
					{
						num = num2;
					}
					else if (num2 != num)
					{
						throw new global::Unity.VisualScripting.Dependencies.NCalc.EvaluationException("When IterateParameters option is used, IEnumerable parameters must have the same number of items.");
					}
				}
				foreach (string key in Parameters.Keys)
				{
					if (Parameters[key] is global::System.Collections.IEnumerable enumerable2)
					{
						ParameterEnumerators.Add(key, enumerable2.GetEnumerator());
					}
				}
				global::System.Collections.Generic.List<object> list = new global::System.Collections.Generic.List<object>();
				for (int i = 0; i < num; i++)
				{
					foreach (string key2 in ParameterEnumerators.Keys)
					{
						global::System.Collections.IEnumerator enumerator5 = ParameterEnumerators[key2];
						enumerator5.MoveNext();
						Parameters[key2] = enumerator5.Current;
					}
					ParsedExpression.Accept(evaluationVisitor);
					list.Add(evaluationVisitor.Result);
				}
				return list;
			}
			ParsedExpression.Accept(evaluationVisitor);
			return evaluationVisitor.Result;
		}

		public static global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression Compile(string expression, bool noCache)
		{
			global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression logicalExpression = null;
			if (_cacheEnabled && !noCache)
			{
				try
				{
					Rwl.AcquireReaderLock(-1);
					if (_compiledExpressions.ContainsKey(expression))
					{
						global::System.WeakReference weakReference = _compiledExpressions[expression];
						logicalExpression = weakReference.Target as global::Unity.VisualScripting.Dependencies.NCalc.LogicalExpression;
						if (weakReference.IsAlive && logicalExpression != null)
						{
							return logicalExpression;
						}
					}
				}
				finally
				{
					Rwl.ReleaseReaderLock();
				}
			}
			if (logicalExpression == null)
			{
				global::Unity.VisualScripting.Dependencies.NCalc.NCalcParser nCalcParser = new global::Unity.VisualScripting.Dependencies.NCalc.NCalcParser(new global::Unity.VisualScripting.Antlr3.Runtime.CommonTokenStream(new global::Unity.VisualScripting.Dependencies.NCalc.NCalcLexer(new global::Unity.VisualScripting.Antlr3.Runtime.ANTLRStringStream(expression))));
				logicalExpression = nCalcParser.ncalcExpression().value;
				if (nCalcParser.Errors != null && nCalcParser.Errors.Count > 0)
				{
					throw new global::Unity.VisualScripting.Dependencies.NCalc.EvaluationException(string.Join(global::System.Environment.NewLine, nCalcParser.Errors.ToArray()));
				}
				if (_cacheEnabled && !noCache)
				{
					try
					{
						Rwl.AcquireWriterLock(-1);
						_compiledExpressions[expression] = new global::System.WeakReference(logicalExpression);
					}
					finally
					{
						Rwl.ReleaseWriterLock();
					}
					CleanCache();
				}
			}
			return logicalExpression;
		}

		private static void CleanCache()
		{
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			try
			{
				Rwl.AcquireWriterLock(-1);
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.WeakReference> compiledExpression in _compiledExpressions)
				{
					if (!compiledExpression.Value.IsAlive)
					{
						list.Add(compiledExpression.Key);
					}
				}
				foreach (string item in list)
				{
					_compiledExpressions.Remove(item);
				}
			}
			finally
			{
				Rwl.ReleaseReaderLock();
			}
		}
	}
}
