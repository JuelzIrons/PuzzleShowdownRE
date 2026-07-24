namespace DG.Tweening.Core
{
	internal struct SafeModeReport
	{
		internal enum SafeModeReportType
		{
			Unset = 0,
			TargetOrFieldMissing = 1,
			Callback = 2,
			StartupFailure = 3
		}

		public int totMissingTargetOrFieldErrors { get; private set; }

		public int totCallbackErrors { get; private set; }

		public int totStartupErrors { get; private set; }

		public int totUnsetErrors { get; private set; }

		public void Add(global::DG.Tweening.Core.SafeModeReport.SafeModeReportType type)
		{
			switch (type)
			{
			case global::DG.Tweening.Core.SafeModeReport.SafeModeReportType.TargetOrFieldMissing:
				totMissingTargetOrFieldErrors++;
				break;
			case global::DG.Tweening.Core.SafeModeReport.SafeModeReportType.Callback:
				totCallbackErrors++;
				break;
			case global::DG.Tweening.Core.SafeModeReport.SafeModeReportType.StartupFailure:
				totStartupErrors++;
				break;
			default:
				totUnsetErrors++;
				break;
			}
		}

		public int GetTotErrors()
		{
			return totMissingTargetOrFieldErrors + totCallbackErrors + totStartupErrors + totUnsetErrors;
		}
	}
}
