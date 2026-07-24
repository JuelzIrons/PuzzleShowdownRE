namespace UnityEngine.InputSystem.Layouts
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, Inherited = false)]
	public sealed class InputControlLayoutAttribute : global::System.Attribute
	{
		internal bool? canRunInBackgroundInternal;

		internal bool? updateBeforeRenderInternal;

		public global::System.Type stateType { get; set; }

		public string stateFormat { get; set; }

		public string[] commonUsages { get; set; }

		public string variants { get; set; }

		public bool isNoisy { get; set; }

		public bool canRunInBackground
		{
			get
			{
				return canRunInBackgroundInternal.Value;
			}
			set
			{
				canRunInBackgroundInternal = value;
			}
		}

		public bool updateBeforeRender
		{
			get
			{
				return updateBeforeRenderInternal.Value;
			}
			set
			{
				updateBeforeRenderInternal = value;
			}
		}

		public bool isGenericTypeOfDevice { get; set; }

		public string displayName { get; set; }

		public string description { get; set; }

		public bool hideInUI { get; set; }
	}
}
