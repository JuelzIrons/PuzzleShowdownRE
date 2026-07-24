namespace UnityEngine.UnityConsent
{
	public struct ConsentState
	{
		public global::UnityEngine.UnityConsent.ConsentStatus AdsIntent;

		public global::UnityEngine.UnityConsent.ConsentStatus AnalyticsIntent;

		public ConsentState()
		{
			AdsIntent = global::UnityEngine.UnityConsent.ConsentStatus.Unspecified;
			AnalyticsIntent = global::UnityEngine.UnityConsent.ConsentStatus.Unspecified;
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3}", "AdsIntent", AdsIntent, "AnalyticsIntent", AnalyticsIntent);
		}
	}
}
