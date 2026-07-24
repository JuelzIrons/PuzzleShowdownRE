namespace Unity.Burst
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Assembly | global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct | global::System.AttributeTargets.Method)]
	public class BurstCompileAttribute : global::System.Attribute
	{
		internal bool? _compileSynchronously;

		internal bool? _debug;

		internal bool? _disableSafetyChecks;

		internal bool? _disableDirectCall;

		public global::Unity.Burst.FloatMode FloatMode { get; set; }

		public global::Unity.Burst.FloatPrecision FloatPrecision { get; set; }

		public bool CompileSynchronously
		{
			get
			{
				if (!_compileSynchronously.HasValue)
				{
					return false;
				}
				return _compileSynchronously.Value;
			}
			set
			{
				_compileSynchronously = value;
			}
		}

		public bool Debug
		{
			get
			{
				if (!_debug.HasValue)
				{
					return false;
				}
				return _debug.Value;
			}
			set
			{
				_debug = value;
			}
		}

		public bool DisableSafetyChecks
		{
			get
			{
				if (!_disableSafetyChecks.HasValue)
				{
					return false;
				}
				return _disableSafetyChecks.Value;
			}
			set
			{
				_disableSafetyChecks = value;
			}
		}

		public bool DisableDirectCall
		{
			get
			{
				if (!_disableDirectCall.HasValue)
				{
					return false;
				}
				return _disableDirectCall.Value;
			}
			set
			{
				_disableDirectCall = value;
			}
		}

		public global::Unity.Burst.OptimizeFor OptimizeFor { get; set; }

		internal string[] Options { get; set; }

		public BurstCompileAttribute()
		{
		}

		public BurstCompileAttribute(global::Unity.Burst.FloatPrecision floatPrecision, global::Unity.Burst.FloatMode floatMode)
		{
			FloatMode = floatMode;
			FloatPrecision = floatPrecision;
		}

		internal BurstCompileAttribute(string[] options)
		{
			Options = options;
		}
	}
}
