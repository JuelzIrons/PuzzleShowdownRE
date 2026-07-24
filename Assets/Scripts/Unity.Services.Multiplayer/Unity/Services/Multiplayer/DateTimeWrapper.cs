namespace Unity.Services.Multiplayer
{
	internal class DateTimeWrapper : global::Unity.Services.Multiplayer.IDateTimeWrapper
	{
		private static readonly global::System.DateTime k_UnixEpoch = new global::System.DateTime(1970, 1, 1, 0, 0, 0, 0, global::System.DateTimeKind.Utc);

		public global::System.DateTime UtcNow => global::System.DateTime.UtcNow;

		public double SecondsSinceUnixEpoch()
		{
			return global::System.Math.Round((global::System.DateTime.UtcNow - k_UnixEpoch).TotalSeconds);
		}
	}
}
