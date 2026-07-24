namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(17)]
	[global::Unity.VisualScripting.UnitFooterPorts(ControlOutputs = true)]
	public sealed class TryCatch : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput @try { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput @catch { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput @finally { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput exception { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable]
		[global::Unity.VisualScripting.TypeFilter(new global::System.Type[] { typeof(global::System.Exception) }, Matching = global::Unity.VisualScripting.TypesMatching.AssignableToAll)]
		[global::Unity.VisualScripting.TypeSet(global::Unity.VisualScripting.TypeSet.SettingsAssembliesTypes)]
		public global::System.Type exceptionType { get; set; } = typeof(global::System.Exception);

		public override bool canDefine
		{
			get
			{
				if (exceptionType != null)
				{
					return typeof(global::System.Exception).IsAssignableFrom(exceptionType);
				}
				return false;
			}
		}

		protected override void Definition()
		{
			enter = ControlInput("enter", Enter);
			@try = ControlOutput("try");
			@catch = ControlOutput("catch");
			@finally = ControlOutput("finally");
			exception = ValueOutput(exceptionType, "exception");
			Assignment(enter, exception);
			Succession(enter, @try);
			Succession(enter, @catch);
			Succession(enter, @finally);
		}

		public global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow)
		{
			if (flow.isCoroutine)
			{
				throw new global::System.NotSupportedException("Coroutines cannot catch exceptions.");
			}
			try
			{
				flow.Invoke(@try);
			}
			catch (global::System.Exception ex)
			{
				if (!exceptionType.IsInstanceOfType(ex))
				{
					throw;
				}
				flow.SetValue(exception, ex);
				flow.Invoke(@catch);
			}
			finally
			{
				flow.Invoke(@finally);
			}
			return null;
		}
	}
}
