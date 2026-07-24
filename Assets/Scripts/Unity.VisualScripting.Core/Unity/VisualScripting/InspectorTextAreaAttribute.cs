namespace Unity.VisualScripting
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Property | global::System.AttributeTargets.Field | global::System.AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorTextAreaAttribute : global::System.Attribute
	{
		private float? _minLines;

		private float? _maxLines;

		public float minLines
		{
			get
			{
				return _minLines.GetValueOrDefault();
			}
			set
			{
				_minLines = value;
			}
		}

		public bool hasMinLines => _minLines.HasValue;

		public float maxLines
		{
			get
			{
				return _maxLines.GetValueOrDefault();
			}
			set
			{
				_maxLines = value;
			}
		}

		public bool hasMaxLines => _maxLines.HasValue;
	}
}
