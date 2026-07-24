namespace Unity.VisualScripting.Dependencies.NCalc
{
	public class FunctionArgs : global::System.EventArgs
	{
		private object _result;

		private global::Unity.VisualScripting.Dependencies.NCalc.Expression[] _parameters = new global::Unity.VisualScripting.Dependencies.NCalc.Expression[0];

		public object Result
		{
			get
			{
				return _result;
			}
			set
			{
				_result = value;
				HasResult = true;
			}
		}

		public bool HasResult { get; set; }

		public global::Unity.VisualScripting.Dependencies.NCalc.Expression[] Parameters
		{
			get
			{
				return _parameters;
			}
			set
			{
				_parameters = value;
			}
		}

		public object[] EvaluateParameters(global::Unity.VisualScripting.Flow flow)
		{
			object[] array = new object[_parameters.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = _parameters[i].Evaluate(flow);
			}
			return array;
		}
	}
}
